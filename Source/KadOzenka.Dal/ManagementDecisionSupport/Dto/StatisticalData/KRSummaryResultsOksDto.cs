using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class KRSummaryResultsOksDto
	{
		public string CadastralNumber { get; set; }
		public string PropertyType { get; set; }
		public decimal? Square { get; set; }
		public string Name { get; set; }
		public string Purpose { get; set; }
		public string Address { get; set; }
		public string Kladr { get; set; }
		public string ParentKn { get; set; }
		public string Location { get; set; }
		public string CadastralQuarter { get; set; }
		public string ZuCadastralNumber { get; set; }
		public string BuildingYear { get; set; }
		public string CommissioningYear { get; set; }
		public string FloorCount { get; set; }
		public string UndergroundFloorCount { get; set; }
		public string FloorNumber { get; set; }
		public string WallMaterial { get; set; }
		public long? AvailabilityPercentage { get; set; }
		public decimal? Upks { get; set; }
		public decimal? CadastralCost { get; set; }
	}
}
