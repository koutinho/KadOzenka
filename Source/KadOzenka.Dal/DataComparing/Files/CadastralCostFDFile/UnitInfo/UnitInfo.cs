using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using GemBox.Spreadsheet;
using ObjectModel.Directory;

namespace KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile.UnitInfo
{
	public class UnitInfo
	{
		public string UnitType { get; protected set; }
		public string Area { get; protected set; }
		public string Address { get; protected set; }
		public string Location { get; protected set; }

		public string Category { get; protected set; }
		public string Utilization { get; protected set; }

		public string Assignation { get; protected set; }


		public UnitInfo(XElement unitElement)
		{
			UnitType = unitElement.XPathSelectElement("Type")?.Value;
			Area = unitElement.XPathSelectElement("Area")?.Value;
			Address = unitElement.XPathSelectElement("Location/Note")?.Value;
			Location = unitElement.XPathSelectElement("Location/Other")?.Value;

			if (UnitType == nameof(PropertyTypes.Stead))
			{
				Category = unitElement.XPathSelectElement("Category")?.Attributes().FirstOrDefault(a => a.Name == "Name")?.Value;
				Utilization = unitElement.XPathSelectElement("Utilization")?.Attributes().FirstOrDefault(a => a.Name == "Name_doc")?.Value;
			}
			else
			{
				if (UnitType == nameof(PropertyTypes.Building))
				{
					Assignation = unitElement.XPathSelectElement("Assignation/Assignation_Building/Ass_Building")?.Value;
				}
				else if (UnitType == nameof(PropertyTypes.Pllacement))
				{
					Assignation = unitElement.XPathSelectElement("Assignation/Assignation_Flat/Ass_Flat")?.Value;
				}
				else if (UnitType == nameof(PropertyTypes.Construction)
				|| UnitType == nameof(PropertyTypes.UncompletedBuilding))
				{
					Assignation = unitElement.XPathSelectElement("Assignation/Formalized_Constr_Uncompleted")?.Value;
				}
			}
		}

		public virtual int FillExcelFile(ExcelWorksheet worksheet, int startRow, SystemType systemType)
		{
			var currentRow = startRow;
			var startCol = GetStartColumnIndex(systemType);
			SetUnitPropertyValue(worksheet, ref currentRow, startCol, "Метод расчета", "Иной", systemType);
			currentRow = FillUnitProperties(worksheet, systemType, currentRow, startCol);

			return currentRow;
		}

		private int FillUnitProperties(ExcelWorksheet worksheet, SystemType systemType, int currentRow, int startCol)
		{
			SetUnitPropertyValue(worksheet, ref currentRow, startCol, "Тип", UnitType, systemType);
			SetUnitPropertyValue(worksheet, ref currentRow, startCol, "Площадь", Area, systemType);
			SetUnitPropertyValue(worksheet, ref currentRow, startCol, "Адрес", Address, systemType);
			SetUnitPropertyValue(worksheet, ref currentRow, startCol, "Местоположение", Location, systemType);
			if (!string.IsNullOrEmpty(Category))
				SetUnitPropertyValue(worksheet, ref currentRow, startCol, "Категория земель из ГКН", Category, systemType);
			if (!string.IsNullOrEmpty(Utilization))
				SetUnitPropertyValue(worksheet, ref currentRow, startCol, "Вид использования по документам", Utilization,
					systemType);
			if (!string.IsNullOrEmpty(Assignation))
				SetUnitPropertyValue(worksheet, ref currentRow, startCol, "Вид использования по классификатору", Assignation,
					systemType);

			return currentRow;
		}

		protected void SetUnitPropertyValue(ExcelWorksheet worksheet, ref int row, int startCol, string propertyName, string propertyValue, SystemType systemType)
		{
			worksheet.Rows[row].Cells[startCol].SetValue($"{propertyName} {(systemType == SystemType.Rsm ? "РСМ" : "ПККО")}");
			worksheet.Rows[row].Cells[startCol].Style.Font.Weight = 600;
			worksheet.Rows[row].Cells[startCol + 1].SetValue(propertyValue);
			row++;
		}

		protected int GetStartColumnIndex(SystemType systemType) => systemType == SystemType.Rsm ? 0 : 2;
	}
}
