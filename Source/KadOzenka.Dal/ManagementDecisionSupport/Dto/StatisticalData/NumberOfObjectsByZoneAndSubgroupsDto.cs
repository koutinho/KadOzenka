namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class NumberOfObjectsByZoneAndSubgroupsDto
	{
		public long? Zone { get; set; }
		public string ZoneNameByCircles { get; set; }
		public string DistrictName { get; set; }
		public string RegionName { get; set; }
		public string ZoneDistrict { get; set; }
		public string ZoneDistrictRegion { get; set; }
		public string PropertyType { get; set; }
		public long FirstTourObjectCount { get; set; }
		public long SecondTourObjectCount { get; set; }
		public long FirstTourObjectCountWithoutGroupChanging { get; set; }
		public long SecondTourObjectCountWithoutGroupChanging { get; set; }
		public long FirstTourObjectCountWithGroupChanging { get; set; }
		public long SecondTourObjectCountWithGroupChanging { get; set; }
		public decimal? FirstTourMinUpks { get; set; }
		public decimal? FirstTourMaxUpks { get; set; }
		public decimal? FirstTourAverageUpks { get; set; }
		public decimal? SecondTourMinUpks { get; set; }
		public decimal? SecondTourMaxUpks { get; set; }
		public decimal? SecondTourAverageUpks { get; set; }
		public decimal? MinUpksVarianceBetweenTours { get; set; }
		public decimal? AverageUpksVarianceBetweenTours { get; set; }
		public decimal? MaxUpksVarianceBetweenTours { get; set; }
		public decimal? MinUpksVarianceBetweenToursWithoutGroupChanging { get; set; }
		public decimal? AverageUpksVarianceBetweenToursWithoutGroupChanging { get; set; }
		public decimal? MaxUpksVarianceBetweenToursWithoutGroupChanging { get; set; }
	}
}
