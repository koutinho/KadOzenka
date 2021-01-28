namespace KadOzenka.Dal.LongProcess.RecycleBin
{
	public class FlushOldDataFromRecycleBinLongProcessParams
	{
		public int KeepDataForPastNDays { get; set; } = 30;
	}
}
