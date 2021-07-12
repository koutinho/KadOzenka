namespace KadOzenka.Dal.Modeling.Dto.Factors
{
	public class AutomaticModelFactorDto : AModelFactorDto
	{
		public decimal? PreviousWeight { get; set; }
		public bool IsActive { get; set; }
	}
}
