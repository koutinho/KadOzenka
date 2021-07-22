using System.Collections.Generic;
using System.IO;
using CommonSdks;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class ResultsForApprovalService
	{
		public QueryManager QueryManager;
		private readonly StatisticalDataService _statisticalDataService;
		private readonly GbuCodRegisterService _gbuCodRegisterService;
		private readonly string _reportResultsForApprovalUpksAverageSqlFileName = "ResultsForApprovalUpksAverage";

		public ResultsForApprovalService(StatisticalDataService statisticalDataService, GbuCodRegisterService gbuCodRegisterService)
		{
			QueryManager = new QueryManager();
			_statisticalDataService = statisticalDataService;
			_gbuCodRegisterService = gbuCodRegisterService;
		}

		public List<ResultsForApprovalDto> GetResultsForApprovalData(long[] taskIdList)
		{
			var notCadastralQuarterType = new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
				QSConditionType.NotEqual, (long) PropertyTypes.CadastralQuartal);
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, new List<QSCondition>{ notCadastralQuarterType });
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "CadastralCost"));

			var table = QueryManager.ExecuteQueryToDataTable(query);

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
			string contents;
			using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_reportResultsForApprovalUpksAverageSqlFileName)))
			{
				contents = sr.ReadToEnd();
			}

			var sql = string.Format(contents, areaDivisionType, string.Join(", ", taskIdList), isOks, _gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id);
			var result = QueryManager.ExecuteSql<ResultsForApprovalUpksAverageDto>(sql);
			return result;
		}
	}
}
