using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using KadOzenka.Web.Models.GbuObject;
using ObjectModel.Directory.ES;

namespace KadOzenka.Web.Models.ExpressScoreReference
{
    public class ImportDataViewModel: IValidatableObject
    {
        /// <summary>
        /// Общий код справочника
        /// </summary>
        [Display(Name = "Общий код справочника")]
        [Required(ErrorMessage = "Поле Общий код справочника обязательное")]
        public string CommonValue { get; set; }

        /// <summary>
        /// Код справочника
        /// </summary>
        [Display(Name = "Код справочника")]
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

        /// <summary>
        /// Признак что загружаем интервальный справочник
        /// </summary>
        [Display(Name = "Интервальный справочник")]
        public bool UseInterval { get; set; }

        /// <summary>
        /// Значение от
        /// </summary>
        [Display(Name = "Код справочника от")]
        public string ValueFrom { get; set; }

        /// <summary>
        /// Значение До
        /// </summary>
        [Display(Name = "Код справочника до")]
        public string ValueTo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        if (UseInterval && (ValueFrom.IsNullOrEmpty() || ValueTo.IsNullOrEmpty()))
	        {
                yield return new ValidationResult(@"Нобходимо выбрать ""Код справочника от"" и ""Код справочника до""");
	        }

	        if (!UseInterval && Value.IsNullOrEmpty())
	        {
		        yield return new ValidationResult(@"Нобходимо выбрать ""Код справочника""");
            }

	        if (UseInterval && ValueType == ReferenceItemCodeType.String)
	        {
		        yield return new ValidationResult(@"Интервальный справочник не может быть типа ""Строка""");
	        }
        }
    }
}
