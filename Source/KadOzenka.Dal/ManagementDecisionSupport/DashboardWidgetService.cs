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
using KadOzenka.Dal.Oks;
using ObjectModel.Directory;
using ObjectModel.KO;

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
			CommonSdks.DataExportCommon.AddRow(mainWorkSheet, 0,
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
					CommonSdks.DataExportCommon.AddRow(mainWorkSheet, excelRow, rowValue.ToArray());
					excelRow++;
				}
			}

			MemoryStream stream = new MemoryStream();
			excelTemplate.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);
			return stream;
		}

		#endregion ObjectsByGroupsWidget
	}
}
