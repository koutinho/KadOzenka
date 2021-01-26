using System.Collections.Generic;
using System.IO;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.Registers.GbuRegistersServices;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class MinMaxAverageUPKSByCadastralQuartersService
	{
		public QueryManager QueryManager;
		private readonly StatisticalDataService _statisticalDataService;
		private readonly GbuCodRegisterService _gbuCodRegisterService;
		private readonly string _sqlFileName = "MinMaxAverageUPKSByCadastralQuarters";

        public MinMaxAverageUPKSByCadastralQuartersService(StatisticalDataService statisticalDataService, GbuCodRegisterService gbuCodRegisterService)
		{
			QueryManager = new QueryManager();
			_statisticalDataService = statisticalDataService;
			_gbuCodRegisterService = gbuCodRegisterService;
		}

        public List<MinMaxAverageUPKSByCadastralQuartersDto> GetMinMaxAverageUPKS(long[] taskList)
        {
	        string contents;
	        using (var sr = new StreamReader(_statisticalDataService.GetSqlQueryFileStream(_sqlFileName)))
	        {
		        contents = sr.ReadToEnd();
            }

	        var result = QueryManager.ExecuteSql<MinMaxAverageUPKSByCadastralQuartersDto>(string.Format(contents, string.Join(", ", taskList), _gbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id));

	        return result;
        }
    }
}
