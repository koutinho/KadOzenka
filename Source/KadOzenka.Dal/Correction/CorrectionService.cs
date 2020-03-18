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
            
        }

        public void EditCorrection(CorrectionByDateDto correctionDto)
        {

        }


        #region Support Methods

        

        #endregion
    }
}
