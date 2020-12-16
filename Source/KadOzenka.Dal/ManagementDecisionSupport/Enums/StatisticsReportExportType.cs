using System.ComponentModel;

namespace KadOzenka.Dal.ManagementDecisionSupport.Enums
{
	public enum StatisticsReportExportType
	{
		[Description("Статистика по количеству загруженных объектов")]
		ImportedObjects,
		[Description("Статистика по количеству выгруженных объектов")]
		ExportedObjects,
		[Description("Статистика по зонам")]
		ZoneStatistics,
		[Description("Статистика по ценообразующим факторам")]
		FactorStatistics,
		[Description("Статистика по группам")]
		GroupStatistics
	}
}
