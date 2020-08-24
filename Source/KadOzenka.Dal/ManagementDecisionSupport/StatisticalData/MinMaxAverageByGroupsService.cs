using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData.Dto.MinMaxAverage;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;

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

        public List<MinMaxAverageByGroupsZuDto> GetDataByGroupsUpksZu(long[] taskIdList)
        {
            string contents;
            using (var sr = new StreamReader(Core.ConfigParam.Configuration.GetFileStream("MinMaxAverageUPKSByGroupsZu", "sql", "SqlQueries")))
            {
                contents = sr.ReadToEnd();
            }

            var result = QSQuery.ExecuteSql<MinMaxAverageByGroupsZuDto>(string.Format(contents, string.Join(", ", taskIdList)));

            var summary = new MinMaxAverageByGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                ObjectUpksMin = result.Min(x => x.ObjectUpksMin),
                ObjectUpksMax = result.Max(x => x.ObjectUpksMax),
                ObjectUpksAvg = result.Average(x => x.ObjectUpksAvg),
                ObjectUpksAvgWeight = result.Average(x => x.ObjectUpksAvgWeight)
            };
            result.Add(summary);

            return result;
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
					DateTime.Now.GetEndOfTheDay(), isLight: true);

				for (var i = 0; i < table.Rows.Count; i++)
				{
					var group = table.Rows[i]["ParentGroup"].ParseToStringNullable();

					var dto = new MinMaxAverageByGroupsObjectDto
					{
						PropertyTypeCode = (PropertyTypes)table.Rows[i]["PropertyType"].ParseToLong(),
						GroupName = string.IsNullOrEmpty(group) ? "Без группы" : group,
						HasGroup = !string.IsNullOrEmpty(group),
						GbuObjectId = table.Rows[i]["ObjectId"].ParseToLongNullable(),
                        CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToStringNullable()
					};

					FillPurposeData(dto, gbuAttributes, buildingPurposeAttr, placementPurposeAttr);

					if (!dto.HasPurpose || dto.HasPurpose && dto.Purpose != null)
					{
						FillingCalcData(calcType, dto, table, i);
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

        public List<MinMaxAverageByGroupsAndSubGroupsZuDto> GetDataByGroupsAndSubgroupsUpksZu(long[] taskIdList)
        {
            string contents;
            using (var sr = new StreamReader(Core.ConfigParam.Configuration.GetFileStream("MinMaxAverageUPKSByGroupsAndSubGroupsZu", "sql", "SqlQueries")))
            {
                contents = sr.ReadToEnd();
            }

            var result = QSQuery.ExecuteSql<MinMaxAverageByGroupsAndSubGroupsZuDto>(string.Format(contents, string.Join(", ", taskIdList)));

            var summary = new MinMaxAverageByGroupsAndSubGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                SubGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                ObjectUpksMin = result.Min(x => x.ObjectUpksMin),
                ObjectUpksMax = result.Max(x => x.ObjectUpksMax),
                ObjectUpksAvg = result.Average(x => x.ObjectUpksAvg),
                ObjectUpksAvgWeight = result.Average(x => x.ObjectUpksAvgWeight)
            };
            result.Add(summary);

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
                        CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToStringNullable()
					};

					FillPurposeData(dto, gbuAttributes, buildingPurposeAttr, placementPurposeAttr);

					if (!dto.HasPurpose || dto.HasPurpose && dto.Purpose != null)
					{
						FillingCalcData(calcType, dto, table, i);
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

        #region Support Methods

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
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));

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
                FillUpks(dto, table, tableCurrentRowIndex);
            }
			else if (calcType == MinMaxAverageByGroupsCalcType.Uprs)
            {
                FillUprs(dto, table, tableCurrentRowIndex);
            }
			else
			{
                FillUpks(dto, table, tableCurrentRowIndex);
                FillUprs(dto, table, tableCurrentRowIndex);
            }
		}

        private static void FillUpks(MinMaxAverageByGroupsObjectDto dto, DataTable table, int tableCurrentRowIndex)
        {
            dto.UpksObjectValue = table.Rows[tableCurrentRowIndex]["ObjectUpks"].ParseToDecimalNullable();
            dto.UpksObjectCost = table.Rows[tableCurrentRowIndex]["ObjectCost"].ParseToDecimalNullable();
            dto.UpksObjectSquare = table.Rows[tableCurrentRowIndex]["ObjectSquare"].ParseToDecimalNullable();
        }

        private void FillUprs(MinMaxAverageByGroupsObjectDto dto, DataTable table, int tableCurrentRowIndex)
        {
            var marketObject = GetMarketObject(dto.CadastralNumber);
            if (marketObject == null)
                return;

            dto.UprsObjectSquare = table.Rows[tableCurrentRowIndex]["ObjectSquare"].ParseToDecimalNullable();
            dto.UprsObjectValue = dto.UprsObjectSquare.GetValueOrDefault() != 0
                ? marketObject.Price / dto.UprsObjectSquare
                : null;
            dto.UprsObjectCost = marketObject.Price;
        }

        private OMCoreObject GetMarketObject(string cadastralNumber)
		{
            var marketObjects = OMCoreObject.Where(x =>
					x.CadastralNumber == cadastralNumber && 
					x.ProcessType_Code == ProcessStep.Dealed 
					&& (x.DealType_Code == DealType.SaleDeal || x.DealType_Code == DealType.SaleSuggestion))
				.Select(x => x.Market_Code)
				.Select(x => x.LastDateUpdate)
				.Select(x => x.ParserTime)
				.Select(x => x.Price)
				.Execute();

            OMCoreObject marketObject;
            if (marketObjects.Count > 1)
			{
				marketObject = marketObjects.Any(x => x.Market_Code == MarketTypes.Rosreestr)
					? marketObjects.Where(x => x.Market_Code == MarketTypes.Rosreestr).OrderByDescending(x => x.LastDateUpdate ?? x.ParserTime).First()
					: marketObjects.OrderByDescending(x => x.LastDateUpdate ?? x.ParserTime).First();
			}
			else
			{
				marketObject = marketObjects.FirstOrDefault();
			}

			return marketObject;
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
                                ? _statisticalDataService.GetCalcValue(upksCalcType, x.ToList().Select(y => new CalcDto
                                {
                                    ObjectValue = y.UpksObjectValue,
                                    ObjectCost = y.UpksObjectCost,
                                    ObjectSquare = y.UpksObjectSquare
                                }).ToList()) 
								: null,
							UprsCalcValue = calcType == MinMaxAverageByGroupsCalcType.Uprs || calcType == MinMaxAverageByGroupsCalcType.UpksAndUprs
                                ? _statisticalDataService.GetCalcValue(upksCalcType, x.ToList().Select(y => new CalcDto
                                {
                                    ObjectValue = y.UprsObjectValue,
                                    ObjectCost = y.UprsObjectCost,
                                    ObjectSquare = y.UprsObjectSquare
                                }).ToList()) 
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
								? _statisticalDataService.GetCalcValue(upksCalcType, x.ToList().Select(y => new CalcDto
                                {
                                    ObjectValue = y.UpksObjectValue,
                                    ObjectCost = y.UpksObjectCost,
                                    ObjectSquare = y.UpksObjectSquare
                                }).ToList()) 
								: null,
							UprsCalcValue = calcType == MinMaxAverageByGroupsCalcType.Uprs || calcType == MinMaxAverageByGroupsCalcType.UpksAndUprs
                                ? _statisticalDataService.GetCalcValue(upksCalcType, x.ToList().Select(y => new CalcDto
                                {
                                    ObjectValue = y.UprsObjectValue,
                                    ObjectCost = y.UprsObjectCost,
                                    ObjectSquare = y.UprsObjectSquare
                                }).ToList()) 
								: null,
						};
						result.Add(dto);
					}
				});
		}

        #endregion
    }
}
