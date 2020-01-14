using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace KadOzenka.Web.Models.Task
{
	public class UnitDto
	{
		public long? Id { get; set; }

		[DisplayName("Тур оценки")]
		public long? TourId { get; set; }
		public long? Tour { get; set; }
		[DisplayName("Тип статьи")]
		public string NoteType { get; set; }
		[DisplayName("Входящий документ")]
		public long? DocumentId { get; set; }
		public string Document { get; set; }
		[DisplayName("Дата создания")]
		public DateTime? TaskCreationDate { get; set; }

		[DisplayName("Кадастровый номер")]
		public string CadastralNumber { get; set; }
		[DisplayName("Кадастровый квартал")]
		public string CadastralBlock { get; set; }
		[DisplayName("Тип объекта")]
		public string PropertyType { get; set; }
		[DisplayName("Площать")]
		public decimal? Square { get; set; }
		[DisplayName("Статус задания")]
		public string Status { get; set; }
		[DisplayName("Дата оценки")]
		public DateTime? UnitCreationDate { get; set; }

		[DisplayName("Наименование группы")]
		public string GroupName { get; set; }
		[DisplayName("УПКС (предварительный)")]
		public decimal? UpksPre { get; set; }
		[DisplayName("КС (предварительная)")]
		public decimal? CadastralCostPre { get; set; }
		[DisplayName("УПКС (окончательный)")]
		public decimal? Upks { get; set; }
		[DisplayName("КС (окончательная)")]
		public decimal? CadastralCost { get; set; }
		[DisplayName("Статус расчета")]
		public string StatusRepeatCalc { get; set; }
		[DisplayName("Анализ стоимости")]
		public string StatusResultCalc { get; set; }
		[DisplayName("Тип объекта, по которому рассчитана КС")]
		public string ParentCalcType { get; set; }
		[DisplayName("Номер объекта, по которому рассчитана КС")]
		public string ParentCalcNumber { get; set; }
		[DisplayName("Исходящий документ")]
		public long? ResponseDocId { get; set; }
		public string ResponseDoc { get; set; }

		[DisplayName("Дата определения")]
		public DateTime? Datevaluation { get; set; }
		[DisplayName("Кадастровая стоимость")]
		public decimal? CostValue { get; set; }
		[DisplayName("Наименование документа")]
		public string DocName { get; set; }
		[DisplayName("Номер акта")]
		public string DocNumber { get; set; }
		[DisplayName("Дата акта")]
		public DateTime? DocDate { get; set; }
	}
}
