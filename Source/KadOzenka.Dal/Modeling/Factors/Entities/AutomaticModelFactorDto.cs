namespace KadOzenka.Dal.Modeling.Factors.Entities
{
	public class AutomaticModelFactorDto : AModelFactorDto
	{
		public decimal? PreviousWeight { get; set; }
		public bool IsActive { get; set; }
	}
}
