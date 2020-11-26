using System.Collections.Generic;
using ObjectModel.Directory;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.DataImport.Dto;
using Microsoft.AspNetCore.Http;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Tour
{
	public class ImportDataModel : IValidatableObject
	{
		[Display(Name = "Тур")]
		public long? TourId { get; set; }

		/// <summary>
		/// Использовать Статус единицы оценки
		/// </summary>
		public bool IsUnitStatusUsed { get; set; } = true;

		[Display(Name = "Статус единицы оценки")]
		public KoUnitStatus? UnitStatus { get; set; }

		/// <summary>
		/// Список значений фильтра
		/// </summary>
		[Display(Name = "Задания на оценку")]
		public List<long> TaskFilter { get; set; }

		public IFormFile File { get; set; }

		/// <summary>
		/// Тип загрузки
		/// </summary>
		public LoadType LoadTypeDocument { get; set; }


		public ImportDataFromExcelDto ToDto()
		{
			return new ImportDataFromExcelDto
			{
				FileName = File.FileName,
				TourId = TourId,
				IsUnitStatusUsed = IsUnitStatusUsed,
				UnitStatus = UnitStatus,
				TaskFilter = TaskFilter,
				MainRegisterId = OMTour.GetRegisterId(),
				RegisterViewId = "KoTours",
				LoadType = (LoadType)LoadTypeDocument
			};
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!TourId.HasValue)
			{
				yield return
					new ValidationResult(errorMessage: "Не выбран Тур",
						memberNames: new[] { nameof(TourId) });
			}

			if (File == null)
			{
				yield return
					new ValidationResult(errorMessage: "Файл не выбран",
						memberNames: new[] { nameof(File) });
			}

			if (IsUnitStatusUsed)
			{
				if (!UnitStatus.HasValue || UnitStatus == KoUnitStatus.None)
				{
					yield return
						new ValidationResult(errorMessage: "Не выбран Статус единицы оценки",
							memberNames: new[] { nameof(UnitStatus) });
				}
			}
			else if (TaskFilter?.Count == null || TaskFilter?.Count == 0)
			{
				yield return
					new ValidationResult(errorMessage: "Список заданий на оценку не может быть пустым",
						memberNames: new[] { nameof(TaskFilter) });
			}

			if ((LoadType)LoadTypeDocument == LoadType.None)
			{
				yield return new ValidationResult("Не указан тип загрузки", new[] { nameof(LoadType) });
			}
		}
	}
}
