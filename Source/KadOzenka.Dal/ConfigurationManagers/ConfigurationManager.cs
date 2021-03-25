using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager;
using KadOzenka.WebClients;

namespace KadOzenka.Dal.ConfigurationManagers
{
	public partial class ConfigurationManager
	{
		public static KoConfigManager KoConfig => ConfigurationSubscriber.GetCurrentKoConfiguration();

		public static ReonServiceConfig ReonConfig => ReonServiceConfig.Current;

	}
}