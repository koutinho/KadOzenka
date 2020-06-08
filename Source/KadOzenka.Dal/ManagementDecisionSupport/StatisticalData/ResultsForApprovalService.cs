using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class ResultsForApprovalService
	{
		private readonly StatisticalDataService _statisticalDataService;

		public ResultsForApprovalService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
		}

		public List<ResultsForApprovalDto> GetResultsForApprovalData(long[] taskIdList)
		{
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList);
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "CadastralCost"));

			var table = query.ExecuteQuery();

			var result = new List<ResultsForApprovalDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new ResultsForApprovalDto
					{
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						CadastralCost = table.Rows[i]["CadastralCost"].ParseToDecimalNullable(),

					};
					result.Add(dto);
				}
			}

			return result;
		}

		public List<ResultsForApprovalUpksAverageDto> GetResultsForApprovalUpksAverageData(long[] taskIdList,
			StatisticDataAreaDivisionType areaDivisionType, bool isOks)
		{
			var conditions = new List<QSCondition>();
			if (isOks)
			{
				conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
					QSConditionType.NotEqual, (long) PropertyTypes.Stead));
				conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
					QSConditionType.NotEqual, (long) PropertyTypes.None));
			}
			else
			{
				conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.Equal,
					(long) PropertyTypes.Stead));
			}

			var quartalDictionaryJoin = new QSJoin
			{
				RegisterId = OMQuartalDictionary.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.CadastralBlock),
					RightOperand = OMQuartalDictionary.GetColumn(x => x.CadastralQuartal)
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
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, conditions,
				new List<QSJoin> {quartalDictionaryJoin, groupJoin});

			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.CadastralQuartal, "CadastralQuartal"));
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.District_Code, "District_Code"));
			query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "GroupName"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));

			var table = query.ExecuteQuery();
			var data = new List<ResultsForApprovalObjectUpksDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new ResultsForApprovalObjectUpksDto
					{
						GroupName = string.IsNullOrEmpty(table.Rows[i]["GroupName"].ParseToStringNullable())
							? "Без группы"
							: table.Rows[i]["GroupName"].ParseToStringNullable(),
						HasGroup = !string.IsNullOrEmpty(table.Rows[i]["GroupName"].ParseToStringNullable()),
						ObjectUpks = table.Rows[i]["ObjectUpks"].ParseToDecimalNullable(),
						//TODO: ObjectWeigth MUST BE CLARIFIED
						ObjectWeigth = 1
					};
					switch (areaDivisionType)
					{
						case StatisticDataAreaDivisionType.Districts:
							dto.Name = ((Hunteds) table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle();
							break;
						case StatisticDataAreaDivisionType.RegionNumbers:
							dto.Name = _statisticalDataService.GetRegionNumberByCadastralQuarter(
								table.Rows[i]["CadastralQuartal"].ParseToString());
							break;
						case StatisticDataAreaDivisionType.Quarters:
							dto.Name = table.Rows[i]["CadastralQuartal"].ParseToString();
							break;
						default:
							throw new NotSupportedException();
					}

					data.Add(dto);
				}
			}

			var dataByGroupNameGrouped = data.GroupBy(x => new {x.GroupName, x.HasGroup})
				.OrderBy(x => x.Key.HasGroup);

			var result = new List<ResultsForApprovalUpksAverageDto>();
			foreach (var groupNameGroup in dataByGroupNameGrouped)
			{
				var groupValues = groupNameGroup.ToList();

				var dtoList = groupValues.GroupBy(x => x.Name).Select(x =>
					new ResultsForApprovalUpksAverageDto
					{
						GroupName = groupNameGroup.Key.GroupName,
						Name = x.Key,
						UpksAverageWeight =
							_statisticalDataService.GetUpksCalcValue(UpksCalcType.AverageWeight, x.ToList())
					}).ToList();
				result.AddRange(dtoList);

				var totalByGroupDto = new ResultsForApprovalUpksAverageDto
				{
					GroupName = groupNameGroup.Key.GroupName,
					Name = "Итого",
					UpksAverageWeight =
						_statisticalDataService.GetUpksCalcValue(UpksCalcType.AverageWeight, groupValues)
				};
				result.Add(totalByGroupDto);
			}

			return result;
		}
	}
}
