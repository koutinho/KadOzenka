using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectConstruction : xmlObjectParticular
	{
		/// <summary>
		/// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
		/// </summary>
		public List<string> ParentCadastralNumbers;
		/// <summary>
		/// Назначение (сооружение, онс)
		/// </summary>
		public string AssignationName;
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
		/// Основные характеристики и их значения
		/// </summary>
		public List<xmlCodeNameValue> KeyParameters;
		/// <summary>
		/// Материал стен
		/// </summary>
		public List<xmlCodeName> Walls;

		public xmlObjectConstruction(xmlObject obj) : base(obj)
		{
			ParentCadastralNumbers = obj.ParentCadastralNumbers;
			AssignationName = obj.AssignationName;
			Floors = obj.Floors;
			Years = obj.Years;
			Name = obj.NameObject;
			KeyParameters = obj.KeyParameters;
			Walls = obj.Walls;
		}
	}
}