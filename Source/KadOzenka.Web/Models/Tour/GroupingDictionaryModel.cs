using System.ComponentModel.DataAnnotations;
using Core.SRD;
using ObjectModel.Directory.ES;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Tour
{
	public class GroupingDictionaryModel
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

		public static GroupingDictionaryModel ToModel(OMGroupingDictionary entity, bool showItems = false)
		{
			var hasValues = entity == null || !OMGroupingDictionariesValues.Where(x => x.DictionaryId == entity.Id).ExecuteExists();

			var isEditAvailable = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.KO_DICT_MODELS_DICTIONARIES_MODIFICATION);

			return new GroupingDictionaryModel
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
