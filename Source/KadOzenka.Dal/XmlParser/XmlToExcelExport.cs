using System;
using System.Collections.Generic;
using System.Linq;
using Core.ConfigParam;
using GemBox.Spreadsheet;

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

				AddEntry(sheet, item.CadastralCost?.Value.ToString(), ReadConfigValue(valOffset + 7), 7, curRow);
				AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 8), 8, curRow);
				AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 9),
					9, curRow);
				AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 10), 10,
					curRow);
				AddEntry(sheet, item.CadastralCost?.ApplicationDate?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 11), 11, curRow);
				AddEntry(sheet, item.CadastralCost?.DocNumber, ReadConfigValue(valOffset + 12), 12, curRow);
				AddEntry(sheet, item.CadastralCost?.DocName, ReadConfigValue(valOffset + 13), 13, curRow);
				AddEntry(sheet, item.CadastralCost?.DateApproval?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 14), 14, curRow);
				AddEntry(sheet, item.CadastralCost?.RevisalStatementDate?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 15), 15, curRow);

				AddEntry(sheet, item.Area?.ToString(), ReadConfigValue(valOffset + 16), 16, curRow);

				AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 17), 17, curRow);
				AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 18), 18, curRow);
				AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 19), 19, curRow);
				//AddEntry(sheet, item.Adress.Place, ReadConfigValue(valOffset + 20), 20, curRow);
				AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 21), 21, curRow);

				AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
				AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
				AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
				AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
				AddEntry(sheet, item.Adress.Street?.Value, ReadConfigValue(valOffset + 26), 26, curRow);
				AddEntry(sheet, item.Adress.Level1?.Value, ReadConfigValue(valOffset + 27), 27, curRow);
				AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 28), 28, curRow);
				AddEntry(sheet, item.Adress.Level2?.Value, ReadConfigValue(valOffset + 29), 29, curRow);
				AddEntry(sheet, item.Adress.Apartment?.Value, ReadConfigValue(valOffset + 30), 30, curRow);


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
					AddEntry(sheet, zoneAndTerritory.FullPartly.GetValueOrDefault() ? "Да" : "Нет", ReadConfigValue(valOffset + 61), 61,
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
					AddEntry(sheet, supervision.AvailabilityViolations.GetValueOrDefault() ? "Да" : "Нет", ReadConfigValue(valOffset + 76), 76,
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

					AddEntry(sheet, supervision.Elimination.EliminationMark ? "Да" : "Нет", ReadConfigValue(valOffset + 88),
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

				int[] rowsAdded = { posGov, posCadNum, posEnc, posZone, 1 };
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

				AddEntry(sheet, item.CadastralCost?.Value.ToString(), ReadConfigValue(valOffset + 7), 7, curRow);
				AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 8), 8,
					curRow);
				AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 9),
					9,
					curRow);
				AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 10), 10,
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

				AddEntry(sheet, item.Area?.ToString(), ReadConfigValue(valOffset + 16), 16, curRow);

				AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 17), 17, curRow);
				AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 18), 18, curRow);
				AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 19), 19, curRow);
				//AddEntry(sheet, item.Adress.Place, ReadConfigValue(valOffset + 20), 20, curRow);
				AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 21), 21, curRow);

				AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
				AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
				AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
				AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
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

				int[] rowsAdded = { posWalls, posCadNum, 1 };
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

				AddEntry(sheet, item.CadastralCost?.Value.ToString(), ReadConfigValue(valOffset + 7), 7, curRow);
				AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 8), 8, curRow);
				AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 9),
					9, curRow);
				AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 10), 10,
					curRow);
				AddEntry(sheet, item.CadastralCost?.ApplicationDate?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 11), 11, curRow);
				AddEntry(sheet, item.CadastralCost?.DocNumber, ReadConfigValue(valOffset + 12), 12, curRow);
				AddEntry(sheet, item.CadastralCost?.DocName, ReadConfigValue(valOffset + 13), 13, curRow);
				AddEntry(sheet, item.CadastralCost?.DateApproval?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 14), 14, curRow);
				AddEntry(sheet, item.CadastralCost?.RevisalStatementDate?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 15), 15, curRow);


				AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 16), 16, curRow);
				AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 17), 17, curRow);
				AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 18), 18, curRow);
				//AddEntry(sheet, item.Adress.Place, ReadConfigValue(valOffset + 19), 19, curRow);
				AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 20), 20, curRow);

				AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 21), 21, curRow);
				AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
				AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
				AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
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

				int[] rowsAdded = { posCadNum, posParams, 1 };
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

				AddEntry(sheet, item.CadastralCost?.Value.ToString(), ReadConfigValue(valOffset + 7), 7, curRow);
				AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 8), 8, curRow);
				AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 9),
					9, curRow);
				AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 10), 10,
					curRow);
				AddEntry(sheet, item.CadastralCost?.ApplicationDate?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 11), 11, curRow);
				AddEntry(sheet, item.CadastralCost?.DocNumber, ReadConfigValue(valOffset + 12), 12, curRow);
				AddEntry(sheet, item.CadastralCost?.DocName, ReadConfigValue(valOffset + 13), 13, curRow);
				AddEntry(sheet, item.CadastralCost?.DateApproval?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 14), 14, curRow);
				AddEntry(sheet, item.CadastralCost?.RevisalStatementDate?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 15), 15, curRow);

				AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 16), 16, curRow);
				AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 17), 17, curRow);
				AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 18), 18, curRow);
				//AddEntry(sheet, item.Adress.Place, ReadConfigValue(valOffset + 19), 19, curRow);
				AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 20), 20, curRow);

				AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 21), 21, curRow);
				AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
				AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
				AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
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

				int[] rowsAdded = { posCadNum, posParams, 1 };
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

				AddEntry(sheet, item.CadastralCost?.Value.ToString(), ReadConfigValue(valOffset + 7), 7, curRow);
				AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 8), 8, curRow);
				AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 9),
					9, curRow);
				AddEntry(sheet, item.CadastralCost?.DocDate?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 10), 10,
					curRow);
				AddEntry(sheet, item.CadastralCost?.ApplicationDate?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 11), 11, curRow);
				AddEntry(sheet, item.CadastralCost?.DocNumber, ReadConfigValue(valOffset + 12), 12, curRow);
				AddEntry(sheet, item.CadastralCost?.DocName, ReadConfigValue(valOffset + 13), 13, curRow);
				AddEntry(sheet, item.CadastralCost?.DateApproval?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 14), 14, curRow);
				AddEntry(sheet, item.CadastralCost?.RevisalStatementDate?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 15), 15, curRow);

				AddEntry(sheet, item.Area?.ToString(), ReadConfigValue(valOffset + 16), 16, curRow);

				AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 17), 17, curRow);
				AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 18), 18, curRow);
				AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 19), 19, curRow);
				//AddEntry(sheet, item.Adress.Place, ReadConfigValue(valOffset + 20), 20, curRow);
				AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 21), 21, curRow);

				AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
				AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
				AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
				AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 25), 25, curRow);
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

				int[] rowsAdded = { posRows, 1 };
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

				AddEntry(sheet, item.CadastralCost?.Value.ToString(), ReadConfigValue(valOffset + 6), 6, curRow);
				AddEntry(sheet, item.CadastralCost?.DateValuation?.ToString("dd/MM/yyyy"),
					ReadConfigValue(valOffset + 7), 7, curRow);
				AddEntry(sheet, item.CadastralCost?.DateEntering?.ToString("dd/MM/yyyy"), ReadConfigValue(valOffset + 8),
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

				AddEntry(sheet, item.Area?.ToString(), ReadConfigValue(valOffset + 15), 15, curRow);

				AddEntry(sheet, item.Adress.KLADR, ReadConfigValue(valOffset + 16), 16, curRow);
				AddEntry(sheet, item.Adress.PostalCode, ReadConfigValue(valOffset + 17), 17, curRow);
				AddEntry(sheet, item.Adress.Region, ReadConfigValue(valOffset + 18), 18, curRow);
				//AddEntry(sheet, item.Adress.Place, ReadConfigValue(valOffset + 19), 19, curRow);
				AddEntry(sheet, item.Adress.Other, ReadConfigValue(valOffset + 20), 20, curRow);

				AddEntry(sheet, item.Adress.District?.Value, ReadConfigValue(valOffset + 21), 21, curRow);
				AddEntry(sheet, item.Adress.Locality?.Value, ReadConfigValue(valOffset + 22), 22, curRow);
				AddEntry(sheet, item.Adress.City?.Value, ReadConfigValue(valOffset + 23), 23, curRow);
				AddEntry(sheet, item.Adress.UrbanDistrict?.Value, ReadConfigValue(valOffset + 24), 24, curRow);
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
				int[] rowsAdded = { 1 };
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
                    column.SetWidth(150,LengthUnit.Pixel);
                }
            }
        }
    }
}