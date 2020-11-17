using Microsoft.AspNetCore.SignalR;

namespace KadOzenka.Web.SignalR
{
	public class EsHubService
	{
		private readonly IHubContext<EsHub> _hubContext;
		public EsHubService(IHubContext<EsHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async void SendCalculateProgress(decimal process, string connectionId)
		{
			await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveProgress", process);
		}

	}
}