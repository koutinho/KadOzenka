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
		[Range(1, long.MaxValue, ErrorMessage = "Недопустимый идентификатор отчета")]
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

		public static ReportLinkModel FromEntity(OMOtchetLink entity)
		{
			var res = new ReportLinkModel
			{
				Id = entity.Id,
				IdReport = entity.IdOtchet.GetValueOrDefault(),
				Descr = entity.Descr,
				Rs = entity.Rs.GetValueOrDefault(),
				Uprs = entity.Uprs.GetValueOrDefault(),
				Use = entity.Use
			};

			return res;

		}

		public OMOtchetLink ToEntity(OMOtchetLink entity)
		{
			if (entity == null)
			{
				return new OMOtchetLink()
				{
					IdOtchet = IdReport,
					Rs = Rs,
					Uprs = Uprs,
					Descr = Descr,
					Use = Use
				};

			}

			entity.IdOtchet = IdReport;
			entity.Rs = Rs;
			entity.Uprs = Uprs;
			entity.Descr = Descr;
			entity.Use = Use;

			return entity;
		}
	}

}