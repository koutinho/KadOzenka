using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements;

namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig
{
	public class CarPlace
	{
		public string CadastralNumberOksAttributeId { get; set; }
		public long? CadastralNumberOksAttributeIdValue => long.TryParse(CadastralNumberOksAttributeId, out var val) ? val : (long?)null;

		public ParentOks ParentOks { get; set; }

		public string AreaAttributeId { get; set; }
		public long? AreaAttributeIdValue => long.TryParse(AreaAttributeId, out var val) ? val : (long?)null;

		public Level PositionInObject { get; set; }

		public string UnitedCadastralNumberAttributeId { get; set; }
		public long? UnitedCadastralNumberAttributeIdValue => long.TryParse(UnitedCadastralNumberAttributeId, out var val) ? val : (long?)null;

		public string FacilityCadastralNumberAttributeId { get; set; }
		public long? FacilityCadastralNumberAttributeIdValue => long.TryParse(FacilityCadastralNumberAttributeId, out var val) ? val : (long?)null;

		public string FacilityPurposeAttributeId { get; set; }
		public long? FacilityPurposeAttributeIdValue => long.TryParse(FacilityPurposeAttributeId, out var val) ? val : (long?)null;

	}
}
