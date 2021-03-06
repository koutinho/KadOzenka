using ModelingBusiness.Model.Entities;
using ObjectModel.KO;

namespace ModelingBusiness.Model.Formulas
{
	public class ExponentialFormula : BaseFormula
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
			var a0 = ProcessNumber(model.A0ForExponential == null ? 1 : model.A0ForExponentialInFormula);
			return $"{a0} * exp({factors})";
		}
	}
}
