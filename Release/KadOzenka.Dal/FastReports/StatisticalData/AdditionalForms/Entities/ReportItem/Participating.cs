namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.ReportItem
{
	public class Participating
	{
		public long? ParticipatingCount { get; set; }
		public long? CountInYear { get; set; }
		public long? CountInDays { get; set; }

		public Participating(long? participatingCount, long? countInYear, long? countInDays)
		{
			ParticipatingCount = participatingCount;
			CountInYear = countInYear;
			CountInDays = countInDays;
		}
	}
}