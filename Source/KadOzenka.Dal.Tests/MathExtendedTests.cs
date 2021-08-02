using System.Collections.Generic;
using CommonSdks;
using KadOzenka.Dal.Tests;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests
{
	public class MathExtendedTests : BaseTests
	{
		[Test]
		public void Can_Calculate_Median_For_Odd_Count()
		{
			var numbers = new List<decimal> {1, 4, 2, 3};

			var median = MathExtended.CalculateMedian(numbers);

			Assert.That(median, Is.EqualTo((double)(2 + 3)/2));
		}

		[Test]
		public void Can_Calculate_Median_For_Even_Count()
		{
			var numbers = new List<decimal> { 1, 4, 2 };

			var median = MathExtended.CalculateMedian(numbers);

			Assert.That(median, Is.EqualTo(2));
		}
	}
}
