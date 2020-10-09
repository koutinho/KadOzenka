using System.Collections.Generic;
using System.IO;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.Registers.GbuRegistersServices;

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
		private readonly GbuCodRegisterService _gbuCodRegisterService;
		private readonly string _reportSqlFileName = "GeneralizedIndicators";

		public GeneralizedIndicatorsService(StatisticalDataService statisticalDataService, GbuCodRegisterService gbuCodRegisterService)
		{
			_statisticalDataService = statisticalDataService;
			_gbuCodRegisterService = gbuCodRegisterService;
		}

		public List<GeneralizedIndicatorsDto> GetData(long[] taskIdList, StatisticDataAreaDivisionType areaDivisionType, PropertyObjectType propertyObjectType)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, areaDivisionType, string.Join(", ", taskIdList), propertyObjectType, _gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id);
			var result = QSQuery.ExecuteSql<GeneralizedIndicatorsDto>(sql);

			return result;
		}
	}
}
