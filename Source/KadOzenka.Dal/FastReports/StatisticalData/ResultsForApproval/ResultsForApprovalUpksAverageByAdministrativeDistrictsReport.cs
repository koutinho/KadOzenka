using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsForApproval
{
	public class ResultsForApprovalUpksAverageByAdministrativeDistrictsReport : ResultsForApprovalUpksAverageReport
	{
		protected override string GetReportTitle(bool isOks)
		{
			return
				$"Среднее значение УПКС объектов недвижимости по административным округам города Москвы ({(isOks ? "ОКС" : "ЗУ")})";
		}

		protected override string GetDataNameColumnText()
		{
			return "Наименование административного округа города Москвы";
		}

		protected override StatisticDataAreaDivisionType GetStatisticDataAreaDivisionTypeReport()
		{
			return StatisticDataAreaDivisionType.Districts;
		}
	}
}
