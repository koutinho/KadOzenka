using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Modeling
{
	[TestFixture]
	public class OmModelTests
	{
		[Test]
		[TestCase(KoAlgoritmType.Line, 1, 2, 3, 1)]
		[TestCase(KoAlgoritmType.Exp, 1, 2, 3, 2)]
		[TestCase(KoAlgoritmType.Multi, 1, 2, 3, 3)]
		public void Check_A0_For_Model(KoAlgoritmType type, decimal line, decimal exp, decimal mult,
			decimal expected)
		{
			var model = new OMModel
			{
				AlgoritmType_Code = type,
				A0 = line,
				A0ForExponential = exp,
				A0ForMultiplicative = mult
			};

			Assert.That(model.GetA0(), Is.EqualTo(expected));
		}
	}
}