using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class BookModel : IValidatableObject
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		[Display(Name = "Идентификатор")]
		public long Id { get; set; }

		/// <summary>
		/// Префикс (PREFICS)
		/// </summary>
		[Display(Name = "Номер")]
		[Required(ErrorMessage = "Поле Номер обязательное")]
		public string Prefics { get; set; }

		/// <summary>
		/// Дата начала (DATE_BEGIN)
		/// </summary>
		[Display(Name = "Дата начала")]
		[Required(ErrorMessage = "Поле Дата начала обязательное")]
		public DateTime? DateBegin { get; set; }

		/// <summary>
		/// Дата окончания (DATE_END)
		/// </summary>
		[Display(Name = "Дата окончания")]
		[Required(ErrorMessage = "Поле Дата окончания обязательное")]
		public DateTime? DateEnd { get; set; }

		/// <summary>
		/// Статус (STATUS)
		/// </summary>
		[Display(Name = "Статус")]
		[Required(ErrorMessage = "Поле Статус обязательное")]
		public long? Status { get; set; }

		/// <summary>
		/// Тип (TYPE)
		/// </summary>
		[Display(Name = "Тип")]
		public long? Type { get; set; }



		public static BookModel FromEntity(OMBook entity)
		{
			if (entity == null)
			{
				return new BookModel
				{
					Id = -1
				};
			}

			return new BookModel
			{
				Id = entity.Id,
				Prefics = entity.Prefics,
				DateBegin = entity.DateBegin,
				DateEnd = entity.DateEnd,
				Status = (long)entity.Status_Code,
				Type = (long)entity.Type_Code
			};
		}

		public static void ToEntity(BookModel bookViewModel, ref OMBook entity)
		{
			entity.Prefics = bookViewModel.Prefics;
			entity.DateBegin = bookViewModel.DateBegin.GetValueOrDefault();
			entity.DateEnd = bookViewModel.DateEnd.GetValueOrDefault();
			if (bookViewModel.Status != null)
			{
				entity.Status_Code = (BookStatus) bookViewModel.Status;
			}
			if (bookViewModel.Type != null)
			{
				entity.Type_Code = (BookType) bookViewModel.Type;
			}
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (DateEnd < DateBegin)
			{
				yield return
					new ValidationResult(errorMessage: "Дата окончания меньше Даты начала",
						memberNames: new[] { "DateEnd" });
			}
		}
	}
}
