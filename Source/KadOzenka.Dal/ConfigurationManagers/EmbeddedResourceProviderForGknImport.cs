using Core.ConfigParam;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;
using Newtonsoft.Json;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models;

namespace KadOzenka.Dal.ConfigurationManagers
{
	public class EmbeddedResourceProviderForGknImport
	{
		private const string _basePathPart = "EmbeddedResource.GknImport.";

		private static DataImporterGknConfig _dataImporterGknConfig;
		public static DataImporterGknConfig DataImporterGknConfig
		{
			get
			{
				if (_dataImporterGknConfig != null) return _dataImporterGknConfig;
				string config = Configuration.GetParamEmbeddedResource<string>($"{_basePathPart}gknImportSettings.json");
				if (config != null)
				{
					return _dataImporterGknConfig = JsonConvert.DeserializeObject<DataImporterGknConfig>(config);
				}
				return _dataImporterGknConfig = new DataImporterGknConfig();

			}
		}


		private static DictionariesForTaskDocument _dictionariesForTaskDocument;
		public static DictionariesForTaskDocument DictionariesForTaskDocument
		{
			get
			{
				if (_dictionariesForTaskDocument != null)
					return _dictionariesForTaskDocument;

				_dictionariesForTaskDocument = new DictionariesForTaskDocument();
				
				return _dictionariesForTaskDocument;
			}
		}
	}
}