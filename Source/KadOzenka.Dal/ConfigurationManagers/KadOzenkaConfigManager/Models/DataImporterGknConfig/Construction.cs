using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements;

namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig
{
	public class Construction
	{
		public string ParentCadastralNumbersAttributeId { get; set; }
		public long? ParentCadastralNumbersAttributeIdValue => long.TryParse(ParentCadastralNumbersAttributeId, out var val) ? val : (long?)null;

		public string NameAttributeId { get; set; }
		public long? NameAttributeIdValue => long.TryParse(NameAttributeId, out var val) ? val : (long?)null;

		public string AssignationNameAttributeId { get; set; }
		public long? AssignationNameAttributeIdValue => long.TryParse(AssignationNameAttributeId, out var val) ? val : (long?)null;

		public string ExploitationCharYearBuiltAttributeId { get; set; }
		public long? ExploitationCharYearBuiltAttributeIdValue => long.TryParse(ExploitationCharYearBuiltAttributeId, out var val) ? val : (long?)null;

		public string ExploitationCharYearUsedAttributeId { get; set; }
		public long? ExploitationCharYearUsedAttributeIdValue => long.TryParse(ExploitationCharYearUsedAttributeId, out var val) ? val : (long?)null;

		public string FloorCountAttributeId { get; set; }
		public long? FloorCountAttributeIdValue => long.TryParse(FloorCountAttributeId, out var val) ? val : (long?)null;

		public string FloorUndergroundCountAttributeId { get; set; }
		public long? FloorUndergroundCountAttributeIdValue => long.TryParse(FloorUndergroundCountAttributeId, out var val) ? val : (long?)null;

		public string KeyParametersAttributeId { get; set; }
		public long? KeyParametersAttributeIdValue => long.TryParse(KeyParametersAttributeId, out var val) ? val : (long?)null;

		public KeyParameter[] KeyParameters { get; set; }

		public string ObjectPermittedUsesAttributeId { get; set; }
		public long? ObjectPermittedUsesAttributeIdValue => long.TryParse(ObjectPermittedUsesAttributeId, out var val) ? val : (long?)null;

		public SubConstruction[] SubConstructions { get; set; }


		public string FlatsCadastralNumbersAttributeId { get; set; }
		public long? FlatsCadastralNumbersAttributeIdValue => long.TryParse(FlatsCadastralNumbersAttributeId, out var val) ? val : (long?)null;

		public string CarParkingSpacesCadastralNumbersAttributeId { get; set; }
		public long? CarParkingSpacesCadastralNumbersAttributeIdValue => long.TryParse(CarParkingSpacesCadastralNumbersAttributeId, out var val) ? val : (long?)null;

		public string UnitedCadastralNumberAttributeId { get; set; }
		public long? UnitedCadastralNumberAttributeIdValue => long.TryParse(UnitedCadastralNumberAttributeId, out var val) ? val : (long?)null;

		public string FacilityCadastralNumberAttributeId { get; set; }
		public long? FacilityCadastralNumberAttributeIdValue => long.TryParse(FacilityCadastralNumberAttributeId, out var val) ? val : (long?)null;

		public string FacilityPurposeAttributeId { get; set; }
		public long? FacilityPurposeAttributeIdValue => long.TryParse(FacilityPurposeAttributeId, out var val) ? val : (long?)null;

		public CulturalHeritage CulturalHeritage { get; set; }
	}
}
