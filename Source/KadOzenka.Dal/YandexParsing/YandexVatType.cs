using System.ComponentModel;

namespace KadOzenka.Dal.YandexParsing
{
	enum YandexVatType
	{
		[Description("НДС включен")]
		NDS,

		[Description("УСН")]
		USN
	}
}
