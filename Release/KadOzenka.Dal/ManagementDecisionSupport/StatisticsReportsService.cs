using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;
using FilterOperator = Kendo.Mvc.FilterOperator;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class StatisticsReportsService
	{
		#region StatisticsReportsWidget

		public List<ZoneRegionDto> GetZoneData()
		{
			var entities = OMQuartalDictionary.Where(x => true)
				.Select(x => x.ZoneRegion)
				.OrderBy(x => x.ZoneRegion).Execute();
			return entities.Select(x => new ZoneRegionDto { Zone = x.ZoneRegion }).DistinctBy(x => x.Zone).ToList();
		}

		public GridDataDto<UnitObjectDto> GetImportedObjectsData(DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
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

			var table = query.ExecuteQuery();
			var result = new List<UnitObjectDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new UnitObjectDto
					{
						ID = table.Rows[i][nameof(UnitObjectDto.ID)].ParseToLong(),
						CadastralNumber = table.Rows[i][nameof(UnitObjectDto.CadastralNumber)].ParseToString(),
						PropertyType = table.Rows[i][nameof(UnitObjectDto.PropertyType)].ParseToString(),
						Square = table.Rows[i][nameof(UnitObjectDto.Square)].ParseToDecimalNullable(),
						TaskCreationDate = table.Rows[i][nameof(UnitObjectDto.TaskCreationDate)].ParseToDateTime(),
					};
					result.Add(dto);
				}
			}
			var total = GetTotalCount(query);

			return new GridDataDto<UnitObjectDto> { Data = result, Total = total };
		}

		public GridDataDto<ExportedObjectDto> GetExportedObjectsData(DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
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

			var table = query.ExecuteQuery();
			var result = new List<ExportedObjectDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new ExportedObjectDto
					{
						ID = table.Rows[i][nameof(ExportedObjectDto.ID)].ParseToLong(),
						CadastralNumber = table.Rows[i][nameof(ExportedObjectDto.CadastralNumber)].ParseToString(),
						PropertyType = table.Rows[i][nameof(ExportedObjectDto.PropertyType)].ParseToString(),
						Square = table.Rows[i][nameof(ExportedObjectDto.Square)].ParseToDecimalNullable(),
						TaskCreationDate = table.Rows[i][nameof(ExportedObjectDto.TaskCreationDate)].ParseToDateTime(),
						Status = table.Rows[i][nameof(ExportedObjectDto.Status)].ParseToString()
					};
					result.Add(dto);
				}
			}
			var total = GetTotalCount(query);

			return new GridDataDto<ExportedObjectDto> { Data = result, Total = total };
		}

		public GridDataDto<ZoneStatisticDto> GetZoneStatisticsData(DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
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

			var table = query.ExecuteQuery();
			var result = new List<ZoneStatisticDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new ZoneStatisticDto
					{
						ID = table.Rows[i][nameof(ZoneStatisticDto.ID)].ParseToLong(),
						CadastralNumber = table.Rows[i][nameof(ZoneStatisticDto.CadastralNumber)].ParseToString(),
						PropertyType = table.Rows[i][nameof(ZoneStatisticDto.PropertyType)].ParseToString(),
						Square = table.Rows[i][nameof(ZoneStatisticDto.Square)].ParseToDecimalNullable(),
						TaskCreationDate = table.Rows[i][nameof(ZoneStatisticDto.TaskCreationDate)].ParseToDateTime(),
						Zone = table.Rows[i][nameof(ZoneStatisticDto.Zone)].ParseToString()
					};
					result.Add(dto);
				}
			}

			var total = GetTotalCount(query);

			return new GridDataDto<ZoneStatisticDto> { Data = result, Total = total };
		}

		public GridDataDto<FactorStatisticDto> GetFactorStatisticsData(DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
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
			var columns = new List<string>
			{
				nameof(FactorStatisticDto.CadastralNumber),
				nameof(FactorStatisticDto.PropertyType),
				nameof(FactorStatisticDto.Square),
				nameof(FactorStatisticDto.TaskCreationDate),
				nameof(FactorStatisticDto.ChangedFactors),
			};

			var countSql = "SELECT COUNT(*) as Total FROM " + mainSql;
			var dataSql = "SELECT * FROM " + mainSql;

			if (request != null)
			{
				AddQueryCustomFilters(ref dataSql, columns, request.Filters);
				AddQueryCustomFilters(ref countSql, columns, request.Filters);
				AddQuerySorting(ref dataSql, columns, request.Sorts);
			}

			if (request?.Page == 1 && request.PageSize > 0)
			{
				dataSql += $" LIMIT {request.PageSize}";
			}
			else if (request?.PageSize > 0)
			{
				dataSql = $@"
select t2.ID,t2.CadastralNumber,t2.PropertyType,t2.Square,t2.TaskCreationDate, t2.ChangedFactors 
from (select t1.*, ROW_NUMBER () OVER(order by 0) QS_ROWNUM 
	from ({dataSql}) t1
) t2 where t2.QS_ROWNUM BETWEEN ({request.PageSize * (request.Page - 1) + 1}) AND ({request.PageSize * (request.Page)})
";
			}

			var result = QSQuery.ExecuteSql<FactorStatisticDto>(dataSql);
			var total = QSQuery.ExecuteSql<GridDataDto<FactorStatisticDto>>(countSql).First().Total;

			return new GridDataDto<FactorStatisticDto> { Data = result, Total = total };
		}

		public GridDataDto<GroupStatisticDto> GetGroupStatisticsData(DataSourceRequest request, DateTime? dateStart, DateTime? dateEnd)
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

			var table = query.ExecuteQuery();

			var result = new List<GroupStatisticDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new GroupStatisticDto
					{
						ID = table.Rows[i][nameof(GroupStatisticDto.ID)].ParseToLong(),
						CadastralNumber = table.Rows[i][nameof(GroupStatisticDto.CadastralNumber)].ParseToString(),
						PropertyType = table.Rows[i][nameof(GroupStatisticDto.PropertyType)].ParseToString(),
						Square = table.Rows[i][nameof(GroupStatisticDto.Square)].ParseToDecimalNullable(),
						TaskCreationDate = table.Rows[i][nameof(GroupStatisticDto.TaskCreationDate)].ParseToDateTime(),
						Group = table.Rows[i][nameof(GroupStatisticDto.Group)].ParseToString(),
						SubGroup = table.Rows[i][nameof(GroupStatisticDto.SubGroup)].ParseToString(),
					};
					result.Add(dto);
				}
			}
			var total = GetTotalCount(query);

			return new GridDataDto<GroupStatisticDto> { Data = result, Total = total };
		}

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

		private long GetTotalCount(QSQuery query)
		{
			query.Columns.Clear();
			query.OrderBy.Clear();
			query.GroupBy.Clear();
			query.PackageSize = 0;
			query.PackageIndex = 0;

			return query.ExecuteCount();
		}

		private void HandleDataSourceRequest(QSQuery query, DataSourceRequest request)
		{
			if (request == null)
			{
				return;
			}

			query.PackageSize = request.PageSize;
			query.PackageIndex = request.Page - 1;

			AddQsCustomFilters(query, request.Filters);
			AddQsSorting(query, request.Sorts);
		}

		private void AddQsCustomFilters(QSQuery query, IList<IFilterDescriptor> filters)
		{
			if (filters.Any())
			{
				foreach (var filter in filters)
				{
					var descriptor = filter as FilterDescriptor;
					if (descriptor != null)
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
					else if (filter is CompositeFilterDescriptor)
					{
						AddQsCustomFilters(query, ((CompositeFilterDescriptor)filter).FilterDescriptors);
					}
				}
			}
		}

		private QSConditionType GetQsConditionType(FilterOperator descriptorOperator)
		{
			switch (descriptorOperator)
			{
				case FilterOperator.Contains:
					return QSConditionType.Contains;
				case FilterOperator.IsEqualTo:
					return QSConditionType.Equal;
				default:
					return QSConditionType.Equal;
			}
		}

		private void AddQsSorting(QSQuery query, IList<SortDescriptor> requestSorts)
		{
			if (requestSorts.Any())
			{
				foreach (var sortDescriptor in requestSorts)
				{
					if (query.Columns.Any(x => x.Alias == sortDescriptor.Member))
					{
						query.OrderBy.Add(new QSOrder
						{
							Order = sortDescriptor.SortDirection == ListSortDirection.Ascending ? QSOrderType.ASC : QSOrderType.DESC,
							ColumnAlias = sortDescriptor.Member
						});
					}
				}
			}
		}

		private void AddQueryCustomFilters(ref string query, List<string> columns, IList<IFilterDescriptor> filters)
		{
			if (filters.Any())
			{
				foreach (var filter in filters)
				{
					var descriptor = filter as FilterDescriptor;
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
					else if (filter is CompositeFilterDescriptor)
					{
						AddQueryCustomFilters(ref query, columns, ((CompositeFilterDescriptor)filter).FilterDescriptors);
					}
				}
			}
		}

		private void AddQuerySorting(ref string query, List<string> columns, IList<SortDescriptor> requestSorts)
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
							$" {sortDescriptor.Member} {(sortDescriptor.SortDirection == ListSortDirection.Ascending ? "ASC" : "DESC")} ");
					}
				}

				query += string.Join(",", sorts);
			}
		}

		#endregion Helpers
	}
}
