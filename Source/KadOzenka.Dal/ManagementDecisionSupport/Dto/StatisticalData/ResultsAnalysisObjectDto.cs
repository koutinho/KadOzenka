using System;

namespace KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData
{
	public class ResultsAnalysisObjectDto
	{
		public string CadastralNumber { get; set; }
		public long? TaskTourId { get; set; }
		public long? TaskId { get; set; }
		public DateTime? TaskCreationDate { get; set; }
		public string PropertyType { get; set; }
		public decimal? Square { get; set; }
		public decimal? Upks { get; set; }
		public decimal? CadastralCost { get; set; }
		public string Status { get; set; }
	}
}
