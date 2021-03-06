using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Register;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;

namespace KadOzenka.Web.Models.ObjectsCharacteristics
{
    public class CharacteristicModel : IValidatableObject
    {
	    public const string EmptyDictionaryNameErrorMessage = "Имя характеристики не может быть пустым";
	    public const string ReferenceTypeWithoutReferenceLinkErrorMessage = "Поле Cправочник обязательное для данного типа характеристики";

        public long Id { get; set; }

        [Required(ErrorMessage = EmptyDictionaryNameErrorMessage)]
        [Display(Name = "Имя характеристики")]
        public string Name { get; set; }

        [Display(Name = "Тип")]
        public RegisterAttributeType Type { get; set; }

        [Display(Name = "Связь со справочником")]
        public long? ReferenceId { get; set; }

        public long RegisterId { get; set; }

        [Display(Name = "Использовать родительскую характеристику для жилых помещений")]
        public bool UseParentAttributeForLivingPlacement { get; set; }

        [Display(Name = "Использовать родительскую характеристику для нежилых помещений")]
        public bool UseParentAttributeForNotLivingPlacement { get; set; }

        [Display(Name = "Использовать родительскую характеристику для машино-мест")]
        public bool UseParentAttributeForCarPlace { get; set; }

        [Display(Name = "Запрет редактирования")]
        public bool DisableAttributeEditing { get; set; }


        public static CharacteristicModel Map(OMAttribute attribute, OMAttributeSettings setting = null)
        {
            return new CharacteristicModel
            {
                Id = attribute.Id,
                RegisterId = attribute.RegisterId,
                Name = attribute.Name,
                Type = attribute.ReferenceId.HasValue
                    ? RegisterAttributeType.REFERENCE
                    : (RegisterAttributeType)attribute.Type,
                ReferenceId = attribute.ReferenceId,
                UseParentAttributeForLivingPlacement = setting != null && setting.UseParentAttributeForLivingPlacements.GetValueOrDefault(),
                UseParentAttributeForNotLivingPlacement = setting != null && setting.UseParentAttributeForNotLivingPlacements.GetValueOrDefault(),
                UseParentAttributeForCarPlace = setting != null && setting.UseParentAttributeForCarPlace.GetValueOrDefault(),
                DisableAttributeEditing = setting != null && setting.DisableEditing.GetValueOrDefault(),
            };
        }

        public static CharacteristicDto UnMap(CharacteristicModel model)
        {
            return new CharacteristicDto
            {
                Id = model.Id,
                Name = model.Name,
                RegisterId = model.RegisterId,
                Type = model.Type,
                ReferenceId = model.ReferenceId,
                UseParentAttributeForLivingPlacement = model.UseParentAttributeForLivingPlacement,
                UseParentAttributeForNotLivingPlacement = model.UseParentAttributeForNotLivingPlacement,
                UseParentAttributeForCarPlace = model.UseParentAttributeForCarPlace,
                DisableAttributeEditing = model.DisableAttributeEditing
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
	        if (Id == -1 && RegisterId == RosreestrRegisterService.Id)
		        yield return new ValidationResult("Добавление характеристик для ЕГРН запрещено");

            if (Type == RegisterAttributeType.REFERENCE && !ReferenceId.HasValue)
            {
                yield return new ValidationResult(errorMessage: ReferenceTypeWithoutReferenceLinkErrorMessage,
                    memberNames: new[] { nameof(ReferenceId) });
            }
        }
    }
}
