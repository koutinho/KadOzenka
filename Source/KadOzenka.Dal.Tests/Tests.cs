using System;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests
{
	[TestFixture]
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test1()
		{
			Console.WriteLine("test");
			Assert.Pass();
		}

		[Test]
		public void Test2()
		{
			Assert.Pass();
		}
	}
}