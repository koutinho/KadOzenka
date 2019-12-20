using System.ComponentModel.DataAnnotations;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class SubjectModel
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
		/// Почтовый адрес
		/// </summary>
		[Display(Name = "Почтовый адрес")]
		public string Address { get; set; }

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

		public static SubjectModel FromEntity(OMSubject entity)
		{
			if (entity == null)
			{
				return new SubjectModel
				{
					Id = -1
				};
			}

			return new SubjectModel
			{
				Id = entity.Id,
				Type = (long)entity.Type_Code,
				Name = entity.Name,
				Surname = entity.F_Name,
				FirstName = entity.I_Name,
				MiddleName = entity.O_Name,
				Address = entity.Address,
				Mail = entity.Mail,
				Phone = entity.Telefon
			};
		}

		public static void ToEntity(SubjectModel subjectViewModel, ref OMSubject entity)
		{
			entity.Name = subjectViewModel.Name;
			entity.F_Name = subjectViewModel.Surname;
			entity.I_Name = subjectViewModel.FirstName;
			entity.O_Name = subjectViewModel.MiddleName;
			entity.Address = subjectViewModel.Address;
			entity.Mail = subjectViewModel.Mail;
			entity.Telefon = subjectViewModel.Phone;

			if (subjectViewModel.Type != null)
			{
				entity.Type_Code = (SubjectType)subjectViewModel.Type;
			}
		}
	}
}