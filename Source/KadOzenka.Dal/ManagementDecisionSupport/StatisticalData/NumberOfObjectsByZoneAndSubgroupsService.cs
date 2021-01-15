using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;
using Platform.Register;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class NumberOfObjectsByZoneAndSubgroupsService
	{
		private readonly StatisticalDataService _statisticalDataService;
		public readonly QueryManager QueryManager;
		public NumberOfObjectsByZoneAndSubgroupsService(StatisticalDataService statisticalDataService)
		{
			QueryManager = new QueryManager();
			_statisticalDataService = statisticalDataService;
		}

		public List<NumberOfObjectsByZoneAndSubgroupsDto> GetNumberOfObjectsByZoneAndSubgroupsData(long firstTourId, long secondTourId, NumberOfObjectsByZoneAndSubgroupsReportDataType reportDataType, bool isOksReportType)
		{
			string fileName = string.Empty, contents = string.Empty;

			if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && isOksReportType) fileName = "NumberOfObjectsByZoneAndSubgroupsData_Initial_OKS";
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && !isOksReportType) fileName = "NumberOfObjectsByZoneAndSubgroupsData_Initial_ZU";
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && isOksReportType) fileName = "NumberOfObjectsByZoneAndSubgroupsData_VUON_OKS";
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && !isOksReportType) fileName = "NumberOfObjectsByZoneAndSubgroupsData_VUON_ZU";

			using (var sr = new StreamReader(Core.ConfigParam.Configuration.GetFileStream(fileName, "sql", "SqlQueries"))) contents = sr.ReadToEnd();
			string firstQuery =
				$"SELECT CORE_REGISTER_ATTRIBUTE.* FROM CORE_REGISTER_ATTRIBUTE, KO_TOUR_ATTRIBUTE_SETTINGS WHERE CORE_REGISTER_ATTRIBUTE.ID=ATTRIBUTE_ID AND TOUR_ID={firstTourId} AND ATTRIBUTE_USING_TYPE_CODE=1;";
			RegisterAttribute firstTourCodeGroupAttr = QueryManager.ExecuteSql<RegisterAttribute>(firstQuery).FirstOrDefault();

			string secondQuery =
				$"SELECT CORE_REGISTER_ATTRIBUTE.* FROM CORE_REGISTER_ATTRIBUTE, KO_TOUR_ATTRIBUTE_SETTINGS WHERE CORE_REGISTER_ATTRIBUTE.ID=ATTRIBUTE_ID AND TOUR_ID={secondTourId} AND ATTRIBUTE_USING_TYPE_CODE=1;";
			RegisterAttribute secondTourCodeGroupAttr = QueryManager.ExecuteSql<RegisterAttribute>(secondQuery).FirstOrDefault();

			if (firstTourCodeGroupAttr == null) throw new Exception($"Для тура {firstTourId} не заданы настройки '{KoAttributeUsingType.CodeGroupAttribute.GetEnumDescription()}'");
			if (secondTourCodeGroupAttr == null) throw new Exception($"Для тура {secondTourId} не заданы настройки '{KoAttributeUsingType.CodeGroupAttribute.GetEnumDescription()}'");

			long? firstTourTaskId = null, secondTourTaskId = null;
			;
			if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && isOksReportType)
				firstTourTaskId = QueryManager.ExecuteSql<TourTaskIdsRegisterAttributes>(
						$"SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE (KO_UNIT.PROPERTY_TYPE_CODE<> 4 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL) AND (KO_UNIT.PROPERTY_TYPE_CODE<> 0 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL)) AND TOUR_ID={firstTourId} AND NOTE_TYPE_CODE=4 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;")
					.FirstOrDefault()?.id;
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && !isOksReportType)
				firstTourTaskId = QueryManager.ExecuteSql<TourTaskIdsRegisterAttributes>(
					$"SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE KO_UNIT.PROPERTY_TYPE_CODE=4) AND TOUR_ID={firstTourId} AND NOTE_TYPE_CODE=4 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;").FirstOrDefault()?.id;
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && isOksReportType)
				firstTourTaskId = QueryManager.ExecuteSql<TourTaskIdsRegisterAttributes>(
					$"SELECT ID FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE (KO_UNIT.PROPERTY_TYPE_CODE<> 4 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL) AND (KO_UNIT.PROPERTY_TYPE_CODE<> 0 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL)) AND TOUR_ID={firstTourId} AND NOTE_TYPE_CODE=1 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;").FirstOrDefault()?.id;
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && !isOksReportType) 
				firstTourTaskId = QueryManager.ExecuteSql<TourTaskIdsRegisterAttributes>(
					$"SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE KO_UNIT.PROPERTY_TYPE_CODE=4) AND TOUR_ID={firstTourId} AND NOTE_TYPE_CODE=1 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;").FirstOrDefault()?.id;

			if (!firstTourTaskId.HasValue) ThrowNotDataException(firstTourId, reportDataType, isOksReportType);

			if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && isOksReportType) 
				secondTourTaskId = QueryManager.ExecuteSql<TourTaskIdsRegisterAttributes>(
					$"SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE (KO_UNIT.PROPERTY_TYPE_CODE<> 4 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL) AND (KO_UNIT.PROPERTY_TYPE_CODE<> 0 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL)) AND TOUR_ID={secondTourId} AND NOTE_TYPE_CODE=4 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;").FirstOrDefault()?.id;
			else if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && !isOksReportType)
				secondTourTaskId = QueryManager.ExecuteSql<TourTaskIdsRegisterAttributes>(
					$"SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE KO_UNIT.PROPERTY_TYPE_CODE=4) AND TOUR_ID={secondTourId} AND NOTE_TYPE_CODE=4 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;").FirstOrDefault()?.id;
			else if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && isOksReportType)
				secondTourTaskId = QueryManager.ExecuteSql<TourTaskIdsRegisterAttributes>(
					$"SELECT ID FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE (KO_UNIT.PROPERTY_TYPE_CODE<> 4 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL) AND (KO_UNIT.PROPERTY_TYPE_CODE<> 0 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL)) AND TOUR_ID={secondTourId} AND NOTE_TYPE_CODE=1 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;").FirstOrDefault()?.id;
			else if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && !isOksReportType)
				secondTourTaskId = QueryManager.ExecuteSql<TourTaskIdsRegisterAttributes>(
					$"SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE KO_UNIT.PROPERTY_TYPE_CODE=4) AND TOUR_ID={secondTourId} AND NOTE_TYPE_CODE=1 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;").FirstOrDefault()?.id;

			if (!secondTourTaskId.HasValue) ThrowNotDataException(secondTourId, reportDataType, isOksReportType);

			CoreRegisterData firstGbuAttributesRD = QueryManager.ExecuteSql<CoreRegisterData>(string.Format("SELECT CORE_REGISTER.* FROM CORE_REGISTER WHERE REGISTERID={0};", firstTourCodeGroupAttr.RegisterId)).FirstOrDefault();
			CoreRegisterData secondGbuAttributesRD = QueryManager.ExecuteSql<CoreRegisterData>(string.Format("SELECT CORE_REGISTER.* FROM CORE_REGISTER WHERE REGISTERID={0};", secondTourCodeGroupAttr.RegisterId)).FirstOrDefault();

			string subQueryFirstTour = string.Empty, subQuerySecondTour = string.Empty;

			if ((AllpriPartitioningType)firstGbuAttributesRD.allpri_partitioning == AllpriPartitioningType.DataType) 
				subQueryFirstTour = string.Format("SELECT ((" + "\n\tSELECT COUNT(*)\n\tFROM {0}_TXT\n\tLEFT JOIN CORE_SRD_USER ON CORE_SRD_USER.ID={0}_TXT.CHANGE_USER_ID\n\tLEFT JOIN CORE_TD_INSTANCE ON CORE_TD_INSTANCE.ID={0}_TXT.CHANGE_DOC_ID\n\tWHERE {0}_TXT.OBJECT_ID=KO_UNIT.OBJECT_ID AND {0}_TXT.ATTRIBUTE_ID IN ({1}))>1" +
														  "\n\tOR (\n\tSELECT COUNT(*)\n\tFROM {0}_NUM\n\tLEFT JOIN CORE_SRD_USER ON CORE_SRD_USER.ID={0}_NUM.CHANGE_USER_ID\n\tLEFT JOIN CORE_TD_INSTANCE ON CORE_TD_INSTANCE.ID={0}_NUM.CHANGE_DOC_ID\n\tWHERE {0}_NUM.OBJECT_ID=KO_UNIT.OBJECT_ID AND {0}_NUM.ATTRIBUTE_ID IN ({1}))>1" +
														  "\n\tOR (\n\tSELECT COUNT(*)\n\tFROM {0}_DT\n\tLEFT JOIN CORE_SRD_USER ON CORE_SRD_USER.ID={0}_DT.CHANGE_USER_ID\n\tLEFT JOIN CORE_TD_INSTANCE ON CORE_TD_INSTANCE.ID={0}_DT.CHANGE_DOC_ID\n\tWHERE {0}_DT.OBJECT_ID=KO_UNIT.OBJECT_ID AND {0}_DT.ATTRIBUTE_ID IN ({1}))>1)", 
													firstGbuAttributesRD.allpri_table, 
													firstTourCodeGroupAttr.Id);
			else if ((AllpriPartitioningType)firstGbuAttributesRD.allpri_partitioning == AllpriPartitioningType.AttributeId)
				subQueryFirstTour = string.Format("SELECT COUNT(*)>1\n\tFROM {0}\n\tLEFT JOIN CORE_SRD_USER ON CORE_SRD_USER.ID={0}.CHANGE_USER_ID\n\tLEFT JOIN CORE_TD_INSTANCE ON CORE_TD_INSTANCE.ID={0}.CHANGE_DOC_ID\n\tWHERE {0}.OBJECT_ID=KO_UNIT.OBJECT_ID", $"{firstGbuAttributesRD.allpri_table}_{firstTourCodeGroupAttr.Id}");
			if ((AllpriPartitioningType)secondGbuAttributesRD.allpri_partitioning == AllpriPartitioningType.DataType)
				subQuerySecondTour = string.Format("SELECT ((" + "\n\tSELECT COUNT(*)\n\tFROM {0}_TXT\n\tLEFT JOIN CORE_SRD_USER ON CORE_SRD_USER.ID={0}_TXT.CHANGE_USER_ID\n\tLEFT JOIN CORE_TD_INSTANCE ON CORE_TD_INSTANCE.ID={0}_TXT.CHANGE_DOC_ID\n\tWHERE {0}_TXT.OBJECT_ID=KO_UNIT.OBJECT_ID AND {0}_TXT.ATTRIBUTE_ID IN ({1}))>1" +
														   "\n\tOR (\n\tSELECT COUNT(*)\n\tFROM {0}_NUM\n\tLEFT JOIN CORE_SRD_USER ON CORE_SRD_USER.ID={0}_NUM.CHANGE_USER_ID\n\tLEFT JOIN CORE_TD_INSTANCE ON CORE_TD_INSTANCE.ID={0}_NUM.CHANGE_DOC_ID\n\tWHERE {0}_NUM.OBJECT_ID=KO_UNIT.OBJECT_ID AND {0}_NUM.ATTRIBUTE_ID IN ({1}))>1" +
														   "\n\tOR (\n\tSELECT COUNT(*)\n\tFROM {0}_DT\n\tLEFT JOIN CORE_SRD_USER ON CORE_SRD_USER.ID={0}_DT.CHANGE_USER_ID\n\tLEFT JOIN CORE_TD_INSTANCE ON CORE_TD_INSTANCE.ID={0}_DT.CHANGE_DOC_ID\n\tWHERE {0}_DT.OBJECT_ID=KO_UNIT.OBJECT_ID AND {0}_DT.ATTRIBUTE_ID IN ({1}))>1)",
													secondGbuAttributesRD.allpri_table,
													secondTourCodeGroupAttr.Id);
			else if ((AllpriPartitioningType)secondGbuAttributesRD.allpri_partitioning == AllpriPartitioningType.AttributeId)
				subQuerySecondTour = string.Format("SELECT COUNT(*)>1\n\tFROM {0}\n\tLEFT JOIN CORE_SRD_USER ON CORE_SRD_USER.ID={0}.CHANGE_USER_ID\n\tLEFT JOIN CORE_TD_INSTANCE ON CORE_TD_INSTANCE.ID={0}.CHANGE_DOC_ID\n\tWHERE {0}.OBJECT_ID=KO_UNIT.OBJECT_ID", $"{secondGbuAttributesRD.allpri_table}_{secondTourCodeGroupAttr.Id}");

			string query = string.Format(contents, subQueryFirstTour, subQuerySecondTour, firstTourTaskId.GetValueOrDefault(), secondTourTaskId.GetValueOrDefault(), firstTourId, secondTourId);

			List<NumberOfObjectsByZoneAndSubgroupsDto> globalResult = QueryManager.ExecuteSql<NumberOfObjectsByZoneAndSubgroupsDto>(query);
			List<NumberOfObjectsByZoneAndSubgroupsDto> result = new List<NumberOfObjectsByZoneAndSubgroupsDto>();
			result.AddRange(globalResult);

			if (isOksReportType)
				result
					.AddRange(globalResult
					.GroupBy(x => new { x.Zone, x.DistrictName, x.RegionName })
					.Select(g => new NumberOfObjectsByZoneAndSubgroupsDto
					{
						Zone = g.First().Zone,
						ZoneNameByCircles = g.First().ZoneNameByCircles,
						DistrictName = g.First().DistrictName,
						RegionName = g.First().RegionName,
						ZoneDistrict = g.First().ZoneDistrict,
						ZoneDistrictRegion = g.First().ZoneDistrictRegion,
						PropertyType = "Всего",
						FirstTourObjectCount = g.Sum(s => s.FirstTourObjectCount),
						SecondTourObjectCount = g.Sum(s => s.SecondTourObjectCount),
						FirstTourObjectCountWithoutGroupChanging = g.Sum(s => s.FirstTourObjectCountWithoutGroupChanging),
						SecondTourObjectCountWithoutGroupChanging = g.Sum(s => s.SecondTourObjectCountWithoutGroupChanging),
						FirstTourObjectCountWithGroupChanging = g.Sum(s => s.FirstTourObjectCountWithGroupChanging),
						SecondTourObjectCountWithGroupChanging = g.Sum(s => s.SecondTourObjectCountWithGroupChanging),
						FirstTourMinUpks = g.Min(min => min.FirstTourMinUpks),
						FirstTourAverageUpks = g.Sum(sum => sum.FirstTourObjectCount) != 0 ? g.Sum(sum => sum.FirstTourSumUpks) / g.Sum(sum => sum.FirstTourObjectCount) : null,
						FirstTourMaxUpks = g.Max(max => max.FirstTourMaxUpks),
						SecondTourMinUpks = g.Min(min => min.SecondTourMinUpks),
						SecondTourAverageUpks = g.Sum(sum => sum.SecondTourObjectCount) != 0 ? g.Sum(sum => sum.SecondTourSumUpks) / g.Sum(sum => sum.SecondTourObjectCount) : null,
						SecondTourMaxUpks = g.Max(max => max.SecondTourMaxUpks),
						MinUpksVarianceBetweenTours = 
							g.Min(min => min.SecondTourMinUpks).HasValue && 
							g.Min(min => min.SecondTourMinUpks) != 0 ? 
							g.Min(min => min.FirstTourMinUpks) / g.Min(min => min.SecondTourMinUpks) : 
							null,
						MaxUpksVarianceBetweenTours = 
							g.Max(max => max.SecondTourMaxUpks).HasValue && 
							g.Max(max => max.SecondTourMaxUpks) != 0 ? 
							g.Max(max => max.FirstTourMaxUpks) / g.Max(max => max.SecondTourMaxUpks) : 
							null,
						AverageUpksVarianceBetweenTours = 
							g.Sum(sum => sum.FirstTourObjectCount) != 0 && 
							g.Sum(sum => sum.SecondTourObjectCount) != 0 && 
							g.Sum(sum => sum.SecondTourSumUpks) != 0 ? 
							(g.Sum(sum => sum.FirstTourSumUpks) / g.Sum(sum => sum.FirstTourObjectCount)) / 
							(g.Sum(sum => sum.SecondTourSumUpks) / g.Sum(sum => sum.SecondTourObjectCount)) : 
							null//,
						//MinUpksVarianceBetweenToursWithoutGroupChanging = g.Min(min => min.MinUpksVarianceBetweenToursWithoutGroupChanging),
						//AverageUpksVarianceBetweenToursWithoutGroupChanging = g.Average(avg => avg.AverageUpksVarianceBetweenToursWithoutGroupChanging),
						//MaxUpksVarianceBetweenToursWithoutGroupChanging = g.Max(max => max.MaxUpksVarianceBetweenToursWithoutGroupChanging)
					})
					.ToList());
			result = result.OrderBy(x => x.Zone).ThenBy(x => x.DistrictName).ThenBy(x => x.RegionName).ThenBy(x => x.PropertyType != "Всего").ThenBy(x => x.PropertyType).ToList();

			return result;
		}

		private void ThrowNotDataException(long tourId, NumberOfObjectsByZoneAndSubgroupsReportDataType reportDataType, bool isOksReportType)
		{
			var tour = OMTour.Where(x => x.Id == tourId).Select(x => x.Year).ExecuteFirstOrDefault();
			var reportDataTypeName = reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial ? KoNoteType.Initial.GetEnumDescription() : KoNoteType.Day.GetEnumDescription();
			throw new Exception($"Тур {tour?.Year} не содержит задания на оценку типа '{reportDataTypeName}' с единицами оценки {(isOksReportType ? "ОКС" : "ЗУ")}");
		}

		private NumberOfObjectsByZoneAndSubgroupsDto NumberOfObjectsByZoneAndSubgroupsDto(long firstTourId, long secondTourId, List<NumberOfObjectsByZoneAndSubgroupsObjectDto> data, string propertyType)
		{
			var firstTourData = data.Where(x => x.TourId == firstTourId).ToList();
			var firstTourCalcData = firstTourData.Select(x => x.Calc).ToList();
			var secondTourData = data.Where(x => x.TourId == secondTourId).ToList();
			var secondTourCalcData = secondTourData.Select(x => x.Calc).ToList();

			var dto = new NumberOfObjectsByZoneAndSubgroupsDto();
			dto.Zone = data.FirstOrDefault()?.Zone;
			dto.ZoneNameByCircles = data.FirstOrDefault()?.ZoneNameByCircles;
			dto.DistrictName = data.FirstOrDefault()?.DistrictName;
			dto.RegionName = data.FirstOrDefault()?.RegionName;
			dto.ZoneDistrict = data.FirstOrDefault()?.ZoneDistrict;
			dto.ZoneDistrictRegion = data.FirstOrDefault()?.ZoneDistrictRegion;
			dto.PropertyType = propertyType;
			dto.FirstTourObjectCount = firstTourData.Count();
			dto.SecondTourObjectCount = secondTourData.Count();
			dto.FirstTourObjectCountWithoutGroupChanging = firstTourData.Count(x => !x.IsGroupChanged);
			dto.SecondTourObjectCountWithoutGroupChanging = secondTourData.Count(x => !x.IsGroupChanged);
			dto.FirstTourObjectCountWithGroupChanging = firstTourData.Count(x => x.IsGroupChanged);
			dto.SecondTourObjectCountWithGroupChanging = secondTourData.Count(x => x.IsGroupChanged);
			dto.FirstTourMinUpks = _statisticalDataService.GetCalcValue(UpksCalcType.Min, firstTourCalcData);
			dto.FirstTourAverageUpks = _statisticalDataService.GetCalcValue(UpksCalcType.Average, firstTourCalcData);
			dto.FirstTourMaxUpks = _statisticalDataService.GetCalcValue(UpksCalcType.Max, firstTourCalcData);
			dto.SecondTourMinUpks = _statisticalDataService.GetCalcValue(UpksCalcType.Min, secondTourCalcData);
			dto.SecondTourAverageUpks = _statisticalDataService.GetCalcValue(UpksCalcType.Average, secondTourCalcData);
			dto.SecondTourMaxUpks = _statisticalDataService.GetCalcValue(UpksCalcType.Max, secondTourCalcData);

			dto.MinUpksVarianceBetweenTours = dto.SecondTourMinUpks.HasValue && dto.SecondTourMinUpks != 0 ? dto.FirstTourMinUpks / dto.SecondTourMinUpks : null;
			dto.MaxUpksVarianceBetweenTours = dto.SecondTourMaxUpks.HasValue && dto.SecondTourMaxUpks != 0 ? dto.FirstTourMaxUpks / dto.SecondTourMaxUpks : null;
			dto.AverageUpksVarianceBetweenTours = dto.SecondTourAverageUpks.HasValue && dto.SecondTourAverageUpks != 0 ? dto.FirstTourAverageUpks / dto.SecondTourAverageUpks : null;

			var firstTourDataWithoutGroupChanging = firstTourData.Where(x => secondTourData.Any(y => y.CadastralNumber == x.CadastralNumber)).ToList();
			var firstTourCalcDataWithoutGroupChanging = firstTourDataWithoutGroupChanging.Select(x => x.Calc).ToList();
			var secondTourDataWithoutGroupChanging = secondTourData.Where(x => firstTourData.Any(y => y.CadastralNumber == x.CadastralNumber)).ToList();
			var secondTourCalcDataWithoutGroupChanging = secondTourDataWithoutGroupChanging.Select(x => x.Calc).ToList();

			var firstTourMinUpksWithoutGroupChanging = _statisticalDataService.GetCalcValue(UpksCalcType.Min, firstTourCalcDataWithoutGroupChanging);
			var firstTourMaxUpksWithoutGroupChanging = _statisticalDataService.GetCalcValue(UpksCalcType.Max, firstTourCalcDataWithoutGroupChanging);
			var firstTourAverageUpksWithoutGroupChanging = _statisticalDataService.GetCalcValue(UpksCalcType.Average, firstTourCalcDataWithoutGroupChanging);

			var secondTourMinUpksWithoutGroupChanging = _statisticalDataService.GetCalcValue(UpksCalcType.Min, secondTourCalcDataWithoutGroupChanging);
			var secondTourMaxUpksWithoutGroupChanging = _statisticalDataService.GetCalcValue(UpksCalcType.Max, secondTourCalcDataWithoutGroupChanging);
			var secondTourAverageUpksWithoutGroupChanging = _statisticalDataService.GetCalcValue(UpksCalcType.Average, secondTourCalcDataWithoutGroupChanging);

			dto.MinUpksVarianceBetweenToursWithoutGroupChanging = secondTourMinUpksWithoutGroupChanging.HasValue && secondTourMinUpksWithoutGroupChanging != 0 ? firstTourMinUpksWithoutGroupChanging / secondTourMinUpksWithoutGroupChanging : null;
			dto.MaxUpksVarianceBetweenToursWithoutGroupChanging = secondTourMaxUpksWithoutGroupChanging.HasValue && secondTourMaxUpksWithoutGroupChanging != 0 ? firstTourMaxUpksWithoutGroupChanging / secondTourMaxUpksWithoutGroupChanging : null;
			dto.AverageUpksVarianceBetweenToursWithoutGroupChanging = secondTourAverageUpksWithoutGroupChanging.HasValue && secondTourAverageUpksWithoutGroupChanging != 0 ? firstTourAverageUpksWithoutGroupChanging / secondTourAverageUpksWithoutGroupChanging : null;

			return dto;
		}
	}
}
