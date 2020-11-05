using KadOzenka.Dal.Modeling.Dto.Factors;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Modeling
{
	public class ManualFactorModel
	{
		public long Id { get; set; }
		public long? GeneralModelId { get; set; }
		public KoAlgoritmType Type { get; set; }
		public long? FactorId { get; set; }
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
				Type = Type,
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
