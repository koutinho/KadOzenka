using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Tasks.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Task
{
    public class TaskModel
    {
        public long Id { get; set; }
        public int RegisterId => OMTask.GetRegisterId();

        [Display(Name = "Дата загрузки")]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Дата изменения сведения (дата оценки)")]
        public DateTime? EstimationDate { get; set; }

        [Display(Name = "Номер документа")]
        [Required(ErrorMessage = "Поле Номер документа обязательное")]
        public string IncomingDocumentRegNumber { get; set; }

        [Display(Name = "Наименование документа")]
        public string IncomingDocumentDescription { get; set; }

        [Display(Name = "Дата создания документа")]
        [Required(ErrorMessage = "Поле Дата создания документа обязательное")]
        public DateTime? IncomingDocumentDate { get; set; }

        [Display(Name = "Дата выпуска документа")]
        [Required(ErrorMessage = "Поле Дата выпуска документа обязательное")]
        public DateTime? IncomingDocumentApproveDate { get; set; }

        [Display(Name = "Тип статьи")]
        [Required(ErrorMessage = "Поле Тип статьи обязательное")]
        public KoNoteType? NoteType { get; set; }

        [Display(Name = "Тур")]
        public long? TourYear { get; set; }


        public static TaskModel ToModel(TaskDto task)
        {
            return new TaskModel
            {
                Id = task.Id,
                CreationDate = task.CreationDate,
                EstimationDate = task.EstimationDate,
                IncomingDocumentRegNumber = task.IncomingDocument?.RegNumber,
                IncomingDocumentDescription = task.IncomingDocument?.Description,
                IncomingDocumentDate = task.IncomingDocument?.CreationDate,
                NoteType = task.NoteType,
                TourYear = task.Tour?.Year
            };
        }
    }
}
