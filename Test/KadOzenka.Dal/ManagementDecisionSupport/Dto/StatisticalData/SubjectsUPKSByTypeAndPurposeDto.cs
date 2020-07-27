using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class SubjectsUPKSByTypeAndPurposeDto
	{
		public string PropertyType { get; set; }
		public string Purpose { get; set; }
		public bool HasPurpose { get; set; }
		public int ObjectsCount { get; set; }
		public UpksCalcType UpksCalcType { get; set; }
		public decimal? UpksCalcValue { get; set; }
	}
}
