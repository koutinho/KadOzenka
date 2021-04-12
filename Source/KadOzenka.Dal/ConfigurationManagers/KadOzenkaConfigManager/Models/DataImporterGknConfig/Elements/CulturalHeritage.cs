namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class CulturalHeritage
	{
		public string EgroknRegNumAttributeId { get; set; }
		public long? EgroknRegNumAttributeIdValue => long.TryParse(EgroknRegNumAttributeId, out var val) ? val : (long?)null;

		public string EgroknObjCulturalAttributeId { get; set; }
		public long? EgroknObjCulturalAttributeIdValue => long.TryParse(EgroknObjCulturalAttributeId, out var val) ? val : (long?)null;

		public string EgroknNameCulturalAttributeId { get; set; }
		public long? EgroknNameCulturalAttributeIdValue => long.TryParse(EgroknNameCulturalAttributeId, out var val) ? val : (long?)null;

		public string RequirementsEnsureAttributeId { get; set; }
		public long? RequirementsEnsureAttributeIdValue => long.TryParse(RequirementsEnsureAttributeId, out var val) ? val : (long?)null;

		public Document Document { get; set; }
	}
}
