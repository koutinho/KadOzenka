using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class NumberOfObjectsByZoneAndSubgroupsObjectDto
	{
		public long? TourId { get; set; }
		public long? GroupId { get; set; }
		public string CadastralNumber { get; set; }
		public long? ObjectId { get; set; }
		public long? Zone { get; set; }
		public string ZoneNameByCircles { get; set; }
		public string DistrictName { get; set; }
		public string RegionName { get; set; }
		public string ZoneDistrict { get; set; }
		public string ZoneDistrictRegion { get; set; }
		public string PropertyType { get; set; }
		public bool IsGroupChanged { get; set; }
		public CalcDto Calc { get; set; }
	}
}
