using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto.Factors;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class AutomaticFactorModel
	{
		public long Id { get; set; }
		public long? ModelId { get; set; }
		
		[Display(Name = "Алгоритм расчета")]
		public KoAlgoritmType AlgorithmType { get; set; }
		public string AlgorithmTypeStr => AlgorithmType.GetEnumDescription();

		[Display(Name = "Фактор")]
		public long? FactorId { get; set; }
		public string Factor { get; set; }

		[Display(Name = "Словарь")]
		public long? DictionaryId { get; set; }

		[Display(Name = "Коэффициент из предыдущего тура")]
		public decimal? PreviousWeight { get; set; }


		public static AutomaticFactorModel ToModel(OMModelFactor factor)
		{
			return new AutomaticFactorModel
			{
				Id = factor.Id,
				ModelId = factor.ModelId,
				AlgorithmType = factor.AlgorithmType_Code,
				FactorId = factor.FactorId,
				DictionaryId = factor.DictionaryId,
				PreviousWeight = factor.PreviousWeight
			};
		}

		public AutomaticModelFactorDto ToDto()
		{
			return new AutomaticModelFactorDto
			{
				Id = Id,
				ModelId = ModelId,
				Type = AlgorithmType,
				FactorId = FactorId,
				DictionaryId = DictionaryId,
				PreviousWeight = PreviousWeight
			};
		}
	}
}
