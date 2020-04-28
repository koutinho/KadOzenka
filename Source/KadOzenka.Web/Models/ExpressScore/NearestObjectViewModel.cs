using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;

namespace CIPJS.Models.ExpressScore
{
	public enum DealTypeShort : long
	{
		[Description("Аренда")]
		Rent = 1,

		[Description("Продажа")]
		Sale = 2
	}

	public class NearestObjectViewModel
	{
		/// <summary>
		/// Площадь
		/// </summary>
		[Required(ErrorMessage = "Заполните площадь")]
		[Range(1, 5000, ErrorMessage = "Диапозон значений площади от 1 до 5000 кв. м")]
		public decimal? Square { get; set; }

		/// <summary>
		/// Сегмент рынка
		/// </summary>
		[Required(ErrorMessage = "Заполните сегмент рынка.")]
		public MarketSegment? Segment { get; set; }

		/// <summary>
		/// Количество аналогов для вывода
		/// </summary>
		[Required(ErrorMessage = "Не указано количество аналогов. Обратитесь к администратору.")]
		public int? Quality { get; set; }

		/// <summary>
		/// Широта точки от которой ищем объекты
		/// </summary>
		[Required(ErrorMessage = "Не указана широта. Обратитесь к администратору.")]
		public decimal? SelectedLat { get; set; }

		/// <summary>
		/// Долгота точки от которой ищем объекты
		/// </summary>
		[Required(ErrorMessage = "Не указана долгота. Обратитесь к администратору.")]
		public decimal? SelectedLng { get; set; }

		/// <summary>
		/// Адрес объекта 
		/// </summary>
		[Required(ErrorMessage = "Не указан адрес")]
		public string Address { get; set; }

		/// <summary>
		/// Тип сделки
		/// </summary>
		[Required(ErrorMessage = "Не указан тип сделки")]
		public DealTypeShort DealTypeShort { get; set; }

		public List<DealType> DealType
		{
			get
			{
				var dealTypes = new List<DealType>(2);
				if (DealTypeShort == DealTypeShort.Rent)
				{
					dealTypes.Add(ObjectModel.Directory.DealType.RentDeal);
					dealTypes.Add(ObjectModel.Directory.DealType.RentSuggestion);
				}
				if(DealTypeShort == DealTypeShort.Sale)
				{
					dealTypes.Add(ObjectModel.Directory.DealType.SaleDeal);
					dealTypes.Add(ObjectModel.Directory.DealType.SaleSuggestion);
				}

				return dealTypes;
			}
		}

		/// <summary>
		/// Дата актуальности
		/// </summary>
		[Required(ErrorMessage = "Не указана дата актуальности")]
		public DateTime? ActualDate { get; set; }

	}
}