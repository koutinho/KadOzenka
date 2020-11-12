using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Register;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;

namespace KadOzenka.Web.Models.ObjectsCharacteristics
{
    public class CharacteristicModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Имя характеристики не может быть пустым")]
        [Display(Name = "Имя характеристики")]
        public string Name { get; set; }

        [Display(Name = "Тип")]
        public RegisterAttributeType Type { get; set; }

        [Display(Name = "Связь со справочником")]
        public long? ReferenceId { get; set; }

        public long RegisterId { get; set; }

        [Display(Name = "Для помещений использовать родительскую характеристику")]
        public bool UseParentAttributeForPlacement { get; set; }


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
                UseParentAttributeForPlacement = setting != null && setting.UseParentAttributeForPlacements.GetValueOrDefault()
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
                UseParentAttributeForPlacement = model.UseParentAttributeForPlacement
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Type == RegisterAttributeType.REFERENCE && !ReferenceId.HasValue)
            {
                yield return new ValidationResult(errorMessage: "Поле Cправочник обязательное для данного типа характеристики",
                    memberNames: new[] { nameof(ReferenceId) });
            }
        }
    }
}
