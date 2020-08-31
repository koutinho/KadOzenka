using Core.Shared.Extensions;
using ObjectModel.Directory;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class CalculationStatisticsDto
	{
		public long SubgroupId { get; set; }
		public string SubgroupName { get; set; }
		public int CalculationMethod { get; set; }
		public string CalculationMethodName => ((KoGroupAlgoritm) CalculationMethod).GetEnumDescription();
		public string Formula { get; set; }
		public string FactorsSubgroups { get; set; }
		public decimal? Coef { get; set; }
		public string SighMarket { get; set; }
	}
}
