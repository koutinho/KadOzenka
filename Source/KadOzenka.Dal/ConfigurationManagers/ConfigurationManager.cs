using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager;
using KadOzenka.WebClients;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager.Interface;

namespace KadOzenka.Dal.ConfigurationManagers
{
	public class ConfigurationManager
	{
		public static KoConfigManager KoConfig => ConfigurationSubscriber.GetCurrentKoConfiguration();

		public static ReonServiceConfig ReonConfig => ReonServiceConfig.Current;

		public static ICoreConfig Core => CoreManager.CoreCurrent;

	}

	public class CoreManager : CoreConfigManager
	{
		public static ICoreConfig CoreCurrent => Current;
	}
}