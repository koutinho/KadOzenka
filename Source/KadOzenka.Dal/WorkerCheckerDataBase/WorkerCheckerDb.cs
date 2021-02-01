using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using KadOzenka.Dal.WorkerCheckerDataBase.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace KadOzenka.Dal.WorkerCheckerDataBase
{
	public class WorkerCheckerDb
	{
		private readonly ILogger _log = Log.ForContext<WorkerCheckerDb>();
		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
		private readonly int _runSecond = 30000; // по умолчанию 30 сек
		private readonly int _sleep = 60000; //по умолчанию 60 сек


		public WorkerCheckerDb(int? runSecond = null, int? sleep =  null)
		{
			_runSecond = runSecond ?? _runSecond;
			_sleep = sleep ?? _sleep;
		}

		public void StartChecker()
		{
			List<IWorkerChecker> checkers = GetCheckers();

			if (!checkers.Any())
			{
				_log.Warning("Воркер чекер не запущен, так как не найдены классы для проверки");
				return;
			}

			Task task = new Task(() =>
				{
					try
					{

						while (true)
						{
							if (_cancellationTokenSource.IsCancellationRequested)
							{
								_log.Warning("Запрошен токен отмены воркер чекера");
								break;
							}

							foreach (var checker in checkers)
							{
								checker.Check();
							}

							Thread.Sleep(_sleep);
						}

					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						ErrorManager.LogError(e);
						CheckerRunner.CleanChecker();
						_log.ForContext("Error", e).Error("Ошибка воркер чекера, перезапуск произойдет автоматически через {sec} сек.", _runSecond / 1000);
						Thread.Sleep(_runSecond);
						CheckerRunner.SetAndRunChecker(_runSecond, _sleep);
					}

				}, _cancellationTokenSource.Token);

			task.Start();

		}

		#region support methods

		public void CancelToken()
		{
			_cancellationTokenSource.Cancel();
		}
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

	/// <summary>
	/// Класс логики по запуску воркер чекера
	/// </summary>
	public static class CheckerRunner
	{
		private static WorkerCheckerDb _checker;

		public static IWebHostBuilder StartWorkerChecker(this IWebHostBuilder builder, IConfigurationRoot config)
		{
			Log.Logger.Warning("Проверяем необходимо ли запускать воркер чекер");
			bool useWorkerChecker = config.GetSection("WorkerChecker:useWorkerChecker").Value == "True";

			CheckTimeProperty(config, out int? runChecker, out int? sleep);

			if (useWorkerChecker)
			{
				SetAndRunChecker(runChecker, sleep);
			}
			else
			{
				Log.Logger.ForContext("config", config, true).Warning("Воркер чекер не запущен т.к флаг useWorkerChecker = false или не нейден");
			}

			ChangeToken.OnChange(config.GetReloadToken,  InvokeChanged, config);

			return builder;
		}

		/// <summary>
		/// Метод запускает воркер чекер
		/// </summary>
		/// <param name="runSecond"> время автозапуска после ошибки</param>
		/// <param name="sleep"> время через которое происходит проверка сущностей для чекера</param>
		public static void SetAndRunChecker(int? runSecond, int? sleep)
		{
			if (_checker == null)
			{
				Log.Logger.ForContext("TimeToRunAfterError", runSecond)
					.ForContext("TimeToSleepCheck", sleep)
					.Warning("Запуск воркер чекера");
				_checker = new WorkerCheckerDb(runSecond, sleep);
				_checker.StartChecker();
			}
			else
			{
				Log.Logger.Warning("Воркер чекер не запущен, т.к существует предыдущий экземпляр");
			}
		}

		public static void CleanChecker()
		{
			if (_checker != null)
			{
				_checker.CancelToken();
				_checker = null;
				Log.Logger.Warning("Воркер чекер отключен");
			}
		}

		private static void InvokeChanged(IConfigurationRoot config)
		{
			Log.Logger.ForContext("config.Providers", config.Providers.ToList(), true).Debug("Сработала подписка на измение кофигурационного файла");

			CheckTimeProperty(config, out int? runChecker, out int? sleep);

			if (config.GetSection("WorkerChecker:useWorkerChecker").Value == "True")
			{
				SetAndRunChecker(runChecker, sleep);
			}
			else
			{
				CleanChecker();
				GC.Collect();
			}
		}


		private static void CheckTimeProperty(IConfigurationRoot config, out int? runChecker, out int? sleep)
		{
			runChecker = null;
			sleep = null;

			if (int.TryParse(config.GetSection("WorkerChecker:timeToRun").Value, out int run))
			{
				runChecker = run;
			}

			if (int.TryParse(config.GetSection("WorkerChecker:timeSleepToCheck").Value, out int timeSleep))
			{
				sleep = timeSleep;
			}
		}
	}

}
