using System.Collections.Generic;

namespace KadOzenka.Dal.CodDictionary.Entities
{
    //TODO KOMO-7 переименовать классы наоборот
    public class CodDictionaryValue
    {
        public long Id { get; set; }
        public List<CodDictionaryValuePure> Values { get; set; }
        public string Code { get; set; }

        public CodDictionaryValue(long id, List<CodDictionaryValuePure> values)
        {
            Id = id;
            Values = values;
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
