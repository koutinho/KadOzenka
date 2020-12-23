using ObjectModel.Directory;

namespace KadOzenka.Dal.Tasks.Dto
{
	public class TaskDataComparingDto
	{
		public long Id { get; set; }
		public KoNoteType? NoteType { get; set; }
		public KoDataComparingTaskChangesStatus? DataComparingTaskChangesStatusCode { get; set; }
		public KoDataComparingCadastralCostStatus? DataComparingCadastralCostStatusCode { get; set; }
		public bool ContainsFdFilesComparingResult { get; set; }
		public bool IsTaskChangesPkkoFileUploaded { get; set; }
		public bool AreCostPkkoFilesUploaded { get; set; }
		public bool AreFdPkkoFilesUploaded { get; set; }
	}
}
