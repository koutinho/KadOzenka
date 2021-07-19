using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Factors.Entities;
using Kendo.Mvc.UI;
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

		[Display(Name = "Использовать в модели")]
		public bool? IsActive { get; set; }

		[Display(Name = "Тип метки")]
		public MarkType MarkType { get; set; }

		public List<long> UnActiveAttributeIds { get; }
		public List<DropDownTreeItemModel> Attributes { get; }



		public AutomaticFactorModel(List<DropDownTreeItemModel> attributes, List<long> unActiveAttributeIds)
		{
			Attributes = attributes;
			UnActiveAttributeIds = unActiveAttributeIds;
		}

		//конструктор без параметров нужен при передачи модели в метод контроллера
		public AutomaticFactorModel()
		{

		}



		public static AutomaticFactorModel ToModel(OMModelFactor factor, List<DropDownTreeItemModel> attributes, List<long> unActiveAttributeIds)
		{
			return new AutomaticFactorModel(attributes, unActiveAttributeIds)
			{
				Id = factor.Id,
				ModelId = factor.ModelId,
				AlgorithmType = factor.AlgorithmType_Code,
				FactorId = factor.FactorId,
				DictionaryId = factor.DictionaryId,
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
				IsActive = IsActive.GetValueOrDefault(),
				MarkType = MarkType
			};
		}
	}
}
