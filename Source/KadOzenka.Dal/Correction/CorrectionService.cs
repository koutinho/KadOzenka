using System;
using System.Linq;
using System.Transactions;
using KadOzenka.Dal.Correction.Dto;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionService
    {
        public CorrectionByDateDto GetCorrectionByDate(DateTime date)
        {
            var dateToCompare = new DateTime(date.Year, date.Month, 1);

            var record = OMIndexesForDateCorrection.Where(x => x.Date == dateToCompare).SelectAll()
                .ExecuteFirstOrDefault();
            if(record == null)
                throw new Exception($"Не найдено индекса на дату: {date.ToString(Consts.DateFormatForDateCorrection)} ");
            
            return new CorrectionByDateDto
            {
                Id = record.Id, 
                Date = record.Date,
                //ConsumerPriceChange = record.ConsumerPriceChange,
                //ConsumerPriceIndex = record.ConsumerPriceIndex,
                ConsumerPriceIndexRosstat = record.ConsumerPriceIndexRosstat
            };
        }

        public DateTime GetNextCorrectionDate()
        {
            var lastCorrectionDate = OMIndexesForDateCorrection.Where(x => true).Select(x => x.Date)
                .OrderByDescending(x => x.Date)
                .ExecuteFirstOrDefault().Date;

            return lastCorrectionDate;
        }

        public void AddCorrection(CorrectionByDateDto correctionDto)
        {
            //var nextConsumerPriceIndex = 1;
            //var consumerPriceChange = (correctionDto.ConsumerPriceIndexRosstat - 100) / 100;
            //var consumerPriceIndex = nextConsumerPriceIndex * (1 + consumerPriceChange);

            //using (var ts = new TransactionScope())
            //{
            //    new OMIndexesForDateCorrection
            //    {
            //        Date = correctionDto.Date,
            //        ConsumerPriceChange = consumerPriceChange,
            //        ConsumerPriceIndex = consumerPriceIndex,
            //        ConsumerPriceIndexRosstat = correctionDto.ConsumerPriceIndexRosstat
            //    }.Save();

            //    new OMIndexesForDateCorrection
            //    {
            //        Date = correctionDto.Date.AddMonths(1),
            //        ConsumerPriceIndex = nextConsumerPriceIndex
            //    }.Save();

            //    RecalculateConsumerPriceIndexes(correctionDto.Date);

            //    ts.Complete();
            //}
        }

        public void EditCorrection(CorrectionByDateDto correctionDto)
        {

        }


        #region Support Methods

        private void RecalculateConsumerPriceIndexes(DateTime startDate)
        {
            var corrections = OMIndexesForDateCorrection.Where(x => x.Date < startDate).SelectAll().OrderByDescending(x => x.Date).Execute();
            for (var i = 0; i < corrections.Count; i++)
            {
                var current = corrections.ElementAt(i);
                var next = corrections.ElementAtOrDefault(i + 1);

                if (next == null)
                    continue;

                current.ConsumerPriceIndex = next.ConsumerPriceIndex * (1 + current.ConsumerPriceChange);
                current.Save();
            }
        }


        #endregion
    }
}
