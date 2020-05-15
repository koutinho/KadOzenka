using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.Charts;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticsReports;
using KadOzenka.Dal.Oks;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class DashboardWidgetService
	{
		#region ObjectsByGroupsWidget

		public List<ChartGroupDto> GetChartParentGroupsData(DateTime actualDate)
		{
			var dateFrom = actualDate.Date;
			var dateTo = actualDate.GetEndOfTheDay();

			var parentGroupChartData = new List<ChartGroupDto>();

			var loadedObjectsByGroups = OMUnit
				.Where(x => x.ParentTask.CreationDate >= dateFrom && x.ParentTask.CreationDate <= dateTo)
				.GroupBy(x => x.GroupId)
				.ExecuteSelect(x => new
				{
					x.GroupId,
					ObjectCount = QSExtensions.Count<OMUnit>(y => 1)
				}).Select(x => new ChartGroupDto{ GroupId = x.GroupId.GetValueOrDefault(-1), ObjectCount = x.ObjectCount}).ToList();
			var groupsId = loadedObjectsByGroups.Select(x => x.GroupId).ToList();
			if (groupsId.IsEmpty())
			{
				return parentGroupChartData;
			}

			var parentGroups = OMGroup.Where(x => groupsId.Contains(x.Id) && x.ParentId == -1
			                      && (x.GroupAlgoritm_Code == KoGroupAlgoritm.MainOKS || x.GroupAlgoritm_Code == KoGroupAlgoritm.MainParcel))
				.Select(x => x.Id)
				.Select(x => x.ParentId)
				.Select(x => x.GroupName)
				.Select(x => x.GroupAlgoritm_Code)
				.Execute();

			foreach (var parentGroup in parentGroups)
			{
				var chartGroupDto = new ChartGroupDto
					{
						GroupId = parentGroup.Id,
						GroupName = parentGroup.GroupName,
						ObjectType = parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainOKS
							? ObjectType.Oks
							: ObjectType.ZU,
						ObjectCount = loadedObjectsByGroups.First(x => x.GroupId == parentGroup.Id).ObjectCount,
						ChildChartGroupDtoList = new List<ChartChildGroupDto>()
					};
				chartGroupDto.ChildChartGroupDtoList.Add(new ChartChildGroupDto
				{
					GroupId = parentGroup.Id, GroupName = parentGroup.GroupName,
					ObjectCount = loadedObjectsByGroups.First(x => x.GroupId == parentGroup.Id).ObjectCount
				});
				parentGroupChartData.Add(chartGroupDto);
			}
			var parentGroupsId = parentGroups.Select(x => x.Id).ToList();

			var childGroupsId = groupsId.Where(x => !parentGroupsId.Contains(x)).ToList();
			var childGroups = OMGroup.Where(x => childGroupsId.Contains(x.Id))
				.Select(x => x.Id)
				.Select(x => x.ParentId)
				.Select(x => x.GroupName)
				.Select(x => x.GroupAlgoritm_Code)
				.Execute();

			var parentGroupCache = new Dictionary<long, OMGroup>();
			foreach (var childGroup in childGroups)
			{
				var objCount = loadedObjectsByGroups.First(x => x.GroupId == childGroup.Id).ObjectCount;
				if (parentGroupChartData.Select(x=>x.GroupId).Contains(childGroup.ParentId.GetValueOrDefault(-1)))
				{
					var parentGroup =
						parentGroupChartData.First(x => x.GroupId == childGroup.ParentId.GetValueOrDefault(-1));
					parentGroup.ObjectCount +=
						objCount;
					parentGroup.ChildChartGroupDtoList.Add(new ChartChildGroupDto
					{
						GroupId = childGroup.Id,
						GroupName = childGroup.GroupName,
						ObjectCount = objCount
					});
				}
				else
				{
					var parentId = childGroup.ParentId;
					while (true)
					{
						if (!parentId.HasValue || parentId == -1)
						{
							break;
						}

						if (!parentGroupCache.ContainsKey(parentId.Value))
						{
							var parentGroupDb = OMGroup.Where(x => x.Id == parentId).SelectAll().ExecuteFirstOrDefault();
							if (parentGroupDb == null)
							{
								break;
							}
							parentGroupCache.Add(parentGroupDb.Id, parentGroupDb);
						}

						var parentGroup = parentGroupCache[parentId.Value];
						if (parentGroup.ParentId == -1)
						{
							if (parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainOKS ||
							    parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainParcel)
							{
								var chartGroupDto = new ChartGroupDto
								{
									GroupId = parentGroup.Id,
									GroupName = parentGroup.GroupName,
									ObjectType = parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainOKS
										? ObjectType.Oks
										: ObjectType.ZU,
									ObjectCount = objCount,
									ChildChartGroupDtoList = new List<ChartChildGroupDto>()
								};
								chartGroupDto.ChildChartGroupDtoList.Add(new ChartChildGroupDto
								{
									GroupId = childGroup.Id,
									GroupName = childGroup.GroupName,
									ObjectCount = objCount
								});
								parentGroupChartData.Add(chartGroupDto);
							}

							break;
						}

						parentId = parentGroup.ParentId;
					}
				}
			}

			return parentGroupChartData;
		}

		public Stream ExportChartDataToExcel(DateTime actualDate)
		{
			ExcelFile excelTemplate = new ExcelFile();
			var mainWorkSheet = excelTemplate.Worksheets.Add("Экспорт данных");
			DataExportCommon.AddRow(mainWorkSheet, 0,
				new object[]
				{
					"Кадастровый номер объекта", "Вид объекта недвижимости", "Площадь", "Кадастровая стоимость",
					"Группа", "Погруппа"
				});

			var dateFrom = actualDate.Date;
			var dateTo = actualDate.GetEndOfTheDay();
			var creationDateFromCondition =
				new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.GreaterOrEqual, dateFrom);
			var creationDateToCondition =
				new QSConditionSimple(OMTask.GetColumn(x => x.CreationDate), QSConditionType.LessOrEqual, dateTo);
			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition> { creationDateFromCondition, creationDateToCondition }
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
					}
				}
			};
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, "Square"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "CadastralCost"));
			query.AddColumn(OMGroup.GetColumn(x => x.Id, "GroupId"));
			query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "GroupName"));
			query.AddColumn(OMGroup.GetColumn(x => x.GroupAlgoritm_Code, "GroupAlgoritm_Code"));
			query.AddColumn(OMGroup.GetColumn(x => x.ParentId, "ParentGroupId"));

			var table = query.ExecuteQuery();

			if (table.Rows.Count != 0)
			{
				var cancelTokenSource = new CancellationTokenSource();
				var options = new ParallelOptions
				{
					CancellationToken = cancelTokenSource.Token,
					MaxDegreeOfParallelism = 20
				};
				var locked = new object();
				var rowValueList = new List<List<object>>();
				var parentGroupCache = new Dictionary<long, OMGroup>();

				Parallel.ForEach(table.AsEnumerable(), options, obj =>
				{
					var groupId = obj["GroupId"].ParseToLongNullable();
					
					if (groupId.HasValue)
					{
						var parentGroupId = obj["ParentGroupId"].ParseToLongNullable();
						var groupAlgoritmCode = obj["GroupAlgoritm_Code"].ParseToLongNullable();

						if (parentGroupId.HasValue && parentGroupId != -1)
						{
							if (!parentGroupCache.ContainsKey(parentGroupId.Value))
							{
								var parentGroupDb = OMGroup.Where(x => x.Id == parentGroupId).SelectAll().ExecuteFirstOrDefault();
								if (parentGroupDb != null)
								{
									lock (locked)
									{
										if (!parentGroupCache.ContainsKey(parentGroupId.Value))
										{
											parentGroupCache.Add(parentGroupId.Value, parentGroupDb);
										}
									}

									var parentGroup = parentGroupCache[parentGroupId.Value];
									if (parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainOKS ||
									    parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainParcel)
									{
										var rowValue = new List<object>
										{
											obj["CadastralNumber"],
											obj["PropertyType"],
											obj["Square"].ParseToDecimalNullable(),
											obj["CadastralCost"].ParseToDecimalNullable(),
											obj["GroupName"],
											parentGroup.GroupName
										};
										lock (locked)
										{
											rowValueList.Add(rowValue);
										}
									}
								}
							}
							else
							{
								var parentGroup = parentGroupCache[parentGroupId.Value];
								if (parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainOKS ||
								     parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainParcel)
								{
									var rowValue = new List<object>
									{
										obj["CadastralNumber"],
										obj["PropertyType"],
										obj["Square"].ParseToDecimalNullable(),
										obj["CadastralCost"].ParseToDecimalNullable(),
										obj["GroupName"],
										parentGroup.GroupName
									};
									lock (locked)
									{
										rowValueList.Add(rowValue);
									}
								}
							}
						}
						else
						{
							if (groupAlgoritmCode == (long) KoGroupAlgoritm.MainOKS ||
							    groupAlgoritmCode == (long) KoGroupAlgoritm.MainParcel)
							{
								var rowValue = new List<object>
								{
									obj["CadastralNumber"],
									obj["PropertyType"],
									obj["Square"].ParseToDecimalNullable(),
									obj["CadastralCost"].ParseToDecimalNullable(),
									obj["GroupName"]
								};
								lock (locked)
								{
									rowValueList.Add(rowValue);
								}
							}
						}
					}
				});

				var excelRow = 1;
				foreach (var rowValue in rowValueList)
				{
					DataExportCommon.AddRow(mainWorkSheet, excelRow, rowValue.ToArray());
					excelRow++;
				}
			}

			MemoryStream stream = new MemoryStream();
			excelTemplate.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);
			return stream;
		}

		#endregion ObjectsByGroupsWidget

		#region StatisticsReportsWidget

		public List<ZoneRegionDto> GetZoneData()
		{
			var entities = OMQuartalDictionary.Where(x => true)
				.Select(x => x.ZoneRegion)
				.OrderBy(x => x.ZoneRegion).Execute();
			return entities.Select(x => new ZoneRegionDto {Zone = x.ZoneRegion}).DistinctBy(x => x.Zone).ToList();
		}

		public List<UnitObjectDto> GetImportedObjectsData(DateTime? dateStart, DateTime? dateEnd)
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
					}
				}
			};
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(UnitObjectDto.CadastralNumber)));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, nameof(UnitObjectDto.PropertyType)));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, nameof(UnitObjectDto.Square)));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, nameof(UnitObjectDto.TaskCreationDate)));

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

			return result;
		}

		public List<ExportedObjectDto> GetExportedObjectsData(DateTime? dateStart, DateTime? dateEnd)
		{
			ValidateTaskCreationDatePeriod(dateStart, dateEnd);

			var recalculatedStatus = KoUnitStatus.Recalculated.GetEnumDescription();
			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition =  new QSConditionGroup
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

			var table = query.ExecuteQuery();
			var result = new List<ExportedObjectDto>();
			if (table.Rows.Count != 0)
			{
				for (var i =0; i < table.Rows.Count; i++)
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

			return result;
		}

		public List<ZoneStatisticDto> GetZoneStatisticsData(DateTime? dateStart, DateTime? dateEnd)
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

			return result;
		}

		public List<FactorStatisticDto> GetFactorStatisticsData(DateTime? dateStart, DateTime? dateEnd)
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
						RegisterId = OMUnitChange.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.Id),
							RightOperand = OMUnitChange.GetColumn(x => x.UnitId)
						},
						JoinType = QSJoinType.Inner
					}
				}
			};
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, nameof(FactorStatisticDto.CadastralNumber)));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, nameof(FactorStatisticDto.PropertyType)));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, nameof(FactorStatisticDto.Square)));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, nameof(FactorStatisticDto.TaskCreationDate)));
			query.AddColumn(OMUnitChange.GetColumn(x => x.ChangeStatus, nameof(FactorStatisticDto.ChangedFactor)));

			var table = query.ExecuteQuery();
			var data = new List<FactorStatisticDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new FactorStatisticDto
					{
						ID = table.Rows[i][nameof(FactorStatisticDto.ID)].ParseToLong(),
						CadastralNumber = table.Rows[i][nameof(FactorStatisticDto.CadastralNumber)].ParseToString(),
						PropertyType = table.Rows[i][nameof(FactorStatisticDto.PropertyType)].ParseToString(),
						Square = table.Rows[i][nameof(FactorStatisticDto.Square)].ParseToDecimalNullable(),
						TaskCreationDate = table.Rows[i][nameof(FactorStatisticDto.TaskCreationDate)].ParseToDateTime(),
						ChangedFactor = table.Rows[i][nameof(FactorStatisticDto.ChangedFactor)].ParseToString()
					};
					data.Add(dto);
				}
			}

			var result = data.GroupBy(x => new { x.ID, x.CadastralNumber, x.PropertyType, x.Square, x.TaskCreationDate }).Select(
				group => new FactorStatisticDto
				{
					ID = group.Key.ID,
					CadastralNumber = group.Key.CadastralNumber,
					PropertyType = group.Key.PropertyType,
					Square = group.Key.Square,
					TaskCreationDate = group.Key.TaskCreationDate,
					ChangedFactors = string.Join("; ",group.ToList().Select(x => x.ChangedFactor).Distinct().ToList())
				}).ToList();

			return result;
		}

		public List<GroupStatisticDto> GetGroupStatisticsData(DateTime? dateStart, DateTime? dateEnd)
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
			var table = query.ExecuteQuery();

			var result = new List<GroupStatisticDto>();
			var groupCache = new Dictionary<long, string>();
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
					};
					var subGroupId = table.Rows[i][nameof(GroupStatisticDto.SubGroupId)].ParseToLongNullable();
					if (subGroupId.HasValue && subGroupId != -1)
					{
						if (groupCache.ContainsKey(subGroupId.Value))
						{
							dto.SubGroup = groupCache[subGroupId.Value];
						}
						else
						{
							var subgroup = OMGroup.Where(x => x.Id == subGroupId).Select(x => x.GroupName)
								.ExecuteFirstOrDefault();
							dto.SubGroup = subgroup?.GroupName;
							groupCache.Add(subGroupId.Value, subgroup?.GroupName);
						}
					}

					result.Add(dto);
				}
			}

			return result;
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

		#endregion Helpers
	}
}
