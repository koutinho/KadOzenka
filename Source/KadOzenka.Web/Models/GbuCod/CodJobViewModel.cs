using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.GbuCod
{
	public class CodJobViewModel : IValidatableObject
    {
        //TODO возможно, вынести в конфиг
        private const int MaxValuesCount = 9;
        private const int MinValuesCount = 1;

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

			for (var i = MinValuesCount; i <= MaxValuesCount; i++)
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult("Не указано Имя справочника");
			}

            if (string.IsNullOrWhiteSpace(Result))
            {
                yield return new ValidationResult("Не указан Результат");
            }

            if (ValuesCount > MaxValuesCount)
            {
                yield return new ValidationResult($"Максимальное количество значений - {MaxValuesCount}");
            }

            if (ValuesCount < MinValuesCount)
            {
                yield return new ValidationResult($"Минимальное количество значений - {MinValuesCount}");
            }

            for (var i = 0; i < ValuesCount; i++)
            {
                if (string.IsNullOrWhiteSpace(Values.ElementAtOrDefault(i)))
                {
                    yield return new ValidationResult($"Значение {i + 1} не может быть пустым");
                }
            }
        }
    }
}
