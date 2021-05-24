using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Models.Filters;

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
		/// Преобразовываемый атрибут
		/// </summary>
		public LevelItemGroup IdAttributeSource { get; set; }

		/// <summary>
		/// Настройки 2 уровня группировки
		/// </summary>
		public LevelItemGroup IdAttributeForSelectionBetween2 { get; set; }

		public Filters Filter1ForSelectionBetween2 { get; set; }
		public Filters Filter2ForSelectionBetween2 { get; set; }

		/// <summary>
		/// Настройки 3 уровня группировки
		/// </summary>
		public LevelItemGroup IdAttributeForSelectionBetween3 { get; set; }
		public Filters Filter1ForSelectionBetween3 { get; set; }
		public Filters Filter2ForSelectionBetween3 { get; set; }
		public Filters Filter3ForSelectionBetween3 { get; set; }



		public GroupingObjectFinalize()
        {
	        IdAttributeSource = new LevelItemGroup();
	        IdAttributeForSelectionBetween2 = new LevelItemGroup();
	        IdAttributeForSelectionBetween3 = new LevelItemGroup();
	        Filter1ForSelectionBetween2 = new Filters();
	        Filter2ForSelectionBetween2 = new Filters();
	        Filter1ForSelectionBetween3 = new Filters();
	        Filter2ForSelectionBetween3 = new Filters();
	        Filter3ForSelectionBetween3 = new Filters();
        }


        public GroupingSettingsFinal CovertToGroupingSettings()
		{
			return new GroupingSettingsFinal
			{
				IdAttributeResult = IdAttributeResult,
				IdAttributeSource = IdAttributeSource.IdFactor,

				IdAttributeFor2Selections = IdAttributeForSelectionBetween2.IdFactor,
				Filter1ForSelectionBetween2 = Filter1ForSelectionBetween2,
				Filter2ForSelectionBetween2 = Filter2ForSelectionBetween2,

				IdAttributeFor3Selections = IdAttributeForSelectionBetween3.IdFactor,
				Filter1ForSelectionBetween3 = Filter1ForSelectionBetween3,
				Filter2ForSelectionBetween3 = Filter2ForSelectionBetween3,
				Filter3ForSelectionBetween3 = Filter3ForSelectionBetween3,

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


	}
}