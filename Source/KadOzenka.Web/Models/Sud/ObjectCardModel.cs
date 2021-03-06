using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.Sud;
using Core.Shared.Extensions;
using Core.SRD;
using ObjectModel.Directory.Sud;

namespace KadOzenka.Web.Models.Sud
{
	public class ObjectCardModel
	{
		public long Id { get; set; }
		[Display(Name = "Кадастровый номер")]
		[Required(ErrorMessage = "Кадастровый номер обязательное поле")]
		public string Kn { get; set; }

		[Display(Name = "Дата определения стоимости")]
		[Required(ErrorMessage = "Дата определения стоимости обязательное поле")]
		public DateTime? Date { get; set; }

		[Display(Name = "Площадь")]
		[Required(ErrorMessage = "Площадь обязательное поле")]
		[Range(1, int.MaxValue, ErrorMessage = "Площадь обязательное поле")]
		public decimal? Square { get; set; }
		[Display(Name = "Оспариваемая стоимость")]
		public decimal? Kc { get; set; }

		
		[Display(Name = "Тип объекта")]
		[Range(1, int.MaxValue, ErrorMessage = "Тип объекта обязательное поле")]
		[Required(ErrorMessage = "Тип объекта обязательное поле")]
		public SudObjectType? ObjectType { get; set; }
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

		[Display(Name = "Тип заявителя")]
		[Required(ErrorMessage = "Тип заявителя обязательное поле")]
		[Range(1, int.MaxValue, ErrorMessage = "Тип заявителя обязательное поле")]
		public ApplicantType? ApplicantType { get; set; }

		[Display(Name = "Форма собственности")]
		public TypeOfOwnership? TypeOfOwnership { get; set; }

		[Display(Name = "Исключение")]
		public bool? IsException { get; set; }

		[Display(Name = "Решение вступило в законную силу")]
		public bool? IsDecisionEnteredIntoForce { get; set; }

		public bool IsEditPermission { get; set; }

		public bool IsApprovePermission { get; set; }

		public bool IsRemovedObject { get; set; }

		public bool IsDecisionEnteredIntoForcePermission { get; set; }


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
				AdditionalAnalysisRequired = Convert.ToBoolean(omObject.AdditionalAnalysis),
				DrsGroup = omDrs?.DrsGroup,
				Basement = omDrs?.DrsSq1,
				Socle = omDrs?.DrsSq2,
				Trade = omDrs?.DrsSq3,
				Office = omDrs?.DrsSq4,
				Production = omDrs?.DrsSq5,
				Parking = omDrs?.DrsSq6,
				Social = omDrs?.DrsSq7,
				Apartments = omDrs?.DrsSq8,
				OtherPurpose = omDrs?.DrsSq9,
				TechnicalCondition = omDrs?.DrsSost,
				RecountReason = omDrs?.DrsPrichin,
				Updrs = omDrs?.DrsUpdrs,
				Drs = omDrs?.DrsDrs,
				DrsOwner = omDrs?.DrsOwner,
				ApplicantType = omObject.ApplicantType_Code,
				TypeOfOwnership = omObject.TypeOfOwnership_Code,
				IsException = Convert.ToBoolean(omObject.Exception),
				IsRemovedObject = omObject.IsRemoved.GetValueOrDefault(),
				IsDecisionEnteredIntoForce = omObject.IsDecisionEnteredIntoForce.GetValueOrDefault()
			};

			model.IsEditPermission = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_EDIT);
			model.IsApprovePermission =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_RESH_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_OTCHET_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_RESH_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OTCHET_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_ZAK_APPROVE) ||
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK_APPROVE);
			model.IsDecisionEnteredIntoForcePermission = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS_EDIT_DECISION_ENTERED_INTO_FORCE);

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
			omObject.Typeobj_Code = model.ObjectType.GetValueOrDefault();
			omObject.Adres = model.Address;
			omObject.NameCenter = model.NameCenter;
			omObject.StatDgi = model.StatDgi;
			omObject.Owner = model.Owner;
			omObject.AdditionalAnalysis = Convert.ToInt16(model.AdditionalAnalysisRequired);
			omObject.ApplicantType_Code = model.ApplicantType.GetValueOrDefault();
			omObject.TypeOfOwnership_Code = model.TypeOfOwnership.GetValueOrDefault();
			omObject.Exception = Convert.ToInt16(model.IsException);
			omObject.IsDecisionEnteredIntoForce = model.IsDecisionEnteredIntoForce;

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
