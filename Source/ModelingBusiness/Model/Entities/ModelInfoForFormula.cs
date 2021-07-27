namespace ModelingBusiness.Model.Entities
{
	public class ModelInfoForFormula
	{
		public string AttributeName { get; }
		public string Correction { get; }
		public string Coefficient { get; }
		public string CorrectingTerm { get; }
		public string K { get; }

		public ModelInfoForFormula(string attributeName, string correction, string coefficient,
			string correctingTerm, string k)
		{
			AttributeName = attributeName;
			Correction = correction;
			Coefficient = coefficient;
			CorrectingTerm = correctingTerm;
			K = k;
		}
	}
}
