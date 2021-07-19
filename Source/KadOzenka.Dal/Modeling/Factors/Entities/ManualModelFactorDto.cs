namespace KadOzenka.Dal.Modeling.Factors.Entities
{
	public class ManualModelFactorDto : AModelFactorDto
	{
		public long? MarkerId { get; set; }
		public decimal Weight { get; set; }
		public decimal B0 { get; set; }
		public decimal? CorrectItem { get; set; }
		public decimal? K { get; set; }
	}
}
