//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Core.Shared.Extensions;
//using KadOzenka.Dal.OutliersChecking;
//using KadOzenka.Web.Models.MarketObject;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Extensions.Caching.Memory;
//using Newtonsoft.Json;
//using Npgsql;
//using ObjectModel.Directory.Common;
//using ObjectModel.Market;
//using Platform.Main.ConfigurationManagers.CoreConfigurationManager;

//namespace KadOzenka.Web.SignalR
//{
//	public class OutliersCheckingListenerService
//	{
//		private readonly IHubContext<OutliersCheckingHub> _hubContext;
//		private readonly IMemoryCache _cache;

//		public bool IsListening { get; private set; }

//		public OutliersCheckingListenerService(IHubContext<OutliersCheckingHub> hubContext, IMemoryCache cache)
//		{
//			_hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
//			_cache = cache ?? throw new ArgumentNullException(nameof(cache));
//		}

//		public void ListenForAlarmNotifications()
//		{
//			IsListening = true;
//			var cancelSource = new CancellationTokenSource();
//			Task.Run(() =>
//			{
//				try
//				{
//					var connectionString = CoreConfigManager.GetConnectionStringSetting()?.ConnectionString;
//					NpgsqlConnection conn = new NpgsqlConnection(connectionString);
//					conn.Open();
//					var listenCommand = conn.CreateCommand();
//					listenCommand.CommandText = "listen notify_market_outliers_checking_updating;";
//					listenCommand.ExecuteNonQuery();
//					conn.Notification += PostgresNotificationReceived;
//					while (true)
//					{
//						conn.Wait();
//					}
//				}
//				catch (Exception)
//				{
//					IsListening = false;
//					_cache.Remove("CurrentState");
//				}

//			}, cancelSource.Token);
//		}

//		private void PostgresNotificationReceived(object sender, NpgsqlNotificationEventArgs e)
//		{
//			_hubContext.Clients.All.SendAsync("ReceiveMessage", CurrentProcessesList());
//		}

//		public string CurrentProcessesList()
//		{
//			var historyProcess = OMOutliersCheckingHistory.Where(x =>
//					x.Status_Code != ImportStatus.Added && x.Status_Code != ImportStatus.Running)
//				.OrderByDescending(x => x.DateCreated)
//				.SelectAll().ExecuteFirstOrDefault();
//			var currentProcess = OMOutliersCheckingHistory.Where(x =>
//					x.Status_Code == ImportStatus.Added || x.Status_Code == ImportStatus.Running)
//				.OrderByDescending(x => x.DateCreated)
//				.SelectAll().ExecuteFirstOrDefault();

//			var model = new OutliersCheckingModel
//			{
//				HasHistory = historyProcess != null,
//				LastDateCreated = historyProcess?.DateCreated,
//				LastDateStarted = historyProcess?.DateStarted,
//				LastDateFinished = historyProcess?.DateFinished,
//				LastStatus = historyProcess?.Status,
//				LastMarketSegment = historyProcess?.MarketSegment,
//				LastPropertyTypes = historyProcess != null ? GetPropertyTypesString(historyProcess) : null,
//				LastTotalObjectsCount = historyProcess?.TotalObjectsCount,
//				LastExcludedObjectsCount = historyProcess?.ExcludedObjectsCount,
//				LastReportDownloadLink = historyProcess != null && historyProcess.ExportId.HasValue 
//					? $"/DataExport/DownloadExportResult?exportId={historyProcess?.ExportId.Value}"
//					: null,
//				HasCurrentAddedProcess = currentProcess != null && currentProcess.Status_Code == ImportStatus.Added,
//				HasCurrentRunningProcess = currentProcess != null && currentProcess.Status_Code == ImportStatus.Running,
//				CurrentProgress = currentProcess != null 
//					? GetCurrentProgress(currentProcess)
//					: 0
//			};

//			string serializedObject = null;
//			try
//			{
//				serializedObject =
//					JsonConvert.SerializeObject(model,
//						Formatting.Indented);
//			}
//			catch (Exception)
//			{
//				// ignored
//			}

//			_cache.Set("OutliersCheckingCurrentState", serializedObject);

//			return _cache.Get("OutliersCheckingCurrentState").ToString();
//		}

//		private static long GetCurrentProgress(OMOutliersCheckingHistory currentProcess)
//		{
//			if (!currentProcess.TotalObjectsCount.HasValue || !currentProcess.CurrentHandledObjectsCount.HasValue)
//				return 0;

//			if (currentProcess.TotalObjectsCount == 0)
//				return 0;

//			return (long)Math.Round(((double)currentProcess.CurrentHandledObjectsCount.Value / currentProcess.TotalObjectsCount.Value) * 100);
//		}

//		private static string GetPropertyTypesString(OMOutliersCheckingHistory history)
//		{
//			return !string.IsNullOrEmpty(history.PropertyTypesMapping) 
//				? string.Join(", ", JsonConvert.DeserializeObject<List<ObjectPropertyTypeDivision>>(history.PropertyTypesMapping).Select(x => x.GetEnumDescription()))
//				: null;
//		}
//	}
//}
