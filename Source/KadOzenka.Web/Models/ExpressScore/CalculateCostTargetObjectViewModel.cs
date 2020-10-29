using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DevExpress.XtraRichEdit.Model;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.ExpressScore.Dto;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Directory.ES;

namespace KadOzenka.Web.Models.ExpressScore
{
	public class CalculateCostTargetObjectViewModel
	{
		/// <summary>
		/// Ид найденных аналогов
		/// </summary>
		[Required(ErrorMessage = "Не найдены объекты-аналоги")]
		public List<int> SelectedPoints { get; set; }

		/// <summary>
		/// Ид объекта оценки 
		/// </summary>
		[Required(ErrorMessage = "Не найден целевой объект")]
		public int? TargetObjectId { get; set; }

        /// <summary>
        /// Ид целевого объекта из market_core_object
        /// </summary>
        public long? TargetMarketObjectId { get; set; }

        /// <summary>
        /// Сценарий расчета
        /// </summary>
        public ScenarioType ScenarioType { get; set; }

		/// <summary>
		/// Сегмент рынка
		/// </summary>
		[Required(ErrorMessage = "Выберите сегмент")]
		public MarketSegment Segment { get; set; }

		/// <summary>
		/// Адрес целевого объекта
		/// </summary>
		[Required(ErrorMessage = "Не заполнен Адрес")]
		public string Address { get; set; }

		/// <summary>
		/// Кадастровый номер
		/// </summary>
		[Required(ErrorMessage = "Не заполнен кадастровый номер")]
		public string Kn { get; set; }

		/// <summary>
		/// Тип сделки
		/// </summary>
		[Required(ErrorMessage = "Не заполнен тип сделки")]
		public DealTypeShort DealType { get; set; }

		/// <summary>
		/// Сериализованные данные для расчета 
		/// </summary>
		public string ComplexCalculateParameters { get; set; }

		/// <summary>
		/// Десерилизованный список параметров для расчета
		/// </summary>
		public List<SearchAttribute> DeserializeComplexParameters => JsonConvert.DeserializeObject<List<SearchAttribute>>(ComplexCalculateParameters ?? string.Empty);
	}
}