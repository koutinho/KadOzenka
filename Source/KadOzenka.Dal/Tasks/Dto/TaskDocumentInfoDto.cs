using System;

namespace KadOzenka.Dal.Tasks.Dto
{
	public class TaskDocumentInfoDto
	{
		public long? TaskId { get; set; }
		public DateTime? DocumentCreateDate { get; set; }
		public string DocumentRegNumber { get; set; }
		public string KoNoteType { get; set; }
	}
}
