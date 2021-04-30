using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;
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


		[DisplayName("Кадастровый номер здания")]
		public string BuildingCadastralNumber { get; set; }
		[DisplayName("Кадастровый номер")]
		public string CadastralNumber { get; set; }
		[DisplayName("Кадастровый квартал")]
		public string CadastralBlock { get; set; }
		[DisplayName("Тип объекта")]
		public PropertyTypes? PropertyType { get; set; }	
		[DisplayName("Площадь")]
		public decimal? Square { get; set; }
		[DisplayName("Статус задания")]
		public KoUnitStatus? Status { get; set; }	
		[DisplayName("Дата создания")]
		public DateTime? UnitCreationDate { get; set; }
		[DisplayName("Дата оценки")]
		public DateTime? AssessmentDate { get; set; }

		[DisplayName("Наименование группы")]
		public string GroupName { get; set; }
		[DisplayName("Оценочная группа")]
		public string GroupNumber { get; set; }
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

		public static UnitDto ToDto(OMUnit unit)
		{
			UnitDto dto = new UnitDto();

			if (unit != null)
			{
				dto.Id = unit.Id;

				dto.ObjectId = unit.ObjectId;

				dto.CadastralNumber = unit.CadastralNumber;
				dto.BuildingCadastralNumber = unit.BuildingCadastralNumber;
				dto.CadastralBlock = unit.CadastralBlock;
				dto.PropertyType = unit.PropertyType_Code;
				dto.Square = unit.Square;
				dto.Status = unit.Status_Code;
				dto.UnitCreationDate = unit.CreationDate;
				dto.AssessmentDate = unit.AssessmentDate;

				dto.UpksPre = unit.UpksPre;
				dto.CadastralCostPre = unit.CadastralCostPre;
				dto.Upks = unit.Upks;
				dto.CadastralCost = unit.CadastralCost;
				dto.StatusRepeatCalc = unit.StatusRepeatCalc_Code;
				dto.StatusResultCalc = unit.StatusResultCalc_Code;
				dto.ParentCalcType = unit.ParentCalcType_Code;
				dto.ParentCalcNumber = unit.ParentCalcNumber;

				OMTask task = OMTask.Where(x => x.Id == unit.TaskId)
					.SelectAll()
					.ExecuteFirstOrDefault();

				if (task != null)
				{
					dto.TaskId = task.Id;

					dto.TourId = task.TourId;
					dto.NoteType = task.NoteType_Code;
					
					OMInstance doc = OMInstance.Where(x => x.Id == task.DocumentId)
						.Select(x => x.RegNumber).ExecuteFirstOrDefault();
					if (doc != null)
					{
						dto.DocumentId = doc.Id;
						dto.Document = doc.RegNumber;
					}

					OMInstance respDoc = OMInstance.Where(x => x.Id == task.ResponseDocId)
						.Select(x => x.RegNumber).ExecuteFirstOrDefault();
					if (respDoc != null)
					{
						dto.ResponseDocId = respDoc.Id;
						dto.ResponseDoc = respDoc.RegNumber;
					}

					dto.TaskCreationDate = task.CreationDate;					

					OMTour tour = OMTour.Where(x => x.Id == dto.TourId)
						.Select(x => x.Year)
						.ExecuteFirstOrDefault();
					dto.Tour = tour?.Year;
				}

				OMGroup group = OMGroup.Where(x => x.Id == unit.GroupId)
					.SelectAll()
					.ExecuteFirstOrDefault();

				if (group != null)
				{
					dto.GroupName = group.GroupName;
					dto.GroupNumber = group.Number;
					dto.GroupId = group.Id;
				}

				OMCostRosreestr costRosreestr = OMCostRosreestr.Where(x => x.IdObject == unit.Id)
					.SelectAll()
					.ExecuteFirstOrDefault();

				if (costRosreestr != null)
				{
					dto.CostRosreestrId = costRosreestr.Id;
					dto.Datevaluation = costRosreestr.Datevaluation;
					dto.CostValue = costRosreestr.Costvalue;
					dto.DocName = costRosreestr.Docname;
					dto.DocNumber = costRosreestr.Docnumber;
					dto.DocDate = costRosreestr.Docdate;
				}
			}

			return dto;
		}
	}
}
