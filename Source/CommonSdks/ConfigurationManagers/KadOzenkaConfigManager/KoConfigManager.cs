using System.Text.Json.Serialization;
using CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models;
using CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;

namespace CommonSdks.ConfigurationManagers.KadOzenkaConfigManager
{
	public class KoConfigManager
	{
		public MapTilesConfig MapTilesConfig { get; set; }

		public DataComparingConfig DataComparingConfig { get; set; }

		public ModelingProcessConfig ModelingProcessConfig { get; set; }

		/// <summary>
		/// Конфигурация для импорта документа к Заданию на оценку
		/// </summary>
		public ImportDocumentForTaskConfig ImportDocumentForTaskConfig { get; set; }

		//Внедренные ресурсы 
		[JsonIgnore]
		public DataImporterGknConfig DataImporterGknConfig => EmbeddedResourceProviderForGknImport.DataImporterGknConfig;

		//Внедренные ресурсы 
		[JsonIgnore]
		public DictionariesForTaskDocument DictionariesForTaskDocument => EmbeddedResourceProviderForGknImport.DictionariesForTaskDocument;
	}
}