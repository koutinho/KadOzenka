using System.ComponentModel;
using Core.Shared.Attributes;

namespace KadOzenka.Dal.Enum
{
	public enum UnitChangeStatus : long
	{
		/// <summary>
		/// Изменение группы
		/// </summary>
		[Description("Изменение группы")]
		[EnumCode("1")]
		Group = 1,

		/// <summary>
		/// Изменение ФС
		/// </summary>
		[Description("Изменение ФС")]
		[EnumCode("2")]
		Fs = 2,

		/// <summary>
		/// Изменение Материала стен"
		/// </summary>
		[Description("Изменение Материала стен")]
		[EnumCode("3")]
		WallMaterial = 3,

		/// <summary>
		/// Изменение года постройки
		/// </summary>
		[Description("Изменение года постройки")]
		[EnumCode("4")]
		BuildYear = 4
	}
}
