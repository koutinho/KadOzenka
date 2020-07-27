namespace KadOzenka.WebClients
{
    public class ReonServiceConfig
    {
        public static ReonServiceConfig Current => Core.ConfigParam.Configuration.GetParam<ReonServiceConfig>("ReonService");

        public string BaseUrl { get; set; }

        public string RoleIdForNotification { get; set; }
    }
}
