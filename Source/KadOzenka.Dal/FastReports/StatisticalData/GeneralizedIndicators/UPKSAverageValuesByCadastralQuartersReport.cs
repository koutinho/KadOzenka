using System.Collections.Specialized;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.FastReports.StatisticalData.GeneralizedIndicators
{
	public class UPKSAverageValuesByCadastralQuartersReport : BaseGeneralizedIndicatorsReport
	{
		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(UPKSAverageValuesByCadastralQuartersReport);
		}

		protected override string GetReportTitle(NameValueCollection query)
		{
			return
				"Обобщенные показатели результатов расчета кадастровой стоимости по кадастровым кварталам города Москвы";
		}

		protected override string GetDataNameColumnText(NameValueCollection query)
		{
			return "Номер кадастрового квартала";
		}

		protected override StatisticDataAreaDivisionType GetStatisticDataAreaDivisionTypeReport(NameValueCollection query)
		{
			return StatisticDataAreaDivisionType.Quarters;
		}
	}
}
