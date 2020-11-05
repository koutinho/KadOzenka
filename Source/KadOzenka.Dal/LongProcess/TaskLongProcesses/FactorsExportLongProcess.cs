using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Core.Main.FileStorages;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Tours;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
    public class FactorsExportLongProcess : LongProcess
    {
        public const string LongProcessName = nameof(FactorsExportLongProcess);

        public static void AddProcessToQueue(FactorsDownloadParams param)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, parameters: param.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);
            WorkerCommon.LogState(processQueue, "Старт выгрузки");
            var factorsDownloadParams = processQueue.Parameters.DeserializeFromXml<FactorsDownloadParams>();

            var taskId = factorsDownloadParams.TaskId;
            var attributes = factorsDownloadParams.Attributes;
            var objectType = factorsDownloadParams.IsOks ? ObjectType.Oks : ObjectType.ZU;
            var userId = factorsDownloadParams.UserId;

            var tfs = new TourFactorService();
            var rs = new RegisterService();

            var omAttr = OMAttribute
                .Where(x => x.Id > 1000 && attributes.Contains(x.Id)).SelectAll().Execute();

            var task = OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();

            var factorsReg = tfs.GetTourRegister(task.TourId ?? 0, objectType);
            var factorsAttr = OMAttribute.Where(x => x.RegisterId == factorsReg.RegisterId).SelectAll().Execute();
            var factorsIdAttr = factorsAttr.OrderBy(x => x.Id).FirstOrDefault()?.Id ?? 0;
            var factorsIdColumn = new QSColumnSimple(factorsIdAttr);

            var unitsReg = rs.GetRegister(201);
            var unitsAttr = OMAttribute.Where(x => x.RegisterId == unitsReg.RegisterId).SelectAll().Execute();
            var unitsIdAttr = unitsAttr.OrderBy(x => x.Id).FirstOrDefault()?.Id ?? 0;
            var unitsIdColumn = new QSColumnSimple(unitsIdAttr);

            var taskIdColumn = new QSColumnSimple(20100400);

            var columns = omAttr
                .Select(x => new QSColumnSimple {AttributeID = x.Id})
                .OrderBy(x => x.AttributeID)
                .ToList<QSColumn>();

            WorkerCommon.SetProgress(processQueue, 15);
            WorkerCommon.LogState(processQueue, "Получение данных");

            QSQuery query = new QSQuery
            {
                MainRegisterID = 201,
                Columns = columns,
                JoinType = QSJoinType.Left,
                ManualJoin = true,
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = (int) factorsReg.RegisterId,
                        JoinType = QSJoinType.Left,
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = unitsIdColumn,
                            RightOperand = factorsIdColumn,
                            RightOperandLevel = 1,
                            LeftOperandLevel = 1
                        }
                    }
                },

                Condition = new QSConditionSimple
                {
                    ConditionType = QSConditionType.Equal,
                    LeftOperand = taskIdColumn,
                    RightOperand = new QSColumnConstant(taskId)
                }
            };

            var table = query.ExecuteQuery();

            WorkerCommon.SetProgress(processQueue, 60);
            WorkerCommon.LogState(processQueue, "Формирование эксель файла");

            var excelFile = new ExcelFile();
            excelFile.Worksheets.Add("Факторы");
            var workSheet = excelFile.Worksheets["Факторы"];
            workSheet.InsertDataTable(table, new InsertDataTableOptions
            {
                ColumnHeaders = true
            });
            workSheet.Panes = new WorksheetPanes(PanesState.Frozen, 0, 1, "A2", PanePosition.BottomLeft);

            WorkerCommon.SetProgress(processQueue, 85);
            WorkerCommon.LogState(processQueue, "Сохранение файла");

            // Переименование шапки с идентификаторов атрибутов на них названия
            var headerCells = workSheet.Rows[0].AllocatedCells;
            foreach (var cell in headerCells)
            {
                if (cell.Value.ToString() == "ID")
                {
                    cell.Value = "Идентификатор";
                }

                var attr = omAttr.FirstOrDefault(x => x.Id == cell.Value.ParseToLong());
                cell.Value = attr?.Name ?? cell.Value;
            }

            var ms = new MemoryStream();
            excelFile.Save(ms, SaveOptions.XlsxDefault);
            ms.Seek(0, SeekOrigin.Begin);

            WorkerCommon.SetProgress(processQueue, 90);
            WorkerCommon.LogState(processQueue, "Формирование оповещения");

            var fsName = "DataExporterByTemplate";
            var dt = DateTime.Now;
            FileStorageManager.Save(ms, fsName, dt, $"{taskId}_FactorsExport.xlsx");

            if (userId != null)
                new MessageService().SendMessages(new MessageDto
                {
                    Addressers = new MessageAddressersDto {UserIds = new long[] {userId.Value}},
                    Subject = $"Выгрузка факторов по задаче с идентификатором {taskId}",
                    Message =
                        $@"Выгрузка завершена. <a href=""/Task/DownloadFactorExportResult?taskId={taskId}&dt={dt}"">Скачать результаты</a>",
                    IsUrgent = true,
                    IsEmail = false,
                    ExpireDate = DateTime.Now.AddHours(2)
                });

            WorkerCommon.SetProgress(processQueue, 100);
            WorkerCommon.LogState(processQueue, "Процесс завершен");
        }

        public class FactorsDownloadParams
        {
            public long TaskId;
            public long[] Attributes;
            public bool IsOks;
            public long? UserId;
        }
    }
}