using System.Threading;
using Core.Register.LongProcessManagment;
using KadOzenka.Dal.DataComparing;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess.DataComparing
{
	public class TaskChangesDataComparingLongProcess : LongProcess
	{
		private ILogger _log = Log.ForContext<TaskChangesDataComparingLongProcess>();

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			_log.Information("Начато выполнение фонового процесса: {ProcessType}", processType.Description);
			WorkerCommon.SetProgress(processQueue, 0);

			var comparer = new TaskChangesDataComparer();
			var filePairs = comparer.FindFilesPairs();
			_log.Debug($"Найдено {filePairs.Count} пар файлов");
			foreach (var filePair in filePairs)
			{
				_log.Debug("Выполняется сравнение файлов '{RsmFileName}' и '{PkkoFileName}'", filePair.Item1, filePair.Item2);
				comparer.Compare(filePair.Item1, filePair.Item2);
			}

			WorkerCommon.SetProgress(processQueue, 100);
			_log.Information("Завершение фонового процесса: {ProcessType}", processType.Description);
		}

	}
}
