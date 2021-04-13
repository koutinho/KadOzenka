using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.Registers.Entities;

namespace KadOzenka.Web.Models.GbuCod
{
    public class CodDictionaryAdditionModel : CodJobViewModel
    {
        public List<string> Values { get; set; }

        public CodDictionaryAdditionModel()
        {
            Values = new List<string>();
        }


        public override CodDictionaryDto ToDto()
        {
            var dto = base.ToDto();
            dto.Values = Values.Select(x => new AttributePure
            {
                Id = -1,
                Name = x
            }).ToList();

            return dto;
        }
    }
}
