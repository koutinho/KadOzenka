using System;

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
		public DateTime? GeneralCalcDate { get; set; }
		public DateTime? ResultCalcDate { get; set; }
	}
}
