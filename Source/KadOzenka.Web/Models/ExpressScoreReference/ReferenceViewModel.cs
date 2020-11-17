using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.SRD;
using ObjectModel.Directory.ES;
using ObjectModel.ES;

namespace KadOzenka.Web.Models.ExpressScoreReference
{
    public class ReferenceViewModel : IValidatableObject
    {
        public long Id { get; set; }

        /// <summary>
        /// Наименование справочника
        /// </summary>
        [Display(Name = "Наименование справочника")]
        [Required(ErrorMessage = "Поле Наименование справочника обязательное")]
        public string Name { get; set; }

        /// <summary>
        /// Тип данных значения справочника
        /// </summary>
        [Display(Name = "Тип данных")]
        [Required(ErrorMessage = "Поле Тип данных значений справочника обязательное")]
        public ReferenceItemCodeType ValueType { get; set; } = ReferenceItemCodeType.String;

        public bool ValueTypeCanBeChanged { get; set; }

        public bool ShowItems { get; set; }

        public bool IsEdit { get; set; }

        /// <summary>
        /// Признак что справочник интервальный
        /// </summary>
        public bool UseInterval { get; set; }

        public static ReferenceViewModel FromEntity(OMEsReference entity, bool valueTypeCanBeChanged = false, bool showItems = false)
        {
            if (entity == null)
            {
                return new ReferenceViewModel
                {
                    Id = -1,
                    ShowItems = showItems,
                    ValueTypeCanBeChanged = valueTypeCanBeChanged,
                    IsEdit = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)
                };
            }

            return new ReferenceViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                ValueType = entity.ValueType_Code,
                ValueTypeCanBeChanged = valueTypeCanBeChanged,
                ShowItems = showItems,
                UseInterval = entity.UseInterval.GetValueOrDefault(),
                IsEdit = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        if (UseInterval && ValueType == ReferenceItemCodeType.String)
	        {
                yield return new ValidationResult(@"Интервальный справочник не может быть типа ""Строка""");
	        }
        }
    }
}
