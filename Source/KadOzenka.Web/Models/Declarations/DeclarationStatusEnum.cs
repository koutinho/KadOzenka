using System.ComponentModel;

namespace KadOzenka.Web.Models.Declarations
{
	public enum DeclarationStatusEnum : long
	{
		/// <summary>
		/// Без статуса
		/// </summary>
		[Description("Без статуса")]
		None = 0,
		/// <summary>
		/// Открыто
		/// </summary>
		[Description("Открыто")]
		Opened = 1,
		/// <summary>
		/// Закрыто
		/// </summary>
		[Description("Закрыто")]
		Closed = 2,
	}
}
