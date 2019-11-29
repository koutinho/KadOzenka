using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using ObjectModel.Commission;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using ObjectModel.Common;

namespace KadOzenka.Dal.DataImport
{
    public static class DataImporterCommission
    {
		public static Stream ImportDataCommissionFromExcel(ExcelFile excelFile, string registerViewId, int mainRegisterId)
		{
			var mainWorkSheet = excelFile.Worksheets[0];

			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 10
			};

			int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();

			mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат сохранения");
			mainWorkSheet.Rows[0].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

			Parallel.ForEach(mainWorkSheet.Rows, options, row =>
			{
				try
				{
					if (row.Index != 0) //все, кроме заголовков
					{

						string kn = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
						string num_s = mainWorkSheet.Rows[row.Index].Cells[2].Value.ParseToString();
						DateTime? date_s = mainWorkSheet.Rows[row.Index].Cells[3].Value.ParseToDateTimeNullable();
						ObjectModel.Commission.OMCost existObject = ObjectModel.Commission.OMCost.Where(x => x.Kn == kn && x.StatementNumber == num_s && x.StatementDate == date_s).SelectAll().ExecuteFirstOrDefault();
						bool newobj = false;
						if (existObject == null)
						{
							existObject = new ObjectModel.Commission.OMCost
							{
								Id = -1,
								StatementDate = date_s,
								StatementNumber = num_s,
								Kn = kn,
							};
							newobj = true;
						}

						ObjectModel.Directory.Commission.DecisionResult dr = ObjectModel.Directory.Commission.DecisionResult.Rejected;
						ObjectModel.Directory.Commission.CommissionType ct = ObjectModel.Directory.Commission.CommissionType.OnUnreliability;
						string tcom = mainWorkSheet.Rows[row.Index].Cells[10].Value.ParseToString();
						string rd = mainWorkSheet.Rows[row.Index].Cells[6].Value.ParseToString();

						if (tcom.ToUpper() == "Установление стоимости".ToUpper()) ct = ObjectModel.Directory.Commission.CommissionType.OnSetCadCost;
						if (rd.ToUpper() == "положительное решение".ToUpper()) dr = ObjectModel.Directory.Commission.DecisionResult.Approved;

						existObject.DecisionResult_Code = dr;
						existObject.CommissionType_Code = ct;

						decimal? d_kc = mainWorkSheet.Rows[row.Index].Cells[8].Value.ParseToDecimalNullable();
						existObject.Kc = (d_kc != null) ? ((d_kc == 0) ? null : d_kc) : (d_kc);
						existObject.DateKc = mainWorkSheet.Rows[row.Index].Cells[9].Value.ParseToDateTimeNullable();
						existObject.DecisionNumber = mainWorkSheet.Rows[row.Index].Cells[4].Value.ParseToString();
						existObject.DecisionDate = mainWorkSheet.Rows[row.Index].Cells[5].Value.ParseToDateTimeNullable();

						decimal? d_mv = mainWorkSheet.Rows[row.Index].Cells[14].Value.ParseToDecimalNullable();
						existObject.MarketValue = (d_mv != null) ? ((d_mv == 0) ? null : d_mv) : (d_mv);


						decimal? d_ckc = mainWorkSheet.Rows[row.Index].Cells[7].Value.ParseToDecimalNullable();
						existObject.CommissionKc = (d_ckc != null) ? ((d_ckc == 0) ? null : d_ckc) : (d_ckc);
						existObject.CommissionGroup = mainWorkSheet.Rows[row.Index].Cells[11].Value.ParseToString();
						existObject.CommissionChange = mainWorkSheet.Rows[row.Index].Cells[12].Value.ParseToString();

						existObject.ApplicantStatus_Code = (ObjectModel.Directory.Commission.ApplicantStatus)EnumExtensions.GetEnumByDescription<ObjectModel.Directory.Commission.ApplicantStatus>(mainWorkSheet.Rows[row.Index].Cells[13].Value.ParseToString());

						existObject.Save();


						if (newobj)
						{
							try
							{
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Новый объект");
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.LightGreen));
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
							}
							catch
							{

							}

						}
						else
						{
							try
							{
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Обновлено");
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Yellow));
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
							}
							catch
							{

							}

						}
					}
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"{ex.Message} (подробно в журнале №{errorId})");
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
				}
			});
			MemoryStream streamResult = new MemoryStream();
			excelFile.Save(streamResult, SaveOptions.XlsxDefault);
			streamResult.Seek(0, SeekOrigin.Begin);

			SaveImportFile(streamResult, excelFile, registerViewId, mainRegisterId, true);
			return streamResult;
		}

		public static void SaveImportFile(Stream stream, ExcelFile excelFile, string registerViewId, int mainRegisterId, bool isResultFile = false)
		{
			var fileName = excelFile.DocumentProperties.Custom["FileName"].ToString();

			var dateStarted = DateTime.Now;
			var importResult = new OMImportDataLog()
			{
				UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
				DateStarted = dateStarted,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
				DataFileName = fileName,
				DateCreated = dateStarted,
				RegisterViewId = registerViewId,
				MainRegisterId = mainRegisterId
			};
			try
			{
				FileStorageManager.Save(
					stream,
					"CommissionFilesStorage",
					dateStarted,
					isResultFile ? importResult.Save() + "_result" : importResult.Save().ToString()
				);
				importResult.DateFinished = DateTime.Now;
				importResult.Save();
			}
			catch (Exception ex)
			{
				long errorId = ErrorManager.LogError(ex);
				importResult.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
				importResult.DateFinished = DateTime.Now;
				importResult.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
				importResult.Save();
			}
		}
	}
}

