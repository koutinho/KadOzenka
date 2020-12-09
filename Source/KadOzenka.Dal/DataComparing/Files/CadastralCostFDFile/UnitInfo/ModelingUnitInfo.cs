using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile.GroupInfo;

namespace KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile.UnitInfo
{
	public class ModelingUnitInfo : UnitInfo
	{
		public string AlgoritmType { get; protected set; }
		public List<FactorValue> UnitEvaluationFactorValues { get; protected set; }

		public ModelingUnitInfo(XElement unitElement, string algoritmType, Dictionary<string, EvaluativeFactorInfo> groupFactorsInfo) : base(unitElement)
		{
			AlgoritmType = algoritmType;
			UnitEvaluationFactorValues = new List<FactorValue>();

			var unitFactors = unitElement.XPathSelectElements("Evaluative_Factors/Evaluative_Factor");
			foreach (var unitFactor in unitFactors)
			{
				var factorId = unitFactor.Attributes().FirstOrDefault(a => a.Name == "ID_Factor")?.Value;
				if (!string.IsNullOrEmpty(factorId))
				{
					var factorInfo = groupFactorsInfo[factorId];
					var factorName = factorInfo.FactorName;
					string factorValue = null, usedValue = null;
					if (factorInfo.IsSignMarket)
					{
						var quantitativeId = unitFactor.XPathSelectElement("Qualitative_Id")?.Value;
						if (quantitativeId != null)
						{
							factorValue = factorInfo.Qualitatives[quantitativeId].Value;
							usedValue = factorInfo.Qualitatives[quantitativeId].Dimension;
						}
					}
					else
					{
						factorValue = unitFactor.XPathSelectElement("Quantitative_Value")?.Value;
					}

					var factor = new FactorValue(factorName, factorInfo.IsSignMarket, factorValue, usedValue);
					UnitEvaluationFactorValues.Add(factor);
				}
			}
		}

		public override int FillExcelFile(ExcelWorksheet worksheet, int startRow, SystemType systemType)
		{
			var currentRow = startRow;
			var startCol = GetStartColumnIndex(systemType);

			SetUnitPropertyValue(worksheet, ref currentRow, startCol, "Метод расчета", "Моделирование", systemType);
			SetUnitPropertyValue(worksheet, ref currentRow, startCol, "Алгоритм расчета", AlgoritmType, systemType);

			worksheet.Rows[currentRow].Cells[startCol].SetValue($"Факторы {(systemType == SystemType.Rsm ? "РСМ" : "ПККО")}");
			worksheet.Rows[currentRow].Cells[startCol].Style.Font.Weight = 600;
			worksheet.Rows[currentRow].Cells[startCol + 1].SetValue($"Значения Факторов {(systemType == SystemType.Rsm ? "РСМ" : "ПККО")}");
			worksheet.Rows[currentRow].Cells[startCol + 1].Style.Font.Weight = 600;
			currentRow++;

			foreach (var unitEvaluationFactorValue in UnitEvaluationFactorValues)
			{
				worksheet.Rows[currentRow].Cells[startCol].SetValue(unitEvaluationFactorValue.FactorName);
				var value = unitEvaluationFactorValue.IsSignMarket 
					? $"{unitEvaluationFactorValue.Value} (подставляемое значение: {unitEvaluationFactorValue.UsedValue})"
					: unitEvaluationFactorValue.Value;
				worksheet.Rows[currentRow].Cells[startCol + 1].SetValue(value);
				currentRow++;
			}

			return currentRow;
		}

	}
}
