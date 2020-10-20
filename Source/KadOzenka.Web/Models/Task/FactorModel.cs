using KadOzenka.Dal.Modeling.Dto.Factors;

namespace KadOzenka.Web.Models.Task
{
	public class FactorModel
	{
		public long Id { get; set; }
		public long? GeneralModelId { get; set; }
		public long? FactorId { get; set; }
		public long? DictionaryId { get; set; }
		public string Factor { get; set; }
		public long? MarkerId { get; set; }
		public decimal Weight { get; set; }
		public decimal B0 { get; set; }
		public bool SignDiv { get; set; }
		public bool SignAdd { get; set; }
		public bool SignMarket { get; set; }


		public ModelFactorDto ToDto()
		{
			return new ModelFactorDto
			{
				Id = Id,
				GeneralModelId = GeneralModelId,
				DictionaryId = DictionaryId,
				FactorId = FactorId,
				Weight = Weight,
				B0 = B0,
				SignDiv = SignDiv,
				SignAdd = SignAdd,
				SignMarket = SignMarket
			};
		}
	}
}
