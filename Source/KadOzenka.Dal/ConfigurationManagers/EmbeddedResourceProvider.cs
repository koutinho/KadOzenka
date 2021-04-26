using Core.ConfigParam;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;
using Newtonsoft.Json;

namespace KadOzenka.Dal.ConfigurationManagers
{
	public class EmbeddedResourceProvider
	{
		private static DataImporterGknConfig _dataImporterGknConfig;
		public static DataImporterGknConfig DataImporterGknConfig
		{
			get
			{
				if (_dataImporterGknConfig != null) return _dataImporterGknConfig;
				string config = Configuration.GetParamEmbeddedResource<string>("EmbeddedResource.GknImport.gknImportSettings.json");
				if (config != null)
				{
					return _dataImporterGknConfig = JsonConvert.DeserializeObject<DataImporterGknConfig>(config);
				}
				return _dataImporterGknConfig = new DataImporterGknConfig();

			}
		}
	}
}