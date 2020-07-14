using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Tours;
using ObjectModel.Common;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport
{
    public static class DataImporterKO
    {
        private static TourFactorService _tourFactorService { get; set; }

        private static TourFactorService TourFactorService => _tourFactorService ?? (_tourFactorService = new TourFactorService());

        /// <summary>
        /// Импорт значений меток по фактору и группе из Excel
        /// groupId - Идентификатор группы
        /// factorId - Идентификатор фактора
        /// deleteOld - Признак удаления старых данных
        /// </summary>
        public static Stream ImportDataMarkerFromExcel(ExcelFile excelFile, string registerViewId, int mainRegisterId, long groupId, long factorId, bool deleteOld)
        {
			var import = CreateDataFileImport(excelFile, registerViewId, mainRegisterId);
			MemoryStream streamResult = new MemoryStream();

	        try
	        {
		        import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
		        import.DateStarted = DateTime.Now;
		        import.Save();

				var mainWorkSheet = excelFile.Worksheets[0];

		        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
		        ParallelOptions options = new ParallelOptions
		        {
			        CancellationToken = cancelTokenSource.Token,
			        MaxDegreeOfParallelism = 10
		        };

		        int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();

		        mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат сохранения");
		        mainWorkSheet.Rows[0].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All,
			        SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

		        List<ObjectModel.KO.OMMarkCatalog> objs = ObjectModel.KO.OMMarkCatalog
			        .Where(x => x.GroupId == groupId && x.FactorId == factorId).SelectAll().Execute();
		        if (deleteOld)
		        {
			        Parallel.ForEach(objs, options, obj => { obj.Destroy(); });
			        objs.Clear();
		        }

		        Parallel.ForEach(mainWorkSheet.Rows, options, row =>
		        {
			        try
			        {
				        if (row.Index != 0) //все, кроме заголовков
				        {
					        string value = mainWorkSheet.Rows[row.Index].Cells[0].Value.ParseToString();
					        string metka = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();

					        ObjectModel.KO.OMMarkCatalog existObject =
						        objs.Find(x => x.ValueFactor.ToUpper() == value.ToUpper());
					        bool newobj = false;
					        if (existObject == null)
					        {
						        existObject = new ObjectModel.KO.OMMarkCatalog
						        {
							        Id = -1,
							        FactorId = factorId,
							        GroupId = groupId,
							        ValueFactor = value.ToUpper(),
							        MetkaFactor = metka.ParseToDecimalNullable()
						        };
						        existObject.Save();
						        newobj = true;
					        }
					        else
					        {
						        existObject.MetkaFactor = metka.ParseToDecimal();
						        existObject.Save();
					        }



					        if (newobj)
					        {
						        try
						        {
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Новый объект");
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
								        .SetSolid(SpreadsheetColor.FromName(ColorName.LightGreen));
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders
								        .SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
									        LineStyle.Thin);
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
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
								        .SetSolid(SpreadsheetColor.FromName(ColorName.Yellow));
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders
								        .SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
									        LineStyle.Thin);
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
				        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
					        .SetSolid(SpreadsheetColor.FromName(ColorName.Red));
				        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All,
					        SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			        }
		        });
		        
		        excelFile.Save(streamResult, SaveOptions.XlsxDefault);
		        streamResult.Seek(0, SeekOrigin.Begin);
		        SaveResultFile(import, streamResult);
	        }
	        catch (Exception ex)
	        {
		        LogError(ex, import);
		        throw;
	        }

	        return streamResult;
        }

        /// <summary>
        /// Импорт группы из Excel
        /// tourId - Идентификатор тура
        /// unitStatus - статус единицы оценки
        /// </summary>
        public static Stream ImportDataGroupNumberFromExcel(ExcelFile excelFile, string registerViewId, int mainRegisterId, long tourId, ObjectModel.Directory.KoUnitStatus unitStatus)
        {
	        var import = CreateDataFileImport(excelFile, registerViewId, mainRegisterId);
	        MemoryStream streamResult = new MemoryStream();

	        try
	        {
		        import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
		        import.DateStarted = DateTime.Now;
		        import.Save();

				var mainWorkSheet = excelFile.Worksheets[0];

		        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
		        ParallelOptions options = new ParallelOptions
		        {
			        CancellationToken = cancelTokenSource.Token,
			        MaxDegreeOfParallelism = 10
		        };

		        int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();

		        mainWorkSheet.Rows[0].Cells[maxColumns].SetValue($"Результат сохранения");
		        mainWorkSheet.Rows[0].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All,
			        SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
		        List<ObjectModel.KO.OMGroup> parcelGroup =
			        ObjectModel.KO.OMGroup.GetListGroupTour(tourId, ObjectModel.Directory.KoGroupAlgoritm.MainParcel);
		        List<ObjectModel.KO.OMGroup> oksGroup =
			        ObjectModel.KO.OMGroup.GetListGroupTour(tourId, ObjectModel.Directory.KoGroupAlgoritm.MainOKS);
		        var groupAttributeFromTourSettings =
			        TourFactorService.GetTourAttributeFromSettings(tourId, KoAttributeUsingType.CodeGroupAttribute);

		        Parallel.ForEach(mainWorkSheet.Rows, options, row =>
		        {
			        try
			        {
				        if (row.Index != 0) //все, кроме заголовков
				        {
					        string cadastralNumber = mainWorkSheet.Rows[row.Index].Cells[0].Value.ParseToString();
					        string numberGroup = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
					        bool findGroup = false;
					        bool findObj = false;
					        ObjectModel.KO.OMUnit unit = ObjectModel.KO.OMUnit
						        .Where(x => x.TourId == tourId && x.Status_Code == unitStatus &&
						                    x.CadastralNumber == cadastralNumber).SelectAll().ExecuteFirstOrDefault();
					        if (unit != null)
					        {
						        findObj = true;
						        if (unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Stead)
						        {
							        ObjectModel.KO.OMGroup group = parcelGroup.Find(x => x.Number == numberGroup);
							        if (group != null)
							        {
								        unit.GroupId = group.Id;
								        unit.Save();
								        findGroup = true;
							        }
						        }
						        else
						        {
							        ObjectModel.KO.OMGroup group = oksGroup.Find(x => x.GroupName == numberGroup);
							        if (group != null)
							        {
								        unit.GroupId = group.Id;
								        unit.Save();
								        findGroup = true;
							        }
						        }
					        }

					        if (findGroup && findObj)
					        {
						        SaveGroupAsObjectAttribute(groupAttributeFromTourSettings?.Id, unit.ObjectId,
							        unit.TaskId, numberGroup);
					        }

					        if (findGroup)
					        {
						        try
						        {
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Группа обновлена");
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
								        .SetSolid(SpreadsheetColor.FromName(ColorName.LightGreen));
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders
								        .SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
									        LineStyle.Thin);
						        }
						        catch
						        {

						        }
					        }
					        else
					        {
						        try
						        {
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue(
								        findObj ? "Указанный объект не найден" : "Указанная группа не найдена");
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
								        .SetSolid(SpreadsheetColor.FromName(ColorName.Yellow));
							        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders
								        .SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black),
									        LineStyle.Thin);
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
				        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern
					        .SetSolid(SpreadsheetColor.FromName(ColorName.Red));
				        mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All,
					        SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
			        }
		        });

		        excelFile.Save(streamResult, SaveOptions.XlsxDefault);
		        streamResult.Seek(0, SeekOrigin.Begin);
		        SaveResultFile(import, streamResult);
	        }
	        catch (Exception ex)
	        {
				LogError(ex, import);
				throw;
	        }

	        return streamResult;
        }

        /// <summary>
        /// Импорт группы из Excel
        /// tourId - Идентификатор тура
        /// taskFilter - Список заданий на оценку
        /// </summary>
        public static Stream ImportDataGroupNumberFromExcel(ExcelFile excelFile, string registerViewId, int mainRegisterId, long tourId, List<long> taskFilter)
        {
	        var import = CreateDataFileImport(excelFile, registerViewId, mainRegisterId);
	        MemoryStream streamResult = new MemoryStream();

	        try
	        {
		        import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
		        import.DateStarted = DateTime.Now;
		        import.Save();

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
	            List<ObjectModel.KO.OMGroup> parcelGroup = ObjectModel.KO.OMGroup.GetListGroupTour(tourId, ObjectModel.Directory.KoGroupAlgoritm.MainParcel);
	            List<ObjectModel.KO.OMGroup> oksGroup = ObjectModel.KO.OMGroup.GetListGroupTour(tourId, ObjectModel.Directory.KoGroupAlgoritm.MainOKS);
	            var groupAttributeFromTourSettings = TourFactorService.GetTourAttributeFromSettings(tourId, KoAttributeUsingType.CodeGroupAttribute);

	            List<ObjectModel.KO.OMUnit> Objs = new List<ObjectModel.KO.OMUnit>();
	            foreach (long taskId in taskFilter)
	            {
	                Objs.AddRange(ObjectModel.KO.OMUnit.Where(x => x.TaskId == taskId).SelectAll().Execute());
	            }

	            Parallel.ForEach(mainWorkSheet.Rows, options, row =>
	            {
	                try
	                {
	                    if (row.Index != 0) //все, кроме заголовков
	                    {
	                        string cadastralNumber = mainWorkSheet.Rows[row.Index].Cells[0].Value.ParseToString();
	                        string numberGroup = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
	                        bool findGroup = false;
	                        bool findObj = false;

	                        ObjectModel.KO.OMUnit unit = Objs.Find(x=>x.CadastralNumber==cadastralNumber);
	                        if (unit != null)
	                        {
	                            findObj = true;
	                            if (unit.PropertyType_Code == ObjectModel.Directory.PropertyTypes.Stead)
	                            {
	                                ObjectModel.KO.OMGroup group = parcelGroup.Find(x => x.Number == numberGroup);
	                                if (group != null)
	                                {
	                                    unit.GroupId = group.Id;
	                                    unit.Save();
	                                    findGroup = true;
	                                }
	                            }
	                            else
	                            {
	                                ObjectModel.KO.OMGroup group = oksGroup.Find(x => x.Number == numberGroup);
	                                if (group != null)
	                                {
	                                    unit.GroupId = group.Id;
	                                    unit.Save();
	                                    findGroup = true;
	                                }
	                            }
	                        }

	                        if (findGroup && findObj)
	                        {
	                            SaveGroupAsObjectAttribute(groupAttributeFromTourSettings?.Id, unit.ObjectId, unit.TaskId, numberGroup);
	                        }

	                        if (findGroup)
	                        {
	                            try
	                            {
	                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Группа обновлена");
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
	                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue(findObj? "Указанный объект не найден" : "Указанная группа не найдена");
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

	            excelFile.Save(streamResult, SaveOptions.XlsxDefault);
	            streamResult.Seek(0, SeekOrigin.Begin);
	            SaveResultFile(import, streamResult);
			}
	        catch (Exception ex)
	        {
				LogError(ex, import);
				throw;
	        }

	        return streamResult;
        }


        #region Support Methods

        private static void SaveGroupAsObjectAttribute(long? attributeId, long? objectId, long? taskId, string numberGroup)
        {
            if(attributeId == null || objectId == null)
                return;

            var task = OMTask.Where(x => x.Id == taskId).Select(x => x.DocumentId).ExecuteFirstOrDefault();

            new GbuObjectAttribute
            {
                ObjectId = objectId.Value,
                AttributeId = attributeId.Value,
                StringValue = numberGroup,
                S = DateTime.Now,
                Ot = DateTime.Now,
                ChangeDocId = task == null ? -1 : task.DocumentId ?? -1,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now
            }.Save();
        }

        private static OMImportDataLog CreateDataFileImport(ExcelFile excelFile, string registerViewId, int mainRegisterId)
        {
	        var fileName = excelFile.DocumentProperties.Custom["FileName"].ToString();
	        MemoryStream str = new MemoryStream();
	        excelFile.Save(str, SaveOptions.XlsxDefault);
	        str.Seek(0, SeekOrigin.Begin);

	        var import = new OMImportDataLog()
	        {
		        UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
		        Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
		        DataFileTitle = DataImporterCommon.GetDataFileTitle(fileName),
		        FileExtension = DataImporterCommon.GetFileExtension(fileName),
		        DateCreated = DateTime.Now,
		        RegisterViewId = registerViewId,
		        MainRegisterId = mainRegisterId
	        };
	        import.Save();

	        import.DataFileName = DataImporterCommon.GetStorageDataFileName(import.Id);
	        FileStorageManager.Save(str, DataImporterCommon.FileStorageName, import.DateCreated, import.DataFileName);
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

        private static void LogError(Exception ex, OMImportDataLog import)
        {
	        long errorId = ErrorManager.LogError(ex);
	        import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
	        import.DateFinished = DateTime.Now;
	        import.ResultMessage = $"{ex.Message}{($" (журнал № {errorId})")}";
	        import.Save();
        }

        #endregion
    }
}

