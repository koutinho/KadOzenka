using System;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectParticular
	{
		/// <summary>
		/// Вид объекта недвижимости
		/// </summary>
		public enTypeObject TypeObject { get; set; }
		/// <summary>
		/// Тип объекта недвижимости
		/// </summary>
		public string TypeRealty { get; set; }
		/// <summary>
		/// Дата создания
		/// </summary>
		public DateTime DateCreate { get; set; }
		/// <summary>
		/// Кадастровый номер
		/// </summary>
		public string CadastralNumber { get; set; }
		/// <summary>
		/// Номер кадастрового квартала
		/// </summary>
		public string CadastralNumberBlock { get; set; }
		/// <summary>
		/// Сведения о кадастровой стоимости
		/// </summary>
		public xmlCost CadastralCost { get; set; }
		/// <summary>
		/// Адрес (местоположение)
		/// </summary>
		public xmlAdress Adress { get; set; }

		public xmlObjectParticular(xmlObject obj)
		{
			TypeObject = obj.TypeObject;
			TypeRealty = obj.TypeRealty;
			DateCreate = obj.DateCreate;
			CadastralNumber = obj.CadastralNumber;
			CadastralNumberBlock = obj.CadastralNumberBlock;
			CadastralCost = obj.CadastralCost;
			Adress = obj.Adress;
		}

		public override string ToString()
		{
			return CadastralNumber;
		}
	}
}