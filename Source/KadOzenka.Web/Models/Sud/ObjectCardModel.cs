using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
	public class ObjectCardModel
	{
		public long Id { get; set; }
		[Display(Name = "Кадастровый номер")]
		public string Kn { get; set; }
		[Display(Name = "Дата определения стоимости")]
		public DateTime? Date { get; set; }
		[Display(Name = "Площадь")]
		public decimal? Square { get; set; }
		[Display(Name = "Оспариваемая стоимость")]
		public decimal? Kc { get; set; }
		[Display(Name = "Тип объекта")]
		public long? ObjectType { get; set; }
		[Display(Name = "Адрес")]
		public string Address { get; set; }
		[Display(Name = "Наименование (ТЦ, БЦ)")]
		public string NameCenter { get; set; }
		[Display(Name = "Внесено в статистику ДГИ")]
		public string StatDgi { get; set; }
		[Display(Name = "Заказчик / Истец")]
		public string Owner { get; set; }
		[Display(Name = "Требуется дополнительный анализ")]
		public bool? AdditionalAnalysisRequired { get; set; }

		public static ObjectCardModel FromOMObject(OMObject omObject)
		{
			return new ObjectCardModel
			{
				Kn = omObject.Kn,
				Date = omObject.Date,
				Square = omObject.Square,
				Kc = omObject.Kc,
				ObjectType = omObject.Typeobj,
				Address = omObject.Adres,
				NameCenter = omObject.NameCenter,
				StatDgi = omObject.StatDgi,
				Owner = omObject.Owner,
				AdditionalAnalysisRequired = omObject.Workstat != null && Convert.ToBoolean(omObject.Workstat.Value)
			};
		}
	}
}
