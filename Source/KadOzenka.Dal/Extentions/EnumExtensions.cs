using System;
using Core.Shared.Extensions;

namespace KadOzenka.Dal.Extentions
{
	public static class EnumExtensions
	{
		public static long? GetEnumByShortTitle<T>(string shortTitle)
		{
			if (shortTitle.IsNullOrEmpty()) return 0;

			Array enumValues = System.Enum.GetValues(typeof(T));

			foreach (System.Enum enumValue in enumValues)
			{
				if (enumValue.ToString() == "None") continue;
				if (enumValue.GetShortTitle().ToLower() == shortTitle.ToLower()) return Convert.ToInt64(enumValue);
			}

			return 0;
		}
    }
}
