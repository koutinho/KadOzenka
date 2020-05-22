namespace KadOzenka.WebClients
{
    public class ReonServiceConfig
    {
        public static ReonServiceConfig Current => Core.ConfigParam.Configuration.GetParam<ReonServiceConfig>("ReonServiceConfig");

        public string BaseUrl { get; set; }
    }
}
