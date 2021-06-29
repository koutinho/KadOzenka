namespace KadOzenka.Dal.Modeling.Formulas
{
	public class ModelInfoForFormula
	{
		public string AttributeName { get; }
		public string WeightInFormula { get; }
		public string B0InFormula { get; }
		public string CorrectingTermInFormula { get; }
		public string KInFormula { get; }

		public ModelInfoForFormula(string attributeName, string weightInFormula, string b0InFormula,
			string correctingTermInFormula, string kInFormula)
		{
			AttributeName = attributeName;
			WeightInFormula = weightInFormula;
			B0InFormula = b0InFormula;
			CorrectingTermInFormula = correctingTermInFormula;
			KInFormula = kInFormula;
		}
	}
}
