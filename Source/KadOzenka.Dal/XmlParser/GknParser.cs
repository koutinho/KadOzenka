using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Core.Register;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport.DataImporterGknNew;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Importers.Base;
using KadOzenka.Dal.DataImport.Validation;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Helpers;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;
using Microsoft.Practices.ObjectBuilder2;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.XmlParser
{
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
        static xsdDictionary dictCultural = null;
        static xsdDictionary dictSpecialTypeFlat = null;
        static xsdDictionary dictNaturalObjects = null;
        static xsdDictionary dictForestUse = null;
        static xsdDictionary dictForestCategoryProtective = null;
        static xsdDictionary dictForestEncumbrances = null;

        public int TotalObjectCounter { get; private set; }
        private readonly object _locker;
        private readonly GbuReportService _gbuReportService;

        public xmlImportGkn(GbuReportService gbuReportService)
        {
	        _locker = new object();
	        _gbuReportService = gbuReportService;
        }

        public static void FillDictionary(string pathSchema)
        {
            if (dictAssignationBuild == null)
                dictAssignationBuild = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dAssBuilding_v02.xsd"), "dAssBuilding");
            if (dictWall == null)
                dictWall = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dWall_v02.xsd"), "dWall");
            if (dictTypeParameter == null)
                dictTypeParameter = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dTypeParameter_v01.xsd"), "dTypeParameter");
            if (dictAssignationFlat == null)
                dictAssignationFlat = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dAssFlat_v02.xsd"), "dAssFlat");
            if (dictAssignationFlatType == null)
                dictAssignationFlatType = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dAssFlatType_v01.xsd"), "dAssFlatType");
            if (dictStrorey == null)
                dictStrorey = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dTypeStorey_v01.xsd"), "dTypeStorey");
            if (dictCat == null)
                dictCat = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dCategories_v01.xsd"), "dCategories");
            if (dictName == null)
                dictName = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dParcels_v02.xsd"), "dParcels");
            if (dictUtilizations == null)
                dictUtilizations = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dUtilizations_v01.xsd"), "dUtilizations");
            if (dictAllowedUse == null)
                dictAllowedUse = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dAllowedUse_v02.xsd"), "dAllowedUse");
            if (dictEncumbrance == null)
                dictEncumbrance = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dEncumbrances_v04.xsd"), "dEncumbrances");
            if (dictAllDocuments == null)
                dictAllDocuments = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dAllDocuments_v05.xsd"), "dAllDocuments");
            if (dictInspection == null)
                dictInspection = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dInspection_v01.xsd"), "dInspection");
            if (dictFormEvents == null)
                dictFormEvents = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dFormEvents_v01.xsd"), "dFormEvents");
            if (dictRealty == null)
                dictRealty = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dRealty_v04.xsd"), "dRealty");
            if (dictCultural == null)
                dictCultural = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dCultural_v01.xsd"), "dCultural");
            if(dictSpecialTypeFlat == null)
	            dictSpecialTypeFlat = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dSpecialTypeFlat_v01.xsd"), "dSpecialTypeFlat");
            if(dictNaturalObjects == null)
                dictNaturalObjects = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dNaturalObjects_v01.xsd"), "dNaturalObjects");
            if(dictForestUse == null)
	            dictForestUse = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dForestUse_v01.xsd"), "dForestUse");
            if(dictForestCategoryProtective == null)
	            dictForestCategoryProtective = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dForestCategoryProtective_v01.xsd"), "dForestCategoryProtective");
            if(dictForestEncumbrances == null)
	            dictForestEncumbrances = new xsdDictionary(PathCombiner.GetFullPath(pathSchema, "dForestEncumbrances_v01.xsd"), "dForestEncumbrances");
        }

        public xmlObjectList GetXmlObject(Stream file, DateTime assessmentDate)
        {
            xmlObjectList objs = new xmlObjectList();
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.Load(file);

            using (XmlNodeList xnBuildings = xmlFile.GetElementsByTagName("Building"))
            {
                foreach (XmlNode xnBuilding in xnBuildings)
                {
	                TotalObjectCounter++;
                    objs.Add(GetData(xnBuilding, enTypeObject.toBuilding, assessmentDate));
                }
            }

            using (XmlNodeList xnConstructions = xmlFile.GetElementsByTagName("Construction"))
            {
                foreach (XmlNode xnConstruction in xnConstructions)
                {
	                TotalObjectCounter++;
                    objs.Add(GetData(xnConstruction, enTypeObject.toConstruction, assessmentDate));
                }
            }

            using (XmlNodeList xnUnConstructions = xmlFile.GetElementsByTagName("Uncompleted"))
            {
                foreach (XmlNode xnConstruction in xnUnConstructions)
                {
	                TotalObjectCounter++;
                    objs.Add(GetData(xnConstruction, enTypeObject.toUncomplited, assessmentDate));
                }
            }

            using (XmlNodeList xnFlats = xmlFile.GetElementsByTagName("Flat"))
            {
                foreach (XmlNode xnFlat in xnFlats)
                {
	                TotalObjectCounter++;
                    objs.Add(GetData(xnFlat, enTypeObject.toFlat, assessmentDate));
                }
            }

            using (XmlNodeList xnCarParkingSpaces = xmlFile.GetElementsByTagName("CarParkingSpace"))
            {
                foreach (XmlNode xnFlat in xnCarParkingSpaces)
                {
	                TotalObjectCounter++;
                    objs.Add(GetData(xnFlat, enTypeObject.toCarPlace, assessmentDate));
                }
            }

            using (XmlNodeList xnParcels = xmlFile.GetElementsByTagName("Parcel"))
            {
                foreach (XmlNode xnParcel in xnParcels)
                {
	                TotalObjectCounter++;
                    objs.Add(GetData(xnParcel, enTypeObject.toParcel, assessmentDate));
                }
            }

            return objs;
        }

        public static xmlDocument GetDocument(XmlNode xnObjectNode)
        {
            xmlDocument doc = new xmlDocument();
            foreach (XmlNode xnChild in xnObjectNode.ChildNodes)
            {
	            if (xnChild.Name == "CodeDocument")
	            {
		            doc.CodeDocument = new xmlCodeName()
		            {
			            Code = xnChild.InnerText,
			            Name = dictAllDocuments.Records.GetRecByCode(xnChild.InnerText).Value
		            };
	            }
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
	                doc.Date = DateTime.TryParse(xnChild.InnerText, out var dateTime) 
		                ? dateTime 
		                : (DateTime?)null;
                }
                if (xnChild.Name == "IssueOrgan")
                {
                    doc.IssueOrgan = xnChild.InnerText;
                }
                if (xnChild.Name == "Desc")
                {
                    doc.Desc = xnChild.InnerText;
                }
            }
            return doc;
        }

        public xmlObject GetData(XmlNode xnObjectNode, enTypeObject typeobject, DateTime assessmentDate)
        {
	        string kn = string.Empty;
	        xmlObject obj = null;
            try
	        { 
			    kn = (xnObjectNode.Attributes["CadastralNumber"] == null) ? string.Empty : xnObjectNode.Attributes["CadastralNumber"].InnerText;
	            DateTime dc = (xnObjectNode.Attributes["DateCreated"] == null) ? DateTime.MinValue : Convert.ToDateTime(xnObjectNode.Attributes["DateCreated"].InnerText);
	            obj = new xmlObject(typeobject, kn, dc, assessmentDate);

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
	                            obj.CadastralCost.DateValuation = DateTime.TryParse(xnChild1.InnerText, out var date) 
		                            ? date 
		                            : (DateTime?)null;
                            }
                            else
                            if (xnChild1.Name == "DateEntering")
                            {
	                            obj.CadastralCost.DateEntering = DateTime.TryParse(xnChild1.InnerText, out var date)
		                            ? date
		                            : (DateTime?)null;
                            }
                            else
                            if (xnChild1.Name == "DateApproval")
                            {
	                            obj.CadastralCost.DateApproval = DateTime.TryParse(xnChild1.InnerText, out var date)
		                            ? date
		                            : (DateTime?)null;
                            }
                            else
                            if (xnChild1.Name == "DocNumber")
                            {
	                            obj.CadastralCost.DocNumber = xnChild1.InnerText;
                            }
                            else
                            if (xnChild1.Name == "DocDate")
                            {
	                            obj.CadastralCost.DocDate = DateTime.TryParse(xnChild1.InnerText, out var date)
		                            ? date
		                            : (DateTime?)null;
                            }
                            else
                            if (xnChild1.Name == "ApplicationDate")
                            {
	                            obj.CadastralCost.ApplicationDate = DateTime.TryParse(xnChild1.InnerText, out var date)
		                            ? date
		                            : (DateTime?)null;
                            }
                            else
                            if (xnChild1.Name == "RevisalStatementDate")
                            {
	                            obj.CadastralCost.RevisalStatementDate = DateTime.TryParse(xnChild1.InnerText, out var date)
		                            ? date
		                            : (DateTime?)null;
                            }
                            else
                            if (xnChild1.Name == "ApplicationLastDate")
                            {
	                            obj.CadastralCost.ApplicationLastDate = DateTime.TryParse(xnChild1.InnerText, out var date)
		                            ? date
		                            : (DateTime?)null;
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
	                        obj.Area = double.TryParse(xnChild.InnerText, out var valResult)
		                        ? valResult
		                        : (double?) null;
                        }
                        else
                        {
                            foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                            {
                                if (xnChild1.Name == "Area")
                                    obj.Area = double.TryParse(xnChild1.InnerText, out var valResult)
	                                    ? valResult
	                                    : (double?)null;
                                if (xnChild1.Name == "Inaccuracy")
	                                obj.AreaInaccuracy = double.TryParse(xnChild1.InnerText, out var valResult)
		                                ? valResult
		                                : (double?)null;
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
                        obj.ParentCadastralNumbers = GetCadastralNumbers(xnChild);
                        #endregion
                        break;
                    case "FlatsCadastralNumbers":
                        #region Кадастровые номера помещений, расположенных в объекте недвижимости
                        obj.FlatsCadastralNumbers = GetCadastralNumbers(xnChild);
	                    #endregion
	                    break;
                    case "CarParkingSpacesCadastralNumbers":
                        #region Кадастровые номера машино-мест, расположенных в объекте недвижимости
                        obj.CarParkingSpacesCadastralNumbers = GetCadastralNumbers(xnChild);
	                    #endregion
	                    break;
                    case "UnitedCadastralNumber":
                        #region Кадастровый номер единого недвижимого комплекса, если объект недвижимости входит в состав единого недвижимого комплекса
                        obj.UnitedCadastralNumbers = GetCadastralNumbers(xnChild);
	                    #endregion
	                    break;
                    case "FacilityCadastralNumber":
                        #region Кадастровый номер и назначение предприятия как имущественного комплекса, если объект недвижимости входит в состав предприятия как имущественного комплекса
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
	                        if (xnChild1.Name == "CadastralNumber")
	                        {
		                        obj.FacilityCadastralNumber = xnChild1.InnerText;
	                        }
	                        if (xnChild1.Name == "Purpose")
	                        {
		                        obj.FacilityPurpose = xnChild1.InnerText;
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
                        if (typeobject == enTypeObject.toParcel)
                        {
	                        foreach (XmlNode xnChild3 in xnChild.ChildNodes)
	                        {
		                        if (xnChild3.Name == "Address")
		                        {
			                        nodeadres = xnChild3;
		                        }
		                        if (xnChild3.Name == "inBounds")
		                        {
			                        switch (xnChild3.InnerText)
			                        {
				                        case "0":
					                        obj.Adress.InBounds = "Расположение ориентира вне границ участка";
					                        break;
				                        case "1":
					                        obj.Adress.InBounds = "Расположение ориентира в границах участка";
					                        break;
				                        case "2":
					                        obj.Adress.InBounds = "Не определено";
					                        break;
			                        }
		                        }
		                        if (xnChild3.Name == "Placed")
		                        {
			                        obj.Adress.Placed = xnChild3.InnerText;
		                        }
		                        if (xnChild3.Name == "Elaboration")
		                        {
			                        obj.Adress.Elaboration = new xmlElaborationLocation();
                                    foreach (XmlNode child in xnChild3.ChildNodes)
			                        {
                                        if (child.Name == "ReferenceMark")
				                        {
					                        obj.Adress.Elaboration.ReferenceMark = child.InnerText;
				                        }
				                        if (child.Name == "Distance")
				                        {
					                        obj.Adress.Elaboration.Distance = child.InnerText;
				                        }
				                        if (child.Name == "Direction")
				                        {
					                        obj.Adress.Elaboration.Direction = child.InnerText;
				                        }
                                    }
		                        }
                            }
                        }

                        foreach (XmlNode xnChild1 in nodeadres.ChildNodes)
                        {
	                        if (xnChild1.Name == "FIAS")
	                        {
		                        obj.Adress.FIAS = xnChild1.InnerText;
	                        }
	                        if (xnChild1.Name == "OKATO")
	                        {
		                        obj.Adress.OKATO = xnChild1.InnerText;
	                        }
                            if (xnChild1.Name == "KLADR")
                            {
                                obj.Adress.KLADR = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "OKTMO")
                            {
	                            obj.Adress.OKTMO = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "PostalCode")
                            {
                                obj.Adress.PostalCode = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "RussianFederation")
                            {
	                            obj.Adress.RussianFederation = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "Region")
                            {
                                obj.Adress.Region = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "Note")
                            {
                                obj.Adress.Note = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "Other")
                            {
                                obj.Adress.Other = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "AddressOrLocation")
                            {
	                            switch (xnChild1.InnerText)
	                            {
                                    case "0":
	                                    obj.Adress.AddressOrLocation = "Описание местоположения объекта недвижимости";
                                        break;
                                    case "1":
	                                    obj.Adress.AddressOrLocation = "Присвоенный в установленном порядке адрес объекта недвижимости";
	                                    break;
	                            }
                            }
                            if (xnChild1.Name == "District")
                            {
                                obj.Adress.District = new xmlAdresLevel()
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
                            if (xnChild1.Name == "SovietVillage")
                            {
	                            obj.Adress.SovietVillage = new xmlAdresLevel()
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
                            if (xnChild1.Name == "PlanningElement")
                            {
	                            obj.Adress.PlanningElement = new xmlAdresLevel()
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
                        obj.DegreeReadiness = xnChild.InnerText.ParseToLongNullable();
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
                            if (xnChild1.Name == "SpecialType")
                            {
	                            obj.AssignationSpecialType = new xmlCodeName()
	                            {
		                            Code = xnChild1.InnerText,
		                            Name = dictSpecialTypeFlat.Records.GetRecByCode(xnChild1.InnerText).Value
	                            };
                            }
                            if (xnChild1.Name == "TotalAssets")
                            {
	                            obj.AssignationTotalAssets = xnChild1.InnerText?.Trim()?.ToLower() == "true";
                            }
                            if (xnChild1.Name == "AuxiliaryFlat")
                            {
	                            obj.AssignationAuxiliaryFlat = xnChild1.InnerText?.Trim()?.ToLower() == "true";
                            }
                        }
                        #endregion
                        break;
                    case "PositionInObject":
                        #region расположение помещения

                        if (typeobject == enTypeObject.toCarPlace)
                        {
	                        var level = GetXmlLevel(xnChild);
	                        obj.Levels.Add(level);
                        }
                        else
                        {
	                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
	                        {
		                        if (xnChild1.Name == "Levels")
		                        {
			                        foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
			                        {
				                        if (xnChild2.Name == "Level")
				                        {
					                        var level = GetXmlLevel(xnChild2);
					                        obj.Levels.Add(level);
				                        }
			                        }
		                        }
		                        if (xnChild1.Name == "Position")
		                        {
			                        obj.Position = new xmlPos();
			                        if (xnChild1.Attributes["NumberOnPlan"] != null)
				                        obj.Position.NumberOnPlan = xnChild1.Attributes["NumberOnPlan"].InnerText;
			                        if (xnChild1.Attributes["Description"] != null)
				                        obj.Position.Description = xnChild1.Attributes["Description"].InnerText;
		                        }
	                        }
                        }
                        #endregion
                        break;
                    case "ParentOKS":
                        #region Характеристики здания

                        obj.ParentOks = new xmlParentOks();
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
                            if (xnChild1.Name == "CadastralNumberOKS")
                            {
	                            obj.ParentOks.CadastralNumberOKS = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "ObjectType")
                            {
	                            obj.ParentOks.ObjectType = new xmlCodeName()
	                            {
		                            Code = xnChild1.InnerText,
		                            Name = dictRealty.Records.GetRecByCode(xnChild1.InnerText).Value
	                            };
                            }
                            if (xnChild1.Name == "AssignationBuilding")
                            {
	                            obj.ParentOks.AssignationBuilding = new xmlCodeName()
	                            {
		                            Code = xnChild1.InnerText,
		                            Name = dictAssignationBuild.Records.GetRecByCode(xnChild1.InnerText).Value
	                            };
                            }
                            if (xnChild1.Name == "AssignationName")
                            {
	                            obj.ParentOks.AssignationName = xnChild1.InnerText;
                            }
                            if (xnChild1.Name == "ElementsConstruct")
                            {
	                            foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
	                            {
		                            if (xnChild2.Name == "Material")
		                            {
			                            if (xnChild2.Attributes["Wall"] != null)
			                            {
				                            string wcode = xnChild2.Attributes["Wall"].InnerText;
				                            obj.ParentOks.Walls.Add(
					                            new xmlCodeName()
					                            {
						                            Code = wcode,
						                            Name = dictWall.Records.GetRecByCode(wcode).Value
					                            });
			                            }
		                            }
	                            }
                            }
                            if (xnChild1.Name == "ExploitationChar")
                            {
	                            obj.ParentOks.Years = new xmlYear();
	                            if (xnChild1.Attributes["YearBuilt"] != null) obj.ParentOks.Years.Year_Built = xnChild1.Attributes["YearBuilt"].InnerText;
	                            if (xnChild1.Attributes["YearUsed"] != null) obj.ParentOks.Years.Year_Used = xnChild1.Attributes["YearUsed"].InnerText;
                            }
                            if (xnChild1.Name == "Floors")
                            {
                                obj.ParentOks.Floors = new xmlFloors();
                                if (xnChild1.Attributes["Floors"] != null) obj.ParentOks.Floors.Floors = xnChild1.Attributes["Floors"].InnerText;
                                if (xnChild1.Attributes["UndergroundFloors"] != null) obj.ParentOks.Floors.Underground_Floors = xnChild1.Attributes["UndergroundFloors"].InnerText;
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
                    case "NaturalObjects":
                        #region Сведения о природных объектах
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
	                    {
		                    if (xnChild1.Name == "NaturalObject")
		                    {
			                    var naturalObject = new xmlNaturalObject();
			                    foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
								{
									if (xnChild2.Name == "Kind")
									{
										naturalObject.Kind = new xmlCodeName
										{
											Code = xnChild2.InnerText,
											Name = dictNaturalObjects.Records.GetRecByCode(xnChild2.InnerText).Value
										};
									}
									if (xnChild2.Name == "Forestry")
									{
										naturalObject.Forestry = xnChild2.InnerText;
									}
                                    if (xnChild2.Name == "ForestUse")
									{
										naturalObject.ForestUse = new xmlCodeName
										{
											Code = xnChild2.InnerText,
											Name = dictForestUse.Records.GetRecByCode(xnChild2.InnerText).Value
										};
									}
                                    if (xnChild2.Name == "QuarterNumbers")
                                    {
	                                    naturalObject.QuarterNumbers = xnChild2.InnerText;
                                    }
                                    if (xnChild2.Name == "TaxationSeparations")
                                    {
	                                    naturalObject.TaxationSeparations = xnChild2.InnerText;
                                    }
                                    if (xnChild2.Name == "ProtectiveForest")
									{
										naturalObject.ProtectiveForest = new xmlCodeName
										{
											Code = xnChild2.InnerText,
											Name = dictForestCategoryProtective.Records.GetRecByCode(xnChild2.InnerText).Value
										};
									}
                                    if (xnChild2.Name == "ForestEncumbrances")
                                    {
	                                    foreach (XmlNode xnChild3 in xnChild2.ChildNodes)
	                                    {
		                                    if (xnChild3.Name == "ForestEncumbrance")
		                                    {
			                                    var forestEncumbrance = new xmlCodeName
			                                    {
				                                    Code = xnChild3.InnerText,
				                                    Name = dictForestEncumbrances.Records.GetRecByCode(xnChild3.InnerText).Value
			                                    };
			                                    naturalObject.ForestEncumbrances.Add(forestEncumbrance);
		                                    }
                                        }
                                    }
                                    if (xnChild2.Name == "WaterObject")
                                    {
	                                    naturalObject.WaterObject = xnChild2.InnerText;
                                    }
                                    if (xnChild2.Name == "NameOther")
                                    {
	                                    naturalObject.NameOther = xnChild2.InnerText;
                                    }
                                    if (xnChild2.Name == "CharOther")
                                    {
	                                    naturalObject.CharOther = xnChild2.InnerText;
                                    }
                                }

								obj.NaturalObjects.Add(naturalObject);
                            }
	                    }
                        #endregion
                        break;
                    case "SubParcels":
                        #region Сведения о частях участка
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
                        {
	                        if (xnChild1.Name == "SubParcel")
	                        {
		                        var subParcel = new xmlSubParcel();
		                        if (xnChild1.Attributes["NumberRecord"] != null)
		                        {
			                        subParcel.NumberRecord = xnChild1.Attributes["NumberRecord"].InnerText;
		                        }
		                        if (xnChild1.Attributes["DateCreated"] != null)
		                        {
			                        subParcel.DateCreated = DateTime.TryParse(xnChild1.Attributes["DateCreated"].InnerText, out var date)
				                        ? date
				                        : (DateTime?)null;
		                        }
                                foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
		                        {
			                        if (xnChild2.Name == "Area")
			                        {
				                        subParcel.Area = double.TryParse(xnChild2.InnerText, out var valResult)
					                        ? valResult
					                        : (double?)null;
			                        }

			                        if (xnChild1.Name == "Inaccuracy")
			                        {
				                        subParcel.AreaInaccuracy = double.TryParse(xnChild2.InnerText, out var valResult)
					                        ? valResult
					                        : (double?)null;
                                    }
			                        if (xnChild2.Name == "Encumbrances")
			                        {
				                        subParcel.Encumbrances = GetEncumbrances<xmlEncumbranceZu>(xnChild2);
			                        }
		                        }

		                        obj.SubParcels.Add(subParcel);
	                        }
                        }
                        #endregion
                        break;
                    case "SubBuildings":
                    case "SubFlats":
                        #region Сведения о частях здания / помещения
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
	                    {
		                    if (xnChild1.Name == "SubBuilding" || xnChild1.Name == "SubFlat")
		                    {
			                    var subBuildingFlat = new xmlSubBuildingFlat();

			                    if (xnChild1.Attributes["NumberRecord"] != null)
			                    {
				                    subBuildingFlat.NumberRecord = xnChild1.Attributes["NumberRecord"].InnerText;
			                    }
			                    if (xnChild1.Attributes["DateCreated"] != null)
			                    {
				                    subBuildingFlat.DateCreated = DateTime.TryParse(xnChild1.Attributes["DateCreated"].InnerText, out var date)
					                    ? date
					                    : (DateTime?)null;
			                    }
                                foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
			                    {
				                    if (xnChild2.Name == "Area")
				                    {
					                    subBuildingFlat.Area = double.TryParse(xnChild2.InnerText, out var valResult)
						                    ? valResult
						                    : (double?)null;
				                    }
				                    if (xnChild2.Name == "Encumbrances")
                                    {
	                                    subBuildingFlat.EncumbrancesOks = GetEncumbrances<xmlEncumbranceOks>(xnChild2);
                                    }
			                    }

                                obj.SubBuildingFlats.Add(subBuildingFlat);
		                    }
	                    }
	                    #endregion
	                    break;
                    case "SubConstructions":
                        #region Сведения о частях сооружения
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
	                    {
		                    if (xnChild1.Name == "SubConstruction")
		                    {
			                    var subConstruction = new xmlSubConstruction();

			                    if (xnChild1.Attributes["NumberRecord"] != null)
			                    {
				                    subConstruction.NumberRecord = xnChild1.Attributes["NumberRecord"].InnerText;
			                    }
			                    if (xnChild1.Attributes["DateCreated"] != null)
			                    {
				                    subConstruction.DateCreated = DateTime.TryParse(xnChild1.Attributes["DateCreated"].InnerText, out var date)
					                    ? date
					                    : (DateTime?)null;
			                    }
                                foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
			                    {
				                    if (xnChild2.Name == "KeyParameter")
				                    {
					                    subConstruction.KeyParameter = new xmlCodeNameValue();
					                    if (xnChild2.Attributes["Type"] != null) {
						                    subConstruction.KeyParameter.Code = xnChild2.Attributes["Type"].InnerText;
						                    subConstruction.KeyParameter.Name = dictTypeParameter.Records.GetRecByCode(xnChild2.Attributes["Type"].InnerText).Value;
					                    }
					                    if (xnChild2.Attributes["Value"] != null) 
						                    subConstruction.KeyParameter.Value = xnChild2.Attributes["Value"].InnerText;
				                    }
				                    if (xnChild2.Name == "Encumbrances")
				                    {
					                    subConstruction.EncumbrancesOks = GetEncumbrances<xmlEncumbranceOks>(xnChild2);
				                    }
			                    }

			                    obj.SubConstructions.Add(subConstruction);
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
                                    if (xnChild4.Name == "FullPartly" && !string.IsNullOrEmpty(xnChild4.InnerText))
                                    {
                                        encum.FullPartly = xnChild4.InnerText?.Trim()?.ToLower() == "true";
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
                                    if (xnChild4.Name == "InspectionEnd")
                                    {
	                                    encum.InspectionEnd = DateTime.TryParse(xnChild4.InnerText, out var date)
		                                    ? date
		                                    : (DateTime?)null;
                                    }
                                    if (xnChild4.Name == "ResultsEvent")
                                    {
                                        foreach (XmlNode xnChild5 in xnChild4.ChildNodes)
                                        {
                                            if (xnChild5.Name == "AvailabilityViolations" && !string.IsNullOrEmpty(xnChild5.InnerText))
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
	                                                    encum.IdentifiedViolations.Area =
		                                                    double.TryParse(xnChild6.InnerText, out var areaResult)
			                                                    ? areaResult
			                                                    : (double?)null;
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
                    case "SurveyingProject":
                        #region Сведения о расположении земельного участка в границах территории, в отношении которой утвержден проект межевания территории
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
	                    {
		                    if (xnChild1.Name == "SurveyProjectNum")
		                    {
			                    obj.SurveyingProjectNum = xnChild1.InnerText;
		                    }
		                    if (xnChild1.Name == "DecisionRequisites")
		                    {
			                    obj.SurveyingProjectDecisionRequisites = GetDocument(xnChild1);
		                    }
                        }
	                    #endregion
	                    break;
                    case "HiredHouse":
                        #region Сведения о создании (эксплуатации) на земельном участке наемного дома
                        obj.HiredHouse = new xmlHiredHouse();
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
	                    {
		                    if (xnChild1.Name == "UseHiredHouse")
		                    {
			                    obj.HiredHouse.UseHiredHouse = xnChild1.InnerText;
		                    }
		                    if (xnChild1.Name == "MunicipalHouse")
		                    {
			                    foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
								{
									if (xnChild2.Name == "ActBuilding")
									{
										obj.HiredHouse.ActBuilding = true;
									}
									if (xnChild2.Name == "ActDevelopment")
									{
										obj.HiredHouse.ActDevelopment = true;
									}
									if (xnChild2.Name == "ContractBuilding")
									{
										foreach (XmlNode xnChild3 in xnChild2.ChildNodes)
										{
											if (xnChild3.Name == "ContractName")
											{
												obj.HiredHouse.ContractBuilding = xnChild3.InnerText;
											}
										}
									}
									if (xnChild2.Name == "ContractDevelopment")
									{
										foreach (XmlNode xnChild3 in xnChild2.ChildNodes)
										{
											if (xnChild3.Name == "ContractName")
											{
												obj.HiredHouse.ContractDevelopment = xnChild3.InnerText;
											}
										}
									}
                                }
		                    }
		                    if (xnChild1.Name == "OwnerHouse")
		                    {
			                    foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
			                    {
				                    if (xnChild2.Name == "OwnerDecision")
				                    {
					                    obj.HiredHouse.OwnerDecision = true;
				                    }
				                    if (xnChild2.Name == "ContractSupport")
				                    {
					                    foreach (XmlNode xnChild3 in xnChild2.ChildNodes)
					                    {
						                    if (xnChild3.Name == "ContractName")
						                    {
							                    obj.HiredHouse.ContractSupport = xnChild3.InnerText;
						                    }
					                    }
				                    }
			                    }
                            }
		                    if (xnChild1.Name == "DocHiredHouse")
		                    {
			                    obj.HiredHouse.DocHiredHouse = GetDocument(xnChild1);
		                    }
                        }
	                    #endregion
	                    break;
                    case "LimitedCirculation":
                        #region Сведения об ограничении оборотоспособности земельного участка в соответствии со статьей 11 Федерального закона от 1 мая 2016 г. № 119-ФЗ
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
	                    {
		                    if (xnChild1.Name == "RightNumber")
		                    {
			                    obj.LimitedCirculation = xnChild1.InnerText;
		                    }
	                    }
	                    #endregion
	                    break;
                    case "ObjectPermittedUses":
                        #region Вид (виды) разрешенного использования
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
	                    {
		                    if (xnChild1.Name == "ObjectPermittedUse" && !string.IsNullOrEmpty(xnChild1.InnerText))
		                    {
			                    obj.ObjectPermittedUses.Add(xnChild1.InnerText);
		                    }
	                    }
	                    #endregion
	                    break;
                    case "CulturalHeritage":
                        #region Сведения о включении объекта недвижимости в единый государственный реестр объектов культурного наследия или об отнесении объекта недвижимости к выявленным объектам культурного наследия

                        obj.CulturalHeritage = new xmlCulturalHeritage();
                        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
	                    {
		                    if (xnChild1.Name == "InclusionEGROKN" || xnChild1.Name == "AssignmentEGROKN")
		                    {
			                    foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
			                    {
				                    if (xnChild2.Name == "RegNum")
				                    {
					                    obj.CulturalHeritage.EgroknRegNum = xnChild2.InnerText;
				                    }
				                    if (xnChild2.Name == "ObjCultural")
				                    {
					                    obj.CulturalHeritage.EgroknObjCultural = new xmlCodeName()
					                    {
						                    Code = xnChild2.InnerText,
						                    Name = dictCultural.Records.GetRecByCode(xnChild2.InnerText).Value
					                    };
				                    }
				                    if (xnChild2.Name == "NameCultural")
				                    {
					                    obj.CulturalHeritage.EgroknNameCultural = xnChild2.InnerText;
				                    }
			                    }
		                    }
		                    if (xnChild1.Name == "RequirementsEnsure")
		                    {
			                    obj.CulturalHeritage.RequirementsEnsure = xnChild1.InnerText;
		                    }
		                    if (xnChild1.Name == "Document")
		                    {
			                    obj.CulturalHeritage.Document = GetDocument(xnChild1);
                            }
                        }
	                    #endregion
                        break;
                    default:
                        break;
                }
            }
                #endregion
	        }
            catch (Exception ex)
            {
	            Serilog.Log.Error(ex, "Ошибка при обработке объекта '{kn}' из xml-файла", kn);

	            var reportRow = _gbuReportService.GetCurrentRow();
	            _gbuReportService.AddValue(kn, BaseImporter.CadastralNumberColumnIndex, reportRow);
	            _gbuReportService.AddValue(ex.Message, BaseImporter.ErrorMessageColumnIndex, reportRow);
            }

            return obj;
        }

        private static xmlLevel GetXmlLevel(XmlNode xmlNode)
        {
	        var level = new xmlLevel();
	        if (xmlNode.Attributes["Number"] != null)
		        level.Number = xmlNode.Attributes["Number"].InnerText;
	        if (xmlNode.Attributes["Type"] != null)
	        {
		        level.Type = new xmlCodeName();
		        level.Type.Code = xmlNode.Attributes["Type"].InnerText;
		        level.Type.Name = dictStrorey.Records.GetValueByCode(xmlNode.Attributes["Type"].InnerText);
	        }

	        foreach (XmlNode xnChild3 in xmlNode.ChildNodes)
	        {
		        if (xnChild3.Name == "Position")
		        {
			        level.Position = new xmlPos();
			        if (xnChild3.Attributes["NumberOnPlan"] != null)
				        level.Position.NumberOnPlan = xnChild3.Attributes["NumberOnPlan"].InnerText;
			        if (xnChild3.Attributes["Description"] != null)
				        level.Position.Description = xnChild3.Attributes["Description"].InnerText;
		        }
	        }

	        return level;
        }

        private static List<T> GetEncumbrances<T>(XmlNode xmlNode) where T: xmlEncumbranceOks, new()
        {
	        var result = new List<T>();

            foreach (XmlNode xnChild1 in xmlNode.ChildNodes)
	        {
		        if (xnChild1.Name == "Encumbrance")
		        {
			        var encumbrance = new T();

			        foreach (XmlNode xnChild2 in xnChild1.ChildNodes)
			        {
				        if (xnChild2.Name == "Name")
				        {
					        encumbrance.Name = xnChild2.InnerText;
				        }

				        if (xnChild2.Name == "Type")
				        {
					        encumbrance.Type = new xmlCodeName()
					        {
						        Code = xnChild2.InnerText,
						        Name = dictEncumbrance.Records.GetValueByCode(xnChild2.InnerText)
					        };
				        }

				        if (xnChild2.Name == "AccountNumber" && encumbrance is xmlEncumbranceZu)
				        {
					        (encumbrance as xmlEncumbranceZu).AccountNumber = xnChild2.InnerText;
				        }
				        if (xnChild2.Name == "CadastralNumberRestriction" && encumbrance is xmlEncumbranceZu)
				        {
					        (encumbrance as xmlEncumbranceZu).CadastralNumberRestriction = xnChild2.InnerText;
				        }

                        if (xnChild2.Name == "Registration")
				        {
					        encumbrance.Registration = new xmlNumberDate();
					        foreach (XmlNode xnChild3 in xnChild2.ChildNodes)
					        {
						        if (xnChild3.Name == "RightNumber")
						        {
							        encumbrance.Registration.Number = xnChild3.InnerText;
						        }

						        if (xnChild3.Name == "RegistrationDate")
						        {
							        encumbrance.Registration.Date = DateTime.TryParse(xnChild3.InnerText, out var resultDateTime)
								        ? resultDateTime
                                        : (DateTime?) null;
						        }
					        }
				        }

				        if (xnChild2.Name == "Document")
				        {
					        encumbrance.Document = GetDocument(xnChild2);
				        }
			        }

			        result.Add(encumbrance);
		        }
	        }

            return result;
        }

        private static List<string> GetCadastralNumbers(XmlNode xnChild)
        {
	        var numbers = new List<string>();
	        foreach (XmlNode xnChild1 in xnChild.ChildNodes)
	        {
		        if (xnChild1.Name == "CadastralNumber")
		        {
			        numbers.Add(xnChild1.InnerText);
		        }
	        }

	        return numbers;
        }

        public static xmlObject GetData(ExcelRow row, string kn, enTypeObject typeobject, DateTime assessmentDate)
        {
	        var obj = new xmlObject(typeobject, kn, null, assessmentDate);

            if (typeobject==enTypeObject.toParcel)
            {
                obj.TypeRealty = "Земельный участок";
                obj.Area = double.TryParse(row.Cells[2].Value.ToString(), out var valResult)
	                ? valResult
	                : (double?)null;
                obj.NameParcel = new xmlCodeName()
                {
                    Code = string.Empty,
                    Name = row.Cells[3].Value.ParseToString()
                };
                obj.CadastralNumberBlock = row.Cells[8].Value.ParseToString();
                obj.Adress = new xmlAdress();
                obj.Adress.KLADR = row.Cells[6].Value.ParseToString();
                obj.Adress.Note = row.Cells[7].Value.ParseToString();
                obj.Adress.Other = row.Cells[5].Value.ParseToString();
                obj.Category = new xmlCodeName()
                {
                    Code = string.Empty,
                    Name = row.Cells[9].Value.ParseToString()
                };
                obj.Utilization = new xmlUtilization();
                obj.Utilization.ByDoc= row.Cells[4].Value.ParseToString();
            }

            if (typeobject == enTypeObject.toBuilding)
            {
                obj.TypeRealty = "Здание";
                obj.Area = double.TryParse(row.Cells[2].Value.ToString(), out var valResult)
	                ? valResult
	                : (double?)null;
                obj.CadastralNumberBlock = row.Cells[8].Value.ParseToString();
                obj.Adress = new xmlAdress();
                obj.Adress.KLADR = row.Cells[6].Value.ParseToString();
                obj.Adress.Note = row.Cells[7].Value.ParseToString();
                obj.Adress.Other = row.Cells[5].Value.ParseToString();

                string zusnum = row.Cells[9].Value.ParseToString();
                if (zusnum != string.Empty)
                {
                    string[] zus = zusnum.Split(';');
                    foreach (string zu in zus)
                        obj.ParentCadastralNumbers.Add(zu.Trim());
                }

                obj.Years = new xmlYear();
                obj.Years.Year_Built = row.Cells[10].Value.ParseToString();
                obj.Years.Year_Used = row.Cells[11].Value.ParseToString();

                obj.Floors = new xmlFloors();
                obj.Floors.Floors = row.Cells[12].Value.ParseToString();
                obj.Floors.Underground_Floors = row.Cells[13].Value.ParseToString();

                obj.AssignationBuilding = new xmlCodeName()
                {
                    Code = string.Empty,
                    Name = row.Cells[4].Value.ParseToString()
                };

                obj.NameObject = row.Cells[3].Value.ParseToString();

                string wcode = row.Cells[14].Value.ParseToString();
                if (wcode != string.Empty)
                {
                    obj.Walls.Add(
                        new xmlCodeName()
                        {
                            Code = string.Empty,
                            Name = wcode
                        });
                }
            }

            if (typeobject == enTypeObject.toConstruction)
            {
                obj.TypeRealty = "Сооружение";
                obj.CadastralNumberBlock = row.Cells[7].Value.ParseToString();
                obj.Adress = new xmlAdress();
                obj.Adress.KLADR = row.Cells[5].Value.ParseToString();
                obj.Adress.Note = row.Cells[6].Value.ParseToString();
                obj.Adress.Other = row.Cells[4].Value.ParseToString();

                string zusnum = row.Cells[8].Value.ParseToString();
                if (zusnum != string.Empty)
                {
                    string[] zus = zusnum.Split(';');
                    foreach (string zu in zus)
                        obj.ParentCadastralNumbers.Add(zu.Trim());
                }

                obj.Years = new xmlYear();
                obj.Years.Year_Built = row.Cells[9].Value.ParseToString();
                obj.Years.Year_Used = row.Cells[10].Value.ParseToString();

                obj.Floors = new xmlFloors();
                obj.Floors.Floors = row.Cells[11].Value.ParseToString();
                obj.Floors.Underground_Floors = row.Cells[12].Value.ParseToString();

                obj.AssignationName = row.Cells[3].Value.ParseToString();
                obj.NameObject = row.Cells[2].Value.ParseToString();


                string wcode = row.Cells[13].Value.ParseToString();
                if (wcode != string.Empty)
                {
                    xmlCodeNameValue tmp = new xmlCodeNameValue();
                    tmp.Code = string.Empty; 
                    tmp.Name = "Характеристика"; 
                    tmp.Value = wcode;
                    obj.KeyParameters.Add(tmp);
                }
            }

            if (typeobject == enTypeObject.toUncomplited)
            {
                obj.TypeRealty = "ОНС";
                obj.CadastralNumberBlock = row.Cells[6].Value.ParseToString();
                obj.Adress = new xmlAdress();
                obj.Adress.KLADR = row.Cells[4].Value.ParseToString();
                obj.Adress.Note = row.Cells[5].Value.ParseToString();
                obj.Adress.Other = row.Cells[3].Value.ParseToString();

                string zusnum = row.Cells[7].Value.ParseToString();
                if (zusnum != string.Empty)
                {
                    string[] zus = zusnum.Split(';');
                    foreach (string zu in zus)
                        obj.ParentCadastralNumbers.Add(zu.Trim());
                }

                obj.AssignationName = row.Cells[2].Value.ParseToString();


                string wcode = row.Cells[8].Value.ParseToString();
                if (wcode != string.Empty)
                {
                    xmlCodeNameValue tmp = new xmlCodeNameValue();
                    tmp.Code = string.Empty;
                    tmp.Name = "Характеристика";
                    tmp.Value = wcode;
                    obj.KeyParameters.Add(tmp);
                }

                obj.DegreeReadiness = row.Cells[9].Value.ParseToLongNullable();
            }

            if (typeobject == enTypeObject.toFlat)
            {
                obj.TypeRealty = "Помещение";
                obj.Area = double.TryParse(row.Cells[2].Value.ToString(), out var valResult)
	                ? valResult
	                : (double?)null;

                obj.CadastralNumberBlock = row.Cells[9].Value.ParseToString();
                obj.Adress = new xmlAdress();
                obj.Adress.KLADR = row.Cells[6].Value.ParseToString();
                obj.Adress.Note = row.Cells[8].Value.ParseToString();
                obj.Adress.Other = row.Cells[5].Value.ParseToString();

                obj.AssignationFlatCode = new xmlCodeName()
                {
                    Code = string.Empty,
                    Name = row.Cells[4].Value.ParseToString()
                };

                obj.NameObject = row.Cells[3].Value.ParseToString();
                obj.CadastralNumberOKS= row.Cells[7].Value.ParseToString();

                obj.Years = new xmlYear();
                obj.Years.Year_Built = row.Cells[10].Value.ParseToString();
                obj.Years.Year_Used = row.Cells[11].Value.ParseToString();

                obj.Levels = new List<xmlLevel>
				{
					new() {Number = row.Cells[12].Value.ParseToString(), Type = new xmlCodeName {Name = "Этаж"}}
                };

                string wcode = row.Cells[13].Value.ParseToString();
                if (wcode != string.Empty)
                {
                    obj.Walls.Add(
                        new xmlCodeName()
                        {
                            Code = string.Empty,
                            Name = wcode
                        });
                }
            }

            return obj;
        }


        #region Импорт обращений

        //public xmlObjectList GetExcelObjectForPetition(ExcelFile excelFile, DateTime assessmentDate)
        //{
        //    xmlObjectList objs = new xmlObjectList();

        //    var mainWorkSheet = excelFile.Worksheets[0];

        //    CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        //    ParallelOptions options = new ParallelOptions
        //    {
        //        CancellationToken = cancelTokenSource.Token,
        //        MaxDegreeOfParallelism = 1
        //    };
        //    var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
        //    Parallel.ForEach(mainWorkSheet.Rows, options, row =>
        //    {
        //     string cadastralNumber = null;
        //        try
        //        {
        //         if (row.Index != 0 && row.Index <= lastUsedRowIndex) //все, кроме заголовков и пустых строк в конце страницы
        //            {
        //             cadastralNumber = row.Cells[0].Value.ParseToString();
        //                string typeobject = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString().ToUpper();
        //                if (typeobject == "ЗЕМЕЛЬНЫЙ УЧАСТОК") 
        //                 objs.Add(GetData(mainWorkSheet.Rows[row.Index], cadastralNumber, enTypeObject.toParcel, assessmentDate));
        //                else if (typeobject == "ЗДАНИЕ") 
        //                 objs.Add(GetData(mainWorkSheet.Rows[row.Index], cadastralNumber, enTypeObject.toBuilding, assessmentDate));
        //                else if (typeobject == "СООРУЖЕНИЕ")
        //                 objs.Add(GetData(mainWorkSheet.Rows[row.Index], cadastralNumber, enTypeObject.toConstruction, assessmentDate));
        //                else if (typeobject == "ОНС") 
        //                 objs.Add(GetData(mainWorkSheet.Rows[row.Index], cadastralNumber, enTypeObject.toUncomplited, assessmentDate));
        //                else if (typeobject == "ПОМЕЩЕНИЕ") 
        //                 objs.Add(GetData(mainWorkSheet.Rows[row.Index], cadastralNumber, enTypeObject.toFlat, assessmentDate));
        //                else
        //                {
        //                 throw new Exception($"Неизвестный тип объекта '{typeobject}'");
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //         lock (_locker)
        //         {
        //          LogErrorInReport(row.Index, cadastralNumber, ex);
        //            }
        //        }
        //    });

        //    return objs;
        //}

        #endregion

        #region Excel Mapping

        public xmlObjectList GetExcelObject(ExcelFile excelFile, List<ColumnToAttributeMapping> columnsMapping,
	        GknAllAttributes importedAttributes)
        {
			var objectTypeMapping = columnsMapping.First(x => x.AttributeId == RequiredFieldsForExcelMapping.ObjectTypeAttributeId);
			var cadastralNumberMapping = columnsMapping.First(x => x.AttributeId == RequiredFieldsForExcelMapping.CadastralNumberAttributeId);
			var squareMapping = columnsMapping.First(x => x.AttributeId == RequiredFieldsForExcelMapping.SquareAttributeId);
			var assessmentDateMapping = columnsMapping.First(x => x.AttributeId == RequiredFieldsForExcelMapping.AssessmentDateAttributeId);

			var objects = new xmlObjectList();
	        var mainWorkSheet = excelFile.Worksheets[0];
	        var objectTypeDescriptions = new ObjectTypeEnumDescriptions();
            var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
	        mainWorkSheet.Rows.ForEach(row =>
	        {
		        string cadastralNumber = null;
                var reportInfo = new ReportInfo();
	            try
		        {
			        if (row.Index == 0 || row.Index > lastUsedRowIndex) 
				        return;

			        TotalObjectCounter++;
                    cadastralNumber = GetStringValue(row, cadastralNumberMapping.ColumnIndex);
                    var square = GknAllAttributes.CastToDouble(RequiredFieldsForExcelMapping.SquareAttributeId,
	                    row.Cells[squareMapping.ColumnIndex].Value);
			        var assessmentDate = GknAllAttributes.CastToDateTime(
				        RequiredFieldsForExcelMapping.AssessmentDateAttributeId,
				        row.Cells[assessmentDateMapping.ColumnIndex].Value);
                    var typeFromFile = GetStringValue(row, objectTypeMapping.ColumnIndex);
                    var typeEnum = GetObjectType(objectTypeDescriptions, typeFromFile);

                    reportInfo.CadastralNumber = cadastralNumber;
                    var obj = MapExcelRowToObject(row, reportInfo, cadastralNumber, square, assessmentDate, typeEnum,
	                    columnsMapping, importedAttributes);
			        objects.Add(obj);
                }
                catch (Exception ex)
                {
	                Serilog.Log.Error(ex, "Ошибка при обработке {rowIndex} строки excel-файла. Объект '{CadastralNumber}'.",
		                row.Index + 1, cadastralNumber);
                    reportInfo.AddError(ex.Message, row.Index);
		        }

                if (reportInfo.MustWriteToReport)
                {
	                LogInfoToReport(reportInfo);
                }
	        });

	        return objects;
        }


        #region Support Methods

        private enTypeObject GetObjectType(ObjectTypeEnumDescriptions descriptions, string typeFromFile)
        {
	        if (string.IsNullOrWhiteSpace(typeFromFile))
		        throw new Exception("Не указан тип объекта");

	        if (string.Equals(descriptions.Stead, typeFromFile, StringComparison.InvariantCultureIgnoreCase))
	        {
		        return enTypeObject.toParcel;
	        }
	        if (string.Equals(descriptions.Building, typeFromFile, StringComparison.InvariantCultureIgnoreCase))
	        {
		        return enTypeObject.toBuilding;
	        }
	        if (string.Equals(descriptions.Construction, typeFromFile, StringComparison.InvariantCultureIgnoreCase))
	        {
		        return enTypeObject.toConstruction;
	        }
	        if (string.Equals(descriptions.Placement, typeFromFile, StringComparison.InvariantCultureIgnoreCase))
	        {
		        return enTypeObject.toFlat;
	        }
	        if (string.Equals(descriptions.UncompletedBuilding, typeFromFile, StringComparison.InvariantCultureIgnoreCase))
	        {
		        return enTypeObject.toUncomplited;
	        }
	        if (string.Equals(descriptions.Parking, typeFromFile, StringComparison.InvariantCultureIgnoreCase))
	        {
		        return enTypeObject.toCarPlace;
	        }

            throw new Exception($"Указан неизвестный тип объекта '{typeFromFile}'");
        }

        private xmlObject MapExcelRowToObject(ExcelRow row, ReportInfo reportInfo, string cadastralNumber,
	        double? square, DateTime? assessmentDate, enTypeObject objectType, 
	        List<ColumnToAttributeMapping> columnsMapping, GknAllAttributes importedAttributes)
        {
	        ValidateExcelObjectRequiredColumns(cadastralNumber, square, assessmentDate);

            var obj = new xmlObject(objectType, cadastralNumber, null, assessmentDate.Value);
            obj.Area = square;

            List<ImportedAttributeGkn> attributes;
            switch (objectType)
            {
	            case enTypeObject.toBuilding:
		            attributes = importedAttributes.Building;
		            break;
	            case enTypeObject.toConstruction:
		            attributes = importedAttributes.Construction;
		            break;
	            case enTypeObject.toFlat:
		            attributes = importedAttributes.Flat;
		            break;
	            case enTypeObject.toCarPlace:
		            attributes = importedAttributes.CarPlace;
		            break;
	            case enTypeObject.toUncomplited:
		            attributes = importedAttributes.Uncompleted;
		            break;
	            case enTypeObject.toParcel:
		            attributes = importedAttributes.Parcel;
		            break;
	            default:
		            throw new ArgumentOutOfRangeException($"Неизвестный тип объекта '{objectType}'");
            }

            columnsMapping.ForEach(map =>
            {
	            var attributeInfo = attributes.FirstOrDefault(x => x.AttributeId == map.AttributeId);
	            //если атрибут не соответствует типу ОН (например, пытаемся записать Материал стен в ЗУ
	            if (attributeInfo == null)
	            {
		            var attributeData = RegisterCache.GetAttributeData(map.AttributeId);
		            if (attributeData.RegisterId != OMUnit.GetRegisterId())
		            {
			            var message = $"{attributeData.Name} ({objectType.GetEnumDescription()})";
                        reportInfo.AddNotProcessedAttribute(message, row.Index, map.ColumnIndex);
		            }
	            }
	            var valueFromExcel = row.Cells[map.ColumnIndex].Value;
	            try
	            {
		            attributeInfo?.SetValue.Invoke(obj, valueFromExcel);
	            }
	            catch (CastingToAttributeTypeException e)
	            {
		            Serilog.Log.Error(e,
			            "Ошибка преобразования типов при обработке {RowIndex} строки excel-файла. Объект '{CadastralNumber}'.",
			            row.Index + 1, cadastralNumber);

                    reportInfo.AddTypeConvertingError(e.Message, row.Index, map.ColumnIndex);
                }
            });

            return obj;
        }

        private void ValidateExcelObjectRequiredColumns(string cadastralNumber, double? square,
	        DateTime? assessmentDate)
        {
	        var messages = new List<string>();

	        if (string.IsNullOrWhiteSpace(cadastralNumber))
		        messages.Add("Кадастровый номер");

	        if (square == null)
		        messages.Add("Площадь");

	        if (assessmentDate == null)
		        messages.Add("Дата оценки");

	        if (messages.Count != 0)
		        throw new Exception($"Пустые значения для обязательных полей: {string.Join(',', messages)}");
        }

        private string GetStringValue(ExcelRow row, int columnIndex)
        {
	        return row.Cells[columnIndex].Value.ParseToStringNullable();
        }

        /// <summary>
        /// Класс с описаниями типов объекта (вынесен отдельно, чтобы не использовать рефлексию в цикле)
        /// </summary>
        private class ObjectTypeEnumDescriptions
        {
	        public readonly string Stead;
	        public readonly string Building;
	        public readonly string Construction;
	        public readonly string Placement;
	        public readonly string UncompletedBuilding;
	        public readonly string Parking;

	        public ObjectTypeEnumDescriptions()
	        {
		        Stead = PropertyTypes.Stead.GetEnumDescription();
		        Building = PropertyTypes.Building.GetEnumDescription();
		        Construction = PropertyTypes.Construction.GetEnumDescription();
		        Placement = PropertyTypes.Pllacement.GetEnumDescription();
		        UncompletedBuilding = PropertyTypes.UncompletedBuilding.GetEnumDescription();
		        Parking = PropertyTypes.Parking.GetEnumDescription();
	        }
        }

        #endregion

        #endregion

        private void LogInfoToReport(ReportInfo reportInfo)
        {
	        var reportRow = _gbuReportService.GetCurrentRow();

            _gbuReportService.AddValue(reportInfo.CadastralNumber, BaseImporter.CadastralNumberColumnIndex, reportRow);
	        _gbuReportService.AddValue(reportInfo.NotProcessedAttributeNames, BaseImporter.NotProcessedAttributesColumnIndex, reportRow);
	        _gbuReportService.AddValue(reportInfo.TypeConvertingError, BaseImporter.TypeConvertingErrorColumnIndex, reportRow);
	        _gbuReportService.AddValue(reportInfo.Error, BaseImporter.ErrorMessageColumnIndex, reportRow);
        }
    }
}
