using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports.DataSourceRequest.Filter;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;
using Serilog;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class StatisticsReportsWidgetService
	{
		private readonly ILogger _log = Log.ForContext<StatisticsReportsWidgetService>();
		#region StatisticsReportsWidget

		public List<ZoneRegionDto> GetZoneData()
		{
			var entities = OMQuartalDictionary.Where(x => true)
				.Select(x => x.ZoneRegion)
				.OrderBy(x => x.ZoneRegion).Execute();
			return entities.Select(x => new ZoneRegionDto { Zone = x.ZoneRegion }).DistinctBy(x => x.Zone).ToList();
		}

		#region ImportedObjects

		public List<UnitObjectDto> GetImportedObjectsData(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			var query = GetImportedObjectsDataQuery(request, dateStart, dateEnd);
			var sql = GetSqlQuery(request, query);
			_log.ForContext("SqlQuery", sql)
				.ForContext("Request", JsonConvert.SerializeObject(request))
				.ForContext("DateStart", dateStart)
				.ForContext("DateEnd", dateEnd)
				.Debug("Получение загруженных объектов");
			var result = QSQuery.ExecuteSql<UnitObjectDto>(sql);

			return result;
		}

		public long GetImportedObjectsDataCount(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			var query = GetImportedObjectsDataQuery(request, dateStart, dateEnd);
			var total = GetTotalCount(query);

			return total;
		}

		private QSQuery GetImportedObjectsDataQuery(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			ValidateTaskCreationDatePeriod(dateStart, dateEnd);

			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.GreaterOrEqual,
							dateStart.Value.Date),
						new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.Less,
							dateEnd.Value.GetEndOfTheDay())
					}
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMTask.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.TaskId),
							RightOperand = OMTask.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					}
				}
			};
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(UnitObjectDto.CadastralNumber)));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, nameof(UnitObjectDto.PropertyType)));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, nameof(UnitObjectDto.Square)));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, nameof(UnitObjectDto.TaskCreationDate)));

			HandleDataSourceRequest(query, request);

			return query;
		}

		#endregion ImportedObjects

		#region ExportedObjects

		public List<ExportedObjectDto> GetExportedObjectsData(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			var query = GetExportedObjectsDataQuery(request, dateStart, dateEnd);
			var sql = GetSqlQuery(request, query);
			_log.ForContext("SqlQuery", sql)
				.ForContext("Request", JsonConvert.SerializeObject(request))
				.ForContext("DateStart", dateStart)
				.ForContext("DateEnd", dateEnd)
				.Debug("Получение выгруженных объектов");
			var result = QSQuery.ExecuteSql<ExportedObjectDto>(sql);

			return result;
		}

		public long GetExportedObjectsDataCount(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			var query = GetExportedObjectsDataQuery(request, dateStart, dateEnd);
			var total = GetTotalCount(query);

			return total;
		}

		private QSQuery GetExportedObjectsDataQuery(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			ValidateTaskCreationDatePeriod(dateStart, dateEnd);

			var recalculatedStatus = KoUnitStatus.Recalculated.GetEnumDescription();
			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition>
					{
						new QSConditionGroup
						{
							Type = QSConditionGroupType.And,
							Conditions = new List<QSCondition>
							{
								new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.GreaterOrEqual,
									dateStart.Value.Date),
								new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.LessOrEqual,
									dateEnd.Value.GetEndOfTheDay())
							}
						},
						new QSConditionSimple(OMUnit.GetColumn(x => x.Status), QSConditionType.Equal, recalculatedStatus)
					}
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMTask.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.TaskId),
							RightOperand = OMTask.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					}
				}
			};
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(ExportedObjectDto.CadastralNumber)));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, nameof(ExportedObjectDto.PropertyType)));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, nameof(ExportedObjectDto.Square)));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, nameof(ExportedObjectDto.TaskCreationDate)));
			query.AddColumn(OMUnit.GetColumn(x => x.Status, nameof(ExportedObjectDto.Status)));

			HandleDataSourceRequest(query, request);

			return query;
		}

		#endregion ExportedObjects

		#region ZoneStatistics

		public List<ZoneStatisticDto> GetZoneStatisticsData(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			var query = GetZoneStatisticsDataQuery(request, dateStart, dateEnd);
			var sql = GetSqlQuery(request, query);
			_log.ForContext("SqlQuery", sql)
				.ForContext("Request", JsonConvert.SerializeObject(request))
				.ForContext("DateStart", dateStart)
				.ForContext("DateEnd", dateEnd)
				.Debug("Получение статистики по зонам");
			var result = QSQuery.ExecuteSql<ZoneStatisticDto>(sql);

			return result;
		}

		public long GetZoneStatisticsDataCount(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			var query = GetZoneStatisticsDataQuery(request, dateStart, dateEnd);
			var total = GetTotalCount(query);

			return total;
		}

		private QSQuery GetZoneStatisticsDataQuery(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			ValidateTaskCreationDatePeriod(dateStart, dateEnd);

			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.GreaterOrEqual,
							dateStart.Value.Date),
						new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.LessOrEqual,
							dateEnd.Value.GetEndOfTheDay())
					}
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMTask.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.TaskId),
							RightOperand = OMTask.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					},
					new QSJoin
					{
						RegisterId = OMQuartalDictionary.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.CadastralBlock),
							RightOperand = OMQuartalDictionary.GetColumn(x => x.CadastralQuartal)
						},
						JoinType = QSJoinType.Left
					}
				}
			};
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(ZoneStatisticDto.CadastralNumber)));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, nameof(ZoneStatisticDto.PropertyType)));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, nameof(ZoneStatisticDto.Square)));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, nameof(ZoneStatisticDto.TaskCreationDate)));
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.ZoneRegion, nameof(ZoneStatisticDto.Zone)));

			HandleDataSourceRequest(query, request);

			return query;
		}

		#endregion ZoneStatistics

		#region FactorStatistics

		public List<FactorStatisticDto> GetFactorStatisticsData(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			var mainSql = GetFactorStatisticsDataMainSql(dateStart, dateEnd);
			var dataSql = "SELECT * FROM " + mainSql;
			var columns = new List<string>
			{
				nameof(FactorStatisticDto.CadastralNumber),
				nameof(FactorStatisticDto.PropertyType),
				nameof(FactorStatisticDto.Square),
				nameof(FactorStatisticDto.TaskCreationDate),
				nameof(FactorStatisticDto.ChangedFactors),
			};
			AddQueryCustomFilters(ref dataSql, columns, request.Filters);
			AddQuerySorting(ref dataSql, columns, request.Sorts);
			dataSql += $" LIMIT {request.PageSize}";
			if (request.Page > 1)
				dataSql += $" OFFSET {(request.Page - 1) * request.PageSize}";
			
			_log.ForContext("SqlQuery", dataSql)
				.ForContext("Request", JsonConvert.SerializeObject(request))
				.ForContext("DateStart", dateStart)
				.ForContext("DateEnd", dateEnd)
				.Debug("Получение статистики по ценообразующим факторам");
			var result = QSQuery.ExecuteSql<FactorStatisticDto>(dataSql);

			return result;
		}

		public long GetFactorStatisticsDataCount(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			var mainSql = GetFactorStatisticsDataMainSql(dateStart, dateEnd);
			var countSql = "SELECT COUNT(*) as Total FROM " + mainSql;
			var columns = new List<string>
			{
				nameof(FactorStatisticDto.CadastralNumber),
				nameof(FactorStatisticDto.PropertyType),
				nameof(FactorStatisticDto.Square),
				nameof(FactorStatisticDto.TaskCreationDate),
				nameof(FactorStatisticDto.ChangedFactors),
			};
			AddQueryCustomFilters(ref countSql, columns, request.Filters);

			//return QSQuery.ExecuteSql<GridDataDto<FactorStatisticDto>>(countSql).First().Total.GetValueOrDefault();
			return QSQuery.ExecuteSql<long?>(countSql).First().GetValueOrDefault();
		}

		private string GetFactorStatisticsDataMainSql(DateTime? dateStart, DateTime? dateEnd)
		{
			ValidateTaskCreationDatePeriod(dateStart, dateEnd);

			var mainSql = $@"
	(SELECT 
			L1_R201.ID AS ID,
			L1_R201.CADASTRAL_NUMBER AS CadastralNumber,
			L1_R201.PROPERTY_TYPE AS PropertyType,
			L1_R201.SQUARE AS Square,
			L1_R203.CREATION_DATE AS TaskCreationDate,
				(SELECT L2.ChangedFactors
				 FROM
					(SELECT L3.id_unit, string_agg(L3.status_change, ', ') AS ChangedFactors 
					FROM
						(SELECT DISTINCT id_unit, status_change
						FROM ko_unit_change) L3
					GROUP  BY 1
					) L2
				 WHERE L2.id_unit = L1_R201.ID)  AS ChangedFactors
		FROM KO_UNIT L1_R201
		 JOIN KO_TASK L1_R203
		 ON (L1_R203.ID = L1_R201.TASK_ID)
		WHERE EXISTS (SELECT 1 FROM ko_unit_change where id_unit=L1_R201.ID)
	 ) main_query
WHERE
	TaskCreationDate >= {CrossDBSQL.ToDate(dateStart.Value.Date)}
	 AND
	TaskCreationDate <= {CrossDBSQL.ToDate(dateEnd.Value.GetEndOfTheDay())}
";
			return mainSql;
		}

		#endregion FactorStatistics

		#region GroupStatistics

		public long GetGroupStatisticsDataCount(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			var query = GetGroupStatisticsQuery(request, dateStart, dateEnd);
			var total = GetTotalCount(query);

			return total;
		}

		public List<GroupStatisticDto> GetGroupStatisticsData(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			var query = GetGroupStatisticsQuery(request, dateStart, dateEnd);
			var sql = GetSqlQuery(request, query);
			_log.ForContext("SqlQuery", sql)
				.ForContext("Request", JsonConvert.SerializeObject(request))
				.ForContext("DateStart", dateStart)
				.ForContext("DateEnd", dateEnd)
				.Debug("Получение статистики по группам");
			var result = QSQuery.ExecuteSql<GroupStatisticDto>(sql);

			return result;
		}

		private QSQuery GetGroupStatisticsQuery(DataSourceRequestDto request, DateTime? dateStart, DateTime? dateEnd)
		{
			ValidateTaskCreationDatePeriod(dateStart, dateEnd);

			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.GreaterOrEqual,
							dateStart.Value.Date),
						new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.LessOrEqual,
							dateEnd.Value.GetEndOfTheDay())
					}
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMTask.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.TaskId),
							RightOperand = OMTask.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					},
					new QSJoin
					{
						RegisterId = OMGroup.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.GroupId),
							RightOperand = OMGroup.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Left
					},
				}
			};
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(GroupStatisticDto.CadastralNumber)));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, nameof(GroupStatisticDto.PropertyType)));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, nameof(GroupStatisticDto.Square)));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, nameof(GroupStatisticDto.TaskCreationDate)));
			query.AddColumn(OMGroup.GetColumn(x => x.GroupName, nameof(GroupStatisticDto.Group)));
			query.AddColumn(OMGroup.GetColumn(x => x.ParentId, nameof(GroupStatisticDto.SubGroupId)));

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
							OMGroup.GetColumn(x => x.ParentId)){
							RightOperandLevel = 1
						}
					}
				}
			};
			query.AddColumn(subQuery, nameof(GroupStatisticDto.SubGroup));

			HandleDataSourceRequest(query, request);

			return query;
		}

		#endregion  GroupStatistics

		#endregion StatisticsReportsWidget

		#region Helpers

		private void ValidateTaskCreationDatePeriod(DateTime? dateStart, DateTime? dateEnd)
		{
			if (!dateStart.HasValue)
			{
				throw new Exception("Не указана дата начала периода");
			}

			if (!dateEnd.HasValue)
			{
				throw new Exception("Не указана дата окончания периода");
			}
		}

		private static string GetSqlQuery(DataSourceRequestDto request, QSQuery query)
		{
			var sql = query.GetSql();
			sql += $" LIMIT {request.PageSize}";
			if (request.Page > 1)
				sql += $" OFFSET {(request.Page - 1) * request.PageSize}";

			return sql;
		}

		private long GetTotalCount(QSQuery query)
		{
			query.Columns.Clear();
			query.OrderBy.Clear();
			query.GroupBy.Clear();
			query.PackageSize = 0;
			query.PackageIndex = 0;
			return query.ExecuteCount();
		}

		private void HandleDataSourceRequest(QSQuery query, DataSourceRequestDto request)
		{
			if (request == null)
			{
				return;
			}

			AddQsCustomFilters(query, request.Filters);
			AddQsSorting(query, request.Sorts);
		}

		private void AddQsCustomFilters(QSQuery query, List<FilterDto> filters)
		{
			if (filters.Any())
			{
				foreach (var filter in filters)
				{
					if (filter is FilterSimpleDto descriptor)
					{
						if (query.Columns.Any(x => x.Alias == descriptor.Member))
						{
							QSCondition condition = null;
							var column = query.Columns.First(x => x.Alias == descriptor.Member);
							if (column is QSColumnSimple)
							{
								var attributeData = RegisterCache.GetAttributeData(((QSColumnSimple)column).AttributeID);
								if (attributeData.Type == RegisterAttributeType.DATE)
								{
									var dateTime = descriptor.Value.ParseToDateTimeNullable();
									if (dateTime.HasValue)
									{
										condition = new QSConditionGroup()
										{
											Conditions = new List<QSCondition>
											{
												new QSConditionSimple(column, QSConditionType.GreaterOrEqual, dateTime.Value.Date.ToString("yyyy-MM-dd")),
												new QSConditionSimple(column, QSConditionType.Less, dateTime.Value.Date.AddDays(1).ToString("yyyy-MM-dd"))
											},
											Type = QSConditionGroupType.And
										};
									}
								}
								else
								{
									condition = new QSConditionSimple(column, GetQsConditionType(descriptor.Operator), new QSColumnConstant(descriptor.Value));
								}

							}
							else
							{
								condition = new QSConditionSimple(column, GetQsConditionType(descriptor.Operator), new QSColumnConstant(descriptor.Value));
							}

							if (condition != null)
							{
								var conditionGroup = new QSConditionGroup(QSConditionGroupType.And);
								conditionGroup.Conditions = new List<QSCondition> { condition, query.Condition };
								query.Condition = conditionGroup;
							}
						}
					}
					else if (filter is FilterCompositeDto)
					{
						AddQsCustomFilters(query, ((FilterCompositeDto)filter).Filters);
					}
				}
			}
		}

		private QSConditionType GetQsConditionType(FilterOperatorType descriptorOperator)
		{
			switch (descriptorOperator)
			{
				case FilterOperatorType.Contains:
					return QSConditionType.Contains;
				case FilterOperatorType.Equal:
					return QSConditionType.Equal;
				default:
					return QSConditionType.Equal;
			}
		}

		private void AddQsSorting(QSQuery query, List<SortDto> requestSorts)
		{
			if (requestSorts.Any())
			{
				foreach (var sortDescriptor in requestSorts)
				{
					if (query.Columns.Any(x => x.Alias == sortDescriptor.Member))
					{
						query.OrderBy.Add(new QSOrder
						{
							Order = sortDescriptor.SortDirection == SortDirectionType.Ascending ? QSOrderType.ASC : QSOrderType.DESC,
							ColumnAlias = sortDescriptor.Member
						});
					}
				}
			}
			//else
			//{
			//	query.OrderBy.Add(new QSOrder
			//	{
			//		ColumnAlias = "ID"
			//	});
			//}
		}

		private void AddQueryCustomFilters(ref string query, List<string> columns, List<FilterDto> filters)
		{
			if (filters.Any())
			{
				foreach (var filter in filters)
				{
					var descriptor = filter as FilterSimpleDto;
					if (descriptor != null)
					{
						if (columns.Any(x => x == descriptor.Member))
						{
							if (descriptor.Member == nameof(FactorStatisticDto.TaskCreationDate))
							{
								var dateTime = descriptor.Value.ParseToDateTimeNullable();
								if (dateTime.HasValue)
								{
									query += $@"
 AND {descriptor.Member} >= {CrossDBSQL.ToDate(dateTime.Value.Date)}
 AND {descriptor.Member} <= {CrossDBSQL.ToDate(dateTime.Value.GetEndOfTheDay())}
";
								}
							}
							if (descriptor.Member == nameof(FactorStatisticDto.CadastralNumber) || descriptor.Member == nameof(FactorStatisticDto.ChangedFactors))
							{
								query += $@"
 AND POSITION('{descriptor.Value}' IN {descriptor.Member}) > 0";
							}

							if (descriptor.Member == nameof(FactorStatisticDto.PropertyType) || descriptor.Member == nameof(FactorStatisticDto.Square))
							{
								query += descriptor.Member == nameof(FactorStatisticDto.Square)
									? $" AND {descriptor.Member} = {descriptor.Value.ParseToDecimal().ToString(CultureInfo.InvariantCulture)}"
									: $" AND {descriptor.Member} = '{descriptor.Value}'";
							}
						}
					}
					else if (filter is FilterCompositeDto)
					{
						AddQueryCustomFilters(ref query, columns, ((FilterCompositeDto)filter).Filters);
					}
				}
			}
		}

		private void AddQuerySorting(ref string query, List<string> columns, List<SortDto> requestSorts)
		{
			if (requestSorts.Any())
			{
				query += " ORDER BY ";
				var sorts = new List<string>();
				foreach (var sortDescriptor in requestSorts)
				{
					if (columns.Any(x => x == sortDescriptor.Member))
					{
						sorts.Add(
							$" {sortDescriptor.Member} {(sortDescriptor.SortDirection == SortDirectionType.Ascending ? "ASC" : "DESC")} ");
					}
				}

				query += string.Join(",", sorts);
			}
			//else
			//{
			//	query += " ORDER BY ID";
			//}
		}

		#endregion Helpers
	}
}
