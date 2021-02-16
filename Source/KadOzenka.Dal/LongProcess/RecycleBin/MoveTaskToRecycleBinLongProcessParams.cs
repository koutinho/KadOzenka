namespace KadOzenka.Dal.LongProcess.RecycleBin
{
	public class MoveTaskToRecycleBinLongProcessParams
	{
		public long TaskId { get; set; }
		public int UserId { get; set; }
		public string TaskName { get; set; }
		public long? TourYear { get; set; }
	}
}
