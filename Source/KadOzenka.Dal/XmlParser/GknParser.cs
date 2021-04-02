using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using ObjectModel.Commission;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using KadOzenka.Dal.DataExport;

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
        public static xmlObject GetData(ExcelRow row, enTypeObject typeobject)
        {
            string kn = row.Cells[0].Value.ParseToString();
            DateTime dc = DateTime.MinValue;
            xmlObject obj = new xmlObject(typeobject, kn, dc);

            if (typeobject==enTypeObject.toParcel)
            {
                obj.TypeRealty = "Земельный участок";
                obj.Area = row.Cells[2].Value.ParseToString();
                obj.NameParcel = new xmlCodeName()
                {
                    Code = string.Empty,
                    Name = row.Cells[3].Value.ParseToString()
                };
                obj.CadastralNumberBlock = row.Cells[8].Value.ParseToString();
                obj.Adress = new xmlAdress();
                obj.Adress.KLADR = row.Cells[6].Value.ParseToString();
                obj.Adress.Place = row.Cells[7].Value.ParseToString();
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
                obj.Area = row.Cells[2].Value.ParseToString();
                obj.CadastralNumberBlock = row.Cells[8].Value.ParseToString();
                obj.Adress = new xmlAdress();
                obj.Adress.KLADR = row.Cells[6].Value.ParseToString();
                obj.Adress.Place = row.Cells[7].Value.ParseToString();
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
                obj.Adress.Place = row.Cells[6].Value.ParseToString();
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
                obj.Adress.Place = row.Cells[5].Value.ParseToString();
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

                obj.DegreeReadiness = row.Cells[9].Value.ParseToString();
            }

            if (typeobject == enTypeObject.toFlat)
            {
                obj.TypeRealty = "Помещение";
                obj.Area = row.Cells[2].Value.ParseToString();

                obj.CadastralNumberBlock = row.Cells[9].Value.ParseToString();
                obj.Adress = new xmlAdress();
                obj.Adress.KLADR = row.Cells[6].Value.ParseToString();
                obj.Adress.Place = row.Cells[8].Value.ParseToString();
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


                xmlPosition tmp = new xmlPosition();
                tmp.Position.Value = row.Cells[12].Value.ParseToString();
                tmp.Position.Name = "Этаж";
                obj.PositionsInObject.Add(tmp);


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


        public static xmlObjectList GetExcelObject(ExcelFile excelFile)
        {
            xmlObjectList objs = new xmlObjectList();

            var mainWorkSheet = excelFile.Worksheets[0];

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 1
            };
            var lastUsedRowIndex = DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
            Parallel.ForEach(mainWorkSheet.Rows, options, row =>
            {
                try
                {
	                if (row.Index != 0 && row.Index <= lastUsedRowIndex) //все, кроме заголовков и пустых строк в конце страницы
                    {
                        string typeobject = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString().ToUpper();
                        if (typeobject == "ЗЕМЕЛЬНЫЙ УЧАСТОК") objs.Add(GetData(mainWorkSheet.Rows[row.Index], enTypeObject.toParcel));
                        if (typeobject == "ЗДАНИЕ") objs.Add(GetData(mainWorkSheet.Rows[row.Index], enTypeObject.toBuilding));
                        if (typeobject == "СООРУЖЕНИЕ") objs.Add(GetData(mainWorkSheet.Rows[row.Index], enTypeObject.toConstruction));
                        if (typeobject == "ОНС") objs.Add(GetData(mainWorkSheet.Rows[row.Index], enTypeObject.toUncomplited));
                        if (typeobject == "ПОМЕЩЕНИЕ") objs.Add(GetData(mainWorkSheet.Rows[row.Index], enTypeObject.toFlat));
                    }
                }
                catch (Exception ex)
                {
                }
            });

            return objs;
        }


    }
}
