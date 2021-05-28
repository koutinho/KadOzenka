using System.Collections.Generic;
using System.Linq;

namespace KadOzenka.Dal.CodDictionary.Entities
{
    public class CodDictionaryValue
    {
        public long Id { get; set; }
        public List<CodDictionaryValuePure> Values { get; set; }
        public string Code { get; set; }

        public CodDictionaryValue(long id, string code, List<CodDictionaryValuePure> values)
        {
            Id = id;
            Code = code;
            Values = values;
        }

        public override string ToString()
        {
            return $"{Code}: {string.Join(',', Values.Select(x => x.Value))}";
        }

        public string GetValueByAttributeId(long attributeId)
        {
            var res = Values.FirstOrDefault(x => x.AttributeId == attributeId)?.Value;
            return res;
        }
    }

    public class CodDictionaryValuePure
    {
        public long AttributeId { get; set; }
        public string Value { get; set; }

        public CodDictionaryValuePure()
        {

        }

        public CodDictionaryValuePure(long attributeId, string value)
        {
            AttributeId = attributeId;
            Value = value;
        }
    }
}
