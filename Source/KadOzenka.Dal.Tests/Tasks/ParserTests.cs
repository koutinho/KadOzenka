//using NUnit.Framework;
//using org.mariuszgromada.math.mxparser;

//namespace KadOzenka.Dal.UnitTests.Tasks
//{
//	/// <summary>
//	/// Класс с примерами работы со сторонней библиотекой из расчета Кадастровой Стоимость
//	/// </summary>
//	[TestFixture]
//	public class ParserTests : BaseTasksTests
//	{
//		[Test]
//		public void How_To_Calculate_Simple_Formula()
//		{
//			var initialString = @"2 * (a + 2)^1 * (6/(b+4) + 4)^2 * (c + 1)^3 * ((d + 7) / 11 + 2)^4";

//			var a = new Argument("a", 1);
//			var b = new Argument("b", 2);
//			var c = new Argument("c", 3);
//			var d = new Argument("d", 4);
//			var e = new Expression(initialString, a, b, c, d);
//			var result = e.calculate();
//			mXparser.consolePrintln("Res 4: " + e.ToString() + " = " + e.calculate());

//			Assert.That(result, Is.EqualTo(777600));
//		}

//		[Test]
//		public void How_To_Calculate_Negative_Numbers()
//		{
//			var factorName = "factor_25023100";
//			var initialString = @$"2 * ({factorName} + (-1))^(-3)";

//			var a = new Argument(factorName, 3);
//			var e = new Expression(initialString, a);
//			var result = e.calculate();

//			Assert.That(result, Is.EqualTo(0.25));
//		}


//		[Test]
//		public void How_To_Calculate_Float_Numbers()
//		{
//			var factorName = "factor_25023100";
//			var initialString = "2 * (factor_25023100 + 2.1)^3.2";

//			var a = new Argument(factorName, 1);
//			var e = new Expression(initialString, a);
//			var result = e.calculate();

//			Assert.That(result, Is.EqualTo(74.71).Within(0.01));
//		}
//	}
//}