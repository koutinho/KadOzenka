using System.ComponentModel;

namespace KadOzenka.Web.Models.Sud
{
	public enum CourtStatusEnum : long
	{
		/// <summary>
		/// Без статуса
		/// </summary>
		[Description("Без статуса")]
		None = 0,
		/// <summary>
		/// Отказано
		/// </summary>
		[Description("Отказано")]
		Denied = 1,
		/// <summary>
		/// Удовлетворено
		/// </summary>
		[Description("Удовлетворено")]
		Satisfied = 2,
		/// <summary>
		/// Приостановлено
		/// </summary>
		[Description("Приостановлено")]
		Paused = 3,
		/// <summary>
		/// Частично удовлетворено
		/// </summary>
		[Description("Частично удовлетворено")]
		PartiallySatisfied = 4,
	}
}
