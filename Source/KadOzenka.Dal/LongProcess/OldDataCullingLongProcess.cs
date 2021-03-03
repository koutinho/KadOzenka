using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Register;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using ObjectModel.Core;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Shared;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
    public class OldDataCullingLongProcess : ILongProcess
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ExportAttributeToKoProcess>();

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            Log.Information("Запуск процесса очистки старых данных из системы");
            Clear();
            processQueue.Progress = 100;
            Log.Information("Завершение процесса очистки старых данных из системы");
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
            Log.ForContext("ErrorId", errorId).Error(ex, "Ошибка фонового процесса. ID объекта {objectId}", objectId);
        }

        public bool Test()
        {
            return true;
        }

        private TimeSpan errorLogRetain = TimeSpan.FromDays(7);
        private TimeSpan messagesRetain = TimeSpan.FromDays(30);
        private TimeSpan processQueueRetain = TimeSpan.FromDays(30);

        private void Clear()
        {
            var errorLogLastRetainDay = DateTime.Today - errorLogRetain;
            var messagesLastRetainDay = DateTime.Today - messagesRetain;
            var processQueueLastRetainDay = DateTime.Today - processQueueRetain;

            CleanErrorLogs(errorLogLastRetainDay);

            CleanLongProcessQueue(processQueueLastRetainDay);

            CleanMessages(messagesLastRetainDay);
        }

        /// <summary>
        /// Очистка таблиц core_messages, core_messages_to
        /// </summary>
        /// <param name="lastRetainDay"></param>
        private static void CleanMessages(DateTime lastRetainDay)
        {
            var messages = OMMessage.Where(x => x.WasSended < lastRetainDay).Select(x => x.Id).Execute()
                .Select(x => x.Id).ToList();
            var messageRegisterId = OMMessage.GetRegisterId();
            Log.Information("Найдено {messageCount} сообщений для удаления", messages.Count);

            var messageToRegisterId = OMMessageTo.GetRegisterId();
            var messageToIdColumn = OMMessageTo.GetColumn(x => x.MessageId);
            var messageWasSendedColumn = OMMessage.GetColumn(x => x.WasSended);
            var query = new QSQuery
            {
                MainRegisterID = messageToRegisterId,
                Condition = new QSConditionSimple
                {
                    ConditionType = QSConditionType.In,
                    LeftOperand = messageToIdColumn,
                    RightOperand = new QSColumnQuery
                    {
                        SubQuery = new QSQuery
                        {
                            MainRegisterID = messageRegisterId,
                            Condition = new QSConditionSimple
                            {
                                ConditionType = QSConditionType.Less,
                                LeftOperand = messageWasSendedColumn,
                                RightOperand = new QSColumnConstant(lastRetainDay)
                            },
                            Columns = new List<QSColumn> {OMMessage.GetColumn(x => x.Id)}
                        }
                    }
                }
            };
            var messageTo = query.ExecuteQuery<MessageToIdDto>();
            var messageToForRemoval = messageTo.Select(x => x.Id).ToList();
            Log.Information("Найдено {messageToCount} сообщений на почту для удаления", messageToForRemoval.Count);

            RemoveRecords(messages, messageRegisterId);
            RemoveRecords(messageToForRemoval, messageToRegisterId);
        }


        /// <summary>
        /// Очистка таблиц core_long_process_queue, core_long_process_log
        /// </summary>
        /// <param name="lastRetainDay"></param>
        private static void CleanLongProcessQueue(DateTime lastRetainDay)
        {
            var queues = OMQueue.Where(x => x.CreateDate < lastRetainDay).Select(x => x.Id).Execute().Select(x => x.Id)
                .ToList();
            ;
            var queueRegisterId = OMQueue.GetRegisterId();
            Log.Information("Найдено {queuesCount} процессов в очереди для удаления", queues.Count);
            RemoveRecords(queues, queueRegisterId);
        }

        /// <summary>
        /// Очистка таблицы core_error_log
        /// </summary>
        /// <param name="lastRetainDay"></param>
        private static void CleanErrorLogs(DateTime lastRetainDay)
        {
            var errorLogs = OMErrorLog.Where(x => x.ErrorDate < lastRetainDay).Select(x => x.Id).Execute()
                .Select(x => x.Id).ToList();
            var registerId = OMErrorLog.GetRegisterId();
            Log.Information("Найдено {errorLogCount} логов ошибок для удаления", errorLogs.Count);
            RemoveRecords(errorLogs, registerId);
        }

        private static void RemoveRecords(List<long> errorLogs, int registerId)
        {
            var objectList = errorLogs.Select(x => new RegisterObject(registerId, x.ParseToInt())).ToList();
            RegisterStorage.Destroy(objectList);
        }

        private class MessageToIdDto
        {
            public long Id { get; set; }
        }
    }
}