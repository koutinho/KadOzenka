namespace ModelingBusiness.Factors.Entities
{
	public class ManualModelFactorDto : AModelFactorDto
	{
		public long? MarkerId { get; set; }
		public decimal Correction { get; set; }
		public decimal Coefficient { get; set; }
		public decimal? CorrectItem { get; set; }
		public decimal? K { get; set; }
	}
}
