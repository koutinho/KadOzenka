using System;
using System.ComponentModel.DataAnnotations;

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
		public string Number { get; set; }
		/// <summary>
		/// Дата отчета
		/// </summary>
		[Display(Name = "Дата отчета")]
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
	}
}