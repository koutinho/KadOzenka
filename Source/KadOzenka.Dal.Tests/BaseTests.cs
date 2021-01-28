using System;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests
{
	public class BaseTests
	{
		protected Random Random { get; set; }

		[OneTimeSetUp]
		public void SetUp()
		{
			Random = new Random();
		}


		protected string GetRandomString(string beginning = "", int maxNumberOfCharacters = 5)
		{
			var guid = Guid.NewGuid();
			var salted = $"{beginning}_{guid}";

			return salted.Substring(0, Math.Min(maxNumberOfCharacters, salted.Length));
		}
	}
}
