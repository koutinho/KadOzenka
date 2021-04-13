namespace KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig
{
	public class General
	{
		public string ObjectTypeAttributeId { get; set; }
		public long? ObjectTypeAttributeIdValue => long.TryParse(ObjectTypeAttributeId, out var val) ? val : (long?)null;

		public string CadastralNumberAttributeId { get; set; }
		public long? CadastralNumberAttributeIdValue => long.TryParse(CadastralNumberAttributeId, out var val) ? val : (long?)null;

		public string DateCreatedAttributeId { get; set; }
		public long? DateCreatedAttributeIdValue => long.TryParse(DateCreatedAttributeId, out var val) ? val : (long?)null;

		public string CadastralBlockAttributeId { get; set; }
		public long? CadastralBlockAttributeIdValue => long.TryParse(CadastralBlockAttributeId, out var val) ? val : (long?)null;

		public string CadastralCostValueAttributeId { get; set; }
		public long? CadastralCostValueAttributeIdValue => long.TryParse(CadastralCostValueAttributeId, out var val) ? val : (long?)null;

		public string CadastralCostDateValuationAttributeId { get; set; }
		public long? CadastralCostDateValuationAttributeIdValue => long.TryParse(CadastralCostDateValuationAttributeId, out var val) ? val : (long?)null;

		public string CadastralCostDateEnteringAttributeId { get; set; }
		public long? CadastralCostDateEnteringAttributeIdValue => long.TryParse(CadastralCostDateEnteringAttributeId, out var val) ? val : (long?)null;

		public string CadastralCostDateApprovalAttributeId { get; set; }
		public long? CadastralCostDateApprovalAttributeIdValue => long.TryParse(CadastralCostDateApprovalAttributeId, out var val) ? val : (long?)null;

		public string CadastralCostDocNumberAttributeId { get; set; }
		public long? CadastralCostDocNumberAttributeIdValue => long.TryParse(CadastralCostDocNumberAttributeId, out var val) ? val : (long?)null;

		public string CadastralCostDocDateAttributeId { get; set; }
		public long? CadastralCostDocDateAttributeIdValue => long.TryParse(CadastralCostDocDateAttributeId, out var val) ? val : (long?)null;

		public string CadastralCostApplicationDateAttributeId { get; set; }
		public long? CadastralCostApplicationDateAttributeIdValue => long.TryParse(CadastralCostApplicationDateAttributeId, out var val) ? val : (long?)null;

		public string CadastralCostRevisalStatementDateAttributeId { get; set; }
		public long? CadastralCostRevisalStatementDateAttributeIdValue => long.TryParse(CadastralCostRevisalStatementDateAttributeId, out var val) ? val : (long?)null;

		public string CadastralCostApplicationLastDateAttributeId { get; set; }
		public long? CadastralCostApplicationLastDateAttributeIdValue => long.TryParse(CadastralCostApplicationLastDateAttributeId, out var val) ? val : (long?)null;

		public string CadastralCostDocNameAttributeId { get; set; }
		public long? CadastralCostDocNameAttributeIdValue => long.TryParse(CadastralCostDocNameAttributeId, out var val) ? val : (long?)null;

		public string LocationAddressInOneStringAttributeId { get; set; }
		public long? LocationAddressInOneStringAttributeIdValue => long.TryParse(LocationAddressInOneStringAttributeId, out var val) ? val : (long?)null;

		public string LocationFiasAttributeId { get; set; }
		public long? LocationFiasAttributeIdValue => long.TryParse(LocationFiasAttributeId, out var val) ? val : (long?)null;

		public string LocationOkatoAttributeId { get; set; }
		public long? LocationOkatoAttributeIdValue => long.TryParse(LocationOkatoAttributeId, out var val) ? val : (long?)null;

		public string LocationKladrAttributeId { get; set; }
		public long? LocationKladrAttributeIdValue => long.TryParse(LocationKladrAttributeId, out var val) ? val : (long?)null;

		public string LocationOktmoAttributeId { get; set; }
		public long? LocationOktmoAttributeIdValue => long.TryParse(LocationOktmoAttributeId, out var val) ? val : (long?)null;

		public string LocationPostalCodeAttributeId { get; set; }
		public long? LocationPostalCodeAttributeIdValue => long.TryParse(LocationPostalCodeAttributeId, out var val) ? val : (long?)null;

		public string LocationRussianFederationAttributeId { get; set; }
		public long? LocationRussianFederationAttributeIdValue => long.TryParse(LocationRussianFederationAttributeId, out var val) ? val : (long?)null;

		public string LocationRegionAttributeId { get; set; }
		public long? LocationRegionAttributeIdValue => long.TryParse(LocationRegionAttributeId, out var val) ? val : (long?)null;

		public string LocationDistrictAttributeId { get; set; }
		public long? LocationDistrictAttributeIdValue => long.TryParse(LocationDistrictAttributeId, out var val) ? val : (long?)null;

		public string LocationCityAttributeId { get; set; }
		public long? LocationCityAttributeIdValue => long.TryParse(LocationCityAttributeId, out var val) ? val : (long?)null;

		public string LocationUrbanDistrictAttributeId { get; set; }
		public long? LocationUrbanDistrictAttributeIdValue => long.TryParse(LocationUrbanDistrictAttributeId, out var val) ? val : (long?)null;

		public string LocationSovietVillageAttributeId { get; set; }
		public long? LocationSovietVillageAttributeIdValue => long.TryParse(LocationSovietVillageAttributeId, out var val) ? val : (long?)null;

		public string LocationLocalityAttributeId { get; set; }
		public long? LocationLocalityAttributeIdValue => long.TryParse(LocationLocalityAttributeId, out var val) ? val : (long?)null;

		public string LocationPlanningElementAttributeId { get; set; }
		public long? LocationPlanningElementAttributeIdValue => long.TryParse(LocationPlanningElementAttributeId, out var val) ? val : (long?)null;

		public string LocationStreetAttributeId { get; set; }
		public long? LocationStreetAttributeIdValue => long.TryParse(LocationStreetAttributeId, out var val) ? val : (long?)null;

		public string LocationLevel1AttributeId { get; set; }
		public long? LocationLevel1AttributeIdValue => long.TryParse(LocationLevel1AttributeId, out var val) ? val : (long?)null;

		public string LocationLevel2AttributeId { get; set; }
		public long? LocationLevel2AttributeIdValue => long.TryParse(LocationLevel2AttributeId, out var val) ? val : (long?)null;

		public string LocationLevel3AttributeId { get; set; }
		public long? LocationLevel3AttributeIdValue => long.TryParse(LocationLevel3AttributeId, out var val) ? val : (long?)null;

		public string LocationApartmentAttributeId { get; set; }
		public long? LocationApartmentAttributeIdValue => long.TryParse(LocationApartmentAttributeId, out var val) ? val : (long?)null;

		public string LocationOtherAttributeId { get; set; }
		public long? LocationOtherAttributeIdValue => long.TryParse(LocationOtherAttributeId, out var val) ? val : (long?)null;

		public string LocationNoteAttributeId { get; set; }
		public long? LocationNoteAttributeIdValue => long.TryParse(LocationNoteAttributeId, out var val) ? val : (long?)null;

		public string LocationAddressOrLocationAttributeId { get; set; }
		public long? LocationAddressOrLocationAttributeIdValue => long.TryParse(LocationAddressOrLocationAttributeId, out var val) ? val : (long?)null;

	}
}