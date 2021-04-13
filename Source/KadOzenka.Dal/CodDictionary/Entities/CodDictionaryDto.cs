using System.Collections.Generic;
using KadOzenka.Dal.Registers.Entities;

namespace KadOzenka.Dal.CodDictionary.Entities
{
    public class CodDictionaryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<AttributePure> Values { get; set; }
    }
}
