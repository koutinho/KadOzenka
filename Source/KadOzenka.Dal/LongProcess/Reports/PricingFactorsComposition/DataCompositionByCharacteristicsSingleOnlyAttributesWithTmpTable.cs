using Core.Register.LongProcessManagment;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using Serilog;
using System.Threading;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	//5 sec
	public class DataCompositionByCharacteristicsSingleOnlyAttributesWithTmpTable : LongProcess
	{
		private static readonly ILogger Logger = Log.ForContext<DataCompositionByCharacteristicsSingleOnlyAttributesWithTmpTable>();

		
		public static long AddProcessToQueue()
		{
			return LongProcessManager.AddTaskToQueue(nameof(DataCompositionByCharacteristicsSingleOnlyAttributesWithTmpTable));
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Logger.Debug("Старт фонового процесса: {Description}.", processType.Description);

			using (Logger.TimeOperation("Полное время работы процесса"))
			{
				var sql = $@"update data_composition_by_characteristics_by_tables cache_table
							set attributes = ARRAY(SELECT DISTINCT UNNEST(attributes || newAttributes))
							from (select object_id, attributes as newAttributes from data_composition_by_characteristics_tmp) as source
							where cache_table.object_id = source.object_id";
				
				var command = DBMngr.Main.GetSqlStringCommand(sql);
				DBMngr.Main.ExecuteNonQuery(command);
			}

			Logger.Debug("Финиш фонового процесса: {Description}.", processType.Description);
		}
	}
}
