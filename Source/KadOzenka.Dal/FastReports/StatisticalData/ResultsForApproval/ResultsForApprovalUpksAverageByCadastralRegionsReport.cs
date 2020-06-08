using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsForApproval
{
	public class ResultsForApprovalUpksAverageByCadastralRegionsReport: ResultsForApprovalUpksAverageReport
	{
		protected override string GetReportTitle(bool isOks)
		{
			return
				$"Среднее значение УПКС объектов недвижимости по кадастровым районам города Москвы ({(isOks ? "ОКС" : "ЗУ")})";
		}

		protected override string GetDataNameColumnText()
		{
			return "Номер кадастрового района";
		}

		protected override StatisticDataAreaDivisionType GetStatisticDataAreaDivisionTypeReport()
		{
			return StatisticDataAreaDivisionType.RegionNumbers;
		}
	}
}
