using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.Sud;
using Core.Shared.Extensions;
using ObjectModel.Directory.Sud;

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
		public SudObjectType ObjectType { get; set; }
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
		[Display(Name = "Группа")]
		public string DrsGroup { get; set; }
		[Display(Name = "Подвал")]
		public decimal? Basement { get; set; }
		[Display(Name = "Цоколь")]
		public decimal? Socle { get; set; }
		[Display(Name = "Торговля")]
		public decimal? Trade { get; set; }
		[Display(Name = "Офис")]
		public decimal? Office { get; set; }
		[Display(Name = "Производство")]
		public decimal? Production { get; set; }
		[Display(Name = "Гаражи, паркинг")]
		public decimal? Parking { get; set; }
		[Display(Name = "Социальное")]
		public decimal? Social { get; set; }
		[Display(Name = "Апартаменты")]
		public decimal? Apartments { get; set; }
		[Display(Name = "Иное назначение(15.7)")]
		public decimal? OtherPurpose { get; set; }
		[Display(Name = "Техническое состояние")]
		public string TechnicalCondition { get; set; }
		[Display(Name = "Причина пересчета")]
		public string RecountReason { get; set; }
		[Display(Name = "УПДРС")]
		public decimal? Updrs { get; set; }
		[Display(Name = "ДРС")]
		public decimal? Drs { get; set; }
		[Display(Name = "Источник")]
		public string DrsOwner { get; set; }


		public static ObjectCardModel FromOM(OMObject omObject, OMDRS omDrs)
		{
			var model = new ObjectCardModel
			{
				Id = omObject.Id,
				Kn = omObject.Kn,
				Date = omObject.Date,
				Square = omObject.Square,
				Kc = omObject.Kc,
				ObjectType = omObject.Typeobj_Code,
				Address = omObject.Adres,
				NameCenter = omObject.NameCenter,
				StatDgi = omObject.StatDgi,
				Owner = omObject.Owner,
				AdditionalAnalysisRequired = Convert.ToBoolean(omObject.Workstat_Code),
				DrsGroup = omDrs.DrsGroup,
				Basement = omDrs.DrsSq1,
				Socle = omDrs.DrsSq2,
				Trade = omDrs.DrsSq3,
				Office = omDrs.DrsSq4,
				Production = omDrs.DrsSq5,
				Parking = omDrs.DrsSq6,
				Social = omDrs.DrsSq7,
				Apartments = omDrs.DrsSq8,
				OtherPurpose = omDrs.DrsSq9,
				TechnicalCondition = omDrs.DrsSost,
				RecountReason = omDrs.DrsPrichin,
				Updrs = omDrs.DrsUpdrs,
				Drs = omDrs.DrsDrs,
				DrsOwner = omDrs.DrsOwner
			};
			return model;
		}

		public static void ToOM(ObjectCardModel model, ref OMObject omObject, ref OMDRS omDrs)
		{
			if (omObject == null)
			{
				throw new ArgumentNullException(nameof(OMObject));
			}
			if (omDrs == null)
			{
				throw new ArgumentNullException(nameof(OMDRS));
			}

			omObject.Kn = model.Kn;
			omObject.Date = model.Date;
			omObject.Square = model.Square;
			omObject.Kc = model.Kc;
			omObject.Typeobj_Code = model.ObjectType;
			omObject.Adres = model.Address;
			omObject.NameCenter = model.NameCenter;
			omObject.StatDgi = model.StatDgi;
			omObject.Owner = model.Owner;
			omObject.Workstat = model.AdditionalAnalysisRequired.GetValueOrDefault(false) ? ProcessingStatus.Processed.GetEnumDescription() : ProcessingStatus.InWork.GetEnumDescription();

			omDrs.DrsGroup = model.DrsGroup;
			omDrs.DrsSq1 = model.Basement;
			omDrs.DrsSq2 = model.Socle;
			omDrs.DrsSq3 = model.Trade;
			omDrs.DrsSq4 = model.Office;
			omDrs.DrsSq5 = model.Production;
			omDrs.DrsSq6 = model.Parking;
			omDrs.DrsSq7 = model.Social;
			omDrs.DrsSq8 = model.Apartments;
			omDrs.DrsSq9 = model.OtherPurpose;
			omDrs.DrsSost = model.TechnicalCondition;
			omDrs.DrsPrichin = model.RecountReason;
			omDrs.DrsUpdrs = model.Updrs;
			omDrs.DrsDrs = model.Drs;
			omDrs.DrsOwner = model.DrsOwner;
		}
	}
}
