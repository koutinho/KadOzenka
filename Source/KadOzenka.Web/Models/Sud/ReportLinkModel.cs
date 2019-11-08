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
		/// Рыночная стоимость
		/// </summary>
		[Required(ErrorMessage = "Поле Рыночная стоимост обязательное")]
		public decimal? Rs { get; set; }

		/// <summary>
		/// Удельная стоимость
		/// </summary>
		[Required(ErrorMessage = "Поле Удельная стоимост обязательное")]
		public decimal? Uprs { get; set; }

		/// <summary>
		/// Текущее использование
		/// </summary>
		[Required(ErrorMessage = "Поле Текущее использование обязательное")]
		public string Use { get; set; }

		/// <summary>
		/// Примечание
		/// </summary>
		public string Descr { get; set; }

		public static ReportLinkModel FromEntity(OMOtchetLink entity)
		{
			var res = new ReportLinkModel();
			res.Id = entity.Id;
			res.Descr = entity.Descr;
			res.Rs = entity.Rs.GetValueOrDefault();
			res.Uprs = entity.Uprs.GetValueOrDefault();
			res.Use = entity.Use;

			return res;

		}
	}

}