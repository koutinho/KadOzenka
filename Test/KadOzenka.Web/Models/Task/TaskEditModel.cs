using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Tasks.Dto;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Task
{
    public class TaskEditModel : TaskModel
    {
        [Display(Name = "Тур")]
        public long? TourId { get; set; }

        [Display(Name = "Статус")]
        [Required(ErrorMessage = "Поле Статус обязательное")]
        public KoTaskStatus? StatusCode { get; set; }

        public long? CommonNumberOfImportedObjects { get; set; }

        public long? PossibleTotalCountOfObjects { get; set; }

        public TaskDto ToDto()
        {
            return new TaskDto
            {
                Id = Id,
                CreationDate = CreationDate,
                EstimationDate = EstimationDate,
                IncomingDocument =  new DocumentDto
                    {
                        RegNumber = IncomingDocumentRegNumber,
                        Description = IncomingDocumentDescription,
                        CreationDate = IncomingDocumentDate
                    },
                NoteType = NoteType,
                Tour = TourId.HasValue
                    ? new TourDto {Id = TourId.Value}
                    : null,
                StatusCode = StatusCode,
                CommonNumberOfImportedObjects = CommonNumberOfImportedObjects,
                PossibleTotalCountOfObjects = PossibleTotalCountOfObjects
            };
        }

        public static TaskEditModel ToEditModel(TaskDto task)
        {
            return new TaskEditModel
            {
                Id = task.Id,
                CreationDate = task.CreationDate,
                EstimationDate = task.EstimationDate,
                IncomingDocumentRegNumber = task.IncomingDocument?.RegNumber,
                IncomingDocumentDescription = task.IncomingDocument?.Description,
                IncomingDocumentDate = task.IncomingDocument?.CreationDate,
                NoteType = task.NoteType,
                TourId = task.Tour?.Id,
                TourYear = task.Tour?.Year,
                StatusCode = task.StatusCode,
                CommonNumberOfImportedObjects = task.CommonNumberOfImportedObjects,
                PossibleTotalCountOfObjects = task.PossibleTotalCountOfObjects
            };
        }
    }
}
