using ObjectModel.Directory;
using ObjectModel.Directory.Ko;

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
		public bool IsActive { get; set; }
		public bool SignExponentiation { get; set; }
		public MarkType MarkType { get; set; }
	}
}
