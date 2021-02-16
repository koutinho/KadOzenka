using Core.Shared.Extensions;

namespace KadOzenka.Dal.Extentions
{
	public static class ObjectExtensions
	{
		public static bool ParseToBooleanFromString(this object obj)
		{
			var strValue = obj.ParseToStringNullable();

			return strValue?.Trim().ToLower() == "да";
		}
    }
}
