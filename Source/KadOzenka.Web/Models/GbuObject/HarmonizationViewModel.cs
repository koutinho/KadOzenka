﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.Gbu.Harmonization;

namespace KadOzenka.Web.Models.GbuObject
{
	public class HarmonizationViewModel : PartialCharacteristicViewModel, IValidatableObject
	{
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

	    public bool IsValuesFilterUsed { get; set; } = false;
	    public bool IsDataActualUsed { get; set; } = false;
	    public bool IsTaskFilterUsed { get; set; } = false;

        /// <summary>
        /// Список значений фильтра
        /// </summary>
        [Display(Name = "Задания на оценку")]
		public List<long> TaskFilter { get; set; }

		/// <summary>
		/// Дата на которую делается гармонизация
		/// </summary>
		[Display(Name = "Дата актулизации")]
		public DateTime? DataActual { get; set; }

		/// <summary>
		/// Фактор 1 уровня 
		/// </summary>
		public long? Level1Attribute { get; set; }

		/// <summary>
		/// Фактор 2 уровня 
		/// </summary>
		public long? Level2Attribute { get; set; }

		/// <summary>
		/// Фактор 3 уровня 
		/// </summary>
		public long? Level3Attribute { get; set; }

		/// <summary>
		/// Фактор 4 уровня 
		/// </summary>
		public long? Level4Attribute { get; set; }

		/// <summary>
		/// Фактор 5 уровня 
		/// </summary>
		public long? Level5Attribute { get; set; }

		/// <summary>
		/// Фактор 6 уровня 
		/// </summary>
		public long? Level6Attribute { get; set; }

		/// <summary>
		/// Фактор 7 уровня 
		/// </summary>
		public long? Level7Attribute { get; set; }

		/// <summary>
		/// Фактор 8 уровня 
		/// </summary>
		public long? Level8Attribute { get; set; }

		/// <summary>
		/// Фактор 9 уровня 
		/// </summary>
		public long? Level9Attribute{get; set; }

		/// <summary>
		/// Фактор 10 уровня 
		/// </summary>
		public long? Level10Attribute { get; set; }

		public HarmonizationSettings ToHarmonizationSettings()
		{
			var settings = new HarmonizationSettings
			{
				IdAttributeResult = IdAttributeResult,
				PropertyType = (PropertyTypes) PropertyType.GetValueOrDefault(),
				SelectAllObject = SelectAllObject,
				IdAttributeFilter = IdAttributeFilter,
				ValuesFilter = ValuesFilter,
				Level1Attribute = Level1Attribute,
				Level2Attribute = Level2Attribute,
				Level3Attribute = Level3Attribute,
				Level4Attribute = Level4Attribute,
				Level5Attribute = Level5Attribute,
				Level6Attribute = Level6Attribute,
				Level7Attribute = Level7Attribute,
				Level8Attribute = Level8Attribute,
				Level9Attribute = Level9Attribute,
				Level10Attribute = Level10Attribute,
				TaskFilter = IsTaskFilterUsed ? TaskFilter : null,
				DateActual = IsDataActualUsed ? DataActual: null
			};

			return settings;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
		    if (!SelectAllObject && IsDataActualUsed)
		    {
		        if (IsDataActualUsed)
		        {
		            if (!DataActual.HasValue)
		            {
		                yield return
		                    new ValidationResult(errorMessage: "Поле Дата актулизации обязательное",
		                        memberNames: new[] { nameof(DataActual) });
		            }
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

		    if (!SelectAllObject && IsValuesFilterUsed)
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
		}
	}
}
