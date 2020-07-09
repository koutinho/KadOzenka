using System;
using System.Collections.Generic;
using ObjectModel.Core.LongProcess;
using System.Threading;
using System.Transactions;
using CadAppraisalDataApi.Models;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Registers;
using KadOzenka.WebClients;
using KadOzenka.WebClients.ReonClient.Api;
using ObjectModel.Core.Register;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;
using Platform.Configurator;

namespace KadOzenka.Dal.LongProcess
{
    public class KoFactorsFromReon : LongProcess
    {
        private const long REOIN_SOURCE_REGISTER_ID = 44355304;
        private RosreestrDataApi ReonWebClientService { get; set; }
        private RegisterAttributeService RegisterAttributeService { get; set; }
        private GbuReportService GbuReportService { get; set; }

        public KoFactorsFromReon()
        {
            ReonWebClientService = new RosreestrDataApi();
            RegisterAttributeService = new RegisterAttributeService();
            GbuReportService = new GbuReportService();
        }


        public static void AddProcessToQueue(long taskId)
        {
            LongProcessManager.AddTaskToQueue(nameof(KoFactorsFromReon), objectId: taskId, registerId: OMTask.GetRegisterId());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            if (!processQueue.ObjectId.HasValue)
            {
                WorkerCommon.SetMessage(processQueue, Consts.Consts.MessageForProcessInterruptedBecauseOfNoObjectId);
                WorkerCommon.SetProgress(processQueue, Consts.Consts.ProgressForProcessInterruptedBecauseOfNoObjectId);
                return;
            }

            var task = GetTask(processQueue.ObjectId.Value);
            var document = GetDocument(task.DocumentId);
            var units = GetUnits(task.Id);

            var errorIds = new List<long>();
            bool isError = false;
            long total = units.Count;
            long success = 0;
            long errors = 0;

            GbuReportService.AddHeaders(0, new List<string> { "Кадастровый номер", "Успешно", "Ошибка" });

            units.ForEach(unit =>
            {
                if (!unit.ObjectId.HasValue)
                    return;

                try
                {
                    var request = GetRequest(task, unit);
                    var response = ReonWebClientService.RosreestrDataGetGraphFactorsByCadNum(request.CadastralNumber, request.EstimationDate);
                    var currentErrorIds = ProcessServiceResponse(unit.ObjectId.Value, document, response);
                    if (currentErrorIds?.Count > 0)
                    {
                        errors++;
                        isError = true;
                        errorIds.AddRange(currentErrorIds);
                        AddRowToReport(unit.CadastralNumber, false, $"Ошибка загрузки (журнал: {string.Join(", ", currentErrorIds)})");
                    }
                    else
                    {
                        success++;
                        AddRowToReport(unit.CadastralNumber, true, string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    errors++;
                    isError = true;
                    var errorId = ErrorManager.LogError(ex);
                    AddRowToReport(unit.CadastralNumber, false, $"Ошибка загрузки (журнал: {errorId})");
                }
            });

            var info = isError
                ? $"При загрузке факторов возникли ошибки. Всего: {total}; Успешно: {success}; Ошибки: {errors}"
                : $"Загрузка факторов выполнена без ошибок. Всего: {total}; Успешно: {success}; Ошибки: {errors}";

            processQueue.Message = info;
            processQueue.Save();

            var reportId = GbuReportService.SaveReport("Получение графических факторов из ИС РЕОН");
            var message = $"{info}\n" + $@"<a href=""/DataExport/DownloadExportResult?exportId={reportId}"">Скачать результат</a>";
            var roleId = ReonServiceConfig.Current.RoleIdForNotification?.ParseToLongNullable();
            NotificationSender.SendNotification(processQueue, "Получение графических факторов из ИС РЕОН завершено", message, roleId);

            WorkerCommon.SetProgress(processQueue, 100);
        }


        #region Support Methods

        private OMTask GetTask(long taskId)
        {
            return OMTask.Where(x => x.Id == taskId)
                .Select(x => x.EstimationDate)
                .Select(x => x.DocumentId)
                .ExecuteFirstOrDefault();
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
            return new FactorsFromReonRequest(unit.CadastralNumber, task.EstimationDate);
        }

        public List<long> ProcessServiceResponse(long objectId, OMInstance taskDocument, GraphFactorsData response)
        {
            var errorIds = new List<long>();
            response.GraphFactors?.ForEach(factor =>
            {
                try
                {
                    var attributeType = RegisterAttributeType.DECIMAL;
                    var attributeName = CreateAttributeName(factor);

                    var attribute = GetAttribute(attributeName);
                    if (attribute == null)
                        attribute = CreateAttribute(attributeName, attributeType);

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
            return string.Join(" - ", new List<string> {factor.LayerSourceName, factor.LayerTargetName, factor.FactorName});
        }

        private OMAttribute GetAttribute(string attributeName)
        {
            return OMAttribute.Where(x => x.RegisterId == REOIN_SOURCE_REGISTER_ID && x.Name == attributeName)
                .Select(x => x.Id)
                .ExecuteFirstOrDefault();
        }

        private OMAttribute CreateAttribute(string attributeName, RegisterAttributeType type)
        {
            OMAttribute omAttribute;
            using (var ts = new TransactionScope())
            {
                omAttribute = RegisterAttributeService.CreateRegisterAttribute(attributeName,
                    REOIN_SOURCE_REGISTER_ID, type, false);

                var dbConfigurator = RegisterConfigurator.GetDbConfigurator();
                RegisterConfigurator.CreateDbColumnForRegister(omAttribute, dbConfigurator);

                ts.Complete();
            }

            RegisterCache.UpdateCache(0, null);

            return omAttribute;
        }

        private void SaveFactor(long objectId, long attributeId, RegisterAttributeType attributeType,
            GraphFactor factor, OMInstance taskDocument)
        {
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

        private void AddRowToReport(string cadastralNumber, bool isSuccessful, string errorMessage)
        {
            var isSuccessfulStr = isSuccessful ? "Да" : "Нет";
            GbuReportService.AddRow(new List<string>{cadastralNumber, isSuccessfulStr, errorMessage});
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
