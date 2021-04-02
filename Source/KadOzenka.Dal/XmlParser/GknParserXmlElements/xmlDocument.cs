using System;

namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Реквизиты документа
	/// </summary>
	public class xmlDocument
	{
		/// <summary>
		/// Код документа
		/// </summary>
		public xmlCodeName CodeDocument;
		/// <summary>
		/// Наименование документа
		/// </summary>
		public string Name;
		/// <summary>
		/// Серия документа
		/// </summary>
		public string Series;
		/// <summary>
		/// Номер документа
		/// </summary>
		public string Number;
		/// <summary>
		/// Дата выдачи (подписания) документа
		/// </summary>
		public DateTime Date;
		/// <summary>
		/// Организация, выдавшая документ. Автор документа
		/// </summary>
		public string IssueOrgan;
		/// <summary>
		/// Особые отметки
		/// </summary>
		public string Desc;
	}
}