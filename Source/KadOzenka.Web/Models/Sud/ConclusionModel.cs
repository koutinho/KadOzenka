using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
	public class ConclusionModel
	{
		/// <summary>
		/// Ид отчета
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Номер отчета
		/// </summary>
		[Display(Name = "Номер заключения")]
		[Required(ErrorMessage = "Поле номер заключения обязательное")]
		public string Number { get; set; }

		/// <summary>
		/// Дата отчета
		/// </summary>
		[Display(Name = "Дата составления")]
		[Required(ErrorMessage = "Поле дата составления обязательное")]
		public DateTime? CreateDate { get; set; }

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
		/// Дата  сдачи рецензии
		/// </summary>
		[Display(Name = "Дата сдачи рецензии")]
		public DateTime? RecDate { get; set; }

		/// <summary>
		/// Исполнитель рецензии
		/// </summary>
		[Display(Name = "Исполнитель рецензии")]
		public string RecUser { get; set; }

		/// <summary>
		/// Номер письма
		/// </summary>
		[Display(Name = "Номер письма")]
		public string RecLetter { get; set; }

		/// <summary>
		/// Предварительная рецензия
		/// </summary>
		public bool RecBefore { get; set; }

		/// <summary>
		/// Рецензия после анализа
		/// </summary>
		public bool RecAfter { get; set; }

		/// <summary>
		/// Согласовано с руководителем
		/// </summary>
		public bool RecSoglas { get; set; }
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



		public static ConclusionModel FromEntity(OMZak conclusion)
		{
			return new ConclusionModel()
			{
				Id = conclusion.Id,
				CreateDate = conclusion.Date,
				Fio = conclusion.Fio,
				IdFio = conclusion.IdFio,
				IdOrg = conclusion.IdOrg,
				IdSro = conclusion.IdSro,
				Sro = conclusion.Sro,
				Number = conclusion.Number,
				Org = conclusion.Org,
				RecAfter = Convert.ToBoolean(conclusion.RecAfter),
				RecBefore = Convert.ToBoolean(conclusion.RecBefore),
				RecDate = conclusion.RecDate,
				RecLetter = conclusion.RecLetter,
				RecSoglas = Convert.ToBoolean(conclusion.RecSoglas),
				RecUser = conclusion.RecUser
			};
		}

		public static void ToEntity(ConclusionModel model, ref OMZak conclusion)
		{
			conclusion.Date = model.CreateDate;
			conclusion.Fio = model.Fio;
			conclusion.IdFio = model.IdFio;
			conclusion.IdOrg = model.IdOrg;
			conclusion.Org = model.Org;
			conclusion.IdSro = model.IdSro;
			conclusion.Sro = model.Sro;
			conclusion.Number = model.Number;
			conclusion.RecLetter = model.RecLetter;
			conclusion.RecDate = model.RecDate;
			conclusion.RecAfter = Convert.ToInt64(model.RecAfter);
			conclusion.RecBefore = Convert.ToInt64(model.RecBefore);
			conclusion.RecSoglas = Convert.ToInt64(model.RecSoglas);
			conclusion.RecUser = model.RecUser;
		}
	}
}