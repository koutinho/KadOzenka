using KadOzenka.WebClients.ConfigurationManagers;

namespace KadOzenka.WebClients
{
	public class ReonServiceConfig
    {
	    public static ReonServiceConfig Current => ConfigurationReonSubscriber.GetCurrentReonConfiguration();

        public string BaseUrl { get; set; }

        public string RoleIdForNotification { get; set; }
    }
}
