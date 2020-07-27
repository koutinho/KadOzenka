using System;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class ResultsAnalysisDto
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
