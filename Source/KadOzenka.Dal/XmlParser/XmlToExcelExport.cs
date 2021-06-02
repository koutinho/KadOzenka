using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.ConfigParam;
using GemBox.Spreadsheet;
using JetBrains.Annotations;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;

namespace KadOzenka.Dal.XmlParser
{
    public class XmlToExcelExport
    {
        private const int SINGLE_FILE_OBJECTS_LIMIT = 400_000;
        private readonly xmlObjectList _allObjects;
        private readonly int[] _config;

        private readonly Dictionary<int, string> _listNames = new Dictionary<int, string>
        {
            {1, "Parcel"},
            {2, "Build"},
            {3, "Construction"},
            {4, "Uncomplited"},
            {5, "Flat"},
            {6, "CarPlace"}
        };

        private Dictionary<string, ExcelFile> _resultExcelFiles;

        public XmlToExcelExport(xmlObjectList objectList, int[] config)
        {
            _config = config;
            _allObjects = objectList;
        }

        private int AllObjectsCount()
        {
            return _allObjects.Constructions.Count
                   + _allObjects.Buildings.Count
                   + _allObjects.Flats.Count
                   + _allObjects.CarPlaces.Count
                   + _allObjects.Parcels.Count
                   + _allObjects.Uncompliteds.Count;
        }

        public Dictionary<string, ExcelFile> GetResult()
        {
            Export();
            return _resultExcelFiles;
        }

        //TODO: необходима доработка выгрузки после талона KOMO-5
        private void Export()
        {
            _resultExcelFiles = new Dictionary<string, ExcelFile>();

            // Если объектов мало - формируем единый xlsx-файл
            if (AllObjectsCount() < SINGLE_FILE_OBJECTS_LIMIT)
            {
                var excelFile = LoadTemplate();
                AddParcels(excelFile.Worksheets[_listNames[1]], _allObjects.Parcels);
                AddBuildings(excelFile.Worksheets[_listNames[2]], _allObjects.Buildings);
                AddConstructions(excelFile.Worksheets[_listNames[3]], _allObjects.Constructions);
                AddIncompleteBuildings(excelFile.Worksheets[_listNames[4]], _allObjects.Uncompliteds);
                AddFlats(excelFile.Worksheets[_listNames[5]], _allObjects.Flats);
                AddCarPlaces(excelFile.Worksheets[_listNames[6]], _allObjects.CarPlaces);
                TrimEmptyWorksheets(excelFile);
                _resultExcelFiles.Add("allObjects", excelFile);
            }
            // Если много - разделяем по типу объекта и количеству
            else
            {
                AddChunked(_allObjects.Parcels);
                AddChunked(_allObjects.Buildings);
                AddChunked(_allObjects.Constructions);
                AddChunked(_allObjects.Uncompliteds);
                AddChunked(_allObjects.Flats);
                AddChunked(_allObjects.CarPlaces);
            }
        }

        private void AddChunked<T>(List<T> list)
        {
            var chunked = SplitList(list);
            var counter = 0;
            foreach (var chunk in chunked)
            {
                var excelFile = LoadTemplate();
                switch (chunk)
                {
                    case List<xmlObjectParcel> castedList:
                        AddParcels(excelFile.Worksheets[_listNames[1]], castedList);
                        LeaveOneWorksheet(excelFile, _listNames[1]);
                        _resultExcelFiles.Add($"Земельные участки {counter:00}", excelFile);
                        break;
                    case List<xmlObjectBuild> castedList:
                        AddBuildings(excelFile.Worksheets[_listNames[2]], castedList);
                        LeaveOneWorksheet(excelFile, _listNames[2]);
                        _resultExcelFiles.Add($"Здания {counter:00}", excelFile);
                        break;
                    case List<xmlObjectConstruction> castedList:
                        AddConstructions(excelFile.Worksheets[_listNames[3]], castedList);
                        LeaveOneWorksheet(excelFile, _listNames[3]);
                        _resultExcelFiles.Add($"Сооружения {counter:00}", excelFile);
                        break;
                    case List<xmlObjectUncomplited> castedList:
                        AddIncompleteBuildings(excelFile.Worksheets[_listNames[4]], castedList);
                        LeaveOneWorksheet(excelFile, _listNames[4]);
                        _resultExcelFiles.Add($"Объекты незавершенного строительства {counter:00}", excelFile);
                        break;
                    case List<xmlObjectFlat> castedList:
                        AddFlats(excelFile.Worksheets[_listNames[5]], castedList);
                        LeaveOneWorksheet(excelFile, _listNames[5]);
                        _resultExcelFiles.Add($"Комнаты {counter:00}", excelFile);
                        break;
                    case List<xmlObjectCarPlace> castedList:
                        AddCarPlaces(excelFile.Worksheets[_listNames[6]], castedList);
                        LeaveOneWorksheet(excelFile, _listNames[6]);
                        _resultExcelFiles.Add($"Машино-места {counter:00}", excelFile);
                        break;
                }

                counter++;
            }
        }

        private static IEnumerable<List<T>> SplitList<T>(List<T> list)
        {
            for (var i = 0; i < list.Count; i += SINGLE_FILE_OBJECTS_LIMIT)
                yield return list.GetRange(i, Math.Min(SINGLE_FILE_OBJECTS_LIMIT, list.Count - i));
        }

        private ExcelFile LoadTemplate()
        {
            var fileStream =
                Configuration.GetFileStream("GknXmlToExcel", ".xls", "ExcelTemplates");

            var excelFile = ExcelFile.Load(fileStream, LoadOptions.XlsDefault);
            return excelFile;
        }

        private bool ReadConfigValue(int value)
        {
            return _config.Contains(value);
        }

        private void AddParcels(ExcelWorksheet sheet, List<xmlObjectParcel> parcels)
        {
            var valOffset = 100;
            sheet.Cells.Style.WrapText = true;
            var curRow = 5;
            foreach (var item in parcels)
            {
                AddEntry(sheet, item.CadastralNumber, true, 1, curRow);
                AddEntry(sheet, item.TypeObject.ToString(), ReadConfigValue(valOffset + 2), 2, curRow);
                AddEntry(sheet, item.TypeRealty, ReadConfigValue(valOffset + 3), 3, curRow);
                AddEntry(sheet, item.DateCreate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 4), 4, curRow);
                AddEntry(sheet, item.CadastralNumberBlock, ReadConfigValue(valOffset + 5), 5, curRow);

                var posCadNum = 0;
                foreach (var innerCadNum in item.InnerCadastralNumbers)
                {
                    AddEntry(sheet, innerCadNum, ReadConfigValue(valOffset + 6), 6, curRow + posCadNum);
                    posCadNum++;
                }

                AddEntry(sheet, item.CadastralCost?.Value?.ToString(), ReadConfigValue(valOffset + 7), 7, curRow);
                AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 8), 8, curRow);
                AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 9),
                    9, curRow);
                AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 10),
                    10,
                    curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 11), 11, curRow);
                AddEntry(sheet, item.CadastralCost?.DocNumber, ReadConfigValue(valOffset + 12), 12, curRow);
                AddEntry(sheet, item.CadastralCost?.DocName, ReadConfigValue(valOffset + 13), 13, curRow);
                AddEntry(sheet, item.CadastralCost?.DateApproval?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 14), 14, curRow);
                AddEntry(sheet, item.CadastralCost?.RevisalStatementDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 15), 15, curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationLastDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 16), 16, curRow);


                AddEntry(sheet, item.Area?.ToString(), ReadConfigValue(valOffset + 16), 16, curRow);


                AddEntry(sheet, item.Adress.FIAS, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.OKATO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.OKTMO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.RussianFederation, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 19), 19, curRow);
                AddEntry(sheet, item.Adress.Note, ReadConfigValue(valOffset + 20), 20, curRow);
                AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 21), 21, curRow);
                AddEntry(sheet, item.Adress.AddressOrLocation, ReadConfigValue(valOffset + 21), 21, curRow);


                AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
                AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
                AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
                AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.SovietVillage?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.PlanningElement?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.Street?.Value, ReadConfigValue(valOffset + 26), 26, curRow);
                AddEntry(sheet, item.Adress.Level1?.Value, ReadConfigValue(valOffset + 27), 27, curRow);
                AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 28), 28, curRow);
                AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 29), 29, curRow);
                AddEntry(sheet, item.Adress.Apartment?.Value, ReadConfigValue(valOffset + 30), 30, curRow);
                AddEntry(sheet, item.Adress.InBounds, ReadConfigValue(valOffset + 30), 30, curRow);
                AddEntry(sheet, item.Adress.Placed, ReadConfigValue(valOffset + 30), 30, curRow);
                AddEntry(sheet, item.Adress.Elaboration?.ReferenceMark, ReadConfigValue(valOffset + 30), 30, curRow);
                AddEntry(sheet, item.Adress.Elaboration?.Distance, ReadConfigValue(valOffset + 30), 30, curRow);
                AddEntry(sheet, item.Adress.Elaboration?.Direction, ReadConfigValue(valOffset + 30), 30, curRow);


                AddEntry(sheet, item.Name.Code, ReadConfigValue(valOffset + 31), 31, curRow);
                AddEntry(sheet, item.Name.Name, ReadConfigValue(valOffset + 32), 32, curRow);

                AddEntry(sheet, item.Category.Code, ReadConfigValue(valOffset + 33), 33, curRow);
                AddEntry(sheet, item.Category.Name, ReadConfigValue(valOffset + 34), 34, curRow);

                AddEntry(sheet, item.Utilization?.Utilization.Code, ReadConfigValue(valOffset + 35), 35, curRow);
                AddEntry(sheet, item.Utilization?.Utilization.Name, ReadConfigValue(valOffset + 36), 36, curRow);
                AddEntry(sheet, item.Utilization?.LandUse.Code, ReadConfigValue(valOffset + 37), 37, curRow);
                AddEntry(sheet, item.Utilization?.LandUse.Name, ReadConfigValue(valOffset + 38), 38, curRow);
                AddEntry(sheet, item.Utilization?.ByDoc, ReadConfigValue(valOffset + 39), 39, curRow);
                AddEntry(sheet, item.Utilization?.PermittedUseText, ReadConfigValue(valOffset + 40), 40, curRow);

                var posEnc = 0;
                //TODO: входит в SubParcels
                //foreach (var encumbrance in item.Encumbrances)
                //{
                //	AddEntry(sheet, encumbrance.Name, ReadConfigValue(valOffset + 41), 41, curRow + posEnc);

                //	AddEntry(sheet, encumbrance.Type?.Code, ReadConfigValue(valOffset + 42), 42, curRow + posEnc);
                //	AddEntry(sheet, encumbrance.Type?.Name, ReadConfigValue(valOffset + 43), 43, curRow + posEnc);

                //	AddEntry(sheet, encumbrance.AccountNumber, ReadConfigValue(valOffset + 44), 44, curRow + posEnc);
                //	AddEntry(sheet, encumbrance.CadastralNumberRestriction, ReadConfigValue(valOffset + 45), 45,
                //		curRow + posEnc);

                //	AddEntry(sheet, encumbrance.Area.ToString(), ReadConfigValue(valOffset + 46), 46, curRow + posEnc);

                //	AddEntry(sheet, encumbrance.Registration?.Number, ReadConfigValue(valOffset + 47), 47,
                //		curRow + posEnc);
                //	AddEntry(sheet, encumbrance.Registration?.Date.ToString("dd/MM/yyyy"),
                //		ReadConfigValue(valOffset + 48), 48, curRow + posEnc);

                //	AddEntry(sheet, encumbrance.Document?.CodeDocument.Code, ReadConfigValue(valOffset + 49), 49,
                //		curRow + posEnc);
                //	AddEntry(sheet, encumbrance.Document?.CodeDocument.Name, ReadConfigValue(valOffset + 50), 50,
                //		curRow + posEnc);
                //	AddEntry(sheet, encumbrance.Document?.Name, ReadConfigValue(valOffset + 51), 51, curRow + posEnc);
                //	AddEntry(sheet, encumbrance.Document?.Series, ReadConfigValue(valOffset + 52), 52, curRow + posEnc);
                //	AddEntry(sheet, encumbrance.Document?.Number, ReadConfigValue(valOffset + 53), 53, curRow + posEnc);
                //	AddEntry(sheet, encumbrance.Document?.Date.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 54),
                //		54, curRow + posEnc);
                //	AddEntry(sheet, encumbrance.Document?.IssueOrgan, ReadConfigValue(valOffset + 55), 55,
                //		curRow + posEnc);
                //	AddEntry(sheet, encumbrance.Document?.Desc, ReadConfigValue(valOffset + 56), 56, curRow + posEnc);
                //	posEnc++;
                //}

                var posZone = 0;
                foreach (var zoneAndTerritory in item.ZonesAndTerritories)
                {
                    AddEntry(sheet, zoneAndTerritory.Description, ReadConfigValue(valOffset + 57), 57,
                        curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.CodeZoneDoc, ReadConfigValue(valOffset + 58), 58,
                        curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.AccountNumber, ReadConfigValue(valOffset + 59), 59,
                        curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.ContentRestrictions, ReadConfigValue(valOffset + 60), 60,
                        curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.FullPartly.GetValueOrDefault() ? "Да" : "Нет",
                        ReadConfigValue(valOffset + 61), 61,
                        curRow + posZone);

                    AddEntry(sheet, zoneAndTerritory.Document?.CodeDocument.Code, ReadConfigValue(valOffset + 62), 62,
                        curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.Document?.CodeDocument.Name, ReadConfigValue(valOffset + 63), 63,
                        curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.Document?.Name, ReadConfigValue(valOffset + 64), 64,
                        curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.Document?.Series, ReadConfigValue(valOffset + 65), 65,
                        curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.Document?.Number, ReadConfigValue(valOffset + 66), 66,
                        curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.Document?.Date?.ToString("dd/MM/yyyy"),
                        ReadConfigValue(valOffset + 67), 67, curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.Document?.IssueOrgan, ReadConfigValue(valOffset + 68), 68,
                        curRow + posZone);
                    AddEntry(sheet, zoneAndTerritory.Document?.Desc, ReadConfigValue(valOffset + 69), 69,
                        curRow + posZone);
                    posZone++;
                }

                var posGov = 0;
                foreach (var supervision in item.GovernmentLandSupervision)
                {
                    AddEntry(sheet, supervision.Agency, ReadConfigValue(valOffset + 70), 70, curRow + posGov);

                    AddEntry(sheet, supervision.EventName.Code, ReadConfigValue(valOffset + 71), 71, curRow + posGov);
                    AddEntry(sheet, supervision.EventName.Name, ReadConfigValue(valOffset + 72), 72, curRow + posGov);
                    AddEntry(sheet, supervision.EventForm.Code, ReadConfigValue(valOffset + 73), 73, curRow + posGov);
                    AddEntry(sheet, supervision.EventForm.Name, ReadConfigValue(valOffset + 74), 74, curRow + posGov);
                    AddEntry(sheet, supervision.InspectionEnd?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 75),
                        75, curRow + posGov);
                    AddEntry(sheet, supervision.AvailabilityViolations.GetValueOrDefault() ? "Да" : "Нет",
                        ReadConfigValue(valOffset + 76), 76,
                        curRow + posGov);
                    AddEntry(sheet, supervision.IdentifiedViolations.Area.ToString(), ReadConfigValue(valOffset + 77),
                        77, curRow + posGov);
                    AddEntry(sheet, supervision.IdentifiedViolations.TypeViolations, ReadConfigValue(valOffset + 78),
                        78, curRow + posGov);
                    AddEntry(sheet, supervision.IdentifiedViolations.SignViolations, ReadConfigValue(valOffset + 79),
                        79, curRow + posGov);
                    AddEntry(sheet, supervision.DocRequisites?.CodeDocument.Code, ReadConfigValue(valOffset + 80), 80,
                        curRow + posGov);
                    AddEntry(sheet, supervision.DocRequisites?.CodeDocument.Name, ReadConfigValue(valOffset + 81), 81,
                        curRow + posGov);
                    AddEntry(sheet, supervision.DocRequisites?.Name, ReadConfigValue(valOffset + 82), 82,
                        curRow + posGov);
                    AddEntry(sheet, supervision.DocRequisites?.Series, ReadConfigValue(valOffset + 83), 83,
                        curRow + posGov);
                    AddEntry(sheet, supervision.DocRequisites?.Number, ReadConfigValue(valOffset + 84), 84,
                        curRow + posGov);
                    AddEntry(sheet, supervision.DocRequisites?.Date?.ToString("dd/MM/yyyy"),
                        ReadConfigValue(valOffset + 85), 85, curRow + posGov);
                    AddEntry(sheet, supervision.DocRequisites?.IssueOrgan, ReadConfigValue(valOffset + 86), 86,
                        curRow + posGov);
                    AddEntry(sheet, supervision.DocRequisites?.Desc, ReadConfigValue(valOffset + 87), 87,
                        curRow + posGov);

                    AddEntry(sheet, supervision.Elimination.EliminationMark ? "Да" : "Нет",
                        ReadConfigValue(valOffset + 88),
                        88, curRow + posGov);
                    AddEntry(sheet, supervision.Elimination.EliminationAgency, ReadConfigValue(valOffset + 89), 89,
                        curRow + posGov);
                    AddEntry(sheet, supervision.EliminationDocRequisites?.CodeDocument.Code,
                        ReadConfigValue(valOffset + 90), 90, curRow + posGov);
                    AddEntry(sheet, supervision.EliminationDocRequisites?.CodeDocument.Name,
                        ReadConfigValue(valOffset + 91), 91, curRow + posGov);
                    AddEntry(sheet, supervision.EliminationDocRequisites?.Name, ReadConfigValue(valOffset + 92), 92,
                        curRow + posGov);
                    AddEntry(sheet, supervision.EliminationDocRequisites?.Series, ReadConfigValue(valOffset + 93), 93,
                        curRow + posGov);
                    AddEntry(sheet, supervision.EliminationDocRequisites?.Number, ReadConfigValue(valOffset + 94), 94,
                        curRow + posGov);
                    AddEntry(sheet, supervision.EliminationDocRequisites?.Date?.ToString("dd/MM/yyyy"),
                        ReadConfigValue(valOffset + 95), 95,
                        curRow + posGov);
                    AddEntry(sheet, supervision.EliminationDocRequisites?.IssueOrgan, ReadConfigValue(valOffset + 96),
                        96, curRow + posGov);
                    AddEntry(sheet, supervision.EliminationDocRequisites?.Desc, ReadConfigValue(valOffset + 97), 97,
                        curRow + posGov);
                    posGov++;
                }

                int[] rowsAdded = {posGov, posCadNum, posEnc, posZone, 1};
                curRow += rowsAdded.Max();

                DrawBorder(sheet, curRow, 97);
            }

            RemoveUncheckedColumns(sheet, valOffset, 97);
        }

        private void AddBuildings(ExcelWorksheet sheet, List<xmlObjectBuild> buildings)
        {
            var valOffset = 200;
            sheet.Cells.Style.WrapText = true;
            var curRow = 5;
            foreach (var item in buildings)
            {
                AddEntry(sheet, item.CadastralNumber, true, 1, curRow);

                AddEntry(sheet, item.TypeObject.ToString(), ReadConfigValue(valOffset + 2), 2, curRow);
                AddEntry(sheet, item.TypeRealty, ReadConfigValue(valOffset + 3), 3, curRow);
                AddEntry(sheet, item.DateCreate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 4), 4, curRow);
                AddEntry(sheet, item.CadastralNumberBlock, ReadConfigValue(valOffset + 5), 5, curRow);

                var posCadNum = 0;
                foreach (var parentCadastralNumber in item.ParentCadastralNumbers)
                {
                    AddEntry(sheet, parentCadastralNumber, ReadConfigValue(valOffset + 6), 6, curRow + posCadNum);
                    posCadNum++;
                }

                AddEntry(sheet, item.CadastralCost?.Value?.ToString(), ReadConfigValue(valOffset + 7), 7, curRow);
                AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 8), 8,
                    curRow);
                AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 9),
                    9,
                    curRow);
                AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 10),
                    10,
                    curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 11),
                    11, curRow);
                AddEntry(sheet, item.CadastralCost?.DocNumber, ReadConfigValue(valOffset + 12), 12, curRow);
                AddEntry(sheet, item.CadastralCost?.DocName, ReadConfigValue(valOffset + 13), 13, curRow);
                AddEntry(sheet, item.CadastralCost?.DateApproval?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 14),
                    14, curRow);
                AddEntry(sheet, item.CadastralCost?.RevisalStatementDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 15), 15, curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationLastDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 16), 16, curRow);


                AddEntry(sheet, item.Area?.ToString(), ReadConfigValue(valOffset + 16), 16, curRow);


                AddEntry(sheet, item.Adress.FIAS, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.OKATO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.OKTMO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.RussianFederation, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 19), 19, curRow);
                AddEntry(sheet, item.Adress.Note, ReadConfigValue(valOffset + 20), 20, curRow);
                AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 21), 21, curRow);
                AddEntry(sheet, item.Adress.AddressOrLocation, ReadConfigValue(valOffset + 21), 21, curRow);


                AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
                AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
                AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
                AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.SovietVillage?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.PlanningElement?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.Street?.Value, ReadConfigValue(valOffset + 26), 26, curRow);
                AddEntry(sheet, item.Adress.Level1?.Value, ReadConfigValue(valOffset + 27), 27, curRow);
                AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 28), 28, curRow);
                AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 29), 29, curRow);
                AddEntry(sheet, item.Adress.Apartment?.Value, ReadConfigValue(valOffset + 30), 30, curRow);

                AddEntry(sheet, item.AssignationBuilding.Code, ReadConfigValue(valOffset + 31), 31, curRow);
                AddEntry(sheet, item.AssignationBuilding.Name, ReadConfigValue(valOffset + 32), 32, curRow);

                AddEntry(sheet, item.Floors.Floors, ReadConfigValue(valOffset + 33), 33, curRow);
                AddEntry(sheet, item.Floors.Underground_Floors, ReadConfigValue(valOffset + 34), 34, curRow);

                AddEntry(sheet, item.Years.Year_Built, ReadConfigValue(valOffset + 35), 35, curRow);
                AddEntry(sheet, item.Years.Year_Used, ReadConfigValue(valOffset + 36), 36, curRow);

                AddEntry(sheet, item.Name, ReadConfigValue(valOffset + 37), 37, curRow);

                var posWalls = 0;
                foreach (var wall in item.Walls)
                {
                    AddEntry(sheet, wall.Code, ReadConfigValue(valOffset + 38), 38, curRow + posWalls);
                    AddEntry(sheet, wall.Name, ReadConfigValue(valOffset + 39), 39, curRow + posWalls);
                    posWalls++;
                }

                int[] rowsAdded = {posWalls, posCadNum, 1};
                curRow += rowsAdded.Max();

                DrawBorder(sheet, curRow, 39);
            }

            RemoveUncheckedColumns(sheet, valOffset, 39);
        }

        private void AddConstructions(ExcelWorksheet sheet, List<xmlObjectConstruction> constructions)
        {
            var valOffset = 300;
            sheet.Cells.Style.WrapText = true;
            var curRow = 5;
            foreach (var item in constructions)
            {
                AddEntry(sheet, item.CadastralNumber, true, 1, curRow);

                AddEntry(sheet, item.TypeObject.ToString(), ReadConfigValue(valOffset + 2), 2, curRow);
                AddEntry(sheet, item.TypeRealty, ReadConfigValue(valOffset + 3), 3, curRow);
                AddEntry(sheet, item.DateCreate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 4), 4, curRow);
                AddEntry(sheet, item.CadastralNumberBlock, ReadConfigValue(valOffset + 5), 5, curRow);

                var posCadNum = 0;
                foreach (var parentCadastralNumber in item.ParentCadastralNumbers)
                {
                    AddEntry(sheet, parentCadastralNumber, ReadConfigValue(valOffset + 6), 6, curRow + posCadNum);
                    posCadNum++;
                }

                AddEntry(sheet, item.CadastralCost?.Value?.ToString(), ReadConfigValue(valOffset + 7), 7, curRow);
                AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 8), 8, curRow);
                AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 9),
                    9, curRow);
                AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 10),
                    10,
                    curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 11), 11, curRow);
                AddEntry(sheet, item.CadastralCost?.DocNumber, ReadConfigValue(valOffset + 12), 12, curRow);
                AddEntry(sheet, item.CadastralCost?.DocName, ReadConfigValue(valOffset + 13), 13, curRow);
                AddEntry(sheet, item.CadastralCost?.DateApproval?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 14), 14, curRow);
                AddEntry(sheet, item.CadastralCost?.RevisalStatementDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 15), 15, curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationLastDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 16), 16, curRow);


                AddEntry(sheet, item.Adress.FIAS, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.OKATO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 16), 16, curRow);
                AddEntry(sheet, item.Adress.OKTMO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.RussianFederation, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.Note, ReadConfigValue(valOffset + 19), 19, curRow);
                AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 20), 20, curRow);
                AddEntry(sheet, item.Adress.AddressOrLocation, ReadConfigValue(valOffset + 21), 21, curRow);


                AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 21), 21, curRow);
                AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
                AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
                AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
                AddEntry(sheet, item.Adress.SovietVillage?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.PlanningElement?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.Street?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.Level1?.Value, ReadConfigValue(valOffset + 26), 26, curRow);
                AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 27), 27, curRow);
                AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 28), 28, curRow);
                AddEntry(sheet, item.Adress.Apartment?.Value, ReadConfigValue(valOffset + 29), 29, curRow);


                AddEntry(sheet, item.AssignationName, ReadConfigValue(valOffset + 30), 30, curRow);

                AddEntry(sheet, item.Floors.Floors, ReadConfigValue(valOffset + 31), 31, curRow);
                AddEntry(sheet, item.Floors.Underground_Floors, ReadConfigValue(valOffset + 32), 32, curRow);

                AddEntry(sheet, item.Years.Year_Built, ReadConfigValue(valOffset + 33), 33, curRow);
                AddEntry(sheet, item.Years.Year_Used, ReadConfigValue(valOffset + 34), 34, curRow);

                AddEntry(sheet, item.Name, ReadConfigValue(valOffset + 35), 35, curRow);

                var posParams = 0;
                foreach (var keyParam in item.KeyParameters)
                {
                    AddEntry(sheet, keyParam.Code, ReadConfigValue(valOffset + 36), 36, curRow + posParams);
                    AddEntry(sheet, keyParam.Name, ReadConfigValue(valOffset + 37), 37, curRow + posParams);
                    AddEntry(sheet, keyParam.Value, ReadConfigValue(valOffset + 38), 38, curRow + posParams);
                    posParams++;
                }

                int[] rowsAdded = {posCadNum, posParams, 1};
                curRow += rowsAdded.Max();

                DrawBorder(sheet, curRow, 38);
            }

            RemoveUncheckedColumns(sheet, valOffset, 38);
        }

        private void AddIncompleteBuildings(ExcelWorksheet sheet, List<xmlObjectUncomplited> incompletedBuildings)
        {
            var valOffset = 400;
            sheet.Cells.Style.WrapText = true;
            var curRow = 5;
            foreach (var item in incompletedBuildings)
            {
                AddEntry(sheet, item.CadastralNumber, true, 1, curRow);

                AddEntry(sheet, item.TypeObject.ToString(), ReadConfigValue(valOffset + 2), 2, curRow);
                AddEntry(sheet, item.TypeRealty, ReadConfigValue(valOffset + 3), 3, curRow);
                AddEntry(sheet, item.DateCreate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 4), 4, curRow);
                AddEntry(sheet, item.CadastralNumberBlock, ReadConfigValue(valOffset + 5), 5, curRow);

                var posCadNum = 0;
                foreach (var parentCadastralNumber in item.ParentCadastralNumbers)
                {
                    AddEntry(sheet, parentCadastralNumber, ReadConfigValue(valOffset + 6), 6, curRow + posCadNum);
                    posCadNum++;
                }

                AddEntry(sheet, item.CadastralCost?.Value?.ToString(), ReadConfigValue(valOffset + 7), 7, curRow);
                AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 8), 8, curRow);
                AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 9),
                    9, curRow);
                AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 10),
                    10,
                    curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 11), 11, curRow);
                AddEntry(sheet, item.CadastralCost?.DocNumber, ReadConfigValue(valOffset + 12), 12, curRow);
                AddEntry(sheet, item.CadastralCost?.DocName, ReadConfigValue(valOffset + 13), 13, curRow);
                AddEntry(sheet, item.CadastralCost?.DateApproval?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 14), 14, curRow);
                AddEntry(sheet, item.CadastralCost?.RevisalStatementDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 15), 15, curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationLastDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 16), 16, curRow);


                AddEntry(sheet, item.Adress.FIAS, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.OKATO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 16), 16, curRow);
                AddEntry(sheet, item.Adress.OKTMO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.RussianFederation, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.Note, ReadConfigValue(valOffset + 19), 19, curRow);
                AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 20), 20, curRow);
                AddEntry(sheet, item.Adress.AddressOrLocation, ReadConfigValue(valOffset + 21), 21, curRow);


                AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 21), 21, curRow);
                AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
                AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
                AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
                AddEntry(sheet, item.Adress.SovietVillage?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.PlanningElement?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.Street?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.Level1?.Value, ReadConfigValue(valOffset + 26), 26, curRow);
                AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 27), 27, curRow);
                AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 28), 28, curRow);
                AddEntry(sheet, item.Adress.Apartment?.Value, ReadConfigValue(valOffset + 29), 29, curRow);

                AddEntry(sheet, item.AssignationName, ReadConfigValue(valOffset + 30), 30, curRow);

                AddEntry(sheet, item.DegreeReadiness?.ToString(), ReadConfigValue(valOffset + 31), 31, curRow);

                var posParams = 0;
                foreach (var keyParam in item.KeyParameters)
                {
                    AddEntry(sheet, keyParam.Code, ReadConfigValue(valOffset + 32), 32, curRow + posParams);
                    AddEntry(sheet, keyParam.Name, ReadConfigValue(valOffset + 33), 33, curRow + posParams);
                    AddEntry(sheet, keyParam.Value, ReadConfigValue(valOffset + 34), 34, curRow + posParams);
                    posParams++;
                }

                int[] rowsAdded = {posCadNum, posParams, 1};
                curRow += rowsAdded.Max();
                DrawBorder(sheet, curRow, 34);
            }

            RemoveUncheckedColumns(sheet, valOffset, 34);
        }

        private void AddFlats(ExcelWorksheet sheet, List<xmlObjectFlat> flats)
        {
            var valOffset = 500;
            sheet.Cells.Style.WrapText = true;
            var curRow = 5;
            foreach (var item in flats)
            {
                AddEntry(sheet, item.CadastralNumber, true, 1, curRow);
                AddEntry(sheet, item.TypeObject.ToString(), ReadConfigValue(valOffset + 2), 2, curRow);
                AddEntry(sheet, item.TypeRealty, ReadConfigValue(valOffset + 3), 3, curRow);
                AddEntry(sheet, item.DateCreate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 4), 4, curRow);
                AddEntry(sheet, item.CadastralNumberBlock, ReadConfigValue(valOffset + 5), 5, curRow);
                AddEntry(sheet, item.CadastralNumberFlat, ReadConfigValue(valOffset + 6), 6, curRow);

                AddEntry(sheet, item.CadastralCost?.Value?.ToString(), ReadConfigValue(valOffset + 7), 7, curRow);
                AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 8), 8, curRow);
                AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 9),
                    9, curRow);
                AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 10),
                    10,
                    curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 11), 11, curRow);
                AddEntry(sheet, item.CadastralCost?.DocNumber, ReadConfigValue(valOffset + 12), 12, curRow);
                AddEntry(sheet, item.CadastralCost?.DocName, ReadConfigValue(valOffset + 13), 13, curRow);
                AddEntry(sheet, item.CadastralCost?.DateApproval?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 14), 14, curRow);
                AddEntry(sheet, item.CadastralCost?.RevisalStatementDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 15), 15, curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationLastDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 16), 16, curRow);


                AddEntry(sheet, item.Area?.ToString(), ReadConfigValue(valOffset + 16), 16, curRow);


                AddEntry(sheet, item.Adress.FIAS, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.OKATO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.OKTMO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.RussianFederation, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 19), 19, curRow);
                AddEntry(sheet, item.Adress.Note, ReadConfigValue(valOffset + 20), 20, curRow);
                AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 21), 21, curRow);
                AddEntry(sheet, item.Adress.AddressOrLocation, ReadConfigValue(valOffset + 21), 21, curRow);


                AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
                AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
                AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
                AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.SovietVillage?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.PlanningElement?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.Street?.Value, ReadConfigValue(valOffset + 26), 26, curRow);
                AddEntry(sheet, item.Adress.Level1?.Value, ReadConfigValue(valOffset + 27), 27, curRow);
                AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 28), 28, curRow);
                AddEntry(sheet, item.Adress.Level3?.Value, ReadConfigValue(valOffset + 29), 29, curRow);
                AddEntry(sheet, item.Adress.Apartment?.Value, ReadConfigValue(valOffset + 30), 30, curRow);

                AddEntry(sheet, item.AssignationFlatCode.Code, ReadConfigValue(valOffset + 31), 31, curRow);
                AddEntry(sheet, item.AssignationFlatCode.Name, ReadConfigValue(valOffset + 32), 32, curRow);

                AddEntry(sheet, item.AssignationFlatType.Code, ReadConfigValue(valOffset + 33), 33, curRow);
                AddEntry(sheet, item.AssignationFlatType.Name, ReadConfigValue(valOffset + 34), 34, curRow);

                AddEntry(sheet, item.Name, ReadConfigValue(valOffset + 35), 35, curRow);

                var posRows = 0;
                //TODO: использовать item.Position и item.Levels
                //foreach (var position in item.PositionsInObject)
                //{
                //	if (!ReadConfigValue(valOffset + 36)
                //		&& !ReadConfigValue(valOffset + 37)
                //		&& !ReadConfigValue(valOffset + 38)) continue;

                //	AddEntry(sheet, position.Position.Code, ReadConfigValue(valOffset + 36), 36, curRow + posRows);
                //	AddEntry(sheet, position.Position.Name, ReadConfigValue(valOffset + 37), 37, curRow + posRows);
                //	AddEntry(sheet, position.Position.Value, ReadConfigValue(valOffset + 38), 38, curRow + posRows);

                //	foreach (var number in position.NumbersOnPlan.Where(number => ReadConfigValue(valOffset + 39)))
                //	{
                //		AddEntry(sheet, number, true, 39, curRow + posRows);
                //		posRows++;
                //	}
                //}

                AddEntry(sheet, item.CadastralNumberOKS, ReadConfigValue(valOffset + 40), 40, curRow);

                //TODO: item.ParentOks
                //AddEntry(sheet, item.parentFloors.Floors, ReadConfigValue(valOffset + 41), 41, curRow);
                //AddEntry(sheet, item.parentFloors.Underground_Floors, ReadConfigValue(valOffset + 42), 42, curRow);

                //AddEntry(sheet, item.parentYears.Year_Built, ReadConfigValue(valOffset + 43), 43, curRow);
                //AddEntry(sheet, item.parentYears.Year_Used, ReadConfigValue(valOffset + 44), 44, curRow);

                //AddEntry(sheet, item.parentAssignationBuilding.Code, ReadConfigValue(valOffset + 45), 45, curRow);
                //AddEntry(sheet, item.parentAssignationBuilding.Name, ReadConfigValue(valOffset + 46), 46, curRow);

                //AddEntry(sheet, item.parentAssignationName, ReadConfigValue(valOffset + 47), 47, curRow);

                //var posWalls = 0;
                //foreach (var wall in item.parentWalls)
                //{
                //	if (!ReadConfigValue(valOffset + 48)
                //		&& !ReadConfigValue(valOffset + 49)) continue;

                //	AddEntry(sheet, wall.Code, ReadConfigValue(valOffset + 48), 48, curRow + posWalls);
                //	AddEntry(sheet, wall.Name, ReadConfigValue(valOffset + 49), 49, curRow + posWalls);
                //	posWalls++;
                //}

                //int[] rowsAdded = { posWalls, posRows, 1 };

                int[] rowsAdded = {posRows, 1};
                curRow += rowsAdded.Max();

                DrawBorder(sheet, curRow, 49);
            }

            RemoveUncheckedColumns(sheet, valOffset, 49);
        }

        private void AddCarPlaces(ExcelWorksheet sheet, List<xmlObjectCarPlace> carPlaces)
        {
            var valOffset = 600;
            sheet.Cells.Style.WrapText = true;
            var curRow = 5;
            foreach (var item in carPlaces)
            {
                AddEntry(sheet, item.CadastralNumber, true, 1, curRow);

                AddEntry(sheet, item.TypeObject.ToString(), ReadConfigValue(valOffset + 2), 2, curRow);
                AddEntry(sheet, item.TypeRealty, ReadConfigValue(valOffset + 3), 3, curRow);
                AddEntry(sheet, item.DateCreate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 4), 4, curRow);
                AddEntry(sheet, item.CadastralNumberBlock, ReadConfigValue(valOffset + 5), 5, curRow);

                AddEntry(sheet, item.CadastralCost?.Value?.ToString(), ReadConfigValue(valOffset + 6), 6, curRow);
                AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 7), 7, curRow);
                AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 8),
                    8, curRow);
                AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 9), 9,
                    curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 10), 10, curRow);
                AddEntry(sheet, item.CadastralCost?.DocNumber, ReadConfigValue(valOffset + 11), 11, curRow);
                AddEntry(sheet, item.CadastralCost?.DocName, ReadConfigValue(valOffset + 12), 12, curRow);
                AddEntry(sheet, item.CadastralCost?.DateApproval?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 13), 13, curRow);
                AddEntry(sheet, item.CadastralCost?.RevisalStatementDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 14), 14, curRow);
                AddEntry(sheet, item.CadastralCost?.ApplicationLastDate?.ToString("dd/MM/yyyy"),
                    ReadConfigValue(valOffset + 15), 15, curRow);


                AddEntry(sheet, item.Area?.ToString(), ReadConfigValue(valOffset + 15), 15, curRow);


                AddEntry(sheet, item.Adress.FIAS, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.OKATO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 16), 16, curRow);
                AddEntry(sheet, item.Adress.OKTMO, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 17), 17, curRow);
                AddEntry(sheet, item.Adress.RussianFederation, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 18), 18, curRow);
                AddEntry(sheet, item.Adress.Note, ReadConfigValue(valOffset + 19), 19, curRow);
                AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 20), 20, curRow);
                AddEntry(sheet, item.Adress.AddressOrLocation, ReadConfigValue(valOffset + 21), 21, curRow);


                AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 21), 21, curRow);
                AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
                AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
                AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
                AddEntry(sheet, item.Adress.SovietVillage?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.PlanningElement?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.Street?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
                AddEntry(sheet, item.Adress.Level1?.Value, ReadConfigValue(valOffset + 26), 26, curRow);
                AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 27), 27, curRow);
                AddEntry(sheet, item.Adress.Level3?.Value, ReadConfigValue(valOffset + 28), 28, curRow);
                AddEntry(sheet, item.Adress.Apartment?.Value, ReadConfigValue(valOffset + 29), 29, curRow);

                //TODO: PositionInObject
                //var posRows = 0;
                //foreach (var position in item.PositionsInObject)
                //{
                //	AddEntry(sheet, position.Position.Code, ReadConfigValue(valOffset + 30), 30, curRow + posRows);
                //	AddEntry(sheet, position.Position.Name, ReadConfigValue(valOffset + 31), 31, curRow + posRows);
                //	AddEntry(sheet, position.Position.Value, ReadConfigValue(valOffset + 32), 32, curRow + posRows);

                //	foreach (var number in position.NumbersOnPlan)
                //	{
                //		AddEntry(sheet, number, ReadConfigValue(valOffset + 33), 33, curRow + posRows);
                //		posRows++;
                //	}
                //}

                AddEntry(sheet, item.CadastralNumberOKS, ReadConfigValue(valOffset + 34), 34, curRow);

                //TODO: item.ParentOks
                //AddEntry(sheet, item.parentFloors.Floors, ReadConfigValue(valOffset + 35), 35, curRow);
                //AddEntry(sheet, item.parentFloors.Underground_Floors, ReadConfigValue(valOffset + 36), 36, curRow);

                //AddEntry(sheet, item.parentYears.Year_Built, ReadConfigValue(valOffset + 37), 37, curRow);
                //AddEntry(sheet, item.parentYears.Year_Used, ReadConfigValue(valOffset + 38), 38, curRow);

                //AddEntry(sheet, item.parentAssignationBuilding.Code, ReadConfigValue(valOffset + 39), 39, curRow);
                //AddEntry(sheet, item.parentAssignationBuilding.Name, ReadConfigValue(valOffset + 40), 40, curRow);

                //AddEntry(sheet, item.parentAssignationName, ReadConfigValue(valOffset + 41), 41, curRow);

                //var posWalls = 0;
                //foreach (var wall in item.parentWalls)
                //{
                //	AddEntry(sheet, wall.Code, ReadConfigValue(valOffset + 42), 42, curRow + posWalls);
                //	AddEntry(sheet, wall.Name, ReadConfigValue(valOffset + 43), 43, curRow + posWalls);
                //	posWalls++;
                //}

                //int[] rowsAdded = { posWalls, posRows, 1 };
                int[] rowsAdded = {1};
                curRow += rowsAdded.Max();

                DrawBorder(sheet, curRow, 43);
            }

            RemoveUncheckedColumns(sheet, valOffset, 43);
        }

        private static void AddEntry(ExcelWorksheet worksheet, string text, bool output, int column, int row,
            int cols = 1, int rows = 1)
        {
            if (!output) return;
            var range = worksheet.Cells
                .GetSubrange(
                    CellRange.RowColumnToPosition(row - 1, column - 1),
                    CellRange.RowColumnToPosition(row + rows - 2, column + cols - 2));
            range.Value = text;
            range.Merged = true;
        }

        private static void DrawBorder(ExcelWorksheet sheet, int row, int widthInColumns)
        {
            for (var col = 0; col < widthInColumns; col++)
                sheet.Cells[row - 1, col].Style.Borders[IndividualBorder.Top].LineStyle = LineStyle.Thin;
        }

        private void RemoveUncheckedColumns(ExcelWorksheet sheet, int valOffset, int colCount)
        {
            for (var c = colCount; c > 1; c--)
                if (!ReadConfigValue(valOffset + c))
                    sheet.Columns.Remove(c - 1, 1);
        }

        private void LeaveOneWorksheet(ExcelFile file, string listToKeep)
        {
            foreach (var (_, value) in _listNames)
                if (value != listToKeep)
                    file.Worksheets.Remove(value);
            SetColumnSize(file);
        }

        private void TrimEmptyWorksheets(ExcelFile file)
        {
            if (!ReadConfigValue(10000)) file.Worksheets.Remove(_listNames[1]);
            if (!ReadConfigValue(20000)) file.Worksheets.Remove(_listNames[2]);
            if (!ReadConfigValue(30000)) file.Worksheets.Remove(_listNames[3]);
            if (!ReadConfigValue(40000)) file.Worksheets.Remove(_listNames[4]);
            if (!ReadConfigValue(50000)) file.Worksheets.Remove(_listNames[5]);
            if (!ReadConfigValue(60000)) file.Worksheets.Remove(_listNames[6]);
            SetColumnSize(file);
        }

        private void SetColumnSize(ExcelFile excelFile)
        {
            foreach (var worksheet in excelFile.Worksheets)
            {
                foreach (var column in worksheet.Columns)
                {
                    column.SetWidth(150, LengthUnit.Pixel);
                }
            }
        }

        [UsedImplicitly]
        private class XmlToExcelExportWorker
        {
            private readonly ExcelWorksheet _sheet;
            private int _curRow;
            private readonly int _configOffset;
            private readonly int[] _config;

            private XmlToExcelExportWorker(ExcelWorksheet sheet, int[] config, int initialRow, int configOffset)
            {
                _sheet = sheet;
                _config = config;
                _curRow = initialRow;
                _configOffset = configOffset;
            }

            private void AddParticular(xmlObjectParticular xml, int col)
            {
                AddEntry(xml.CadastralNumber, col + 0);
                AddEntry(xml.TypeObject.ToString(), col + 0);
                AddEntry(xml.TypeRealty, col + 0);
                AddEntry(xml.DateCreate, col + 0);
                AddEntry(xml.CadastralNumberBlock, col + 0);
                AddCost(xml.CadastralCost, col + 0);
                AddAddress(xml.Adress, col + 0);
                AddEntry(xml.AssessmentDate, col + 0);
                AddEntry(xml.Area, col + 0);
            }

            [UsedImplicitly]
            [MustUseReturnValue]
            public int AddParcel(xmlObjectParcel xml, int col)
            {
                AddParticular(xml, 0);
                var innerCadNumbers = AddEntry(xml.InnerCadastralNumbers, col + 0);
                AddEntry(xml.Area, col + 0);
                AddEntry(xml.AreaInaccuracy, col + 0);
                AddCodeName(xml.Name, col + 0);
                AddCodeName(xml.Category, col + 0);
                AddUtilization(xml.Utilization, col + 0);

                var naturalObjects = 0;
                foreach (var natObject in xml.NaturalObjects)
                {
                    var natObjCount = AddNaturalObject(natObject, 0, naturalObjects);
                    naturalObjects += natObjCount;
                }

                var subParcels = 0;
                foreach (var subParcel in xml.SubParcels)
                {
                    var subParcelCount = AddSubParcel(subParcel, 0, subParcels);
                    subParcels += subParcelCount;
                }

                AddEntry(xml.FacilityCadastralNumber, col + 0);
                AddEntry(xml.FacilityPurpose, col + 0);

                var zonesAndTerritories = 0;
                foreach (var zones in xml.ZonesAndTerritories)
                {
                    AddZoneAndTerritory(zones, 0, zonesAndTerritories);
                    zonesAndTerritories++;
                }

                var governmentLandSupervision = 0;
                foreach (var supervision in xml.GovernmentLandSupervision)
                {
                    AddSupervisionEvent(supervision, 0, governmentLandSupervision);
                    governmentLandSupervision++;
                }

                AddEntry(xml.SurveyingProjectNum, col + 0);
                AddDocument(xml.SurveyingProjectDecisionRequisites, col + 0);
                AddHiredHouse(xml.HiredHouse, col + 0);
                AddEntry(xml.LimitedCirculation, col + 0);

                int[] rowsAdded =
                    {innerCadNumbers, naturalObjects, subParcels, zonesAndTerritories, governmentLandSupervision, 1};
                return rowsAdded.Max();
            }

            [UsedImplicitly]
            [MustUseReturnValue]
            public int AddBuild(xmlObjectBuild xml, int col)
            {
                AddParticular(xml, 0);
                var parentCadNumbers = AddEntry(xml.ParentCadastralNumbers, col + 0);
                AddEntry(xml.Area, col + 0);
                AddCodeName(xml.AssignationBuilding, col + 0);
                AddFloors(xml.Floors, col + 0);
                AddYear(xml.Years, col + 0);
                AddEntry(xml.Name, col + 0);

                var walls = 0;
                foreach (var wall in xml.Walls)
                {
                    AddCodeName(wall, 0, walls);
                    walls++;
                }

                var permittedUses = AddEntry(xml.ObjectPermittedUses, col + 0);

                var subBuildings = 0;
                foreach (var subBuildingFlat in xml.SubBuildings)
                {
                    var subBuildingCount = AddSubBuildingFlat(subBuildingFlat, 0, subBuildings);
                    subBuildings += subBuildingCount;
                }

                var flatsCadNumbers = AddEntry(xml.FlatsCadastralNumbers, col + 0);
                var carSpacesCadNumbers = AddEntry(xml.CarParkingSpacesCadastralNumbers, col + 0);
                var unitedCadNumbers = AddEntry(xml.UnitedCadastralNumbers, col + 0);
                AddEntry(xml.FacilityCadastralNumber, col + 0);
                AddEntry(xml.FacilityPurpose, col + 0);
                AddCulturalHeritage(xml.CulturalHeritage, col + 0);

                int[] rowsAdded =
                {
                    parentCadNumbers, walls, permittedUses, subBuildings,
                    flatsCadNumbers, carSpacesCadNumbers, unitedCadNumbers, 1
                };

                return rowsAdded.Max();
            }

            [UsedImplicitly]
            [MustUseReturnValue]
            public int AddConstruction(xmlObjectConstruction xml, int col)
            {
                AddParticular(xml, 0);
                var parentCadNumbers = AddEntry(xml.ParentCadastralNumbers, col + 0);
                AddEntry(xml.AssignationName, col + 0);
                AddFloors(xml.Floors, col + 0);
                AddYear(xml.Years, col + 0);
                AddEntry(xml.Name, col + 0);

                var keyParameters = 0;
                foreach (var param in xml.KeyParameters)
                {
                    AddCodeNameValue(param, 0, keyParameters);
                    keyParameters++;
                }

                var permittedUses = AddEntry(xml.ObjectPermittedUses, col + 0);

                var subConstructions = 0;
                foreach (var subConstruction in xml.SubConstructions)
                {
                    var constructionCount = AddSubConstruction(subConstruction, 0, subConstructions);
                    subConstructions += constructionCount;
                }

                var flatsCadNumbers = AddEntry(xml.FlatsCadastralNumbers, col + 0);
                var carSpacesCadNumbers = AddEntry(xml.CarParkingSpacesCadastralNumbers, col + 0);
                var unitedCadNumbers = AddEntry(xml.UnitedCadastralNumbers, col + 0);
                AddEntry(xml.FacilityCadastralNumber, col + 0);
                AddEntry(xml.FacilityPurpose, col + 0);
                AddCulturalHeritage(xml.CulturalHeritage, col + 0);

                int[] rowsAdded =
                {
                    parentCadNumbers, keyParameters, permittedUses, subConstructions,
                    flatsCadNumbers, carSpacesCadNumbers, unitedCadNumbers, 1
                };
                return rowsAdded.Max();
            }

            [UsedImplicitly]
            [MustUseReturnValue]
            public int AddUncomplited(xmlObjectUncomplited xml, int col)
            {
                AddParticular(xml, 0);
                var parentCadNumbers = AddEntry(xml.ParentCadastralNumbers, col + 0);
                AddEntry(xml.AssignationName, col + 0);
                AddEntry(xml.DegreeReadiness, col + 0);
                var keyParameters = 0;
                foreach (var param in xml.KeyParameters)
                {
                    AddCodeNameValue(param, 0, keyParameters);
                    keyParameters++;
                }

                AddEntry(xml.FacilityCadastralNumber, col + 0);
                AddEntry(xml.FacilityPurpose, col + 0);

                int[] rowsAdded = {parentCadNumbers, keyParameters, 1};
                return rowsAdded.Max();
            }

            [UsedImplicitly]
            [MustUseReturnValue]
            public int AddFlat(xmlObjectFlat xml, int col)
            {
                AddParticular(xml,0);
                AddEntry(xml.CadastralNumberFlat, col + 0);
                AddEntry(xml.CadastralNumberOKS, col + 0);
                var parentOks = AddParentOks(xml.ParentOks, col + 0);
                AddEntry(xml.Name, col + 0);
                AddEntry(xml.Area, col + 0);
                AddPos(xml.Position, col + 0);

                var levelCounter = 0;
                foreach (var level in xml.Levels)
                {
                    AddLevel(level, 0, levelCounter);
                    levelCounter++;
                }

                AddCodeName(xml.AssignationFlatCode, col + 0);
                AddCodeName(xml.AssignationFlatType, col + 0);
                AddCodeName(xml.AssignationSpecialType, col + 0);
                AddEntry(xml.AssignationTotalAssets, col + 0);
                AddEntry(xml.AssignationAuxiliaryFlat, col + 0);
                var permittedUses = AddEntry(xml.ObjectPermittedUses, col + 0);

                var subFlats = 0;
                foreach (var buildingFlat in xml.SubFlats)
                {
                    var flatsCount = AddSubBuildingFlat(buildingFlat, 0, subFlats);
                    subFlats += flatsCount;
                }

                var unitedCadNumbers = AddEntry(xml.UnitedCadastralNumbers, col + 0);
                AddEntry(xml.FacilityCadastralNumber, col + 0);
                AddEntry(xml.FacilityPurpose, col + 0);
                AddCulturalHeritage(xml.CulturalHeritage, col + 0);

                int[] rowsAdded = {levelCounter, permittedUses, subFlats, unitedCadNumbers, parentOks, 1};
                return rowsAdded.Max();
            }

            [UsedImplicitly]
            [MustUseReturnValue]
            public int AddCarPlace(xmlObjectCarPlace xml, int col)
            {
                AddParticular(xml, 0);
                AddEntry(xml.Area, col + 0);
                AddEntry(xml.CadastralNumberOKS, col + 0);
                var parentOks = AddParentOks(xml.ParentOks, col + 0);
                AddLevel(xml.PositionInObject, col + 0);
                var unitedCadNumbers = AddEntry(xml.UnitedCadastralNumbers, col + 0);
                AddEntry(xml.FacilityCadastralNumber, col + 0);
                AddEntry(xml.FacilityPurpose, col + 0);

                int[] rowsAdded = {unitedCadNumbers, parentOks, 1};
                return rowsAdded.Max();
            }

            /// <summary>
            /// Ширина: 10 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddCost(xmlCost xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Value, col + col + 0);
                AddEntry(xml.DateValuation, col + 1);
                AddEntry(xml.DateEntering, col + 2);
                AddEntry(xml.DateApproval, col + 3);
                AddEntry(xml.DocNumber, col + 4);
                AddEntry(xml.DocDate, col + 5);
                AddEntry(xml.ApplicationDate, col + 6);
                AddEntry(xml.RevisalStatementDate, col + 7);
                AddEntry(xml.ApplicationLastDate, col + 8);
                AddEntry(xml.DocName, col + 9);
            }

            /// <summary>
            /// Ширина: 25 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddAddress(xmlAdress xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.AddressStr, col + 0);
                AddEntry(xml.FIAS, col + 1);
                AddEntry(xml.OKATO, col + 2);
                AddEntry(xml.KLADR, col + 3);
                AddEntry(xml.OKTMO, col + 4);
                AddEntry(xml.PostalCode, col + 5);
                AddEntry(xml.RussianFederation, col + 6);
                AddEntry(xml.Region, col + 7);
                AddEntry(xml.Note, col + 8);
                AddEntry(xml.Other, col + 9);
                AddEntry(xml.AddressOrLocation, col + 10);
                AddAddressLevel(xml.District, col + 11);
                AddAddressLevel(xml.City, col + 12);
                AddAddressLevel(xml.UrbanDistrict, col + 13);
                AddAddressLevel(xml.SovietVillage, col + 14);
                AddAddressLevel(xml.Locality, col + 15);
                AddAddressLevel(xml.PlanningElement, col + 16);
                AddAddressLevel(xml.Street, col + 17);
                AddAddressLevel(xml.Level1, col + 18);
                AddAddressLevel(xml.Level2, col + 19);
                AddAddressLevel(xml.Level3, col + 20);
                AddAddressLevel(xml.Apartment, col + 21);
                AddEntry(xml.InBounds, col + 22);
                AddEntry(xml.Placed, col + 23);
                AddElaborationLocation(xml.Elaboration, col + 24);
            }

            /// <summary>
            /// Ширина: 1 колонка, игнорируем тип во второй колонке
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddAddressLevel(xmlAdresLevel xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Value, col + col + 0);
                //AddEntry(xml.Type,col + 0);
            }

            /// <summary>
            /// Ширина: 3 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddElaborationLocation(xmlElaborationLocation xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.ReferenceMark, col + col + 0);
                AddEntry(xml.Distance, col + 1);
                AddEntry(xml.Direction, col + 2);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddCodeName(xmlCodeName xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Name, col + col + 0);
                AddEntry(xml.Code, col + 1);
            }

            /// <summary>
            /// Ширина: 4 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddUtilization(xmlUtilization xml, int col, int rowOffset = 0)
            {
                AddCodeName(xml.LandUse, col + col + 0);
                AddEntry(xml.ByDoc, col + 2);
                AddEntry(xml.PermittedUseText, col + 3);
            }

            /// <summary>
            /// Ширина: 14 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            /// <returns></returns>
            [MustUseReturnValue]
            private int AddNaturalObject(xmlNaturalObject xml, int col, int rowOffset = 0)
            {
                AddCodeName(xml.Kind, col+col + 0);
                AddEntry(xml.Forestry, col+2);
                AddCodeName(xml.ForestUse, col+3);
                AddEntry(xml.QuarterNumbers, col+5);
                AddEntry(xml.TaxationSeparations, col+6);
                AddCodeName(xml.ProtectiveForest, col+7);

                var encumbrances = 0;
                foreach (var encumbrance in xml.ForestEncumbrances)
                {
                    AddCodeName(encumbrance, col+9, encumbrances);
                    encumbrances++;
                }

                AddEntry(xml.WaterObject, col+11);
                AddEntry(xml.NameOther, col+12);
                AddEntry(xml.CharOther, col+13);

                return new[] {encumbrances, 1}.Max();
            }

            /// <summary>
            /// Ширина: 19 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            /// <returns></returns>
            [MustUseReturnValue]
            private int AddSubParcel(xmlSubParcel xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Area, col + 0);
                AddEntry(xml.AreaInaccuracy, col + 1);

                var encumbrances = 0;
                foreach (var encumbrance in xml.Encumbrances)
                {
                    AddEncumbranceZu(encumbrance, 2, encumbrances);
                    encumbrances++;
                }

                AddEntry(xml.NumberRecord, col + 17);
                AddEntry(xml.DateCreated, col + 18);

                return new[] {encumbrances, 1}.Max();
            }

            /// <summary>
            /// Ширина: 15 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddEncumbranceZu(xmlEncumbranceZu xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Name, col + 0);
                AddCodeName(xml.Type, col + 1);
                AddNumberDate(xml.Registration, col + 3);
                AddDocument(xml.Document, col + 5);
                AddEntry(xml.AccountNumber, col + 13);
                AddEntry(xml.CadastralNumberRestriction, col + 14);
            }

            /// <summary>
            /// Ширина: 13 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddEncumbranceOks(xmlEncumbranceOks xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Name, col + 0);
                AddCodeName(xml.Type, col + 1);
                AddNumberDate(xml.Registration, col + 3);
                AddDocument(xml.Document, col + 5);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddNumberDate(xmlNumberDate xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Number, col + 0);
                AddEntry(xml.Date, col + 1);
            }

            /// <summary>
            /// Ширина: 3 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddCodeNameValue(xmlCodeNameValue xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Name, col + 0);
                AddEntry(xml.Code, col + 1);
                AddEntry(xml.Value, col + 2);
            }

            /// <summary>
            /// Ширина: 8 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddDocument(xmlDocument xml, int col, int rowOffset = 0)
            {
                AddCodeName(xml.CodeDocument, col + 0);
                AddEntry(xml.Name, col + 2);
                AddEntry(xml.Series, col + 3);
                AddEntry(xml.Number, col + 4);
                AddEntry(xml.Date, col + 5);
                AddEntry(xml.IssueOrgan, col + 6);
                AddEntry(xml.Desc, col + 7);
            }

            /// <summary>
            /// Ширина: 13 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddZoneAndTerritory(xmlZoneAndTerritory xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Description, col + 0);
                AddEntry(xml.CodeZoneDoc, col + 1);
                AddEntry(xml.AccountNumber, col + 2);
                AddEntry(xml.ContentRestrictions, col + 3);
                AddEntry(xml.FullPartly, col + 4);
                AddDocument(xml.Document, col + 5);
            }

            /// <summary>
            /// Ширина: 27 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddSupervisionEvent(xmlSupervisionEvent xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Agency, col + 0);
                AddCodeName(xml.EventName, col + 1);
                AddCodeName(xml.EventForm, col + 3);
                AddEntry(xml.InspectionEnd, col + 4);
                AddEntry(xml.AvailabilityViolations, col + 5);
                AddIdentifiedViolations(xml.IdentifiedViolations, col + 6);
                AddDocument(xml.DocRequisites, col + 9);
                AddElimination(xml.Elimination, col + 17);
                AddDocument(xml.EliminationDocRequisites, col + 19);
            }

            /// <summary>
            /// Ширина: 3 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddIdentifiedViolations(xmlIdentifiedViolations xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Area, col + 0);
                AddEntry(xml.TypeViolations, col + 1);
                AddEntry(xml.SignViolations, col + 2);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddElimination(xmlElimination xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.EliminationMark, col + 0);
                AddEntry(xml.EliminationAgency, col + 1);
            }

            /// <summary>
            /// Ширина: 15 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddHiredHouse(xmlHiredHouse xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.UseHiredHouse, col + 0);
                AddEntry(xml.ActBuilding, col + 1);
                AddEntry(xml.ActDevelopment, col + 2);
                AddEntry(xml.ContractBuilding, col + 3);
                AddEntry(xml.ContractDevelopment, col + 4);
                AddEntry(xml.OwnerDecision, col + 5);
                AddEntry(xml.ContractSupport, col + 6);
                AddDocument(xml.DocHiredHouse, col + 7);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddFloors(xmlFloors xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Floors, col + 0);
                AddEntry(xml.Underground_Floors, col + 1);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddYear(xmlYear xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Year_Built, col + 0);
                AddEntry(xml.Year_Used, col + 1);
            }

            /// <summary>
            /// Ширина: 16 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            /// <returns></returns>
            [MustUseReturnValue]
            private int AddSubBuildingFlat(xmlSubBuildingFlat xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Area, col + 0);

                var encumbrances = 0;
                foreach (var encumbrance in xml.EncumbrancesOks)
                {
                    AddEncumbranceOks(encumbrance, 1, encumbrances);
                    encumbrances++;
                }

                AddEntry(xml.NumberRecord, col + 14);
                AddEntry(xml.DateCreated, col + 15);

                return new[] {encumbrances, 1}.Max();
            }

            /// <summary>
            /// Ширина: 13 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddCulturalHeritage(xmlCulturalHeritage xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.EgroknRegNum, col + 0);
                AddCodeName(xml.EgroknObjCultural, col + 2);
                AddEntry(xml.EgroknNameCultural, col + 3);
                AddEntry(xml.RequirementsEnsure, col + 4);
                AddDocument(xml.Document, col + 5);
            }

            [MustUseReturnValue]
            private int AddParentOks(xmlParentOks xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.CadastralNumberOKS, col + 0);
                AddCodeName(xml.ObjectType, col + 0);
                AddCodeName(xml.AssignationBuilding, col + 0);
                AddEntry(xml.AssignationName, col + 0);

                var walls = 0;
                foreach (var wall in xml.Walls)
                {
                    AddCodeName(wall, 0, walls);
                    walls++;
                }

                AddYear(xml.Years, col + 0);
                AddFloors(xml.Floors, col + 0);

                return new[] {walls, 1}.Max();
            }

            private void AddLevel(xmlLevel xml, int col, int rowOffset = 0)
            {
                AddPos(xml.Position, col + 0);
                AddEntry(xml.Number, col + 0);
                AddCodeName(xml.Type, col + 0);
            }

            private void AddPos(xmlPos xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.NumberOnPlan, col + 0);
                AddEntry(xml.Description, col + 0);
            }

            [MustUseReturnValue]
            private int AddSubConstruction(xmlSubConstruction xml, int col, int rowOffset = 0)
            {
                AddCodeNameValue(xml.KeyParameter, col + 0);
                var encumbrances = 0;
                foreach (var encumbrance in xml.EncumbrancesOks)
                {
                    AddEncumbranceOks(encumbrance, 0, encumbrances);
                    encumbrances++;
                }

                AddEntry(xml.NumberRecord, col + 0);
                AddEntry(xml.DateCreated, col + 0);

                return new[] {encumbrances, 1}.Max();
            }

            private bool ReadConfigValue(int value)
            {
                return _config.Contains(value);
            }

            private void AddEntry(double? value, int column, int rowOffset = 0, int cols = 1, int rows = 1)
            {
                AddEntry(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), column, rowOffset, cols,
                    rows);
            }

            private void AddEntry(bool? value, int column, int rowOffset = 0, int cols = 1, int rows = 1)
            {
                AddEntry(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), column, rowOffset, cols,
                    rows);
            }

            private void AddEntry(DateTime? value, int column, int rowOffset = 0, int cols = 1, int rows = 1)
            {
                AddEntry(value.GetValueOrDefault().ToString(CultureInfo.InvariantCulture), column, rowOffset, cols,
                    rows);
            }

            [MustUseReturnValue]
            private int AddEntry(List<string> value, int column, int rowOffset = 0, int cols = 1, int rows = 1)
            {
                var counter = 0;
                foreach (var val in value)
                {
                    AddEntry(val, column, rowOffset + counter, cols, rows);
                    counter++;
                }

                return counter;
            }

            private void AddEntry(string text, int column, int rowOffset = 0, int cols = 1, int rows = 1)
            {
                if (!ReadConfigValue(_configOffset + column)) return;
                var range = _sheet.Cells
                    .GetSubrange(
                        CellRange.RowColumnToPosition(_curRow + rowOffset - 1, column - 1),
                        CellRange.RowColumnToPosition(_curRow + rowOffset + rows - 2, column + cols - 2));
                range.Value = text;
                range.Merged = true;
            }
        }
    }
}