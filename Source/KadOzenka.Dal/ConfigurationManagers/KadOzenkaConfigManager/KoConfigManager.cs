using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models;

namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager
{
	public class KoConfigManager
	{
		public MapTilesConfig MapTilesConfig { get; set; }

		public DataComparingConfig DataComparingConfig { get; set; }

		public ModelingProcessConfig ModelingProcessConfig { get; set; }
	}
}