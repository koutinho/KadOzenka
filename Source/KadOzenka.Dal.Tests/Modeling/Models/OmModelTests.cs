using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Modeling.Models
{
	[TestFixture]
	public class OmModelTests
	{
		[TestCase(KoAlgoritmType.Line, 1, 2, 3, ExpectedResult = 1)]
		[TestCase(KoAlgoritmType.Exp, 1, 2, 3, ExpectedResult = 2)]
		[TestCase(KoAlgoritmType.Multi, 1, 2, 3, ExpectedResult = 3)]
		public decimal? Check_A0_For_Model(KoAlgoritmType type, decimal line, decimal exp, decimal mult)
		{
			var model = new OMModel
			{
				AlgoritmType_Code = type,
				A0 = line,
				A0ForExponential = exp,
				A0ForMultiplicative = mult
			};

			return model.GetA0();
		}
	}
}