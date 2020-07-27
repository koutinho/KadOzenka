using System.Collections.Generic;
using ObjectModel.Directory;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace KadOzenka.Web.Models.Tour
{
	public class ImportGroupDataModel
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

		public List<ValidationResult> Validate()
		{
			var errors = new List<ValidationResult>();

			if (!TourId.HasValue)
			{
				errors.Add(
					new ValidationResult(errorMessage: "Не выбран Тур",
						memberNames: new[] { nameof(TourId) }));
			}

			if (File == null)
			{
				errors.Add(
					new ValidationResult(errorMessage: "Файл не выбран",
						memberNames: new[] { nameof(File) }));
			}

			if (IsUnitStatusUsed)
			{
				if (!UnitStatus.HasValue)
				{

					errors.Add(
						new ValidationResult(errorMessage: "Не выбран Статус единицы оценки",
							memberNames: new[] { nameof(UnitStatus) }));
				}
			}
			else if (TaskFilter?.Count == null || TaskFilter?.Count == 0)
			{
				errors.Add(
					new ValidationResult(errorMessage: "Список заданий на оценку не может быть пустым",
						memberNames: new[] { nameof(TaskFilter) }));
			}

			return errors;
		}
	}
}
