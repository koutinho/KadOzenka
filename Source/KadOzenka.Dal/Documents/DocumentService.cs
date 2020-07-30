﻿using System;
using KadOzenka.Dal.Documents.Dto;
using ObjectModel.Core.TD;
using ObjectModel.KO;

namespace KadOzenka.Dal.Documents
{
    public class DocumentService
    {
        private readonly DateTime _defaultCreateDate = DateTime.Today;

        public OMInstance GetDocumentById(long? documentId)
        {
            return GetDocumentByIdInternal(documentId);
        }

        public int AddDocument(DocumentDto documentDto)
        {
            ValidateDocument(documentDto);

            return new OMInstance
            {
                Description = documentDto.Description,
                RegNumber = documentDto.RegNumber,
                ApproveDate = documentDto.ApproveDate,
                CreateDate = documentDto.CreateDate ?? _defaultCreateDate
            }.Save();
        }

        public void UpdateDocument(DocumentDto documentDto)
        {
            ValidateDocument(documentDto);

            var document = GetDocumentByIdInternal(documentDto.Id);

            document.Description = documentDto.Description;
            document.RegNumber = documentDto.RegNumber;
            document.ApproveDate = documentDto.ApproveDate;

            document.Save();
        }

        public void DeleteDocument(long documentId)
        {
            var document = GetDocumentByIdInternal(documentId);

            var taskWithIncomingDocument = OMTask.Where(x => x.DocumentId == documentId).ExecuteFirstOrDefault();
            if(taskWithIncomingDocument != null)
                throw new Exception($"Нельзя удалить документ, т.к. он является входящим документом для задачи c Id='{taskWithIncomingDocument.Id}'");

            var taskWithResponseDocument = OMTask.Where(x => x.ResponseDocId == documentId).ExecuteFirstOrDefault();
            if (taskWithResponseDocument != null)
                throw new Exception($"Нельзя удалить документ, т.к. он является исходящим документом для задачи c Id='{taskWithResponseDocument.Id}'");

            document.Destroy();
        }


        #region Support Methods

        private OMInstance GetDocumentByIdInternal(long? documentId)
        {
            if (documentId.GetValueOrDefault() == 0)
                throw new Exception("Не передан Id документа для поиска");

            var document = OMInstance.Where(x => x.Id == documentId).SelectAll().ExecuteFirstOrDefault();
            if (document == null)
                throw new Exception($"Не найден документ с Id='{documentId}'");

            return document;
        }

        private void ValidateDocument(DocumentDto documentDto)
        {
            if (string.IsNullOrWhiteSpace(documentDto.Description))
                throw new Exception("Не заполнено Наименование документа");

            if (string.IsNullOrWhiteSpace(documentDto.RegNumber))
                throw new Exception("Не заполнен Номер документа");

            var createDate = documentDto.CreateDate ?? _defaultCreateDate;
            var isTheSameDocumentExists = OMInstance
                .Where(x => x.RegNumber == documentDto.RegNumber && x.CreateDate == createDate).ExecuteExists();
            if (isTheSameDocumentExists)
                throw new Exception(
                    $"Документ с номером '{documentDto.RegNumber}' и датой создания '{createDate.ToShortDateString()}' уже существует");
        }

        #endregion
    }
}
