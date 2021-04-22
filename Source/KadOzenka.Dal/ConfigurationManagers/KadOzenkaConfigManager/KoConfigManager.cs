using System.Text.Json.Serialization;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;

namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager
{
	public class KoConfigManager
	{
		public MapTilesConfig MapTilesConfig { get; set; }

		public DataComparingConfig DataComparingConfig { get; set; }

		public ModelingProcessConfig ModelingProcessConfig { get; set; }

		//Внедренные ресурсы 
		[JsonIgnore]
		public DataImporterGknConfig DataImporterGknConfig => EmbeddedResourceProvider.DataImporterGknConfig;
	}
}