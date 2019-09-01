using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CIPJS.Models.Fsp
{
    public class FspBalanceDetails
    {
        #region properties
        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Год
        /// </summary>
        [Display(Name = "Год")]
        public int Year { get; set; }
        /// <summary>
        /// Месяц
        /// </summary>
        [Display(Name = "Период")]
        public string Month { get; set; }
        /// <summary>
        /// Нераспределенный остаток на начало периода
        /// </summary>
        [Display(Name = "Нераспределенный остаток на начало периода")]
        public decimal? OstatokSum { get; set; }
        /// <summary>
        /// Сумма зачислений, нарастающим итогом
        /// </summary>
        [Display(Name = "Зачислено")]
        public decimal? SumOpl { get; set; }
        /// <summary>
        /// Сумма начислений МФЦ в периоде, нарастающим итогом
        /// </summary>
        [Display(Name = "Начислено МФЦ")]
        public decimal? SumNachMfc { get; set; }
        /// <summary>
        /// Сумма начислений ГБУ в периоде, нарастающим итогом
        /// </summary>
        [Display(Name = "Контрольное начисление")]
        public decimal? SumNachGby { get; set; }
        /// <summary>
        /// Оплачено начисление, да/нет
        /// </summary>
        [Display(Name = "Оплачено начисление")]
        public bool? FlagOpl { get; set; }
        /// <summary>
        /// Застраховано, да/нет
        /// </summary>
        [Display(Name = "Застраховано")]
        public bool? FlagInsur { get; set; }


        [Display(Name = "Последний период с флагом застраховано")]
        public DateTime? StrahEnd { get; set; }
        #endregion

        public static FspBalanceDetails OMMap(OMBalance entity, int num)
        {
            var fspBalanceDetails = new FspBalanceDetails
            {
                Number = num,
                Year = entity.PeriodRegDate.Value.Year,
                Month = entity.PeriodRegDate.HasValue ? entity.PeriodRegDate.Value.ToString("MMMM yyyy") : "",
                OstatokSum = entity.OstatokSum,
                SumOpl = entity.SumOpl.HasValue ? entity.SumOpl.Value : 0,
                SumNachMfc = entity.SumNachMfc.HasValue ? entity.SumNachMfc.Value : 0,
                SumNachGby = entity.SumNachGby.HasValue ? entity.SumNachGby.Value : 0,
                FlagOpl = entity.FlagOpl,
                FlagInsur = entity.FlagInsur,
                StrahEnd = entity.StrahEnd
            };

            return fspBalanceDetails;
        }
    }
}
