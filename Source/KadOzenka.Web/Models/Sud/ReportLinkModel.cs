using System.ComponentModel.DataAnnotations;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
	public class ReportLinkModel
	{
		/// <summary>
		/// Ид отчета
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Идентификатор отчета
		/// </summary>
		[Required(ErrorMessage = "Поле Отчет об оценке обязательное")]
		[Range(1, long.MaxValue, ErrorMessage = "Поле Отчет об оценке обязательное")]
		public long IdReport { get; set; }

		/// <summary>
		/// Рыночная стоимость
		/// </summary>
		[Required(ErrorMessage = "Поле рыночная стоимость обязательное")]
		[Range(0.00001, long.MaxValue, ErrorMessage = "Недопустимое значение рыночной стоимости")]
		public decimal? Rs { get; set; }

		/// <summary>
		/// Удельная стоимость
		/// </summary>
		[Required(ErrorMessage = "Поле удельная стоимость обязательное")]
		[Range(0.00001, long.MaxValue, ErrorMessage = "Недопустимое значение удельной стоимости")]
		public decimal? Uprs { get; set; }

		/// <summary>
		/// Текущее использование
		/// </summary>
		public string Use { get; set; }

		/// <summary>
		/// Примечание
		/// </summary>
		public string Descr { get; set; }

		/// <summary>
		/// Номер отчета
		/// </summary>
		public string ReportNumber { get; set; }
		/// <summary>
		/// Номер отчета
		/// </summary>
		[Required(ErrorMessage = "Ид объекта не указан")]
		[Range(1, long.MaxValue, ErrorMessage = "Недопустимое значение ид объекта")]
		public long SudObjectId { get; set; }

		public static ReportLinkModel FromEntity(OMOtchetLink entity, OMOtchet report)
		{
			var res = new ReportLinkModel
			{
				Id = entity.Id,
				IdReport = entity.IdOtchet.GetValueOrDefault(),
				Descr = entity.Descr,
				Rs = entity.Rs.GetValueOrDefault(),
				Uprs = entity.Uprs.GetValueOrDefault(),
				Use = entity.Use,
				ReportNumber = report.Number
			};

			return res;

		}

		public static void ToEntity(ReportLinkModel model, ref OMOtchetLink reportLink)
		{

			reportLink.IdOtchet = model.IdReport;
			reportLink.Rs = model.Rs;
			reportLink.Uprs = model.Uprs;
			reportLink.Descr = model.Descr;
			reportLink.Use = model.Use;
			reportLink.IdObject = model.SudObjectId;
		}
	}

}