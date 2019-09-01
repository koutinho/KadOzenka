using Core.Shared.Extensions;
using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace CIPJS.Models.Fsp
{
    public class FspInputPlatDetails
    {
        #region properties
        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Месяц
        /// </summary>
        [Display(Name = "Период")]
        public string PeriodRegDate { get; set; }
        /// <summary>
        /// Дата оплаты
        /// </summary>
        [Display(Name = "Дата оплаты")]
        public string PmtDate { get; set; }
        /// <summary>
        /// Сумма оплаты
        /// </summary>
        [Display(Name = "Сумма оплаты")]
        public string SumOpl { get; set; }
        /// <summary>
        /// Подтверждено банком
        /// </summary>
        [Display(Name = "Подтверждено банком")]
        public string ConfirmedBank { get; set; }
        /// <summary>
        /// Код плательщика
        /// </summary>
        [Display(Name = "Код плательщика")]
        public string Kodpl { get; set; }

        public long? Year { get; set; }

        /// <summary>
        /// Ставка за 1 кв.м
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// Банковский день
        /// </summary>
        public string BankDay { get; set; }

        /// <summary>
        /// Оплачиваемый период (банк)
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Период документа (банк)
        /// </summary>
        public string DocPeriod { get; set; }

        #endregion

        public static FspInputPlatDetails OMMap(OMInputPlat entity, int num)
        {
            var cultureRu = new CultureInfo("ru-RU");

            var fspInputPlatDetails = new FspInputPlatDetails
            {
                Number = num,
                PeriodRegDate = entity.PeriodRegDate.HasValue ? entity.PeriodRegDate.Value.ToString("MMMM yyyy") : string.Empty,
                PmtDate = entity.PmtDate.HasValue ? entity.PmtDate.Value.ToString("dd.MM.yyyy") : string.Empty,
                SumOpl = entity.SumOpl.HasValue ? entity.SumOpl.Value.ToString("N2", cultureRu) : string.Empty,
                ConfirmedBank = entity.LinkBankId.HasValue ? "Да" : "Нет",
                Kodpl = entity.Kodpl,
                Year = entity.PeriodRegDate?.Year,
                Rate = (entity.SumOpl.HasValue && entity.ParentFsp?.OplKodpl != null && entity.ParentFsp.OplKodpl != 0) 
                    ? (entity.SumOpl.Value / entity.ParentFsp.OplKodpl.Value).ToString("N2", cultureRu) : string.Empty,
                BankDay = entity.ParentBankPlat?.ParentSvodBank?.BankDay?.ToString("dd.MM.yyyy") ?? string.Empty,
                Period = entity.ParentBankPlat?.Period?.ToString("MMMM yyyy") ?? string.Empty,
                DocPeriod = entity.ParentBankPlat?.DocPeriod?.ToString("MMMM yyyy") ?? string.Empty
            };

            return fspInputPlatDetails;
        }
    }
}
