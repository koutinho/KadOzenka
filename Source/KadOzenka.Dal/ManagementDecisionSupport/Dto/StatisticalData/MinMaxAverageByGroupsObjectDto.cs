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

        public decimal? UpksObjectValue { get; set; }
        public decimal? UpksObjectCost { get; set; }
        public decimal? UpksObjectSquare { get; set; }

        public decimal? UprsObjectValue { get; set; }
        public decimal? UprsObjectCost { get; set; }
        public decimal? UprsObjectSquare { get; set; }

        public string CadastralNumber { get; set; }
	}
}
