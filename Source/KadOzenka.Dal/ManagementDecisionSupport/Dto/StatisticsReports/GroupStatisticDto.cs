namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports
{
	public class GroupStatisticDto : UnitObjectDto
	{
		public string Group { get; set; }
		public long? SubGroupId { get; set; }
		public string SubGroup { get; set; }
	}
}
