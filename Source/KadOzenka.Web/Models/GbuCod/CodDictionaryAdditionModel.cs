using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.CodDictionary;

namespace KadOzenka.Web.Models.GbuCod
{
    public class CodDictionaryAdditionModel : CodJobViewModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return CodDictionaryService.ValidateCodDictionaryForAddition(ToDto());
        }
    }
}
