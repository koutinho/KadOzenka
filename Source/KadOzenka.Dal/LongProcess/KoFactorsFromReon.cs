using System;
using System.Collections.Generic;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Transactions;
using CadAppraisalDataApi.Models;
using CommonSdks.ConfigurationManagers;
using CommonSdks.Excel;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.InputParameters;
using KadOzenka.Dal.Registers;
using KadOzenka.WebClients.ReonClient.Api;
using ObjectModel.Core.Register;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;
using Platform.Configurator;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
    public class KoFactorsFromReon : LongProcess
    {
        public const string LongProcessName = nameof(KoFactorsFromReon);
        private static readonly ILogger _log = Log.ForContext<KoFactorsFromReon>();

        public static readonly long ReonSourceRegisterId = 44355304;
        public static readonly string AttributeNameSeparator = " - ";
        private RosreestrDataApi ReonWebClientService { get; set; }
        private RegisterAttributeService RegisterAttributeService { get; set; }
        private readonly GbuReportService.Column _cadastralNumberColumn;
        private readonly GbuReportService.Column _operationResultColumn;
        private readonly GbuReportService.Column _errorColumn;

        public KoFactorsFromReon()
        {
            ReonWebClientService = new RosreestrDataApi();
            RegisterAttributeService = new RegisterAttributeService();

            _cadastralNumberColumn = new GbuReportService.Column
            {
	            Header = "Кадастровый номер",
	            Index = 0,
	            Width = 6
            };
            _operationResultColumn = new GbuReportService.Column
            {
	            Header = "Успешно",
	            Index = 1,
	            Width = 2
            };
            _errorColumn = new GbuReportService.Column
            {
	            Header = "Ошибка",
	            Index = 2,
	            Width = 8
            };
        }


        public static void AddProcessToQueue(KoFactorsFromReonInputParameters inputParameters)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, parameters: inputParameters.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            
            var messageSubject = "Получение графических факторов из ИС РЕОН";
            KoFactorsFromReonInputParameters inputParameters = null;
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                inputParameters = processQueue.Parameters.DeserializeFromXml<KoFactorsFromReonInputParameters>();
            }
            if (inputParameters == null || inputParameters.TaskId == 0)
            {
                WorkerCommon.SetMessage(processQueue, Common.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
                WorkerCommon.SetProgress(processQueue, Common.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
                NotificationSender.SendNotification(processQueue, messageSubject,
                    "Операция завершена с ошибкой, т.к. нет входных данных. Подробнее в списке процессов");
                return;
            }

            var task = GetTask(inputParameters.TaskId);
            var document = GetDocument(task.DocumentId);
            var units = GetUnits(task.Id);

            var errorIds = new List<long>();
            bool isError = false;
            long total = units.Count;
            long success = 0;
            long errors = 0;

            using var gbuReportService = new GbuReportService("Получение графических факторов из ИС РЕОН");
            gbuReportService.AddHeaders(new List<string>
            {
	            _cadastralNumberColumn.Header, _operationResultColumn.Header, _errorColumn.Header
            });
            gbuReportService.SetIndividualWidth(_cadastralNumberColumn.Index, _cadastralNumberColumn.Width);
            gbuReportService.SetIndividualWidth(_operationResultColumn.Index, _operationResultColumn.Width);
            gbuReportService.SetIndividualWidth(_errorColumn.Index, _errorColumn.Width);

            units.ForEach(unit =>
            {
                if (!unit.ObjectId.HasValue)
                    return;

                try
                {
                    var request = GetRequest(task, unit);
                    var response = ReonWebClientService.RosreestrDataGetGraphFactorsByCadNum(request.CadastralNumber, request.EstimationDate);
                   
                    var currentErrorIds = ProcessServiceResponse(unit.ObjectId.Value, document, inputParameters.AttributeIds, response);
                    if (currentErrorIds?.Count > 0)
                    {
                        errors++;
                        isError = true;
                        errorIds.AddRange(currentErrorIds);
                        AddRowToReport(gbuReportService, unit.CadastralNumber, false, $"Ошибка загрузки (журнал: {string.Join(", ", currentErrorIds)})");
                    }
                    else
                    {
                        success++;
                        AddRowToReport(gbuReportService, unit.CadastralNumber, true, string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    errors++;
                    isError = true;
                    // Нет смысла логировать однотипные ошибки в БД
                    if (errors < 10)
                    {
                        var errorId = ErrorManager.LogError(ex);
                        AddRowToReport(gbuReportService, unit.CadastralNumber, false, $"Ошибка загрузки (журнал: {errorId})");
                    }
                    else {
                        AddRowToReport(gbuReportService, unit.CadastralNumber, false, $"Ошибка загрузки (без записи в журнал)");
                    }
                }
            });

            var info = isError
                ? $"При загрузке факторов возникли ошибки. Всего единиц оценки: {total}; Успешно: {success}; Ошибки: {errors}"
                : $"Загрузка факторов выполнена без ошибок. Всего единиц оценки: {total}; Успешно: {success}; Ошибки: {errors}";
            WorkerCommon.SetMessage(processQueue, info);
            var reportId = gbuReportService.SaveReport();
            var message = $"{info}\n" + $@"<a href=""{gbuReportService.GetUrlToDownloadFile(reportId)}"">Скачать результат</a>";
            var roleId = ConfigurationManager.WebClientsConfig.RoleIdForNotification?.ParseToLongNullable();
            NotificationSender.SendNotification(processQueue, messageSubject, message, roleId);

            WorkerCommon.SetProgress(processQueue, 100);
        }


        #region Support Methods

        private OMTask GetTask(long taskId)
        {
            var task = OMTask.Where(x => x.Id == taskId)
                .Select(x => x.EstimationDate)
                .Select(x => x.DocumentId)
                .ExecuteFirstOrDefault();

            if (task == null)
                throw new Exception($"Не найдено задание на оценку с Id ='{taskId}'");

            return task;
        }

        private OMInstance GetDocument(long? documentId)
        {
            return OMInstance.Where(x => x.Id == documentId)
                .Select(x => x.Id)
                .Select(x => x.CreateDate)
                .ExecuteFirstOrDefault();
        }

        private List<OMUnit> GetUnits(long taskId)
        {
            return OMUnit.Where(x =>
                    x.TaskId == taskId && (x.PropertyType_Code == PropertyTypes.Building ||
                                           x.PropertyType_Code == PropertyTypes.Stead))
                .Select(x => x.CadastralNumber)
                .Select(x => x.ObjectId)
                .Execute();
        }

        private FactorsFromReonRequest GetRequest(OMTask task, OMUnit unit)
        {
            return new FactorsFromReonRequest(unit.CadastralNumber, task.EstimationDate ?? DateTime.Today);
        }

        public List<long> ProcessServiceResponse(long objectId, OMInstance taskDocument, List<long> selectedAttributeIds, GraphFactorsData response)
        {
            var errorIds = new List<long>();
            response.GraphFactors?.ForEach(factor =>
            {
                try
                {
                    var attributeType = RegisterAttributeType.DECIMAL;
                    var attributeName = CreateAttributeName(factor);
                

                    var attribute = GetAttribute(attributeName);
                    _log.ForContext("HasInDataBase", attribute != null).Verbose("Обработка атрибута с именем {AttributeName}.", attributeName);
                    if (attribute == null)
                    {
                        attribute = CreateAttribute(attributeName, attributeType);
                    }

                    if (selectedAttributeIds.Contains(attribute.Id))
                        SaveFactor(objectId, attribute.Id, attributeType, factor, taskDocument);
                }
                catch (Exception ex)
                {
                    long errorId = ErrorManager.LogError(ex);
                    errorIds.Add(errorId);
                }
            });

            return errorIds;
        }

        //private RegisterAttributeType GetFactorType(object factorValue)
        //{
        //    if (factorValue == null)
        //        return RegisterAttributeType.STRING;

        //    if (decimal.TryParse(factorValue.ToString(), out var number))
        //        return RegisterAttributeType.DECIMAL;

        //    if (DateTime.TryParse(factorValue.ToString(), out var date))
        //        return RegisterAttributeType.DATE;

        //    return RegisterAttributeType.STRING;
        //}

        private string CreateAttributeName(GraphFactor factor)
        {
            return string.Join(AttributeNameSeparator, new List<string> {factor.LayerSourceName, factor.LayerTargetName, factor.FactorName});
        }

        private OMAttribute GetAttribute(string attributeName)
        {
            return OMAttribute.Where(x => x.RegisterId == ReonSourceRegisterId && x.Name == attributeName)
                .Select(x => x.Id)
                .ExecuteFirstOrDefault();
        }

        private OMAttribute CreateAttribute(string attributeName, RegisterAttributeType type)
        {
            OMAttribute omAttribute;
            using (var ts = new TransactionScope())
            {
                omAttribute = RegisterAttributeService.CreateRegisterAttribute(attributeName,
                    ReonSourceRegisterId, type, false);

                var dbConfigurator = RegisterConfigurator.GetDbConfigurator();
                RegisterConfigurator.CreateDbColumnForRegister(omAttribute, dbConfigurator);

                ts.Complete();
            }

            RegisterCache.UpdateCache(0, null);
            _log.Debug("Создан атрибут {attributeName} с Id {AttributeId}.", attributeName, omAttribute.Id);

            return omAttribute;
        }

        private void SaveFactor(long objectId, long attributeId, RegisterAttributeType attributeType,
            GraphFactor factor, OMInstance taskDocument)
        {
            _log.ForContext("HasInCache", RegisterCache.RegisterAttributes.TryGetValue(attributeId, out var _))
                .Verbose("Обработка атрибута с Id {AttributeId}.", attributeId);

            var gbuObjectAttribute = new GbuObjectAttribute
            {
                ObjectId = objectId,
                AttributeId = attributeId,
                NumValue = factor.FactorValue,
                S = taskDocument.CreateDate,
                Ot = taskDocument.CreateDate,
                ChangeDocId = taskDocument.Id,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now
            };

            //if (factor.FactorValue != null)
            //{
            //    switch (attributeType)
            //    {
            //        case RegisterAttributeType.STRING:
            //            gbuObjectAttribute.StringValue = factor.FactorValue.ToString();
            //            break;
            //        case RegisterAttributeType.DECIMAL:
            //            gbuObjectAttribute.NumValue = decimal.Parse(factor.FactorValue.ToString());
            //            break;
            //        case RegisterAttributeType.DATE:
            //            gbuObjectAttribute.DtValue = DateTime.Parse(factor.FactorValue.ToString());
            //            break;
            //    }
            //}

            gbuObjectAttribute.Save();
        }

        private void AddRowToReport(GbuReportService gbuReportService, string cadastralNumber, bool isSuccessful, string errorMessage)
        {
	        var row = gbuReportService.GetCurrentRow();
            var isSuccessfulStr = isSuccessful ? "Да" : "Нет";
            gbuReportService.AddRow(row, new List<string>{cadastralNumber, isSuccessfulStr, errorMessage});
        }

        #endregion


        #region Entities

        public class FactorsFromReonRequest
        {
            public string CadastralNumber { get; set; }

            public DateTime? EstimationDate { get; set; }

            public FactorsFromReonRequest(string cadastralNumber, DateTime? estimationDate)
            {
                CadastralNumber = cadastralNumber;
                EstimationDate = estimationDate;
            }
        }

        #endregion
    }
}
