using ObjectModel.Directory;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class SubjectsUPKSByTypeAndPurposeObjectDto : CalcDto
	{
		public PropertyTypes PropertyTypeCode { get; set; }
		public string Purpose { get; set; }
		public bool HasPurpose { get; set; }
		public long? GbuObjectId { get; internal set; }
	}
}
