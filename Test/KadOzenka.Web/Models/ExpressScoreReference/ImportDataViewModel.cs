using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KadOzenka.Web.Models.GbuObject;
using ObjectModel.Directory.ES;

namespace KadOzenka.Web.Models.ExpressScoreReference
{
    public class ImportDataViewModel
    {
        /// <summary>
        /// Код справочника
        /// </summary>
        [Display(Name = "Код справочника")]
        [Required(ErrorMessage = "Поле Код справочника обязательное")]
        public string Value { get; set; }

        /// <summary>
        /// Значение для расчета
        /// </summary>
        [Display(Name = "Значение для расчета")]
        [Required(ErrorMessage = "Поле Значение для расчета обязательное")]
        public string CalcValue { get; set; }

        /// <summary>
        /// Тип данных значения справочника
        /// </summary>
        [Display(Name = "Тип данных")]
        [Required(ErrorMessage = "Поле Тип данных значения справочника обязательное")]
        public ReferenceItemCodeType ValueType { get; set; } = ReferenceItemCodeType.String;

        /// <summary>
        /// Справочник 
        /// </summary>
        [Display(Name = "Справочник")]
        public PartialReferenceViewModel Reference { get; set; } = new PartialReferenceViewModel();
    }
}
