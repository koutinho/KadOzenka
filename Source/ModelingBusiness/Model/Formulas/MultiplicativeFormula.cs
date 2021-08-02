using ModelingBusiness.Model.Entities;
using ObjectModel.KO;

namespace ModelingBusiness.Model.Formulas
{
	public class MultiplicativeFormula : BaseFormula
	{
		public override string FactorsSeparator => "*";


		public override string GetPartForNoneMarkType(ModelInfoForFormula modelInfo)
		{
			return $"({modelInfo.AttributeName} + {modelInfo.Correction})^{modelInfo.Coefficient}";
		}

		public override string GetPartForDefaultMarkType(ModelInfoForFormula modelInfo)
		{
			return $"({MarkTagInFormula}({modelInfo.AttributeName}) + {modelInfo.Correction})^{modelInfo.Coefficient}";
		}

		public override string GetPartForStraightMarkType(ModelInfoForFormula modelInfo)
		{
			return $"(({modelInfo.AttributeName} + {modelInfo.CorrectingTerm}) / {modelInfo.K} + {modelInfo.Correction})^{modelInfo.Coefficient}";
		}

		public override string GetPartForReverseMarkType(ModelInfoForFormula modelInfo)
		{
			return $"({modelInfo.K}/({modelInfo.AttributeName}+{modelInfo.CorrectingTerm}) + {modelInfo.Correction})^{modelInfo.Coefficient}";
		}

		public override string GetBaseFormulaPart(OMModel model, string factors)
		{
			var a0 = ProcessNumber(model.A0ForMultiplicative == null ? 1 : model.A0ForMultiplicativeInFormula);
			return $"{a0} * {factors}";
		}
	}
}
