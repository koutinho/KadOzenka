namespace KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults.Entities
{
	public class ReportItem
	{
		public string CadastralDistrict { get; set; }
		public string CadastralNumber { get; set; }
		public string Type { get; set; }
		public decimal? Square { get; set; }
		public decimal? Upks { get; set; }
		public decimal? Cost { get; set; }
	}
}
