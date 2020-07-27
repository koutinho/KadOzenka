using System.Collections.Specialized;
using System.IO;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.FastReports.StatisticalData.GeneralizedIndicators
{
	public class GeneralizedIndicatorsReport : BaseGeneralizedIndicatorsReport
	{
		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(GeneralizedIndicatorsReport);
		}

		protected override string GetReportTitle(NameValueCollection query)
		{
			var reportType = GetQueryParam<string>("ReportType", query);
			switch (reportType)
			{
				case "По кадастровым районам":
					return "Обобщенные показатели результатов расчета кадастровой стоимости по кадастровым районам города Москвы";
				case "По административным округам":
					return "Обобщенные показатели результатов расчета кадастровой стоимости по административным округам города Москвы";
				case "По муниципальным районам":
					return "Обобщенные показатели результатов расчета кадастровой стоимости по районам города Москвы";
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}
		}

		protected override string GetDataNameColumnText(NameValueCollection query)
		{
			var reportType = GetQueryParam<string>("ReportType", query);
			switch (reportType)
			{
				case "По кадастровым районам":
					return "Номер кадастрового района";
				case "По административным округам":
					return "Наименование административного округа города Москвы";
				case "По муниципальным районам":
					return "Наименование района города Москвы";
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}
		}

		protected override StatisticDataAreaDivisionType GetStatisticDataAreaDivisionTypeReport(NameValueCollection query)
		{
			var reportType = GetQueryParam<string>("ReportType", query);
			switch (reportType)
			{
				case "По кадастровым районам":
					return StatisticDataAreaDivisionType.RegionNumbers;
				case "По административным округам":
					return StatisticDataAreaDivisionType.Districts;
				case "По муниципальным районам":
					return StatisticDataAreaDivisionType.Regions;
				default:
					throw new InvalidDataException($"Неизвестный тип формирования данных: {reportType}");
			}
		}
	}
}
