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
				Status = (long)entity.Status_Code
			};
		}

		public static void ToEntity(CourtModel model, ref OMSud entity)
		{
			entity.Name = model.Name;
			entity.Number = model.Number;
			entity.Date = model.Date;
			entity.SudDate = model.SudDate;
			entity.Status_Code = (CourtStatus)(model.Status ?? 0);
		}
	}
}