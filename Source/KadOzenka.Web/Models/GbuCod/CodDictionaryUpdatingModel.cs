using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.Registers.Entities;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.GbuCod
{
    public class CodDictionaryUpdatingModel : CodJobViewModel
    {
        public List<AttributePure> RegisterAttributes { get; set; }

        public static CodDictionaryUpdatingModel ToModel(OMCodJob entity, ICodDictionaryService service)
        {
            var registerAttributes = service.GetDictionaryRegisterAttributes(entity.RegisterId, false)
                .Select(x => new AttributePure
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            return new CodDictionaryUpdatingModel
            {
                Id = entity.Id,
                Name = entity.NameJob,
                RegisterAttributes = registerAttributes,
                ValuesCount = registerAttributes.Count
            };
        }

        public override CodDictionaryDto ToDto()
        {
            var dto = base.ToDto();
            dto.Values = RegisterAttributes;

            return dto;
        }
    }
}
