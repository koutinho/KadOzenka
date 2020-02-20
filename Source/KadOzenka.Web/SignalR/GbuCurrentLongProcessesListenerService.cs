using System;
using System.Configuration;
using System.Linq;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Models.GbuProgressBar;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Npgsql;

namespace KadOzenka.Web.SignalR
{
	public class GbuCurrentLongProcessesListenerService
	{
		static string ConnectionString() => ConfigurationManager.ConnectionStrings["Main"]?.ConnectionString;

		private readonly IHubContext<GbuLongProcessesProgressBarHub> _hubContext;
		private readonly IMemoryCache _cache;
		private readonly GbuLongProcessesService _service;

		public GbuCurrentLongProcessesListenerService(IHubContext<GbuLongProcessesProgressBarHub> hubContext, IMemoryCache cache, GbuLongProcessesService service)
		{
			_hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
			_cache = cache ?? throw new ArgumentNullException(nameof(cache));
			_service = service ?? throw new ArgumentNullException(nameof(service));
		}

		public void ListenForAlarmNotifications()
		{
			NpgsqlConnection conn = new NpgsqlConnection(ConnectionString());
			conn.StateChange += conn_StateChange;
			conn.Open();
			var listenCommand = conn.CreateCommand();
			listenCommand.CommandText = $"listen notify_gbu_long_proc_updating;";
			listenCommand.ExecuteNonQuery();
			conn.Notification += PostgresNotificationReceived;
			_hubContext.Clients.All.SendAsync(this.CurrentProcessesList());
			while (true)
			{
				conn.Wait();
			}
		}

		private void PostgresNotificationReceived(object sender, NpgsqlNotificationEventArgs e)
		{
			_hubContext.Clients.All.SendAsync("ReceiveMessage", this.CurrentProcessesList());
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

		private void conn_StateChange(object sender, System.Data.StateChangeEventArgs e)
		{

			//_hubContext.Clients.All.SendAsync("Current State: " + e.CurrentState.ToString() + " Original State: " + e.OriginalState.ToString(), "connection state changed");
		}
	}
}
