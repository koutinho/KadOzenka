using System.Collections.Generic;
using System.IO;
using Core.Register;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class KRSummaryResultsService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly string _reportOksSqlFileName = "KRSummaryResultsOKS";
		private readonly string _reportZuSqlFileName = "KRSummaryResultsZU";

		public KRSummaryResultsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
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
				_statisticalDataService.GetRosreestrObjectNameAttribute().Id,
				_statisticalDataService.GetRosreestrConstructionPurposeAttribute().Id,
				_statisticalDataService.GetRosreestrAddressAttribute().Id,
				_statisticalDataService.GetRosreestrLocationAttribute().Id,
				_statisticalDataService.GetRosreestrParcelAttribute().Id,
				_statisticalDataService.GetRosreestrBuildYearAttribute().Id,
				_statisticalDataService.GetRosreestrCommissioningYearAttribute().Id,
				_statisticalDataService.GetRosreestrFloorsNumberAttribute().Id,
				_statisticalDataService.GetRosreestrUndergroundFloorsNumberAttribute().Id,
				_statisticalDataService.GetRosreestrFloorAttribute().Id,
				_statisticalDataService.GetRosreestrWallMaterialAttribute().Id
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
				_statisticalDataService.GetRosreestrTypeOfUseByDocumentsAttribute().Id,
				_statisticalDataService.GetRosreestrAddressAttribute().Id,
				_statisticalDataService.GetRosreestrLocationAttribute().Id,
				_statisticalDataService.GetRosreestrParcelCategoryAttribute().Id
			);

			var result = QSQuery.ExecuteSql<KRSummaryResultsZuDto>(sql);

			return result;
		}
	}
}
