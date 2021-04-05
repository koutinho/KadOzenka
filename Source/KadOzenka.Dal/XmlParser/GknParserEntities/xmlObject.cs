using System;
using System.Collections.Generic;

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
		public DateTime DateCreate { get; set; }
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
		public string Area { get; set; }
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
		public string DegreeReadiness { get; set; }
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
		/// Местоположение в объекте недвижимости
		/// </summary>
		public List<xmlPosition> PositionsInObject { get; set; }
		/// <summary>
		/// Разрешенное использование участка
		/// </summary>
		public xmlUtilization Utilization { get; set; }
		/// <summary>
		/// Сведения об ограничениях (обременениях) прав
		/// </summary>
		public List<xmlEncumbrance> Encumbrances { get; set; }
		/// <summary>
		/// Сведения о расположении земельного участка в границах зоны или территории
		/// </summary>
		public List<xmlZoneAndTerritory> ZoneAndTerritorys { get; set; }
		/// <summary>
		/// Сведения о результатах проведения государственного земельного надзора
		/// </summary>
		public List<xmlSupervisionEvent> GovernmentLandSupervision { get; set; }

		public xmlObject(enTypeObject typeObject, string cadastralNumber, DateTime dateCreate)
		{
			TypeObject = typeObject;
			DateCreate = dateCreate;
			CadastralNumber = cadastralNumber;
			ParentCadastralNumbers = new List<string>();
			InnerCadastralNumbers = new List<string>();
			Walls = new List<xmlCodeName>();
			PositionsInObject = new List<xmlPosition>();
			KeyParameters = new List<xmlCodeNameValue>();
			Encumbrances = new List<xmlEncumbrance>();
			ZoneAndTerritorys = new List<xmlZoneAndTerritory>();
			GovernmentLandSupervision = new List<xmlSupervisionEvent>();
			Floors = new xmlFloors();
			Years = new xmlYear();
			AssignationBuilding = new xmlCodeName();
			AssignationFlatCode = new xmlCodeName();
			AssignationFlatType = new xmlCodeName();
			Utilization = new xmlUtilization();
		}
		public override string ToString()
		{
			return CadastralNumber;
		}
	}
}