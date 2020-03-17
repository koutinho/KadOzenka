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
        public string IncomingDocumentRegNumber { get; set; }

        [Display(Name = "Наименование документа")]
        public string IncomingDocumentDescription { get; set; }

        [Display(Name = "Дата документа")]
        public DateTime? IncomingDocumentDate { get; set; }

        [Display(Name = "Тип статьи")]
        public KoNoteType? NoteType { get; set; }

        [Display(Name = "Тур")]
        public long? TourYear { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }

        public long? CommonNumberOfImportedObjects { get; set; }

        public long? PossibleTotalCountOfObjects { get; set; }


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
                TourYear = task.Tour?.Year,
                Status = task.Status,
                CommonNumberOfImportedObjects = task.CommonNumberOfImportedObjects,
                PossibleTotalCountOfObjects = task.PossibleTotalCountOfObjects
            };
        }
    }
}
