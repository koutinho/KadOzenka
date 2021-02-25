namespace KadOzenka.Dal.LongProcess.Reports.AdditionalForms.AnalysisOfResuluts.Entities
{
	public class ReportItem
	{
		public string CadastralNumber { get; set; }
		public string PropertyType { get; set; }
		public decimal? Square { get; set; }
		public decimal? PreviousUpks { get; set; }
		public decimal? PreviousCadastralCost { get; set; }
		public decimal? Upks { get; set; }
		public decimal? CadastralCost { get; set; }
		public string Status { get; set; }
	}
}