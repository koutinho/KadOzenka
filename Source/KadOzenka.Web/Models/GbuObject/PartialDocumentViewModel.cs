using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.Documents.Dto;

namespace KadOzenka.Web.Models.GbuObject
{
	public class PartialDocumentViewModel
    {
        public string ModelPrefix { get; set; }
        private DocumentService DocumentService { get; set; }

        /// <summary>
        /// Идентификатор документа, куда будет записан результат 
        /// </summary>
        [Display(Name = "Документ")]
        public long? IdDocument { get; set; }

        /// <summary>
        /// Имя нового документа
        /// </summary>
        [Display(Name = "Номер")]
	    public string NewDocumentRegNumber { get; set; }

        /// <summary>
        /// Имя нового документа
        /// </summary>
        [Display(Name = "Имя документа")]
        public string NewDocumentName { get; set; }

        [Display(Name = "Дата")]
        public DateTime? NewDocumentDate { get; set; }

        /// <summary>
        /// Флаг указывающий используем старый или новый документ
        /// </summary>
        public bool IsNewDocument { get; set; } = false;


        public PartialDocumentViewModel()
        {
            DocumentService = new DocumentService();
        }


        public void ProcessDocument()
        {
            if (!IsNewDocument)
                return;

            var documentDto = new DocumentDto
            {
                RegNumber = NewDocumentRegNumber,
                Description = NewDocumentName,
                CreateDate = NewDocumentDate
            };

            var documentId = DocumentService.AddDocument(documentDto);
            if (documentId == 0)
                throw new Exception("Не корректные данные для создания нового документа");

            IdDocument = documentId;
        }
    }
}