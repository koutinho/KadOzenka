using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.LongProcess.RecycleBin;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tasks.Dto;

namespace KadOzenka.Web.Models.Task
{
	public class TaskDeleteModel
	{
		public long TaskId { get; set; }
		public string TaskName { get; set; }
		public long? TourYear { get; set; }
		public bool CanTaskBeDeleted { get; set; }
		public bool IsDuplicateProcessExists { get; set; }

		public static TaskDeleteModel ToModel(TaskDto dto, bool canTaskBeDeleted, bool isDuplicateProcessExists)
		{
			return new TaskDeleteModel
			{
				TaskId = dto.Id, 
				TourYear = dto.Tour?.Year,
				TaskName = TaskService.GetTemplateForTaskName(dto.EstimationDate, dto.IncomingDocument.CreationDate,
					dto.IncomingDocument.RegNumber, dto.NoteType.GetEnumDescription()),
				CanTaskBeDeleted = canTaskBeDeleted,
				IsDuplicateProcessExists = isDuplicateProcessExists
			};
		}

		public MoveTaskToRecycleBinLongProcessParams ToSettings()
		{
			return new MoveTaskToRecycleBinLongProcessParams
			{
				TaskId = TaskId,
				TaskName = TaskName,
				TourYear = TourYear,
				UserId = SRDSession.GetCurrentUserId().Value
			};
		}
	}
}
