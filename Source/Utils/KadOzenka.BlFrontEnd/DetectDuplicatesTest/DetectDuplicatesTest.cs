﻿using System;
using GemBox.Spreadsheet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Market;
using Core.Shared.Extensions;

namespace KadOzenka.BlFrontEnd.DetectDuplicatesTest
{
	public class DetectDuplicatesTest
	{
		public string SourceFilePath => @"C:\TFSProjects\CIPJS\KadOzenka\Source\Игра в дубли.xlsx";
		public string ResultFilePath => @"C:\TFSProjects\CIPJS\KadOzenka\Source\Игра в дубли результат.xlsx";

		public void Test()
		{
			ClearTestTable();
			ImportDataToTestTable();
			PerformDuplicatesSearch();
			ExportResult();
		}

		private void ClearTestTable()
		{
			var command = DBMngr.Main.GetSqlStringCommand(@"delete from market_core_object_test;");
			DBMngr.Main.ExecuteNonQuery(command, false);
		}

		private void ImportDataToTestTable()
		{
			var excelFile = ExcelFile.Load(SourceFilePath, new XlsxLoadOptions());
			var ws = excelFile.Worksheets[0];

			if (ws.Rows.Count <= 1)
			{
				throw new Exception("В указанном файле отсутствуют данные");
			}

			var objectsCount = ws.Rows.Count - 1;
			Console.WriteLine($"Начата загрузка объектов в базу...");
			for (var i = 1; i < ws.Rows.Count; i++)
			{
				try
				{
					var @object = new OMCoreObjectTest
					{
						CadastralNumber = ws.Rows[i].Cells[0].Value?.ToString(),
						Address = ws.Rows[i].Cells[1].Value?.ToString(),
						ParserTime = ws.Rows[i].Cells[2].Value?.ParseToDateTimeNullable(),
						Description = ws.Rows[i].Cells[3].Value?.ToString(),
						Area = ws.Rows[i].Cells[4].Value?.ParseToDecimalNullable(),
						Price = ws.Rows[i].Cells[5].Value?.ParseToLongNullable(),
						PricePerMeter = ws.Rows[i].Cells[6].Value?.ParseToDecimalNullable()
					};
					@object.Save();
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Не удалось загрузить {i} объект: {ex.Message}");
					objectsCount--;
				}
			}
			Console.WriteLine($"{objectsCount} объектов успешно загружены в базу.");
		}

		private void PerformDuplicatesSearch()
		{
			Console.WriteLine($"Начато выполнение процедуры поиска дубликатов.");
			//TODO: Perform Duplicates Search
			Console.WriteLine($"Процедура поиска дубликатов завершена.");
		}

		private void ExportResult()
		{
			Console.WriteLine($"Начата выгрузка объектов в результирующий файл.");
			var objects = OMCoreObjectTest
				.Where(x => true)
				.SelectAll()
				.Execute();
			var resultWorkbook = new ExcelFile();
			var worksheet = resultWorkbook.Worksheets.Add("1");

			worksheet.Cells[$"A1"].Value = OMCoreObjectTest.GetAttributeData(x => x.Id).Name;
			worksheet.Cells[$"B1"].Value = OMCoreObjectTest.GetAttributeData(x => x.CadastralNumber).Name;
			worksheet.Cells[$"C1"].Value = OMCoreObjectTest.GetAttributeData(x => x.Address).Name;
			worksheet.Cells[$"D1"].Value = OMCoreObjectTest.GetAttributeData(x => x.ParserTime).Name;
			worksheet.Cells[$"E1"].Value = OMCoreObjectTest.GetAttributeData(x => x.Description).Name;
			worksheet.Cells[$"F1"].Value = OMCoreObjectTest.GetAttributeData(x => x.Area).Name;
			worksheet.Cells[$"G1"].Value = OMCoreObjectTest.GetAttributeData(x => x.Price).Name;
			worksheet.Cells[$"H1"].Value = OMCoreObjectTest.GetAttributeData(x => x.PricePerMeter).Name;
			worksheet.Cells[$"I1"].Value = OMCoreObjectTest.GetAttributeData(x => x.ProcessType).Name;
			worksheet.Cells[$"J1"].Value = OMCoreObjectTest.GetAttributeData(x => x.ExclusionStatus).Name;
			var i = 2;
			foreach (var omCoreObjectTest in objects)
			{
				worksheet.Cells[$"A{i}"].Value = omCoreObjectTest.Id;
				worksheet.Cells[$"B{i}"].Value = omCoreObjectTest.CadastralNumber;
				worksheet.Cells[$"C{i}"].Value = omCoreObjectTest.Address;
				worksheet.Cells[$"D{i}"].Value = omCoreObjectTest.ParserTime;
				worksheet.Cells[$"E{i}"].Value = omCoreObjectTest.Description;
				worksheet.Cells[$"F{i}"].Value = omCoreObjectTest.Area;
				worksheet.Cells[$"G{i}"].Value = omCoreObjectTest.Price;
				worksheet.Cells[$"H{i}"].Value = omCoreObjectTest.PricePerMeter;
				worksheet.Cells[$"I{i}"].Value = omCoreObjectTest.ProcessType;
				worksheet.Cells[$"J{i}"].Value = omCoreObjectTest.ExclusionStatus;
				i++;
			}
			resultWorkbook.Save(ResultFilePath);
			Console.WriteLine($"Объекты успешно выгружены в файл {ResultFilePath}.");
		}
	}
}
