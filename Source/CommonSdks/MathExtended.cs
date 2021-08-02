using System.Collections.Generic;
using System.Linq;

namespace CommonSdks
{
	public static class MathExtended
	{
		public static decimal CalculateMedian(List<decimal> prices)
		{
			var count = prices.Count;
			var halfIndex = prices.Count / 2;
			var sortedPrices = prices.OrderBy(n => n).ToList();
			decimal median;
			if (count % 2 == 0)
			{
				median = (sortedPrices.ElementAt(halfIndex) + sortedPrices.ElementAt(halfIndex - 1)) / 2;
			}
			else
			{
				median = sortedPrices.ElementAt(halfIndex);
			}

			return median;
		}
	}
}
