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
		[Display(Name = "Проверка декларации на соответствии утвержденной форме Приказа № 846 от 27.12.2016")]
		public long? MatchingApprovedFormCheck { get; set; }

		/// <summary>
		/// Проверка декларации на наличие основных данных, позволяющих идентифицировать объект (CHECK_POINT2)
		/// </summary>
		[Display(Name = "Проверка декларации на наличие основных данных, позволяющих идентифицировать объект")]
		public long? PresenceMainDataCheck { get; set; }

		/// <summary>
		/// Уведомление о продлении сроков рассмотрения (CHECK_POINT3)
		/// </summary>
		[Display(Name = "Уведомление о продлении сроков рассмотрения")]
		public long? NotificationOfConsiderationTermsProlongation { get; set; }

		/// <summary>
		/// Декларация, прошедшая формальную проверку, подлежит дальнейшей проверке (CHECK_POINT4)
		/// </summary>
		[Display(Name = "Декларация, прошедшая формальную проверку, подлежит дальнейшей проверке")]
		public long? FurtherDeclarationCheck { get; set; }

		/// <summary>
		/// Плановая дата рассмотрения (DATE_CHECK_PLAN)
		/// </summary>
		[Display(Name = "Плановая дата рассмотрения")]
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
		public string ApprovedCharacteristic { get; set; }

		/// <summary>
		/// Отрицательный отзыв по декларации ("TEXT_NO")
		/// </summary>
		[Display(Name = "Непринятые хар-ки")]
		public string RejectedCharacteristic { get; set; }

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
				MatchingApprovedFormCheck = (long)entity.CheckPoint1_Code,
				PresenceMainDataCheck = (long)entity.CheckPoint2_Code,
				NotificationOfConsiderationTermsProlongation = (long)entity.CheckPoint3_Code,
				FurtherDeclarationCheck = (long)entity.CheckPoint4_Code,
				DateCheckPlan = entity.DateCheckPlan,
				DateCheckFact = entity.DateCheckFact,
				ApprovedCharacteristic = result.TextYes,
				RejectedCharacteristic = result.TextNo
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
			if (declarationViewModel.NotificationOfConsiderationTermsProlongation.HasValue)
			{
				entity.CheckPoint3_Code = (CheckStatus)declarationViewModel.NotificationOfConsiderationTermsProlongation.GetValueOrDefault();
			}
			if (declarationViewModel.FurtherDeclarationCheck.HasValue)
			{
				entity.CheckPoint4_Code = (CheckStatus)declarationViewModel.FurtherDeclarationCheck.GetValueOrDefault();
			}
			entity.DateCheckPlan = declarationViewModel.DateCheckPlan;
			entity.DateCheckFact = declarationViewModel.DateCheckFact;
			result.TextYes = declarationViewModel.ApprovedCharacteristic;
			result.TextNo = declarationViewModel.RejectedCharacteristic;
		}
	}
}
