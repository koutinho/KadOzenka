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
    public static class DataImporterKO
    {
        /// <summary>
        /// Импорт значений меток по фактору и группе из Excel
        /// groupId - Идентификатор группы
        /// factorId - Идентификатор фактора
        /// deleteOld - Признак удаления старых данных
        /// </summary>
        public static Stream ImportDataMarkerFromExcel(ExcelFile excelFile, string registerViewId, int mainRegisterId, long groupId, long factorId, bool deleteOld)
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

            List<ObjectModel.KO.OMMarkCatalog> objs = ObjectModel.KO.OMMarkCatalog.Where(x => x.GroupId == groupId && x.FactorId == factorId).SelectAll().Execute();
            if (deleteOld)
            {
                Parallel.ForEach(objs, options, obj =>
                {
                    obj.Destroy();
                });
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

                        ObjectModel.KO.OMMarkCatalog existObject = objs.Find(x => x.ValueFactor.ToUpper() == value.ToUpper());
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
                    "KOFilesStorage",
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


        /// <summary>
        /// Импорт группы из Excel
        /// tourId - Идентификатор тура
        /// unitStatus - статус единицы оценки
        /// </summary>
        public static Stream ImportDataGroupNumberFromExcel(ExcelFile excelFile, string registerViewId, int mainRegisterId, long tourId, ObjectModel.Directory.KoUnitStatus unitStatus)
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
            List<ObjectModel.KO.OMGroup> parcelGroup = ObjectModel.KO.OMGroup.GetListGroupTour(tourId, ObjectModel.Directory.KoGroupAlgoritm.MainParcel);
            List<ObjectModel.KO.OMGroup> oksGroup = ObjectModel.KO.OMGroup.GetListGroupTour(tourId, ObjectModel.Directory.KoGroupAlgoritm.MainOKS);

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
                        ObjectModel.KO.OMUnit unit = ObjectModel.KO.OMUnit.Where(x => x.TourId == tourId && x.Status_Code == unitStatus && x.CadastralNumber==cadastralNumber).SelectAll().ExecuteFirstOrDefault();
                        if (unit!=null)
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
                                mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue(findObj ? "Указанный объект не найден" : "Указанная группа не найдена");
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

        /// <summary>
        /// Импорт группы из Excel
        /// tourId - Идентификатор тура
        /// taskFilter - Список заданий на оценку
        /// </summary>
        public static Stream ImportDataGroupNumberFromExcel(ExcelFile excelFile, string registerViewId, int mainRegisterId, long tourId, List<long> taskFilter)
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
            List<ObjectModel.KO.OMGroup> parcelGroup = ObjectModel.KO.OMGroup.GetListGroupTour(tourId, ObjectModel.Directory.KoGroupAlgoritm.MainParcel);
            List<ObjectModel.KO.OMGroup> oksGroup = ObjectModel.KO.OMGroup.GetListGroupTour(tourId, ObjectModel.Directory.KoGroupAlgoritm.MainOKS);

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
                                ObjectModel.KO.OMGroup group = oksGroup.Find(x => x.GroupName == numberGroup);
                                if (group != null)
                                {
                                    unit.GroupId = group.Id;
                                    unit.Save();
                                    findGroup = true;
                                }
                            }

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
            MemoryStream streamResult = new MemoryStream();
            excelFile.Save(streamResult, SaveOptions.XlsxDefault);
            streamResult.Seek(0, SeekOrigin.Begin);
            SaveImportFile(streamResult, excelFile, registerViewId, mainRegisterId, true);
            return streamResult;
        }
    }
}

