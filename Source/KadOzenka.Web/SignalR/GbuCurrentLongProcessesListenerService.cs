using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.GbuProgressBar;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Npgsql;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;

namespace KadOzenka.Web.SignalR
{
	public class GbuCurrentLongProcessesListenerService
	{
		private readonly IHubContext<GbuLongProcessesProgressBarHub> _hubContext;
		private readonly IMemoryCache _cache;
		private readonly GbuLongProcessesService _service;

		public bool IsListening { get; private set; }

		public GbuCurrentLongProcessesListenerService(IHubContext<GbuLongProcessesProgressBarHub> hubContext, IMemoryCache cache, GbuLongProcessesService service)
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
					listenCommand.CommandText = "listen notify_gbu_long_proc_updating;";
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
					_cache.Remove("CurrentProcessesList");
				}

			}, cancelSource.Token);
		}

		private void PostgresNotificationReceived(object sender, NpgsqlNotificationEventArgs e)
		{
			_hubContext.Clients.All.SendAsync("ReceiveMessage", CurrentProcessesList());
		}

		public string CurrentProcessesList()
		{
			var processesList = _service.GetCurrentLongProcessesList()
				.Select(LongProcessViewModel.ToModel).ToList();
			string serializedObject = null;
			try
			{
				serializedObject =
					JsonConvert.SerializeObject(processesList,
						Formatting.Indented);
			}
			catch (Exception)
			{
				// ignored
			}

			_cache.Set("CurrentProcessesList", serializedObject);

			return _cache.Get("CurrentProcessesList").ToString();
		}
	}
}
