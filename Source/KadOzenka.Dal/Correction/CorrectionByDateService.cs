using System;
using System.Linq;
using System.Transactions;
using KadOzenka.Dal.Correction.Dto;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByDateService
    {
        public CorrectionByDateDto GetCorrectionByDate(DateTime date)
        {
            var dateToCompare = new DateTime(date.Year, date.Month, 1);

            var correction = OMIndexesForDateCorrection.Where(x => x.Date == dateToCompare).SelectAll()
                .ExecuteFirstOrDefault();
            if(correction == null)
                throw new Exception($"Не найдено индекса на дату: {date.ToString(Consts.DateFormatForDateCorrection)} ");

            return ToDto(correction);
        }

        public CorrectionByDateDto GetNextCorrection()
        {
            var correction = OMIndexesForDateCorrection.Where(x => true).SelectAll()
                .OrderByDescending(x => x.Date)
                .ExecuteFirstOrDefault();

            return ToDto(correction);
        }

        public void EditCorrection(CorrectionByDateDto input)
        {
            var correction = OMIndexesForDateCorrection.Where(x => x.Id == input.Id).SelectAll()
                .ExecuteFirstOrDefault();
            if (correction == null)
                throw new Exception($"Не найден индекс по Id '{input.Id}' ");

            using (var ts = new TransactionScope())
            {
                correction.ConsumerPriceIndexRosstat = input.ConsumerPriceIndexRosstat;
                correction.Save();

                ts.Complete();
            }

            //т.е. мы добавляем новое значение
            if (correction.ConsumerPriceIndex == 1)
            {
                new OMIndexesForDateCorrection
                {
                    Date = correction.Date.AddMonths(1),
                    ConsumerPriceIndex = 1
                }.Save();
            }

            RecalculateConsumerPriceIndexes();
        }


        #region Support Methods

        private void RecalculateConsumerPriceIndexes()
        {
            var corrections = OMIndexesForDateCorrection.Where(x => true).SelectAll().OrderByDescending(x => x.Date).Execute();
            for (var i = 0; i < corrections.Count; i++)
            {
                var current = corrections.ElementAt(i);
                var next = corrections.ElementAtOrDefault(i + 1);

                if (next == null)
                    continue;

                var consumerPriceChange = (next.ConsumerPriceIndexRosstat - 100) / 100;
                var consumerPriceIndex = current.ConsumerPriceIndex * (1 + consumerPriceChange);

                next.ConsumerPriceChange = consumerPriceChange;
                next.ConsumerPriceIndex = consumerPriceIndex;
                next.Save();
            }
        }

        private CorrectionByDateDto ToDto(OMIndexesForDateCorrection omCorrection)
        {
            return new CorrectionByDateDto
            {
                Id = omCorrection.Id,
                Date = omCorrection.Date,
                //ConsumerPriceChange = omCorrection.ConsumerPriceChange,
                //ConsumerPriceIndex = omCorrection.ConsumerPriceIndex,
                ConsumerPriceIndexRosstat = omCorrection.ConsumerPriceIndexRosstat
            };
        }

        #endregion
    }
}
