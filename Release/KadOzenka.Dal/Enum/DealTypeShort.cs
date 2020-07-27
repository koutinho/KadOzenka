using System.ComponentModel;

namespace KadOzenka.Dal.Enum
{
	public enum DealTypeShort
	{
		[Description("Аренда")]
		Rent = 1,

		[Description("Продажа")]
		Sale = 2
	}
}