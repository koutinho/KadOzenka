using System;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectParticular
	{
		/// <summary>
		/// Вид объекта недвижимости
		/// </summary>
		public enTypeObject TypeObject;
		/// <summary>
		/// Тип объекта недвижимости
		/// </summary>
		public string TypeRealty;
		/// <summary>
		/// Дата создания
		/// </summary>
		public DateTime DateCreate;
		/// <summary>
		/// Кадастровый номер
		/// </summary>
		public string CadastralNumber;
		/// <summary>
		/// Номер кадастрового квартала
		/// </summary>
		public string CadastralNumberBlock;
		/// <summary>
		/// Сведения о кадастровой стоимости
		/// </summary>
		public xmlCost CadastralCost;
		/// <summary>
		/// Адрес (местоположение)
		/// </summary>
		public xmlAdress Adress;

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