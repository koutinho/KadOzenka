using System.Threading;
using Core.Messages;
using Core.Register.LongProcessManagment;
using KadOzenka.Dal.DataComparing;
using KadOzenka.Dal.DataComparing.DataComparers;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess.DataComparing
{
	public class DataComparingLongProcess : LongProcess
	{
		private ILogger _log;
		private IDataComparer _dataComparer;

		public DataComparingLongProcess(ILogger logger, IDataComparer dataComparer)
		{
			_log = logger;
			_dataComparer = dataComparer;
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description);
			WorkerCommon.SetProgress(processQueue, 0);

			var messageAddresses = JsonConvert.DeserializeObject<MessageAddressersDto>(processType.Parameters);
			_dataComparer.PerformProc(messageAddresses);

			WorkerCommon.SetProgress(processQueue, 100);
			_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);
		}
	}
}
