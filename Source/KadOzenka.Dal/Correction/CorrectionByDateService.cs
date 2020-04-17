using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.Correction.Dto;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByDateService
    {
        public static readonly int PrecisionForPrice = 2;
        public static readonly int PrecisionForCoefficients = 4;


        public List<CorrectionByDateDto> GetAverageCoefficients(long marketSegmentCode)
        {
            return GetAverageCoefficients().Where(x => x.MarketSegment == (MarketSegment)marketSegmentCode).ToList();
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
            MarketSegment segment, decimal coefficient)
        {
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
                    Coefficient = coefficient
                }.Save();
            }
            else
            {
                existedRecord.Coefficient = coefficient;
                existedRecord.Save();
            }
        }


        public CorrectionByDateDto GetNextConsumerIndex()
        {
            var index = OMIndexesForDateCorrection.Where(x => true).SelectAll()
                .OrderByDescending(x => x.Date)
                .ExecuteFirstOrDefault();

            return ToDto(index);
        }

        public void UpdateConsumerPriceIndexes()
        {
            var startDate = new DateTime(2019, 01, 01);
            var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            var coefficients = GetCoefficients();

            var objectsGroupedBySegment = OMCoreObject.Where(x =>
                    (x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal) &&
                    x.BuildingCadastralNumber != null && x.BuildingCadastralNumber != "" &&
                    x.PropertyMarketSegment != null &&
                    x.ParserTime != null && x.ParserTime >= startDate && x.ParserTime <= endDate)
                .SelectAll(false)
                //todo remove
                //.SetPackageSize(10000).SetPackageIndex(0)
                .Execute()
                .GroupBy(x => x.PropertyMarketSegment_Code)
                .ToList();

            objectsGroupedBySegment.ForEach(groupBySegment =>
            {
                var objectsGroupedByBuilding = groupBySegment.GroupBy(x => x.BuildingCadastralNumber).ToList();

                objectsGroupedByBuilding.ForEach(groupByBuilding =>
                {
                    var objectsInBuilding = groupByBuilding.ToList();

                    for (var i = startDate; i <= endDate; i = i.AddMonths(1))
                    {
                        var currentPeriod = i;
                        var previousPeriod = i.AddYears(-1);

                        var objectFromCurrentPeriod = objectsInBuilding.Where(x =>
                            x.ParserTime?.Year == currentPeriod.Year && x.ParserTime?.Month == currentPeriod.Month).ToList();

                        var objectFromPreviousPeriod = objectsInBuilding.Where(x =>
                            x.ParserTime?.Year == previousPeriod.Year && x.ParserTime?.Month == previousPeriod.Month).ToList();

                        var isBuildingContainSalesInTwoPeriods = objectFromCurrentPeriod.Count > 0 && objectFromPreviousPeriod.Count > 0;

                        if (isBuildingContainSalesInTwoPeriods)
                        {
                            var averagePriceForObjectsFromCurrentPeriod = GetAveragePricePerMeter(objectFromCurrentPeriod);
                            var averagePriceForObjectsFromPreviousPeriod = GetAveragePricePerMeter(objectFromPreviousPeriod);

                            var coefficient = Math.Round(averagePriceForObjectsFromCurrentPeriod / averagePriceForObjectsFromPreviousPeriod, PrecisionForCoefficients);

                            SaveCoefficients(coefficients, currentPeriod, groupByBuilding.Key, groupBySegment.Key, coefficient);
                        }
                    }
                });
            });

            //TODO update price

            //var indexes = OMIndexesForDateCorrection.Where(x => true).SelectAll().OrderByDescending(x => x.Date).Execute();

            //RecalculateConsumerPriceIndexes(indexes);

            //using (var ts = new TransactionScope())
            //{
            //    for (var i = 0; i < indexes.Count; i++)
            //    {
            //        indexes[i].Save();
            //    }

            //    ts.Complete();
            //}

            //CorrectionByDateForMarketObjectsLongProcess.AddProcessToQueue();
        }

        public OMIndexesForDateCorrection GetDefaultNewConsumerIndex(DateTime date)
        {
            return new OMIndexesForDateCorrection
            {
                Date = date,
                Coefficient = 1
            };
        }

        public List<OMCoreObject> GetMarketObjectsForUpdate()
        {
            return OMCoreObject
                .Where(x => x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal)
                .SelectAll().Execute();
        }

        public decimal? CalculatePriceAfterCorrectionByDate(OMCoreObject obj, List<OMIndexesForDateCorrection> consumerIndexes)
        {
            if (obj.LastDateUpdate == null && obj.ParserTime == null)
                return null;

            var date = obj.LastDateUpdate ?? obj.ParserTime.Value;
            var dateToCompare = new DateTime(date.Year, date.Month, 1);

            var inflationRate = consumerIndexes.Where(x => x.Date >= dateToCompare).Sum(x => x.Coefficient);

            var priceDifference = (obj.Price * inflationRate) / 100;
            return obj.Price + priceDifference;
        }

        public List<OMIndexesForDateCorrection> RecalculateConsumerPriceIndexes(List<OMIndexesForDateCorrection> indexes)
        {
            for (var i = 0; i < indexes.Count; i++)
            {
                var current = indexes.ElementAt(i);
                var next = indexes.ElementAtOrDefault(i + 1);

                if (next == null)
                    continue;

                //var consumerPriceChange = (next.ConsumerPriceIndexRosstat - 100) / 100;
                //var consumerPriceIndex = current.ConsumerPriceIndex * (1 + consumerPriceChange);

                //next.ConsumerPriceChange = consumerPriceChange;
                //next.ConsumerPriceIndex = consumerPriceIndex;
            }

            return indexes;
        }


        #region Support Methods

        private List<CorrectionByDateDto> GetAverageCoefficients()
        {
            return OMIndexesForDateCorrection.Where(x => x.IsExcluded == false || x.IsExcluded == null)
                .OrderByDescending(x => x.Date)
                .SelectAll().Execute()
                .GroupBy(x => new { x.MarketSegment_Code, x.Date }).Select(
                    group => new CorrectionByDateDto
                    {
                        Date = group.Key.Date,
                        MarketSegment = group.Key.MarketSegment_Code,
                        Coefficient = Math.Round(
                            group.ToList().DefaultIfEmpty().Average(x => x.Coefficient),
                            PrecisionForCoefficients)
                    }).ToList();
        }

        private CorrectionByDateDto ToDto(OMIndexesForDateCorrection index)
        {
            if (index == null)
                return new CorrectionByDateDto();

            return new CorrectionByDateDto
            {
                Id = index.Id,
                BuildingCadastralNumber = index.BuildingCadastralNumber,
                MarketSegment = index.MarketSegment_Code,
                Coefficient = index.Coefficient,
                IsExcludeFromCalculation = index.IsExcluded.GetValueOrDefault()
            };
        }

        #endregion
    }
}
