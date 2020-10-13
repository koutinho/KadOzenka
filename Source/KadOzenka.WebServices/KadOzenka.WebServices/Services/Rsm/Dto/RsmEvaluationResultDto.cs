namespace KadOzenka.WebServices.Services.ModelDto
{
	public class RsmEvaluationResultDto
	{
		public string GroupNumber { get; set; }

		public string SubGroupNumber { get; set; }

		public decimal? Upks { get; set; }

		public decimal? CadastralCost { get; set; }

		public string TypeOfUseByDocuments { get; set; }

		public string TypeOfUseByClassifier { get; set; }
	}
}