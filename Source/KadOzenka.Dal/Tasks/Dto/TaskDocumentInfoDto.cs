using System;

namespace KadOzenka.Dal.Tasks.Dto
{
	public class TaskDocumentInfoDto
	{
		public long TaskId { get; set; }
		public long? TourId { get; set; }
		public DateTime? EstimationDate { get; set; }
		public DateTime? DocumentCreateDate { get; set; }
		public string DocumentRegNumber { get; set; }
		public string KoNoteType { get; set; }
	}
}
