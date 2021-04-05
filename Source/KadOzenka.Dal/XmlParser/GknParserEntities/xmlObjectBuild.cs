using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectBuild : xmlObjectParticular
	{
		/// <summary>
		/// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
		/// </summary>
		public List<string> ParentCadastralNumbers { get; set; }
		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public string Area { get; set; }
		/// <summary>
		/// Назначение здания
		/// </summary>
		public xmlCodeName AssignationBuilding { get; set; }
		/// <summary>
		/// Количество этажей (в том числе подземных)
		/// </summary>
		public xmlFloors Floors { get; set; }
		/// <summary>
		/// Эксплуатационные характеристики
		/// </summary>
		public xmlYear Years { get; set; }
		/// <summary>
		/// Наименование ОКС
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Материал наружных стен здания
		/// </summary>
		public List<xmlCodeName> Walls { get; set; }

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