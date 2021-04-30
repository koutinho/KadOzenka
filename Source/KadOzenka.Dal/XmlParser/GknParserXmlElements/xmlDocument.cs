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
		public xmlCodeName CodeDocument { get; set; }
		/// <summary>
		/// Наименование документа
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Серия документа
		/// </summary>
		public string Series { get; set; }
		/// <summary>
		/// Номер документа
		/// </summary>
		public string Number { get; set; }
		/// <summary>
		/// Дата выдачи (подписания) документа
		/// </summary>
		public DateTime? Date { get; set; }
		/// <summary>
		/// Организация, выдавшая документ. Автор документа
		/// </summary>
		public string IssueOrgan { get; set; }
		/// <summary>
		/// Особые отметки
		/// </summary>
		public string Desc { get; set; }

		public xmlDocument()
		{
			CodeDocument = new xmlCodeName();
		}
	}
}