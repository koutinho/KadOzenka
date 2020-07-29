using System;
using ObjectModel.Core.TD;

namespace KadOzenka.Dal.Documents
{
    public class DocumentService
    {
        public OMInstance GetDocumentById(long? documentId)
        {
            if (documentId.GetValueOrDefault() == 0)
                throw new Exception("Не передан Id документа для поиска");

            var document = OMInstance.Where(x => x.Id == documentId).SelectAll().ExecuteFirstOrDefault();
            if(document == null)
                throw new Exception($"Не найден документ с Id='{documentId}'");

            return document;
        }
    }
}
