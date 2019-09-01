using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.DAL.Balance
{
    public class BalanceService
    {
        /// <summary>
        /// Получить список ведомостей учета страховых взносов по идентификатору ФСП
        /// </summary>
        /// <param name="id">Идентификатор помещения</param>
        /// <returns>Список ведомостей учета страховых взносов</returns>
        public List<OMBalance> GetByFspId(long? id)
        {
            var result = new List<OMBalance>();

            if (id.HasValue)
            {
                var fsp = OMFsp.Where(x => x.EmpId == id).SelectAll().Execute().FirstOrDefault();

                if (fsp != null)
                {
                    var date_now = DateTime.Now;
                    var lastDayMonthDate = new DateTime(date_now.Year, date_now.Month, DateTime.DaysInMonth(date_now.Year, date_now.Month));

                    result = OMBalance
                              .Where(x => x.FspId == id && x.PeriodRegDate >= fsp.DateOpen && x.PeriodRegDate <= lastDayMonthDate)
                              .SelectAll().Execute();
                }
            }

            return result;
        }       
    }
}
