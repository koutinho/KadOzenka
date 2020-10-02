using System.Collections.Generic;

namespace KadOzenka.Dal.GbuObject.Dto
{
	/// <summary>
	/// Соответствие атрибутов ГБУ и КО
	/// </summary>
	public class ExportAttributeItem
	{
		/// <summary>
		/// Идентификатор фактора КО
		/// </summary>
		public long IdAttributeKO;
		/// <summary>
		/// Идентификатор фактора ГБУ
		/// </summary>
		public long IdAttributeGBU;
	}

	/// <summary>
	/// Настройки переноса атрибутов из ГБУшной части в КОшную
	/// </summary>
	public class GbuExportAttributeSettings
	{
		/// <summary>
		/// Список заданий на оценку
		/// </summary>
		public List<long> TaskFilter;

		/// <summary>
		/// Список сопоставленных атрибутов
		/// </summary>
		public List<ExportAttributeItem> Attributes;
	}
}
