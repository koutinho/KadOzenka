using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.Tasks.Dto;
using KadOzenka.Web.Models.GbuObject;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Task
{
    public class TaskCreationModel
    {
        public long Id { get; set; }
        public int RegisterId => OMTask.GetRegisterId();

        [Display(Name = "Тур")]
        public long? TourYear { get; set; }

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

        public static TaskCreationModel ToModel(TaskDto task)
        {
            return new TaskCreationModel
            {
	            Id = task.Id,
                TourYear = task.Tour?.Year,
	            NoteType = task.NoteType,
	            EstimationDate = task.EstimationDate
            };
        }
    }
}
