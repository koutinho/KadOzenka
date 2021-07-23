using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.ChunkUpload.Dtos;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.DataImport.Validation;
using KadOzenka.Web.Models.GbuObject;
using Microsoft.AspNetCore.Http;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Task
{
	public class ColumnToAttributeMappingModel
	{
		public int ColumnIndex { get; set; }
		public long AttributeId { get; set; }
	}

    public class TaskCreationModel
    {
        public long Id { get; set; }
        public int RegisterId => OMTask.GetRegisterId();

        [Display(Name = "Тур")]
        public long? TourId { get; set; }

        [Display(Name = "Тип статьи")]
        [Required(ErrorMessage = "Поле Тип статьи обязательное")]
        public KoNoteType? NoteType { get; set; }

        [Display(Name = "Дата изменения сведения (дата оценки)")]
        public DateTime? EstimationDate { get; set; }

        /// <summary>
        /// Частичное представление документа (для создания еденицы оценки)
        /// </summary>
        [Display(Name = "Документ")]
        public PartialDocumentViewModel Document { get; set; } = new PartialDocumentViewModel();

        [Display(Name = "Тип файла")]
        public DocumentType DocumentType { get; set; }

        public List<FileContentDto> XmlFiles { get; set; }
        public IFormFile ExcelFile { get; set; }
        public List<ColumnToAttributeMappingModel> ExcelColumnsMapping { get; set; }


        public TaskCreationModel()
        {
            XmlFiles = new List<FileContentDto>();
            ExcelColumnsMapping = new List<ColumnToAttributeMappingModel>();
        }

        public void Validate()
        {
	        if (DocumentType == DocumentType.Excel)
	        {
		        if (ExcelFile == null && ExcelColumnsMapping.Count != 0)
			        throw new Exception("Сделано сопоставление полей, но не передан Excel-файл");

                if (ExcelFile != null && ExcelColumnsMapping.Count == 0)
			        throw new Exception("Не указано соответствие колонок Excel-файла загружаемым атрибутам");

                DataImporterGknValidator.ValidateExcelColumnsForNotPetition(ExcelColumnsMapping.Select(x => x.AttributeId).ToList());
	        }
        }
    }
}
