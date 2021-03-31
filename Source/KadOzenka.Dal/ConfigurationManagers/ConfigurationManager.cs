using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager;
using KadOzenka.WebClients;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;

namespace KadOzenka.Dal.ConfigurationManagers
{
	public class ConfigurationManager
	{
		public static KoConfigManager KoConfig => ConfigurationSubscriber.GetCurrentKoConfiguration();

		public static ReonServiceConfig ReonConfig => ReonServiceConfig.Current;

		public static CoreConfigManager Core => CoreConfigManager.Current;

	}
}