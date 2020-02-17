using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.Gbu.CodSelection;

namespace KadOzenka.Web.Models.GbuObject
{
	public class UnloadingFromDicViewModel : IValidatableObject
	{
		/// <summary>
		/// Идентификатор задания ЦОД
		/// </summary>
		[Display(Name = "Задание ЦОД")]
		[Required(ErrorMessage = "Выберете Задание ЦОД")]
		public long? IdCodJob { get; set; }

		/// <summary>
		/// Идентификатор атрибута, куда будет записан результат 
		/// </summary>
		[Display(Name = "Характеристика")]
		[Required(ErrorMessage = "Выберете результирующую Характеристику")]
		public long? IdAttributeResult { get; set; }

		/// <summary>
		/// Тип объекта 
		/// </summary>
		[Required(ErrorMessage = "Выберете Тип объекта")]
		public long? PropertyType { get; set; }

		/// <summary>
		/// Выборка по всем объектам
		/// </summary>
		public bool SelectAllObject { get; set; } = true;

		/// <summary>
		/// Идентификатор аттрибута - фильтра
		/// </summary>
		[Display(Name = "Характеристика")]
		public long? IdAttributeFilter { get; set; }

		/// <summary>
		/// Список значений фильтра
		/// </summary>
		[Display(Name = "Значение")]
		public List<string> ValuesFilter { get; set; }

		/// <summary>
		/// Документ для значения по умолчанию 
		/// </summary>
		[Display(Name = "Документ")]
		[Required(ErrorMessage = "Выберете документ")]
		public long? IdDocument { get; set; }

		public CodSelectionSettings ToCodSelectionSettings()
		{
			return new CodSelectionSettings
				{
					IdAttributeFilter = IdAttributeFilter,
					IdDocument = IdDocument,
					IdAttributeResult = IdAttributeResult,
					IdCodJob = IdCodJob,
					PropertyType = PropertyType != null ? (PropertyTypes) PropertyType : PropertyTypes.UnitedPropertyComplex,
					SelectAllObject = SelectAllObject,
					ValuesFilter = ValuesFilter
				};
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{

			if (!SelectAllObject)
			{
				if (ValuesFilter?.Count == null || ValuesFilter?.Count == 0)
				{
					yield return
						new ValidationResult(errorMessage: "Список значений фильтра не может быть пустым",
							memberNames: new[] { nameof(ValuesFilter) });
				}
				if (!IdAttributeFilter.HasValue)
				{
					yield return
						new ValidationResult(errorMessage: "Поле Идентификатор атрибута-фильтра обязательное",
							memberNames: new[] { nameof(IdAttributeFilter) });
				}
			}
		}

	}
}