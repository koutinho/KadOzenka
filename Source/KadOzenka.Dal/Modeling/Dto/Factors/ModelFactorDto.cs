using ObjectModel.Directory;

namespace KadOzenka.Dal.Modeling.Dto.Factors
{
	public class ModelFactorDto
	{
		public long Id { get; set; }
		public long? GeneralModelId { get; set; }
		public KoAlgoritmType Type { get; set; }
		public long? FactorId { get; set; }
		public long? DictionaryId { get; set; }
		public string Factor { get; set; }
		public long? MarkerId { get; set; }
		public decimal Weight { get; set; }
		public decimal B0 { get; set; }
		public bool SignDiv { get; set; }
		public bool SignAdd { get; set; }
		public bool SignMarket { get; set; }
	}
}
