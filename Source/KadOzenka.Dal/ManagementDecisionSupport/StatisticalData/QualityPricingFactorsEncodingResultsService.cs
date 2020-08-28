using System;
using System.Collections.Generic;
using System.IO;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using ObjectModel.Directory;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class QualityPricingFactorsEncodingResultsService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly string _reportGroupingSqlFileName = "QualityPricingFactorsEncodingResultsGrouping";
		private readonly string _reportOksSqlFileName = "QualityPricingFactorsEncodingResultsOks";
		private readonly string _reportZuSqlFileName = "QualityPricingFactorsEncodingResultsZu";

		public QualityPricingFactorsEncodingResultsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
		}

		public List<QualityPricingFactorsEncodingResultsOksDto> GetDataForOksObjects(long[] taskIdList, long parentKnAttributeId, long typeOfUsingNameAttributeId, long typeOfUsingCodeAttributeId,
			long typeOfUsingCodeSourceAttributeId, long typeOfUsingGroupCodeAttributeId, long functionalGroupNameAttributeId, long segmentAttributeId)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportOksSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", taskIdList),
				RegisterCache.GetAttributeData(parentKnAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingNameAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingCodeAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingCodeSourceAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUsingGroupCodeAttributeId).Id,
				RegisterCache.GetAttributeData(segmentAttributeId).Id,
				RegisterCache.GetAttributeData(functionalGroupNameAttributeId).Id,
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
			var result = QSQuery.ExecuteSql<QualityPricingFactorsEncodingResultsOksDto>(sql);

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
				_statisticalDataService.GetRosreestrParcelNameAttribute().Id,
				_statisticalDataService.GetRosreestrTypeOfUseByDocumentsAttribute().Id,
				_statisticalDataService.GetRosreestrAddressAttribute().Id,
				_statisticalDataService.GetRosreestrLocationAttribute().Id
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
