using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsForApproval
{
	public class ResultsForApprovalUpksAverageByCadastralQuartersReport : ResultsForApprovalUpksAverageReport
	{
		protected override string GetReportTitle(bool isOks)
		{
			return
				$"Средние значение УПКС объектов недвижимости по кадастровым кварталам города Москвы ({(isOks ? "ОКС" : "ЗУ")})";
		}

		protected override string GetDataNameColumnText()
		{
			return "Номер кадастрового квартала";
		}

		protected override StatisticDataAreaDivisionType GetStatisticDataAreaDivisionTypeReport()
		{
			return StatisticDataAreaDivisionType.Quarters;
		}
	}
}
