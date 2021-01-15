using System;
using System.Collections.Generic;
using System.IO;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using ObjectModel.Directory;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class QualityPricingFactorsEncodingResultsService
	{
		public readonly QueryManager QueryManager;
		private readonly StatisticalDataService _statisticalDataService;
		private readonly RosreestrRegisterService _rosreestrRegisterService;
		private readonly GbuCodRegisterService _gbuCodRegisterService;
		private readonly string _reportGroupingSqlFileName = "QualityPricingFactorsEncodingResultsGrouping";
		private readonly string _reportOksSqlFileName = "QualityPricingFactorsEncodingResultsOks";
		private readonly string _reportZuSqlFileName = "QualityPricingFactorsEncodingResultsZu";

		public QualityPricingFactorsEncodingResultsService(StatisticalDataService statisticalDataService, GbuCodRegisterService gbuCodRegisterService)
		{
			QueryManager = new QueryManager();
			_statisticalDataService = statisticalDataService;
            _rosreestrRegisterService = new RosreestrRegisterService();
            _gbuCodRegisterService = gbuCodRegisterService;
		}

		public List<QualityPricingFactorsEncodingResultsOksDto> GetDataForOksObjects(long[] taskIdList, long parentKnAttributeId, long typeOfUsingNameAttributeId, long typeOfUsingCodeAttributeId,
			long typeOfUsingCodeSourceAttributeId, long typeOfUsingGroupCodeAttributeId, long functionalGroupNameAttributeId, long segmentAttributeId)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportOksSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			if (QueryManager.IsRequestCancellationToken())
			{
				return new List<QualityPricingFactorsEncodingResultsOksDto>();
			}

			var sql = string.Format(contents, string.Join(", ", taskIdList),
				RegisterCache.GetAttributeData(parentKnAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingNameAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingCodeAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingCodeSourceAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingGroupCodeAttributeId).Id,
				RegisterCache.GetAttributeData(segmentAttributeId).Id,
				RegisterCache.GetAttributeData(functionalGroupNameAttributeId).Id,
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
			var result = QueryManager.ExecuteSql<QualityPricingFactorsEncodingResultsOksDto>(sql);

			return result;
		}

		public List<QualityPricingFactorsEncodingResultsZuDto> GetDataForZuObjects(long[] taskIdList, long linkedObjectsInfoAttributeId, long linkedObjectsInfoSourceAttributeId,
			long segmentAttributeId, long typeOfUsingNameAttributeId, long typeOfUsingCodeAttributeId, long typeOfUsingCodeSourceAttributeId)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportZuSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", taskIdList),
				RegisterCache.GetAttributeData(linkedObjectsInfoAttributeId).Id,
				RegisterCache.GetAttributeData(linkedObjectsInfoSourceAttributeId).Id,
				RegisterCache.GetAttributeData(segmentAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingNameAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingCodeAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingCodeSourceAttributeId).Id,
                _rosreestrRegisterService.GetParcelNameAttribute().Id,
                _rosreestrRegisterService.GetTypeOfUseByDocumentsAttribute().Id,
                _rosreestrRegisterService.GetAddressAttribute().Id,
                _rosreestrRegisterService.GetLocationAttribute().Id,
				_gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id
			);
			var result = QSQuery.ExecuteSql<QualityPricingFactorsEncodingResultsZuDto>(sql);

			return result;
		}


		public List<QualityPricingFactorsEncodingResultsGroupingDto> GetGroupingData(long[] taskIdList, long tourId)
		{
			var codeGroupAttr = _statisticalDataService.GetGroupAttributeFromTourSettings(tourId);
			if (codeGroupAttr == null)
			{
				throw new Exception($"Для тура {tourId} не заданы настройки '{KoAttributeUsingType.CodeGroupAttribute.GetEnumDescription()}'");
			}

			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportGroupingSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", taskIdList), codeGroupAttr.Id);
			var result = QSQuery.ExecuteSql<QualityPricingFactorsEncodingResultsGroupingDto>(sql);

			return result;
		}
	}
}
