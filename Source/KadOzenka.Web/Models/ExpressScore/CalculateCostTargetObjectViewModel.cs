﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Enum;
using ObjectModel.Directory.ES;

namespace KadOzenka.Web.Models.ExpressScore
{
	public class CalculateCostTargetObjectViewModel
	{
		/// <summary>
		/// Площадь
		/// </summary>
		[Required(ErrorMessage = "Заполните поле площадь")]
		public decimal? Square { get; set; }

		/// <summary>
		/// Этаж
		/// </summary>
		[Required(ErrorMessage = "Заполните поле этаж")]
		public int? Floor { get; set; }

		/// <summary>
		/// Ид найденных аналогов
		/// </summary>
		[Required(ErrorMessage = "Не найдены объкты аналоги")]
		public List<int> SelectedPoints { get; set; }

		/// <summary>
		/// Ид объекта оценки 
		/// </summary>
		[Required(ErrorMessage = "Не найден целевой объект")]
		public int? TargetObjectId { get; set; }

		/// <summary>
		/// Сценарий расчета
		/// </summary>
		public ScenarioType ScenarioType { get; set; }
	}
}