namespace KadOzenka.Dal.ExpressScore.Dto
{
	public class CostFactorsDto
	{
		public string Name { get; set; }
		public decimal? Coefficient { get; set; }
		public int AttributeId { get; set; }
		public int DictionaryId { get; set; }
	}
}