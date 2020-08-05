using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Tours;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.BlFrontEnd.ExportKO
{
	public class AttributeMapExport
	{
		public static void Export()
		{
			string filePath = "C:\\WORK\\ИД Атрибуты реестров 250-253.xlsx";
			ExcelFile excelFile = ExcelFile.Load(filePath, new XlsxLoadOptions());

			var GbuObjectService = new GbuObjectService();
			var TourFactorService = new TourFactorService();
			var existedGbuAttributes = GbuObjectService.GetGbuAttributes();

			for (int i = 0; i < 4; i++)
			{
				var mainWorkSheet = excelFile.Worksheets[i];

				int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();
				mainWorkSheet.Rows[0].Cells[maxColumns].SetValue("Результат сохранения");

				var existedKoTourAttributes = GetTourAttributes(TourFactorService, i);
				for (var j = 1; j < mainWorkSheet.Rows.Count; j++)
				{
					try
					{
						var gbuAttrIdStr = mainWorkSheet.Rows[j].Cells[5].Value.ParseToStringNullable();
						if (gbuAttrIdStr == "#N/A" || gbuAttrIdStr == "#Н/Д")
						{
							continue;
						}

						if (long.TryParse(gbuAttrIdStr, out var gbuAttrId))
						{
							if (existedGbuAttributes.All(x => x.Id != gbuAttrId))
							{
								throw new Exception($"Не найден ГБУ атрибут с ИД {gbuAttrId}");
							}

							var koAttrId = mainWorkSheet.Rows[j].Cells[1].Value.ParseToLong();

							if (existedKoTourAttributes.All(x => x.Id != koAttrId))
							{
								throw new Exception($"Не найден KO атрибут с ИД {koAttrId}");
							}

							var record = new OMTransferAttributes();
							switch (i)
							{
								case 0:
									record.TourId = 2018;
									record.IsOks = true;
									break;
								case 1:
									record.TourId = 2018;
									record.IsOks = false;
									break;
								case 2:
									record.TourId = 2016;
									record.IsOks = true;
									break;
								case 3:
									record.TourId = 2016;
									record.IsOks = false;
									break;
							}

							record.KoId = koAttrId;
							record.GbuId = gbuAttrId;
							var recordId = record.Save();

							mainWorkSheet.Rows[j].Cells[maxColumns].SetValue($"Добавлено в 257 реестр (ИД:{recordId})");
							mainWorkSheet.Rows[j].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.LightGreen));
							mainWorkSheet.Rows[j].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
						}
					}
					catch (Exception ex)
					{
						mainWorkSheet.Rows[j].Cells[maxColumns].SetValue($"Ошибка: {ex.Message}");
						mainWorkSheet.Rows[j].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
						mainWorkSheet.Rows[j].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
					}
				}

			}

			MemoryStream streamResult = new MemoryStream();
			excelFile.Save(streamResult, SaveOptions.XlsxDefault);
			streamResult.Seek(0, SeekOrigin.Begin);

			using (FileStream file = new FileStream("C:\\WORK\\ИД Атрибуты реестров 250-253 (Result).xlsx", FileMode.Create, FileAccess.Write))
			{
				byte[] bytes = new byte[streamResult.Length];
				streamResult.Read(bytes);
				file.Write(bytes);
				streamResult.Close();
			}
		}

		private static List<OMAttribute> GetTourAttributes(TourFactorService service, int worksheetNum)
		{
			switch (worksheetNum)
			{
				case 0:
					return service.GetTourAttributes(2018, ObjectType.Oks);
				case 1:
					return service.GetTourAttributes(2018, ObjectType.ZU);
				case 2:
					return service.GetTourAttributes(2016, ObjectType.Oks);
				case 3:
					return service.GetTourAttributes(2016, ObjectType.ZU);
				default:
					throw new NotSupportedException();
			}
		}
	}
}
