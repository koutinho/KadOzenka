using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using KadOzenka.Dal.Correction.Dto;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByDateService
    {
        public List<CorrectionByDateDto> GetConsumerIndexes()
        {
            var indexes = OMIndexesForDateCorrection.Where(x => true).SelectAll()
                .OrderBy(x => x.Date)
                .Execute();

            var result = new List<CorrectionByDateDto>();
            indexes.ForEach(x => result.Add(ToDto(x)));

            return result;
        }

        public CorrectionByDateDto GetConsumerIndexByDate(DateTime date)
        {
            var dateToCompare = new DateTime(date.Year, date.Month, 1);

            var index = OMIndexesForDateCorrection.Where(x => x.Date == dateToCompare).SelectAll()
                .ExecuteFirstOrDefault();
            if(index == null)
                throw new Exception($"Не найдено индекса на дату: {date.ToString(Consts.DateFormatForDateCorrection)} ");

            return ToDto(index);
        }

        public CorrectionByDateDto GetNextConsumerIndex()
        {
            var index = OMIndexesForDateCorrection.Where(x => true).SelectAll()
                .OrderByDescending(x => x.Date)
                .ExecuteFirstOrDefault();

            return ToDto(index);
        }

        public void EditConsumerIndex(CorrectionByDateDto input)
        {
            var index = OMIndexesForDateCorrection.Where(x => x.Id == input.Id).SelectAll()
                .ExecuteFirstOrDefault();
            if (index == null)
                throw new Exception($"Не найден индекс по Id '{input.Id}' ");

            using (var ts = new TransactionScope())
            {
                index.ConsumerPriceIndexRosstat = input.ConsumerPriceIndexRosstat;
                index.Save();

                ts.Complete();
            }

            //т.е. мы добавляем новое значение
            if (index.ConsumerPriceIndex == 1)
            {
                new OMIndexesForDateCorrection
                {
                    Date = index.Date.AddMonths(1),
                    ConsumerPriceIndex = 1
                }.Save();
            }

            RecalculateConsumerPriceIndexes();
        }


        #region Support Methods

        private void RecalculateConsumerPriceIndexes()
        {
            var indexes = OMIndexesForDateCorrection.Where(x => true).SelectAll().OrderByDescending(x => x.Date).Execute();
            for (var i = 0; i < indexes.Count; i++)
            {
                var current = indexes.ElementAt(i);
                var next = indexes.ElementAtOrDefault(i + 1);

                if (next == null)
                    continue;

                var consumerPriceChange = (next.ConsumerPriceIndexRosstat - 100) / 100;
                var consumerPriceIndex = current.ConsumerPriceIndex * (1 + consumerPriceChange);

                next.ConsumerPriceChange = consumerPriceChange;
                next.ConsumerPriceIndex = consumerPriceIndex;

                //todo map to transaction
                next.Save();
            }
        }

        private CorrectionByDateDto ToDto(OMIndexesForDateCorrection omCorrection)
        {
            return new CorrectionByDateDto(omCorrection.ConsumerPriceIndex, omCorrection.ConsumerPriceChange)
            {
                Id = omCorrection.Id,
                Date = omCorrection.Date,
                ConsumerPriceIndexRosstat = omCorrection.ConsumerPriceIndexRosstat
            };
        }

        #endregion
    }
}
