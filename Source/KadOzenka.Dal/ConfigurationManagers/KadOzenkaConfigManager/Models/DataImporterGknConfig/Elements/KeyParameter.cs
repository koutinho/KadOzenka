namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class KeyParameter
	{
		public string KeyParameterAttributeId { get; set; }
		public long? KeyParameterAttributeIdValue => long.TryParse(KeyParameterAttributeId, out var val) ? val : (long?)null;

		public string KeyParameterValueAttributeId { get; set; }
		public long? KeyParameterValueAttributeIdValue => long.TryParse(KeyParameterValueAttributeId, out var val) ? val : (long?)null;
	}
}
