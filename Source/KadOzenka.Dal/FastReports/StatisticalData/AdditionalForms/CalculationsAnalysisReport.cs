using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.DataInfo;
using KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms.Entities.ReportItem;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.FastReports.StatisticalData.AdditionalForms
{
	public class CalculationsAnalysisReport : StatisticalDataReport
	{
		protected override string TemplateName(NameValueCollection query)
		{
			return "AdditionalFormsCalculationsAnalysisReport";
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			var taskIdList = GetTaskIdList(query);
			var reportItems = GetReportItems(taskIdList);

			var dataSet = new DataSet();
			var dataTable = GetReportDataTable(reportItems);
			dataSet.Tables.Add(dataTable);

			return dataSet;
		}

		#region Support Methods

		public DataTable GetReportDataTable(List<ReportItem> reportItems)
		{
			var dataTable = new DataTable("Data");
			dataTable.Columns.Add("CadastralNumber", typeof(string));
			dataTable.Columns.Add("Type", typeof(string));
			dataTable.Columns.Add("Square", typeof(string));
			dataTable.Columns.Add("ObjectNameTypeOfUse", typeof(string));
			dataTable.Columns.Add("Purpose", typeof(string));
			dataTable.Columns.Add("Address", typeof(string));
			dataTable.Columns.Add("Location", typeof(string));
			dataTable.Columns.Add("EvaluationSubgroup2018", typeof(string));
			dataTable.Columns.Add("Upks2018", typeof(string));
			dataTable.Columns.Add("CadastralCost2018", typeof(decimal));
			dataTable.Columns.Add("CadastralQuartal2018", typeof(string));
			dataTable.Columns.Add("TaskType", typeof(string));
			dataTable.Columns.Add("EvaluationSubgroup", typeof(string));
			dataTable.Columns.Add("Upks", typeof(decimal));
			dataTable.Columns.Add("CadastralCost", typeof(decimal));
			dataTable.Columns.Add("CadastralQuartal", typeof(string));
			dataTable.Columns.Add("EGRNChangeDate", typeof(string));
			dataTable.Columns.Add("Status", typeof(string));
			dataTable.Columns.Add("Changes", typeof(string));
			dataTable.Columns.Add("MinUpksByCadastralQuartal", typeof(decimal));
			dataTable.Columns.Add("AverageUpksByCadastralQuartal", typeof(decimal));
			dataTable.Columns.Add("MaxUpksByCadastralQuartal", typeof(decimal));
			dataTable.Columns.Add("MinUpksByZone", typeof(decimal));
			dataTable.Columns.Add("AverageUpksByZone", typeof(decimal));
			dataTable.Columns.Add("MaxUpksByZone", typeof(decimal));
			dataTable.Columns.Add("ParticipatingCount", typeof(decimal));
			dataTable.Columns.Add("CountInYear", typeof(decimal));
			dataTable.Columns.Add("CountInDays", typeof(decimal));

			foreach (var item in reportItems)
			{
				dataTable.Rows.Add(item.MarketObjectCharacteristics.CadastralNumber,
					item.MarketObjectCharacteristics.Type.GetEnumDescription(),
					item.MarketObjectCharacteristics.RosreestrSquareValue,
					item.MarketObjectCharacteristics.ObjectNameTypeOfUse,
					item.MarketObjectCharacteristics.Purpose,
					item.MarketObjectCharacteristics.Address,
					item.MarketObjectCharacteristics.Location,
					item.ReportGko2018.EvaluationSubgroup2018,
					(item.ReportGko2018.Upks2018.HasValue
						? Math.Round(item.ReportGko2018.Upks2018.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					item.ReportGko2018.CadastralCost2018,
					item.ReportGko2018.CadastralQuartal2018,
					item.Vyon.TaskType.GetEnumDescription(),
					item.Vyon.EvaluationSubgroup,
					(item.Vyon.Upks.HasValue
						? Math.Round(item.Vyon.Upks.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					item.Vyon.CadastralCost,
					item.Vyon.CadastralQuartal,
					item.Vyon.EGRNChangeDate.HasValue ? item.Vyon.EGRNChangeDate.Value.ToString(DateFormat) : null,
					item.Vyon.Status.GetEnumDescription(),
					item.Vyon.Changes,
					(item.AdditionalInfo.MinUpksByCadastralQuartal.HasValue
						? Math.Round(item.AdditionalInfo.MinUpksByCadastralQuartal.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(item.AdditionalInfo.AverageUpksByCadastralQuartal.HasValue
						? Math.Round(item.AdditionalInfo.AverageUpksByCadastralQuartal.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(item.AdditionalInfo.MaxUpksByCadastralQuartal.HasValue
						? Math.Round(item.AdditionalInfo.MaxUpksByCadastralQuartal.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(item.AdditionalInfo.MinUpksByZone.HasValue
						? Math.Round(item.AdditionalInfo.MinUpksByZone.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(item.AdditionalInfo.AverageUpksByZone.HasValue
						? Math.Round(item.AdditionalInfo.AverageUpksByZone.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					(item.AdditionalInfo.MaxUpksByZone.HasValue
						? Math.Round(item.AdditionalInfo.MaxUpksByZone.Value, PrecisionForDecimalValues)
						: (decimal?)null),
					item.Participating.ParticipatingCount,
					item.Participating.CountInYear,
					item.Participating.CountInDays
					);
			}

			return dataTable;
		}

		public List<ReportItem> GetReportItems(long[] taskIdList)
		{
			var unitMainData = GetUnitMainData(taskIdList);
			var cadastralNumbers = unitMainData.Select(x => x.CadastralNumber).ToList();

			var gko2018Data = GetGko2018Data(cadastralNumbers);
			var zonesInfo = GetZoneCalcInfo(unitMainData);
			var cadastralQuartalsInfo = GetCadastralQuartalCalcInfo(unitMainData);
			///var dayCountInfo = GetCountInfo(cadastralNumbers, KoNoteType.Day);
			//var yearCountInfo = GetCountInfo(cadastralNumbers, KoNoteType.Year);
			//var countInfo = GetCountInfo(cadastralNumbers);

			var result = new List<ReportItem>();
			foreach (var objectCharacteristicsDto in unitMainData)
			{
				var marketObjectCharacteristics = new MarketObjectCharacteristics(objectCharacteristicsDto);
				var reportGko2018 = new ReportGko2018();
				var gko2018UnitData = gko2018Data.FirstOrDefault(x =>
					x.CadastralNumber == objectCharacteristicsDto.CadastralNumber);
				if (gko2018UnitData != null)
				{
					reportGko2018.FillData(gko2018UnitData);
				}

				var vyon = new Vyon(objectCharacteristicsDto);

				var additionalInfo = new AdditionalInfo();
				if (cadastralQuartalsInfo.TryGetValue(objectCharacteristicsDto.CadastralQuartal, out var cadastralQuartalInfo))
				{
					additionalInfo.FillCadastralQuartalInfo(cadastralQuartalInfo);
				}
				if (objectCharacteristicsDto.Zone.HasValue && zonesInfo.TryGetValue(objectCharacteristicsDto.Zone.Value, out var zoneInfo))
				{
					additionalInfo.FillZoneInfo(zoneInfo);
				}

				var participatingCount = OMUnit
					.Where(x => x.CadastralNumber == objectCharacteristicsDto.CadastralNumber).ExecuteCount();
				var participatingDateCount = OMUnit
					.Where(x => x.CadastralNumber == objectCharacteristicsDto.CadastralNumber && x.ParentTask.NoteType_Code == KoNoteType.Day).ExecuteCount();
				var participatingYearCount = OMUnit
					.Where(x => x.CadastralNumber == objectCharacteristicsDto.CadastralNumber && x.ParentTask.NoteType_Code == KoNoteType.Year).ExecuteCount();

				//var participating = new Participating(
				//	countInfo.FirstOrDefault(x => x.CadastralNumber == objectCharacteristicsDto.CadastralNumber)?.Count,
				//	dayCountInfo.FirstOrDefault(x => x.CadastralNumber == objectCharacteristicsDto.CadastralNumber)
				//		?.Count,
				//	yearCountInfo.FirstOrDefault(x => x.CadastralNumber == objectCharacteristicsDto.CadastralNumber)
				//		?.Count);
				var participating = new Participating(
					participatingCount,
					participatingDateCount,
					participatingYearCount);

				var item = new ReportItem();
				item.MarketObjectCharacteristics = marketObjectCharacteristics;
				item.ReportGko2018 = reportGko2018;
				item.Vyon = vyon;
				item.AdditionalInfo = additionalInfo;
				item.Participating = participating;

				result.Add(item);
			}

			return result;
		}

		private List<CountInfo> GetCountInfo(List<string> cadastralNumbers, KoNoteType? taskType = null)
		{
			var query = OMUnit
				.Where(x => cadastralNumbers.Contains(x.CadastralNumber));
			if (taskType.HasValue)
			{
				query.And(x => x.ParentTask.NoteType_Code == taskType);
			}

			return query.GroupBy(x => x.CadastralNumber)
				.ExecuteSelect(x => new
				{
					x.CadastralNumber,
					Count = QSExtensions.Count<OMUnit>(y => 1)
				})
				.Select(x => new CountInfo {CadastralNumber = x.CadastralNumber, Count = x.Count}).ToList();
		}

		private Dictionary<string, CalcInfo> GetCadastralQuartalCalcInfo(List<ObjectMainInfo> data)
		{
			var dataGroupByCadastralQuartal = data
				.GroupBy(x => x.CadastralQuartal);
			var cadastralQuartalInfo = new Dictionary<string, CalcInfo>();
			foreach (var cadastralQuartalDataGroup in dataGroupByCadastralQuartal)
			{
				var calcDtos = cadastralQuartalDataGroup.Select(x => new CalcDto
				{ ObjectValue = x.ObjectUpks, ObjectCost = x.ObjectCost, ObjectSquare = x.UnitSquare }).ToList();
				cadastralQuartalInfo.Add(cadastralQuartalDataGroup.Key,
					new CalcInfo
					{
						MinUpks = StatisticalDataService.GetCalcValue(UpksCalcType.Min, calcDtos),
						MaxUpks = StatisticalDataService.GetCalcValue(UpksCalcType.Max, calcDtos),
						AverageUpks = StatisticalDataService.GetCalcValue(UpksCalcType.Average, calcDtos),
					});
			}

			return cadastralQuartalInfo;
		}

		private Dictionary<long, CalcInfo> GetZoneCalcInfo(List<ObjectMainInfo> data)
		{
			var dataGroupByZone = data.Where(x => x.Zone.HasValue)
				.GroupBy(x => x.Zone);
			var zoneInfo = new Dictionary<long, CalcInfo>();
			foreach (var zoneDataGroup in dataGroupByZone)
			{
				var calcDtos = zoneDataGroup.Select(x => new CalcDto
				{ ObjectValue = x.ObjectUpks, ObjectCost = x.ObjectCost, ObjectSquare = x.UnitSquare }).ToList();
				zoneInfo.Add(zoneDataGroup.Key.Value,
					new CalcInfo
					{
						MinUpks = StatisticalDataService.GetCalcValue(UpksCalcType.Min, calcDtos),
						MaxUpks = StatisticalDataService.GetCalcValue(UpksCalcType.Max, calcDtos),
						AverageUpks = StatisticalDataService.GetCalcValue(UpksCalcType.Average, calcDtos),
					});
			}

			return zoneInfo;
		}

		private List<Gko2018Info> GetGko2018Data(List<string> cadastralNumbers)
		{
			var data = new List<Gko2018Info>();
			if (cadastralNumbers.Count > 0)
			{
				var tourId = OMTour.Where(x => x.Year == 2018).ExecuteFirstOrDefault().Id;
				var query = new QSQuery
				{
					MainRegisterID = OMUnit.GetRegisterId(),
					Condition = new QSConditionGroup
					{
						Type = QSConditionGroupType.And,
						Conditions = new List<QSCondition>
						{
							new QSConditionSimple(OMTask.GetColumn(x => x.TourId), QSConditionType.Equal, tourId),
							new QSConditionSimple(OMTask.GetColumn(x => x.NoteType_Code), QSConditionType.Equal,
								(long) KoNoteType.Initial),
							new QSConditionSimple(OMUnit.GetColumn(x => x.CadastralNumber), QSConditionType.In,
								cadastralNumbers)
						}
					},
					Joins = new List<QSJoin>
					{
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
				query.AddColumn(OMUnit.GetColumn(x => x.CadastralBlock, "CadastralQuartal"));
				query.AddColumn(OMUnit.GetColumn(x => x.Upks, "Upks"));
				query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "CadastralCost"));
				query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "GroupName"));
				var parentGroupSubQuery2018 = new QSQuery(OMGroup.GetRegisterId())
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
				query.AddColumn(parentGroupSubQuery2018, "ParentGroup");
				var table = query.ExecuteQuery();

				if (table.Rows.Count != 0)
				{
					for (var i = 0; i < table.Rows.Count; i++)
					{
						var subGroupName = table.Rows[i]["GroupName"]
							.ParseToStringNullable();
						var parentGroupName = table.Rows[i]["ParentGroup"]
							.ParseToStringNullable();
						var gko2018 = new Gko2018Info
						{
							EvaluationSubgroup2018 = $"{parentGroupName} {subGroupName}",
							Upks2018 = table.Rows[i]["Upks"].ParseToDecimalNullable(),
							CadastralCost2018 = table.Rows[i]["CadastralCost"].ParseToDecimalNullable(),
							CadastralQuartal2018 = table.Rows[i]["CadastralQuartal"].ParseToString(),
							CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						};
						data.Add(gko2018);
					}
				}
			}

			return data;
		}

		private List<ObjectMainInfo> GetUnitMainData(long[] taskIdList)
		{
			var attributesDictionary = new Dictionary<string, RegisterAttribute>();
			attributesDictionary.Add(nameof(ObjectMainInfo.RosreestrSquareValue),
				StatisticalDataService.GetRosreestrSquareAttribute());
			attributesDictionary.Add(nameof(ObjectMainInfo.ObjectName),
				StatisticalDataService.GetRosreestrObjectNameAttribute());
			attributesDictionary.Add(nameof(ObjectMainInfo.TypeOfUse),
				StatisticalDataService.GetRosreestrTypeOfUseByDocumentsAttribute());
			attributesDictionary.Add(nameof(ObjectMainInfo.BuildingPurpose),
				StatisticalDataService.GetRosreestrBuildingPurposeAttribute());
			attributesDictionary.Add(nameof(ObjectMainInfo.PlacementPurpose),
				StatisticalDataService.GetRosreestrPlacementPurposeAttribute());
			attributesDictionary.Add(nameof(ObjectMainInfo.ConstructionPurpose),
				StatisticalDataService.GetRosreestrConstructionPurposeAttribute());
			attributesDictionary.Add(nameof(ObjectMainInfo.Address),
				StatisticalDataService.GetRosreestrAddressAttribute());
			attributesDictionary.Add(nameof(ObjectMainInfo.Location),
				StatisticalDataService.GetRosreestrLocationAttribute());

			var quartalDictionaryJoin = new QSJoin
			{
				RegisterId = OMQuartalDictionary.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.CadastralBlock),
					RightOperand = OMQuartalDictionary.GetColumn(x => x.CadastralQuartal)
				},
				JoinType = QSJoinType.Left
			};
			var gbuObjectJoin = new QSJoin
			{
				RegisterId = OMMainObject.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.ObjectId),
					RightOperand = OMMainObject.GetColumn(x => x.Id)
				},
				JoinType = QSJoinType.Inner
			};
			var taskJoin = new QSJoin
			{
				RegisterId = OMTask.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.TaskId),
					RightOperand = OMTask.GetColumn(x => x.Id)
				},
				JoinType = QSJoinType.Inner
			};
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

			var query = StatisticalDataService.GetQueryForUnitsByTasks(taskIdList,
				additionalJoins: new List<QSJoin> { gbuObjectJoin, quartalDictionaryJoin, taskJoin, groupJoin });

			query.AddColumn(OMMainObject.GetColumn(x => x.Id, "GbuObjectId"));
			query.AddColumn(OMMainObject.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMMainObject.GetColumn(x => x.ObjectType_Code, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralBlock, "CadastralQuartal"));
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.Zone, "Zone"));
			query.AddColumn(OMUnit.GetColumn(x => x.Id, "UnitId"));
			query.AddColumn(OMUnit.GetColumn(x => x.Square, "UnitSquare"));
			query.AddColumn(OMTask.GetColumn(x => x.NoteType_Code, "NoteType"));
			query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "GroupName"));
			var parentGroupSubQuery = new QSQuery(OMGroup.GetRegisterId())
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
			query.AddColumn(parentGroupSubQuery, "ParentGroup");
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "ObjectCost"));
			query.AddColumn(OMUnit.GetColumn(x => x.CreationDate, "CreationDate"));
			query.AddColumn(OMUnit.GetColumn(x => x.Status_Code, "Status"));

			var table = query.ExecuteQuery();
			var data = new List<ObjectMainInfo>();
			if (table.Rows.Count != 0)
			{
				var gbuObjectIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					gbuObjectIds.Add(table.Rows[i]["GbuObjectId"].ParseToLong());
				}

				var gbuAttributes = GbuObjectService.GetAllAttributes(gbuObjectIds,
					attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
					attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
					DateTime.Now.GetEndOfTheDay());

				var unitIds = new List<long>();
				for (var i = 0; i < table.Rows.Count; i++)
				{
					unitIds.Add(table.Rows[i]["UnitId"].ParseToLong());
				}

				var changedFactors = OMUnitChange.Where(x => unitIds.Contains(x.UnitId))
					.Select(x => x.UnitId)
					.Select(x => x.ChangeStatus)
					.Execute()
					.GroupBy(x => x.UnitId)
					.ToDictionary(x => x.Key, x => x.ToList());

				for (var i = 0; i < table.Rows.Count; i++)
				{
					var subGroupName = table.Rows[i]["GroupName"]
						.ParseToStringNullable();
					var parentGroupName = table.Rows[i]["ParentGroup"]
						.ParseToStringNullable();
					var dto = new ObjectMainInfo
					{
						UnitId = table.Rows[i]["UnitId"].ParseToLong(),
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						PropertyType = (PropertyTypes)table.Rows[i]["PropertyType"].ParseToLong(),
						UnitSquare = table.Rows[i]["UnitSquare"].ParseToDecimalNullable(),
						TaskType = (KoNoteType)table.Rows[i]["NoteType"].ParseToLong(),
						Group = $"{parentGroupName} {subGroupName}",
						ObjectUpks = table.Rows[i]["ObjectUpks"].ParseToDecimalNullable(),
						ObjectCost = table.Rows[i]["ObjectCost"].ParseToDecimalNullable(),
						CadastralQuartal = table.Rows[i]["CadastralQuartal"].ParseToString(),
						Zone = table.Rows[i]["Zone"].ParseToLongNullable(),
						UnitCreationDate = table.Rows[i]["CreationDate"].ParseToDateTimeNullable(),
						Status = (KoUnitStatus)table.Rows[i]["Status"].ParseToLong(),
					};

					if (changedFactors.TryGetValue(dto.UnitId, out var unitFactors))
					{
						dto.ChangedFactors = string.Join(", ", unitFactors.Select(x => x.ChangeStatus).ToList());
					}

					foreach (var attribute in gbuAttributes.Where(x => x.ObjectId == table.Rows[i]["GbuObjectId"].ParseToLong()))
					{
						var attributeKeys = attributesDictionary.Where(x => x.Value.Id == attribute.AttributeId)
							.Select(x => x.Key);
						foreach (var key in attributeKeys)
						{
							typeof(ObjectMainInfo).GetProperty(key)
								.SetValue(dto, attribute.GetValueInString());
						}
					}

					data.Add(dto);
				}
			}

			return data;
		}

		#endregion Support Methods
	}
}
