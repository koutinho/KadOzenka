using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class MinMaxAverageUPKSByAdministrativeDistrictsDto
	{
		public string AdditionalName { get; set; }
		public string Name { get; set; }
		public int ObjectsCount { get; set; }
		public UpksCalcType UpksCalcType { get; set; }
		public string PropertyType { get; set; }
		public decimal? UpksCalcValue { get; set; }
	}
}
