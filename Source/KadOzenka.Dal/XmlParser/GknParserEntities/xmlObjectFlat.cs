using System.Collections.Generic;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectFlat : xmlObjectParticular
	{
		/// <summary>
		/// Кадастровый номер квартиры, в которой расположена комната
		/// </summary>
		public string CadastralNumberFlat { get; set; }
		/// <summary>
		/// Кадастровый номер здания или сооружения, в котором расположено помещение
		/// </summary>
		public string CadastralNumberOKS { get; set; }
		/// <summary>
		/// Характеристики объекта недвижимости, в котором расположено помещение, машино-место
		/// </summary>
		public xmlParentOks ParentOks { get; set; }
		/// <summary>
		/// Наименование
		/// </summary>
		public string Name { get; set; }


		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public double? Area { get; set; }

		/// <summary>
		/// Расположение в пределах объекта, не имеющего этажи
		/// </summary>
		public xmlPos Position { get; set; }
		/// <summary>
		/// Расположение в пределах объекта, не имеющего этажи
		/// </summary>
		public List<xmlLevel> Levels { get; set; }

		/// <summary>
		/// Назначение помещения
		/// </summary>
		public xmlCodeName AssignationFlatCode { get; set; }
		/// <summary>
		/// Вид помещения
		/// </summary>
		public xmlCodeName AssignationFlatType { get; set; }
		/// <summary>
		/// Вид жилого помещения специализированного жилищного фонда
		/// </summary>
		public xmlCodeName AssignationSpecialType { get; set; }
		/// <summary>
		/// Нежилое помещение - общее имущество в многоквартирном доме (True - да)
		/// </summary>
		public bool? AssignationTotalAssets { get; set; }
		/// <summary>
		/// Нежилое помещение - помещение вспомогательного использования (True - да)
		/// </summary>
		public bool? AssignationAuxiliaryFlat { get; set; }

		/// <summary>
		/// Вид (виды) разрешенного использования
		/// </summary>
		public List<string> ObjectPermittedUses { get; set; }
		/// <summary>
		/// Сведения о части помещения
		/// </summary>
		public List<xmlSubBuildingFlat> SubFlats { get; set; }

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
		/// <summary>
		/// Сведения о включении объекта недвижимости в единый государственный реестр объектов культурного наследия или об отнесении объекта недвижимости к выявленным объектам культурного наследия
		/// </summary>
		public xmlCulturalHeritage CulturalHeritage { get; set; }

		public xmlObjectFlat(xmlObject obj) : base(obj)
		{
			CadastralNumberFlat = obj.CadastralNumberFlat;
			CadastralNumberOKS = obj.CadastralNumberOKS;
			ParentOks = obj.ParentOks;
			Name = obj.NameObject;
			Area = obj.Area;
			AssignationFlatCode = obj.AssignationFlatCode;
			AssignationFlatType = obj.AssignationFlatType;
			AssignationSpecialType = obj.AssignationSpecialType;
			AssignationTotalAssets = obj.AssignationTotalAssets;
			AssignationAuxiliaryFlat = obj.AssignationAuxiliaryFlat;
			ObjectPermittedUses = obj.ObjectPermittedUses;
			SubFlats = obj.SubBuildingFlats;
			UnitedCadastralNumbers = obj.UnitedCadastralNumbers;
			FacilityCadastralNumber = obj.FacilityCadastralNumber;
			FacilityPurpose = obj.FacilityPurpose;
			CulturalHeritage = obj.CulturalHeritage;
			Position = obj.Position;
			Levels = obj.Levels;
		}
	}
}