using System.Collections.Generic;

namespace KadOzenka.Dal.CodDictionary.Entities
{
    public class CodDictionaryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Result { get; set; }

        public List<string> Values { get; set; }
    }
}
