using System.ComponentModel.DataAnnotations;
using Core.SRD;
using ObjectModel.Directory.ES;
using ObjectModel.ES;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Modeling
{
	public class DictionaryModel
	{
		public long Id { get; set; }

		/// <summary>
		/// Наименование справочника
		/// </summary>
		[Display(Name = "Наименование справочника")]
		[Required(ErrorMessage = "Поле Наименование справочника обязательное")]
		public string Name { get; set; }

		/// <summary>
		/// Тип данных значения справочника
		/// </summary>
		[Display(Name = "Тип данных")]
		[Required(ErrorMessage = "Поле Тип данных значений справочника обязательное")]
		public ReferenceItemCodeType ValueType { get; set; } = ReferenceItemCodeType.String;

		public bool ValueTypeCanBeChanged { get; set; }

		public bool ShowItems { get; set; }


		public bool IsEdit { get; set; }

		public static DictionaryModel FromEntity(OMModelingDictionaries entity, bool showItems = false)
		{
			if (entity == null)
			{
				return new DictionaryModel
				{
					Id = -1,
					ShowItems = showItems,
					ValueTypeCanBeChanged = true,
					IsEdit = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)
				};
			}

			var hasValues = !OMModelingDictionariesValues.Where(x => x.DictionaryId == entity.Id).ExecuteExists();

			return new DictionaryModel
			{
				Id = entity.Id,
				Name = entity.Name,
				ValueType = entity.Type_Code,
				ValueTypeCanBeChanged = hasValues,
				ShowItems = showItems,
				IsEdit = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)
			};
		}
    }
}
