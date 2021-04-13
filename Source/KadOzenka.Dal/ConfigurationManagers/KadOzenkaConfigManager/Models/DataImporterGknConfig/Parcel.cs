using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements;

namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig
{
	public class Parcel
	{
		public string LocationInBoundsAttributeId { get; set; }
		public long? LocationInBoundsAttributeIdValue => long.TryParse(LocationInBoundsAttributeId, out var val) ? val : (long?)null;

		public string LocationPlacedAttributeId { get; set; }
		public long? LocationPlacedAttributeIdValue => long.TryParse(LocationPlacedAttributeId, out var val) ? val : (long?)null;

		public string LocationElaborationReferenceMarkAttributeId { get; set; }
		public long? LocationElaborationReferenceMarkAttributeIdValue => long.TryParse(LocationElaborationReferenceMarkAttributeId, out var val) ? val : (long?)null;

		public string LocationElaborationDistanceAttributeId { get; set; }
		public long? LocationElaborationDistanceAttributeIdValue => long.TryParse(LocationElaborationDistanceAttributeId, out var val) ? val : (long?)null;

		public string LocationElaborationDirectionAttributeId { get; set; }
		public long? LocationElaborationDirectionAttributeIdValue => long.TryParse(LocationElaborationDirectionAttributeId, out var val) ? val : (long?)null;


		public string NameAttributeId { get; set; }
		public long? NameAttributeIdValue => long.TryParse(NameAttributeId, out var val) ? val : (long?)null;

		public string InnerCadastralNumbersAttributeId { get; set; }
		public long? InnerCadastralNumbersAttributeIdValue => long.TryParse(InnerCadastralNumbersAttributeId, out var val) ? val : (long?)null;

		public string AreaAttributeId { get; set; }
		public long? AreaAttributeIdValue => long.TryParse(AreaAttributeId, out var val) ? val : (long?)null;

		public string AreaInaccuracyAttributeId { get; set; }
		public long? AreaInaccuracyAttributeIdValue => long.TryParse(AreaInaccuracyAttributeId, out var val) ? val : (long?)null;

		public string CategoryAttributeId { get; set; }
		public long? CategoryAttributeIdValue => long.TryParse(CategoryAttributeId, out var val) ? val : (long?)null;

		public string UtilizationUtilizationAttributeId { get; set; }
		public long? UtilizationUtilizationAttributeIdValue => long.TryParse(UtilizationUtilizationAttributeId, out var val) ? val : (long?)null;

		public string UtilizationByDocAttributeId { get; set; }
		public long? UtilizationByDocAttributeIdValue => long.TryParse(UtilizationByDocAttributeId, out var val) ? val : (long?)null;

		public string UtilizationLandUseAttributeId { get; set; }
		public long? UtilizationLandUseAttributeIdValue => long.TryParse(UtilizationLandUseAttributeId, out var val) ? val : (long?)null;

		public string UtilizationPermittedUseTextAttributeId { get; set; }
		public long? UtilizationPermittedUseTextAttributeIdValue => long.TryParse(UtilizationPermittedUseTextAttributeId, out var val) ? val : (long?)null;

		public NaturalObject[] NaturalObjects { get; set; }

		public SubParcel[] SubParcels { get; set; }

		public string FacilityCadastralNumberAttributeId { get; set; }
		public long? FacilityCadastralNumberAttributeIdValue => long.TryParse(FacilityCadastralNumberAttributeId, out var val) ? val : (long?)null;

		public string FacilityPurposeAttributeId { get; set; }
		public long? FacilityPurposeAttributeIdValue => long.TryParse(FacilityPurposeAttributeId, out var val) ? val : (long?)null;

		public ZoneAndTerritory[] ZonesAndTerritories { get; set; }
		public SupervisionEvent[] GovernmentLandSupervision { get; set; }

		public string SurveyingProjectNumAttributeId { get; set; }
		public long? SurveyingProjectNumAttributeIdValue => long.TryParse(SurveyingProjectNumAttributeId, out var val) ? val : (long?)null;

		public Document SurveyingProjectDecisionRequisites { get; set; }

		public HiredHouse HiredHouse { get; set; }

		public string LimitedCirculationAttributeId { get; set; }
		public long? LimitedCirculationAttributeIdValue => long.TryParse(LimitedCirculationAttributeId, out var val) ? val : (long?)null;
	}
}