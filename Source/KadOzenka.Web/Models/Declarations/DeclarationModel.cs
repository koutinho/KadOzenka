using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Register.DAL;
using Core.Shared.Misc;
using Core.UI.Registers.Services;
using KadOzenka.Web.Controllers;
using KadOzenka.Web.Extensions;
using KadOzenka.Web.Models.Declarations.DeclarationTabModel;
using ObjectModel.Core.Shared;
using ObjectModel.Core.SRD;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class DeclarationModel : IValidatableObject
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		[Display(Name = "Идентификатор")]
		public long Id { get; set; }

		/// <summary>
		/// Идентификатор регистратора (USER_REG_ID)
		/// </summary>
		[Display(Name = "Регистратор")]
		public long? UserRegId { get; set; }

		/// <summary>
		/// Податель декларации (OWNER_TYPE)
		/// </summary>
		[Display(Name = "Подача декларации")]
		public OwnerType? OwnerType { get; set; }

		/// <summary>
		/// Отображение заявителя (он же собственник)
		/// </summary>
		public string OwnerDisplay { get; set; }

		/// <summary>
		/// Идентификатор заявителя (он же собственник) (OWNER_ID)
		/// </summary>
		[Display(Name = "Заявитель")]
		public long? OwnerId { get; set; }

		/// <summary>
		/// Отображение представителя заявителя
		/// </summary>
		public string AgentDisplay { get; set; }

		/// <summary>
		/// Идентификатор представителя заявителя (AGENT_ID)
		/// </summary>
		[Display(Name = "Представитель")]
		public long? AgentId { get; set; }

		/// <summary>
		/// Тип уведомления заявителя (UVED_TYPE_OWNER)
		/// </summary>
		[Display(Name = "Способ получения уведомления")]
		public SendUvedType? UvedTypeOwner { get; set; }

		/// <summary>
		/// Тип уведомления представителя заявителя (UVED_TYPE_AGENT)
		/// </summary>
		[Display(Name = "Способ получения уведомления")]
		public SendUvedType? UvedTypeAgent { get; set; }

		/// <summary>
		/// Название документа, удостоверяющего полномочия представителя заявителя (СERTIFICATE_NAME)
		/// </summary>
		[Display(Name = "Документ удостоверяющий полномочия")]
		public string CertificateName { get; set; }

		/// <summary>
		/// Номер документа, удостоверяющего полномочия  представителя заявителя (СERTIFICATE_NUM)
		/// </summary>
		[Display(Name = "Номер")]
		public string CertificateNum { get; set; }

		/// <summary>
		/// Дата документа, удостоверяющего полномочия представителя заявителя (СERTIFICATE_DATE)
		/// </summary>
		[Display(Name = "Дата")]
		public DateTime? CertificateDate { get; set; }

		/// <summary>
		/// Кадастровый номер объекта (CADASTRAL_NUM_OBJ)
		/// </summary>
		[Display(Name = "Кадастровый номер")]
		[Required(ErrorMessage = "Поле Кадастровый номер обязательное")]
		public string CadastralObjectNumber { get; set; }

		/// <summary>
		/// Тип объекта (TYPE_OBJ)
		/// </summary>
		[Display(Name = "Тип объекта")]
		[Required(ErrorMessage = "Поле Тип объекта обязательное")]
		public ObjectType? ObjectType { get; set; }

		/// <summary>
		/// Входящая дата ГБУ (DATE_IN)
		/// </summary>
		[Display(Name = "Входящая дата ГБУ")]
		[Required(ErrorMessage = "Поле Входящая дата ГБУ обязательное")]
		public DateTime? DateIn { get; set; }

		/// <summary>
		/// Входящий номер (NUM_IN)
		/// </summary>
		[Display(Name = "Входящий №")]
		[Required(ErrorMessage = "Поле Входящий № обязательное")]
		public string NumberIn { get; set; }

		/// <summary>
		/// Отображение книги
		/// </summary>
		public string BookDisplay { get; set; }

		/// <summary>
		/// Идентификатор книги (BOOK_ID)
		/// </summary>
		[Display(Name = "Книга")]
		[Required(ErrorMessage = "Поле Книга обязательное")]
		public long? BookId { get; set; }

		/// <summary>
		/// Дата выдачи исполнителю (DATE_IN_ISP)
		/// </summary>
		[Display(Name = "Дата выдачи исполнителю")]
		public DateTime? DateInIsp { get; set; }

		/// <summary>
		/// Отображение испольнителя по внесению
		/// </summary>
		public string UserIspDisplay { get; set; }

		/// <summary>
		/// Идентификатор исполнителя (USER_ISP_ID)
		/// </summary>
		[Display(Name = "Исполнитель по внесению")]
		public long? UserIspId { get; set; }

		/// <summary>
		/// Плановая дата внесения (DATE_IN_PLAN)
		/// </summary>
		[Display(Name = "Плановая дата внесения")]
		public DateTime? DateInPlan { get; set; }

		/// <summary>
		/// Фактическая дата внесения (DATE_IN_FACT)
		/// </summary>
		[Display(Name = "Фактическая дата внесения")]
		public DateTime? DateInFact { get; set; }

		/// <summary>
		/// Срок рассмотрения декларации (DURATION_IN)
		/// </summary>
		[Display(Name = "Срок рассмотрения")]
		public DateTime? DurationDateIn { get; set; }

		/// <summary>
		/// Фактическая дата завершения (DATE_END)
		/// </summary>
		[Display(Name = "Фактическое завершение")]
		public DateTime? DateEnd { get; set; }

		/// <summary>
		/// Статус декларации (STATUS)
		/// </summary>
		[Display(Name = "Статус")]
		//[Required(ErrorMessage = "Поле Статус обязательное")]
		public long? Status { get; set; }

		public int RegisterId { get; } = OMDeclaration.GetRegisterId();

		public DeclarationFormalCheckModel FormalCheckModel { get; set; }

		public bool IsEditDeclaration { get; set; }
		public bool IsCreateDeclaration { get; set; }
		public bool IsEditDeclarationSupplyBlock { get; set; }
		public bool IsEditDeclarationProcessingBlock { get; set; }
		public bool IsEditDeclarationStatus { get; set; }
		public bool IsEditDeclarationFormalChecking { get; set; }
		public bool IsEditDeclarationAttachments { get; set; }

		public static DeclarationModel FromEntity(OMDeclaration entity, OMSubject owner, OMSubject agent, OMBook book, OMUser userIsp, OMResult result)
		{
			if (entity == null)
			{
				return new DeclarationModel
				{
					Id = -1,
					FormalCheckModel = DeclarationFormalCheckModel.FromEntity(null, null),
					UserIspId = userIsp?.Id,
					UserIspDisplay = userIsp?.FullName
				};
			}

			return new DeclarationModel
			{
				Id = entity.Id,
				OwnerType = entity.OwnerType_Code,
				OwnerId = entity.Owner_Id,
				OwnerDisplay = owner != null
					? owner.Type_Code == SubjectType.Ul
						? owner.Name
						: $"{owner.F_Name} {owner.I_Name} {owner.O_Name}"
					: null,
				AgentId = entity.Agent_Id,
				AgentDisplay = agent != null
					? agent.Type_Code == SubjectType.Ul
						? agent.Name
						: $"{agent.F_Name} {agent.I_Name} {agent.O_Name}"
					: null,
				UvedTypeOwner = entity.UvedTypeOwner_Code,
				UvedTypeAgent = entity.UvedTypeAgent_Code,
				CertificateName = entity.CertificateName,
				CertificateNum = entity.CertificateNum,
				CertificateDate = entity.CertificateDate,
				CadastralObjectNumber = entity.CadastralNumObj,
				ObjectType = entity.TypeObj_Code,
				DateIn = entity.DateIn,
				NumberIn = entity.NumIn,
				BookId = entity.Book_Id,
				BookDisplay = book?.Prefics,
				DateInIsp = entity.DateInIsp,
				UserIspId = entity.UserIsp_Id,
				UserIspDisplay = userIsp?.FullName,
				DateInPlan = entity.DateInPlan,
				DateInFact = entity.DateInFact,
				DurationDateIn = entity.DurationIn,
				DateEnd = entity.DateEnd,
				Status = (long)entity.Status_Code,
				FormalCheckModel = DeclarationFormalCheckModel.FromEntity(entity, result)
			};
		}

		public static void ToEntity(DeclarationModel declarationViewModel, ref OMDeclaration entity, ref OMResult result)
		{
			if (declarationViewModel.DateIn != entity.DateIn ||
			    (entity.Status_Code != (StatusDec) declarationViewModel.Status.GetValueOrDefault() &&
			     (StatusDec) declarationViewModel.Status.GetValueOrDefault() == StatusDec.Rejection))
			{
				if (declarationViewModel.DateIn.HasValue)
				{
					declarationViewModel.DurationDateIn =
						(StatusDec) declarationViewModel.Status.GetValueOrDefault() == StatusDec.Rejection
							? CalendarHolidays.GetDateFromWorkDays(declarationViewModel.DateIn.Value.AddDays(-1),
								DeclarationsController.DurationWorkDaysCountForRejectedDeclaration)
							: CalendarHolidays.GetDateFromWorkDays(declarationViewModel.DateIn.Value.AddDays(-1),
								DeclarationsController.DurationWorkDaysCount);
					declarationViewModel.FormalCheckModel.CheckTime = CalendarHolidays.GetDateFromWorkDays(
						declarationViewModel.DateIn.Value.AddDays(-1),
						DeclarationsController.CheckTimeDaysCount);
				}
				else
				{
					declarationViewModel.DurationDateIn = null;
					declarationViewModel.FormalCheckModel.CheckTime = null;
				}
			}
			if (entity.DurationIn != declarationViewModel.DurationDateIn)
			{
				declarationViewModel.FormalCheckModel.DateCheckPlan = (StatusDec)declarationViewModel.Status.GetValueOrDefault() == StatusDec.Rejection 
					? declarationViewModel.DurationDateIn 
					: declarationViewModel.DurationDateIn?.GetStartWorkDate(DeclarationsController.DaysDiffBetweenDateCheckPlanAndDurationDateIn - 1);
			}

			entity.OwnerType_Code = declarationViewModel.OwnerType.GetValueOrDefault();
			entity.Owner_Id = declarationViewModel.OwnerId;
			entity.Agent_Id = declarationViewModel.AgentId;
			entity.UvedTypeOwner_Code = declarationViewModel.UvedTypeOwner.GetValueOrDefault();
			entity.UvedTypeAgent_Code = declarationViewModel.UvedTypeAgent.GetValueOrDefault();
			entity.CertificateName = declarationViewModel.CertificateName;
			entity.CertificateNum = declarationViewModel.CertificateNum;
			entity.CertificateDate = declarationViewModel.CertificateDate;
			entity.CadastralNumObj = declarationViewModel.CadastralObjectNumber;
			entity.TypeObj_Code = declarationViewModel.ObjectType.GetValueOrDefault();
			entity.DateIn = declarationViewModel.DateIn;
			entity.NumIn = declarationViewModel.NumberIn;
			entity.Book_Id = declarationViewModel.BookId.Value;
			entity.UserIsp_Id = declarationViewModel.UserIspId;
			entity.DateInIsp = declarationViewModel.DateInIsp;
			entity.DateInPlan = declarationViewModel.DateInPlan;
			entity.DateInFact = declarationViewModel.DateInFact;
			entity.DurationIn = declarationViewModel.DurationDateIn;
			entity.DateEnd = declarationViewModel.DateEnd;
			if (declarationViewModel.Status.HasValue)
			{
				entity.Status_Code = (StatusDec)declarationViewModel.Status.GetValueOrDefault();
			}
			DeclarationFormalCheckModel.ToEntity(declarationViewModel.FormalCheckModel, ref entity, ref result);
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!BookId.HasValue || BookId == 0)
			{
				yield return
					new ValidationResult(errorMessage: "Поле Книга обязательное",
						memberNames: new[] { "BookId" });
			}

			if ((!AgentId.HasValue || AgentId == 0) && (!OwnerId.HasValue || OwnerId == 0))
			{
				yield return
					new ValidationResult(errorMessage: "Должен быть указан Заявитель или Представитель",
						memberNames: new[] { "AgentId", "OwnerId" });
			}

			if (AgentId.HasValue && AgentId != 0)
			{
				foreach (var validationResult in ValidateSubjectNotificationType(AgentId, UvedTypeAgent, "Представителя", nameof(UvedTypeAgent))) yield return validationResult;
			}

			if (OwnerId.HasValue && OwnerId != 0)
			{
				foreach (var validationResult in ValidateSubjectNotificationType(OwnerId, UvedTypeOwner, "Заявителя", nameof(UvedTypeOwner))) yield return validationResult;
			}

			if (!ObjectType.HasValue || ObjectType == ObjectModel.Directory.Declarations.ObjectType.None)
			{
				yield return
					new ValidationResult(errorMessage: "Поле Тип объекта обязательное",
						memberNames: new[] { "ObjectType" });
			}
		}

		private IEnumerable<ValidationResult> ValidateSubjectNotificationType(long? subjectId, SendUvedType? subjectUvedType, string subjectName, string subjectUvedTypeField)
		{
			var subject = OMSubject
				.Where(x => x.Id == subjectId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (subjectUvedType == SendUvedType.Email)
			{
				if (string.IsNullOrWhiteSpace(subject?.Mail))
				{
					yield return
						new ValidationResult(
							errorMessage:
							$"У выбранного {subjectName} отсутствуют необходимые данные для данного Способа получения уведомления: Адрес электронной почты",
							memberNames: new[] {subjectUvedTypeField});
				}
			}
			else if (string.IsNullOrWhiteSpace(subject?.Zip) || string.IsNullOrWhiteSpace(subject?.City) ||
			         string.IsNullOrWhiteSpace(subject?.Street) || string.IsNullOrWhiteSpace(subject?.House))
			{
				var emptyAddressParts = new List<string>();
				if (string.IsNullOrWhiteSpace(subject?.Zip))
				{
					emptyAddressParts.Add("Индекс");
				}

				if (string.IsNullOrWhiteSpace(subject?.City))
				{
					emptyAddressParts.Add("Город");
				}

				if (string.IsNullOrWhiteSpace(subject?.Street))
				{
					emptyAddressParts.Add("Улица");
				}

				if (string.IsNullOrWhiteSpace(subject?.House))
				{
					emptyAddressParts.Add("Дом");
				}

				yield return
					new ValidationResult(
						errorMessage:
						$"У выбранного {subjectName} отсутствуют необходимые данные для данного Способа получения уведомления: {string.Join(", ", emptyAddressParts)}",
						memberNames: new[] { subjectUvedTypeField });
			}
		}
	}
}
