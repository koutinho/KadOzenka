using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectFlat : xmlObjectParticular
	{
		/// <summary>
		/// Кадастровый номер квартиры, в которой расположена комната
		/// </summary>
		public string CadastralNumberFlat;
		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public string Area;
		/// <summary>
		/// Назначение помещения
		/// </summary>
		public xmlCodeName AssignationFlatCode;
		/// <summary>
		/// Вид помещения
		/// </summary>
		public xmlCodeName AssignationFlatType;
		/// <summary>
		/// Наименование
		/// </summary>
		public string Name;
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

		public xmlObjectFlat(xmlObject obj) : base(obj)
		{
			CadastralNumberFlat = obj.CadastralNumberFlat;
			Area = obj.Area;
			AssignationFlatCode = obj.AssignationFlatCode;
			AssignationFlatType = obj.AssignationFlatType;
			Name = obj.NameObject;
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