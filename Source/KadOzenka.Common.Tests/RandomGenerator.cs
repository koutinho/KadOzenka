using System;
using System.Collections.Generic;
using System.Linq;

namespace KadOzenka.Common.Tests
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

		public static int GenerateRandomInteger(int minNumber = 1, int maxNumber = 100)
		{
			return Random.Next(minNumber, maxNumber);
		}

		public static long GenerateRandomLong(int minNumber = 1, int maxNumber = 100)
		{
			return Random.Next(minNumber, maxNumber);
		}

		public static decimal GenerateRandomDecimal()
		{
			var integer = Random.Next();
			return (decimal) (integer / 2.3);
		}

		public static DateTime GenerateRandomDate()
		{
			var startDate = new DateTime(1995, 1, 1);
			var daysToToday = (DateTime.Today - startDate).Days;

			return startDate.AddDays(Random.Next(daysToToday));
		}

		public static List<T> CreateListWithRepeatedElements<T>(T element, int count)
		{
			var elements = new List<T>(count);
			elements.AddRange(Enumerable.Repeat(element, count));
			return elements;
		}
	}
}
