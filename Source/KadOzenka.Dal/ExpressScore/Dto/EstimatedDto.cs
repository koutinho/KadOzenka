namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class EstimatedDto
	{
		public long Id { get; set; }

		public string WallMaterial { get; set; }

		public decimal DistanceToMetro { get; set; }

		public decimal DistanceToHistoryCityCenter { get; set; }

		public decimal DistanceToHighway { get; set; }

		public string IndustrialZone { get; set; }

		public decimal CoefficientTerritoryValue { get; set; }

		public int Floor { get; set; }
	}
}