using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Register;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Gbu.GroupingAlgoritm;

namespace KadOzenka.Web.Models.GbuObject
{
	public class GroupingObjectFinalize : PartialCharacteristicViewModel, IValidatableObject
	{
		/// <summary>
		/// Выборка по всем объектам
		/// </summary>
		public bool SelectAllObject { get; set; } = true;
		public bool IsDataActualUsed { get; set; } = false;
	    public bool IsTaskFilterUsed { get; set; } = false;

        /// <summary>
        /// Список значений фильтра
        /// </summary>
        [Display(Name = "Задания на оценку")]
		public List<long> TaskFilter { get; set; }

        [Display(Name = "Статус")]
        public List<ObjectChangeStatus> ObjectChangeStatus { get; set; }

		/// <summary>
		/// Дата на которую делается гармонизация
		/// </summary>
		[Display(Name = "Дата актулизации")]
		public DateTime? DataActual { get; set; }

		/// <summary>
		/// Настройки 1 уровня группировки
		/// </summary>
		public LevelItemGroup IdAttributeSource { get; set; }

		/// <summary>
		/// Настройки 2 уровня группировки
		/// </summary>
		public LevelItemGroup IdAttributeForSelectionBetween2 { get; set; }

		public UiFilters Filter1ForSelectionBetween2 { get; set; }
		public UiFilters Filter2ForSelectionBetween2 { get; set; }

		/// <summary>
		/// Настройки 3 уровня группировки
		/// </summary>
		public LevelItemGroup IdAttributeForSelectionBetween3 { get; set; }
		public UiFilters Filter1ForSelectionBetween3 { get; set; }
		public UiFilters Filter2ForSelectionBetween3 { get; set; }
		public UiFilters Filter3ForSelectionBetween3 { get; set; }



		public GroupingObjectFinalize()
        {
	        IdAttributeSource = new LevelItemGroup();
	        IdAttributeForSelectionBetween2 = new LevelItemGroup();
	        IdAttributeForSelectionBetween3 = new LevelItemGroup();
	        Filter1ForSelectionBetween2 = new UiFilters();
	        Filter2ForSelectionBetween2 = new UiFilters();
	        Filter1ForSelectionBetween3 = new UiFilters();
	        Filter2ForSelectionBetween3 = new UiFilters();
	        Filter3ForSelectionBetween3 = new UiFilters();
        }


        public GroupingSettings CovertToGroupingSettings()
		{
			return new GroupingSettings
			{
				//IdAttributeDocument = IdAttributeDocument,
				IdAttributeResult = IdAttributeResult,
				//IdAttributeSource = IdAttributeSource,
				Level1 = IdAttributeSource.ConvertToLevelItem(),
				Level2 = IdAttributeForSelectionBetween2.ConvertToLevelItem(),
				Level3 = IdAttributeForSelectionBetween3.ConvertToLevelItem(),
				SelectAllObject = SelectAllObject,
				DateActual = DataActual,
				TaskFilter = TaskFilter ?? new List<long>(),
				ObjectChangeStatus = ObjectChangeStatus
			};
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!SelectAllObject && IsDataActualUsed)
			{
				if (!DataActual.HasValue)
				{
					yield return
						new ValidationResult(errorMessage: "Поле Дата актулизации обязательное",
							memberNames: new[] {nameof(DataActual)});
				}
			}

		    if (!SelectAllObject && IsTaskFilterUsed)
		    {
		        if (TaskFilter?.Count == null || TaskFilter?.Count == 0)
		        {
		            yield return
		                new ValidationResult(errorMessage: "Список заданий на оценку не может быть пустым",
		                    memberNames: new[] { nameof(TaskFilter) });
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

		}

		public class UiFilters
		{
			public FilteringType Type { get; set; }
			public DateFilter DateFilter { get; set; }
			public StringFilter StringFilter { get; set; }
			public NumberFilter NumberFilter { get; set; }
			public ReferenceFilter ReferenceFilter { get; set; }
		}

		public class DateFilter
		{
			public FilteringTypeDate FilteringType { get; set; }
			public DateTime? Value { get; set; }
			public DateTime? Value2 { get; set; }
		}

		public class StringFilter
		{
			public FilteringTypeString FilteringType { get; set; }
			public string Value { get; set; }

		}

		public class NumberFilter
		{
			public FilteringTypeNumber FilteringType { get; set; }
			public decimal? Value { get; set; }
			public decimal? Value2 { get; set; }
		}

		public class ReferenceFilter
		{
			public FilteringTypeReference FilteringType { get; set; }
			public long? Value { get; set; }
		}

	}
}