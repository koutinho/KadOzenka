using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.Gbu.CodSelection;

namespace KadOzenka.Web.Models.GbuObject
{
	public class UnloadingFromDicViewModel : PartialCharacteristicViewModel, IValidatableObject
	{
		/// <summary>
		/// Идентификатор задания ЦОД
		/// </summary>
		[Display(Name = "Задание ЦОД")]
		[Required(ErrorMessage = "Выберете Задание ЦОД")]
		public long? IdCodJob { get; set; }

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
        public PartialDocumentViewModel Document { get; set; } = new PartialDocumentViewModel();

        public CodSelectionSettings ToCodSelectionSettings()
		{
			return new CodSelectionSettings
				{
					IdAttributeFilter = IdAttributeFilter,
					IdDocument = Document.IdDocument,
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

			if (IsNewAttribute)
			{
				if (string.IsNullOrEmpty(NameNewAttribute))
				{
					yield return
						new ValidationResult(errorMessage: "Имя нового атрибута не может быть пустым",
							memberNames: new[] { nameof(NameNewAttribute) });
				}

				if (TypeNewAttribute == null)
				{
					yield return
						new ValidationResult(errorMessage: "Тип нового атрибута не может быть пустым",
							memberNames: new[] { nameof(TypeNewAttribute) });
				}

				if (RegistryId == null)
				{
					yield return
						new ValidationResult(errorMessage: "Выберите реестр",
							memberNames: new[] { nameof(RegistryId) });
				}
			}
			else if (IdAttributeResult == null)
			{
				yield return
					new ValidationResult(errorMessage: "Заполните результирующую характеристику",
						memberNames: new[] { nameof(IdAttributeResult) });
			}

		    if (!Document.IsNewDocument && Document.IdDocument == null)
		    {
		        
		        yield return
		            new ValidationResult(errorMessage: "Выберете документ",
		                memberNames: new[] { nameof(Document) });
            }
        }

	}
}