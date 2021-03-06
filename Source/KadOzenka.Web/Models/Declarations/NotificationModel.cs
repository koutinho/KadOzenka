using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class NotificationModel
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		[Display(Name = "Идентификатор")]
		public long Id { get; set; }

		/// <summary>
		/// Идентификатор декларации (DECLARATION_ID)
		/// </summary>
		[Required(ErrorMessage = "Идентификатор декларации не указан")]
		[Range(1, long.MaxValue, ErrorMessage = "Недопустимое значение ид декларации")]
		public long DeclarationId { get; set; }

		/// <summary>
		/// Идентификатор книги (BOOK_ID)
		/// </summary>
		[Required(ErrorMessage = "Идентификатор книги не указан")]
		[Range(1, long.MaxValue, ErrorMessage = "Недопустимое значение ид книги")]
		[Display(Name = "Книга")]
		public long BookId { get; set; }

		/// <summary>
		/// Отображение книги
		/// </summary>
		public string BookDisplay { get; set; }

		/// <summary>
		/// Номер (NUM)
		/// </summary>
		[Display(Name = "Номер")]
		[MaxLength(255, ErrorMessage = "Максимальная длина значения для поля 'Номер' составляет 255 символа")]
		public string Number { get; set; }

		/// <summary>
		/// Дата (DATE)
		/// </summary>
		[Display(Name = "Дата")]
		public DateTime? Date { get; set; }

		/// <summary>
		/// Тип уведомления (TYPE)
		/// </summary>
		[Display(Name = "Тип уведомления")]
		[Required(ErrorMessage = "Выберете тип уведомления")]
		public UvedType? Type { get; set; }

		/// <summary>
		/// Номер почтового уведомления (MAIL_NUM)
		/// </summary>
		[Display(Name = "Номер почтового уведомления")]
		[MaxLength(255, ErrorMessage = "Максимальная длина значения для поля 'Номер почтового уведомления' составляет 255 символа")]
		public string MailNumber { get; set; }

		/// <summary>
		/// Дата почтового уведомления (MAIL_DATE)
		/// </summary>
		[Display(Name = "Дата почтового уведомления")]
		public DateTime? MailDate { get; set; }

		/// <summary>
		/// Причина отказа (REJECTION_REASON)
		/// </summary>
		[Display(Name = "Причина отказа")]
		public string RejectionReason { get; set; }

		/// <summary>
		/// Причина отказа (REJECTION_REASON_TYPE)
		/// </summary>
		[Display(Name = "Причина отказа")]
		public RejectionReasonType? RejectionReasonType { get; set; }

		/// <summary>
		/// Причина отказа (REJECTION_REASON_TYPE)
		/// </summary>
		[Display(Name = "Причины отказа")]
		public List<RejectionReasonType> RejectionReasonTypes { get; set; }

		/// <summary>
		/// Приложение (ANNEX)
		/// </summary>
		[Display(Name = "Приложение")]
        public string Annex { get; set; }

		public bool IsEditApproveNotifications { get; set; }
		public bool IsEditOtherNotifications { get; set; }

		public static NotificationModel FromEntity(OMUved entity, OMBook book,
			List<OMUvedRejectionReasonType> rejectionReasonTypes)
		{
			if (entity == null)
			{
				return new NotificationModel
				{
					Id = -1,
					BookId = book.Id,
					BookDisplay = book.Prefics
				};
			}

			return new NotificationModel
			{
				Id = entity.Id,
				BookId = entity.Book_Id,
				BookDisplay = book.Prefics,
				Number = entity.Num,
				Date = entity.Date,
				Type = entity.Type_Code,
				MailNumber = entity.MailNum,
				MailDate = entity.MailDate,
				RejectionReason = entity.RejectionReason,
				RejectionReasonType = entity.RejectionReasonType_Code,
				Annex = entity.Annex,
				RejectionReasonTypes = rejectionReasonTypes?.Select(x => x.RejectionReasonType_Code).ToList()
			};
		}

		public static void ToEntity(NotificationModel notificationViewModel, ref OMUved entity)
		{
			entity.Declaration_Id = notificationViewModel.DeclarationId;
			entity.Type_Code = notificationViewModel.Type.GetValueOrDefault();
			entity.Book_Id = notificationViewModel.BookId;
			entity.Num = notificationViewModel.Number;
			entity.Date = notificationViewModel.Date;
			entity.MailNum = notificationViewModel.MailNumber;
			entity.MailDate = notificationViewModel.MailDate;
			entity.RejectionReason = notificationViewModel.RejectionReason;
			entity.RejectionReasonType_Code = notificationViewModel.RejectionReasonType.GetValueOrDefault();
			entity.Annex = notificationViewModel.Annex;
		}
	}
}
