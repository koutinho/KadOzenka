using KadOzenka.Dal.Tasks.Dto;

namespace KadOzenka.Dal.Tasks.Responses
{
	public class TaskDataComparingDtoResponse
	{
		public bool Success { get; set; }
		public TaskDataComparingDto TaskDataComparingDto { get; set; }
		public string ErrorMessage { get; set; }
	}
}
