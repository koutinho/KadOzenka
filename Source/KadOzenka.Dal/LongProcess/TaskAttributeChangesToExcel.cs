using System;
using System.IO;
using System.Threading;
using Core.Main.FileStorages;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.Tasks;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess
{
    public class TaskAttributeChangesToExcel : LongProcess
    {
        public const string LongProcessName = nameof(TaskAttributeChangesToExcel);

        public static void AddProcessToQueue(TaskAttributeChangesParams taskParams)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, parameters: taskParams.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            var taskParams = processQueue.Parameters.DeserializeFromXml<TaskAttributeChangesParams>();

            var svc = new TaskService();
            var stream = svc.DataMappingToExcelAlt(taskParams.KOTaskId);
            stream.Seek(0, SeekOrigin.Begin);
            var fsName = "DataExporterByTemplate";
            var dt = DateTime.Today;
            FileStorageManager.Save(stream, fsName, dt, $"{taskParams.KOTaskId}_TaskAttributeChanges.xlsx");

            if (taskParams.UserId != null)
                new MessageService().SendMessages(new MessageDto
                {
                    Addressers = new MessageAddressersDto {UserIds = new long[] {taskParams.UserId.Value}},
                    Subject = $"Выгрузка изменений атрибутов по задаче с идентификатором {taskParams.KOTaskId}",
                    //Message = $@"Процесс формирования отчета завершен. <a href="">Скачать результаты</a>",
                    IsUrgent = true,
                    IsEmail = false,
                    ExpireDate = DateTime.Now.AddHours(2)
                });
            WorkerCommon.SetProgress(processQueue, 100);
        }

        public class TaskAttributeChangesParams
        {
            public long KOTaskId;
            public int? UserId;
        }
    }
}