using System.Collections.Generic;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;

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
		public double? Area { get; set; }
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
		/// <summary>
		/// Вид (виды) разрешенного использования
		/// </summary>
		public List<string> ObjectPermittedUses { get; set; }
		/// <summary>
		/// Сведения о части здания
		/// </summary>
		public List<xmlSubBuildingFlat> SubBuildings { get; set; }
		/// <summary>
		/// Кадастровые номера помещений, расположенных в объекте недвижимости
		/// </summary>
		public List<string> FlatsCadastralNumbers { get; set; }
		/// <summary>
		/// Кадастровые номера машино-мест, расположенных в объекте недвижимости
		/// </summary>
		public List<string> CarParkingSpacesCadastralNumbers { get; set; }
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

		public xmlObjectBuild(xmlObject obj) : base(obj)
		{
			ParentCadastralNumbers = obj.ParentCadastralNumbers;
			Area = obj.Area;
			AssignationBuilding = obj.AssignationBuilding;
			Floors = obj.Floors;
			Years = obj.Years;
			Name = obj.NameObject;
			Walls = obj.Walls;
			ObjectPermittedUses = obj.ObjectPermittedUses;
			SubBuildings = obj.SubBuildingFlats;
			FlatsCadastralNumbers = obj.FlatsCadastralNumbers;
			CarParkingSpacesCadastralNumbers = obj.CarParkingSpacesCadastralNumbers;
			UnitedCadastralNumbers = obj.UnitedCadastralNumbers;
			FacilityCadastralNumber = obj.FacilityCadastralNumber;
			FacilityPurpose = obj.FacilityPurpose;
			CulturalHeritage = obj.CulturalHeritage;
		}
	}
}