using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Register;
using KadOzenka.Dal.CodDictionary;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.GbuCod
{
    public class CodDictionaryUpdatingModel : CodJobViewModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return CodDictionaryService.ValidateCodDictionaryForUpdating(ToDto());
        }

        public static CodDictionaryUpdatingModel ToModel(OMCodJob entity)
        {
            var values = RegisterCache.RegisterAttributes.Values
                .Where(x => x.RegisterId == entity.RegisterId && !x.IsPrimaryKey)
                .Select(x => x.Name).ToList();

            return new CodDictionaryUpdatingModel
            {
                Id = entity.Id,
                Name = entity.NameJob,
                Result = entity.ResultJob,
                ValuesCount = values.Count,
                Values = values
            };
        }
	}
}
