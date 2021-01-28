using NUnit.Framework;

namespace KadOzenka.Dal.Tests
{
	[TestFixture]
	public class Test
	{
		[Test]
		[TestCase(0, 0, 0)]
		public void AddNumbersTest(int a, int b, int expected)
		{
			var result = a + b;

			Assert.That(result, Is.EqualTo(expected));
		}
    }
}
