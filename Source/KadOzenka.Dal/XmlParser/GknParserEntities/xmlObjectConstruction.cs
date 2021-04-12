using System.Collections.Generic;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObjectConstruction : xmlObjectParticular
	{
		/// <summary>
		/// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
		/// </summary>
		public List<string> ParentCadastralNumbers { get; set; }
		/// <summary>
		/// Назначение (сооружение, онс)
		/// </summary>
		public string AssignationName { get; set; }
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
		/// Основные характеристики и их значения
		/// </summary>
		public List<xmlCodeNameValue> KeyParameters { get; set; }

		/// <summary>
		/// Вид (виды) разрешенного использования
		/// </summary>
		public List<string> ObjectPermittedUses { get; set; }


		/// <summary>
		/// Сведения о частях сооружения
		/// </summary>
		public List<xmlSubConstruction> SubConstructions { get; set; }


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

		public xmlObjectConstruction(xmlObject obj) : base(obj)
		{
			ParentCadastralNumbers = obj.ParentCadastralNumbers;
			AssignationName = obj.AssignationName;
			Floors = obj.Floors;
			Years = obj.Years;
			Name = obj.NameObject;
			KeyParameters = obj.KeyParameters;
			ObjectPermittedUses = obj.ObjectPermittedUses;
			SubConstructions = obj.SubConstructions;
			FlatsCadastralNumbers = obj.FlatsCadastralNumbers;
			CarParkingSpacesCadastralNumbers = obj.CarParkingSpacesCadastralNumbers;
			UnitedCadastralNumbers = obj.UnitedCadastralNumbers;
			FacilityCadastralNumber = obj.FacilityCadastralNumber;
			FacilityPurpose = obj.FacilityPurpose;
			CulturalHeritage = obj.CulturalHeritage;
		}
	}
}