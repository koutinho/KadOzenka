using ObjectModel.Directory;
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
		public long? ObjectId { get; set; }
		public long? TaskId { get; set; }
		public long? CostRosreestrId { get; set; }
		
		[DisplayName("Тур оценки")]
		public long? Tour { get; set; }
		public long? TourId { get; set; }		
		[DisplayName("Тип статьи")]
		public KoNoteType? NoteType { get; set; }		
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
		public PropertyTypes? PropertyType { get; set; }	
		[DisplayName("Площать")]
		public decimal? Square { get; set; }
		[DisplayName("Статус задания")]
		public KoUnitStatus? Status { get; set; }	
		[DisplayName("Дата оценки")]
		public DateTime? UnitCreationDate { get; set; }

		[DisplayName("Наименование группы")]
		public string GroupName { get; set; }
		public long? GroupId { get; set; }
		[DisplayName("УПКС (предварительный)")]
		public decimal? UpksPre { get; set; }
		[DisplayName("КС (предварительная)")]
		public decimal? CadastralCostPre { get; set; }
		[DisplayName("УПКС (окончательный)")]
		public decimal? Upks { get; set; }
		[DisplayName("КС (окончательная)")]
		public decimal? CadastralCost { get; set; }
		[DisplayName("Статус расчета")]
		public KoStatusRepeatCalc? StatusRepeatCalc { get; set; }		
		[DisplayName("Анализ стоимости")]
		public KoStatusResultCalc? StatusResultCalc { get; set; }		
		[DisplayName("Тип объекта, по которому рассчитана КС")]
		public KoParentCalcType? ParentCalcType { get; set; }		
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
