using System;
using System.Collections.Generic;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObject
	{
		/// <summary>
		/// Вид объекта недвижимости
		/// </summary>
		public enTypeObject TypeObject { get; set; }
		/// <summary>
		/// Тип объекта недвижимости
		/// </summary>
		public string TypeRealty { get; set; }
		/// <summary>
		/// Дата создания
		/// </summary>
		public DateTime? DateCreate { get; set; }
		/// <summary>
		/// Кадастровый номер
		/// </summary>
		public string CadastralNumber { get; set; }
		/// <summary>
		/// Номер кадастрового квартала
		/// </summary>
		public string CadastralNumberBlock { get; set; }
		/// <summary>
		/// Кадастровый номер квартиры, в которой расположена комната
		/// </summary>
		public string CadastralNumberFlat { get; set; }
		/// <summary>
		/// Кадастровый номер здания или сооружения, в котором расположено помещение
		/// </summary>
		public string CadastralNumberOKS { get; set; }
		/// <summary>
		/// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
		/// </summary>
		public List<string> ParentCadastralNumbers { get; set; }
		/// <summary>
		/// Кадастровые номера объектов недвижимости, расположенных в пределах земельного участка
		/// </summary>
		public List<string> InnerCadastralNumbers { get; set; }
		/// <summary>
		/// Сведения о кадастровой стоимости
		/// </summary>
		public xmlCost CadastralCost { get; set; }
		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public double? Area { get; set; }
		/// <summary>
		/// Погрешность измерения
		/// </summary>
		public double? AreaInaccuracy { get; set; }
		/// <summary>
		/// Адрес (местоположение)
		/// </summary>
		public xmlAdress Adress { get; set; }
		/// <summary>
		/// Назначение здания
		/// </summary>
		public xmlCodeName AssignationBuilding { get; set; }
		/// <summary>
		/// Назначение (сооружение, онс)
		/// </summary>
		public string AssignationName { get; set; }
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
		/// Количество этажей (в том числе подземных)
		/// </summary>
		public xmlFloors Floors { get; set; }
		/// <summary>
		/// Эксплуатационные характеристики
		/// </summary>
		public xmlYear Years { get; set; }
		/// <summary>
		/// Наименование участка
		/// </summary>
		public xmlCodeName NameParcel { get; set; }
		/// <summary>
		/// Наименование ОКС
		/// </summary>
		public string NameObject { get; set; }
		/// <summary>
		/// Степень готовности в процентах
		/// </summary>
		public long? DegreeReadiness { get; set; }
		/// <summary>
		/// Категория земель
		/// </summary>
		public xmlCodeName Category { get; set; }
		/// <summary>
		/// Материал наружных стен здания
		/// </summary>
		public List<xmlCodeName> Walls { get; set; }
		/// <summary>
		/// Основные характеристики и их значения
		/// </summary>
		public List<xmlCodeNameValue> KeyParameters { get; set; }
		/// <summary>
		/// Расположение в пределах объекта, не имеющего этажи
		/// </summary>
		public xmlPos Position { get; set; }
		/// <summary>
		/// Расположение в пределах объекта, не имеющего этажи
		/// </summary>
		public List<xmlLevel> Levels { get; set; }
		/// <summary>
		/// Разрешенное использование участка
		/// </summary>
		public xmlUtilization Utilization { get; set; }
		/// <summary>
		/// Сведения о природных объектах
		/// </summary>
		public List<xmlNaturalObject> NaturalObjects { get; set; }
		/// <summary>
		/// Сведения о частях участка
		/// </summary>
		public List<xmlSubParcel> SubParcels { get; set; }
		/// <summary>
		/// Сведения о расположении земельного участка в границах зоны или территории
		/// </summary>
		public List<xmlZoneAndTerritory> ZoneAndTerritorys { get; set; }
		/// <summary>
		/// Сведения о результатах проведения государственного земельного надзора
		/// </summary>
		public List<xmlSupervisionEvent> GovernmentLandSupervision { get; set; }
		/// <summary>
		/// Учетный номер утвержденного проекта межевания территории
		/// </summary>
		public string SurveyingProjectNum { get; set; }
		/// <summary>
		/// Реквизиты решения
		/// </summary>
		public xmlDocument SurveyingProjectDecisionRequisites { get; set; }
		/// <summary>
		/// Сведения о создании (эксплуатации) на земельном участке наёмного дома
		/// </summary>
		public xmlHiredHouse HiredHouse { get; set; }
		/// <summary>
		/// Сведения об ограничении оборотоспособности земельного участка в соответствии со статьей 11 Федерального закона от 1 мая 2016 г. № 119-ФЗ
		/// </summary>
		public string LimitedCirculation { get; set; }
		/// <summary>
		/// Вид (виды) разрешенного использования
		/// </summary>
		public List<string> ObjectPermittedUses { get; set; }
		/// <summary>
		/// Сведения о части здания, части помещения
		/// </summary>
		public List<xmlSubBuildingFlat> SubBuildingFlats { get; set; }
		/// <summary>
		/// Сведения о частях сооружения
		/// </summary>
		public List<xmlSubConstruction> SubConstructions { get; set; }
		/// <summary>
		/// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
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
		/// <summary>
		/// Характеристики объекта недвижимости, в котором расположено помещение, машино-место
		/// </summary>
		public xmlParentOks ParentOks { get; set; }
		/// <summary>
		/// Дата оценки
		/// </summary>
		public DateTime AssessmentDate { get; set; }

		public xmlObject(enTypeObject typeObject, string cadastralNumber, DateTime? dateCreate, DateTime assessmentDate)
		{
			TypeObject = typeObject;
			DateCreate = dateCreate;
			CadastralNumber = cadastralNumber;
			ParentCadastralNumbers = new List<string>();
			InnerCadastralNumbers = new List<string>();
			Walls = new List<xmlCodeName>();
			KeyParameters = new List<xmlCodeNameValue>();
			Levels = new List<xmlLevel>();
			NaturalObjects = new List<xmlNaturalObject>();
			SubParcels = new List<xmlSubParcel>();
			ZoneAndTerritorys = new List<xmlZoneAndTerritory>();
			GovernmentLandSupervision = new List<xmlSupervisionEvent>();
			ObjectPermittedUses = new List<string>();
			SubBuildingFlats = new List<xmlSubBuildingFlat>();
			SubConstructions = new List<xmlSubConstruction>();
			FlatsCadastralNumbers = new List<string>();
			CarParkingSpacesCadastralNumbers = new List<string>();
			UnitedCadastralNumbers = new List<string>();
			AssessmentDate = assessmentDate;
			Adress = new xmlAdress();
			CadastralCost = new xmlCost();
			NameParcel = new xmlCodeName();
			Category = new xmlCodeName();
			Utilization = new xmlUtilization();
			HiredHouse = new xmlHiredHouse();
			AssignationBuilding = new xmlCodeName();
			Years = new xmlYear();
			Floors = new xmlFloors();
			CulturalHeritage = new xmlCulturalHeritage();
			ParentOks = new xmlParentOks();
			AssignationFlatCode = new xmlCodeName();
			AssignationFlatType = new xmlCodeName();
			AssignationSpecialType = new xmlCodeName();
			Position = new xmlPos();
		}
		public override string ToString()
		{
			return CadastralNumber;
		}
	}
}