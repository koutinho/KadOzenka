using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;
using Platform.Register;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class NumberOfObjectsByZoneAndSubgroupsService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly GbuObjectService _gbuObjectService;

		public NumberOfObjectsByZoneAndSubgroupsService(StatisticalDataService statisticalDataService, GbuObjectService gbuObjectService)
		{
			_statisticalDataService = statisticalDataService;
			_gbuObjectService = gbuObjectService;
		}

		public List<NumberOfObjectsByZoneAndSubgroupsDto> GetNumberOfObjectsByZoneAndSubgroupsData(long firstTourId, long secondTourId, NumberOfObjectsByZoneAndSubgroupsReportDataType reportDataType, bool isOksReportType)
		{
			string fileName = string.Empty, contents = string.Empty;

			if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && isOksReportType) fileName = "NumberOfObjectsByZoneAndSubgroupsData_Initial_OKS";
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && !isOksReportType) fileName = "NumberOfObjectsByZoneAndSubgroupsData_Initial_ZU";
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && isOksReportType) fileName = "NumberOfObjectsByZoneAndSubgroupsData_VUON_OKS";
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && !isOksReportType) fileName = "NumberOfObjectsByZoneAndSubgroupsData_VUON_ZU";

			using (var sr = new StreamReader(Core.ConfigParam.Configuration.GetFileStream(fileName, "sql", "SqlQueries"))) contents = sr.ReadToEnd();

			RegisterAttribute firstTourCodeGroupAttr = QSQuery.ExecuteSql<RegisterAttribute>(string.Format("SELECT CORE_REGISTER_ATTRIBUTE.* FROM CORE_REGISTER_ATTRIBUTE, KO_TOUR_ATTRIBUTE_SETTINGS WHERE CORE_REGISTER_ATTRIBUTE.ID=ATTRIBUTE_ID AND TOUR_ID={0} AND ATTRIBUTE_USING_TYPE_CODE=1;", firstTourId)).FirstOrDefault();
			RegisterAttribute secondTourCodeGroupAttr = QSQuery.ExecuteSql<RegisterAttribute>(string.Format("SELECT CORE_REGISTER_ATTRIBUTE.* FROM CORE_REGISTER_ATTRIBUTE, KO_TOUR_ATTRIBUTE_SETTINGS WHERE CORE_REGISTER_ATTRIBUTE.ID=ATTRIBUTE_ID AND TOUR_ID={0} AND ATTRIBUTE_USING_TYPE_CODE=1;", secondTourId)).FirstOrDefault();
			if (firstTourCodeGroupAttr == null) throw new Exception($"Для тура {firstTourId} не заданы настройки '{KoAttributeUsingType.CodeGroupAttribute.GetEnumDescription()}'");
			if (secondTourCodeGroupAttr == null) throw new Exception($"Для тура {secondTourId} не заданы настройки '{KoAttributeUsingType.CodeGroupAttribute.GetEnumDescription()}'");

			long? firstTourTaskId = null, secondTourTaskId = null;

			if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && isOksReportType) firstTourTaskId = QSQuery.ExecuteSql<TourTaskIdsRegisterAttributes>(string.Format("SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE (KO_UNIT.PROPERTY_TYPE_CODE<> 4 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL) AND (KO_UNIT.PROPERTY_TYPE_CODE<> 0 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL)) AND TOUR_ID={0} AND NOTE_TYPE_CODE=4 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;", firstTourId)).FirstOrDefault()?.id;
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && !isOksReportType) firstTourTaskId = QSQuery.ExecuteSql<TourTaskIdsRegisterAttributes>(string.Format("SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE KO_UNIT.PROPERTY_TYPE_CODE=4) AND TOUR_ID={0} AND NOTE_TYPE_CODE=4 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;", firstTourId)).FirstOrDefault()?.id;
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && isOksReportType) firstTourTaskId = QSQuery.ExecuteSql<TourTaskIdsRegisterAttributes>(string.Format("SELECT ID FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE (KO_UNIT.PROPERTY_TYPE_CODE<> 4 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL) AND (KO_UNIT.PROPERTY_TYPE_CODE<> 0 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL)) AND TOUR_ID={0} AND NOTE_TYPE_CODE=1 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;", firstTourId)).FirstOrDefault()?.id;
			else if(reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && !isOksReportType) firstTourTaskId = QSQuery.ExecuteSql<TourTaskIdsRegisterAttributes>(string.Format("SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE KO_UNIT.PROPERTY_TYPE_CODE=4) AND TOUR_ID={0} AND NOTE_TYPE_CODE=1 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;", firstTourId)).FirstOrDefault()?.id;

			if (!firstTourTaskId.HasValue) ThrowNotDataException(firstTourId, reportDataType, isOksReportType);

			if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && isOksReportType) secondTourTaskId = QSQuery.ExecuteSql<TourTaskIdsRegisterAttributes>(string.Format("SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE (KO_UNIT.PROPERTY_TYPE_CODE<> 4 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL) AND (KO_UNIT.PROPERTY_TYPE_CODE<> 0 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL)) AND TOUR_ID={0} AND NOTE_TYPE_CODE=4 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;", secondTourId)).FirstOrDefault()?.id;
			else if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial && !isOksReportType) secondTourTaskId = QSQuery.ExecuteSql<TourTaskIdsRegisterAttributes>(string.Format("SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE KO_UNIT.PROPERTY_TYPE_CODE=4) AND TOUR_ID={0} AND NOTE_TYPE_CODE=4 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;", secondTourId)).FirstOrDefault()?.id;
			else if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && isOksReportType) secondTourTaskId = QSQuery.ExecuteSql<TourTaskIdsRegisterAttributes>(string.Format("SELECT ID FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE (KO_UNIT.PROPERTY_TYPE_CODE<> 4 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL) AND (KO_UNIT.PROPERTY_TYPE_CODE<> 0 OR KO_UNIT.PROPERTY_TYPE_CODE IS NULL)) AND TOUR_ID={0} AND NOTE_TYPE_CODE=1 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;", secondTourId)).FirstOrDefault()?.id;
			else if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnVuon && !isOksReportType) secondTourTaskId = QSQuery.ExecuteSql<TourTaskIdsRegisterAttributes>(string.Format("SELECT * FROM KO_TASK WHERE KO_TASK.ID IN (SELECT KO_UNIT.TASK_ID FROM KO_UNIT WHERE KO_UNIT.PROPERTY_TYPE_CODE=4) AND TOUR_ID={0} AND NOTE_TYPE_CODE=1 ORDER BY KO_TASK.CREATION_DATE DESC LIMIT 1;", secondTourId)).FirstOrDefault()?.id;

			if (!secondTourTaskId.HasValue) ThrowNotDataException(secondTourId, reportDataType, isOksReportType);

			CoreRegisterData firstGbuAttributesRD = QSQuery.ExecuteSql<CoreRegisterData>(string.Format("SELECT CORE_REGISTER.* FROM CORE_REGISTER WHERE REGISTERID={0};", firstTourCodeGroupAttr.RegisterId)).FirstOrDefault();
			CoreRegisterData secondGbuAttributesRD = QSQuery.ExecuteSql<CoreRegisterData>(string.Format("SELECT CORE_REGISTER.* FROM CORE_REGISTER WHERE REGISTERID={0};", secondTourCodeGroupAttr.RegisterId)).FirstOrDefault();

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

			string Query = string.Format(contents, subQueryFirstTour, subQuerySecondTour, firstTourTaskId.GetValueOrDefault(), secondTourTaskId.GetValueOrDefault(), firstTourId, secondTourId);

			List<NumberOfObjectsByZoneAndSubgroupsDto> globalResult = QSQuery.ExecuteSql<NumberOfObjectsByZoneAndSubgroupsDto>(Query);
			List<NumberOfObjectsByZoneAndSubgroupsDto> Result = new List<NumberOfObjectsByZoneAndSubgroupsDto>();
			Result.AddRange(globalResult);

			if (isOksReportType)
				Result
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
						FirstTourAverageUpks = g.Average(avg => avg.FirstTourAverageUpks),
						FirstTourMaxUpks = g.Max(max => max.FirstTourMaxUpks),
						SecondTourMinUpks = g.Min(min => min.SecondTourMinUpks),
						SecondTourAverageUpks = g.Average(avg => avg.SecondTourAverageUpks),
						SecondTourMaxUpks = g.Max(max => max.SecondTourMaxUpks),
						MinUpksVarianceBetweenTours = g.Min(min => min.SecondTourMinUpks).HasValue && g.Min(min => min.SecondTourMinUpks) != 0 ? g.Min(min => min.FirstTourMinUpks) / g.Min(min => min.SecondTourMinUpks) : null,
						MaxUpksVarianceBetweenTours = g.Max(max => max.SecondTourMaxUpks).HasValue && g.Max(max => max.SecondTourMaxUpks) != 0 ? g.Max(max => max.FirstTourMaxUpks) / g.Max(max => max.SecondTourMaxUpks) : null,
						AverageUpksVarianceBetweenTours = g.Average(avg => avg.SecondTourAverageUpks).HasValue && g.Average(avg => avg.SecondTourAverageUpks) != 0 ? g.Average(avg => avg.FirstTourAverageUpks) / g.Average(avg => avg.SecondTourAverageUpks) : null,
						MinUpksVarianceBetweenToursWithoutGroupChanging = g.Min(min => min.MinUpksVarianceBetweenToursWithoutGroupChanging),
						AverageUpksVarianceBetweenToursWithoutGroupChanging = g.Average(avg => avg.AverageUpksVarianceBetweenToursWithoutGroupChanging),
						MaxUpksVarianceBetweenToursWithoutGroupChanging = g.Max(max => max.MaxUpksVarianceBetweenToursWithoutGroupChanging)
					})
					.ToList());
			Result = Result.OrderBy(x => x.Zone).ThenBy(x => x.DistrictName).ThenBy(x => x.RegionName).ThenBy(x => x.PropertyType != "Всего").ThenBy(x => x.PropertyType).ToList();

			return Result;

			var conditions = new List<QSCondition>();

			//TODO: при переделке отчета учесть, что теперь мы исключаем юниты типа "Кадастровый квартал" (PropertyTypes.CadastralQuartal)
			if (isOksReportType)
			{
				conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.NotEqual, (long)PropertyTypes.Stead));
				conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.NotEqual, (long)PropertyTypes.None));
			}
			else conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.Equal, (long)PropertyTypes.Stead));

			conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.TaskId), QSConditionType.In, new List<double> { firstTourTaskId.GetValueOrDefault(), secondTourTaskId.GetValueOrDefault() }));


			/*TODO: при переделке отчета учесть, что теперь кадастровый квартал сначала юнита ищется в гбу части по атрибуту "Кадастровый квартал итоговый"
			//TODO: если он отсутствует, то тогда уже берется поле юнита CADASTRAL_BLOCK
			//TODO: атрибут "Кадастровый квартал итоговый" получать через сервис GbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id
			//TODO: пример можно посмотреть в NumberOfObjectsByAdministrativeDistrictsByGroups_Districts.sql
			*/
			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = conditions
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMQuartalDictionary.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.CadastralBlock),
							RightOperand = OMQuartalDictionary.GetColumn(x => x.CadastralQuartal)
						},
						JoinType = QSJoinType.Inner
					}
				}
			};

			query.AddColumn(OMUnit.GetColumn(x => x.TourId, "TourId"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, "ObjectId"));
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.Zone, "Zone"));
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.ZoneNameByCircles, "ZoneNameByCircles"));
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.District_Code, "District_Code"));
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.Region_Code, "Region_Code"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));
            query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "ObjectCost"));
            query.AddColumn(OMUnit.GetColumn(x => x.Square, "ObjectSquare"));

			Console.WriteLine(query.GetSql());

			var table = query.ExecuteQuery();

			var data = new List<NumberOfObjectsByZoneAndSubgroupsObjectDto>();
			if (table.Rows.Count != 0)
			{
				var firstObjectIds = new List<long>();
				var secondObjectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					if (table.Rows[i]["TourId"].ParseToLong() == firstTourId) firstObjectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
					else secondObjectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
				}

				var firstGbuAttributes = _gbuObjectService.GetAllAttributes(firstObjectIds, new List<long> { firstTourCodeGroupAttr.RegisterId }, new List<long> { firstTourCodeGroupAttr.Id });
				var secondGbuAttributes = _gbuObjectService.GetAllAttributes(secondObjectIds, new List<long> { secondTourCodeGroupAttr.RegisterId }, new List<long> { secondTourCodeGroupAttr.Id });

				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new NumberOfObjectsByZoneAndSubgroupsObjectDto
					{
						TourId = table.Rows[i]["TourId"].ParseToLong(),
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						ObjectId = table.Rows[i]["ObjectId"].ParseToLong(),
						Zone = table.Rows[i]["Zone"].ParseToLong(),
						ZoneNameByCircles = table.Rows[i]["ZoneNameByCircles"].ParseToString(),
						DistrictName = ((Hunteds)table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle(),
						RegionName = ((Districts)table.Rows[i]["Region_Code"].ParseToLong()).GetEnumDescription(),
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						Calc = new CalcDto
                        {
                            ObjectValue = table.Rows[i]["ObjectUpks"].ParseToDecimalNullable(),
                            ObjectCost = table.Rows[i]["ObjectCost"].ParseToDecimalNullable(),
                            ObjectSquare = table.Rows[i]["ObjectSquare"].ParseToDecimalNullable(),
						}
					};
					dto.ZoneDistrict = $"Зона {dto.Zone}_{dto.DistrictName}";
					dto.ZoneDistrictRegion = $"{dto.ZoneDistrict}_{dto.RegionName}";

					if (dto.TourId == firstTourId) dto.IsGroupChanged = firstGbuAttributes.Count(x => x.ObjectId == dto.ObjectId) > 1;
					else dto.IsGroupChanged = secondGbuAttributes.Count(x => x.ObjectId == dto.ObjectId) > 1;

					data.Add(dto);
				}
			}

			var dataGrouped = data.GroupBy(x => new { x.Zone, x.DistrictName, x.RegionName }).OrderBy(x => x.Key.Zone).ThenBy(x => x.Key.DistrictName).ThenBy(x => x.Key.RegionName);

			var result = new List<NumberOfObjectsByZoneAndSubgroupsDto>();
			foreach (var dataGroup in dataGrouped)
			{
				if (isOksReportType)
				{
					var values = dataGroup.ToList();
					var totalDto = NumberOfObjectsByZoneAndSubgroupsDto(firstTourId, secondTourId, dataGroup.ToList(), "Всего");
					result.Add(totalDto);

					var dataGroupedByPropertyType = values.GroupBy(x => x.PropertyType);
					foreach (var dataByPropertyType in dataGroupedByPropertyType)
					{
						var dto = NumberOfObjectsByZoneAndSubgroupsDto(firstTourId, secondTourId, dataByPropertyType.ToList(), dataByPropertyType.Key);
						result.Add(dto);
					}
				}
				else
				{
					var dto = NumberOfObjectsByZoneAndSubgroupsDto(firstTourId, secondTourId, dataGroup.ToList(), PropertyTypes.Stead.GetEnumDescription());
					result.Add(dto);
				}
			}
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
