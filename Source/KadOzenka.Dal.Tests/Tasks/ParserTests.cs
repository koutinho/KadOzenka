using NUnit.Framework;
using org.mariuszgromada.math.mxparser;

namespace KadOzenka.Dal.UnitTests.Tasks
{
	[TestFixture]
	public class ParserTests : BaseTasksTests
	{
		[Test]
		public void Test()
		{
			//var a = @"";
			//var initialString = @"10 * ("№ квартала БТИ" + 2)^1 * (6/("test"+5) + 4)^3 * (метка("UNOM") + 8)^7 * (("Аварийность_2018" + 10) / 11 + 13)^12 * ("12" + 2)^1";
			//A - № квартала БТИ
			//B - test
			//C - метка("UNOM")
			//D - Аварийность_2018
			var initialString = @"2 * (a + 2)^1 * (6/(b+4) + 4)^2 * (c + 1)^3 * ((d + 7) / 11 + 2)^4";

			var a = new Argument("a", 1);
			var b = new Argument("b", 2);
			var c = new Argument("c", 3);
			var d = new Argument("d", 4);
			var e = new Expression(initialString, a, b, c, d);
			var result = e.calculate();
			mXparser.consolePrintln("Res 4: " + e.getExpressionString() + " = " + e.calculate());

			Assert.That(result, Is.EqualTo(777600));
		}
	}
}