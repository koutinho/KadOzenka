namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class CalculationStatisticsDto
	{
		public long SubgroupId { get; set; }
		public string SubgroupName { get; set; }
		public string CalculationMethod { get; set; }
		public string Formula { get; set; }
		public string FactorsSubgroups { get; set; }
		public decimal? Coef { get; set; }
		public string SighMarket { get; set; }
	}
}
