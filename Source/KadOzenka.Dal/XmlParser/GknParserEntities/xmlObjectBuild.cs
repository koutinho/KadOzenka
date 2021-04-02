using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectBuild : xmlObjectParticular
	{
		/// <summary>
		/// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
		/// </summary>
		public List<string> ParentCadastralNumbers;
		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public string Area;
		/// <summary>
		/// Назначение здания
		/// </summary>
		public xmlCodeName AssignationBuilding;
		/// <summary>
		/// Количество этажей (в том числе подземных)
		/// </summary>
		public xmlFloors Floors;
		/// <summary>
		/// Эксплуатационные характеристики
		/// </summary>
		public xmlYear Years;
		/// <summary>
		/// Наименование ОКС
		/// </summary>
		public string Name;
		/// <summary>
		/// Материал наружных стен здания
		/// </summary>
		public List<xmlCodeName> Walls;

		public xmlObjectBuild(xmlObject obj) : base(obj)
		{
			ParentCadastralNumbers = obj.ParentCadastralNumbers;
			Area = obj.Area;
			AssignationBuilding = obj.AssignationBuilding;
			Floors = obj.Floors;
			Years = obj.Years;
			Name = obj.NameObject;
			Walls = obj.Walls;
		}
	}
}