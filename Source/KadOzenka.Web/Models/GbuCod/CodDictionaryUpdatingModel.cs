using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.Registers.Entities;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.GbuCod
{
    public class CodDictionaryUpdatingModel : CodJobViewModel, IValidatableObject
    {
        public List<AttributePure> RegisterAttributes { get; private set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return CodDictionaryService.ValidateCodDictionaryForUpdating(ToDto());
        }

        public static CodDictionaryUpdatingModel ToModel(OMCodJob entity)
        {
            //TODO KOMO-7 унести в сервис
            var registerAttributes = CodDictionaryService.GetDictionaryRegisterAttributes(entity.RegisterId)
                .Where(x => x.Name != CodDictionaryConsts.CodeColumnName)
                .Select(x => new AttributePure
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            var values = registerAttributes.Select(x => x.Name).ToList();

            return new CodDictionaryUpdatingModel
            {
                Id = entity.Id,
                Name = entity.NameJob,
                RegisterAttributes = registerAttributes,
                ValuesCount = values.Count,
                Values = values
            };
        }
	}
}
