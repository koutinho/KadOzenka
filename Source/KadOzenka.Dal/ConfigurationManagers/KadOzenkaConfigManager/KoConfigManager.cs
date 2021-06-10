using System.Text.Json.Serialization;
using System.Xml;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;

namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager
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