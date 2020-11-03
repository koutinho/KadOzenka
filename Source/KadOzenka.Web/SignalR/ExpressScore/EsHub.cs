using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace KadOzenka.Web.SignalR
{
	public class EsHub: Hub
	{

		public async void SendMessage()
		{
			await Clients.Caller.SendAsync("Connection", Context.ConnectionId);
		}
		
	}
}