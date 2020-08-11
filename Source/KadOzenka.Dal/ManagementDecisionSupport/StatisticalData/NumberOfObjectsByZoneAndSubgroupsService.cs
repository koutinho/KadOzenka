using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;

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
			var firstTourCodeGroupAttr = _statisticalDataService.GetGroupAttributeFromTourSettings(firstTourId);
			if (firstTourCodeGroupAttr == null)
			{
				throw new Exception($"Для тура {firstTourId} не заданы настройки '{KoAttributeUsingType.CodeGroupAttribute.GetEnumDescription()}'");
			}
			var secondTourCodeGroupAttr = _statisticalDataService.GetGroupAttributeFromTourSettings(secondTourId);
			if (secondTourCodeGroupAttr == null)
			{
				throw new Exception($"Для тура {secondTourId} не заданы настройки '{KoAttributeUsingType.CodeGroupAttribute.GetEnumDescription()}'");
			}

			var conditions = new List<QSCondition>();
			if (isOksReportType)
			{
				conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
					QSConditionType.NotEqual, (long)PropertyTypes.Stead));
				conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
					QSConditionType.NotEqual, (long)PropertyTypes.None));
			}
			else
			{
				conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
					QSConditionType.Equal, (long)PropertyTypes.Stead));
			}

			long? firstTourTaskId, secondTourTaskId;
			if (reportDataType == NumberOfObjectsByZoneAndSubgroupsReportDataType.BasedOnInitial)
			{
				firstTourTaskId = GetTourTaskId(firstTourId, KoNoteType.Initial, isOksReportType);
				secondTourTaskId = GetTourTaskId(secondTourId, KoNoteType.Initial, isOksReportType);
			}
			else
			{
				firstTourTaskId = GetTourTaskId(firstTourId, KoNoteType.Day, isOksReportType);
				secondTourTaskId = GetTourTaskId(secondTourId, KoNoteType.Day, isOksReportType);
			}

			conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.TaskId), QSConditionType.In, new List<double> { firstTourTaskId.GetValueOrDefault(), secondTourTaskId.GetValueOrDefault() }));

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

			var table = query.ExecuteQuery();

			var data = new List<NumberOfObjectsByZoneAndSubgroupsObjectDto>();
			if (table.Rows.Count != 0)
			{
				var firstObjectIds = new List<long>();
				var secondObjectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					if (table.Rows[i]["TourId"].ParseToLong() == firstTourId)
					{
						firstObjectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
					}
					else
					{
						secondObjectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
					}

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

					if (dto.TourId == firstTourId)
					{
						dto.IsGroupChanged = firstGbuAttributes.Count(x => x.ObjectId == dto.ObjectId) > 1;
					}
					else
					{
						dto.IsGroupChanged = secondGbuAttributes.Count(x => x.ObjectId == dto.ObjectId) > 1;
					}
					data.Add(dto);
				}


			}

			var dataGrouped = data.GroupBy(x => new { x.Zone, x.DistrictName, x.RegionName })
				.OrderBy(x => x.Key.Zone)
				.ThenBy(x => x.Key.DistrictName)
				.ThenBy(x => x.Key.RegionName);

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

		private long? GetTourTaskId(long tourId, KoNoteType koNoteType, bool isOks)
		{
			long? taskId = null;
			var tasks = OMTask.Where(x => x.TourId == tourId
										  && x.NoteType_Code == koNoteType)
				.Select(x => x.CreationDate)
				.Execute().OrderByDescending(x => x.CreationDate).ToList();
			foreach (var task in tasks)
			{
				if (isOks)
				{
					if (OMUnit.Where(x =>
							x.TaskId == task.Id && x.PropertyType_Code != PropertyTypes.Stead &&
							x.PropertyType_Code != PropertyTypes.None)
						.ExecuteExists())
					{
						taskId = task.Id;
						break;
					}
				}
				else
				{
					if (OMUnit.Where(x =>
							x.TaskId == task.Id && x.PropertyType_Code == PropertyTypes.Stead)
						.ExecuteExists())
					{
						taskId = task.Id;
						break;
					}
				}
			}

			return taskId;
		}

		private NumberOfObjectsByZoneAndSubgroupsDto NumberOfObjectsByZoneAndSubgroupsDto(long firstTourId, long secondTourId,
			List<NumberOfObjectsByZoneAndSubgroupsObjectDto> data, string propertyType)
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

			dto.MinUpksVarianceBetweenTours = dto.SecondTourMinUpks.HasValue && dto.SecondTourMinUpks != 0
				? dto.FirstTourMinUpks / dto.SecondTourMinUpks
				: null;
			dto.MaxUpksVarianceBetweenTours = dto.SecondTourMaxUpks.HasValue && dto.SecondTourMaxUpks != 0
				? dto.FirstTourMaxUpks / dto.SecondTourMaxUpks
				: null;
			dto.AverageUpksVarianceBetweenTours = dto.SecondTourAverageUpks.HasValue && dto.SecondTourAverageUpks != 0
				? dto.FirstTourAverageUpks / dto.SecondTourAverageUpks
				: null;

			var firstTourDataWithoutGroupChanging = firstTourData
				.Where(x => secondTourData.Any(y => y.CadastralNumber == x.CadastralNumber)).ToList();
			var firstTourCalcDataWithoutGroupChanging = firstTourDataWithoutGroupChanging.Select(x => x.Calc).ToList();
			var secondTourDataWithoutGroupChanging = secondTourData
				.Where(x => firstTourData.Any(y => y.CadastralNumber == x.CadastralNumber)).ToList();
			var secondTourCalcDataWithoutGroupChanging = secondTourDataWithoutGroupChanging.Select(x => x.Calc).ToList();

			var firstTourMinUpksWithoutGroupChanging =
				_statisticalDataService.GetCalcValue(UpksCalcType.Min, firstTourCalcDataWithoutGroupChanging);
			var firstTourMaxUpksWithoutGroupChanging =
				_statisticalDataService.GetCalcValue(UpksCalcType.Max, firstTourCalcDataWithoutGroupChanging);
			var firstTourAverageUpksWithoutGroupChanging =
				_statisticalDataService.GetCalcValue(UpksCalcType.Average, firstTourCalcDataWithoutGroupChanging);

			var secondTourMinUpksWithoutGroupChanging =
				_statisticalDataService.GetCalcValue(UpksCalcType.Min, secondTourCalcDataWithoutGroupChanging);
			var secondTourMaxUpksWithoutGroupChanging =
				_statisticalDataService.GetCalcValue(UpksCalcType.Max, secondTourCalcDataWithoutGroupChanging);
			var secondTourAverageUpksWithoutGroupChanging =
				_statisticalDataService.GetCalcValue(UpksCalcType.Average, secondTourCalcDataWithoutGroupChanging);

			dto.MinUpksVarianceBetweenToursWithoutGroupChanging =
				secondTourMinUpksWithoutGroupChanging.HasValue && secondTourMinUpksWithoutGroupChanging != 0
					? firstTourMinUpksWithoutGroupChanging / secondTourMinUpksWithoutGroupChanging
					: null;
			dto.MaxUpksVarianceBetweenToursWithoutGroupChanging =
				secondTourMaxUpksWithoutGroupChanging.HasValue && secondTourMaxUpksWithoutGroupChanging != 0
					? firstTourMaxUpksWithoutGroupChanging / secondTourMaxUpksWithoutGroupChanging
					: null;
			dto.AverageUpksVarianceBetweenToursWithoutGroupChanging =
				secondTourAverageUpksWithoutGroupChanging.HasValue && secondTourAverageUpksWithoutGroupChanging != 0
					? firstTourAverageUpksWithoutGroupChanging / secondTourAverageUpksWithoutGroupChanging
					: null;

			return dto;
		}
	}
}
