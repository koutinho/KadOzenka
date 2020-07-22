using System;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations.DeclarationTabModel
{
	public class DeclarationFormalCheckModel
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		[Display(Name = "Идентификатор")]
		public long Id { get; set; }

		/// <summary>
		/// Проверка декларации на соответствии утвержденной форме Приказа № 846 от 27.12.2016 (CHECK_POINT1)
		/// </summary>
		[Display(Name = "Проверка декларации на соответствие утвержденной форме Приказа № 318 от 04.06.2019")]
		public long? MatchingApprovedFormCheck { get; set; }

		/// <summary>
		/// Проверка декларации на наличие основных данных, позволяющих идентифицировать объект (CHECK_POINT2)
		/// </summary>
		[Display(Name = "Проверка декларации на наличие основных данных, позволяющих идентифицировать объект")]
		public long? PresenceMainDataCheck { get; set; }

		/// <summary>
		/// Декларация, прошедшая формальную проверку, подлежит дальнейшей проверке (CHECK_POINT4)
		/// </summary>
		[Display(Name = "Декларация, прошедшая формальную проверку, подлежит дальнейшей проверке")]
		public long? FurtherDeclarationCheck { get; set; }

		/// <summary>
		/// Плановая дата рассмотрения (DATE_CHECK_PLAN)
		/// </summary>
		[Display(Name = "Дата предварительного контроля")]
		public DateTime? DateCheckPlan { get; set; }

		/// <summary>
		/// Фактическая дата рассмотрения (DATE_CHECK_FACT)
		/// </summary>
		[Display(Name = "Фактическая дата рассмотрения")]
		public DateTime? DateCheckFact { get; set; }

		/// <summary>
		/// Положительный отзыв по декларации ("TEXT_YES")
		/// </summary>
		[Display(Name = "Принятые хар-ки")]
		[MaxLength(1024, ErrorMessage = "Максимальная длина значения для поля 'Принятые хар-ки' составляет 1024 символа")]
		public string ApprovedCharacteristic { get; set; }

		/// <summary>
		/// Отрицательный отзыв по декларации ("TEXT_NO")
		/// </summary>
		[Display(Name = "Непринятые хар-ки")]
		[MaxLength(1024, ErrorMessage = "Максимальная длина значения для поля 'Непринятые хар-ки' составляет 1024 символа")]
		public string RejectedCharacteristic { get; set; }

		/// <summary>
		/// Контрольный срок (CHECK_TIME)
		/// </summary>
		[Display(Name = "Контрольный срок")]
		public DateTime? CheckTime { get; set; }

		public static DeclarationFormalCheckModel FromEntity(OMDeclaration entity, OMResult result)
		{
			if (entity == null)
			{
				return new DeclarationFormalCheckModel
				{
					Id = -1
				};
			}

			return new DeclarationFormalCheckModel
			{
				Id = entity.Id,
				MatchingApprovedFormCheck = !string.IsNullOrEmpty(entity.CheckPoint1) ? (long)entity.CheckPoint1_Code : (long?)null,
				PresenceMainDataCheck = !string.IsNullOrEmpty(entity.CheckPoint2) ? (long)entity.CheckPoint2_Code : (long?)null,
				FurtherDeclarationCheck = !string.IsNullOrEmpty(entity.CheckPoint4) ? (long)entity.CheckPoint4_Code : (long?)null,
				DateCheckPlan = entity.DateCheckPlan,
				DateCheckFact = entity.DateCheckFact,
				ApprovedCharacteristic = result.TextYes,
				RejectedCharacteristic = result.TextNo,
				CheckTime = entity.CheckTime
			};
		}

		public static void ToEntity(DeclarationFormalCheckModel declarationViewModel, ref OMDeclaration entity, ref OMResult result)
		{
			if (declarationViewModel.MatchingApprovedFormCheck.HasValue)
			{
				entity.CheckPoint1_Code = (CheckStatus)declarationViewModel.MatchingApprovedFormCheck.GetValueOrDefault();
			}
			if (declarationViewModel.PresenceMainDataCheck.HasValue)
			{
				entity.CheckPoint2_Code = (CheckStatus)declarationViewModel.PresenceMainDataCheck.GetValueOrDefault();
			}
			if (declarationViewModel.FurtherDeclarationCheck.HasValue)
			{
				entity.CheckPoint4_Code = (CheckStatus)declarationViewModel.FurtherDeclarationCheck.GetValueOrDefault();
			}
			entity.DateCheckPlan = declarationViewModel.DateCheckPlan;
			entity.DateCheckFact = declarationViewModel.DateCheckFact;
			result.TextYes = declarationViewModel.ApprovedCharacteristic;
			result.TextNo = declarationViewModel.RejectedCharacteristic;
			entity.CheckTime = declarationViewModel.CheckTime;
		}
	}
}
