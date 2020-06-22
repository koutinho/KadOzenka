using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public enum MinMaxAverageByGroupsCalcType
	{
		Upks,
		Uprs,
		UpksAndUprs
	}

	public class MinMaxAverageByGroupsService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly GbuObjectService _gbuObjectService;

		public MinMaxAverageByGroupsService(StatisticalDataService statisticalDataService, GbuObjectService gbuObjectService)
		{
			_statisticalDataService = statisticalDataService;
			_gbuObjectService = gbuObjectService;
		}

		public List<MinMaxAverageByGroupsDataDto> GetDataByGroups(long[] taskIdList, bool isOks, MinMaxAverageByGroupsCalcType calcType)
		{
			var table = GetData(taskIdList, isOks);

			var buildingPurposeAttr = _statisticalDataService.GetRosreestrBuildingPurposeAttribute();
			var placementPurposeAttr = _statisticalDataService.GetRosreestrPlacementPurposeAttribute();
			var data = new List<MinMaxAverageByGroupsObjectDto>();
			if (table.Rows.Count != 0)
			{
				var objectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					objectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
				}

				var gbuAttributes = _gbuObjectService.GetAllAttributes(objectIds,
					new List<long> { buildingPurposeAttr.RegisterId, placementPurposeAttr.RegisterId },
					new List<long> { buildingPurposeAttr.Id, placementPurposeAttr.Id },
					DateTime.Now.GetEndOfTheDay());

				for (var i = 0; i < table.Rows.Count; i++)
				{
					var group = table.Rows[i]["ParentGroup"].ParseToStringNullable();

					var dto = new MinMaxAverageByGroupsObjectDto
					{
						PropertyTypeCode = (PropertyTypes)table.Rows[i]["PropertyType"].ParseToLong(),
						GroupName = string.IsNullOrEmpty(group) ? "Без группы" : group,
						HasGroup = !string.IsNullOrEmpty(group),
						GbuObjectId = table.Rows[i]["ObjectId"].ParseToLongNullable(),
						UpksCalcDto = new CalcDto(),
						UprsCalcDto = new CalcDto()
					};

					FillingCalcData(calcType, dto, table, i);
					FillPurposeData(dto, gbuAttributes, buildingPurposeAttr, placementPurposeAttr);

					if (!dto.HasPurpose || dto.HasPurpose && dto.Purpose != null)
					{
						data.Add(dto);
					}
				}
			}

			var result = new List<MinMaxAverageByGroupsDataDto>();

			var dataByGroupedByGroups = data.GroupBy(x => new { x.GroupName, x.HasGroup });
			foreach (var nameGroup in dataByGroupedByGroups)
			{
				var nameGroupValues = nameGroup.ToList();
				FillDataForGroup(nameGroupValues, nameGroupValues.FirstOrDefault()?.GroupName, result, calcType);
			}
			FillDataForGroup(data, "Итого по субъекту РФ г Москва", result, calcType);

			return result;
		}

		public List<MinMaxAverageByGroupsAndSubgroupsDataDto> GetDataByGroupsAndSubgroups(long[] taskIdList, bool isOks, MinMaxAverageByGroupsCalcType calcType)
		{
			var table = GetData(taskIdList, isOks);

			var buildingPurposeAttr = _statisticalDataService.GetRosreestrBuildingPurposeAttribute();
			var placementPurposeAttr = _statisticalDataService.GetRosreestrPlacementPurposeAttribute();
			var data = new List<MinMaxAverageByGroupsAndSubgroupsObjectDto>();
			if (table.Rows.Count != 0)
			{
				var objectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					objectIds.Add(table.Rows[i]["ObjectId"].ParseToLong());
				}

				var gbuAttributes = _gbuObjectService.GetAllAttributes(objectIds,
					new List<long> { buildingPurposeAttr.RegisterId, placementPurposeAttr.RegisterId },
					new List<long> { buildingPurposeAttr.Id, placementPurposeAttr.Id },
					DateTime.Now.GetEndOfTheDay());

				for (var i = 0; i < table.Rows.Count; i++)
				{
					var group = table.Rows[i]["ParentGroup"].ParseToStringNullable();
					var subgroup = table.Rows[i]["Group"].ParseToStringNullable();

					var dto = new MinMaxAverageByGroupsAndSubgroupsObjectDto
					{
						PropertyTypeCode = (PropertyTypes)table.Rows[i]["PropertyType"].ParseToLong(),
						GroupName = string.IsNullOrEmpty(group) ? "Без группы" : group,
						HasGroup = !string.IsNullOrEmpty(group),
						SubgroupName = string.IsNullOrEmpty(subgroup) ? "Без группы" : subgroup,
						HasSubgroup = !string.IsNullOrEmpty(subgroup),
						GbuObjectId = table.Rows[i]["ObjectId"].ParseToLongNullable(),
						UpksCalcDto = new CalcDto(),
						UprsCalcDto = new CalcDto()
					};

					FillingCalcData(calcType, dto, table, i);
					FillPurposeData(dto, gbuAttributes, buildingPurposeAttr, placementPurposeAttr);

					if (!dto.HasPurpose || dto.HasPurpose && dto.Purpose != null)
					{
						data.Add(dto);
					}
				}
			}

			var result = new List<MinMaxAverageByGroupsAndSubgroupsDataDto>();

			var dataByGroupedByGroups = data.GroupBy(x => new { x.GroupName, x.HasGroup, x.SubgroupName, x.HasSubgroup });
			foreach (var nameGroup in dataByGroupedByGroups)
			{
				var nameGroupValues = nameGroup.ToList();
				FillDataForGroupAndSubgroup(nameGroupValues, nameGroupValues.FirstOrDefault()?.GroupName,
					nameGroupValues.FirstOrDefault()?.SubgroupName, result, calcType);
			}
			FillDataForGroupAndSubgroup(data, "Итого по субъекту РФ г Москва",
				"Итого по субъекту РФ г Москва", result, calcType);

			return result;
		}

		private DataTable GetData(long[] taskIdList, bool isOks)
		{
			var conditions = new List<QSCondition>();
			if (isOks)
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

			var groupJoin = new QSJoin
			{
				RegisterId = OMGroup.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.GroupId),
					RightOperand = OMGroup.GetColumn(x => x.Id)
				},
				JoinType = QSJoinType.Left
			};

			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, conditions,
				new List<QSJoin>() { groupJoin });

			query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "Group"));
			query.AddColumn(OMUnit.GetColumn(x => x.ObjectId, "ObjectId"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType_Code, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));
            query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "ObjectCost"));
            query.AddColumn(OMUnit.GetColumn(x => x.Square, "ObjectSquare"));

			var subQuery = new QSQuery(OMGroup.GetRegisterId())
			{
				Columns = new List<QSColumn>
				{
					OMGroup.GetColumn(x => x.GroupName)
				},
				Condition = new QSConditionGroup(QSConditionGroupType.And)
				{
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(
							OMGroup.GetColumn(x => x.Id),
							QSConditionType.Equal,
							OMGroup.GetColumn(x => x.ParentId))
						{
							RightOperandLevel = 1
						}
					}
				}
			};
			query.AddColumn(subQuery, "ParentGroup");

			var table = query.ExecuteQuery();
			return table;
		}

		private void FillingCalcData(MinMaxAverageByGroupsCalcType calcType, MinMaxAverageByGroupsObjectDto dto,
			DataTable table, int tableCurrentRowIndex)
		{
			if (calcType == MinMaxAverageByGroupsCalcType.Upks)
			{
				dto.UpksCalcDto.ObjectValue = table.Rows[tableCurrentRowIndex]["ObjectUpks"].ParseToDecimalNullable();
				dto.UpksCalcDto.ObjectCost = table.Rows[tableCurrentRowIndex]["ObjectCost"].ParseToDecimalNullable();
				dto.UpksCalcDto.ObjectSquare = table.Rows[tableCurrentRowIndex]["ObjectSquare"].ParseToDecimalNullable();
            }
			else if (calcType == MinMaxAverageByGroupsCalcType.Uprs)
			{
				//TODO: How to calculate uprs?
				dto.UprsCalcDto.ObjectValue = table.Rows[tableCurrentRowIndex]["ObjectUpks"].ParseToDecimalNullable();
                dto.UprsCalcDto.ObjectCost = table.Rows[tableCurrentRowIndex]["ObjectCost"].ParseToDecimalNullable();
                dto.UprsCalcDto.ObjectSquare = table.Rows[tableCurrentRowIndex]["ObjectSquare"].ParseToDecimalNullable();
			}
			else
			{
				dto.UpksCalcDto.ObjectValue = table.Rows[tableCurrentRowIndex]["ObjectUpks"].ParseToDecimalNullable();
                dto.UpksCalcDto.ObjectCost = table.Rows[tableCurrentRowIndex]["ObjectCost"].ParseToDecimalNullable();
                dto.UpksCalcDto.ObjectSquare = table.Rows[tableCurrentRowIndex]["ObjectSquare"].ParseToDecimalNullable();

				//TODO: How to calculate uprs?
				dto.UprsCalcDto.ObjectValue = table.Rows[tableCurrentRowIndex]["ObjectUpks"].ParseToDecimalNullable();
                dto.UprsCalcDto.ObjectCost = table.Rows[tableCurrentRowIndex]["ObjectCost"].ParseToDecimalNullable();
                dto.UprsCalcDto.ObjectSquare = table.Rows[tableCurrentRowIndex]["ObjectSquare"].ParseToDecimalNullable();
			}
		}

		private void FillPurposeData(MinMaxAverageByGroupsObjectDto dto, List<GbuObjectAttribute> gbuAttributes,
			RegisterAttribute buildingPurposeAttr, RegisterAttribute placementPurposeAttr)
		{
			if (dto.PropertyTypeCode == PropertyTypes.Building)
			{
				dto.HasPurpose = true;
				var purpose = gbuAttributes
					.FirstOrDefault(x => x.ObjectId == dto.GbuObjectId && x.AttributeId == buildingPurposeAttr.Id);
				if (purpose != null)
				{
					dto.Purpose = purpose.GetValueInString();
				}
			}
			else if (dto.PropertyTypeCode == PropertyTypes.Pllacement)
			{
				dto.HasPurpose = true;
				var purpose = gbuAttributes
					.FirstOrDefault(x => x.ObjectId == dto.GbuObjectId && x.AttributeId == placementPurposeAttr.Id);
				if (purpose != null)
				{
					dto.Purpose = purpose.GetValueInString();
				}
			}
		}

		private void FillDataForGroup(List<MinMaxAverageByGroupsObjectDto> groupObjectsList,
			string groupName, List<MinMaxAverageByGroupsDataDto> result, MinMaxAverageByGroupsCalcType calcType)
		{
			var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>();
			groupObjectsList.GroupBy(x => new { x.PropertyTypeCode, x.Purpose, x.HasPurpose })
				.ForEach(x =>
				{
					foreach (var upksCalcType in upksCalcTypes)
					{
						var dto = new MinMaxAverageByGroupsDataDto
						{
							GroupName = groupName,
							ObjectsCount = groupObjectsList.Count,
							PropertyType = x.Key.PropertyTypeCode.GetEnumDescription(),
							Purpose = x.Key.Purpose,
							HasPurpose = x.Key.HasPurpose,
							CalcType = upksCalcType,
							UpksCalcValue = calcType == MinMaxAverageByGroupsCalcType.Upks || calcType == MinMaxAverageByGroupsCalcType.UpksAndUprs 
								? _statisticalDataService.GetCalcValue(upksCalcType, x.ToList().Select(y => y.UpksCalcDto).ToList()) 
								: null,
							UprsCalcValue = calcType == MinMaxAverageByGroupsCalcType.Uprs || calcType == MinMaxAverageByGroupsCalcType.UpksAndUprs 
								? _statisticalDataService.GetCalcValue(upksCalcType, x.ToList().Select(y => y.UprsCalcDto).ToList()) 
								: null,
						};
						result.Add(dto);
					}
				});
		}

		private void FillDataForGroupAndSubgroup(List<MinMaxAverageByGroupsAndSubgroupsObjectDto> groupObjectsList,
			string groupName, string subgroupName, List<MinMaxAverageByGroupsAndSubgroupsDataDto> result, MinMaxAverageByGroupsCalcType calcType)
		{
			var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>();
			groupObjectsList.GroupBy(x => new { x.PropertyTypeCode, x.Purpose, x.HasPurpose })
				.ForEach(x =>
				{
					foreach (var upksCalcType in upksCalcTypes)
					{
						var dto = new MinMaxAverageByGroupsAndSubgroupsDataDto
						{
							GroupName = groupName,
							SubgroupName = subgroupName,
							ObjectsCount = groupObjectsList.Count,
							PropertyType = x.Key.PropertyTypeCode.GetEnumDescription(),
							Purpose = x.Key.Purpose,
							HasPurpose = x.Key.HasPurpose,
							CalcType = upksCalcType,
							UpksCalcValue = calcType == MinMaxAverageByGroupsCalcType.Upks || calcType == MinMaxAverageByGroupsCalcType.UpksAndUprs 
								? _statisticalDataService.GetCalcValue(upksCalcType, x.ToList().Select(y => y.UpksCalcDto).ToList()) 
								: null,
							UprsCalcValue = calcType == MinMaxAverageByGroupsCalcType.Uprs || calcType == MinMaxAverageByGroupsCalcType.UpksAndUprs 
								? _statisticalDataService.GetCalcValue(upksCalcType, x.ToList().Select(y => y.UprsCalcDto).ToList()) 
								: null,
						};
						result.Add(dto);
					}
				});
		}
	}
}
