using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectParcel : xmlObjectParticular
	{
		/// <summary>
		/// Кадастровые номера объектов недвижимости, расположенных в пределах земельного участка
		/// </summary>
		public List<string> InnerCadastralNumbers { get; set; }
		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public string Area { get; set; }
		/// <summary>
		/// Наименование участка
		/// </summary>
		public xmlCodeName Name { get; set; }
		/// <summary>
		/// Категория земель
		/// </summary>
		public xmlCodeName Category { get; set; }
		/// <summary>
		/// Разрешенное использование участка
		/// </summary>
		public xmlUtilization Utilization { get; set; }
		/// <summary>
		/// Сведения об ограничениях (обременениях) прав
		/// </summary>
		public List<xmlEncumbrance> Encumbrances { get; set; }
		/// <summary>
		/// Сведения о расположении земельного участка в границах зоны или территории
		/// </summary>
		public List<xmlZoneAndTerritory> ZoneAndTerritorys { get; set; }
		/// <summary>
		/// Сведения о результатах проведения государственного земельного надзора
		/// </summary>
		public List<xmlSupervisionEvent> GovernmentLandSupervision { get; set; }

		public xmlObjectParcel(xmlObject obj) : base(obj)
		{
			InnerCadastralNumbers = obj.InnerCadastralNumbers;
			Area = obj.Area;
			Name = obj.NameParcel;
			Category = obj.Category;
			Utilization = obj.Utilization;
			Encumbrances = obj.Encumbrances;
			ZoneAndTerritorys = obj.ZoneAndTerritorys;
			GovernmentLandSupervision = obj.GovernmentLandSupervision;
		}
	}
}