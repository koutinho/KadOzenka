using ObjectModel.Directory;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class MinMaxAverageByGroupsObjectDto
	{
		public string GroupName { get; set; }
		public bool HasGroup { get; set; }
		public PropertyTypes PropertyTypeCode { get; set; }
		public string Purpose { get; set; }
		public bool HasPurpose { get; set; }
		public long? GbuObjectId { get; set; }
		public CalcDto UpksCalcDto { get; set; }
		public CalcDto UprsCalcDto { get; set; }
		public string CadastralNumber { get; set; }
	}
}
