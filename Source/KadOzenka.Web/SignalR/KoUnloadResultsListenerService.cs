using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using KadOzenka.Web.Models.Tour;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Npgsql;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Web.SignalR
{
	public class KoUnloadResultsListenerService
	{
		private IHubContext<KoUnloadResultsProgressHub> _hubContext;
		private long _koUnloadResultQueueChangedEventsCount = 0;
		private readonly TimeSpan _updatesSendingInterval = TimeSpan.FromMilliseconds(3000);
		private static object _syncRoot = new object();
		private ILogger _log = Log.ForContext<KoUnloadResultsListenerService>();

		public bool IsListening { get; private set; }

		static string ConnectionString() => ConfigurationManager.ConnectionStrings["Main"]?.ConnectionString;

		public KoUnloadResultsListenerService(IHubContext<KoUnloadResultsProgressHub> hubContext)
		{
			_hubContext = hubContext;
			InitiateUpdatesSending();
			ListenForAlarmNotifications();
		}

		public void InitiateUpdatesSending()
		{
			_log.Verbose("Запуск процесса оповещения об обновлениях выгрузок");
			var cancelSource = new CancellationTokenSource();
			Task.Run(() =>
			{
				while (true)
				{
					Thread.Sleep(_updatesSendingInterval);
					if (_koUnloadResultQueueChangedEventsCount > 0)
					{
						Console.WriteLine(DateTime.Now);
						var queues = GetUnloadSettingsQueues();
						_hubContext.Clients.All.SendAsync("dataWereUpdated", new { updateTime = DateTime.Now, data = queues });
						lock (_syncRoot)
						{
							_koUnloadResultQueueChangedEventsCount--;
						}
					}
				}
			}, cancelSource.Token);
		}

		public void ListenForAlarmNotifications()
		{
			_log.Verbose("Запуск процесса прослушивания оповещений выгрузок");
			IsListening = true;
			var cancelSource = new CancellationTokenSource();
			Task.Run(() =>
			{
				try
				{
					NpgsqlConnection conn = new NpgsqlConnection(ConnectionString());
					conn.Open();
					var listenCommand = conn.CreateCommand();
					listenCommand.CommandText = $"listen notify_ko_unload_result_proc_updating;";
					listenCommand.ExecuteNonQuery();
					conn.Notification += PostgresNotificationReceived;
					
					while (true)
					{
						conn.Wait();
					}
				}
				catch (Exception exception)
				{
					IsListening = false;
				}
				
			}, cancelSource.Token);
		}

		private void PostgresNotificationReceived(object sender, NpgsqlNotificationEventArgs e)
		{
			lock (_syncRoot)
			{
				// второе оповещение нужно для обновления данных после того как процесс завершится
				_koUnloadResultQueueChangedEventsCount = 2;
			}
		}

		public List<UnloadSettingsQueueModel> GetUnloadSettingsQueues()
		{
			var result = new List<UnloadSettingsQueueModel>();
			var startDate = DateTime.Now.Date;
			var endDate = DateTime.Now.GetEndOfTheDay();
			var omUnloadResultQueues = OMUnloadResultQueue
				.Where(x => x.DateFinished == null || x.DateFinished >= startDate && x.DateFinished <= endDate)
				.SelectAll()
				.Execute().OrderBy(x => x.DateCreated).ToList();
			_log.ForContext("omUnloadResultQueuesCount",omUnloadResultQueues.Count)
				.Verbose("Получение данных очередей выгрузок");

			// Множественные запросы к базе с опросом статуса
			foreach (var unloadResultQueue in omUnloadResultQueues)
			{
				var omQueue = OMQueue.Where(x => x.ObjectId == unloadResultQueue.Id).ExecuteFirstOrDefault();
				result.Add(
					new UnloadSettingsQueueModel
					{
						Id = unloadResultQueue.Id,
						DateCreated = unloadResultQueue.DateCreated,
						DateStarted = unloadResultQueue.DateStarted,
						DateFinished = unloadResultQueue.DateFinished,
						Status = unloadResultQueue.Status,
						UnloadCurrentCount = unloadResultQueue.UnloadCurrentCount,
						UnloadTotalCount = unloadResultQueue.UnloadTotalCount,
						CurrentUnloadType = unloadResultQueue.CurrentUnloadType,
						CurrentUnloadProgress = unloadResultQueue.CurrentUnloadProgress,
						UnloadTypes =
							(JsonConvert.DeserializeObject<List<KoUnloadResultType>>(unloadResultQueue
								.UnloadTypesMapping))
							.Select(y => y.GetEnumDescription()).ToList(),
						LongProcessUrl = $"/RegistersView/CoreLongProcessQueue?Transition=1&97500100={omQueue?.Id}",
						ExportFile = unloadResultQueue.FinalArchiveExportId.HasValue
							? new UnloadSettingsQueueExportFileModel {FileId = unloadResultQueue.FinalArchiveExportId.Value, FileName = "Результаты оценки",
								DownloadUrl = $"/DataExport/DownloadExportResult?exportId={unloadResultQueue.FinalArchiveExportId}"}
							: null
					});
			}

			return result;
		}
	}
}
