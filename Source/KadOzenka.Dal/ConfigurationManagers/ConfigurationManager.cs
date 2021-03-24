using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager;

namespace KadOzenka.Dal.ConfigurationManagers
{
	public class ConfigurationManager
	{
		public static KoConfigManager KoConfig => ConfigurationSubscriber.GetCurrentKoConfiguration();
	}
}