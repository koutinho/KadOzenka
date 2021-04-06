using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Dal.Registers;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.GbuCod
{
	public class CodJobViewModel : IValidatableObject
    {
        public long Id { get; set; }
        public bool IsReadOnly => Id != -1;

        [Display(Name="Задание ЦОД")]
		public string Name { get; set; }

		[Display(Name = "Результат")]
		public string Result { get; set; }

        [Display(Name = "Количество значений")]
        public int ValuesCount { get; set; }
        public List<SelectListItem> PossibleValuesCount { get; set; }
        public List<string> Values { get; set; }



        public CodJobViewModel()
        {
            Values = new List<string>();
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

		public static CodJobViewModel FromEntity(OMCodJob entity)
		{
			if (entity == null)
			{
				return new CodJobViewModel
				{
					Id = -1
				};
			}

			return new CodJobViewModel
			{
				Id = entity.Id,
				Name = entity.NameJob,
				Result = entity.ResultJob
			};
		}

		public static void ToEntity(CodJobViewModel viewModel, ref OMCodJob codJob)
		{
			codJob.NameJob = viewModel.Name;
			codJob.ResultJob = viewModel.Result;
		}

        public CodDictionaryDto ToDto()
        {
            return new CodDictionaryDto
            {
                Id = Id,
                Name = Name,
                Result = Result,
                Values = Values
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return CodDictionaryService.ValidateCodDictionary(ToDto());
        }
    }
}
