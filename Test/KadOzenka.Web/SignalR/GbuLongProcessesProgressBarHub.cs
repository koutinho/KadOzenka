using System.Threading.Tasks;
using KadOzenka.Dal.GbuObject;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace KadOzenka.Web.SignalR
{
	public class GbuLongProcessesProgressBarHub : Hub
	{
		private readonly IMemoryCache _cache;
		private readonly IHubContext<GbuLongProcessesProgressBarHub> _hubContext;
		private readonly GbuLongProcessesService _service;

		public GbuLongProcessesProgressBarHub(IMemoryCache cache, IHubContext<GbuLongProcessesProgressBarHub> hubContext, GbuLongProcessesService service)
		{
			_cache = cache;
			_hubContext = hubContext;
			_service = service;
		}

		public async Task SendMessage()
		{
			if (!_cache.TryGetValue("CurrentProcessesList", out string response))
			{
				var progressController = new GbuCurrentLongProcessesListenerService(_hubContext, _cache, _service);
				progressController.ListenForAlarmNotifications();
				var currentProcessesList = progressController.CurrentProcessesList();
				_cache.Set("CurrentProcessesList", currentProcessesList);
				await Clients.All.SendAsync("ReceiveMessage", _cache.Get("CurrentProcessesList").ToString());
			}
			else
			{
				await Clients.All.SendAsync("ReceiveMessage", _cache.Get("CurrentProcessesList").ToString());
			}
		}
	}
}
