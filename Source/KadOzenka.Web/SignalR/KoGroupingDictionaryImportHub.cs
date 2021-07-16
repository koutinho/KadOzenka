using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace KadOzenka.Web.SignalR
{
	public class KoGroupingDictionaryImportHub : Hub
	{
		private readonly IMemoryCache _cache;
		private readonly KoGroupingDictionaryImportListenerService _listenerService;

		public KoGroupingDictionaryImportHub(IMemoryCache cache, KoGroupingDictionaryImportListenerService listenerService)
		{
			_cache = cache;
			_listenerService = listenerService;
		}

		public async Task SendMessage()
		{
			if (!_cache.TryGetValue("CurrentDictionariesList", out string response))
			{
				if (!_listenerService.IsListening)
				{
					_listenerService.ListenForAlarmNotifications();
				}

				await Clients.All.SendAsync("ReceiveMessage", _listenerService.CurrentDictionariesList());
			}
			else
			{
				await Clients.All.SendAsync("ReceiveMessage", _cache.Get("CurrentDictionariesList").ToString());
			}
		}
	}
}
