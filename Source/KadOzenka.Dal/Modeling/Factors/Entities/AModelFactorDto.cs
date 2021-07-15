using ObjectModel.Directory;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.Modeling.Factors.Entities
{
	public abstract class AModelFactorDto
	{
		public long Id { get; set; }
		public long? ModelId { get; set; }
		public KoAlgoritmType Type { get; set; }
		public long? FactorId { get; set; }
		public long? DictionaryId { get; set; }
		public MarkType MarkType { get; set; }
	}
}
