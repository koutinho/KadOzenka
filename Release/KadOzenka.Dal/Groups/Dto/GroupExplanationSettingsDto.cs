namespace KadOzenka.Dal.Groups.Dto
{
	public class GroupExplanationSettingsDto
	{
		public long GroupId { get; set; }
		public string AppliedApproachesInCadastralCost { get; set; }
		public string AppliedEvaluationMethodsInCadastralCost { get; set; }
		public string CadastralCostDetermingMethod { get; set; }
		public string ModelJustification { get; set; }
		public string ObjectsSegment { get; set; }
		public string ObjectsSubgroup { get; set; }
		public string CadastralCostCalculationOrderDescription { get; set; }
		public string PriceZoneCharacteristic { get; set; }
		public string MarketSegment { get; set; }
		public string MarketSegmentFunctioningFeatures { get; set; }
	}
}
