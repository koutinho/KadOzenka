using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Modeling.Factors.Entities;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class ManualFactorModel : GeneralFactorModel
	{
		public override bool IsAutomatic => false;

		[Display(Name = "Коэффициент")]
		public decimal B0 { get; set; }

		[Display(Name = "Поправка")]
		public decimal Weight { get; set; }


		public static ManualFactorModel ToModel(long generalModelId, OMModelFactor factor)
		{
			return new()
			{
				Id = factor.Id,
				ModelId = generalModelId,
				FactorId = factor.FactorId,
				DictionaryId = factor.DictionaryId,
				Weight = factor.Weight,
				B0 = factor.B0,
				MarkType = factor.MarkType_Code,
				CorrectItem = factor.CorrectingTerm,
				K = factor.K
			};
		}


		public ManualModelFactorDto ToDto()
		{
			return new()
			{
				Id = Id,
				ModelId = ModelId,
				Type = AlgorithmType,
				FactorId = FactorId,
				DictionaryId = DictionaryId,
				Weight = Weight,
				B0 = B0,
				MarkType = MarkType,
				CorrectItem = CorrectItem,
				K = K
			};
		}
	}
}
