using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using KadOzenka.Dal.Correction.Dto;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByRoomService
    {
        private const int PrecisionForPrice = 2;
        private const int PrecisionForCoefficients = 4;

        public void UpdateMarketObjectsPrice(DateTime date)
        {
            var numberOfRooms = new long?[] { 1, 2, 3 };

            var coefficients = GetCoefficients(date);
            var excludedBuildings = coefficients.Where(x => x.IsExcluded.GetValueOrDefault()).Select(x => x.BuildingCadastralNumber).ToList();
            
            var objectsGroupedBySegment = OMCoreObject.Where(x =>
                    x.PropertyMarketSegment != null && x.BuildingCadastralNumber != null &&
                    x.RoomsCount != null && numberOfRooms.Contains(x.RoomsCount) &&
                    x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal)
                .SelectAll(false)
                .Execute()
                .GroupBy(x => new {x.PropertyMarketSegment_Code}).ToList();
            
            objectsGroupedBySegment.ForEach(groupBySegment =>
            {
                var objectsGroupedByBuilding = groupBySegment
                    .Where(x => !excludedBuildings.Contains(x.BuildingCadastralNumber))
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

                        SaveCoefficients(coefficients, date, groupByBuilding.Key, groupBySegment.Key.PropertyMarketSegment_Code, oneRoomCoefficient, threeRoomsCoefficient);
                    }
                });
            });

            CalculatePriceAfterCorrectionByRooms(date);
        }

        public List<CorrectionByRoomCoefficientsDto> GetAverageCoefficients(long marketSegmentCode)
        {
            return GetAverageCoefficients().Where(x => x.MarketSegment == (MarketSegment) marketSegmentCode).ToList();
        }

        public List<CorrectionByRoomCoefficientsDto> GetDetailedCoefficients(long marketSegmentCode, DateTime date)
        {
            return OMCoefficientsForCorrectionByRooms.Where(x =>
                    x.MarketSegment_Code == (MarketSegment) marketSegmentCode && x.ChangingDate == date)
                .OrderBy(x => x.BuildingCadastralNumber)
                .SelectAll().Execute().Select(
                    x => new CorrectionByRoomCoefficientsDto
                    {
                        Id = x.Id,
                        BuildingCadastralNumber = x.BuildingCadastralNumber,
                        OneRoomCoefficient = x.OneRoomCoefficient,
                        ThreeRoomsCoefficient = x.ThreeRoomsCoefficient,
                        IsExcludeFromCalculation = x.IsExcluded.GetValueOrDefault()
                    }).ToList();
        }

        public bool ChangeBuildingsStatusInCalculation(List<CorrectionByRoomCoefficientsDto> coefficients)
        {
            if (coefficients.Count == 0)
                return false;

            var isDataUpdated = false;
            coefficients.ForEach(record =>
            {
                var recordFromDb = OMCoefficientsForCorrectionByRooms.Where(x => x.Id == record.Id).SelectAll().ExecuteFirstOrDefault();
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

        private List<CorrectionByRoomCoefficientsDto> GetAverageCoefficients()
        {
            return OMCoefficientsForCorrectionByRooms.Where(x => x.IsExcluded == false || x.IsExcluded == null)
                .OrderByDescending(x => x.ChangingDate)
                .SelectAll().Execute()
                .GroupBy(x => new { x.MarketSegment_Code, x.ChangingDate }).Select(
                    group => new CorrectionByRoomCoefficientsDto
                    {
                        Date = group.Key.ChangingDate,
                        MarketSegment = group.Key.MarketSegment_Code,
                        OneRoomCoefficient = Math.Round(
                            group.ToList().DefaultIfEmpty().Average(x => x.OneRoomCoefficient),
                            PrecisionForCoefficients),
                        ThreeRoomsCoefficient = Math.Round(
                            group.ToList().DefaultIfEmpty().Average(x => x.ThreeRoomsCoefficient),
                            PrecisionForCoefficients)
                    }).ToList();
        }

        private void CalculatePriceAfterCorrectionByRooms(DateTime date)
        {
            var coefficients = GetAverageCoefficients().Where(x => x.Date == date);

            var objects = OMCoreObject.Where(x => x.RoomsCount == 1 || x.RoomsCount == 3).SelectAll().Execute();
            var objectsIds = objects.Select(x => x.Id);
            var priceChangingHistory = OMPriceAfterCorrectionByRoomsHistory.Where(x => objectsIds.Contains(x.InitialId)).SelectAll().Execute();

            objects.ForEach(obj =>
            {
                var coefficientByMarketSegment = coefficients.FirstOrDefault(x => x.MarketSegment == obj.PropertyMarketSegment_Code);
                if(coefficientByMarketSegment == null)
                    return;

                var coefficient = 0m;
                switch (obj.RoomsCount)
                {
                    case 1:
                        coefficient = coefficientByMarketSegment.OneRoomCoefficient;
                        break;
                    case 3:
                        coefficient = coefficientByMarketSegment.ThreeRoomsCoefficient;
                        break;
                }

                var newPriceAfterCorrectionByRoom = Math.Round(obj.Price.GetValueOrDefault() * coefficient, PrecisionForPrice);

                using (var ts = new TransactionScope())
                {
                    SavePriceChangingHistory(priceChangingHistory, obj, date, newPriceAfterCorrectionByRoom);

                    obj.PriceAfterCorrectionByRooms = newPriceAfterCorrectionByRoom;
                    obj.Save();

                    ts.Complete();
                }
            });
        }

        private decimal GetAveragePricePerMeter(IEnumerable<OMCoreObject> objects, int numberOfRooms)
        {
            return objects.Where(x => x.RoomsCount == numberOfRooms).Select(x => x.PricePerMeter.GetValueOrDefault()).Average();
        }

        private List<OMCoefficientsForCorrectionByRooms> GetCoefficients(DateTime date)
        {
            return OMCoefficientsForCorrectionByRooms.Where(x => x.ChangingDate == date).SelectAll().Execute().ToList();
        }

        private void SaveCoefficients(List<OMCoefficientsForCorrectionByRooms> coefficients, DateTime date, string buildingCadastralNumber, 
            MarketSegment segment, decimal oneRoomCoefficient, decimal threeRoomsCoefficient)
        {
            var existedRecord = coefficients.FirstOrDefault(x =>
                x.BuildingCadastralNumber == buildingCadastralNumber && x.MarketSegment_Code == segment &&
                x.ChangingDate == date);
            if (existedRecord == null)
            {
                new OMCoefficientsForCorrectionByRooms
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

        private void SavePriceChangingHistory(List<OMPriceAfterCorrectionByRoomsHistory> history, OMCoreObject obj,
            DateTime changingDate, decimal newPriceAfterCorrectionByRoom)
        {
            var existedRecord = history.FirstOrDefault(x => x.InitialId == obj.Id && x.ChangingDate == changingDate);
            if (existedRecord == null)
            {
                new OMPriceAfterCorrectionByRoomsHistory
                {
                    InitialId = obj.Id,
                    ChangingDate = changingDate,
                    PriceValueFrom = obj.PriceAfterCorrectionByRooms.GetValueOrDefault(),
                    PriceValueTo = newPriceAfterCorrectionByRoom
                }.Save();
            }
            else
            {
                existedRecord.PriceValueFrom = obj.PriceAfterCorrectionByRooms.GetValueOrDefault();
                existedRecord.PriceValueTo = newPriceAfterCorrectionByRoom;
                existedRecord.Save();
            }
        }

        #endregion
    }
}
