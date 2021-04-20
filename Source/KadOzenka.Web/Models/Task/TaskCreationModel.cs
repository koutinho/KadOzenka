using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
