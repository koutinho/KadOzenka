using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectUncomplited : xmlObjectParticular
	{
		/// <summary>
		/// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
		/// </summary>
		public List<string> ParentCadastralNumbers;
		/// <summary>
		/// Назначение
		/// </summary>
		public string AssignationName;
		/// <summary>
		/// Степень готовности в процентах
		/// </summary>
		public string DegreeReadiness;
		/// <summary>
		/// Основные характеристики и их значения
		/// </summary>
		public List<xmlCodeNameValue> KeyParameters;

		public xmlObjectUncomplited(xmlObject obj) : base(obj)
		{
			ParentCadastralNumbers = obj.ParentCadastralNumbers;
			AssignationName = obj.AssignationName;
			DegreeReadiness = obj.DegreeReadiness;
			KeyParameters = obj.KeyParameters;
		}
	}
}