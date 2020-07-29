using System.Collections.Generic;
using KadOzenka.Web.Models.Tour;
using Microsoft.AspNetCore.SignalR;

namespace KadOzenka.Web.SignalR
{
	public class KoUnloadResultsProgressHub : Hub
	{
		private readonly KoUnloadResultsListenerService _koUnloadResultsListenerService;

		public KoUnloadResultsProgressHub(IHubContext<KoUnloadResultsProgressHub> hubContext, KoUnloadResultsListenerService koUnloadResultsListenerService)
		{
			_koUnloadResultsListenerService = koUnloadResultsListenerService;
			if (!_koUnloadResultsListenerService.IsListening)
			{
				_koUnloadResultsListenerService.ListenForAlarmNotifications();
			}
		}

		public IEnumerable<UnloadSettingsQueueModel> ReadData()
		{
			var queues = _koUnloadResultsListenerService.GetUnloadSettingsQueues();
			return queues;
		}
	}
}
