using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.CodDictionary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Web.Models.GbuCod
{
	public class CodJobViewModel
    {
        public long Id { get; set; }
        public bool IsReadOnly => Id != -1;

        [Display(Name="Задание ЦОД")]
		public string Name { get; set; }

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

        public CodDictionaryDto ToDto()
        {
            return new CodDictionaryDto
            {
                Id = Id,
                Name = Name,
                Values = Values
            };
        }
    }
}
