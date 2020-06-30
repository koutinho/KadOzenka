using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.PricingFactorsComposition;
using KadOzenka.Dal.LongProcess.InputParameters;
using System.IO;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.SRD;
using GemBox.Spreadsheet;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.LongProcess
{
    public class PreviousToursReportProcess : LongProcess
    {
        private readonly ExcelFile _excelTemplate;
        private readonly ExcelWorksheet _mainWorkSheet;
        private int _currentRowIndex;
        private string _reportGbuStorage = "SaveReportPath";
        private string _reportName = "\"Состав данных о результатах кадастровой оценки предыдущих туров\"";


        public PreviousToursReportProcess()
        {
            _excelTemplate = new ExcelFile();
            _mainWorkSheet = _excelTemplate.Worksheets.Add("Лист 1");
            _mainWorkSheet.Cells.Style.Font.Name = "Times New Roman";
        }

        public static void AddProcessToQueue(PreviousToursReportInputParameters inputParameters)
        {
            LongProcessManager.AddTaskToQueue(nameof(PreviousToursReportProcess), parameters: inputParameters.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            PreviousToursReportInputParameters inputParameters = null;
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                inputParameters = processQueue.Parameters.DeserializeFromXml<PreviousToursReportInputParameters>();
            }
            if (inputParameters?.TaskIds == null || inputParameters.TaskIds.Count == 0) 
            {
                WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
                WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
                NotificationSender.SendNotification(processQueue, $"Отчет {_reportName}",
                    "Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов");
                return;
            }

            try
            {
                var report = new PreviousToursReport();
                var reportInfo = report.GetReportInfo(inputParameters.TaskIds, inputParameters.GroupId);
                WorkerCommon.SetProgress(processQueue, 50);

                var tourYears = reportInfo.Tours.Select(x => x.Year.ToString()).ToList();
                var pricingFactorNames = reportInfo.PricingFactors.Select(x => x.Name).ToList();

                GenerateReportHeader(reportInfo.Title, reportInfo.ColumnTitles, tourYears, pricingFactorNames);

                GenerateReportBody(reportInfo, report, tourYears, pricingFactorNames);

                var reportId = SaveReport();
                var message = "Операция успешно завершена." +
                              $@"<a href=""/GbuObject/GetFileResult?reportId={reportId}"">Скачать результат</a>";
                NotificationSender.SendNotification(processQueue, $"Отчет {_reportName}", message);

                WorkerCommon.SetProgress(processQueue, 100);

                //TODO для тестирования
                var link = $"https://localhost:50252/GbuObject/GetFileResult?reportId={reportId}";
            }
            catch (Exception ex)
            {
                var errorId = ErrorManager.LogError(ex);
                var message = $"Ошибка построения (журнал: {errorId})";
                NotificationSender.SendNotification(processQueue, $"Отчет {_reportName}", message);
                throw;
            }
        }


        #region Support Methods

        private void GenerateReportHeader(string reportTitle, List<string> columnTitles, List<string> tourYears, List<string> pricingFactorNames)
        {
            var columnsWithoutTourSeparation = new List<string>
            {
                "№ п/п",
                "Кадастровый номер"
            };

            var allColumnTitles = new List<string>();
            allColumnTitles.AddRange(columnsWithoutTourSeparation);
            allColumnTitles.AddRange(columnTitles);
            allColumnTitles.AddRange(pricingFactorNames);

            //для колонки с № п/п и КН  не нужны подзаголовки с турами
            var tourColumns = new List<string>();
            tourColumns.AddRange(columnsWithoutTourSeparation);
            tourColumns.AddRange(Enumerable
                .Repeat(tourYears, allColumnTitles.Count - columnsWithoutTourSeparation.Count)
                .SelectMany(x => x.ToList()).ToList());

            AddTitle(reportTitle, allColumnTitles.Count * tourYears.Count);
            AddHeaders(allColumnTitles, tourYears.Count, columnsWithoutTourSeparation.Count);
            AddRow(tourColumns, true);
        }

        private void AddTitle(string title, int mergedColumnsCount)
        {
            mergedColumnsCount--;
            var startColumnIndex = 0;
            var endColumnIndex = startColumnIndex + mergedColumnsCount;

            _mainWorkSheet.Rows[_currentRowIndex].Cells[startColumnIndex].SetValue(title);

            var cells = _mainWorkSheet.Cells.GetSubrangeAbsolute(_currentRowIndex, startColumnIndex, _currentRowIndex, endColumnIndex);
            cells.Merged = true;
            cells.Style = GetCellStyle(true);

            _currentRowIndex++;
        }

        private void AddHeaders(List<string> titles, int mergedColumnsCount, int numberOfColumnsWithoutTourSeparation)
        {
            mergedColumnsCount--;
            var endColumnIndex = -1;
            foreach (var title in titles)
            {
                endColumnIndex++;
                var startColumnIndex = endColumnIndex;
                var startRowIndex = _currentRowIndex;
                var endRowIndex = startRowIndex;

                _mainWorkSheet.Rows[_currentRowIndex].Cells[startColumnIndex].SetValue(title);

                //оставляем первые N ячеек необъединенными по горизонтали, но объединенными по вертикали
                if (startColumnIndex < numberOfColumnsWithoutTourSeparation)
                {
                    endColumnIndex = startColumnIndex;
                    endRowIndex++;
                }
                else
                {
                    endColumnIndex = startColumnIndex + mergedColumnsCount;
                }

                var cells = _mainWorkSheet.Cells.GetSubrangeAbsolute(startRowIndex, startColumnIndex, endRowIndex, endColumnIndex);
                cells.Merged = true;
                cells.Style = GetCellStyle(true);
            }

            _currentRowIndex++;
        }

        private void AddRow(List<string> values, bool isHeaderCellStyle = false)
        {
            for (var i = 0; i < values.Count; i++)
            {
                var cells = _mainWorkSheet.Rows[_currentRowIndex].Cells[i];
                cells.SetValue(values[i]);
                cells.Style = GetCellStyle(isHeaderCellStyle);
            }

            _currentRowIndex++;
        }

        private CellStyle GetCellStyle(bool isHeaderCellStyle = false)
        {
            var style = new CellStyle
            {
                HorizontalAlignment = HorizontalAlignmentStyle.Center,
                VerticalAlignment = VerticalAlignmentStyle.Center,
                WrapText = true
            };

            if(isHeaderCellStyle)
                style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

            return style;
        }

        /// <summary>
        /// Построчная генерация отчета
        /// </summary>
        /// <param name="reportInfo"></param>
        /// <param name="report"></param>
        /// <param name="tourYears"></param>
        /// <param name="pricingFactorNames"></param>
        private void GenerateReportBody(PreviousToursReport.PreviousToursReportInfo reportInfo, PreviousToursReport report, List<string> tourYears,
            List<string> pricingFactorNames)
        {
            var index = 1;
            var groupedReportItems = reportInfo.Items.GroupBy(x => x.CadastralNumber).ToList();
            groupedReportItems.ForEach(groupedItem =>
            {
                var rowValues = new List<string> { index.ToString(), groupedItem.Key };

                reportInfo.ColumnTitles.ForEach(title =>
                {
                    tourYears.ForEach(tourYear =>
                    {
                        var itemInTour = groupedItem.FirstOrDefault(x => x.Tour?.Year.ToString() == tourYear);
                        rowValues.Add(report.GetValueForReportItem(title, itemInTour)?.ToString());
                    });
                });

                pricingFactorNames.ForEach(factorName =>
                {
                    tourYears.ForEach(tourYear =>
                    {
                        var itemInTour = groupedItem.FirstOrDefault(x => x.Tour?.Year.ToString() == tourYear);
                        var itemFactor = itemInTour?.Factors?.FirstOrDefault(x => x.Name == factorName);
                        rowValues.Add(itemFactor?.Value);
                    });
                });

                index++;
                AddRow(rowValues);
            });
        }

        private long SaveReport()
        {
            try
            {
                var stream = new MemoryStream();
                _excelTemplate.Save(stream, SaveOptions.XlsxDefault);
                stream.Seek(0, SeekOrigin.Begin);

                var currentDate = DateTime.Now;
                var export = new OMExportByTemplates
                {
                    UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
                    DateCreated = currentDate,
                    Status = (int)ImportStatus.Added,
                    TemplateFileName = _reportName,
                    MainRegisterId = OMMainObject.GetRegisterId(),
                    RegisterViewId = "GbuObjects"
                };
                export.Save();

                FileStorageManager.Save(stream, _reportGbuStorage, currentDate, export.Id.ToString());

                return export.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ErrorManager.LogError(e);
                throw;
            }
        }

        #endregion
    }
}
