using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using KadOzenka.Dal.Correction.Dto;
using KadOzenka.Dal.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByDateService
    {
        public List<CorrectionByDateDto> GetAllConsumerIndexes()
        {
            var indexes = OMIndexesForDateCorrection.Where(x => true).SelectAll()
                .OrderBy(x => x.Date)
                .Execute();

            var result = new List<CorrectionByDateDto>();
            indexes.ForEach(x => result.Add(ToDto(x)));

            return result;
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
            var indexes = OMIndexesForDateCorrection.Where(x => true).SelectAll().OrderByDescending(x => x.Date).Execute();

            RecalculateConsumerPriceIndexes(indexes);

            using (var ts = new TransactionScope())
            {
                for (var i = 0; i < indexes.Count; i++)
                {
                    indexes[i].Save();
                }

                ts.Complete();
            }

            CorrectionByDateForMarketObjectsLongProcess.AddProcessToQueue();
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
