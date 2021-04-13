using System.Collections.Generic;
using KadOzenka.Dal.CodDictionary.Entities;
using ObjectModel.KO;

namespace KadOzenka.Dal.CodDictionary
{
    public interface ICodDictionaryService
    {
        long AddCodDictionary(CodDictionaryDto codDictionary);

        void UpdateCodDictionary(CodDictionaryDto codDictionary);

        OMCodJob GetDictionary(long id);

        void DeleteDictionary(long id);

        void UpdateDictionaryValue(long dictionaryId, CodDictionaryValue value);

        CodDictionaryValue GetDictionaryValue(OMCodJob dictionary, long dictionaryValueId);

        CodDictionaryValue GetDictionaryValue(long registerId, long dictionaryValueId);

        List<CodDictionaryValue> GetDictionaryValues(long registerId);
    }
}
