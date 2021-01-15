using System;
using System.Collections.Generic;
using System.IO;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class AdditionalFormsService
	{
		public readonly QueryManager QueryManager;
		private readonly StatisticalDataService _statisticalDataService;
		private readonly string _reportCalculationStatisticsFileName = "AdditionalForms_CalculationStatistics";
		private readonly string _reportMarketFileName = "AdditionalForms_Market";
		private readonly string _reportResultsAnalysisFileName = "AdditionalForms_ResultsAnalysis";

		public AdditionalFormsService(StatisticalDataService statisticalDataService)
		{
			QueryManager =  new QueryManager();
			_statisticalDataService = statisticalDataService;
		}

		public List<ChangesUploadingDto> GetChangesUploadingData(long[] taskIdList)
		{
			var unitChangeJoin = new QSJoin
			{
				RegisterId = OMUnitChange.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.Id),
					RightOperand = OMUnitChange.GetColumn(x => x.UnitId)
				},
				JoinType = QSJoinType.Inner
			};
			var taskJoin = new QSJoin
			{
				RegisterId = OMUnitChange.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.TaskId),
					RightOperand = OMTask.GetColumn(x => x.Id)
				},
				JoinType = QSJoinType.Inner
			};

			var notCadastralQuarterType = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
				QSConditionType.NotEqual, (long)PropertyTypes.CadastralQuartal);
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, new List<QSCondition>{ notCadastralQuarterType }, new List<QSJoin> {unitChangeJoin, taskJoin});
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.StatusRepeatCalc, "StatusRepeatCalc"));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, "CreationDate"));
			query.AddColumn(OMUnitChange.GetColumn(x => x.OldValue, "OldValue"));
			query.AddColumn(OMUnitChange.GetColumn(x => x.NewValue, "NewValue"));
			query.AddColumn(OMUnitChange.GetColumn(x => x.ChangeStatus, "ChangeStatus"));

			var table = query.ExecuteQuery();
			var result = new List<ChangesUploadingDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new ChangesUploadingDto
					{
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						ChangedDate = table.Rows[i]["CreationDate"].ParseToDateTimeNullable(),
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						Status = table.Rows[i]["StatusRepeatCalc"].ParseToString(),
						OldValue = table.Rows[i]["OldValue"].ParseToString(),
						NewValue = table.Rows[i]["NewValue"].ParseToString(),
						Changing = table.Rows[i]["ChangeStatus"].ParseToString(),
					};

					result.Add(dto);
				}
			}

			return result;
		}

		public List<CalculationStatisticsDto> GetCalculationStatisticsData(long[] taskIdList, bool isOks)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportCalculationStatisticsFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", taskIdList), isOks);
			var result = QSQuery.ExecuteSql<CalculationStatisticsDto>(sql);

			return result;
		}

		public List<MarketDataDto> GetMarketData(DateTime? dateFrom, DateTime? dateTo, long typeOfUseCodeAttributeId, long oksGroupAttributeId, long typeOfUseAttributeId)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportMarketFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents,
				dateFrom.HasValue ? CrossDBSQL.ToDate(dateFrom.Value) : "null",
				dateTo.HasValue ? CrossDBSQL.ToDate(dateTo.Value) : "null",
				RegisterCache.GetAttributeData(typeOfUseCodeAttributeId).Id,
				RegisterCache.GetAttributeData(oksGroupAttributeId).Id,
				RegisterCache.GetAttributeData(typeOfUseAttributeId).Id);
			var result = QueryManager.ExecuteSql<MarketDataDto>(sql);

			return result;
		}

		public List<ResultsAnalysisDto> GetResultsAnalysisData(long[] taskIdList)
		{
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportResultsAnalysisFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, string.Join(", ", taskIdList));
			var result = QSQuery.ExecuteSql<ResultsAnalysisDto>(sql);

			return result;
		}
	}
}
