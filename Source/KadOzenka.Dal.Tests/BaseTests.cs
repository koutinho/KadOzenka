using System;

namespace KadOzenka.Dal.Tests
{
	public class BaseTests
	{
		protected string GenerateRandomString(string beginning = "", int maxNumberOfCharacters = 5)
		{
			var guid = Guid.NewGuid();
			var salted = $"{beginning}_{guid}";

			return salted.Substring(0, Math.Min(maxNumberOfCharacters, salted.Length));
		}
	}
}
