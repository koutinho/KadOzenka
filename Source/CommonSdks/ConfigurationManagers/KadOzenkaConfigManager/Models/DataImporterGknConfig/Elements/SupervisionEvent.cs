namespace CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig.Elements
{
	public class SupervisionEvent
	{
		public string AgencyAttributeId { get; set; }
		public long? AgencyAttributeIdValue => long.TryParse(AgencyAttributeId, out var val) ? val : (long?)null;

		public string EventNameAttributeId { get; set; }
		public long? EventNameAttributeIdValue => long.TryParse(EventNameAttributeId, out var val) ? val : (long?)null;

		public string EventFormAttributeId { get; set; }
		public long? EventFormAttributeIdValue => long.TryParse(EventFormAttributeId, out var val) ? val : (long?)null;

		public string InspectionEndAttributeId { get; set; }
		public long? InspectionEndAttributeIdValue => long.TryParse(InspectionEndAttributeId, out var val) ? val : (long?)null;

		public string AvailabilityViolationsAttributeId { get; set; }
		public long? AvailabilityViolationsAttributeIdValue => long.TryParse(AvailabilityViolationsAttributeId, out var val) ? val : (long?)null;

		public string IdentifiedViolationsTypeViolationsAttributeId { get; set; }
		public long? IdentifiedViolationsTypeViolationsAttributeIdValue => long.TryParse(IdentifiedViolationsTypeViolationsAttributeId, out var val) ? val : (long?)null;

		public string IdentifiedViolationsSignViolationsAttributeId { get; set; }
		public long? IdentifiedViolationsSignViolationsAttributeIdValue => long.TryParse(IdentifiedViolationsSignViolationsAttributeId, out var val) ? val : (long?)null;
		
		public string IdentifiedViolationsAreaAttributeId { get; set; }
		public long? IdentifiedViolationsAreaAttributeIdValue => long.TryParse(IdentifiedViolationsAreaAttributeId, out var val) ? val : (long?)null;

		public Document DocRequisites { get; set; }

		public string EliminationMarkAttributeId { get; set; }
		public long? EliminationMarkAttributeIdValue => long.TryParse(EliminationMarkAttributeId, out var val) ? val : (long?)null;

		public string EliminationAgencyAttributeId { get; set; }
		public long? EliminationAgencyAttributeIdValue => long.TryParse(EliminationAgencyAttributeId, out var val) ? val : (long?)null;

		public Document EliminationDocRequisites { get; set; }
	}
}
