using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectCarPlace : xmlObjectParticular
	{
		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public double? Area { get; set; }
		/// <summary>
		/// Кадастровый номер здания или сооружения, в котором расположено помещение
		/// </summary>
		public string CadastralNumberOKS { get; set; }
		/// <summary>
		/// Характеристики объекта недвижимости, в котором расположено помещение, машино-место
		/// </summary>
		public xmlParentOks ParentOks { get; set; }
		/// <summary>
		/// Местоположение в объекте недвижимости
		/// </summary>
		public xmlLevel PositionInObject { get; set; }
		/// <summary>
		/// Кадастровый номер единого недвижимого комплекса, если объект недвижимости входит в состав единого недвижимого комплекса
		/// </summary>
		public List<string> UnitedCadastralNumbers { get; set; }
		/// <summary>
		/// Кадастровый номер, если объект недвижимости входит в состав предприятия как имущественного комплекса
		/// </summary>
		public string FacilityCadastralNumber { get; set; }
		/// <summary>
		/// Назначение предприятия как имущественного комплекса, если объект недвижимости входит в состав предприятия как имущественного комплекса
		/// </summary>
		public string FacilityPurpose { get; set; }

		public xmlObjectCarPlace(xmlObject obj) : base(obj)
		{
			Area = obj.Area;
			CadastralNumberOKS = obj.CadastralNumberOKS;
			ParentOks = obj.ParentOks;
			PositionInObject = obj.Levels?.FirstOrDefault();
			UnitedCadastralNumbers = obj.UnitedCadastralNumbers;
			FacilityCadastralNumber = obj.FacilityCadastralNumber;
			FacilityPurpose = obj.FacilityPurpose;
		}
	}
}