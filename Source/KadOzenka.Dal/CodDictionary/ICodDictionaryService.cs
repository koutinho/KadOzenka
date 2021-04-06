using KadOzenka.Dal.CodDictionary.Entities;

namespace KadOzenka.Dal.CodDictionary
{
    public interface ICodDictionaryService
    {
        long AddCodDictionary(CodDictionaryDto codDictionary);
    }
}
