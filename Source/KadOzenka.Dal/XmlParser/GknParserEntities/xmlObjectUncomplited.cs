using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectUncomplited : xmlObjectParticular
	{
		/// <summary>
		/// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
		/// </summary>
		public List<string> ParentCadastralNumbers { get; set; }
		/// <summary>
		/// Назначение
		/// </summary>
		public string AssignationName { get; set; }
		/// <summary>
		/// Степень готовности в процентах
		/// </summary>
		public long? DegreeReadiness { get; set; }
		/// <summary>
		/// Основные характеристики и их значения
		/// </summary>
		public List<xmlCodeNameValue> KeyParameters { get; set; }
		/// <summary>
		/// Кадастровый номер, если объект недвижимости входит в состав предприятия как имущественного комплекса
		/// </summary>
		public string FacilityCadastralNumber { get; set; }
		/// <summary>
		/// Назначение предприятия как имущественного комплекса, если объект недвижимости входит в состав предприятия как имущественного комплекса
		/// </summary>
		public string FacilityPurpose { get; set; }

		public xmlObjectUncomplited(xmlObject obj) : base(obj)
		{
			ParentCadastralNumbers = obj.ParentCadastralNumbers;
			AssignationName = obj.AssignationName;
			DegreeReadiness = obj.DegreeReadiness;
			KeyParameters = obj.KeyParameters;
			FacilityCadastralNumber = obj.FacilityCadastralNumber;
			FacilityPurpose = obj.FacilityPurpose;
		}
	}
}