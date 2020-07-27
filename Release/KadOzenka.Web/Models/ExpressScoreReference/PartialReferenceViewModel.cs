using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.ExpressScoreReference
{
	public class PartialReferenceViewModel : IValidatableObject
    {
        public string ModelPrefix { get; set; }

        /// <summary>
        /// Идентификатор справочника, куда будет записан результат 
        /// </summary>
        [Display(Name = "Справочник")]
        public long? IdReference { get; set; }

        [Display(Name = "Удалить старые данные")]
        public bool DeleteOldValues { get; set; } = false;

        /// <summary>
        /// Имя нового справочника
        /// </summary>
        [Display(Name = "Наименование справочника")]
        public string NewReferenceName { get; set; }

        /// <summary>
        /// Флаг указывающий используем старый или новый справочник
        /// </summary>
        public bool IsNewReference { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsNewReference)
            {
                if (string.IsNullOrEmpty(NewReferenceName))
                {
                    yield return
                        new ValidationResult(errorMessage: "Наименование нового справочника не может быть пустым",
                            memberNames: new[] { nameof(NewReferenceName) });
                }
            }
            else if (!IdReference.HasValue)
            {
                yield return
                    new ValidationResult(errorMessage: "Выберете справочник",
                        memberNames: new[] { nameof(IdReference) });
            }
        }
    }
}