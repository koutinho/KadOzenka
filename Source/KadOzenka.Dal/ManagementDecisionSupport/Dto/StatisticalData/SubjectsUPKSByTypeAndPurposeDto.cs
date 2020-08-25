using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class SubjectsUPKSByTypeAndPurposeDto
	{
		public string PropertyType { get; set; }
		public string Purpose { get; set; }
		public bool HasPurpose { get; set; }
		public int ObjectsCount { get; set; }
		public int UpksCalcType { get; set; }
		public UpksCalcType UpksCalcTypeEnum => (UpksCalcType)UpksCalcType;
		public decimal? UpksCalcValue { get; set; }
	}
}
