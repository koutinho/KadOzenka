using System;

namespace KadOzenka.Dal.Tests
{
	public static class RandomGenerator
	{
		private static Random Random { get; }

		static RandomGenerator()
		{
			Random = new Random();
		}

		public static string GetRandomString(string beginning = "", int maxNumberOfCharacters = 5)
		{
			var guid = Guid.NewGuid();
			var salted = $"{beginning}_{guid}";

			return salted.Substring(0, Math.Min(maxNumberOfCharacters, salted.Length));
		}

		public static int GenerateRandomInteger(int minNumber = 0, int maxNumber = 100)
		{
			return Random.Next(minNumber, maxNumber);
		}

		public static decimal GenerateRandomDecimal()
		{
			var integer = Random.Next();
			return (decimal) (integer / 2.3);
		}
	}
}
