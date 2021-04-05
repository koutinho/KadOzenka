namespace KadOzenka.Dal.XmlParser
{
	/// <summary>
	/// Адрес
	/// </summary>
	public class xmlAdress
	{
		/// <summary>
		/// КЛАДР
		/// </summary>
		public string KLADR { get; set; }
		/// <summary>
		/// Почтовый индекс
		/// </summary>
		public string PostalCode { get; set; }
		/// <summary>
		/// Регион
		/// </summary>
		public string Region { get; set; }
		/// <summary>
		/// Неформализованное описание
		/// </summary>
		public string Place { get; set; }
		/// <summary>
		/// Иное
		/// </summary>
		public string Other { get; set; }
		/// <summary>
		/// Район
		/// </summary>
		public xmlAdresLevel District { get; set; }
		/// <summary>
		/// Населенный пункт
		/// </summary>
		public xmlAdresLevel Locality { get; set; }
		/// <summary>
		/// Муниципальное образование
		/// </summary>
		public xmlAdresLevel City { get; set; }
		/// <summary>
		/// Городской район
		/// </summary>
		public xmlAdresLevel UrbanDistrict { get; set; }
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
			return (adress.Place==null)?string.Empty: adress.Place;
		}
	}
}