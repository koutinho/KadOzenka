using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class GeneralizedIndicatorsDto
	{
		public string AdditionalName { get; set; }
		public string Name { get; set; }
		public long GroupId { get; set; }
		public string GroupName { get; set; }
		public int ObjectsCount { get; set; }
		public int UpksCalcType { get; set; }
		public UpksCalcType UpksCalcTypeEnum => (UpksCalcType) UpksCalcType;
		public decimal? UpksCalcValue { get; set; }
	}
}
