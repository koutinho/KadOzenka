namespace CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class Encumbrance
	{
		public string NameAttributeId { get; set; }
		public long? NameAttributeIdValue => long.TryParse(NameAttributeId, out var val) ? val : (long?)null;

		public string TypeAttributeId { get; set; }
		public long? TypeAttributeIdValue => long.TryParse(TypeAttributeId, out var val) ? val : (long?)null;

		public string RegistrationNumberAttributeId { get; set; }
		public long? RegistrationNumberAttributeIdValue => long.TryParse(RegistrationNumberAttributeId, out var val) ? val : (long?)null;

		public string RegistrationDateAttributeId { get; set; }
		public long? RegistrationDateAttributeIdValue => long.TryParse(RegistrationDateAttributeId, out var val) ? val : (long?)null;

		public Document Document { get; set; }
	}

	public class EncumbranceZu : Encumbrance
	{
		public string AccountNumberAttributeId { get; set; }
		public long? AccountNumberAttributeIdValue => long.TryParse(AccountNumberAttributeId, out var val) ? val : (long?)null;

		public string CadastralNumberRestrictionAttributeId { get; set; }
		public long? CadastralNumberRestrictionAttributeIdValue => long.TryParse(CadastralNumberRestrictionAttributeId, out var val) ? val : (long?)null;
	}
}