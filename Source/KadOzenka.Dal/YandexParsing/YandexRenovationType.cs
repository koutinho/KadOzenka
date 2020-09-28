using System.ComponentModel;

namespace KadOzenka.Dal.YandexParsing
{
	public enum YandexRenovationType
	{
		[Description("Дизайнерский ремонт")]
		DESIGNER_RENOVATION,

		[Description("Требуется ремонт")]
		NEEDS_RENOVATION,

		[Description("Не бабушкин ремонт")]
		NON_GRANDMOTHER,

		[Description("Косметический ремонт")]
		COSMETIC_DONE,

		[Description("Черновой ремонт")]
		PRIME_RENOVATION,

		[Description("Евроремонт")]
		EURO
	}
}
