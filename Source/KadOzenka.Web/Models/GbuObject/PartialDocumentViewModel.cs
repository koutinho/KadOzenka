using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.Documents.Dto;

namespace KadOzenka.Web.Models.GbuObject
{
	public class PartialDocumentViewModel : IValidatableObject
    {
        public string ModelPrefix { get; set; }
        private DocumentService DocumentService { get; }

        [Display(Name = "Документ")]
        public long? IdDocument { get; set; }

        [Display(Name = "Номер")]
	    public string NewDocumentRegNumber { get; set; }

        [Display(Name = "Имя документа")]
        public string NewDocumentName { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime? NewDocumentDate { get; set; }
        
        [Display(Name = "Дата выпуска")]
        public DateTime? NewDocumentApproveDate { get; set; }

        [Display(Name = "Дата изменения")]
        public DateTime? NewDocumentChangeDate { get; set; }

        /// <summary>
        /// Флаг указывающий используем старый или новый документ
        /// </summary>
        public bool IsNewDocument { get; set; } = false;


        public PartialDocumentViewModel()
        {
	        NewDocumentDate = DateTime.Today;
	        DocumentService = new DocumentService();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        if (IsNewDocument && NewDocumentApproveDate == null)
	        {
		        yield return
			        new ValidationResult("'Дата выпуска документа' - обязательное поле");
	        }
        }


        public void ProcessDocument()
        {
            if (!IsNewDocument)
                return;

            if (NewDocumentApproveDate == null)
	            throw new Exception("Не заполнена Дата выпуска документа");

            var documentDto = new DocumentDto
            {
                RegNumber = NewDocumentRegNumber,
                Description = NewDocumentName,
                CreateDate = NewDocumentDate,
                ApproveDate = NewDocumentApproveDate,
                ChangeDate = NewDocumentChangeDate
            };

            var documentId = DocumentService.AddDocument(documentDto);
            if (documentId == 0)
                throw new Exception("Не корректные данные для создания нового документа");

            IdDocument = documentId;
        }
    }
}