using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KadOzenka.Dal.Tours;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Npgsql;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;

namespace KadOzenka.Web.SignalR
{
	public class KoGroupingDictionaryImportListenerService
	{
		private readonly IHubContext<KoGroupingDictionaryImportHub> _hubContext;
		private readonly IMemoryCache _cache;
		private readonly GroupingDictionaryService _service;

		public bool IsListening { get; private set; }

		public KoGroupingDictionaryImportListenerService(IHubContext<KoGroupingDictionaryImportHub> hubContext, IMemoryCache cache, GroupingDictionaryService service)
		{
			_hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
			_cache = cache ?? throw new ArgumentNullException(nameof(cache));
			_service = service ?? throw new ArgumentNullException(nameof(service));
		}

		public void ListenForAlarmNotifications()
		{
			IsListening = true;
			var cancelSource = new CancellationTokenSource();
			Task.Run(() =>
			{
				try
				{
					var connectionString = CoreConfigManager.GetConnectionStringSetting()?.ConnectionString;
					var conn = new NpgsqlConnection(connectionString);
					conn.Open();
					var listenCommand = conn.CreateCommand();
					listenCommand.CommandText = "listen notify_ko_grouping_dictionaries_updating;";
					listenCommand.ExecuteNonQuery();
					conn.Notification += PostgresNotificationReceived;
					while (true)
					{
						conn.Wait();
					}
				}
				catch (Exception)
				{
					IsListening = false;
					_cache.Remove("CurrentDictionariesList");
				}

			}, cancelSource.Token);
		}

		private void PostgresNotificationReceived(object sender, NpgsqlNotificationEventArgs e)
		{
			_hubContext.Clients.All.SendAsync("ReceiveMessage", CurrentDictionariesList());
		}

		public string CurrentDictionariesList()
		{

			var dictionaries = _service.GetDictionaries().Select(x => new SelectListItem
			{
				Text = x.Name,
				Value = x.Id.ToString()
			}).ToList();

			dictionaries.Insert(0, new SelectListItem("", ""));

			string serializedObject = null;
			try
			{
				serializedObject =
					JsonConvert.SerializeObject(dictionaries, Formatting.Indented);
			}
			catch (Exception)
			{
				// ignored
			}

			_cache.Set("CurrentDictionariesList", serializedObject);

			return _cache.Get("CurrentDictionariesList").ToString();
		}
	}
}
