using System.Collections.Generic;
using System.IO;
using Core.Register;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.Registers;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class KRSummaryResultsService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly RosreestrRegisterService _rosreestrRegisterService;
		private readonly string _reportOksSqlFileName = "KRSummaryResultsOKS";
		private readonly string _reportZuSqlFileName = "KRSummaryResultsZU";

		public KRSummaryResultsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
            _rosreestrRegisterService = new RosreestrRegisterService();
		}

		public List<KRSummaryResultsOksDto> GetKRSummaryResultsOksData(long[] taskIdList, long klardAttributeId, long parentKnAttributeId)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportOksSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", taskIdList),
				RegisterCache.GetAttributeData(klardAttributeId).Id,
				RegisterCache.GetAttributeData(parentKnAttributeId).Id,
                _rosreestrRegisterService.GetRosreestrObjectNameAttribute().Id,
                _rosreestrRegisterService.GetRosreestrConstructionPurposeAttribute().Id,
                _rosreestrRegisterService.GetRosreestrAddressAttribute().Id,
                _rosreestrRegisterService.GetRosreestrLocationAttribute().Id,
                _rosreestrRegisterService.GetRosreestrParcelAttribute().Id,
                _rosreestrRegisterService.GetRosreestrBuildYearAttribute().Id,
                _rosreestrRegisterService.GetRosreestrCommissioningYearAttribute().Id,
                _rosreestrRegisterService.GetRosreestrFloorsNumberAttribute().Id,
                _rosreestrRegisterService.GetRosreestrUndergroundFloorsNumberAttribute().Id,
                _rosreestrRegisterService.GetRosreestrFloorAttribute().Id,
                _rosreestrRegisterService.GetRosreestrWallMaterialAttribute().Id
			);

			var result = QSQuery.ExecuteSql<KRSummaryResultsOksDto>(sql);

			return result;
		}

		public List<KRSummaryResultsZuDto> GetKRSummaryResultsZuData(long[] taskIdList, long klardAttributeId)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportZuSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", taskIdList),
				RegisterCache.GetAttributeData(klardAttributeId).Id,
                _rosreestrRegisterService.GetRosreestrTypeOfUseByDocumentsAttribute().Id,
                _rosreestrRegisterService.GetRosreestrAddressAttribute().Id,
                _rosreestrRegisterService.GetRosreestrLocationAttribute().Id,
                _rosreestrRegisterService.GetRosreestrParcelCategoryAttribute().Id
			);

			var result = QSQuery.ExecuteSql<KRSummaryResultsZuDto>(sql);

			return result;
		}
	}
}
