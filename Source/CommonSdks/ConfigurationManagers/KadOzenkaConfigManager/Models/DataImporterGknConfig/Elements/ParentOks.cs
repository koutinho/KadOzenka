namespace CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class ParentOks
	{
		public string CadastralNumberOksAttributeId { get; set; }
		public long? CadastralNumberOksAttributeIdValue => long.TryParse(CadastralNumberOksAttributeId, out var val) ? val : (long?)null;

		public string ObjectTypeAttributeId { get; set; }
		public long? ObjectTypeAttributeIdValue => long.TryParse(ObjectTypeAttributeId, out var val) ? val : (long?)null;

		public string AssignationBuildingAttributeId { get; set; }
		public long? AssignationBuildingAttributeIdValue => long.TryParse(AssignationBuildingAttributeId, out var val) ? val : (long?)null;

		public string AssignationNameAttributeId { get; set; }
		public long? AssignationNameAttributeIdValue => long.TryParse(AssignationNameAttributeId, out var val) ? val : (long?)null;

		public string WallMaterialAttributeId { get; set; }
		public long? WallMaterialAttributeIdValue => long.TryParse(WallMaterialAttributeId, out var val) ? val : (long?)null;

		public string ExploitationCharYearBuiltAttributeId { get; set; }
		public long? ExploitationCharYearBuiltAttributeIdValue => long.TryParse(ExploitationCharYearBuiltAttributeId, out var val) ? val : (long?)null;

		public string ExploitationCharYearUsedAttributeId { get; set; }
		public long? ExploitationCharYearUsedAttributeIdValue => long.TryParse(ExploitationCharYearUsedAttributeId, out var val) ? val : (long?)null;

		public string FloorCountAttributeId { get; set; }
		public long? FloorCountAttributeIdValue => long.TryParse(FloorCountAttributeId, out var val) ? val : (long?)null;

		public string FloorUndergroundCountAttributeId { get; set; }
		public long? FloorUndergroundCountAttributeIdValue => long.TryParse(FloorUndergroundCountAttributeId, out var val) ? val : (long?)null;
	}
}
