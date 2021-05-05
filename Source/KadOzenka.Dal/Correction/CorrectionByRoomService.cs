using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.Shared.Extensions;
using KadOzenka.Dal.Correction.Dto;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces.Corrections;
using ObjectModel.Directory;
using ObjectModel.Directory.MarketObjects;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByRoomService : CorrectionBaseService
    {
        public static readonly int PrecisionForPrice = 2;
        public static readonly int PrecisionForCoefficients = 4;
        private IMarketObjectsForCorrectionByRoom MarketObjectsService { get; }
        public List<MarketSegment> CalculatedMarketSegments => new List<MarketSegment>() {MarketSegment.MZHS};


        public CorrectionByRoomService()
        {
	        MarketObjectsService = new MarketObjectsForCorrectionsService();
        }


        public List<CorrectionByRoomCoefficientsDto> GetAverageCoefficients(long marketSegmentCode)
        {
            if (!CalculatedMarketSegments.Contains((MarketSegment) marketSegmentCode))
            {
                throw new Exception($"Данная корректировка определяется только для сегментов: {string.Join(", ", CalculatedMarketSegments.Select(x => x.GetEnumDescription()).ToList())}");
            }

            return GetAverageCoefficients().Where(x => x.MarketSegment == (MarketSegment) marketSegmentCode).ToList();
        }

        public List<CorrectionByRoomCoefficientsDto> GetAverageCoefficients(DateTime date)
        {
            return GetAverageCoefficients().Where(x => x.Date == date).ToList();
        }

        public List<CorrectionByRoomCoefficientsDto> GetDetailedCoefficients(long marketSegmentCode, DateTime date)
        {
            if (!CalculatedMarketSegments.Contains((MarketSegment)marketSegmentCode))
            {
                throw new Exception($"Данная корректировка определяется только для сегментов: {string.Join(", ", CalculatedMarketSegments.Select(x => x.GetEnumDescription()).ToList())}");
            }

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

        public bool IsBuildingContainAllRoomsTypes(List<OMCoreObject> objectsInBuilding)
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

        public decimal GetAveragePricePerMeter(IEnumerable<OMCoreObject> objects, int numberOfRooms)
        {
            return objects.Where(x => x.RoomsCount == numberOfRooms).Average(x => x.PricePerMeter.GetValueOrDefault());
        }

        public List<OMCoefficientsForCorrectionByRooms> GetCoefficients(DateTime date)
        {
            return OMCoefficientsForCorrectionByRooms.Where(x => x.ChangingDate == date).SelectAll().Execute().ToList();
        }

        public void SaveCoefficients(List<OMCoefficientsForCorrectionByRooms> coefficients, DateTime date, string buildingCadastralNumber,
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

        public bool IsOneRoomCoefIncludedInCalculationLimit(decimal? coefficientByBuildingQuarter)
        {
            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByRoom);
            var result = (!settings.LowerLimitForCoefficient.HasValue || coefficientByBuildingQuarter >= settings.LowerLimitForCoefficient.Value)
                         && (!settings.UpperLimitForCoefficient.HasValue || coefficientByBuildingQuarter <= settings.UpperLimitForCoefficient.Value);

            return result;
        }

        public bool IsThreeRoomsCoefIncludedInCalculationLimit(decimal? coefficientByBuildingQuarter)
        {
            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByRoom);
            var result = (!settings.LowerLimitForTheSecondCoefficient.HasValue || coefficientByBuildingQuarter >= settings.LowerLimitForTheSecondCoefficient.Value)
                         && (!settings.UpperLimitForTheSecondCoefficient.HasValue || coefficientByBuildingQuarter <= settings.UpperLimitForTheSecondCoefficient.Value);

            return result;
        }

        public void CalculatePriceAfterCorrectionByRooms(DateTime date)
        {
            var coefficients = GetAverageCoefficients().Where(x => x.Date == date);

            var objects = MarketObjectsService.GetObjectsForCorrectionByRoom();
            var objectsIds = objects.Select(x => x.Id);
            var priceChangingHistory = OMPriceAfterCorrectionByRoomsHistory.Where(x => objectsIds.Contains(x.InitialId)).SelectAll().Execute();

            objects.ForEach(obj =>
            {
                var isCoefExists = true;
                var coefficientByMarketSegment = coefficients.FirstOrDefault(x => x.MarketSegment == obj.PropertyMarketSegment_Code);
                if (coefficientByMarketSegment == null)
                    isCoefExists = false;

                var coefficient = 0m;
                switch (obj.RoomsCount)
                {
                    case 1:
                        if (coefficientByMarketSegment?.OneRoomCoefficient != null)
                        {
                            coefficient = coefficientByMarketSegment.OneRoomCoefficient.Value;
                        }
                        else
                        {
                            isCoefExists = false;
                        }
                        break;
                    case 3:
                        if (coefficientByMarketSegment?.ThreeRoomsCoefficient != null)
                        {
                            coefficient = coefficientByMarketSegment.ThreeRoomsCoefficient.Value;
                        }
                        else
                        {
                            isCoefExists = false;
                        }
                        break;
                }

                var newPriceAfterCorrectionByRoom = isCoefExists ? Math.Round(obj.Price.GetValueOrDefault() * coefficient, PrecisionForPrice) : (decimal?) null;

                using (var ts = new TransactionScope())
                {
                    SavePriceChangingHistory(priceChangingHistory, obj, date, newPriceAfterCorrectionByRoom);

                    obj.PriceAfterCorrectionByRooms = newPriceAfterCorrectionByRoom;
                    obj.Save();

                    ts.Complete();
                }
            });
        }


        #region Support Methods

        private List<CorrectionByRoomCoefficientsDto> GetAverageCoefficients()
        {
            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByRoom);

            var result = OMCoefficientsForCorrectionByRooms.Where(x => CalculatedMarketSegments.Contains(x.MarketSegment_Code) && x.IsExcluded == false || x.IsExcluded == null)
                .OrderByDescending(x => x.ChangingDate)
                .SelectAll().Execute()
                .GroupBy(x => new {x.MarketSegment_Code, x.ChangingDate}).Select(
                    group =>
                    {
                        var filterValues = @group.ToList().Where(x =>
                            (!settings.LowerLimitForCoefficient.HasValue ||
                             x.OneRoomCoefficient >= settings.LowerLimitForCoefficient.Value)
                            && (!settings.UpperLimitForCoefficient.HasValue ||
                                x.OneRoomCoefficient <= settings.UpperLimitForCoefficient.Value)).ToList();
                        var oneRoomCoefficient = filterValues.Count > 0
                            ? Math.Round(filterValues.Average(x => x.OneRoomCoefficient), PrecisionForCoefficients)
                            : (decimal?) null;

                        filterValues = @group.ToList().Where(x =>
                            (!settings.LowerLimitForTheSecondCoefficient.HasValue || x.ThreeRoomsCoefficient >=
                             settings.LowerLimitForTheSecondCoefficient.Value)
                            && (!settings.UpperLimitForTheSecondCoefficient.HasValue || x.ThreeRoomsCoefficient <=
                                settings.UpperLimitForTheSecondCoefficient.Value)).ToList();
                        var threeRoomsCoefficient = filterValues.Count > 0
                            ? Math.Round(filterValues.Average(x => x.ThreeRoomsCoefficient), PrecisionForCoefficients)
                            : (decimal?) null;
                        var dto = new CorrectionByRoomCoefficientsDto
                        {
                            Date = @group.Key.ChangingDate,
                            MarketSegment = @group.Key.MarketSegment_Code,
                            OneRoomCoefficient = oneRoomCoefficient,
                            ThreeRoomsCoefficient = threeRoomsCoefficient
                        };

                        return oneRoomCoefficient.HasValue || threeRoomsCoefficient.HasValue ? dto : null;
                    }
                ).Where(x => x != null).ToList();

            return result;
        }

        private void SavePriceChangingHistory(List<OMPriceAfterCorrectionByRoomsHistory> history, OMCoreObject obj,
            DateTime changingDate, decimal? newPriceAfterCorrectionByRoom)
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
