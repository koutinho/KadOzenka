using Core.Shared.Extensions;

namespace CommonSdks.Extentions
{
	public static class ObjectExtensions
	{
		//платформа не обрабатывает значения "Есть"/"Нет"
		public static bool TryParseToBooleanExtended(this string obj, out bool outValue)
		{
			var firstResult = obj.TryParseToBoolean(out outValue);
			if (firstResult)
				return true;

			var strValue = obj?.ToUpper();
			if (string.IsNullOrWhiteSpace(strValue))
				return false;

			switch (strValue)
			{
				case "ЕСТЬ":
					outValue = true;
					return true;
				case "НЕТ":
					outValue = false;
					return true;
				default:
					return false;
			}
		}
    }
}
