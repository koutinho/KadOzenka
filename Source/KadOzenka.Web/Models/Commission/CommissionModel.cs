using System;
using System.ComponentModel.DataAnnotations;
using Core.RefLib;
using DevExpress.CodeParser;
using ObjectModel.Commission;
using ObjectModel.Directory.Commission;

namespace CIPJS.Models.Commission
{
	public class CommissionModel
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		[Display(Name = "Идентификатор")]
		public long Id { get; set; }
		/// <summary>
		/// Тип комиссии 
		/// </summary>
		[Display(Name = "Тип комиссии")]
		[Required(ErrorMessage = "Поле тип комиссии  обязательное")]
		[Range(1, int.MaxValue, ErrorMessage = "Поле тип комиссии  обязательное")]
		public long CommissionType { get; set; }
		/// <summary>
		/// Кадастровый номер объекта
		/// </summary>
		[Display(Name = "Кадастровый номер объекта")]
		public string Kn { get; set; }
		/// <summary>
		/// Оспариваемая кадастровая стоимость
		/// </summary>
		[Display(Name = "Оспариваемая кадастровая стоимость")]
		[Required(ErrorMessage = "Поле оспариваемая кадастровая стоимость  обязательное")]
		[Range(0.001, int.MaxValue, ErrorMessage = "Поле оспариваемая кадастровая стоимость  обязательное")]
		public decimal? Kc { get; set; }
		/// <summary>
		/// Дата определения кадастровой стоимости
		/// </summary>
		[Display(Name = "Дата определения кадастровой стоимости")]
		public DateTime? DateKc { get; set; }
		/// <summary>
		/// Номер заявления
		/// </summary>
		[Display(Name = "Номер заявления")]
		[Required(ErrorMessage = "Поле номер заявления  обязательное")]
		public string StatementNumber { get; set; }
		/// <summary>
		/// Дата заявления
		/// </summary>
		[Display(Name = "Дата заявления")]
		[Required(ErrorMessage = "Поле дата заявления  обязательное")]
		public DateTime? StatementDate { get; set; }
		/// <summary>
		/// Статус заявителя
		/// </summary>
		[Display(Name = "Статус заявителя")]
		[Required(ErrorMessage = "Поле статус заявителя  обязательное")]
		[Range(1, int.MaxValue, ErrorMessage = "Поле статус заявителя  обязательное")]
		public long? ApplicantStatus { get; set; }
		/// <summary>
		/// Номер решения
		/// </summary>
		[Display(Name = "Номер решения")]
		public string DecisionNumber { get; set; }
		/// <summary>
		/// Дата решения
		/// </summary>
		[Display(Name = "Дата решения")]
		public DateTime? DecisionDate { get; set; }
		/// <summary>
		/// Решение комиссии
		/// </summary>
		[Display(Name = "Решение комиссии")]
		public long? DecisionResult { get; set; }
		/// <summary>
		/// Рыночная стоимость после оспаривания
		/// </summary>
		[Display(Name = "Рыночная стоимость после оспаривания")]
		public decimal? MarketValue { get; set; }
		/// <summary>
		/// Кадастровая стоимость по решению 
		/// </summary>
		[Display(Name = "Кадастровая стоимость по решению")]
		public decimal? CommissionKc { get; set; }
		/// <summary>
		/// Группа после оспаривания
		/// </summary>
		[Display(Name = "Группа после оспаривания")]
		public string CommissionGroup { get; set; }
		/// <summary>
		/// Изменения характеристик
		/// </summary>
		[Display(Name = "Изменения характеристик")]
		public string CommissionChange { get; set; }


		public static CommissionModel FromEntity(OMCost entity)
		{
			if (entity == null) return new CommissionModel();

			return new CommissionModel()
			{
				Id = entity.Id,
				CommissionType = (long)entity.CommissionType_Code,
				CommissionKc = entity.CommissionKc,
				CommissionChange = entity.CommissionChange,
				CommissionGroup = entity.CommissionGroup,
				MarketValue = entity.MarketValue,
				DecisionNumber = entity.DecisionNumber,
				DecisionDate = entity.DecisionDate,
				DateKc = entity.DateKc,
				DecisionResult = (long)entity.DecisionResult_Code,
				StatementDate = entity.StatementDate,
				ApplicantStatus = (long)entity.ApplicantStatus_Code,
				Kn = entity.Kn,
				Kc = entity.Kc,
				StatementNumber = entity.StatementNumber

			};
		}

		public static void ToEntity(CommissionModel commissionViewModel, ref OMCost entity)
		{
			entity.MarketValue = commissionViewModel.MarketValue;
			entity.CommissionType_Code = (CommissionType)commissionViewModel.CommissionType;
			entity.CommissionKc = commissionViewModel.CommissionKc;
			entity.StatementDate = commissionViewModel.StatementDate;
			entity.DecisionNumber = commissionViewModel.DecisionNumber;
			entity.DateKc = commissionViewModel.DateKc;
			entity.Kn = commissionViewModel.Kn;
			entity.Kc = commissionViewModel.Kc;
			entity.StatementNumber = commissionViewModel.StatementNumber;
			entity.DecisionDate = commissionViewModel.DecisionDate;
			entity.DecisionResult_Code = (DecisionResult)commissionViewModel.DecisionResult;
			entity.ApplicantStatus_Code = (ApplicantStatus)commissionViewModel.ApplicantStatus;
			entity.CommissionChange = commissionViewModel.CommissionChange;
			entity.CommissionGroup = commissionViewModel.CommissionGroup;

			var items = ReferencesCommon.GetItems<DecisionResult>(true);
		}
	}
}