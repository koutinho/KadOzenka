using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace KadOzenka.Web.SignalR
{
	public class OutliersCheckingHub : Hub
	{
		private readonly IMemoryCache _cache;
		private readonly OutliersCheckingListenerService _listenerService;

		public OutliersCheckingHub(IMemoryCache cache, OutliersCheckingListenerService listenerService)
		{
			_cache = cache;
			_listenerService = listenerService;
		}

		public async Task SendMessage()
		{
			if (!_cache.TryGetValue("OutliersCheckingCurrentState", out string response))
			{
				if (!_listenerService.IsListening)
				{
					_listenerService.ListenForAlarmNotifications();
				}

				await Clients.All.SendAsync("ReceiveMessage", _listenerService.CurrentProcessesList());
			}
			else
			{
				await Clients.All.SendAsync("ReceiveMessage", _cache.Get("OutliersCheckingCurrentState").ToString());
			}
		}
	}
}
