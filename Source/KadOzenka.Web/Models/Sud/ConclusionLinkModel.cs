using System.ComponentModel.DataAnnotations;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
	public class ConclusionLinkModel
	{
		/// <summary>
		/// ид модели
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		///  Идентификатор объекта
		/// </summary>
		public long? IdObject { get; set; }

		/// <summary>
		/// Идентификатор заключения
		/// </summary>
		[Required(ErrorMessage = "Поле экспертное заключение обязательное")]
		[Range(1, long.MaxValue, ErrorMessage = "Поле экспертное заключение  обязательное")]
		public long? IdConclusion { get; set; }

		/// <summary>
		/// Текущее использование
		/// </summary>
		public string Use { get; set; }

		/// <summary>
		///Рыночная стоимость 
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
		/// Примечание
		/// </summary>
		public string Descr { get; set; }

		/// <summary>
		/// Ид объекта
		/// </summary>
		public long SudObjectId { get; set; }

		/// <summary>
		/// Номер заключения
		/// </summary>
		public string ConclusionNumber { get; set; }

		public static ConclusionLinkModel FromEntity(OMZakLink conclusionLink, OMZak conclusion)
		{
			return new ConclusionLinkModel()
			{
				Id = conclusionLink.Id,
				IdConclusion = conclusionLink.IdZak,
				Descr = conclusionLink.Descr,
				Rs = conclusionLink.Rs.GetValueOrDefault(),
				Uprs = conclusionLink.Uprs.GetValueOrDefault(),
				Use = conclusionLink.Use,
				ConclusionNumber = conclusion.Number
			};

		}

		public static void ToEntity(ConclusionLinkModel model, ref OMZakLink conclusionLink)
		{

			conclusionLink.IdZak = model.IdConclusion;
			conclusionLink.Rs = model.Rs;
			conclusionLink.Uprs = model.Uprs;
			conclusionLink.Descr = model.Descr;
			conclusionLink.Use = model.Use;
			conclusionLink.IdObject = model.SudObjectId;
		}

	}
}