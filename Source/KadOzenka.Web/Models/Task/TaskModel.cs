using System;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Tasks.Dto;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Task
{
    public class TaskModel
    {
        public long Id { get; set; }
        public int RegisterId => OMTask.GetRegisterId();

        [Display(Name = "Дата загрузки")]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Номер документа")]
        public string IncomingDocumentRegNumber { get; set; }

        [Display(Name = "Наименование документа")]
        public string IncomingDocumentDescription { get; set; }

        [Display(Name = "Дата документа")]
        public DateTime? IncomingDocumentDate { get; set; }

        [Display(Name = "Тип статьи")]
        public string NoteType { get; set; }

        [Display(Name = "Тур")]
        public long? TourYear { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }


        public static TaskModel ToModel(TaskDto task)
        {
            return new TaskModel
            {
                Id = task.Id,
                CreationDate = task.CreationDate,
                IncomingDocumentRegNumber = task.IncomingDocument?.RegNumber,
                IncomingDocumentDescription = task.IncomingDocument?.Description,
                IncomingDocumentDate = task.IncomingDocument?.CreationDate,
                NoteType = task.NoteType,
                TourYear = task.Tour?.Year,
                Status = task.Status
            };
        }
    }
}
