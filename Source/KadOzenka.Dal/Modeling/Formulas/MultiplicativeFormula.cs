using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Formulas
{
	public class MultiplicativeFormula : BaseFormula
	{
		public override string FactorsSeparator => "*";


		public override string GetPartForNoneMarkType(ModelInfoForFormula modelInfo)
		{
			return $"({modelInfo.AttributeName} + {modelInfo.WeightInFormula})^{modelInfo.B0InFormula}";
		}

		public override string GetPartForDefaultMarkType(ModelInfoForFormula modelInfo)
		{
			return $"({MarkTagInFormula}({modelInfo.AttributeName}) + {modelInfo.WeightInFormula})^{modelInfo.B0InFormula}";
		}

		public override string GetPartForStraightMarkType(ModelInfoForFormula modelInfo)
		{
			return $"(({modelInfo.AttributeName} + {modelInfo.CorrectingTermInFormula}) / {modelInfo.KInFormula} + {modelInfo.WeightInFormula})^{modelInfo.B0InFormula}";
		}

		public override string GetPartForReverseMarkType(ModelInfoForFormula modelInfo)
		{
			return $"({modelInfo.KInFormula}/({modelInfo.AttributeName}+{modelInfo.CorrectingTermInFormula}) + {modelInfo.WeightInFormula})^{modelInfo.B0InFormula}";
		}

		public override string GetBaseFormulaPart(OMModel model, string factors)
		{
			var a0 = model.A0ForMultiplicative == null ? 1 : model.A0ForMultiplicativeInFormula;
			return $"{a0} * {factors}";
		}
	}
}
