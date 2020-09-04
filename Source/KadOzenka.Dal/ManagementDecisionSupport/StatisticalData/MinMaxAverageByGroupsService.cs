﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Core.ConfigParam;
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


        #region UPRS
        //TODO возможно, вынести в отдельный сервис

        public List<UprsByGroupsZuDto> GetDataByGroupsUprsZu(long[] taskIdList)
        {
            var sql = GetSqlForUprs(taskIdList, false);
            var result = QSQuery.ExecuteSql<UprsByGroupsZuDto>(sql);

            var summary = new UprsByGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Min = result.Min(x => x.Min),
                Max = result.Max(x => x.Max),
                Avg = result.Average(x => x.Avg),
                AvgWeight = result.Average(x => x.AvgWeight)
            };
            result.Add(summary);

            return result;
        }

        public List<UprsByGroupsAndSubGroupsZuDto> GetDataByGroupsAndSubgroupsUprsZu(long[] taskIdList)
        {
            var sql = GetSqlForUprs(taskIdList, true);

            var result = QSQuery.ExecuteSql<UprsByGroupsAndSubGroupsZuDto>(sql);

            var summary = new UprsByGroupsAndSubGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                SubGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Min = result.Min(x => x.Min),
                Max = result.Max(x => x.Max),
                Avg = result.Average(x => x.Avg),
                AvgWeight = result.Average(x => x.AvgWeight)
            };
            result.Add(summary);

            return result;
        }

        private string GetSqlForUprs(long[] taskIdList, bool withSubGroups)
        {
            string contents;
            using (var sr = new StreamReader(Configuration.GetFileStream("MinMaxAverageUPRSZu", "sql", "SqlQueries")))
            {
                contents = sr.ReadToEnd();
            }

            var subGroupSelectionFromGrouping = string.Empty;
            var subGroupSelectionFromQuery = string.Empty;
            var subGroupForGrouping = string.Empty;
            if (withSubGroups)
            {
                subGroupSelectionFromGrouping = @"temp.""SubGroup"",";
                subGroupSelectionFromQuery = @"L1_R205.GROUP_NAME as ""SubGroup"",";
                subGroupForGrouping = @", temp.""SubGroup""";
            }

            return string.Format(contents, subGroupSelectionFromGrouping, subGroupSelectionFromQuery, string.Join(",", taskIdList), subGroupForGrouping);
        }

        #endregion

        #region UPKS

        private class GroupingOks
        {
            public string PropertyType { get; set; }
            public bool HasPurpose { get; set; }
            public string Purpose { get; set; }
        }

        private class GroupingOksDictionary
        {
            public GroupingOks Key { get; set; }
            public List<UpksByGroupsOksDto> Values { get; set; }
        }

        public List<UpksByGroupsZuDto> GetDataByGroupsUpksZu(long[] taskIdList)
        {
            var sql = GetSqlForUpks(taskIdList, false);
            var result = QSQuery.ExecuteSql<UpksByGroupsZuDto>(sql);

            var summary = new UpksByGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Min = result.Min(x => x.Min),
                Max = result.Max(x => x.Max),
                Avg = result.Average(x => x.Avg),
                AvgWeight = result.Average(x => x.AvgWeight)
            };
            result.Add(summary);

            return result;
        }

        public List<UpksByGroupsAndSubGroupsZuDto> GetDataByGroupsAndSubgroupsUpksZu(long[] taskIdList)
        {
            var sql = GetSqlForUpks(taskIdList, true);
            var result = QSQuery.ExecuteSql<UpksByGroupsAndSubGroupsZuDto>(sql);

            var summary = new UpksByGroupsAndSubGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                SubGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Min = result.Min(x => x.Min),
                Max = result.Max(x => x.Max),
                Avg = result.Average(x => x.Avg),
                AvgWeight = result.Average(x => x.AvgWeight)
            };
            result.Add(summary);

            return result;
        }

        private string GetSqlForUpks(long[] taskIdList, bool withSubGroups)
        {
            string contents;
            using (var sr = new StreamReader(Configuration.GetFileStream("MinMaxAverageUPKSZu", "sql", "SqlQueries")))
            {
                contents = sr.ReadToEnd();
            }

            var subGroupSelectionFromQuery = string.Empty;
            var subGroupForGrouping = string.Empty;
            if (withSubGroups)
            {
                subGroupSelectionFromQuery = @"L1_R205.GROUP_NAME as ""SubGroup"",";
                subGroupForGrouping = @", ""SubGroup""";
            }

            return string.Format(contents, subGroupSelectionFromQuery, string.Join(",", taskIdList), subGroupForGrouping);
        }

        public List<UpksByGroupsOksDto> GetDataByGroupsUpksOks(long[] taskIdList)
        {
            string contents;
            using (var sr = new StreamReader(Configuration.GetFileStream("MinMaxAverageUPKSOks", "sql", "SqlQueries")))
            {
                contents = sr.ReadToEnd();
            }

            var buildingPurposeAttr = _statisticalDataService.GetRosreestrBuildingPurposeAttribute();
            var placementPurposeAttr = _statisticalDataService.GetRosreestrPlacementPurposeAttribute();

            var sql = string.Format(contents, string.Join(", ", taskIdList), buildingPurposeAttr.Id, placementPurposeAttr.Id);
            var result = QSQuery.ExecuteSql<UpksByGroupsOksDto>(sql);

            var groupingOksDictionaries = result.GroupBy(x => new
            {
                x.Purpose,
                x.HasPurpose,
                x.PropertyType
            }, (k, g) => new GroupingOksDictionary
            {
                Key = new GroupingOks
                {
                    HasPurpose = k.HasPurpose,
                    Purpose = k.Purpose,
                    PropertyType = k.PropertyType,
                },
                Values = g.ToList()
            }).ToList();

            groupingOksDictionaries.ForEach(x =>
            {
                var summary = GetSummaryUpksOks(x);

                result.Add(summary);
            });

            return result;
        }

        private static UpksByGroupsOksDto GetSummaryUpksOks(GroupingOksDictionary groupingOksDictionary)
        {
            var objectCount = groupingOksDictionary.Values.GroupBy(x => x.ParentGroup).Sum(x => x.FirstOrDefault()?.ObjectsCount ?? 0);

            var upksValues = groupingOksDictionary.Values.ToList();

            return new UpksByGroupsOksDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                PropertyType = groupingOksDictionary.Key.PropertyType,
                Purpose = groupingOksDictionary.Key.Purpose,
                HasPurpose = groupingOksDictionary.Key.HasPurpose,
                ObjectsCount = objectCount,
                Min = upksValues.Min(x => x.Min),
                Avg = upksValues.Average(x => x.Avg),
                AvgWeight = upksValues.Average(x => x.AvgWeight),
                Max = upksValues.Max(x => x.Max)
            };
        }

        #endregion


        #region UPKS And UPRS

        public List<UpksAndUprsByGroupsZuDto> GetDataByGroupsUpksAndUprsZu(long[] taskIdList)
        {
            var upksSql = GetSqlForUpks(taskIdList, false);
            var upksResults = QSQuery.ExecuteSql<UpksByGroupsZuDto>(upksSql);

            var uprsSql = GetSqlForUprs(taskIdList, false);
            var uprsResults = QSQuery.ExecuteSql<UprsByGroupsZuDto>(uprsSql);

            var result = new List<UpksAndUprsByGroupsZuDto>();
            foreach (var upks in upksResults)
            {
                var uprs = uprsResults.FirstOrDefault(x => x.ParentGroup == upks.ParentGroup);
                var map = Map(uprs, upks);
                result.Add(map);
            }

            result.Add(new UpksAndUprsByGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Upks = new CalculationInfoDto
                {
                    Min = result.Min(x => x.Upks.Min),
                    Avg = result.Average(x => x.Upks.Avg),
                    AvgWeight = result.Average(x => x.Upks.AvgWeight),
                    Max = result.Max(x => x.Upks.Max)
                },
                Uprs = new CalculationInfoDto
                {
                    Min = result.Min(x => x.Uprs.Min),
                    Avg = result.Average(x => x.Uprs.Avg),
                    AvgWeight = result.Average(x => x.Uprs.AvgWeight),
                    Max = result.Max(x => x.Uprs.Max)
                }
            });

            return result;
        }

        public List<ByGroupsAndSubGroupsZuDto> GetDataByGroupsAndSubGroupsUpksAndUprsZu(long[] taskIdList)
        {
            var upksSql = GetSqlForUpks(taskIdList, true);
            var upksResults = QSQuery.ExecuteSql<UpksByGroupsAndSubGroupsZuDto>(upksSql);

            var uprsSql = GetSqlForUprs(taskIdList, true);
            var uprsResults = QSQuery.ExecuteSql<UprsByGroupsAndSubGroupsZuDto>(uprsSql);

            var result = new List<ByGroupsAndSubGroupsZuDto>();
            foreach (var upks in upksResults)
            {
                var uprs = uprsResults.FirstOrDefault(x => x.ParentGroup == upks.ParentGroup && x.SubGroup == upks.SubGroup);
                var map = Map(uprs, upks);
                map.SubGroup = uprs?.SubGroup;
                result.Add(map);
            }

            result.Add(new ByGroupsAndSubGroupsZuDto
            {
                ParentGroup = "Итого по субъекту РФ г Москва",
                SubGroup = "Итого по субъекту РФ г Москва",
                ObjectsCount = result.Sum(x => x.ObjectsCount),
                Upks = new CalculationInfoDto
                {
                    Min = result.Min(x => x.Upks.Min),
                    Avg = result.Average(x => x.Upks.Avg),
                    AvgWeight = result.Average(x => x.Upks.AvgWeight),
                    Max = result.Max(x => x.Upks.Max)
                },
                Uprs = new CalculationInfoDto
                {
                    Min = result.Min(x => x.Uprs.Min),
                    Avg = result.Average(x => x.Uprs.Avg),
                    AvgWeight = result.Average(x => x.Uprs.AvgWeight),
                    Max = result.Max(x => x.Uprs.Max)
                }
            });

            return result;
        }

        private ByGroupsAndSubGroupsZuDto Map(UprsByGroupsZuDto uprs,
            UpksByGroupsZuDto upks)
        {
            return new ByGroupsAndSubGroupsZuDto
            {
                ParentGroup = uprs?.ParentGroup,
                ObjectsCount = uprs?.ObjectsCount ?? 0,
                Upks = new CalculationInfoDto
                {
                    Min = upks.Min,
                    Avg = upks.Avg,
                    AvgWeight = upks.AvgWeight,
                    Max = upks.Max
                },
                Uprs = new CalculationInfoDto
                {
                    Min = uprs?.Min,
                    Avg = uprs?.Avg,
                    AvgWeight = uprs?.AvgWeight,
                    Max = uprs?.Max
                }
            };
        }

        #endregion


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
