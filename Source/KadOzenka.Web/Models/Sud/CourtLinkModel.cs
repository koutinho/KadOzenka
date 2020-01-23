using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
	public class CourtLinkModel
	{
		/// <summary>
		/// Ид
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Идентификатор объекта
		/// </summary>
		[Required(ErrorMessage = "Ид объекта не указан")]
		[Range(1, long.MaxValue, ErrorMessage = "Недопустимое значение ид объекта")]
		public long ObjectId { get; set; }

		/// <summary>
		/// Идентификатор судебного дела
		/// </summary>
		[Display(Name = "Судебное решение")]
		[Required(ErrorMessage = "Поле Судебное решение обязательное")]
		[Range(1, long.MaxValue, ErrorMessage = "Поле Судебное решение обязательное")]
		public long SudId { get; set; }

		/// <summary>
		/// Номер судебного решения
		/// </summary>
		public string SudNumber { get; set; }

		/// <summary>
		/// Рыночная стоимость
		/// </summary>
		[Display(Name = "Рыночная стоимость")]
		[Required(ErrorMessage = "Поле рыночная стоимость обязательное")]
		[Range(0.00001, long.MaxValue, ErrorMessage = "Недопустимое значение рыночной стоимости")]
		public decimal? Rs { get; set; }


		/// <summary>
		/// Удельный показатель
		/// </summary>
		[Display(Name = "Удельный показатель")]
		[Required(ErrorMessage = "Поле удельный показатель обязательное")]
		[Range(0.00001, long.MaxValue, ErrorMessage = "Недопустимое значение удельного показателя")]
		public decimal? Uprs { get; set; }

		/// <summary>
		/// Источник информации
		/// </summary>
		[Display(Name = "Источник информации")]
		public string Use { get; set; }

		/// <summary>
		/// Примечание
		/// </summary>
		[Display(Name = "Примечание")]
		public string Description { get; set; }

		public bool IsEditCourt { get; set; }
		public bool IsEditCourtLink { get; set; }
		public decimal SquareObject { get; set; }
		public static CourtLinkModel FromEntity(OMSudLink omSudLink, OMSud omSud)
		{
			var res = new CourtLinkModel
			{
				Id = omSudLink.Id,
				SudId = omSud.Id,
				SudNumber = !string.IsNullOrEmpty(omSud.Number) && omSud.Date != null ? $"{omSud.Number} от {omSud.Date.GetString()}" : !string.IsNullOrEmpty(omSud.Number) ? omSud.Number : "",
				Rs = omSudLink.Rs.GetValueOrDefault(),
				Use = omSudLink.Use,
				Uprs = omSudLink.Uprs,
				Description = omSudLink.Descr,
			};

			return res;
		}

		public static void ToEntity(CourtLinkModel model, ref OMSudLink courtLink)
		{
			courtLink.IdSud = model.SudId;
			courtLink.Rs = model.Rs;
			courtLink.Descr = model.Description;
			courtLink.Use = model.Use;
			courtLink.IdObject = model.ObjectId;
			courtLink.Uprs = model.Uprs;
		}
	}

}