using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectCarPlace : xmlObjectParticular
	{
		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public string Area;
		/// <summary>
		/// Местоположение в объекте недвижимости
		/// </summary>
		public List<xmlPosition> PositionsInObject;
		/// <summary>
		/// Кадастровый номер здания или сооружения, в котором расположено помещение
		/// </summary>
		public string CadastralNumberOKS;
		/// <summary>
		/// Количество этажей (в том числе подземных)
		/// </summary>
		public xmlFloors parentFloors;
		/// <summary>
		/// Эксплуатационные характеристики
		/// </summary>
		public xmlYear parentYears;
		/// <summary>
		/// Назначение здания
		/// </summary>
		public xmlCodeName parentAssignationBuilding;
		/// <summary>
		/// Назначение (сооружение, онс)
		/// </summary>
		public string parentAssignationName;
		/// <summary>
		/// Материал наружных стен здания
		/// </summary>
		public List<xmlCodeName> parentWalls;

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