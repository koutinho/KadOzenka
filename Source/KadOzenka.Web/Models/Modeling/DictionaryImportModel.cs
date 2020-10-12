﻿using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory.ES;

namespace KadOzenka.Web.Models.Modeling
{
    public class DictionaryImportModel
    {
	    [Display(Name = "Код справочника")]
        [Required(ErrorMessage = "Поле 'Код справочника' обязательное")]
        public string Value { get; set; }

        [Display(Name = "Значение для расчета")]
        [Required(ErrorMessage = "Поле 'Значение для расчета' обязательное")]
        public string CalcValue { get; set; }

        [Display(Name = "Тип данных")]
        [Required(ErrorMessage = "Поле 'Тип данных' значения справочника обязательное")]
        public ReferenceItemCodeType ValueType { get; set; } = ReferenceItemCodeType.String;

        [Display(Name = "Справочник")]
        public PartialDictionaryModel Dictionary { get; set; } = new PartialDictionaryModel();
    }
}
