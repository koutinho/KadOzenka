using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.CodDictionary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Web.Models.GbuCod
{
    public class CodJobViewModel : IValidatableObject
    {
        public long Id { get; set; }
        public bool IsReadOnly => Id != -1;

        [Display(Name = "Задание ЦОД")] public string Name { get; set; }

        [Display(Name = "Количество значений")]
        public int ValuesCount { get; set; }

        public List<SelectListItem> PossibleValuesCount { get; set; }


        public CodJobViewModel()
        {
            PossibleValuesCount = new List<SelectListItem>();

            for (var i = CodDictionaryConsts.MinValuesCount; i <= CodDictionaryConsts.MaxValuesCount; i++)
            {
                PossibleValuesCount.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
        }

        public virtual CodDictionaryDto ToDto()
        {
            return new CodDictionaryDto
            {
                Id = Id,
                Name = Name
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return CodDictionaryService.ValidateCodDictionary(ToDto());
        }
    }
}