using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Formulas
{
	public class LinearFormula : BaseFormula
	{
		public override string FactorsSeparator => "+";

		
		public override string GetPartForNoneMarkType(ModelInfoForFormula modelInfo)
		{
			return $"{modelInfo.AttributeName}*{modelInfo.B0InFormula}";
		}

		public override string GetPartForDefaultMarkType(ModelInfoForFormula modelInfo)
		{
			return $"метка({modelInfo.AttributeName})*{modelInfo.B0InFormula}";
		}

		public override string GetPartForStraightMarkType(ModelInfoForFormula modelInfo)
		{
			return $"({modelInfo.AttributeName}+{modelInfo.CorrectingTermInFormula})/{modelInfo.KInFormula} * {modelInfo.B0InFormula}";
		}

		public override string GetPartForReverseMarkType(ModelInfoForFormula modelInfo)
		{
			return $"{modelInfo.KInFormula}/({modelInfo.AttributeName}+{modelInfo.CorrectingTermInFormula})*{modelInfo.B0InFormula}";
		}

		public override string GetBaseFormulaPart(OMModel model, string factors)
		{
			var a0 = ProcessNumber(model.A0 == null ? 1 : model.A0ForLinearInFormula);
			
			return $"{a0} + {factors}";
		}
	}
}
