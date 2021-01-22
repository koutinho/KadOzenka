using System;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Serilog;

namespace KadOzenka.Dal.WorkerCheckerDataBase
{
	public class WorkerCheckerDb
	{
		private readonly ILogger _log = Log.ForContext<WorkerCheckerDb>();

		private int _runSecond = 30;
		public void StartChecker()
		{
			Task task = new Task(() =>
			{
				try
				{
					_log.Debug("Запуск воркер чекера");

					while (true)
					{

						Thread.Sleep(1000);
					}

				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					ErrorManager.LogError(e);
					_log.ForContext("Error", e).Error("Ошибка воркер чекера, перезапуск произойдет автоматически через {sec} сек.", _runSecond);
				}
				finally
				{
					Thread.Sleep(_runSecond);
					_log.Warning("Автозапуск воркер чекера");
					new WorkerCheckerDb().StartChecker();
					GC.Collect();
				}

			});

			task.Start();

		}
	}
}
