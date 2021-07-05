using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Register;
using KadOzenka.Dal.Modeling.Dto.Factors;
using Microsoft.AspNetCore.Mvc.Rendering;
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
		public decimal Correction { get; set; }
		public decimal B0 { get; set; }
		public bool SignDiv { get; set; }
		public bool SignAdd { get; set; }
		public bool SignMarket { get; set; }

		[Display(Name = "Тип метки")]
		public MarkType MarkType { get; set; }

		[Display(Name = "Корректирующее слагаемое")]
		public decimal? CorrectItem { get; set; }

		[Display(Name = "K=[A+M]/2")]
		public decimal? K { get; set; }

		[Display(Name = "Словарь")]
		public long? DictionaryId { get; set; }
		public List<SelectListItem> Dictionaries { get; set; }


		public static ManualFactorModel ToModel(long generalModelId, OMModelFactor factor)
		{
			return new()
			{
				Id = factor.Id,
				GeneralModelId = generalModelId,
				FactorId = factor.FactorId,
				Factor = RegisterCache.GetAttributeData(factor.FactorId.GetValueOrDefault()).Name,
				MarkerId = factor.MarkerId,
				Correction = factor.Weight,
				B0 = factor.B0,
				SignDiv = factor.SignDiv,
				SignAdd = factor.SignAdd,
				MarkType = factor.MarkType_Code,
				CorrectItem = factor.CorrectingTerm,
				K = factor.K,
				DictionaryId = factor.DictionaryId
			};
		}


		public ManualModelFactorDto ToDto()
		{
			return new()
			{
				Id = Id,
				GeneralModelId = GeneralModelId,
				Type = Type,
				FactorId = FactorId,
				Weight = Correction,
				B0 = B0,
				SignDiv = SignDiv,
				SignAdd = SignAdd,
				MarkType = MarkType,
				CorrectItem = CorrectItem,
				K = K,
				DictionaryId = DictionaryId
			};
		}
	}
}
