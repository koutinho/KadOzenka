using Core.Register;
using KadOzenka.Dal.Modeling.Dto.Factors;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class ManualFactorModel
	{
		public long Id { get; set; }
		public bool IsNewFactor => Id == -1;
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
		public MarkType MarkType { get; set; }

		public static ManualFactorModel ToModel(long generalModelId, OMModelFactor factor)
		{
			return new ManualFactorModel
			{
				Id = factor.Id,
				GeneralModelId = generalModelId,
				FactorId = factor.FactorId,
				Factor = RegisterCache.GetAttributeData(factor.FactorId.GetValueOrDefault()).Name,
				MarkerId = factor.MarkerId,
				Weight = factor.Weight,
				B0 = factor.B0,
				SignDiv = factor.SignDiv,
				SignAdd = factor.SignAdd,
				MarkType = factor.MarkType_Code
			};
		}

		public ManualModelFactorDto ToDto()
		{
			return new ManualModelFactorDto
			{
				Id = Id,
				GeneralModelId = GeneralModelId,
				Type = Type,
				FactorId = FactorId,
				Weight = Weight,
				B0 = B0,
				SignDiv = SignDiv,
				SignAdd = SignAdd,
				MarkType = MarkType
			};
		}
	}
}
