using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using ObjectModel.Commission;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace KadOzenka.Dal.XmlParser
{
    /// <summary>
    /// Реквизиты документа
    /// </summary>
    public class xmlDocument
    {
        /// <summary>
        /// Код документа
        /// </summary>
        public xmlCodeName CodeDocument;
        /// <summary>
        /// Наименование документа
        /// </summary>
        public string Name;
        /// <summary>
        /// Серия документа
        /// </summary>
        public string Series;
        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number;
        /// <summary>
        /// Дата выдачи (подписания) документа
        /// </summary>
        public DateTime Date;
        /// <summary>
        /// Организация, выдавшая документ. Автор документа
        /// </summary>
        public string IssueOrgan;
        /// <summary>
        /// Особые отметки
        /// </summary>
        public string Desc;
    }
    /// <summary>
    /// Количество этажей (в том числе подземных)
    /// </summary>
    public class xmlFloors
    {
        /// <summary>
        /// Количество этажей
        /// </summary>
        public string Floors;
        /// <summary>
        /// В том числе подземных этажей
        /// </summary>
        public string Underground_Floors;
    }
    /// <summary>
    /// Эксплуатационные характеристики
    /// </summary>
    public class xmlYear
    {
        /// <summary>
        /// Год завершения строительства
        /// </summary>
        public string Year_Built;
        /// <summary>
        /// Год ввода в эксплуатацию по завершении строительства
        /// </summary>
        public string Year_Used;
    }
    /// <summary>
    /// Ограничения (обременения)
    /// </summary>
    public class xmlEncumbrance
    {
        /// <summary>
        /// Содержание ограничения (обременения)
        /// </summary>
        public string Name;
        /// <summary>
        /// Код по справочнику
        /// </summary>
        public xmlCodeName Type;
        /// <summary>
        /// Реестровый номер границы зоны, территории
        /// </summary>
        public string AccountNumber;
        /// <summary>
        /// Кадастровый номер ЗУ, в пользу которого установлен сервитут
        /// </summary>
        public string CadastralNumberRestriction;
        /// <summary>
        /// Площадь
        /// </summary>
        public double Area;
        /// <summary>
        /// Государственная регистрация ограничения (обременения)
        /// </summary>
        public xmlNumberDate Registration;
        /// <summary>
        /// Реквизиты документа, на основании которого возникает ограничение (обременение)
        /// </summary>
        public xmlDocument Document;
    }
    /// <summary>
    /// Сведения о расположении земельного участка в границах зоны или территории
    /// </summary>
    public class xmlZoneAndTerritory
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Description;
        /// <summary>
        /// Вид или наименование по документу
        /// </summary>
        public string CodeZoneDoc;
        /// <summary>
        /// Реестровый номер границы
        /// </summary>
        public string AccountNumber;
        /// <summary>
        /// Содержание ограничения
        /// </summary>
        public string ContentRestrictions;
        /// <summary>
        /// Полностью входит в зону
        /// </summary>
        public bool FullPartly;
        /// <summary>
        /// Реквизиты решения
        /// </summary>
        public xmlDocument Document;
    }
    /// <summary>
    /// Местоположение в объекте недвижимости
    /// </summary>
    public class xmlPosition
    {
        /// <summary>
        /// Уровень (этаж)
        /// </summary>
        public xmlCodeNameValue Position;
        /// <summary>
        /// Номер на плане
        /// </summary>
        public List<string> NumbersOnPlan;
        public xmlPosition()
        {
            Position = new xmlCodeNameValue();
            NumbersOnPlan = new List<string>();
        }
    }
    /// <summary>
    /// Сведения о кадастровой стоимости
    /// </summary>
    public class xmlCost
    {
        /// <summary>
        /// Значение
        /// </summary>
        public double Value;
        /// <summary>
        /// Дата определения кадастровой стоимости
        /// </summary>
        public DateTime DateValuation;
        /// <summary>
        /// Дата внесения сведений о кадастровой стоимости в ЕГРН
        /// </summary>
        public DateTime DateEntering;
        /// <summary>
        /// Дата акта об утверждении кадастровой стоимости
        /// </summary>
        public DateTime DocDate;
        /// <summary>
        /// Дата начала применения кадастровой стоимости
        /// </summary>
        public DateTime ApplicationDate;
        /// <summary>
        /// Номер акта об утверждении кадастровой стоимости
        /// </summary>
        public string DocNumber;
        /// <summary>
        /// Наименование документа об утверждении кадастровой стоимости
        /// </summary>
        public string DocName;
        /// <summary>
        /// Дата утверждения кадастровой стоимости
        /// </summary>
        public DateTime DateApproval;
        /// <summary>
        /// Дата подачи заявления о пересмотре кадастровой стоимости
        /// </summary>
        public DateTime RevisalStatementDate;
    }
    /// <summary>
    /// Адресный уровень
    /// </summary>
    public class xmlAdresLevel
    {
        public string Value;
        public string Type;
    }
    /// <summary>
    /// Адрес
    /// </summary>
    public class xmlAdress
    {
        /// <summary>
        /// КЛАДР
        /// </summary>
        public string KLADR;
        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string PostalCode;
        /// <summary>
        /// Регион
        /// </summary>
        public string Region;
        /// <summary>
        /// Неформализованное описание
        /// </summary>
        public string Place;
        /// <summary>
        /// Иное
        /// </summary>
        public string Other;
        /// <summary>
        /// Район
        /// </summary>
        public xmlAdresLevel District;
        /// <summary>
        /// Населенный пункт
        /// </summary>
        public xmlAdresLevel Locality;
        /// <summary>
        /// Муниципальное образование
        /// </summary>
        public xmlAdresLevel City;
        /// <summary>
        /// Городской район
        /// </summary>
        public xmlAdresLevel UrbanDistrict;
        /// <summary>
        /// Улица
        /// </summary>
        public xmlAdresLevel Street;
        /// <summary>
        /// Дом
        /// </summary>
        public xmlAdresLevel Level1;
        /// <summary>
        /// Корпус
        /// </summary>
        public xmlAdresLevel Level2;
        /// <summary>
        /// Строение
        /// </summary>
        public xmlAdresLevel Level3;
        /// <summary>
        /// Квартира
        /// </summary>
        public xmlAdresLevel Apartment;

        public static string GetTextAdress(xmlAdress adress)
        {
            string res = string.Empty;
            if (adress.PostalCode != string.Empty && adress.PostalCode!=null) res += adress.PostalCode + ", ";
            if (adress.Region != string.Empty && adress.Region != null) res += adress.Region + ", ";
            if (adress.District != null) res += adress.District.Type + " " + adress.District.Value + ", ";
            if (adress.City != null) res += adress.City.Type + " " + adress.City.Value + ", ";
            if (adress.UrbanDistrict != null) res += adress.UrbanDistrict.Type + " " + adress.UrbanDistrict.Value + ", ";
            if (adress.Locality != null) res += adress.Locality.Type + " " + adress.Locality.Value + ", ";
            if (adress.Street != null) res += adress.Street.Type + " " + adress.Street.Value + ", ";
            if (adress.Level1 != null) res += adress.Level1.Type + " " + adress.Level1.Value + ", ";
            if (adress.Level2 != null) res += adress.Level2.Type + " " + adress.Level2.Value + ", ";
            if (adress.Level3 != null) res += adress.Level3.Type + " " + adress.Level3.Value + ", ";
            if (adress.Apartment != null) res += adress.Apartment.Type + " " + adress.Apartment.Value + ", ";
            if (adress.Other != string.Empty && adress.Other != null) res += adress.Other + ", ";
            res = res.Trim().TrimEnd(',');
            return res;
        }
        public static string GetTextPlace(xmlAdress adress)
        {
            return (adress.Place==null)?string.Empty: adress.Place;
        }
    }
    /// <summary>
    /// Разрешенное использование участка
    /// </summary>
    public class xmlUtilization
    {
        /// <summary>
        /// Вид разрешенного использования в соответствии с ранее использовавшимся классификатором
        /// </summary>
        public xmlCodeName Utilization;
        /// <summary>
        /// Вид разрешенного использования земельного участка в соответствии с классификатором, утвержденным приказом Минэкономразвития России от 01.09.2014 № 540
        /// </summary>
        public xmlCodeName LandUse;
        /// <summary>
        /// Вид использования участка по документу
        /// </summary>
        public string ByDoc;
        /// <summary>
        /// Разрешенное использование (текстовое описание)
        /// </summary>
        public string PermittedUseText;

        public xmlUtilization()
        {
            Utilization = new xmlCodeName();
            LandUse = new xmlCodeName();
        }
    }
    /// <summary>
    /// Выявленное правонарушение
    /// </summary>
    public class xmlIdentifiedViolations
    {
        /// <summary>
        /// Площадь (в кв. м)
        /// </summary>
        public double Area;
        /// <summary>
        /// Вид выявленного правонарушения
        /// </summary>
        public string TypeViolations;
        /// <summary>
        /// Признаки выявленного правонарушения
        /// </summary>
        public string SignViolations;
    }
    /// <summary>
    /// Сведения об устранении выявленного нарушения
    /// </summary>
    public class xmlElimination
    {
        /// <summary>
        /// Отметка об устранении выявленного нарушения: 1(true)-устранено
        /// </summary>
        public bool EliminationMark;
        /// <summary>
        /// Наименование органа, принявшего решение об устранении правонарушения
        /// </summary>
        public string EliminationAgency;
    }
    /// <summary>
    /// Сведения о результатах проведения государственного земельного надзора
    /// </summary>
    public class xmlSupervisionEvent
    {
        /// <summary>
        /// Наименование органа, проводившего мероприятие по государственному земельному надзору
        /// </summary>
        public string Agency;
        /// <summary>
        /// Мероприятие государственного земельного надзора по соблюдению требований законодательства (плановая, внеплановая проверка, административное обследование)
        /// </summary>
        public xmlCodeName EventName;
        /// <summary>
        /// Форма проведения плановой или внеплановой проверки
        /// </summary>
        public xmlCodeName EventForm;
        /// <summary>
        /// Дата окончания проверки
        /// </summary>
        public DateTime InspectionEnd;
        /// <summary>
        /// Наличие нарушения: правонарушение выявлено (1-true)/не выявлено (0-false)
        /// </summary>
        public bool AvailabilityViolations;
        /// <summary>
        /// Выявленное правонарушение
        /// </summary>
        public xmlIdentifiedViolations IdentifiedViolations;
        /// <summary>
        /// Реквизиты оформленных документов
        /// </summary>
        public xmlDocument DocRequisites;
        /// <summary>
        /// Сведения об устранении выявленного нарушения
        /// </summary>
        public xmlElimination Elimination;
        /// <summary>
        /// Реквизиты документа, содержащего сведения об устранении правонарушения
        /// </summary>
        public xmlDocument EliminationDocRequisites;
    }
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
    public class xmlObjectParcel
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
        /// Наименование участка
        /// </summary>
        public xmlCodeName Name;
        /// <summary>
        /// Категория земель
        /// </summary>
        public xmlCodeName Category;
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

        public xmlObjectParcel(xmlObject obj)
        {
            TypeObject = obj.TypeObject;
            TypeRealty = obj.TypeRealty;
            DateCreate = obj.DateCreate;
            CadastralNumber = obj.CadastralNumber;
            CadastralNumberBlock = obj.CadastralNumberBlock;
            InnerCadastralNumbers = obj.InnerCadastralNumbers;
            CadastralCost = obj.CadastralCost;
            Area = obj.Area;
            Adress = obj.Adress;
            Name = obj.NameParcel;
            Category = obj.Category;
            Utilization = obj.Utilization;
            Encumbrances = obj.Encumbrances;
            ZoneAndTerritorys = obj.ZoneAndTerritorys;
            GovernmentLandSupervision = obj.GovernmentLandSupervision;
        }
        public override string ToString()
        {
            return CadastralNumber;
        }
    }
    public class xmlObjectBuild
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
        /// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
        /// </summary>
        public List<string> ParentCadastralNumbers;
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
        /// Материал наружных стен здания
        /// </summary>
        public List<xmlCodeName> Walls;

        public xmlObjectBuild(xmlObject obj)
        {
            TypeRealty = obj.TypeRealty;
            TypeObject = obj.TypeObject;
            DateCreate = obj.DateCreate;
            CadastralNumber = obj.CadastralNumber;
            CadastralNumberBlock = obj.CadastralNumberBlock;
            ParentCadastralNumbers = obj.ParentCadastralNumbers;
            CadastralCost = obj.CadastralCost;
            Area = obj.Area;
            Adress = obj.Adress;
            AssignationBuilding = obj.AssignationBuilding;
            Floors = obj.Floors;
            Years = obj.Years;
            Name = obj.NameObject;
            Walls = obj.Walls;
        }
        public override string ToString()
        {
            return CadastralNumber;
        }
    }
    public class xmlObjectConstruction
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
        /// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
        /// </summary>
        public List<string> ParentCadastralNumbers;
        /// <summary>
        /// Сведения о кадастровой стоимости
        /// </summary>
        public xmlCost CadastralCost;
        /// <summary>
        /// Адрес (местоположение)
        /// </summary>
        public xmlAdress Adress;
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

        public xmlObjectConstruction(xmlObject obj)
        {
            TypeRealty = obj.TypeRealty;
            TypeObject = obj.TypeObject;
            DateCreate = obj.DateCreate;
            CadastralNumber = obj.CadastralNumber;
            CadastralNumberBlock = obj.CadastralNumberBlock;
            ParentCadastralNumbers = obj.ParentCadastralNumbers;
            CadastralCost = obj.CadastralCost;
            Adress = obj.Adress;
            AssignationName = obj.AssignationName;
            Floors = obj.Floors;
            Years = obj.Years;
            Name = obj.NameObject;
            KeyParameters = obj.KeyParameters;
        }
        public override string ToString()
        {
            return CadastralNumber;
        }
    }
    public class xmlObjectUncomplited
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
        /// Кадастровый номер земельного участка (земельных участков), в пределах которого (которых) расположен данный объект недвижимости
        /// </summary>
        public List<string> ParentCadastralNumbers;
        /// <summary>
        /// Сведения о кадастровой стоимости
        /// </summary>
        public xmlCost CadastralCost;
        /// <summary>
        /// Адрес (местоположение)
        /// </summary>
        public xmlAdress Adress;
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

        public xmlObjectUncomplited(xmlObject obj)
        {
            TypeRealty = obj.TypeRealty;
            TypeObject = obj.TypeObject;
            DateCreate = obj.DateCreate;
            CadastralNumber = obj.CadastralNumber;
            CadastralNumberBlock = obj.CadastralNumberBlock;
            ParentCadastralNumbers = obj.ParentCadastralNumbers;
            CadastralCost = obj.CadastralCost;
            Adress = obj.Adress;
            AssignationName = obj.AssignationName;
            DegreeReadiness = obj.DegreeReadiness;
            KeyParameters = obj.KeyParameters;
        }
        public override string ToString()
        {
            return CadastralNumber;
        }
    }
    public class xmlObjectFlat
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
        /// Назначение помещения
        /// </summary>
        public xmlCodeName AssignationFlatCode;
        /// <summary>
        /// Вид помещения
        /// </summary>
        public xmlCodeName AssignationFlatType;
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name;
        /// <summary>
        /// Местоположение в объекте недвижимости
        /// </summary>
        public List<xmlPosition> PositionsInObject;

        /// <summary>
        /// Кадастровый номер здания или сооружения, в котором расположено помещение
        /// </summary>
        public string CadastralNumberOKS;
        /// <summary>
        /// Количество этажей (в том числе подземных)
        /// </summary>
        public xmlFloors parentFloors;
        /// <summary>
        /// Эксплуатационные характеристики
        /// </summary>
        public xmlYear parentYears;
        /// <summary>
        /// Назначение здания
        /// </summary>
        public xmlCodeName parentAssignationBuilding;
        /// <summary>
        /// Назначение (сооружение, онс)
        /// </summary>
        public string parentAssignationName;
        /// <summary>
        /// Материал наружных стен здания
        /// </summary>
        public List<xmlCodeName> parentWalls;

        public xmlObjectFlat(xmlObject obj)
        {
            TypeRealty = obj.TypeRealty;
            TypeObject = obj.TypeObject;
            DateCreate = obj.DateCreate;
            CadastralNumber = obj.CadastralNumber;
            CadastralNumberBlock = obj.CadastralNumberBlock;
            CadastralNumberFlat = obj.CadastralNumberFlat;
            CadastralCost = obj.CadastralCost;
            Area = obj.Area;
            Adress = obj.Adress;
            AssignationFlatCode = obj.AssignationFlatCode;
            AssignationFlatType = obj.AssignationFlatType;
            Name = obj.NameObject;
            PositionsInObject = obj.PositionsInObject;
            CadastralNumberOKS = obj.CadastralNumberOKS;
            parentFloors = obj.Floors;
            parentYears = obj.Years;
            parentAssignationBuilding = obj.AssignationBuilding;
            parentAssignationName = obj.AssignationName;
            parentWalls = obj.Walls;
        }
        public override string ToString()
        {
            return CadastralNumber;
        }
    }
    public class xmlObjectCarPlace
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
        /// Местоположение в объекте недвижимости
        /// </summary>
        public List<xmlPosition> PositionsInObject;

        /// <summary>
        /// Кадастровый номер здания или сооружения, в котором расположено помещение
        /// </summary>
        public string CadastralNumberOKS;
        /// <summary>
        /// Количество этажей (в том числе подземных)
        /// </summary>
        public xmlFloors parentFloors;
        /// <summary>
        /// Эксплуатационные характеристики
        /// </summary>
        public xmlYear parentYears;
        /// <summary>
        /// Назначение здания
        /// </summary>
        public xmlCodeName parentAssignationBuilding;
        /// <summary>
        /// Назначение (сооружение, онс)
        /// </summary>
        public string parentAssignationName;
        /// <summary>
        /// Материал наружных стен здания
        /// </summary>
        public List<xmlCodeName> parentWalls;

        public xmlObjectCarPlace(xmlObject obj)
        {
            TypeRealty = obj.TypeRealty;
            TypeObject = obj.TypeObject;
            DateCreate = obj.DateCreate;
            CadastralNumber = obj.CadastralNumber;
            CadastralNumberBlock = obj.CadastralNumberBlock;
            CadastralCost = obj.CadastralCost;
            Area = obj.Area;
            Adress = obj.Adress;
            PositionsInObject = obj.PositionsInObject;
            CadastralNumberOKS = obj.CadastralNumberOKS;
            parentFloors = obj.Floors;
            parentYears = obj.Years;
            parentAssignationBuilding = obj.AssignationBuilding;
            parentAssignationName = obj.AssignationName;
            parentWalls = obj.Walls;
        }
        public override string ToString()
        {
            return CadastralNumber;
        }
    }
    public class xmlObjectList
    {
        public List<xmlObjectBuild> Buildings;
        public List<xmlObjectConstruction> Constructions;
        public List<xmlObjectUncomplited> Uncompliteds;
        public List<xmlObjectFlat> Flats;
        public List<xmlObjectCarPlace> CarPlaces;
        public List<xmlObjectParcel> Parcels;
        public object myLock;

        public xmlObjectList()
        {
            Buildings = new List<xmlObjectBuild>();
            Constructions = new List<xmlObjectConstruction>();
            Uncompliteds = new List<xmlObjectUncomplited>();
            Flats = new List<xmlObjectFlat>();
            CarPlaces = new List<xmlObjectCarPlace>();
            Parcels = new List<xmlObjectParcel>();
            myLock = new object();
        }

        public void Add(xmlObject obj)
        {
            lock (myLock)
            {
                switch (obj.TypeObject)
                {
                    case enTypeObject.toBuilding:
                        Buildings.Add(new xmlObjectBuild(obj));
                        break;
                    case enTypeObject.toConstruction:
                        Constructions.Add(new xmlObjectConstruction(obj));
                        break;
                    case enTypeObject.toFlat:
                        Flats.Add(new xmlObjectFlat(obj));
                        break;
                    case enTypeObject.toCarPlace:
                        CarPlaces.Add(new xmlObjectCarPlace(obj));
                        break;
                    case enTypeObject.toUncomplited:
                        Uncompliteds.Add(new xmlObjectUncomplited(obj));
                        break;
                    case enTypeObject.toParcel:
                        Parcels.Add(new xmlObjectParcel(obj));
                        break;
                    default:
                        break;
                }
            };
        }

        public void Add(xmlObjectList objs)
        {
            Buildings.AddRange(objs.Buildings);
            Constructions.AddRange(objs.Constructions);
            Uncompliteds.AddRange(objs.Uncompliteds);
            Flats.AddRange(objs.Flats);
            CarPlaces.AddRange(objs.CarPlaces);
            Parcels.AddRange(objs.Parcels);
        }
        public void Clear()
        {
            Buildings.ForEach(x => x = null);
            Buildings.Clear();
            Constructions.ForEach(x => x = null);
            Constructions.Clear();
            Uncompliteds.ForEach(x => x = null);
            Uncompliteds.Clear();
            Flats.ForEach(x => x = null);
            Flats.Clear();
            CarPlaces.ForEach(x => x = null);
            CarPlaces.Clear();
            Parcels.ForEach(x => x = null);
            Parcels.Clear();
        }
    }
    public class xmlImportGkn
    {
        static xsdDictionary dictAssignationBuild = null;
        static xsdDictionary dictWall = null;
        static xsdDictionary dictTypeParameter = null;
        static xsdDictionary dictAssignationFlat = null;
        static xsdDictionary dictAssignationFlatType = null;
        static xsdDictionary dictStrorey = null;
        static xsdDictionary dictCat = null;
        static xsdDictionary dictName = null;
        static xsdDictionary dictUtilizations = null;
        static xsdDictionary dictAllowedUse = null;
        static xsdDictionary dictEncumbrance = null;
        static xsdDictionary dictAllDocuments = null;
        static xsdDictionary dictInspection = null;
        static xsdDictionary dictFormEvents = null;
        static xsdDictionary dictRealty = null;
        public static void FillDictionary(string pathSchema)
        {
            if (dictAssignationBuild == null)
                dictAssignationBuild = new xsdDictionary(pathSchema + "\\dAssBuilding_v02.xsd", "dAssBuilding");
            if (dictWall == null)
                dictWall = new xsdDictionary(pathSchema + "\\dWall_v02.xsd", "dWall");
            if (dictTypeParameter == null)
                dictTypeParameter = new xsdDictionary(pathSchema + "\\dTypeParameter_v01.xsd", "dTypeParameter");
            if (dictAssignationFlat == null)
                dictAssignationFlat = new xsdDictionary(pathSchema + "\\dAssFlat_v02.xsd", "dAssFlat");
            if (dictAssignationFlatType == null)
                dictAssignationFlatType = new xsdDictionary(pathSchema + "\\dAssFlatType_v01.xsd", "dAssFlatType");
            if (dictStrorey == null)
                dictStrorey = new xsdDictionary(pathSchema + "\\dTypeStorey_v01.xsd", "dTypeStorey");
            if (dictCat == null)
                dictCat = new xsdDictionary(pathSchema + "\\dCategories_v01.xsd", "dCategories");
            if (dictName == null)
                dictName = new xsdDictionary(pathSchema + "\\dParcels_v02.xsd", "dParcels");
            if (dictUtilizations == null)
                dictUtilizations = new xsdDictionary(pathSchema + "\\dUtilizations_v01.xsd", "dUtilizations");
            if (dictAllowedUse == null)
                dictAllowedUse = new xsdDictionary(pathSchema + "\\dAllowedUse_v02.xsd", "dAllowedUse");
            if (dictEncumbrance == null)
                dictEncumbrance = new xsdDictionary(pathSchema + "\\dEncumbrances_v04.xsd", "dEncumbrances");
            if (dictAllDocuments == null)
                dictAllDocuments = new xsdDictionary(pathSchema + "\\dAllDocuments_v05.xsd", "dAllDocuments");
            if (dictInspection == null)
                dictInspection = new xsdDictionary(pathSchema + "\\dInspection_v01.xsd", "dInspection");
            if (dictFormEvents == null)
                dictFormEvents = new xsdDictionary(pathSchema + "\\dFormEvents_v01.xsd", "dFormEvents");
            if (dictRealty == null)
                dictRealty = new xsdDictionary(pathSchema + "\\dRealty_v04.xsd", "dRealty");
        }
        public static xmlObjectList GetXmlObject(Stream file)
        {
            xmlObjectList objs = new xmlObjectList();
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.Load(file);

            using (XmlNodeList xnBuildings = xmlFile.GetElementsByTagName("Building"))
            {
                foreach (XmlNode xnBuilding in xnBuildings)
                {
                    objs.Add(GetData(xnBuilding, enTypeObject.toBuilding));
                }
            }


            using (XmlNodeList xnConstructions = xmlFile.GetElementsByTagName("Construction"))
            {
                foreach (XmlNode xnConstruction in xnConstructions)
                {
                    objs.Add(GetData(xnConstruction, enTypeObject.toConstruction));
                }
            }


            using (XmlNodeList xnUnConstructions = xmlFile.GetElementsByTagName("Uncompleted"))
            {
                foreach (XmlNode xnConstruction in xnUnConstructions)
                {
                    objs.Add(GetData(xnConstruction, enTypeObject.toUncomplited));
                }
            }


            using (XmlNodeList xnFlats = xmlFile.GetElementsByTagName("Flat"))
            {
                foreach (XmlNode xnFlat in xnFlats)
                {
                    objs.Add(GetData(xnFlat, enTypeObject.toFlat));
                }
            }

            using (XmlNodeList xnCarParkingSpaces = xmlFile.GetElementsByTagName("CarParkingSpace"))
            {
                foreach (XmlNode xnFlat in xnCarParkingSpaces)
                {
                    objs.Add(GetData(xnFlat, enTypeObject.toCarPlace));
                }
            }


            using (XmlNodeList xnParcels = xmlFile.GetElementsByTagName("Parcel"))
            {
                foreach (XmlNode xnParcel in xnParcels)
                {
                    objs.Add(GetData(xnParcel, enTypeObject.toParcel));
                }
            }

            return objs;
        }
        public static xmlDocument GetDocument(XmlNode xnObjectNode)
        {
            xmlDocument doc = new xmlDocument();
            foreach (XmlNode xnChild in xnObjectNode.ChildNodes)
            {
                if (xnChild.Name == "Name")
                {
                    doc.Name = xnChild.InnerText;
                }
                if (xnChild.Name == "Series")
                {
                    doc.Series = xnChild.InnerText;
                }
                if (xnChild.Name == "Number")
                {
                    doc.Number = xnChild.InnerText;
                }
                if (xnChild.Name == "Date")
                {
                    if (!DateTime.TryParse(xnChild.InnerText, out doc.Date)) doc.Date = DateTime.MinValue;
                }
                if (xnChild.Name == "IssueOrgan")
                {
                    doc.IssueOrgan = xnChild.InnerText;
                }
                if (xnChild.Name == "Desc")
                {
                    doc.Desc = xnChild.InnerText;
                }
                if (xnChild.Name == "CodeDocument")
                {
                    doc.CodeDocument = new xmlCodeName()
                    {
                        Code = xnChild.InnerText,
                        Name = dictAllDocuments.Records.GetRecByCode(xnChild.InnerText).Value
                    };

                }
            }
            return doc;
        }
        public static xmlObject GetData(XmlNode xnObjectNode, enTypeObject typeobject)
        {
            string kn = (xnObjectNode.Attributes["CadastralNumber"] == null) ? string.Empty : xnObjectNode.Attributes["CadastralNumber"].InnerText;
            DateTime dc = (xnObjectNode.Attributes["DateCreated"] == null) ? DateTime.MinValue : Convert.ToDateTime(xnObjectNode.Attributes["DateCreated"].InnerText);
            xmlObject obj = new xmlObject(typeobject, kn, dc);

            #region Импорт
            foreach (XmlNode xnChild in xnObjectNode.ChildNodes)
            {
                switch (xnChild.Name)
                {
                    case "ObjectType":
                        #region Вид объекта
                        obj.TypeRealty = dictRealty.Records.GetValueByCode(xnChild.InnerText);
                        #endregion
                        break;
                    case "CadastralCost":
                        #region Кадастровая стоимость
                        obj.CadastralCost = new xmlCost();
                        if (xnChild.Attributes["Value"] != null) obj.CadastralCost.Value = xnChild.Attributes["Value"].InnerText.ParseToDouble();
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
                            if (xnChild1.Name == "DateValuation")
                            {
                                if (!DateTime.TryParse(xnChild1.InnerText, out obj.CadastralCost.DateValuation)) obj.CadastralCost.DateValuation = DateTime.MinValue;
                            }
                            else
                            if (xnChild1.Name == "DateEntering")
                            {
                                if (!DateTime.TryParse(xnChild1.InnerText, out obj.CadastralCost.DateEntering)) obj.CadastralCost.DateEntering = DateTime.MinValue;
                            }
                            else
                            if (xnChild1.Name == "DocDate")
                            {
                                if (!DateTime.TryParse(xnChild1.InnerText, out obj.CadastralCost.DocDate)) obj.CadastralCost.DocDate = DateTime.MinValue;
                            }
                            else
                            if (xnChild1.Name == "ApplicationDate")
                            {
                                if (!DateTime.TryParse(xnChild1.InnerText, out obj.CadastralCost.ApplicationDate)) obj.CadastralCost.ApplicationDate = DateTime.MinValue;
                            }
                            else
                            if (xnChild1.Name == "DateApproval")
                            {
                                if (!DateTime.TryParse(xnChild1.InnerText, out obj.CadastralCost.DateApproval)) obj.CadastralCost.DateApproval = DateTime.MinValue;
                            }
                            else
                            if (xnChild1.Name == "RevisalStatementDate")
                            {
                                if (!DateTime.TryParse(xnChild1.InnerText, out obj.CadastralCost.RevisalStatementDate)) obj.CadastralCost.RevisalStatementDate = DateTime.MinValue;
                            }
                            else
                            if (xnChild1.Name == "DocNumber")
                            {
                                obj.CadastralCost.DocNumber = xnChild1.InnerText;
                            }
                            else
                            if (xnChild1.Name == "DocName")
                            {
                                obj.CadastralCost.DocName = xnChild1.InnerText;
                            }
                        }
                        #endregion
                        break;
                    case "Area":
                        #region Площадь
                        if (typeobject != enTypeObject.toParcel)
                        {
                            obj.Area = xnChild.InnerText;
                        }
                        else
                        {
                            foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                            {
                                if (xnChild1.Name == "Area")
                                    obj.Area = xnChild1.InnerText;
                            }
                        }
                        #endregion
                        break;
                    case "Floors":
                        #region Этажность
                        obj.Floors = new xmlFloors();
                        if (xnChild.Attributes["Floors"] != null) obj.Floors.Floors = xnChild.Attributes["Floors"].InnerText;
                        if (xnChild.Attributes["UndergroundFloors"] != null) obj.Floors.Underground_Floors = xnChild.Attributes["UndergroundFloors"].InnerText;
                        #endregion
                        break;
                    case "ExploitationChar":
                        #region Год постройки
                        obj.Years = new xmlYear();
                        if (xnChild.Attributes["YearBuilt"] != null) obj.Years.Year_Built = xnChild.Attributes["YearBuilt"].InnerText;
                        if (xnChild.Attributes["YearUsed"] != null) obj.Years.Year_Used = xnChild.Attributes["YearUsed"].InnerText;
                        #endregion
                        break;
                    case "AssignationBuilding":
                        #region Назначение здания
                        obj.AssignationBuilding = new xmlCodeName()
                        {
                            Code = xnChild.InnerText,
                            Name = dictAssignationBuild.Records.GetRecByCode(xnChild.InnerText).Value
                        };
                        #endregion
                        break;
                    case "AssignationName":
                        #region Назначение сооружения
                        obj.AssignationName = xnChild.InnerText;
                        #endregion
                        break;
                    case "Name":
                        #region Наименование
                        if (typeobject != enTypeObject.toParcel)
                        {
                            obj.NameObject = xnChild.InnerText;
                        }
                        else
                        {
                            obj.NameParcel = new xmlCodeName()
                            {
                                Code = xnChild.InnerText,
                                Name = dictName.Records.GetValueByCode(xnChild.InnerText)
                            };
                        }
                        #endregion
                        break;
                    case "ParentCadastralNumbers":
                        #region Номера земельных участков
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
                            if (xnChild1.Name == "CadastralNumber")
                            {
                                obj.ParentCadastralNumbers.Add(xnChild1.InnerText);
                            }
                        }
                        #endregion
                        break;
                    case "CadastralBlock":
                        #region Кадастровый квартал
                        obj.CadastralNumberBlock = xnChild.InnerText;
                        #endregion
                        break;
                    case "ElementsConstruct":
                        #region Материал стен
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
                            if (xnChild1.Name == "Material")
                            {
                                if (xnChild1.Attributes["Wall"] != null)
                                {
                                    string wcode = xnChild1.Attributes["Wall"].InnerText;
                                    obj.Walls.Add(
                                        new xmlCodeName()
                                        {
                                            Code = wcode,
                                            Name = dictWall.Records.GetRecByCode(wcode).Value
                                        });
                                }
                            }
                        }
                        #endregion
                        break;
                    case "Location":
                        #region Адрес
                        obj.Adress = new xmlAdress();
                        XmlNode nodeadres = xnChild;
                        foreach (XmlNode xnChild3 in xnChild.ChildNodes)
                        {
                            if (xnChild3.Name == "Address")
                                nodeadres = xnChild3;
                        }
                        foreach (XmlNode xnChild1 in nodeadres.ChildNodes)
                        {
                            if (xnChild1.Name == "KLADR")
                            {
                                obj.Adress.KLADR = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "PostalCode")
                            {
                                obj.Adress.PostalCode = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "Region")
                            {
                                obj.Adress.Region = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "Note")
                            {
                                obj.Adress.Place = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "Other")
                            {
                                obj.Adress.Other = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "District")
                            {
                                obj.Adress.District = new xmlAdresLevel()
                                {
                                    Type = xnChild1.Attributes["Type"].InnerText,
                                    Value = xnChild1.Attributes["Name"].InnerText
                                };
                            }
                            if (xnChild1.Name == "Locality")
                            {
                                obj.Adress.Locality = new xmlAdresLevel()
                                {
                                    Type = xnChild1.Attributes["Type"].InnerText,
                                    Value = xnChild1.Attributes["Name"].InnerText
                                };
                            }
                            if (xnChild1.Name == "City")
                            {
                                obj.Adress.City = new xmlAdresLevel()
                                {
                                    Type = xnChild1.Attributes["Type"].InnerText,
                                    Value = xnChild1.Attributes["Name"].InnerText
                                };
                            }
                            if (xnChild1.Name == "UrbanDistrict")
                            {
                                obj.Adress.UrbanDistrict = new xmlAdresLevel()
                                {
                                    Type = xnChild1.Attributes["Type"].InnerText,
                                    Value = xnChild1.Attributes["Name"].InnerText
                                };
                            }
                            if (xnChild1.Name == "Street")
                            {
                                obj.Adress.Street = new xmlAdresLevel()
                                {
                                    Type = xnChild1.Attributes["Type"].InnerText,
                                    Value = xnChild1.Attributes["Name"].InnerText
                                };
                            }
                            if (xnChild1.Name == "Level1")
                            {
                                obj.Adress.Level1 = new xmlAdresLevel()
                                {
                                    Type = xnChild1.Attributes["Type"].InnerText,
                                    Value = xnChild1.Attributes["Value"].InnerText
                                };
                            }
                            if (xnChild1.Name == "Level2")
                            {
                                obj.Adress.Level2 = new xmlAdresLevel()
                                {
                                    Type = xnChild1.Attributes["Type"].InnerText,
                                    Value = xnChild1.Attributes["Value"].InnerText
                                };
                            }
                            if (xnChild1.Name == "Level3")
                            {
                                obj.Adress.Level3 = new xmlAdresLevel()
                                {
                                    Type = xnChild1.Attributes["Type"].InnerText,
                                    Value = xnChild1.Attributes["Value"].InnerText
                                };
                            }
                            if (xnChild1.Name == "Apartment")
                            {
                                obj.Adress.Apartment = new xmlAdresLevel()
                                {
                                    Type = xnChild1.Attributes["Type"].InnerText,
                                    Value = xnChild1.Attributes["Value"].InnerText
                                };
                            }
                        }
                        #endregion
                        break;
                    case "KeyParameters":
                        #region Основные характеристики
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
                            if (xnChild1.Name == "KeyParameter")
                            {
                                xmlCodeNameValue tmp = new xmlCodeNameValue();
                                if (xnChild1.Attributes["Type"] != null) { tmp.Code = xnChild1.Attributes["Type"].InnerText; tmp.Name = dictTypeParameter.Records.GetRecByCode(xnChild1.Attributes["Type"].InnerText).Value; };
                                if (xnChild1.Attributes["Value"] != null) tmp.Value = xnChild1.Attributes["Value"].InnerText;
                                obj.KeyParameters.Add(tmp);
                            }
                        }
                        #endregion
                        break;
                    case "DegreeReadiness":
                        #region Процент готовности
                        obj.DegreeReadiness = xnChild.InnerText;
                        #endregion
                        break;
                    case "Assignation":
                        #region назначение помещения
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
                            if (xnChild1.Name == "AssignationCode")
                            {
                                obj.AssignationFlatCode = new xmlCodeName()
                                {
                                    Code = xnChild1.InnerText,
                                    Name = dictAssignationFlat.Records.GetRecByCode(xnChild1.InnerText).Value
                                };
                            }

                            if (xnChild1.Name == "AssignationType")
                            {
                                obj.AssignationFlatType = new xmlCodeName()
                                {
                                    Code = xnChild1.InnerText,
                                    Name = dictAssignationFlatType.Records.GetRecByCode(xnChild1.InnerText).Value
                                };
                            }
                        }
                        #endregion
                        break;
                    case "PositionInObject":
                        #region расположение помещения
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
                            if (xnChild1.Name == "Levels")
                            {
                                foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
                                {
                                    if (xnChild2.Name == "Level")
                                    {
                                        xmlPosition tmp = new xmlPosition();
                                        if (xnChild2.Attributes["Number"] != null) tmp.Position.Value = xnChild2.Attributes["Number"].InnerText;
                                        if (xnChild2.Attributes["Type"] != null)
                                        {
                                            tmp.Position.Code = xnChild2.Attributes["Type"].InnerText;
                                            tmp.Position.Name = dictStrorey.Records.GetValueByCode(xnChild2.Attributes["Type"].InnerText);
                                        }
                                        foreach (XmlNode xnChild3 in xnChild2.ChildNodes)
                                        {
                                            if (xnChild3.Name == "Position")
                                            {
                                                if (xnChild3.Attributes["NumberOnPlan"] != null) tmp.NumbersOnPlan.Add(xnChild3.Attributes["NumberOnPlan"].InnerText);
                                            }
                                        }
                                        obj.PositionsInObject.Add(tmp);
                                    }
                                }
                            }
                            if (xnChild1.Name == "Position")
                            {
                                xmlPosition tmp = new xmlPosition();
                                if (xnChild.Attributes["Number"] != null) tmp.Position.Value = xnChild.Attributes["Number"].InnerText;
                                if (xnChild.Attributes["Type"] != null)
                                {
                                    tmp.Position.Code = xnChild.Attributes["Type"].InnerText;
                                    tmp.Position.Name = dictStrorey.Records.GetValueByCode(xnChild.Attributes["Type"].InnerText);
                                }
                                foreach (XmlNode xnChild3 in xnChild.ChildNodes)
                                {
                                    if (xnChild3.Name == "Position")
                                    {
                                        if (xnChild3.Attributes["NumberOnPlan"] != null) tmp.NumbersOnPlan.Add(xnChild3.Attributes["NumberOnPlan"].InnerText);
                                    }
                                }
                                obj.PositionsInObject.Add(tmp);
                            }
                        }
                        #endregion
                        break;
                    case "ParentOKS":
                        #region Характеристики здания
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
                            if (xnChild1.Name == "CadastralNumberOKS")
                            {
                                obj.CadastralNumberOKS = (xnChild1.InnerText);
                            }
                            //Этажность
                            if (xnChild1.Name == "Floors")
                            {
                                obj.Floors = new xmlFloors();
                                if (xnChild1.Attributes["Floors"] != null) obj.Floors.Floors = xnChild1.Attributes["Floors"].InnerText;
                                if (xnChild1.Attributes["UndergroundFloors"] != null) obj.Floors.Underground_Floors = xnChild1.Attributes["UndergroundFloors"].InnerText;
                            }
                            //Год постройки
                            if (xnChild1.Name == "ExploitationChar")
                            {
                                obj.Years = new xmlYear();
                                if (xnChild1.Attributes["YearBuilt"] != null) obj.Years.Year_Built = xnChild1.Attributes["YearBuilt"].InnerText;
                                if (xnChild1.Attributes["YearUsed"] != null) obj.Years.Year_Used = xnChild1.Attributes["YearUsed"].InnerText;
                            }
                            //Материал стен
                            if (xnChild1.Name == "ElementsConstruct")
                            {
                                foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
                                {
                                    if (xnChild2.Name == "Material")
                                    {
                                        if (xnChild2.Attributes["Wall"] != null)
                                        {
                                            string wcode = xnChild2.Attributes["Wall"].InnerText;
                                            obj.Walls.Add(
                                                new xmlCodeName()
                                                {
                                                    Code = wcode,
                                                    Name = dictWall.Records.GetRecByCode(wcode).Value
                                                });
                                        }
                                    }
                                }
                            }
                            //Назначение здания
                            if (xnChild1.Name == "AssignationBuilding")
                            {
                                obj.AssignationBuilding = new xmlCodeName()
                                {
                                    Code = xnChild1.InnerText,
                                    Name = dictAssignationBuild.Records.GetRecByCode(xnChild1.InnerText).Value
                                };
                            }
                        }
                        #endregion
                        break;
                    case "CadastralNumberOKS":
                        #region Характеристики здания
                        obj.CadastralNumberOKS = (xnChild.InnerText);
                        #endregion
                        break;
                    case "CadastralNumberFlat":
                        #region Кадастровый номер квартиры, в которой расположена комната
                        obj.CadastralNumberFlat = xnChild.InnerText;
                        #endregion
                        break;
                    case "Category":
                        #region Категория земель
                        obj.Category = new xmlCodeName()
                        {
                            Code = xnChild.InnerText,
                            Name = dictCat.Records.GetValueByCode(xnChild.InnerText)
                        };
                        #endregion
                        break;
                    case "Utilization":
                        #region Вид использования
                        obj.Utilization = new xmlUtilization();
                        if (xnChild.Attributes["ByDoc"] != null) obj.Utilization.ByDoc = xnChild.Attributes["ByDoc"].InnerText;
                        if (xnChild.Attributes["PermittedUseText"] != null) obj.Utilization.PermittedUseText = xnChild.Attributes["PermittedUseText"].InnerText;
                        if (xnChild.Attributes["Utilization"] != null)
                        {
                            obj.Utilization.Utilization = new xmlCodeName()
                            {
                                Code = xnChild.Attributes["Utilization"].InnerText,
                                Name = dictUtilizations.Records.GetValueByCode(xnChild.Attributes["Utilization"].InnerText)
                            };
                        }
                        if (xnChild.Attributes["LandUse"] != null)
                        {
                            obj.Utilization.LandUse = new xmlCodeName()
                            {
                                Code = xnChild.Attributes["LandUse"].InnerText,
                                Name = dictAllowedUse.Records.GetValueByCode(xnChild.Attributes["LandUse"].InnerText)
                            };
                        }
                        #endregion
                        break;
                    case "InnerCadastralNumbers":
                        #region Номера оксов
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
                            if (xnChild1.Name == "CadastralNumber")
                            {
                                obj.InnerCadastralNumbers.Add(xnChild1.InnerText);
                            }
                        }
                        #endregion
                        break;
                    case "SubParcels":
                        #region Обременения и ограничения
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
                            if (xnChild1.Name == "SubParcel")
                            {
                                string area = null;
                                foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
                                {
                                    if (xnChild2.Name == "Area")
                                    {
                                        foreach (XmlNode xnChild5 in xnChild2.ChildNodes)
                                        {
                                            if (xnChild5.Name == "Area")
                                            {
                                                area = xnChild5.InnerText;
                                            }
                                        }
                                    }
                                    if (xnChild2.Name == "Encumbrances")
                                    {
                                        foreach (XmlNode xnChild3 in xnChild2.ChildNodes)
                                        {
                                            if (xnChild3.Name == "Encumbrance")
                                            {
                                                xmlEncumbrance encum = new xmlEncumbrance();
                                                if (!double.TryParse(area, out encum.Area))
                                                    encum.Area = double.MinValue;
                                                foreach (XmlNode xnChild4 in xnChild3.ChildNodes)
                                                {
                                                    if (xnChild4.Name == "Type")
                                                    {
                                                        encum.Type = new xmlCodeName()
                                                        {
                                                            Code = xnChild4.InnerText,
                                                            Name = dictEncumbrance.Records.GetValueByCode(xnChild4.InnerText)
                                                        };
                                                    }
                                                    if (xnChild4.Name == "Name")
                                                    {
                                                        encum.Name = xnChild4.InnerText;
                                                    }
                                                    if (xnChild4.Name == "AccountNumber")
                                                    {
                                                        encum.AccountNumber = xnChild4.InnerText;
                                                    }
                                                    if (xnChild4.Name == "CadastralNumberRestriction")
                                                    {
                                                        encum.CadastralNumberRestriction = xnChild4.InnerText;
                                                    }
                                                    if (xnChild4.Name == "Registration")
                                                    {
                                                        encum.Registration = new xmlNumberDate();
                                                        foreach (XmlNode xnChild5 in xnChild4.ChildNodes)
                                                        {
                                                            if (xnChild5.Name == "RightNumber")
                                                            {
                                                                encum.Registration.Number = xnChild5.InnerText;
                                                            }
                                                            if (xnChild5.Name == "RegistrationDate")
                                                            {
                                                                if (!DateTime.TryParse(xnChild5.InnerText, out encum.Registration.Date))
                                                                    encum.Registration.Date = DateTime.MinValue;
                                                            }
                                                        }
                                                    }
                                                    if (xnChild4.Name == "Document")
                                                    {
                                                        encum.Document = GetDocument(xnChild4);
                                                    }
                                                }
                                                obj.Encumbrances.Add(encum);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        break;
                    case "ZonesAndTerritories":
                        #region Сведения о расположении земельного участка в границах зон, территорий
                        foreach (XmlNode xnChild3 in xnChild.ChildNodes)
                        {
                            if (xnChild3.Name == "ZoneAndTerritory")
                            {
                                xmlZoneAndTerritory encum = new xmlZoneAndTerritory();
                                foreach (XmlNode xnChild4 in xnChild3.ChildNodes)
                                {
                                    if (xnChild4.Name == "Description")
                                    {
                                        encum.Description = xnChild4.InnerText;
                                    }
                                    if (xnChild4.Name == "CodeZoneDoc")
                                    {
                                        encum.CodeZoneDoc = xnChild4.InnerText;
                                    }
                                    if (xnChild4.Name == "AccountNumber")
                                    {
                                        encum.AccountNumber = xnChild4.InnerText;
                                    }
                                    if (xnChild4.Name == "ContentRestrictions")
                                    {
                                        encum.ContentRestrictions = xnChild4.InnerText;
                                    }
                                    if (xnChild4.Name == "FullPartly")
                                    {
                                        encum.FullPartly = xnChild4.InnerText == "1" ? true : false;
                                    }
                                    if (xnChild4.Name == "Document")
                                    {
                                        encum.Document = GetDocument(xnChild4);
                                    }
                                }
                                obj.ZoneAndTerritorys.Add(encum);
                            }
                        }
                        #endregion
                        break;
                    case "GovernmentLandSupervision":
                        #region Сведения о результатах проведения государственного земельного надзора
                        foreach (XmlNode xnChild3 in xnChild.ChildNodes)
                        {
                            if (xnChild3.Name == "SupervisionEvent")
                            {
                                xmlSupervisionEvent encum = new xmlSupervisionEvent();
                                foreach (XmlNode xnChild4 in xnChild3.ChildNodes)
                                {
                                    if (xnChild4.Name == "Agency")
                                    {
                                        encum.Agency = xnChild4.InnerText;
                                    }
                                    if (xnChild4.Name == "EventName")
                                    {
                                        encum.EventName = new xmlCodeName()
                                        {
                                            Code = xnChild4.InnerText,
                                            Name = dictInspection.Records.GetValueByCode(xnChild4.InnerText)
                                        };
                                    }
                                    if (xnChild4.Name == "EventForm")
                                    {
                                        encum.EventForm = new xmlCodeName()
                                        {
                                            Code = xnChild4.InnerText,
                                            Name = dictFormEvents.Records.GetValueByCode(xnChild4.InnerText)
                                        };
                                    }
                                    if (xnChild4.Name == "ResultsEvent")
                                    {
                                        foreach (XmlNode xnChild5 in xnChild4.ChildNodes)
                                        {
                                            if (xnChild5.Name == "AvailabilityViolations")
                                            {
                                                encum.AvailabilityViolations = xnChild5.InnerText == "1";
                                            }
                                            if (xnChild5.Name == "IdentifiedViolations")
                                            {
                                                encum.IdentifiedViolations = new xmlIdentifiedViolations();
                                                foreach (XmlNode xnChild6 in xnChild5.ChildNodes)
                                                {
                                                    if (xnChild6.Name == "TypeViolations")
                                                    {
                                                        encum.IdentifiedViolations.TypeViolations = xnChild6.InnerText;
                                                    }
                                                    if (xnChild6.Name == "SignViolations")
                                                    {
                                                        encum.IdentifiedViolations.SignViolations = xnChild6.InnerText;
                                                    }
                                                    if (xnChild6.Name == "Area")
                                                    {
                                                        if (!double.TryParse(xnChild6.InnerText, out encum.IdentifiedViolations.Area))
                                                            encum.IdentifiedViolations.Area = double.MinValue;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (xnChild4.Name == "DocRequisites")
                                    {
                                        encum.DocRequisites = GetDocument(xnChild4);
                                    }
                                    if (xnChild4.Name == "Elimination")
                                    {
                                        encum.Elimination = new xmlElimination();
                                        foreach (XmlNode xnChild6 in xnChild4.ChildNodes)
                                        {
                                            if (xnChild6.Name == "EliminationMark")
                                            {
                                                encum.Elimination.EliminationMark = xnChild6.InnerText == "1";
                                            }
                                            if (xnChild6.Name == "EliminationAgency")
                                            {
                                                encum.Elimination.EliminationAgency = xnChild6.InnerText;
                                            }
                                        }

                                    }
                                    if (xnChild4.Name == "EliminationDocRequisites")
                                    {
                                        encum.EliminationDocRequisites = GetDocument(xnChild4);
                                    }
                                }
                                obj.GovernmentLandSupervision.Add(encum);
                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
            }
            #endregion

            return obj;
        }
    }



}
