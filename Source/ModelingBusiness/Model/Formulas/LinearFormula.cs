using ModelingBusiness.Model.Entities;
using ObjectModel.KO;

namespace ModelingBusiness.Model.Formulas
{
	public class LinearFormula : BaseFormula
	{
		public override string FactorsSeparator => "+";

		
		public override string GetPartForNoneMarkType(ModelInfoForFormula modelInfo)
		{
			return $"{modelInfo.AttributeName}*{modelInfo.Coefficient}";
		}

		public override string GetPartForDefaultMarkType(ModelInfoForFormula modelInfo)
		{
			return $"метка({modelInfo.AttributeName})*{modelInfo.Coefficient}";
		}

		public override string GetPartForStraightMarkType(ModelInfoForFormula modelInfo)
		{
			return $"({modelInfo.AttributeName}+{modelInfo.CorrectingTerm})/{modelInfo.K} * {modelInfo.Coefficient}";
		}

		public override string GetPartForReverseMarkType(ModelInfoForFormula modelInfo)
		{
			return $"{modelInfo.K}/({modelInfo.AttributeName}+{modelInfo.CorrectingTerm})*{modelInfo.Coefficient}";
		}

		public override string GetBaseFormulaPart(OMModel model, string factors)
		{
			var a0 = ProcessNumber(model.A0 == null ? 1 : model.A0ForLinearInFormula);
			
			return $"{a0} + {factors}";
		}
	}
}
