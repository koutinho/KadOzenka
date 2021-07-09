using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KadOzenka.Web.Models.Tour
{
	public class PartialGroupingDictionaryModel : IValidatableObject
    {
        public static string NoDictionaryIdErrorMessage => "Выберете справочник";
        public static string EmptyDictionaryNameErrorMessage => "Наименование нового справочника не может быть пустым";


        public string ModelPrefix { get; set; }

        [Display(Name = "Справочник")]
        public long? DictionaryId { get; set; }

        [Display(Name = "Удалить старые данные")]
        public bool DeleteOldValues { get; set; } = false;

        [Display(Name = "Наименование справочника")]
        public string NewDictionaryName { get; set; }

        public bool IsNewDictionary { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsNewDictionary)
            {
                if (string.IsNullOrEmpty(NewDictionaryName))
                {
                    yield return
                        new ValidationResult(errorMessage: EmptyDictionaryNameErrorMessage,
                            memberNames: new[] { nameof(NewDictionaryName) });
                }
            }
            else if (!DictionaryId.HasValue)
            {
                yield return
                    new ValidationResult(errorMessage: NoDictionaryIdErrorMessage,
                        memberNames: new[] { nameof(DictionaryId) });
            }
        }
    }
}