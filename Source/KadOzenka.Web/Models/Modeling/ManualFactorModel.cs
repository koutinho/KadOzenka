using System.ComponentModel.DataAnnotations;
using ModelingBusiness.Factors.Entities;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class ManualFactorModel : GeneralFactorModel
	{
		public override bool IsAutomatic => false;

		[Display(Name = "Коэффициент")]
		public decimal? Coefficient { get; set; }

		[Display(Name = "Поправка")]
		public decimal Correction { get; set; }


		public static ManualFactorModel ToModel(OMModelFactor factor, KoAlgoritmType type)
		{
			return new()
			{
				Id = factor.Id,
				ModelId = factor.ModelId,
				FactorId = factor.FactorId,
				DictionaryId = factor.DictionaryId,
				Correction = factor.Correction,
				Coefficient = factor.GetCoefficient(type),
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
