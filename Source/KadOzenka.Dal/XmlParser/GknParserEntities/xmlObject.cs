using System;
using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser
{
	public class xmlObject
	{
		/// <summary>
		/// Вид объекта недвижимости
		/// </summary>
		public enTypeObject TypeObject;
		/// <summary>
		/// Тип объекта недвижимости
		/// </summary>
		public string TypeRealty;
		/// <summary>
		/// Дата создания
		/// </summary>
		public DateTime DateCreate;
		/// <summary>
		/// Кадастровый номер
		/// </summary>
		public string CadastralNumber;
		/// <summary>
		/// Номер кадастрового квартала
		/// </summary>
		public string CadastralNumberBlock;
		/// <summary>
		/// Кадастровый номер квартиры, в которой расположена комната
		/// </summary>
		public string CadastralNumberFlat;
		/// <summary>
		/// Кадастровый номер здания или сооружения, в котором расположено помещение
		/// </summary>
		public string CadastralNumberOKS;
		/// <summary>
		/// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
		/// </summary>
		public List<string> ParentCadastralNumbers;
		/// <summary>
		/// Кадастровые номера объектов недвижимости, расположенных в пределах земельного участка
		/// </summary>
		public List<string> InnerCadastralNumbers;
		/// <summary>
		/// Сведения о кадастровой стоимости
		/// </summary>
		public xmlCost CadastralCost;
		/// <summary>
		/// Площадь в квадратных метрах
		/// </summary>
		public string Area;
		/// <summary>
		/// Адрес (местоположение)
		/// </summary>
		public xmlAdress Adress;
		/// <summary>
		/// Назначение здания
		/// </summary>
		public xmlCodeName AssignationBuilding;
		/// <summary>
		/// Назначение (сооружение, онс)
		/// </summary>
		public string AssignationName;
		/// <summary>
		/// Назначение помещения
		/// </summary>
		public xmlCodeName AssignationFlatCode;
		/// <summary>
		/// Вид помещения
		/// </summary>
		public xmlCodeName AssignationFlatType;
		/// <summary>
		/// Количество этажей (в том числе подземных)
		/// </summary>
		public xmlFloors Floors;
		/// <summary>
		/// Эксплуатационные характеристики
		/// </summary>
		public xmlYear Years;
		/// <summary>
		/// Наименование участка
		/// </summary>
		public xmlCodeName NameParcel;
		/// <summary>
		/// Наименование ОКС
		/// </summary>
		public string NameObject;
		/// <summary>
		/// Степень готовности в процентах
		/// </summary>
		public string DegreeReadiness;
		/// <summary>
		/// Категория земель
		/// </summary>
		public xmlCodeName Category;
		/// <summary>
		/// Материал наружных стен здания
		/// </summary>
		public List<xmlCodeName> Walls;
		/// <summary>
		/// Основные характеристики и их значения
		/// </summary>
		public List<xmlCodeNameValue> KeyParameters;
		/// <summary>
		/// Местоположение в объекте недвижимости
		/// </summary>
		public List<xmlPosition> PositionsInObject;
		/// <summary>
		/// Разрешенное использование участка
		/// </summary>
		public xmlUtilization Utilization;
		/// <summary>
		/// Сведения об ограничениях (обременениях) прав
		/// </summary>
		public List<xmlEncumbrance> Encumbrances;
		/// <summary>
		/// Сведения о расположении земельного участка в границах зоны или территории
		/// </summary>
		public List<xmlZoneAndTerritory> ZoneAndTerritorys;
		/// <summary>
		/// Сведения о результатах проведения государственного земельного надзора
		/// </summary>
		public List<xmlSupervisionEvent> GovernmentLandSupervision;

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