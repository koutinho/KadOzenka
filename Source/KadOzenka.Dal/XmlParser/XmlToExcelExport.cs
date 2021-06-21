using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.ConfigParam;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using JetBrains.Annotations;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;
using Microsoft.Practices.ObjectBuilder2;

namespace KadOzenka.Dal.XmlParser
{
    public class XmlToExcelExport
    {
        private const int SINGLE_FILE_OBJECTS_LIMIT = 400_000;
        private readonly xmlObjectList _allObjects;
        private readonly int[] _config;

        private readonly Dictionary<int, string> _listNames = new Dictionary<int, string>
        {
            {1, "ЗУ"},
            {2, "Здания"},
            {3, "Сооружения"},
            {4, "ОНС"},
            {5, "Помещения"},
            {6, "Машино-места"}
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
                _resultExcelFiles.Add("Все объекты", excelFile);
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
            var valOffset = 0;
            sheet.Cells.Style.WrapText = true;
            var converter = new XmlToExcelExportWorker(sheet, _config, 5, valOffset);

            foreach (var parcel in parcels)
            {
                converter.AddParcel(parcel, 0);
            }

            RemoveUncheckedColumns(sheet, valOffset, 158);
        }

        private void AddBuildings(ExcelWorksheet sheet, List<xmlObjectBuild> buildings)
        {
            var valOffset = 200;
            sheet.Cells.Style.WrapText = true;

            var converter = new XmlToExcelExportWorker(sheet, _config, 5, valOffset);
            foreach (var building in buildings)
            {
                converter.AddBuild(building, 0);
            }

            RemoveUncheckedColumns(sheet, valOffset, 90);
        }

        private void AddConstructions(ExcelWorksheet sheet, List<xmlObjectConstruction> constructions)
        {
            var valOffset = 300;
            sheet.Cells.Style.WrapText = true;

            var converter = new XmlToExcelExportWorker(sheet, _config, 5, valOffset);
            foreach (var building in constructions)
            {
                converter.AddConstruction(building, 0);
            }

            RemoveUncheckedColumns(sheet, valOffset, 91);
        }

        private void AddIncompleteBuildings(ExcelWorksheet sheet, List<xmlObjectUncomplited> incompletedBuildings)
        {
            var valOffset = 400;
            sheet.Cells.Style.WrapText = true;

            var converter = new XmlToExcelExportWorker(sheet, _config, 3, valOffset);
            foreach (var building in incompletedBuildings)
            {
                converter.AddUncomplited(building, 0);
            }

            RemoveUncheckedColumns(sheet, valOffset, 52);
        }

        private void AddFlats(ExcelWorksheet sheet, List<xmlObjectFlat> flats)
        {
            var valOffset = 500;
            sheet.Cells.Style.WrapText = true;
            var curRow = 5;

            var converter = new XmlToExcelExportWorker(sheet, _config, 5, valOffset);
            foreach (var flat in flats)
            {
                converter.AddFlat(flat, 0);
            }

            RemoveUncheckedColumns(sheet, valOffset, 108);
        }

        private void AddCarPlaces(ExcelWorksheet sheet, List<xmlObjectCarPlace> carPlaces)
        {
            var valOffset = 700;
            sheet.Cells.Style.WrapText = true;
            var curRow = 3;
            var converter = new XmlToExcelExportWorker(sheet, _config, 5, valOffset);
            foreach (var carPlace in carPlaces)
            {
                converter.AddCarPlace(carPlace, 0);
            }

            RemoveUncheckedColumns(sheet, valOffset, 66);
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

        public class XmlToExcelExportWorker
        {
            private readonly ExcelWorksheet _sheet;
            private int _curRow;
            private readonly int _configOffset;
            private readonly int[] _config;

            public XmlToExcelExportWorker(ExcelWorksheet sheet, int[] config, int initialRow, int configOffset)
            {
                _sheet = sheet;
                _config = config;
                _curRow = initialRow;
                _configOffset = configOffset;
            }

            /// <summary>
            /// Ширина: 44 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            private void AddParticular(xmlObjectParticular xml, int col)
            {
                AddEntry(xml.CadastralNumber, col + 0);
                AddEntry(xml.TypeObject.GetEnumDescription(), col + 1);
                AddEntry(xml.TypeRealty, col + 2);
                AddEntry(xml.DateCreate, col + 3);
                AddEntry(xml.CadastralNumberBlock, col + 4);
                AddCost(xml.CadastralCost, col + 5);
                AddAddress(xml.Adress, col + 15);
                AddEntry(xml.AssessmentDate, col + 42);
                AddEntry(xml.Area, col + 43);
            }

            public void AddParcel(xmlObjectParcel xml, int col)
            {
                var columnsTotal = 158;

                AddParticular(xml, col + 0);
                var innerCadNumbers = AddEntry(xml.InnerCadastralNumbers, col + 44);
                AddEntry(xml.Area, col + 45);
                AddEntry(xml.AreaInaccuracy, col + 46);
                AddCodeName(xml.Name, col + 47);
                AddCodeName(xml.Category, col + 49);
                AddUtilization(xml.Utilization, col + 51);

                var naturalObjects = 0;
                foreach (var natObject in xml.NaturalObjects)
                {
                    var natObjCount = AddNaturalObject(natObject, col + 57, naturalObjects);
                    naturalObjects += natObjCount;
                }

                var subParcels = 0;
                foreach (var subParcel in xml.SubParcels)
                {
                    var subParcelCount = AddSubParcel(subParcel, col + 71, subParcels);
                    subParcels += subParcelCount;
                }

                AddEntry(xml.FacilityCadastralNumber, col + 90);
                AddEntry(xml.FacilityPurpose, col + 91);

                var zonesAndTerritories = 0;
                foreach (var zones in xml.ZonesAndTerritories)
                {
                    AddZoneAndTerritory(zones, col + 92, zonesAndTerritories);
                    zonesAndTerritories++;
                }

                var governmentLandSupervision = 0;
                foreach (var supervision in xml.GovernmentLandSupervision)
                {
                    AddSupervisionEvent(supervision, col + 105, governmentLandSupervision);
                    governmentLandSupervision++;
                }

                AddEntry(xml.SurveyingProjectNum, col + 133);
                AddDocument(xml.SurveyingProjectDecisionRequisites, col + 134);
                AddHiredHouse(xml.HiredHouse, col + 142);
                AddEntry(xml.LimitedCirculation, col + 157);

                int[] rowsAdded =
                    {innerCadNumbers, naturalObjects, subParcels, zonesAndTerritories, governmentLandSupervision, 1};

                var rowsToAdvance = RowsToAdvance(rowsAdded, columnsTotal);

                _curRow += rowsToAdvance;

                DrawBorder(columnsTotal);
            }

            public void AddBuild(xmlObjectBuild xml, int col)
            {
                var columnsTotal = 90;
                AddParticular(xml, col + 0);
                var parentCadNumbers = AddEntry(xml.ParentCadastralNumbers, col + 44);
                AddEntry(xml.Area, col + 45);
                AddCodeName(xml.AssignationBuilding, col + 46);
                AddFloors(xml.Floors, col + 48);
                AddYear(xml.Years, col + 50);
                AddEntry(xml.Name, col + 52);

                var walls = 0;
                foreach (var wall in xml.Walls)
                {
                    AddCodeName(wall, col + 53, walls);
                    walls++;
                }

                var permittedUses = AddEntry(xml.ObjectPermittedUses, col + 55);

                var subBuildings = 0;
                foreach (var subBuildingFlat in xml.SubBuildings)
                {
                    var subBuildingCount = AddSubBuildingFlat(subBuildingFlat, col + 56, subBuildings);
                    subBuildings += subBuildingCount;
                }

                var flatsCadNumbers = AddEntry(xml.FlatsCadastralNumbers, col + 72);
                var carSpacesCadNumbers = AddEntry(xml.CarParkingSpacesCadastralNumbers, col + 73);
                var unitedCadNumbers = AddEntry(xml.UnitedCadastralNumbers, col + 74);
                AddEntry(xml.FacilityCadastralNumber, col + 75);
                AddEntry(xml.FacilityPurpose, col + 76);
                AddCulturalHeritage(xml.CulturalHeritage, col + 77);

                int[] rowsAdded =
                {
                    parentCadNumbers, walls, permittedUses, subBuildings,
                    flatsCadNumbers, carSpacesCadNumbers, unitedCadNumbers, 1
                };

                var rowsToAdvance = RowsToAdvance(rowsAdded, columnsTotal);

                _curRow += rowsToAdvance;

                DrawBorder(columnsTotal);
            }

            public void AddConstruction(xmlObjectConstruction xml, int col)
            {
                var columnsTotal = 92;
                AddParticular(xml, col + 0);
                var parentCadNumbers = AddEntry(xml.ParentCadastralNumbers, col + 44);
                AddEntry(xml.AssignationName, col + 45);
                AddFloors(xml.Floors, col + 46);
                AddYear(xml.Years, col + 48);
                AddEntry(xml.Name, col + 50);

                var keyParameters = 0;
                foreach (var param in xml.KeyParameters)
                {
                    AddCodeNameValue(param, col + 51, keyParameters);
                    keyParameters++;
                }

                var permittedUses = AddEntry(xml.ObjectPermittedUses, col + 54);

                var subConstructions = 0;
                foreach (var subConstruction in xml.SubConstructions)
                {
                    var constructionCount = AddSubConstruction(subConstruction, col + 55, subConstructions);
                    subConstructions += constructionCount;
                }

                var flatsCadNumbers = AddEntry(xml.FlatsCadastralNumbers, col + 73);
                var carSpacesCadNumbers = AddEntry(xml.CarParkingSpacesCadastralNumbers, col + 74);
                var unitedCadNumbers = AddEntry(xml.UnitedCadastralNumbers, col + 75);
                AddEntry(xml.FacilityCadastralNumber, col + 76);
                AddEntry(xml.FacilityPurpose, col + 77);
                AddCulturalHeritage(xml.CulturalHeritage, col + 78);

                int[] rowsAdded = {
                    parentCadNumbers, keyParameters, permittedUses, subConstructions,
                    flatsCadNumbers, carSpacesCadNumbers, unitedCadNumbers, 1
                };

                var rowsToAdvance = RowsToAdvance(rowsAdded, columnsTotal);

                _curRow += rowsToAdvance;

                DrawBorder(columnsTotal);
            }

            public void AddUncomplited(xmlObjectUncomplited xml, int col)
            {
                var columnsTotal = 52;

                AddParticular(xml, col + 0);
                var parentCadNumbers = AddEntry(xml.ParentCadastralNumbers, col + 44);
                AddEntry(xml.AssignationName, col + 45);
                AddEntry(xml.DegreeReadiness, col + 46);
                var keyParameters = 0;
                foreach (var param in xml.KeyParameters)
                {
                    AddCodeNameValue(param, col + 47, keyParameters);
                    keyParameters++;
                }

                AddEntry(xml.FacilityCadastralNumber, col + 50);
                AddEntry(xml.FacilityPurpose, col + 51);

                int[] rowsAdded = {parentCadNumbers, keyParameters, 1};
                var rowsToAdvance = RowsToAdvance(rowsAdded, columnsTotal);

                _curRow += rowsToAdvance;

                DrawBorder(columnsTotal);
            }

            public void AddFlat(xmlObjectFlat xml, int col)
            {
                var columnsTotal = 108;

                AddParticular(xml,col + 0);
                AddEntry(xml.CadastralNumberFlat, col + 44);
                AddEntry(xml.CadastralNumberOKS, col + 45);
                var parentOks = AddParentOks(xml.ParentOks, col + 46);
                AddEntry(xml.Name, col + 58);
                AddEntry(xml.Area, col + 59);
                AddPos(xml.Position, col + 60);

                var levelCounter = 0;
                foreach (var level in xml.Levels)
                {
                    AddLevel(level, col + 62, levelCounter);
                    levelCounter++;
                }

                AddCodeName(xml.AssignationFlatCode, col + 67);
                AddCodeName(xml.AssignationFlatType, col + 69);
                AddCodeName(xml.AssignationSpecialType, col + 71);
                AddEntry(xml.AssignationTotalAssets, col + 73);
                AddEntry(xml.AssignationAuxiliaryFlat, col + 74);
                var permittedUses = AddEntry(xml.ObjectPermittedUses, col + 75);

                var subFlats = 0;
                foreach (var buildingFlat in xml.SubFlats)
                {
                    var flatsCount = AddSubBuildingFlat(buildingFlat, col + 76, subFlats);
                    subFlats += flatsCount;
                }

                var unitedCadNumbers = AddEntry(xml.UnitedCadastralNumbers, col + 92);
                AddEntry(xml.FacilityCadastralNumber, col + 93);
                AddEntry(xml.FacilityPurpose, col + 94);
                AddCulturalHeritage(xml.CulturalHeritage, col + 95);

                int[] rowsAdded = {levelCounter, permittedUses, subFlats, unitedCadNumbers, parentOks, 1};
                var rowsToAdvance = RowsToAdvance(rowsAdded, columnsTotal);

                _curRow += rowsToAdvance;

                DrawBorder(columnsTotal);
            }

            public void AddCarPlace(xmlObjectCarPlace xml, int col)
            {
                var columnsTotal = 66;
                AddParticular(xml, col + 0);
                AddEntry(xml.Area, col + 44);
                AddEntry(xml.CadastralNumberOKS, col + 45);
                var parentOks = AddParentOks(xml.ParentOks, col + 46);
                AddLevel(xml.PositionInObject, col + 58);
                var unitedCadNumbers = AddEntry(xml.UnitedCadastralNumbers, col + 63);
                AddEntry(xml.FacilityCadastralNumber, col + 64);
                AddEntry(xml.FacilityPurpose, col + 65);

                int[] rowsAdded = {unitedCadNumbers, parentOks, 1};
                var rowsToAdvance = RowsToAdvance(rowsAdded, columnsTotal);

                _curRow += rowsToAdvance;

                DrawBorder(columnsTotal);
            }

            private int RowsToAdvance(int[] rowsAdded, int columnsToCheck)
            {
                bool hasValue = true;
                int rowsToAdvance = 1;
                for (var i = rowsAdded.Max(); i > 1 && hasValue; i--)
                {
                    var range = _sheet.Cells
                        .GetSubrange(
                            CellRange.RowColumnToPosition(_curRow + i, 1),
                            CellRange.RowColumnToPosition(_curRow + i, columnsToCheck));
                    hasValue = range.Any(x => x.Value != null && !x.Value.ToString().IsNullOrEmpty());
                    rowsToAdvance = 1 + rowsAdded.Max() - i;
                }

                return rowsToAdvance;
            }

            /// <summary>
            /// Ширина: 10 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddCost(xmlCost xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Value, col + 0, rowOffset);
                AddEntry(xml.DateValuation, col + 1, rowOffset);
                AddEntry(xml.DateEntering, col + 2, rowOffset);
                AddEntry(xml.DateApproval, col + 3, rowOffset);
                AddEntry(xml.DocNumber, col + 4, rowOffset);
                AddEntry(xml.DocDate, col + 5, rowOffset);
                AddEntry(xml.ApplicationDate, col + 6, rowOffset);
                AddEntry(xml.RevisalStatementDate, col + 7, rowOffset);
                AddEntry(xml.ApplicationLastDate, col + 8, rowOffset);
                AddEntry(xml.DocName, col + 9, rowOffset);
            }

            /// <summary>
            /// Ширина: 27 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddAddress(xmlAdress xml, int col, int rowOffset = 0)
            {
                // Для ОКС
                AddEntry(xml.AddressStr, col + 0, rowOffset);
                AddEntry(xml.FIAS, col + 1, rowOffset);
                AddEntry(xml.OKATO, col + 2, rowOffset);
                AddEntry(xml.KLADR, col + 3, rowOffset);
                AddEntry(xml.OKTMO, col + 4, rowOffset);
                AddEntry(xml.PostalCode, col + 5, rowOffset);
                AddEntry(xml.RussianFederation, col + 6, rowOffset);
                AddEntry(xml.Region, col + 7, rowOffset);
                AddEntry(xml.Note, col + 8, rowOffset);
                AddEntry(xml.Other, col + 9, rowOffset);
                AddEntry(xml.AddressOrLocation, col + 10, rowOffset);
                AddAddressLevel(xml.District, col + 11, rowOffset);
                AddAddressLevel(xml.City, col + 12, rowOffset);
                AddAddressLevel(xml.UrbanDistrict, col + 13, rowOffset);
                AddAddressLevel(xml.SovietVillage, col + 14, rowOffset);
                AddAddressLevel(xml.Locality, col + 15, rowOffset);
                AddAddressLevel(xml.PlanningElement, col + 16, rowOffset);
                AddAddressLevel(xml.Street, col + 17, rowOffset);
                AddAddressLevel(xml.Level1, col + 18, rowOffset);
                AddAddressLevel(xml.Level2, col + 19, rowOffset);
                AddAddressLevel(xml.Level3, col + 20, rowOffset);
                AddAddressLevel(xml.Apartment, col + 21, rowOffset);
                // Для ЗУ
                AddEntry(xml.InBounds, col + 22, rowOffset);
                AddEntry(xml.Placed, col + 23, rowOffset);
                AddElaborationLocation(xml.Elaboration, col + 24, rowOffset);
            }

            /// <summary>
            /// Ширина: 1 колонка, игнорируем тип во второй колонке
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddAddressLevel(xmlAdresLevel xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Value, col + 0, rowOffset);
                //AddEntry(xml.Type,col + 0, rowOffset);
            }

            /// <summary>
            /// Ширина: 3 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddElaborationLocation(xmlElaborationLocation xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.ReferenceMark, col + 0, rowOffset);
                AddEntry(xml.Distance, col + 1, rowOffset);
                AddEntry(xml.Direction, col + 2, rowOffset);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddCodeName(xmlCodeName xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Name, col + 0, rowOffset);
                AddEntry(xml.Code, col + 1, rowOffset);
            }

            /// <summary>
            /// Ширина: 6 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddUtilization(xmlUtilization xml, int col, int rowOffset = 0)
            {
                AddCodeName(xml.Utilization, col + 0);
                AddCodeName(xml.LandUse, col + 2, rowOffset);
                AddEntry(xml.ByDoc, col + 4, rowOffset);
                AddEntry(xml.PermittedUseText, col + 5, rowOffset);
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
                AddCodeName(xml.Kind, col+0, rowOffset);
                AddEntry(xml.Forestry, col+2, rowOffset);
                AddCodeName(xml.ForestUse, col+3, rowOffset);
                AddEntry(xml.QuarterNumbers, col+5, rowOffset);
                AddEntry(xml.TaxationSeparations, col+6, rowOffset);
                AddCodeName(xml.ProtectiveForest, col+7, rowOffset);

                var encumbrances = 0;
                foreach (var encumbrance in xml.ForestEncumbrances)
                {
                    AddCodeName(encumbrance, col+9, rowOffset + encumbrances);
                    encumbrances++;
                }

                AddEntry(xml.WaterObject, col+11, rowOffset);
                AddEntry(xml.NameOther, col+12, rowOffset);
                AddEntry(xml.CharOther, col+13, rowOffset);

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
                AddEntry(xml.Area, col + 0, rowOffset);
                AddEntry(xml.AreaInaccuracy, col + 1, rowOffset);

                var encumbrances = 0;
                foreach (var encumbrance in xml.Encumbrances)
                {
                    AddEncumbranceZu(encumbrance, col + 2, rowOffset + encumbrances);
                    encumbrances++;
                }

                AddEntry(xml.NumberRecord, col + 17, rowOffset);
                AddEntry(xml.DateCreated, col + 18, rowOffset);

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
                AddEntry(xml.Name, col + 0, rowOffset);
                AddCodeName(xml.Type, col + 1, rowOffset);
                AddNumberDate(xml.Registration, col + 3, rowOffset);
                AddDocument(xml.Document, col + 5, rowOffset);
                AddEntry(xml.AccountNumber, col + 13, rowOffset);
                AddEntry(xml.CadastralNumberRestriction, col + 14, rowOffset);
            }

            /// <summary>
            /// Ширина: 13 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddEncumbranceOks(xmlEncumbranceOks xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Name, col + 0, rowOffset);
                AddCodeName(xml.Type, col + 1, rowOffset);
                AddNumberDate(xml.Registration, col + 3, rowOffset);
                AddDocument(xml.Document, col + 5, rowOffset);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddNumberDate(xmlNumberDate xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Number, col + 0, rowOffset);
                AddEntry(xml.Date, col + 1, rowOffset);
            }

            /// <summary>
            /// Ширина: 3 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddCodeNameValue(xmlCodeNameValue xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Name, col + 0, rowOffset);
                AddEntry(xml.Code, col + 1, rowOffset);
                AddEntry(xml.Value, col + 2, rowOffset);
            }

            /// <summary>
            /// Ширина: 8 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddDocument(xmlDocument xml, int col, int rowOffset = 0)
            {
                AddCodeName(xml.CodeDocument, col + 0, rowOffset);
                AddEntry(xml.Name, col + 2, rowOffset);
                AddEntry(xml.Series, col + 3, rowOffset);
                AddEntry(xml.Number, col + 4, rowOffset);
                AddEntry(xml.Date, col + 5, rowOffset);
                AddEntry(xml.IssueOrgan, col + 6, rowOffset);
                AddEntry(xml.Desc, col + 7, rowOffset);
            }

            /// <summary>
            /// Ширина: 13 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddZoneAndTerritory(xmlZoneAndTerritory xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Description, col + 0, rowOffset);
                AddEntry(xml.CodeZoneDoc, col + 1, rowOffset);
                AddEntry(xml.AccountNumber, col + 2, rowOffset);
                AddEntry(xml.ContentRestrictions, col + 3, rowOffset);
                AddEntry(xml.FullPartly, col + 4, rowOffset);
                AddDocument(xml.Document, col + 5, rowOffset);
            }

            /// <summary>
            /// Ширина: 28 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddSupervisionEvent(xmlSupervisionEvent xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Agency, col + 0, rowOffset);
                AddCodeName(xml.EventName, col + 1, rowOffset);
                AddCodeName(xml.EventForm, col + 3, rowOffset);
                AddEntry(xml.InspectionEnd, col + 5, rowOffset);
                AddEntry(xml.AvailabilityViolations, col + 6, rowOffset);
                AddIdentifiedViolations(xml.IdentifiedViolations, col + 7, rowOffset);
                AddDocument(xml.DocRequisites, col + 10, rowOffset);
                AddElimination(xml.Elimination, col + 18, rowOffset);
                AddDocument(xml.EliminationDocRequisites, col + 20, rowOffset);
            }

            /// <summary>
            /// Ширина: 3 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddIdentifiedViolations(xmlIdentifiedViolations xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Area, col + 0, rowOffset);
                AddEntry(xml.TypeViolations, col + 1, rowOffset);
                AddEntry(xml.SignViolations, col + 2, rowOffset);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddElimination(xmlElimination xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.EliminationMark, col + 0, rowOffset);
                AddEntry(xml.EliminationAgency, col + 1, rowOffset);
            }

            /// <summary>
            /// Ширина: 15 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddHiredHouse(xmlHiredHouse xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.UseHiredHouse, col + 0, rowOffset);
                AddEntry(xml.ActBuilding, col + 1, rowOffset);
                AddEntry(xml.ActDevelopment, col + 2, rowOffset);
                AddEntry(xml.ContractBuilding, col + 3, rowOffset);
                AddEntry(xml.ContractDevelopment, col + 4, rowOffset);
                AddEntry(xml.OwnerDecision, col + 5, rowOffset);
                AddEntry(xml.ContractSupport, col + 6, rowOffset);
                AddDocument(xml.DocHiredHouse, col + 7, rowOffset);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddFloors(xmlFloors xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Floors, col + 0, rowOffset);
                AddEntry(xml.Underground_Floors, col + 1, rowOffset);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddYear(xmlYear xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.Year_Built, col + 0, rowOffset);
                AddEntry(xml.Year_Used, col + 1, rowOffset);
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
                AddEntry(xml.Area, col + 0, rowOffset);

                var encumbrances = 0;
                foreach (var encumbrance in xml.EncumbrancesOks)
                {
                    AddEncumbranceOks(encumbrance, 1, rowOffset + encumbrances);
                    encumbrances++;
                }

                AddEntry(xml.NumberRecord, col + 14, rowOffset);
                AddEntry(xml.DateCreated, col + 15, rowOffset);

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
                AddEntry(xml.EgroknRegNum, col + 0, rowOffset);
                AddCodeName(xml.EgroknObjCultural, col + 2, rowOffset);
                AddEntry(xml.EgroknNameCultural, col + 3, rowOffset);
                AddEntry(xml.RequirementsEnsure, col + 4, rowOffset);
                AddDocument(xml.Document, col + 5, rowOffset);
            }

            /// <summary>
            /// Ширина: 12 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            /// <returns></returns>
            [MustUseReturnValue]
            private int AddParentOks(xmlParentOks xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.CadastralNumberOKS, col + 0, rowOffset);
                AddCodeName(xml.ObjectType, col + 1, rowOffset);
                AddCodeName(xml.AssignationBuilding, col + 3, rowOffset);
                AddEntry(xml.AssignationName, col + 5, rowOffset);

                var walls = 0;
                foreach (var wall in xml.Walls)
                {
                    AddCodeName(wall, col + 6, walls);
                    walls++;
                }

                AddYear(xml.Years, col + 8, rowOffset);
                AddFloors(xml.Floors, col + 10, rowOffset);

                return new[] {walls, 1}.Max();
            }

            /// <summary>
            /// Ширина: 5 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddLevel(xmlLevel xml, int col, int rowOffset = 0)
            {
                AddPos(xml.Position, col + 0, rowOffset);
                AddEntry(xml.Number, col + 2, rowOffset);
                AddCodeName(xml.Type, col + 3, rowOffset);
            }

            /// <summary>
            /// Ширина: 2 колонки
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            private void AddPos(xmlPos xml, int col, int rowOffset = 0)
            {
                AddEntry(xml.NumberOnPlan, col + 0, rowOffset);
                AddEntry(xml.Description, col + 1, rowOffset);
            }

            /// <summary>
            /// Ширина: 18 колонок
            /// </summary>
            /// <param name="xml"></param>
            /// <param name="col"></param>
            /// <param name="rowOffset"></param>
            /// <returns></returns>
            [MustUseReturnValue]
            private int AddSubConstruction(xmlSubConstruction xml, int col, int rowOffset = 0)
            {
                AddCodeNameValue(xml.KeyParameter, col + 0, rowOffset);
                var encumbrances = 0;
                foreach (var encumbrance in xml.EncumbrancesOks)
                {
                    AddEncumbranceOks(encumbrance, col + 3, rowOffset + encumbrances);
                    encumbrances++;
                }

                AddEntry(xml.NumberRecord, col + 16, rowOffset);
                AddEntry(xml.DateCreated, col + 17, rowOffset);

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
                var textVal = value switch
                {
                    true => "Да",
                    false => "Нет",
                    null => ""
                };
                AddEntry(textVal, column, rowOffset, cols, rows);
            }

            private void AddEntry(DateTime? value, int column, int rowOffset = 0, int cols = 1, int rows = 1)
            {
                AddEntry(value?.ToString("dd/MM/yyyy"), column, rowOffset, cols, rows);
            }

            [MustUseReturnValue]
            private int AddEntry(IEnumerable<string> value, int column, int rowOffset = 0, int cols = 1, int rows = 1)
            {
                var resultValue = value.JoinStrings(";");
                AddEntry(resultValue, column, rowOffset, cols, rows);
                return 1;
            }

            private void AddEntry(string text, int column, int rowOffset = 0, int cols = 1, int rows = 1)
            {
                if (!ReadConfigValue(_configOffset + column + 1)) return;
                var range = _sheet.Cells
                    .GetSubrange(
                        CellRange.RowColumnToPosition(_curRow + rowOffset, column),
                        CellRange.RowColumnToPosition(_curRow + rowOffset + rows - 1, column + cols - 1));
                range.Value = text;
                range.Merged = true;
            }

            private void DrawBorder(int widthInColumns)
            {
                for (var col = 0; col < widthInColumns; col++)
                    _sheet.Cells[_curRow, col].Style.Borders[IndividualBorder.Top].LineStyle = LineStyle.Thin;
            }
        }
    }
}