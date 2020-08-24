using System.Collections.Generic;
using System.IO;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class MinMaxAverageUPKSByCadastralQuartersService
	{
		private readonly StatisticalDataService _statisticalDataService;
		private readonly string _sqlFileName = "MinMaxAverageUPKSByCadastralQuarters";

        public MinMaxAverageUPKSByCadastralQuartersService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
		}

        public List<MinMaxAverageUPKSByCadastralQuartersDto> GetMinMaxAverageUPKS(long[] taskList)
        {
	        string contents;
	        using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_sqlFileName)))
	        {
		        contents = sr.ReadToEnd();
            }

	        var result = QSQuery.ExecuteSql<MinMaxAverageUPKSByCadastralQuartersDto>(string.Format(contents, string.Join(", ", taskList)));

	        return result;
        }
    }
}
