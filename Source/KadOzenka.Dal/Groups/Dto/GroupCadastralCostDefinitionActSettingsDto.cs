namespace KadOzenka.Dal.Groups.Dto
{
	public class GroupCadastralCostDefinitionActSettingsDto
	{
		public long GroupId { get; set; }
		public string CadastralCostEstimationModelsReferences { get; set; }
		public string AssumptionsReference { get; set; }
		public string OtherCostRelatedInfo { get; set; }
	}
}
