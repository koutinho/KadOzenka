using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks.Excel;
using GemBox.Spreadsheet;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataExport
{
    /// <summary>
    /// Класс выгрузки изменений по объектам.
    /// </summary>
    public class DEKOChange : IKoUnloadResult
    {
        /// <summary>
        /// Выгрузка изменений
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadChange)]
        public static List<ResultKoUnloadSettings> ExportUnitChangeToExcel(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings settings, SetProgress setProgress)
        {
            if (unloadResultQueue != null)
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var result = new List<ResultKoUnloadSettings>();
            var progressMessage = "Выгрузка изменений";
            string filename = "Список_изменений_"+DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            ExcelFile excelTemplate = new ExcelFile();

            var mainWorkSheet = excelTemplate.Worksheets.Add("Изменения");

            ExcelFileHelper.AddRow(mainWorkSheet, 0, new object[] { "КН", "Дата изменения", "Тип", "Статус", "Старое значение", "Новое значение", "Изменение" });
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };

            var taskCounter = 0;
            var progress = 0;
            foreach (long taskId in settings.TaskFilter)
            {
                List<ObjectModel.KO.OMUnit> units = null;
                if(settings.IsDataComparingUnload)
                    units = ObjectModel.KO.OMUnit.Where(x => x.TaskId == taskId).SelectAll().Execute();
                else
                {
                    units = settings.UnloadParcel
                        ? ObjectModel.KO.OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == PropertyTypes.Stead).SelectAll().Execute()
                        : ObjectModel.KO.OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code != PropertyTypes.Stead).SelectAll().Execute();
                }

                var unitsCounter = 0;
                if (units.Count > 0)
                {
                    int row = 1;

                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        List<ObjectModel.KO.OMUnitChange> changes = ObjectModel.KO.OMUnitChange.Where(x => x.UnitId == unit.Id).SelectAll().Execute();

                        foreach (ObjectModel.KO.OMUnitChange change in changes)
                        {
	                        ExcelFileHelper.AddRow(mainWorkSheet, row, new object[] { unit.CadastralNumber, unit.CreationDate.Value, unit.PropertyType, unit.StatusRepeatCalc, change.OldValue, change.NewValue, change.ChangeStatus });
                            row++;
                        }

                        unitsCounter++;
                        progress = (unitsCounter * 100 / units.Count + taskCounter * 100) / settings.TaskFilter.Count;
                        setProgress?.Invoke(progress, progressMessage:progressMessage);
                        if (unloadResultQueue != null)
                            KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                }

                taskCounter++;
            }

            if (settings.IsDataComparingUnload)
            {
                MemoryStream stream = new MemoryStream();
                excelTemplate.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
                stream.Seek(0, SeekOrigin.Begin);

                using var fs = File.Create(settings.FileName);
                fs.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fs);
            }
            else
            {
                long id = SaveUnloadResult.SaveResult(filename, excelTemplate, unloadResultQueue.Id, KoUnloadResultType.UnloadChange);
                //excelTemplate.Save(filename, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
                var resultFile = new ResultKoUnloadSettings
                {
                    FileId = id,
                    FileName = filename,
                    TaskId = settings.TaskFilter.FirstOrDefault()
                };
                result.Add(resultFile);
            }
            

            if (unloadResultQueue != null)
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress?.Invoke(100, true, progressMessage);

            return result;
        }
    }
}