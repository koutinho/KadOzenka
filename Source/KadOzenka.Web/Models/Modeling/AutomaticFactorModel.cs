using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Factors.Entities;
using Kendo.Mvc.UI;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class AutomaticFactorModel : GeneralFactorModel
	{
		public override bool IsAutomatic => true;

		[Display(Name = "Использовать в модели")]
		public bool? IsActive { get; set; }
		public string AlgorithmTypeStr => AlgorithmType.GetEnumDescription();

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
			return new(attributes, unActiveAttributeIds)
			{
				Id = factor.Id,
				ModelId = factor.ModelId,
				FactorId = factor.FactorId,
				DictionaryId = factor.DictionaryId,
				MarkType = factor.MarkType_Code,
				CorrectItem = factor.CorrectingTerm,
				K = factor.K,
				AlgorithmType = factor.AlgorithmType_Code,
				IsActive = factor.IsActive.GetValueOrDefault(),
			};
		}

		public AutomaticModelFactorDto ToDto()
		{
			return new()
			{
				Id = Id,
				ModelId = ModelId,
				Type = AlgorithmType,
				FactorId = FactorId,
				DictionaryId = DictionaryId,
				MarkType = MarkType,
				IsActive = IsActive.GetValueOrDefault(),
			};
		}
	}
}
