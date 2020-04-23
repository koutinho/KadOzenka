using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using KadOzenka.Dal.Correction.Dto;
using KadOzenka.Dal.Correction.Dto.CorrectionSettings;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByDateService
    {
        public List<CorrectionByDateDto> GetAverageCoefficientsBySegments(long marketSegmentCode)
        {
            return GetAverageCoefficientsBySegments().Where(x => x.MarketSegment == (MarketSegment)marketSegmentCode).ToList();
        }

        public List<CorrectionByDateDto> GetDetailedCoefficients(long marketSegmentCode, DateTime date)
        {
            return OMIndexesForDateCorrection.Where(x =>
                    x.MarketSegment_Code == (MarketSegment)marketSegmentCode && x.Date == date)
                .OrderBy(x => x.BuildingCadastralNumber)
                .SelectAll().Execute().Select(
                    x => new CorrectionByDateDto
                    {
                        Id = x.Id,
                        BuildingCadastralNumber = x.BuildingCadastralNumber,
                        Coefficient = x.Coefficient,
                        IsExcludeFromCalculation = x.IsExcluded.GetValueOrDefault()
                    }).ToList();
        }

        public bool ChangeBuildingsStatusInCalculation(List<CorrectionByDateDto> coefficients)
        {
            if (coefficients.Count == 0)
                return false;

            var isDataUpdated = false;
            coefficients.ForEach(record =>
            {
                var recordFromDb = OMIndexesForDateCorrection.Where(x => x.Id == record.Id).SelectAll().ExecuteFirstOrDefault();
                if (recordFromDb == null)
                    return;

                if (recordFromDb.IsExcluded != record.IsExcludeFromCalculation)
                    isDataUpdated = true;

                recordFromDb.IsExcluded = record.IsExcludeFromCalculation;
                recordFromDb.Save();
            });

            return isDataUpdated;
        }

        public decimal GetAveragePricePerMeter(IEnumerable<OMCoreObject> objects)
        {
            return objects.Average(x => x.PricePerMeter.GetValueOrDefault());
        }

        public List<OMIndexesForDateCorrection> GetCoefficients()
        {
            return OMIndexesForDateCorrection.Where(x => true).SelectAll().Execute().ToList();
        }

        public void SaveCoefficients(List<OMIndexesForDateCorrection> coefficients, DateTime date, string buildingCadastralNumber,
            MarketSegment segment, decimal coefficient, CorrectionSettings settings)
        {
            var isExcluded = !IsCoefIncludedInCalculationLimit(coefficient, settings);
            var existedRecord = coefficients.FirstOrDefault(x =>
                x.BuildingCadastralNumber == buildingCadastralNumber && x.MarketSegment_Code == segment &&
                x.Date == date);
            if (existedRecord == null)
            {
                new OMIndexesForDateCorrection
                {
                    BuildingCadastralNumber = buildingCadastralNumber,
                    MarketSegment_Code = segment,
                    Date = date,
                    Coefficient = coefficient,
                    IsExcluded = isExcluded
                }.Save();
            }
            else
            {
                existedRecord.Coefficient = coefficient;
                existedRecord.IsExcluded = isExcluded;
                existedRecord.Save();
            }
        }

        public bool IsCoefIncludedInCalculationLimit(decimal? coefficientByBuildingQuarter, CorrectionSettings settings)
        {
            var value = (!settings.LowerLimitForCoefficient.HasValue || coefficientByBuildingQuarter >= settings.LowerLimitForCoefficient.Value)
                   && (!settings.UpperLimitForCoefficient.HasValue || coefficientByBuildingQuarter <= settings.UpperLimitForCoefficient.Value);

            return value;
        }

        public void CalculatePriceAfterCorrectionByDate()
        {
            var coefficients = GetAverageCoefficientsBySegments();

            var objects = GetMarketObjectsForUpdate();
            var objectsIds = objects.Select(x => x.Id);
            var priceChangingHistory = OMPriceAfterCorrectionByDateHistory.Where(x => objectsIds.Contains(x.InitialId)).SelectAll().Execute();

            objects.ForEach(obj =>
            {
                var date = obj.LastDateUpdate ?? obj.ParserTime.Value;
                var dateToCompare = new DateTime(date.Year, date.Month, 1);

                var coefficientByMarketSegment = coefficients.FirstOrDefault(x => x.MarketSegment == obj.PropertyMarketSegment_Code && x.Date == dateToCompare);
                if (coefficientByMarketSegment == null)
                    return;

                var newPrice = Math.Round(obj.Price.GetValueOrDefault() * coefficientByMarketSegment.Coefficient,
                    Consts.PrecisionForPrice);
                using (var ts = new TransactionScope())
                {
                    SavePriceChangingHistory(priceChangingHistory, obj, newPrice);

                    obj.PriceAfterCorrectionByDate = newPrice;
                    obj.Save();

                    ts.Complete();
                }
            });
        }

        public List<OMCoreObject> GetMarketObjectsForUpdate()
        {
            return OMCoreObject
                .Where(x => x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal && 
                            (x.ParserTime != null || x.LastDateUpdate != null) && 
                            x.PropertyMarketSegment != null)
                .SelectAll().Execute();
        }


        #region Support Methods

        private List<CorrectionByDateDto> GetAverageCoefficientsBySegments()
        {
            return OMIndexesForDateCorrection.Where(x => x.IsExcluded == false || x.IsExcluded == null)
                .OrderByDescending(x => x.Date)
                .SelectAll().Execute()
                .GroupBy(x => new {x.MarketSegment_Code, x.Date}).Select(
                    group => new CorrectionByDateDto
                    {
                        Date = group.Key.Date,
                        MarketSegment = group.Key.MarketSegment_Code,
                        Coefficient = Math.Round(group.ToList().DefaultIfEmpty().Average(x => x.Coefficient),
                            Consts.PrecisionForCoefficients)
                    }).ToList();
        }

        private void SavePriceChangingHistory(List<OMPriceAfterCorrectionByDateHistory> history, OMCoreObject obj, decimal newPrice)
        {
            var date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var existedRecord = history.FirstOrDefault(x => x.InitialId == obj.Id && x.ChangingDate == date);
            if (existedRecord == null)
            {
                new OMPriceAfterCorrectionByDateHistory
                {
                    InitialId = obj.Id,
                    ChangingDate = date,
                    PriceValueFrom = obj.PriceAfterCorrectionByDate.GetValueOrDefault(),
                    PriceValueTo = newPrice
                }.Save();
            }
            else
            {
                existedRecord.PriceValueFrom = obj.PriceAfterCorrectionByDate.GetValueOrDefault();
                existedRecord.PriceValueTo = newPrice;
                existedRecord.Save();
            }
        }

        #endregion
    }
}
