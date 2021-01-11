using System.ComponentModel;
using Core.Shared.Attributes;

namespace KadOzenka.Dal.DataImport.DataImporterByTemplate
{
	public enum DataImportStatus
	{
		[ShortTitle("Успешно")]
		[Description("Загрузка успешно завершена")]
		Success,

		[ShortTitle("Ошибка")]
		[Description("Не удалось выполнить загрузку. Файл содержит некорректные данные")]
		Failed,

		[ShortTitle("Частично загружено")]
		[Description("Файл частично загружен")]
		PartiallyLoaded
	}
}
