using System.Collections.Generic;

namespace KadOzenka.Dal.CodDictionary.Entities
{
    //TODO KOMO-7 переименовать классы наоборот
    public class CodDictionaryValues
    {
        public long Id { get; set; }
        public List<CodDictionaryValue> Values { get; set; }
        public string Code { get; set; }

        public CodDictionaryValues(long id, List<CodDictionaryValue> values)
        {
            Id = id;
            Values = values;
        }
    }

    public class CodDictionaryValue
    {
        public long AttributeId { get; set; }
        public string Value { get; set; }

        public CodDictionaryValue()
        {

        }

        public CodDictionaryValue(long attributeId, string value)
        {
            AttributeId = attributeId;
            Value = value;
        }
    }
}
