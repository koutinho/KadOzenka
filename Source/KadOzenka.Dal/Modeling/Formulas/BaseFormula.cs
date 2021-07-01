using System.Text;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling.Formulas
{
	public abstract class BaseFormula
	{
		public static readonly string MarkTagInFormula = "метка";
		public abstract string FactorsSeparator { get; }


		public abstract string GetPartForNoneMarkType(ModelInfoForFormula modelInfo);

		public abstract string GetPartForDefaultMarkType(ModelInfoForFormula modelInfo);

		public abstract string GetPartForStraightMarkType(ModelInfoForFormula modelInfo);

		public abstract string GetPartForReverseMarkType(ModelInfoForFormula modelInfo);

		public abstract string GetBaseFormulaPart(OMModel model, string factors);

		public string ProcessNumber(decimal number)
		{
			var numberInFormula = $"{number}";
			if (number < 0)
			{
				numberInFormula = $"({numberInFormula})";
			}

			//все стронние библиотеки (для отрисовки формулы и расчета) работают с ".",
			//но разделить для культуры в приложении - ","
			return numberInFormula.Replace(",", ".");
		}
	}
}