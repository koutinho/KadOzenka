using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements;

namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig
{
	public class Flat
	{
		public string CadastralNumberFlatAttributeId { get; set; }
		public long? CadastralNumberFlatAttributeIdValue => long.TryParse(CadastralNumberFlatAttributeId, out var val) ? val : (long?)null;

		public string CadastralNumberOksAttributeId { get; set; }
		public long? CadastralNumberOksAttributeIdValue => long.TryParse(CadastralNumberOksAttributeId, out var val) ? val : (long?)null;

		public ParentOks ParentOks { get; set; }


		public string NameAttributeId { get; set; }
		public long? NameAttributeIdValue => long.TryParse(NameAttributeId, out var val) ? val : (long?)null;

		public string AssignationAssignationCodeAttributeId { get; set; }
		public long? AssignationAssignationCodeAttributeIdValue => long.TryParse(AssignationAssignationCodeAttributeId, out var val) ? val : (long?)null;

		public string AssignationAssignationTypeAttributeId { get; set; }
		public long? AssignationAssignationTypeAttributeIdValue => long.TryParse(AssignationAssignationTypeAttributeId, out var val) ? val : (long?)null;

		public string AssignationSpecialTypeAttributeId { get; set; }
		public long? AssignationSpecialTypeAttributeIdValue => long.TryParse(AssignationSpecialTypeAttributeId, out var val) ? val : (long?)null;

		public string AssignationTotalAssetsAttributeId { get; set; }
		public long? AssignationTotalAssetsAttributeIdValue => long.TryParse(AssignationTotalAssetsAttributeId, out var val) ? val : (long?)null;

		public string AssignationAuxiliaryFlatAttributeId { get; set; }
		public long? AssignationAuxiliaryFlatAttributeIdValue => long.TryParse(AssignationAuxiliaryFlatAttributeId, out var val) ? val : (long?)null;

		public string AreaAttributeId { get; set; }
		public long? AreaAttributeIdValue => long.TryParse(AreaAttributeId, out var val) ? val : (long?)null;

		public string PositionNumberOnPlanAttributeId { get; set; }
		public long? PositionNumberOnPlanAttributeIdValue => long.TryParse(PositionNumberOnPlanAttributeId, out var val) ? val : (long?)null;

		public string PositionDescriptionAttributeId { get; set; }
		public long? PositionDescriptionAttributeIdValue => long.TryParse(PositionDescriptionAttributeId, out var val) ? val : (long?)null;

		public Level[] Levels { get; set; }

		public string ObjectPermittedUsesAttributeId { get; set; }
		public long? ObjectPermittedUsesAttributeIdValue => long.TryParse(ObjectPermittedUsesAttributeId, out var val) ? val : (long?)null;

		public SubBuildingFlat[] SubFlats { get; set; }

		public string UnitedCadastralNumberAttributeId { get; set; }
		public long? UnitedCadastralNumberAttributeIdValue => long.TryParse(UnitedCadastralNumberAttributeId, out var val) ? val : (long?)null;

		public string FacilityCadastralNumberAttributeId { get; set; }
		public long? FacilityCadastralNumberAttributeIdValue => long.TryParse(FacilityCadastralNumberAttributeId, out var val) ? val : (long?)null;

		public string FacilityPurposeAttributeId { get; set; }
		public long? FacilityPurposeAttributeIdValue => long.TryParse(FacilityPurposeAttributeId, out var val) ? val : (long?)null;

		public CulturalHeritage CulturalHeritage { get; set; }
	}
}
