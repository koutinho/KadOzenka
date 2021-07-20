using CommonSdks.ConfigurationManagers.KadOzenkaConfigManager;
using CommonSdks.ConfigurationManagers.WebClients;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager;
using Platform.Main.ConfigurationManagers.CoreConfigurationManager.Interface;

namespace CommonSdks.ConfigurationManagers
{
	public class ConfigurationManager
	{
		public static KoConfigManager KoConfig => ConfigurationSubscriber.GetCurrentKoConfiguration();

		public static WebClientsConfig WebClientsConfig => WebClientsConfig.Current;

		public static ICoreConfig Core => CoreManager.CoreCurrent;

	}

	public class CoreManager : CoreConfigManager
	{
		public static ICoreConfig CoreCurrent => Current;
	}
}