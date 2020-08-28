using System.Collections.Generic;
using System.IO;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public enum PropertyObjectType
	{
		Oks,
		Zu,
		OksAndZu
	}

	public class GeneralizedIndicatorsService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly string _reportSqlFileName = "GeneralizedIndicators";

		public GeneralizedIndicatorsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
		}

		public List<GeneralizedIndicatorsDto> GetData(long[] taskIdList, StatisticDataAreaDivisionType areaDivisionType, PropertyObjectType propertyObjectType)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, areaDivisionType, string.Join(", ", taskIdList), propertyObjectType);
			var result = QSQuery.ExecuteSql<GeneralizedIndicatorsDto>(sql);

			return result;
		}
	}
}
