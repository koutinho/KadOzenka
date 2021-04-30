using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser.GknParserXmlElements
{
	public class xmlParentOks
	{
		/// <summary>
		/// Кадастровый номер здания или сооружения
		/// </summary>
		public string CadastralNumberOKS { get; set; }
		/// <summary>
		/// Вид объекта недвижимости - здание или сооружение
		/// </summary>
		public xmlCodeName ObjectType { get; set; }
		/// <summary>
		/// Назначение здания
		/// </summary>
		public xmlCodeName AssignationBuilding { get; set; }
		/// <summary>
		/// Назначение сооружения
		/// </summary>
		public string AssignationName { get; set; }
		/// <summary>
		/// Материал наружных стен здания
		/// </summary>
		public List<xmlCodeName> Walls { get; set; }
		/// <summary>
		/// Эксплуатационные характеристики
		/// </summary>
		public xmlYear Years { get; set; }
		/// <summary>
		/// Количество этажей (в том числе подземных)
		/// </summary>
		public xmlFloors Floors { get; set; }
		
		public xmlParentOks()
		{
			ObjectType = new xmlCodeName();
			AssignationBuilding = new xmlCodeName();
			Walls = new List<xmlCodeName>();
			Years = new xmlYear();
			Floors = new xmlFloors();
		}
	}
}
