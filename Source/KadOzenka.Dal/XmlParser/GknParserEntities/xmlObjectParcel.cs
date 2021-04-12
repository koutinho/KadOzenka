using System.Collections.Generic;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;

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
		public double? Area { get; set; }
		/// <summary>
		/// Погрешность измерения
		/// </summary>
		public double? AreaInaccuracy { get; set; }
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
		/// Сведения о природных объектах
		/// </summary>
		public List<xmlNaturalObject> NaturalObjects { get; set; }
		/// <summary>
		/// Сведения о частях участка
		/// </summary>
		public List<xmlSubParcel> SubParcels { get; set; }
		/// <summary>
		/// Кадастровый номер, если объект недвижимости входит в состав предприятия как имущественного комплекса
		/// </summary>
		public string FacilityCadastralNumber { get; set; }
		/// <summary>
		/// Назначение предприятия как имущественного комплекса, если объект недвижимости входит в состав предприятия как имущественного комплекса
		/// </summary>
		public string FacilityPurpose { get; set; }
		/// <summary>
		/// Сведения о расположении земельного участка в границах зоны или территории
		/// </summary>
		public List<xmlZoneAndTerritory> ZonesAndTerritories { get; set; }
		/// <summary>
		/// Сведения о результатах проведения государственного земельного надзора
		/// </summary>
		public List<xmlSupervisionEvent> GovernmentLandSupervision { get; set; }
		/// <summary>
		/// Учетный номер утвержденного проекта межевания территории
		/// </summary>
		public string SurveyingProjectNum { get; set; }
		/// <summary>
		/// Реквизиты решения
		/// </summary>
		public xmlDocument SurveyingProjectDecisionRequisites { get; set; }
		/// <summary>
		/// Сведения о создании (эксплуатации) на земельном участке наёмного дома
		/// </summary>
		public xmlHiredHouse HiredHouse { get; set; }
		/// <summary>
		/// Сведения об ограничении оборотоспособности земельного участка в соответствии со статьей 11 Федерального закона от 1 мая 2016 г. № 119-ФЗ
		/// </summary>
		public string LimitedCirculation { get; set; }

		public xmlObjectParcel(xmlObject obj) : base(obj)
		{
			InnerCadastralNumbers = obj.InnerCadastralNumbers;
			Area = obj.Area;
			AreaInaccuracy = obj.AreaInaccuracy;
			Name = obj.NameParcel;
			Category = obj.Category;
			Utilization = obj.Utilization;
			NaturalObjects = obj.NaturalObjects;
			SubParcels = obj.SubParcels;
			FacilityCadastralNumber = obj.FacilityCadastralNumber;
			FacilityPurpose = obj.FacilityPurpose;
			ZonesAndTerritories = obj.ZoneAndTerritorys;
			GovernmentLandSupervision = obj.GovernmentLandSupervision;
			SurveyingProjectNum = obj.SurveyingProjectNum;
			SurveyingProjectDecisionRequisites = obj.SurveyingProjectDecisionRequisites;
			HiredHouse = obj.HiredHouse;
			LimitedCirculation = obj.LimitedCirculation;
		}
	}
}