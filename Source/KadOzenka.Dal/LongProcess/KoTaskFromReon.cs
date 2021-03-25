using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ObjectModel.Core.LongProcess;
using System.Threading;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using IO.Swagger.Model;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Tasks;
using KadOzenka.WebClients.ReonClient.Api;
using ObjectModel.Directory;
using ObjectModel.KO;
using Core.Shared.Extensions;
using KadOzenka.Dal.ConfigurationManagers;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.Documents.Dto;
using KadOzenka.Dal.GbuObject;
using KadOzenka.WebClients.ConfigurationManagers;

namespace KadOzenka.Dal.LongProcess
{
    public class KoTaskFromReon : LongProcess
    {
        private TaskService TaskService { get; set; }
        private DocumentService DocumentService { get; set; }
        private RosreestrDataApi ReonWebClientService { get; set; }
        private readonly GbuReportService.Column _taskNumberColumn;
        private readonly GbuReportService.Column _taskDateColumn;
        private readonly GbuReportService.Column _tourYearColumn;
        private readonly GbuReportService.Column _filesCountColumn;
        private readonly GbuReportService.Column _errorColumn;

        public KoTaskFromReon()
        {
            TaskService = new TaskService();
            DocumentService = new DocumentService();
            ReonWebClientService = new RosreestrDataApi();

            _taskNumberColumn = new GbuReportService.Column
            {
                Header = "Номер задания",
                Index = 0,
                Width = 8
            };
            _taskDateColumn = new GbuReportService.Column
            {
	            Header = "Дата задания",
	            Index = 1,
	            Width = 4
            };
            _tourYearColumn = new GbuReportService.Column
            {
	            Header = "Год тура",
	            Index = 2,
	            Width = 2
            };
            _filesCountColumn = new GbuReportService.Column
            {
	            Header = "Количество файлов",
	            Index = 3,
	            Width = 2
            };
            _errorColumn = new GbuReportService.Column
            {
	            Header = "Ошибка",
	            Index = 4,
	            Width = 8
            };
        }

        

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);

            using var reportService = new GbuReportService("Получение заданий на оценку из ИС РЕОН");
            reportService.AddHeaders(new List<string>
            {
	            _taskNumberColumn.Header, _taskDateColumn.Header, 
	            _tourYearColumn.Header, _filesCountColumn.Header,
	            _errorColumn.Header
            });
            reportService.SetIndividualWidth(_taskNumberColumn.Index, _taskNumberColumn.Width);
            reportService.SetIndividualWidth(_taskDateColumn.Index, _taskDateColumn.Width);
            reportService.SetIndividualWidth(_tourYearColumn.Index, _tourYearColumn.Width);
            reportService.SetIndividualWidth(_filesCountColumn.Index, _filesCountColumn.Width);
            reportService.SetIndividualWidth(_errorColumn.Index, _errorColumn.Width);

            var request = GetRequest(processType);
            var response = ReonWebClientService.RosreestrDataGetRRData(request.DateFrom, request.DateTo);
            var errorIds = ProcessResponse(reportService, response);

            var info = errorIds.Count > 0 
                ? $"Не удалось обработать все данные, подробно в журнале №{string.Join(", ", errorIds)}" 
                : "Операция выполнена успешно. Задания созданы. Загрузка добавлена в очередь, по результатам загрузки будет отправлено сообщение.";

            var reportId = reportService.SaveReport();
            var message = $"{info}\n" + $@"<a href=""{reportService.GetUrlToDownloadFile(reportId)}"">Скачать результат</a>";
            var roleId = ConfigurationManager.ReonConfig.RoleIdForNotification?.ParseToLongNullable();
            NotificationSender.SendNotification(processQueue, "Получение заданий на оценку из ИС РЕОН", message, roleId);

            WorkerCommon.SetProgress(processQueue, 100);
        }


        #region Support Methods

        private TaskFromReonRequest GetRequest(OMProcessType processType)
        {
            if(processType != null && processType.Parameters.IsNotEmpty())
                return new TaskFromReonRequest(processType.Parameters.ParseToDateTime(), processType.Parameters.ParseToDateTime().AddDays(1));

            var dateFrom = DateTime.Today.AddDays(-1);
            var dateTo = DateTime.Today;

            return new TaskFromReonRequest(dateFrom, dateTo);
        }

        public List<long> ProcessResponse(GbuReportService reportService, List<RRDataLoadModel> tasksFromResponse)
        {
            var errorIds = new List<long>();
            tasksFromResponse.ForEach(task =>
            {
                OMTask omTask = null;
                try
                {
                    var documentDto = new DocumentDto
                    {
                        RegNumber = task.DocNumber,
                        Description = task.DocName,
                        CreateDate = task.DocDate
                    };
                    var documentId = DocumentService.AddDocument(documentDto);

                    omTask = CreateTask(task, documentId);

                    foreach (var fileInfo in task.XmlDocUrls)
                    {
                        ProcessFile(fileInfo, omTask.Id);
                    }

                    var taskNumber = TaskService.GetTemplateForTaskName(task.DateAppraisal, task.DocDate, task.DocNumber, omTask.NoteType);
                    AddRowToReport(reportService, taskNumber, omTask.CreationDate, task.TourYear, task.XmlDocUrls.Count, string.Empty);
                }
                catch (Exception ex)
                {
                    long errorId = ErrorManager.LogError(ex);
                    errorIds.Add(errorId);

                    var taskNumber = TaskService.GetTemplateForTaskName(task.DateAppraisal, task.DocDate, task.DocNumber, omTask?.NoteType);
                    AddRowToReport(reportService, taskNumber, omTask?.CreationDate, task.TourYear, task.XmlDocUrls.Count,
                        $"Ошибка загрузки (журнал: {errorId})");
                }
            });

            return errorIds;
        }

        private OMTask CreateTask(RRDataLoadModel task, long documentId)
        {
            var omTask = new OMTask
            {
                TourId = GetTourByYear(task),
                DocumentId = documentId,
                CreationDate = DateTime.Now,
                EstimationDate = task.DateAppraisal,
                NoteType_Code = GetTaskNoteType(task.LoadType),
                Status_Code = KoTaskStatus.InWork
            };
            omTask.Save();

            return omTask;
        }

        private long GetTourByYear(RRDataLoadModel task)
        {
            if (task.TourYear == null)
                throw new Exception(
                    $"Сервис РЕОН не вернул год для задачи. Создание задачи прервано. Номер документа: '{task.DocNumber}'");

            var tour = OMTour.Where(x => x.Year == task.TourYear).Select(x => x.Id).ExecuteFirstOrDefault();
            if(tour == null)
                throw new Exception($"Не найден тур с годом '{task.TourYear}'.");

            return tour.Id;
        }


        private KoNoteType GetTaskNoteType(string loadType)
        {
            switch (loadType)
            {
                case "Плановый":
                    return KoNoteType.Initial;
                case "Вновь учтенные объекты":
                    return KoNoteType.Day;
                case "Внеплановый (полный)":
                    return KoNoteType.Year;
                default:
                    return KoNoteType.None;
            }
        }

        private void ProcessFile(DocUrl fileInfo, long taskId)
        {
            var url = ConfigurationManager.ReonConfig.BaseUrl + "/RosreestrData/" + fileInfo.Url;
            var data = GetFileData(url);
            var stream = new MemoryStream(data);

            DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", fileInfo.FileName,
                stream, OMTask.GetRegisterId(), taskId);
        }

        private static byte[] GetFileData(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadData(url);
            }
        }

        private void AddRowToReport(GbuReportService reportService, string taskNumber, DateTime? taskDate, int? tourYear, int filesCount, string errorMessage)
        {
	        var row = reportService.GetCurrentRow();

	        reportService.AddRow(row, new List<string>
                {taskNumber, taskDate?.ToShortDateString(), tourYear?.ToString(), filesCount.ToString(), errorMessage});
        }

        #endregion


        #region Entities

        public class TaskFromReonRequest
        {
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }

            public TaskFromReonRequest(DateTime dateFrom, DateTime dateTo)
            {
                DateFrom = dateFrom;
                DateTo = dateTo;
            }
        }

        #endregion
    }
}
