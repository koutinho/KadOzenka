using System.ComponentModel;

namespace KadOzenka.Dal.Api.Enums
{
	public enum SectionType
	{
		[Description("Серилог")]
		Serilog = 1,
		[Description("Платформа")]
		Core = 2,
		[Description("Кад оценка")]
		KoConfig = 3,
	}
}