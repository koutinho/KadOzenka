using System.ComponentModel.DataAnnotations;
using Core.SRD;
using ObjectModel.Directory.ES;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class DictionaryModel
	{
		public long Id { get; set; }

		[Display(Name = "Наименование справочника")]
		[Required(ErrorMessage = "Поле 'Наименование справочника' обязательное")]
		public string Name { get; set; }

		[Display(Name = "Тип данных")]
		[Required(ErrorMessage = "Поле 'Тип данных' значений справочника обязательное")]
		public ReferenceItemCodeType ValueType { get; set; } = ReferenceItemCodeType.String;

		public bool ValueTypeCanBeChanged { get; set; }
		public bool ShowItems { get; set; }


		public bool IsEdit { get; set; }

		public static DictionaryModel FromEntity(OMModelingDictionary entity, bool showItems = false)
		{
			var hasValues = entity == null || !OMModelingDictionariesValues.Where(x => x.DictionaryId == entity.Id).ExecuteExists();

			var isEditAvailable = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.KO_DICT_MODELS);

			return new DictionaryModel
			{
				Id = entity?.Id ?? -1,
				Name = entity?.Name,
				ValueType = entity?.Type_Code ?? ReferenceItemCodeType.String,
				ShowItems = showItems,
				ValueTypeCanBeChanged = hasValues,
				IsEdit = isEditAvailable
			};
		}
    }
}
