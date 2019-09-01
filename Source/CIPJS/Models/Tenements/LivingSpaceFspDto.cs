using System;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.Tenements
{
    public class LivingSpaceFspDto
    {
        public string Title { get; set; }

        [Display(Name = "Номер ФСП")]
        public string FspNumber { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Тип ФСП")]
        public string FspType { get; set; }

        [Display(Name = "Код плательщика")]
        public long? PayerCode { get; set; }

        [Display(Name = "Номер полиса / свидетельства")]
        public string Number { get; set; }

        [Display(Name = "Дата начала действия договора")]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "Общая площадь помещения")]
        public decimal? TotalArea { get; set; }

        [Display(Name = "Площадь, подлежащая страхованию")]
        public decimal? InsuranceArea { get; set; }
    }
}