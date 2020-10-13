using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory.Sud;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
	public class CourtModel
	{
		/// <summary>
		/// Ид суда
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Наименование суда
		/// </summary>
		[Display(Name = "Наименование суда")]
		[Required(ErrorMessage = "Поле наименование суда обязательное")]
		public string Name { get; set; }
		/// <summary>
		/// Номер судебного дела
		/// </summary>
		[Display(Name = "Номер дела")]
		[Required(ErrorMessage = "Поле номер дела обязательное")]
		public string Number { get; set; }
		/// <summary>
		/// Дата заседания
		/// </summary>
		[Display(Name = "Дата заседания")]
		public DateTime? Date { get; set; }
		/// <summary>
		/// Дата судебного акта
		/// </summary>
		[Display(Name = "Дата судебного акта")]
		public DateTime? SudDate { get; set; }
		/// <summary>
		/// Статус дела
		/// </summary>
		[Display(Name = "Статус дела")]
		public long? Status { get; set; }
		/// <summary>
		/// Архивный номер
		/// </summary>
		[Display(Name = "Номер архивный")]
		public string ArchiveNumber { get; set; }
		/// <summary>
		/// Номер апелляции
		/// </summary>
		[Display(Name = "Номер апелляции")]
		public string AppealNumber { get; set; }
		/// <summary>
		/// Дата определения (апелляция)
		/// </summary>
		[Display(Name = "Дата определения (апелляция)")]
		public DateTime? AppealDate { get; set; }


		public bool IsEditCourt { get; set; }

		public int RegisterId { get; } = OMSud.GetRegisterId();

		public static CourtModel FromEntity(OMSud entity)
		{
			return new CourtModel()
			{
				Id = entity.Id,
				Name = entity.Name,
				Number = entity.Number,
				Date = entity.Date,
				SudDate = entity.SudDate,
				Status = (long)entity.Status_Code,
				ArchiveNumber = entity.ArchiveNumber,
				AppealNumber = entity.AppealNumber,
				AppealDate = entity.AppealDate
			};
		}

		public static void ToEntity(CourtModel model, ref OMSud entity)
		{
			entity.Name = model.Name;
			entity.Number = model.Number;
			entity.Date = model.Date;
			entity.SudDate = model.SudDate;
			entity.Status_Code = (CourtStatus)(model.Status ?? 0);
			entity.AppealNumber = model.AppealNumber;
			entity.AppealDate = model.AppealDate;
			entity.ArchiveNumber = model.ArchiveNumber;
		}
	}
}