using KadOzenka.Dal.XmlParser.GknParserXmlElements;

namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Адрес
	/// </summary>
	public class xmlAdress
	{
		/// <summary>
		/// Уникальный номер адресообразующего элемента в государственном адресном реестре
		/// </summary>
		public string FIAS { get; set; }
		/// <summary>
		/// ОКАТО
		/// </summary>
		public string OKATO { get; set; }
		/// <summary>
		/// КЛАДР
		/// </summary>
		public string KLADR { get; set; }
		/// <summary>
		/// OKTMO
		/// </summary>
		public string OKTMO { get; set; }
		/// <summary>
		/// Почтовый индекс
		/// </summary>
		public string PostalCode { get; set; }
		/// <summary>
		/// Российская Федерация
		/// </summary>
		public string RussianFederation { get; set; }
		/// <summary>
		/// Регион
		/// </summary>
		public string Region { get; set; }
		/// <summary>
		/// Неформализованное описание
		/// </summary>
		public string Note { get; set; }
		/// <summary>
		/// Иное
		/// </summary>
		public string Other { get; set; }
		/// <summary>
		/// Признак, позволяющий отличить присвоенный в установленном порядке адрес объекта недвижимости
		/// и местоположение объекта недвижимости
		/// </summary>
		public string AddressOrLocation { get; set; }
		/// <summary>
		/// Район
		/// </summary>
		public xmlAdresLevel District { get; set; }
		/// <summary>
		/// Муниципальное образование
		/// </summary>
		public xmlAdresLevel City { get; set; }
		/// <summary>
		/// Городской район
		/// </summary>
		public xmlAdresLevel UrbanDistrict { get; set; }
		/// <summary>
		/// Сельсовет
		/// </summary>
		public xmlAdresLevel SovietVillage { get; set; }
		/// <summary>
		/// Населенный пункт
		/// </summary>
		public xmlAdresLevel Locality { get; set; }
		/// <summary>
		/// Элемент планировочной структуры
		/// </summary>
		public xmlAdresLevel PlanningElement { get; set; }
		/// <summary>
		/// Улица
		/// </summary>
		public xmlAdresLevel Street { get; set; }
		/// <summary>
		/// Дом
		/// </summary>
		public xmlAdresLevel Level1 { get; set; }
		/// <summary>
		/// Корпус
		/// </summary>
		public xmlAdresLevel Level2 { get; set; }
		/// <summary>
		/// Строение
		/// </summary>
		public xmlAdresLevel Level3 { get; set; }
		/// <summary>
		/// Квартира
		/// </summary>
		public xmlAdresLevel Apartment { get; set; }

		/// <summary>
		/// В границах
		/// </summary>
		public string InBounds { get; set; }

		/// <summary>
		/// Положение на ДКК
		/// </summary>
		public string Placed { get; set; }

		/// <summary>
		/// Уточнение местоположения
		/// </summary>
		public xmlElaborationLocation Elaboration { get; set; }

		public static string GetTextAdress(xmlAdress adress)
		{
			string res = string.Empty;
			if (adress.PostalCode != string.Empty && adress.PostalCode!=null) res += adress.PostalCode + ", ";
			if (adress.Region != string.Empty && adress.Region != null) res += adress.Region + ", ";
			if (adress.District != null) res += adress.District.Type + " " + adress.District.Value + ", ";
			if (adress.City != null) res += adress.City.Type + " " + adress.City.Value + ", ";
			if (adress.UrbanDistrict != null) res += adress.UrbanDistrict.Type + " " + adress.UrbanDistrict.Value + ", ";
			if (adress.Locality != null) res += adress.Locality.Type + " " + adress.Locality.Value + ", ";
			if (adress.Street != null) res += adress.Street.Type + " " + adress.Street.Value + ", ";
			if (adress.Level1 != null) res += adress.Level1.Type + " " + adress.Level1.Value + ", ";
			if (adress.Level2 != null) res += adress.Level2.Type + " " + adress.Level2.Value + ", ";
			if (adress.Level3 != null) res += adress.Level3.Type + " " + adress.Level3.Value + ", ";
			if (adress.Apartment != null) res += adress.Apartment.Type + " " + adress.Apartment.Value + ", ";
			if (adress.Other != string.Empty && adress.Other != null) res += adress.Other + ", ";
			res = res.Trim().TrimEnd(',');
			return res;
		}
		public static string GetTextPlace(xmlAdress adress)
		{
			return (adress.Note==null)?string.Empty: adress.Note;
		}
	}
}