using System.ComponentModel;

namespace KadOzenka.Dal.YandexParsing
{
	enum YandexQualityType
	{
		[Description("Отличное")]
		EXCELLENT,

		[Description("Хорошее")]
		GOOD,

		[Description("Нормальное")]
		NORMAL,

		[Description("Плохое")]
		POOR,

		[Description("Ужасное")]
		AWFUL
	}
}
