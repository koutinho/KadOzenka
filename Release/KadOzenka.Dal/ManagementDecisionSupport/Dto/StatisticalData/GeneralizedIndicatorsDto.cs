using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class GeneralizedIndicatorsDto
	{
		public string AdditionalName { get; set; }
		public string Name { get; set; }
		public string GroupName { get; set; }
		public int ObjectsCount { get; set; }
		public UpksCalcType UpksCalcType { get; set; }
		public decimal? UpksCalcValue { get; set; }
	}
}
