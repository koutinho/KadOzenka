using System.ComponentModel;

namespace KadOzenka.Dal.Enum
{
	public enum ScenarioCalculateEnum
	{
		[Description("Расчет ЕОН (ОКС + ЗУ)")]
		Eon = 1,
		[Description("Расчет ОКС без доли ЗУ")]
		Oks =2,
	}
}