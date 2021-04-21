using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Register;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Web.Models.DataUpload;
using KadOzenka.Web.Models.GbuObject;
using Microsoft.AspNetCore.Http;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Task
{
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

        public List<IFormFile> XmlFiles { get; set; }
        public IFormFile ExcelFile { get; set; }
        public List<DataColumnDto> ExcelColumnsMapping { get; set; }


        public TaskCreationModel()
        {
            XmlFiles = new List<IFormFile>();
            ExcelColumnsMapping = new List<DataColumnDto>();
        }

        public void Validate()
        {
	        if (DocumentType == DocumentType.Excel)
	        {
		        if (ExcelFile == null)
			        throw new Exception("Не передан Excel-файл");

                if (ExcelColumnsMapping.Count == 0)
			        throw new Exception("Не указано соответствие колонок Excel-файла загружаемым атрибутам");

		        var notSelectedRequiredAttributeIds = Dal.DataImport.DataImporterGknNew.Consts.RequiredAttributeIds
			        .Except(ExcelColumnsMapping.Select(x => x.AttributeId)).ToList();
		        if (notSelectedRequiredAttributeIds.Count != 0)
		        {
			        var attributeNames = RegisterCache.RegisterAttributes
				        .Where(x => notSelectedRequiredAttributeIds.Contains(x.Key)).Select(x => x.Value.Name).ToList();
			        var message = string.Join(',', attributeNames);

			        throw new Exception($"Не указаны обязательные параметры: {message}");
                }
	        }
	        
        }
    }
}
