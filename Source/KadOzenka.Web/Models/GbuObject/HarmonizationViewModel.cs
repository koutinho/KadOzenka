using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.GbuObject
{
    public class PartialNewHarmonizationLevel
    {
        public int RowNumber { get; set; }
        public int LevelNumber { get; set; }
        public long? AttributeId { get; set; }
    }

    public class HarmonizationViewModel : PartialCharacteristicViewModel, IValidatableObject
	{
        public static int NumberOfConstantLevelsInHarmonization => 10;

        /// <summary>
        /// Тип объекта 
        /// </summary>
        [Required(ErrorMessage = "Выберете Тип объекта")]
		public long? PropertyType { get; set; }

        /// <summary>
        /// Дополнительное значение к типу "Здания" 
        /// </summary>
        public BuildingPurpose BuildingPurpose
        {
            get
            {
                if (PropertyType != (long) PropertyTypes.Building)
                    return BuildingPurpose.None;
                if(IsNotLivingBuilding && (IsLivingBuilding || IsApartmentHouse))
                    return BuildingPurpose.None;

                if (IsLivingBuilding && IsApartmentHouse)
                    return BuildingPurpose.LiveAndApartmentHouse;
                if (IsLivingBuilding)
                    return BuildingPurpose.Live;
                if (IsNotLivingBuilding)
                    return BuildingPurpose.NotLive;
                if (IsApartmentHouse)
                    return BuildingPurpose.ApartmentHouse;

                return BuildingPurpose.None;
            }
        }
        public bool IsLivingBuilding { get; set; }
        public bool IsNotLivingBuilding { get; set; }
        public bool IsApartmentHouse { get; set; }

        /// <summary>
        /// Дополнительное значение к типу "Помещение" 
        /// </summary>
        public PlacementPurpose PlacementPurpose
        {
            get
            {
                if (PropertyType != (long)PropertyTypes.Pllacement)
                    return PlacementPurpose.None;

                if (IsLivingPlacement)
                    return PlacementPurpose.Live;
                if (IsNotLivingPlacement)
                    return PlacementPurpose.NotLive;
                if (IsParkingPlace)
                    return PlacementPurpose.ParkingPlace;

                return PlacementPurpose.None;
            }
        }
        public bool IsLivingPlacement { get; set; }
        public bool IsNotLivingPlacement { get; set; }
        public bool IsParkingPlace { get; set; }

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

        /// <summary>
        /// Факторы, добавленные юзером
        /// </summary>
        public List<PartialNewHarmonizationLevel> AdditionalCustomLevels { get; set; }


        public HarmonizationSettings ToHarmonizationSettings()
        {
            if (IdAttributeResult == null)
                throw new Exception("Не заполнена результирующая характеристика");

            var settings = new HarmonizationSettings
			{
				IdAttributeResult = IdAttributeResult.Value,
				PropertyType = (PropertyTypes) PropertyType.GetValueOrDefault(),
                BuildingPurpose = BuildingPurpose,
                PlacementPurpose = PlacementPurpose,
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
                AdditionalLevels = MapAdditionalCustomLevels(),
				TaskFilter = IsTaskFilterUsed ? TaskFilter : null,
				DateActual = IsDataActualUsed ? DataActual: null
			};

			return settings;
		}

        protected List<AdditionalLevelsForHarmonization> MapAdditionalCustomLevels()
        {
            return AdditionalCustomLevels?.Select(x => new AdditionalLevelsForHarmonization
            {
                LevelNumber = x.LevelNumber,
                AttributeId = x.AttributeId
            }).ToList();
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

            if (IsDataActualUsed)
            {
                if (!IsValuesFilterUsed)
                {
                    yield return
                        new ValidationResult(errorMessage: "Выберите характеристику и ее значение на выбранную дату актуальности",
                            memberNames: new[] { nameof(IsValuesFilterUsed) });
                }
            }

            if (PropertyType == (long)PropertyTypes.Building && IsNotLivingBuilding)
            {
                if (IsLivingBuilding || IsApartmentHouse)
                    yield return new ValidationResult(
                        $"Должен быть выбран только один тип для здания: '{BuildingPurpose.NotLive.GetEnumDescription()}' ");
            }

            if (PropertyType == (long)PropertyTypes.Pllacement)
            {
                if((IsLivingPlacement && (IsNotLivingPlacement || IsParkingPlace)) ||
                    (IsNotLivingPlacement && (IsLivingPlacement || IsParkingPlace)) ||
                     (IsParkingPlace && (IsLivingPlacement || IsNotLivingPlacement)))
                    yield return new ValidationResult("Должен быть выбран только один тип для помещения");
            }
        }
    }
}
