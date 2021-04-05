using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectCarPlace : xmlObjectParticular
	{
		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public string Area { get; set; }
		/// <summary>
		/// Местоположение в объекте недвижимости
		/// </summary>
		public List<xmlPosition> PositionsInObject { get; set; }
		/// <summary>
		/// Кадастровый номер здания или сооружения, в котором расположено помещение
		/// </summary>
		public string CadastralNumberOKS { get; set; }
		/// <summary>
		/// Количество этажей (в том числе подземных)
		/// </summary>
		public xmlFloors parentFloors { get; set; }
		/// <summary>
		/// Эксплуатационные характеристики
		/// </summary>
		public xmlYear parentYears { get; set; }
		/// <summary>
		/// Назначение здания
		/// </summary>
		public xmlCodeName parentAssignationBuilding { get; set; }
		/// <summary>
		/// Назначение (сооружение, онс)
		/// </summary>
		public string parentAssignationName { get; set; }
		/// <summary>
		/// Материал наружных стен здания
		/// </summary>
		public List<xmlCodeName> parentWalls { get; set; }

		public xmlObjectCarPlace(xmlObject obj) : base(obj)
		{
			Area = obj.Area;
			PositionsInObject = obj.PositionsInObject;
			CadastralNumberOKS = obj.CadastralNumberOKS;
			parentFloors = obj.Floors;
			parentYears = obj.Years;
			parentAssignationBuilding = obj.AssignationBuilding;
			parentAssignationName = obj.AssignationName;
			parentWalls = obj.Walls;
		}
	}
}