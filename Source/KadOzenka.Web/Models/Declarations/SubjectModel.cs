using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class SubjectModel : IValidatableObject
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		[Display(Name = "Идентификатор")]
		public long Id { get; set; }

		/// <summary>
		/// Тип субъекта (TYPE)
		/// </summary>
		[Display(Name = "Тип субъекта")]
		[Required(ErrorMessage = "Выберете Тип субъекта (Физическое лицо / Юридическое лицо)")]
		public long? Type { get; set; }

		/// <summary>
		/// Наименование юридического лица (NAME)
		/// </summary>
		[Display(Name = "Наименование")]
		public string Name { get; set; }

		/// <summary>
		/// Фамилия физического лица/представителя заявителя
		/// </summary>
		[Display(Name = "Фамилия")]
		public string Surname { get; set; }

		/// <summary>
		/// Имя физического лица/представителя заявителя
		/// </summary>
		[Display(Name = "Имя")]
		public string FirstName { get; set; }

		/// <summary>
		/// Отчество физического лица/представителя заявителя
		/// </summary>
		[Display(Name = "Отчество")]
		public string MiddleName { get; set; }

		/// <summary>
		/// Адрес электронной почты
		/// </summary>
		[Display(Name = "Адрес электронной почты")]
		public string Mail { get; set; }

		/// <summary>
		/// Телефон для связи
		/// </summary>
		[Display(Name = "Телефон")]
		public string Phone { get; set; }

		/// <summary>
		/// Индекс (ZIP)
		/// </summary>
		[Display(Name = "Индекс")]
		public string Zip { get; set; }

		/// <summary>
		/// Город (CITY)
		/// </summary>
		[Display(Name = "Город")]
		public string City { get; set; }

		/// <summary>
		/// Улица (STREET)
		/// </summary>
		[Display(Name = "Улица")]
		public string Street { get; set; }

		/// <summary>
		/// Дом (HOUSE)
		/// </summary>
		[Display(Name = "Дом")]
		public string House { get; set; }

		/// <summary>
		/// Строение (BUILDING)
		/// </summary>
		[Display(Name = "Строение")]
		public string Building { get; set; }

		/// <summary>
		/// Квартира (FLAT)
		/// </summary>
		[Display(Name = "Квартира")]
		public ulong? Flat { get; set; }

		public bool IsEditSubject { get; set; }
		public bool IsCreateSubject { get; set; }

		public static SubjectModel FromEntity(OMSubject entity)
		{
			if (entity == null)
			{
				return new SubjectModel
				{
					Id = -1
				};
			}
			var subject = new SubjectModel
			{
				Id = entity.Id,
				Type = (long)entity.Type_Code,
				Name = entity.Name,
				Surname = entity.F_Name,
				FirstName = entity.I_Name,
				MiddleName = entity.O_Name,
				Mail = entity.Mail,
				Phone = entity.Telefon,
				Zip = entity.Zip,
				City = entity.City,
				Street = entity.Street,
				House = entity.House,
				Building = entity.Building,
			};
			if (!string.IsNullOrWhiteSpace(entity.Flat))
			{
				ulong.TryParse(entity.Flat, out var flat);
				subject.Flat = flat;
			}

			return subject;
		}

		public static void ToEntity(SubjectModel subjectViewModel, ref OMSubject entity)
		{
			entity.Name = subjectViewModel.Name;
			entity.F_Name = subjectViewModel.Surname;
			entity.I_Name = subjectViewModel.FirstName;
			entity.O_Name = subjectViewModel.MiddleName;
			entity.Mail = subjectViewModel.Mail;
			entity.Telefon = subjectViewModel.Phone;
			entity.Zip = subjectViewModel.Zip;
			entity.City = subjectViewModel.City;
			entity.Street = subjectViewModel.Street;
			entity.House = subjectViewModel.House;
			entity.Building = subjectViewModel.Building;
			entity.Flat = subjectViewModel.Flat?.ToString();

			if (subjectViewModel.Type != null)
			{
				entity.Type_Code = (SubjectType)subjectViewModel.Type;
			}
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Type == (int)SubjectType.Fl && (string.IsNullOrWhiteSpace(Surname) || string.IsNullOrWhiteSpace(FirstName)))
			{
				yield return
					new ValidationResult(errorMessage: "Поля Фамилия, Имя обязательны для Физлица",
						memberNames: new[] { "Surname", "FirstName" });
			}
			if (Type == (int)SubjectType.Ul && string.IsNullOrWhiteSpace(Name))
			{
				yield return
					new ValidationResult(errorMessage: "Полe Наименование обязательно для Юрлица",
						memberNames: new[] { "Name" });
			}
		}
	}
}