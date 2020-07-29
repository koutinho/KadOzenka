using System;
using KadOzenka.Dal.Documents.Dto;
using ObjectModel.Core.TD;

namespace KadOzenka.Dal.Documents
{
    public class DocumentService
    {
        public OMInstance GetDocumentById(long? documentId)
        {
            return GetDocumentByIdInternal(documentId);
        }

        public int UpdateDocument(DocumentDto documentDto)
        {
            var document = GetDocumentByIdInternal(documentDto.Id);

            document.Description = documentDto.Description;
            document.RegNumber = documentDto.RegNumber;
            document.ApproveDate = documentDto.ApproveDate;
            return document.Save();
        }

        public void DeleteDocument(long documentId)
        {
            var document = GetDocumentByIdInternal(documentId);

            document.Destroy();
        }


        #region Support Methods

        public OMInstance GetDocumentByIdInternal(long? documentId)
        {
            if (documentId.GetValueOrDefault() == 0)
                throw new Exception("Не передан Id документа для поиска");

            var document = OMInstance.Where(x => x.Id == documentId).SelectAll().ExecuteFirstOrDefault();
            if (document == null)
                throw new Exception($"Не найден документ с Id='{documentId}'");

            return document;
        }

        #endregion
    }
}
