using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
	public class ReportModel
	{
		/// <summary>
		/// Ид отчета
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Номер отчета
		/// </summary>
		[Display(Name = "Номер отчета")]
		[Required(ErrorMessage = "Поле номер отчета обязательное")]
		public string Number { get; set; }
		/// <summary>
		/// Дата отчета
		/// </summary>
		[Display(Name = "Дата отчета")]
		[Required(ErrorMessage = "Поле дата отчета обязательное")]
		public DateTime? ReportDate { get; set; }
		/// <summary>
		/// Организация
		/// </summary>
		[Display(Name = "Организация")]
		public string Org { get; set; }
		/// <summary>
		/// ФИО оценщика
		/// </summary>
		[Display(Name = "Оценщик")]
		public string Fio { get; set; }
		/// <summary>
		/// СРО
		/// </summary>
		[Display(Name = "СРО")]
		[Required(ErrorMessage = "Поле СРО обязательное")]
		public string Sro { get; set; }
		/// <summary>
		/// Дата получения
		/// </summary>
		[Display(Name = "Дата получения")]
		public DateTime? DateIn { get; set; }
		/// <summary>
		/// Жалоба в СРО
		/// </summary>
		[Display(Name = "Жалоба в СРО")]
		public bool Claim { get; set; }

		/// <summary>
		/// ИД организации
		/// </summary>
		public long? IdOrg { get; set; }
		/// <summary>
		/// ИД оценщика
		/// </summary>
		public long? IdFio { get; set; }
		/// <summary>
		/// ИД СРО
		/// </summary>
		public long? IdSro { get; set; }

		public bool IsEditReport { get; set; }

		public int RegisterId { get; } = OMOtchet.GetRegisterId();
		public static ReportModel FromEntity(OMOtchet report)
		{
			return new ReportModel()
			{
				Id = report.Id,
				Claim = Convert.ToBoolean(report.Jalob.GetValueOrDefault()),
				DateIn = report.DateIn,
				ReportDate = report.Date,
				Fio = report.Fio,
				IdFio = report.IdFio,
				IdOrg = report.IdOrg,
				IdSro = report.IdSro,
				Sro = report.Sro,
				Number = report.Number,
				Org = report.Org
			};
		}

		public static void ToEntity(ReportModel model, ref OMOtchet report)
		{
			report.Date = model.ReportDate;
			report.DateIn = model.DateIn;
			report.Fio = model.Fio;
			report.IdFio = model.IdFio;
			report.IdOrg = model.IdOrg;
			report.Org = model.Org;
			report.IdSro = model.IdSro;
			report.Sro = model.Sro;
			report.Number = model.Number;
			report.Jalob = Convert.ToInt64(model.Claim);
		}
	}
}