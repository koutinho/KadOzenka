using System.ComponentModel.DataAnnotations;
using ModelingBusiness.Factors.Entities;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class ManualFactorModel : GeneralFactorModel
	{
		public override bool IsAutomatic => false;

		[Display(Name = "Коэффициент")]
		public decimal Coefficient { get; set; }

		[Display(Name = "Поправка")]
		public decimal Correction { get; set; }


		public static ManualFactorModel ToModel(long generalModelId, OMModelFactor factor)
		{
			return new()
			{
				Id = factor.Id,
				ModelId = generalModelId,
				FactorId = factor.FactorId,
				DictionaryId = factor.DictionaryId,
				Correction = factor.Correction,
				Coefficient = factor.CoefficientForLinear,
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
				Correction = Correction,
				Coefficient = Coefficient,
				MarkType = MarkType,
				CorrectItem = CorrectItem,
				K = K
			};
		}
	}
}
