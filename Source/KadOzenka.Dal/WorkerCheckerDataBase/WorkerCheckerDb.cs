using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using KadOzenka.Dal.WorkerCheckerDataBase.Interface;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace KadOzenka.Dal.WorkerCheckerDataBase
{
	public class WorkerCheckerDb
	{
		private readonly ILogger _log = Log.ForContext<WorkerCheckerDb>();

		private const int RUN_SECOND = 30000; // перезапуск в случае ошибки
		private const int SLEEP = 60000; // мин

		public void StartChecker()
		{
			List<IWorkerChecker> checkers = GetCheckers();

			if (!checkers.Any())
			{
				return;
			}

			Task task = new Task(() =>
				{
					try
					{
						_log.Debug("Запуск воркер чекера");

						while (true)
						{

							foreach (var checker in checkers)
							{
								checker.Check();
							}

							Thread.Sleep(SLEEP);
						}

					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						ErrorManager.LogError(e);
						_log.ForContext("Error", e).Error("Ошибка воркер чекера, перезапуск произойдет автоматически через {sec} сек.", RUN_SECOND/1000);
					}
					finally
					{
						Thread.Sleep(RUN_SECOND);
						_log.Warning("Автозапуск воркер чекера");
						new WorkerCheckerDb().StartChecker();
						GC.Collect();
					}

				});

			task.Start();

		}

		#region support methods

		private List<IWorkerChecker> GetCheckers()
		{
			try
			{
				var res = new List<IWorkerChecker>();

				var type = typeof(IWorkerChecker);
				var types = AppDomain.CurrentDomain.GetAssemblies()
					.SelectMany(s => s.GetTypes())
					.Where(p => type.IsAssignableFrom(p) && !p.IsInterface).ToList();

				_log.Debug("Найдено {count} объектов для воркер чекера", types.Count);

				if (types.Any())
				{
					foreach (var cType in types)
					{
						_log.Debug("Найден чекер {name}", cType.Name);

						var instance = Activator.CreateInstance(cType) as IWorkerChecker;
						res.Add(instance);
					}
				}

				return res;
			}
			catch (Exception e)
			{
				_log.ForContext("Error", e, true).Error("Ошибка при получении чекеров");
				Console.WriteLine(e);
				return new List<IWorkerChecker>();
			}
		}

		#endregion
	}

	public static class StartChecker
	{
		public static IWebHostBuilder StartWorkerChecker(this IWebHostBuilder builder)
		{
			new WorkerCheckerDb().StartChecker();
			return builder;
		}
	}
}
