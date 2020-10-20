using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using DevExpress.CodeParser;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.ExpressScore.Dto;
using Newtonsoft.Json;
using ObjectModel.Directory;
using RestSharp.Deserializers;

namespace CIPJS.Models.ExpressScore
{

	public class NearestObjectViewModel
	{
		/// <summary>
		/// Площадь
		/// </summary>
		//public decimal? Square { get; set; }

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
		/// Кадастровый номер
		/// </summary>
		[Required(ErrorMessage = "Не указан кадастровый номер")]
		public string Kn { get; set; }


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

		/// <summary>
		/// Учитывать или нет год постройки для поиска объектов
		/// </summary>
		//public bool UseYearBuild { get; set; }

		/// <summary>
		/// Учитывать или нет площадь для поиска объектов
		/// </summary>
		//public bool UseSquare { get; set; }

		/// <summary>
		/// Сериализованный список объектов SearchParameter для поиска
		/// </summary>
		public string SearchParameters { get; set; }

		/// <summary>
		/// Десерилизованный список параметров для поиска
		/// </summary>
		public List<SearchAttribute> DeserializeSearchParameters => JsonConvert.DeserializeObject<List<SearchAttribute>>(SearchParameters ?? string.Empty);

	}
}