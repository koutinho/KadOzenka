

using ObjectModel.Directory.Declarations;

namespace KadOzenka.Dal.Declarations.Dto
{
	public class CreateSubjectDto
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Тип субъекта (TYPE)
		/// </summary>
		public SubjectType Type { get; set; }

		/// <summary>
		/// Наименование юридического лица (NAME)
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Фамилия физического лица/представителя заявителя
		/// </summary>
		public string Surname { get; set; }

		/// <summary>
		/// Имя физического лица/представителя заявителя
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Отчество физического лица/представителя заявителя
		/// </summary>
		public string MiddleName { get; set; }

		/// <summary>
		/// Адрес электронной почты
		/// </summary>
		public string Mail { get; set; }

		/// <summary>
		/// Телефон для связи
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// Индекс (ZIP)
		/// </summary>
		public string Zip { get; set; }

		/// <summary>
		/// Город (CITY)
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Улица (STREET)
		/// </summary>
		public string Street { get; set; }

		/// <summary>
		/// Дом (HOUSE)
		/// </summary>
		public string House { get; set; }

		/// <summary>
		/// Строение (BUILDING)
		/// </summary>
		public string Building { get; set; }

		/// <summary>
		/// Квартира (FLAT)
		/// </summary>
		public ulong? Flat { get; set; }
	}
}