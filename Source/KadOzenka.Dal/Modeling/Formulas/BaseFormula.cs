﻿using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Formulas
{
	public abstract class BaseFormula
	{
		public static readonly string MarkTagInFormula = "метка";
		
		public abstract string FactorsSeparator { get; }

		public abstract decimal GetA0(OMModel model);
		
		public abstract string GetPartForNoneMarkType(ModelInfoForFormula modelInfo);

		public abstract string GetPartForDefaultMarkType(ModelInfoForFormula modelInfo);

		public abstract string GetPartForStraightMarkType(ModelInfoForFormula modelInfo);

		public abstract string GetPartForReverseMarkType(ModelInfoForFormula modelInfo);
	}
}