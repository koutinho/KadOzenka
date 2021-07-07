using KadOzenka.WebClients.ConfigurationManagers;

namespace KadOzenka.WebClients
{
	public class WebClientsConfig
    {
	    public static WebClientsConfig Current => ConfigurationWebClientsSubscriber.GetCurrentWebClientsConfiguration();

        public string BaseUrl { get; set; }

        public string RoleIdForNotification { get; set; }

        public string RgisBaseUrl { get; set; }

    }
}
