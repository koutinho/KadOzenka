using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto.Factors;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class AutomaticFactorModel
	{
		public long Id { get; set; }
		public long? ModelId { get; set; }
		public bool IsNewFactor => Id == -1;
		
		[Display(Name = "Алгоритм расчета")]
		public KoAlgoritmType AlgorithmType { get; set; }
		public string AlgorithmTypeStr => AlgorithmType.GetEnumDescription();

		[Display(Name = "Фактор")]
		public long? FactorId { get; set; }
		public string Factor { get; set; }

		[Display(Name = "Словарь")]
		public string DictionaryName { get; set; }
		public long? DictionaryId { get; set; }

		[Display(Name = "Коэффициент из предыдущего тура")]
		public decimal? PreviousWeight { get; set; }

		[Display(Name = "Использовать в модели")]
		public bool? IsActive { get; set; }

		[Display(Name = "Тип метки")]
		public MarkType MarkType { get; set; }


		public static AutomaticFactorModel ToModel(OMModelFactor factor)
		{
			return new AutomaticFactorModel
			{
				Id = factor.Id,
				ModelId = factor.ModelId,
				AlgorithmType = factor.AlgorithmType_Code,
				FactorId = factor.FactorId,
				DictionaryId = factor.DictionaryId,
				PreviousWeight = factor.PreviousWeight,
				IsActive = factor.IsActive.GetValueOrDefault(),
				MarkType = factor.MarkType_Code
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
				PreviousWeight = PreviousWeight,
				IsActive = IsActive.GetValueOrDefault(),
				MarkType = MarkType
			};
		}
	}
}
