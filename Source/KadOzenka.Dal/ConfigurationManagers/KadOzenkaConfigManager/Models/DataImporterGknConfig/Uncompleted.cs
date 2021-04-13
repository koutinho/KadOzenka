using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements;

namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig
{
	public class Uncompleted
	{
		public string ParentCadastralNumbersAttributeId { get; set; }
		public long? ParentCadastralNumbersAttributeIdValue => long.TryParse(ParentCadastralNumbersAttributeId, out var val) ? val : (long?)null;

		public string AssignationNameAttributeId { get; set; }
		public long? AssignationNameAttributeIdValue => long.TryParse(AssignationNameAttributeId, out var val) ? val : (long?)null;

		public string KeyParametersAttributeId { get; set; }
		public long? KeyParametersAttributeIdValue => long.TryParse(KeyParametersAttributeId, out var val) ? val : (long?)null;

		public KeyParameter[] KeyParameters { get; set; }

		public string DegreeReadinessAttributeId { get; set; }
		public long? DegreeReadinessAttributeIdValue => long.TryParse(DegreeReadinessAttributeId, out var val) ? val : (long?)null;

		public string FacilityCadastralNumberAttributeId { get; set; }
		public long? FacilityCadastralNumberAttributeIdValue => long.TryParse(FacilityCadastralNumberAttributeId, out var val) ? val : (long?)null;

		public string FacilityPurposeAttributeId { get; set; }
		public long? FacilityPurposeAttributeIdValue => long.TryParse(FacilityPurposeAttributeId, out var val) ? val : (long?)null;
	}
}
