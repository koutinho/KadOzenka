using System.Collections.Generic;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using ObjectModel.KO;

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
	}
}
