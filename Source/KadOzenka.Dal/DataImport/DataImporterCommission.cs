using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks;
using Core.SRD;
using KadOzenka.Dal.DataExport;
using ObjectModel.Common;

namespace KadOzenka.Dal.DataImport
{
    public static class DataImporterCommission
    {
		public static void ImportDataCommissionFromExcel(ExcelFile excelFile, string registerViewId, int mainRegisterId)
		{
			var mainWorkSheet = excelFile.Worksheets[0];

			var fileName = excelFile.DocumentProperties.Custom["FileName"].ToString();
			MemoryStream str = new MemoryStream();
			excelFile.Save(str, SaveOptions.XlsxDefault);
			str.Seek(0, SeekOrigin.Begin);

			var import = SaveDataFile(str, registerViewId, mainRegisterId, fileName);

			try
			{
				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
				import.Save();

				CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
				ParallelOptions options = new ParallelOptions
				{
					CancellationToken = cancelTokenSource.Token,
					MaxDegreeOfParallelism = 10
				};

				int maxColumns = CommonSdks.DataExportCommon.GetLastUsedColumnIndex(mainWorkSheet) + 1;
				var lastUsedRowIndex = CommonSdks.DataExportCommon.GetLastUsedRowIndex(mainWorkSheet);
				mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат сохранения");
				//mainWorkSheet.Rows[0].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All,
				//	SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

				Parallel.ForEach(mainWorkSheet.Rows, options, row =>
				{
					try
					{
						if (row.Index != 0 && row.Index <= lastUsedRowIndex) //все, кроме заголовков и пустых строк в конце страницы
						{

							string kn = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
							string num_s = mainWorkSheet.Rows[row.Index].Cells[2].Value.ParseToString();
							DateTime? date_s = mainWorkSheet.Rows[row.Index].Cells[3].Value.ParseToDateTimeNullable();
							ObjectModel.Commission.OMCost existObject = ObjectModel.Commission.OMCost
								.Where(x => x.Kn == kn && x.StatementNumber == num_s && x.StatementDate == date_s)
								.SelectAll().ExecuteFirstOrDefault();
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

							ObjectModel.Directory.Commission.DecisionResult dr = ObjectModel.Directory.Commission
								.DecisionResult.Rejected;
							ObjectModel.Directory.Commission.CommissionType ct = ObjectModel.Directory.Commission
								.CommissionType.OnUnreliability;
							string tcom = mainWorkSheet.Rows[row.Index].Cells[10].Value.ParseToString();
							string rd = mainWorkSheet.Rows[row.Index].Cells[6].Value.ParseToString();

							if (tcom.ToUpper() == "Установление стоимости".ToUpper())
								ct = ObjectModel.Directory.Commission.CommissionType.OnSetCadCost;
							if (rd.ToUpper() == "положительное решение".ToUpper())
								dr = ObjectModel.Directory.Commission.DecisionResult.Approved;

							existObject.DecisionResult_Code = dr;
							existObject.CommissionType_Code = ct;

							decimal? d_kc = mainWorkSheet.Rows[row.Index].Cells[8].Value.ParseToDecimalNullable();
							existObject.Kc = (d_kc != null) ? ((d_kc == 0) ? null : d_kc) : (d_kc);
							existObject.DateKc = mainWorkSheet.Rows[row.Index].Cells[9].Value.ParseToDateTimeNullable();
							existObject.DecisionNumber = mainWorkSheet.Rows[row.Index].Cells[4].Value.ParseToString();
							existObject.DecisionDate =
								mainWorkSheet.Rows[row.Index].Cells[5].Value.ParseToDateTimeNullable();

							decimal? d_mv = mainWorkSheet.Rows[row.Index].Cells[14].Value.ParseToDecimalNullable();
							existObject.MarketValue = (d_mv != null) ? ((d_mv == 0) ? null : d_mv) : (d_mv);


							decimal? d_ckc = mainWorkSheet.Rows[row.Index].Cells[7].Value.ParseToDecimalNullable();
							existObject.CommissionKc = (d_ckc != null) ? ((d_ckc == 0) ? null : d_ckc) : (d_ckc);
							existObject.CommissionGroup = mainWorkSheet.Rows[row.Index].Cells[11].Value.ParseToString();
							existObject.CommissionChange =
								mainWorkSheet.Rows[row.Index].Cells[12].Value.ParseToString();

							existObject.ApplicantStatus_Code =
								(ObjectModel.Directory.Commission.ApplicantStatus) EnumExtensions
									.GetEnumByDescription<ObjectModel.Directory.Commission.ApplicantStatus>(
										mainWorkSheet.Rows[row.Index].Cells[13].Value.ParseToString());

							existObject.Save();


							if (newobj)
							{
								try
								{
									mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Новый объект");
									//mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
									//	.SetSolid(SpreadsheetColor.FromName(ColorName.LightGreen));
									//mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders
									//	.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
									//		LineStyle.Thin);
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
									//mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
									//	.SetSolid(SpreadsheetColor.FromName(ColorName.Yellow));
									//mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders
									//	.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
									//		LineStyle.Thin);
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
						mainWorkSheet.Rows[row.Index].Cells[maxColumns]
							.SetValue($"{ex.Message} (подробно в журнале №{errorId})");
						//mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
						//	.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
						//mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All,
						//	SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
					}
				});
				MemoryStream streamResult = new MemoryStream();
				excelFile.Save(streamResult, SaveOptions.XlsxDefault);
				streamResult.Seek(0, SeekOrigin.Begin);

				SaveResultFile(import, streamResult);
			}
			catch (Exception ex)
			{
				long errorId = ErrorManager.LogError(ex);
				import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
				import.DateFinished = DateTime.Now;
				import.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
				import.Save();

				throw;
			}
		}

		private static OMImportDataLog SaveDataFile(Stream stream, string registerViewId, int mainRegisterId,
			string fileName)
		{
			var dateStarted = DateTime.Now;
			var import = new OMImportDataLog()
			{
				UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
				DateStarted = dateStarted,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
				DataFileTitle = DataImporterCommon.GetDataFileTitle(fileName),
				FileExtension = DataImporterCommon.GetFileExtension(fileName),
				DateCreated = dateStarted,
				RegisterViewId = registerViewId,
				MainRegisterId = mainRegisterId
			};
			import.Save();

			import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
			FileStorageManager.Save(stream, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
			import.Save();

			return import;
		}

		private static void SaveResultFile(OMImportDataLog import, MemoryStream streamResult)
		{
			import.ResultFileTitle = DataImporterCommon.GetFileResultTitleFromDataTitle(import);
			import.ResultFileName = DataImporterCommon.GetStorageResultFileName(import.Id);
			import.DateFinished = DateTime.Now;
			FileStorageManager.Save(streamResult, DataImporterCommon.FileStorageName, import.DateFinished.Value, import.ResultFileName);
			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
			import.Save();
		}
    }
}

