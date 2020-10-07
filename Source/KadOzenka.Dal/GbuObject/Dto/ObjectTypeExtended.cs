using System.ComponentModel;

namespace KadOzenka.Dal.GbuObject.Dto
{
	public enum ObjectTypeExtended
	{
		[Description("Объект капитального строительства")]
		Oks,
		[Description("Земельный участок")]
		Zu,
		[Description("ОКС/ЗУ")]
		Both
	}
}
