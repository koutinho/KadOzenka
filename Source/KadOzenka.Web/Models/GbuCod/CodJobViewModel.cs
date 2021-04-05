using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.GbuCod
{
	public class CodJobViewModel
    {
        //TODO возможно, вынести в конфиг
        private const int MaxValuesCount = 9;

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

			for (var i = 1; i <= MaxValuesCount; i++)
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
	}
}
