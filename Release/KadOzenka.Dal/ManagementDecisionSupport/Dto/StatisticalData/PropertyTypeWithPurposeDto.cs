using ObjectModel.Directory;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class PropertyTypeWithPurposeDto
	{
		public long? GbuObjectId { get; set; }
		public PropertyTypes PropertyTypeCode { get; set; }
		public string Purpose { get; set; }
		public bool HasPurpose { get; set; }
	}
}
