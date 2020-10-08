using System.Collections.Generic;
using System.IO;
using Core.Register;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.Registers.GbuRegistersServices;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class KRSummaryResultsService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly RosreestrRegisterService _rosreestrRegisterService;
		private readonly GbuCodRegisterService _gbuCodRegisterService;
		private readonly string _reportOksSqlFileName = "KRSummaryResultsOKS";
		private readonly string _reportZuSqlFileName = "KRSummaryResultsZU";

		public KRSummaryResultsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
            _rosreestrRegisterService = new RosreestrRegisterService();
            _gbuCodRegisterService = new GbuCodRegisterService();
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
                _rosreestrRegisterService.GetObjectNameAttribute().Id,
                _rosreestrRegisterService.GetConstructionPurposeAttribute().Id,
                _rosreestrRegisterService.GetAddressAttribute().Id,
                _rosreestrRegisterService.GetLocationAttribute().Id,
                _rosreestrRegisterService.GetParcelAttribute().Id,
                _rosreestrRegisterService.GetBuildYearAttribute().Id,
                _rosreestrRegisterService.GetCommissioningYearAttribute().Id,
                _rosreestrRegisterService.GetFloorsNumberAttribute().Id,
                _rosreestrRegisterService.GetUndergroundFloorsNumberAttribute().Id,
                _rosreestrRegisterService.GetFloorAttribute().Id,
                _rosreestrRegisterService.GetWallMaterialAttribute().Id,
				_gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id
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
                _rosreestrRegisterService.GetTypeOfUseByDocumentsAttribute().Id,
                _rosreestrRegisterService.GetAddressAttribute().Id,
                _rosreestrRegisterService.GetLocationAttribute().Id,
                _rosreestrRegisterService.GetParcelCategoryAttribute().Id,
				_gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id
			);

			var result = QSQuery.ExecuteSql<KRSummaryResultsZuDto>(sql);

			return result;
		}
	}
}
