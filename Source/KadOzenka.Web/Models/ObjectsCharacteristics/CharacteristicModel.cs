using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Register;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;

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


        public static CharacteristicModel Map(CharacteristicDto dto)
        {
            return new CharacteristicModel
            {
                Id = dto.Id,
                RegisterId = dto.RegisterId,
                Name = dto.Name,
                Type = dto.ReferenceId.HasValue
                    ? RegisterAttributeType.REFERENCE
                    : dto.Type,
                ReferenceId = dto.ReferenceId
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
                ReferenceId = model.ReferenceId
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
