using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.Correction.Dto;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByRoomService
    {
        private const int PrecisionForPrice = 2;
        private const int PrecisionForCoefficients = 4;

        public void UpdateMarketObjectsPrice()
        {
            var numberOfRooms = new long?[] { 1, 2, 3 };
            var statisticsBySegment = new Dictionary<MarketSegment, StatisticsBySegment>();

            var date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var history = GetHistory(date);
            var excludedBuildings = history.Where(x => x.IsExcluded.GetValueOrDefault()).Select(x => x.BuildingCadastralNumber).ToList();
            
            var objectsGroupedBySegment = OMCoreObject.Where(x =>
                    x.PropertyMarketSegment != null && x.BuildingCadastralNumber != null &&
                    x.RoomsCount != null && numberOfRooms.Contains(x.RoomsCount) &&
                    x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal)
                .SelectAll(false)
                //TODO remove
                .SetPackageSize(100000).SetPackageIndex(0)
                .Execute()
                .GroupBy(x => new {x.PropertyMarketSegment_Code}).ToList();
            
            objectsGroupedBySegment.ForEach(groupBySegment =>
            {
                var oneRoomCoefficients = new List<decimal>();
                var threeRoomsCoefficients = new List<decimal>();

                var objectsGroupedByBuilding = groupBySegment
                    .Where(x => !excludedBuildings.Contains(x.BuildingCadastralNumber)).ToList()
                    .GroupBy(x => x.BuildingCadastralNumber).ToList();
                objectsGroupedByBuilding.ForEach(groupByBuilding =>
                {
                    var objectsInBuilding = groupByBuilding.ToList();
                    if (IsBuildingContainAllRoomsTypes(objectsInBuilding))
                    {
                        var oneRoomAveragePricePerMeter = GetAveragePricePerMeter(objectsInBuilding, 1);
                        var twoRoomsAveragePricePerMeter = GetAveragePricePerMeter(objectsInBuilding, 2);
                        var threeRoomsAveragePricePerMeter = GetAveragePricePerMeter(objectsInBuilding, 3);

                        var oneRoomCoefficient = Math.Round(twoRoomsAveragePricePerMeter / oneRoomAveragePricePerMeter, PrecisionForCoefficients);
                        var threeRoomsCoefficient = Math.Round(twoRoomsAveragePricePerMeter / threeRoomsAveragePricePerMeter, PrecisionForCoefficients);

                        oneRoomCoefficients.Add(oneRoomCoefficient);
                        threeRoomsCoefficients.Add(threeRoomsCoefficient);

                        SaveHistory(history, date, groupByBuilding.Key, groupBySegment.Key.PropertyMarketSegment_Code, oneRoomCoefficient, threeRoomsCoefficient);
                    }
                });

                statisticsBySegment.Add(groupBySegment.Key.PropertyMarketSegment_Code,
                    new StatisticsBySegment(oneRoomCoefficients.DefaultIfEmpty().Average(),
                        threeRoomsCoefficients.DefaultIfEmpty().Average()));
            });

            CalculatePriceAfterCorrectionByRooms(statisticsBySegment);
        }

        public List<CorrectionByRoomHistoryDto> GetCorrectionByRoomGeneralHistory(long marketSegmentCode)
        {
            return OMPriceCorrectionByRoomsHistory.Where(x =>
                    x.MarketSegment_Code == (MarketSegment) marketSegmentCode &&
                    (x.IsExcluded == false || x.IsExcluded == null))
                .OrderByDescending(x => x.ChangingDate)
                .SelectAll().Execute().GroupBy(x => x.ChangingDate).Select(
                    group => new CorrectionByRoomHistoryDto
                    {
                        Date = group.Key,
                        OneRoomCoefficient = Math.Round(
                            group.ToList().DefaultIfEmpty().Average(x => x.OneRoomCoefficient),
                            PrecisionForCoefficients),
                        ThreeRoomsCoefficient = Math.Round(
                            group.ToList().DefaultIfEmpty().Average(x => x.ThreeRoomsCoefficient),
                            PrecisionForCoefficients)
                    }).ToList();
        }

        public List<CorrectionByRoomHistoryDto> GetCorrectionByRoomDetailedHistory(long marketSegmentCode, DateTime date)
        {
            return OMPriceCorrectionByRoomsHistory.Where(x =>
                    x.MarketSegment_Code == (MarketSegment) marketSegmentCode && x.ChangingDate == date)
                .OrderBy(x => x.BuildingCadastralNumber)
                .SelectAll().Execute().Select(
                    x => new CorrectionByRoomHistoryDto
                    {
                        Id = x.Id,
                        BuildingCadastralNumber = x.BuildingCadastralNumber,
                        OneRoomCoefficient = x.OneRoomCoefficient,
                        ThreeRoomsCoefficient = x.ThreeRoomsCoefficient,
                        IsExcludeFromCalculation = x.IsExcluded.GetValueOrDefault()
                    }).ToList();
        }

        public bool ChangeBuildingsStatusInCalculation(List<CorrectionByRoomHistoryDto> historyRecords)
        {
            if (historyRecords.Count == 0)
                return false;

            var isDataUpdated = false;
            historyRecords.ForEach(record =>
            {
                var recordFromDb = OMPriceCorrectionByRoomsHistory.Where(x => x.Id == record.Id).SelectAll().ExecuteFirstOrDefault();
                if (recordFromDb == null)
                    return;

                if (recordFromDb.IsExcluded != record.IsExcludeFromCalculation)
                    isDataUpdated = true;

                recordFromDb.IsExcluded = record.IsExcludeFromCalculation;
                recordFromDb.Save();
            });

            return isDataUpdated;
        }


        #region Support Methods

        private bool IsBuildingContainAllRoomsTypes(List<OMCoreObject> objectsInBuilding)
        {
            bool haveOneRoomApartment = false, haveTwoRoomsApartment = false, haveThreeRoomsApartment = false;

            objectsInBuilding.ForEach(obj =>
            {
                switch (obj.RoomsCount)
                {
                    case 1:
                        haveOneRoomApartment = true;
                        break;
                    case 2:
                        haveTwoRoomsApartment = true;
                        break;
                    case 3:
                        haveThreeRoomsApartment = true;
                        break;
                }
            });

            return haveOneRoomApartment && haveTwoRoomsApartment && haveThreeRoomsApartment;
        }

        private void CalculatePriceAfterCorrectionByRooms(Dictionary<MarketSegment, StatisticsBySegment> statistics)
        {
            var objects = OMCoreObject.Where(x => x.RoomsCount == 1 || x.RoomsCount == 3).SelectAll().Execute();
            objects.ForEach(x =>
            {
                if (!statistics.TryGetValue(x.PropertyMarketSegment_Code, out var coefficients))
                    return;

                var coefficient = 0m;
                switch (x.RoomsCount)
                {
                    case 1:
                        coefficient = coefficients.OneRoomCoefficient;
                        break;
                    case 3:
                        coefficient = coefficients.ThreeRoomsCoefficient;
                        break;
                }

                x.PriceAfterCorrectionByRooms = Math.Round(x.Price.GetValueOrDefault() * coefficient, PrecisionForPrice);
                x.Save();
            });
        }

        private decimal GetAveragePricePerMeter(IEnumerable<OMCoreObject> objects, int numberOfRooms)
        {
            return objects.Where(x => x.RoomsCount == numberOfRooms).Select(x => x.PricePerMeter.GetValueOrDefault()).Average();
        }

        private void SaveHistory(List<OMPriceCorrectionByRoomsHistory> history, DateTime date, string buildingCadastralNumber, 
            MarketSegment segment, decimal oneRoomCoefficient, decimal threeRoomsCoefficient)
        {
            var existedRecord = history.FirstOrDefault(x =>
                x.BuildingCadastralNumber == buildingCadastralNumber && x.MarketSegment_Code == segment &&
                x.ChangingDate == date);
            if (existedRecord == null)
            {
                new OMPriceCorrectionByRoomsHistory
                {
                    BuildingCadastralNumber = buildingCadastralNumber,
                    MarketSegment_Code = segment,
                    ChangingDate = date,
                    OneRoomCoefficient = oneRoomCoefficient,
                    ThreeRoomsCoefficient = threeRoomsCoefficient
                }.Save();
            }
            else
            {
                existedRecord.OneRoomCoefficient = oneRoomCoefficient;
                existedRecord.ThreeRoomsCoefficient = threeRoomsCoefficient;
                existedRecord.Save();
            }
        }

        private List<OMPriceCorrectionByRoomsHistory> GetHistory(DateTime date)
        {
            return OMPriceCorrectionByRoomsHistory.Where(x => x.ChangingDate == date).SelectAll().Execute().ToList();
        }

        private class StatisticsBySegment
        {
            public decimal OneRoomCoefficient { get; }
            public decimal ThreeRoomsCoefficient { get; }

            public StatisticsBySegment(decimal oneRoomCoefficient, decimal threeRoomsCoefficient)
            {
                OneRoomCoefficient = oneRoomCoefficient;
                ThreeRoomsCoefficient = threeRoomsCoefficient;
            }
        }

        #endregion
    }
}
