using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.Fsp
{
    public class FspCreateDto
    {
        [Display(Name = "Идентификатор объекта")]
        public long ObjId { get; set; }

        [Display(Name = "Идентификатор страховой компании")]
        public long? InsuranceOrganizationId { get; set; }

        [Display(Name = "Тип договора")]
        public FSPType? FspType { get; set; }

        [Display(Name = "Код плательщика")]
        public string Kodpl { get; set; }

        [Display(Name = "Номер полиса/свидетельства")]
        public string Npol { get; set; }

        [Display(Name = "Площадь по договору")]
        public decimal? OplKodpl { get; set; }

        [Display(Name = "Дата начала действия договора")]
        public DateTime? DateBegin { get; set; }

        [Display(Name = "Дата окончания действия договора")]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Условия страхования")]
        public InsuranceTerms? Pralt { get; set; }

        [Display(Name = "Страховая сумма")]
        public decimal? Ss { get; set; }

        [Display(Name = "Сумма страхового взноса")]
        public decimal? Soplvz { get; set; }
    }
}
