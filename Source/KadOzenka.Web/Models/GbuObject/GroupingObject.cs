using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Gbu.GroupingAlgoritm;

namespace KadOzenka.Web.Models.GbuObject
{
	/// <summary>
	/// Настройки уровня группировки
	/// </summary>
	public class LevelItemGroup
	{
		/// <summary>
		/// Название значения
		/// </summary>
		public string CodValueName { get; set; }
		/// <summary>
		/// Идентификатор значения в справочнике
		/// </summary>
		public long? CodValueId { get; set; }
		/// <summary>
		/// Признак пропуска дефиса
		/// </summary>
		public bool SkipDefis { get; set; }
		/// <summary>
		/// Идентификатор аттрибута
		/// </summary>
		public int? IdFactor { get; set; }

		public LevelItem ConvertToLevelItem()
		{
			return new LevelItem
			{
				IdFactor = IdFactor,
				SkipDefis = SkipDefis,
				CodValueName = CodValueName,
				CodValueId = CodValueId
			};
		}
	}

	public class GroupingObject : PartialCharacteristicViewModel, IValidatableObject
	{
		/// <summary>
		/// Идентификатор задания ЦОД
		/// </summary>
		[Display(Name = "Справочник")]
		[Required(ErrorMessage = "Заполните справочник")]
		public int? IdCodJob { get; set; }

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
		public LevelItemGroup Level1 { get; set; }

		/// <summary>
		/// Настройки 2 уровня группировки
		/// </summary>
		public LevelItemGroup Level2 { get; set; }

		/// <summary>
		/// Настройки 3 уровня группировки
		/// </summary>
		public LevelItemGroup Level3 { get; set; }

		/// <summary>
		/// Настройки 4 уровня группировки
		/// </summary>
		public LevelItemGroup Level4 { get; set; }

		/// <summary>
		/// Настройки 5 уровня группировки
		/// </summary>
		public LevelItemGroup Level5 { get; set; }

		/// <summary>
		/// Настройки 6 уровня группировки
		/// </summary>
		public LevelItemGroup Level6 { get; set; }

		/// <summary>
		/// Настройки 7 уровня группировки
		/// </summary>
		public LevelItemGroup Level7 { get; set; }

		/// <summary>
		/// Настройки 8 уровня группировки
		/// </summary>
		public LevelItemGroup Level8 { get; set; }

		/// <summary>
		/// Настройки 9 уровня группировки
		/// </summary>
		public LevelItemGroup Level9 { get; set; }

		/// <summary>
		/// Идентификатор атрибута, куда будут записаны источники 
		/// </summary>
		//[Display(Name = "Источник")]
		//public int? IdAttributeSource { get; set; }

	    // [Display(Name = "Документ")]
     //    public int? IdAttributeDocument { get; set; }

        public GroupingObject()
        {
	        Level1 = new LevelItemGroup();
	        Level2 = new LevelItemGroup();
	        Level3 = new LevelItemGroup();
	        Level4 = new LevelItemGroup();
	        Level5 = new LevelItemGroup();
	        Level6 = new LevelItemGroup();
	        Level7 = new LevelItemGroup();
	        Level8 = new LevelItemGroup();
	        Level9 = new LevelItemGroup();
        }


        public GroupingSettings CovertToGroupingSettings()
		{
			return new GroupingSettings
			{
				IdCodJob = IdCodJob,
				//IdAttributeDocument = IdAttributeDocument,
				IdAttributeResult = IdAttributeResult,
				//IdAttributeSource = IdAttributeSource,
				Level1 = Level1.ConvertToLevelItem(),
				Level2 = Level2.ConvertToLevelItem(),
				Level3 = Level3.ConvertToLevelItem(),
				Level4 = Level4.ConvertToLevelItem(),
				Level5 = Level5.ConvertToLevelItem(),
				Level6 = Level6.ConvertToLevelItem(),
				Level7 = Level7.ConvertToLevelItem(),
				Level8 = Level8.ConvertToLevelItem(),
				Level9 = Level9.ConvertToLevelItem(),
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