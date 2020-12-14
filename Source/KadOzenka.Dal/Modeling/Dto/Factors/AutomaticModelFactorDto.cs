using ObjectModel.Directory;

namespace KadOzenka.Dal.Modeling.Dto.Factors
{
	public class AutomaticModelFactorDto
	{
		public long Id { get; set; }
		public long? ModelId { get; set; }
		public KoAlgoritmType Type { get; set; }
		public long? FactorId { get; set; }
		public long? DictionaryId { get; set; }
		public decimal? PreviousWeight { get; set; }
	}
}
