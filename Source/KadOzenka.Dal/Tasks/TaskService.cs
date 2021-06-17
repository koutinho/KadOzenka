using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.Tasks.Dto;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.TD;
using ObjectModel.KO;
using Core.Shared.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Transactions;
using Core.Register;
using Core.Shared.Misc;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataComparing;
using KadOzenka.Dal.DataComparing.Exceptions;
using KadOzenka.Dal.DataComparing.StorageManagers;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Models.Task;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using KadOzenka.Dal.Tasks.Responses;
using KadOzenka.Dal.Tours;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Shared;
using ObjectModel.Directory;
using Serilog;
using OMAttachment = ObjectModel.Core.TD.OMAttachment;

namespace KadOzenka.Dal.Tasks
{
    public class TaskService : ITaskService
    {
	    private static readonly ILogger _log = Log.ForContext<TaskService>();

        public DocumentService DocumentService { get; set; }
        public TourFactorService TourFactorService { get; }
        public RecycleBinService RecycleBinService { get; }
        private IRosreestrRegisterService RosreestrRegisterService { get; }

        public TaskService()
        {
            DocumentService = new DocumentService();
            TourFactorService = new TourFactorService();
            RecycleBinService = new RecycleBinService();
            RosreestrRegisterService = new RosreestrRegisterService();
        }

        public TaskDto GetTaskById(long taskId)
        {
            var task = OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
                return null;

            var responseDocument = GetDocumentById(task.ResponseDocId);
            var incomingDocument = GetDocumentById(task.DocumentId);
            var tour = GetTourById(task.TourId);

            FillNumbersOfImportedAndPossibleTotalObjects(taskId, out var commonNumberOfImportedObjects, out var possibleTotalCountOfObjects);

            return new TaskDto
            {
                Id = task.Id,
                CreationDate = task.CreationDate,
                EstimationDate = task.EstimationDate,
                Tour = tour,
                NoteType = task.NoteType_Code,
                Status = task.Status,
                StatusCode = task.Status_Code,
                ResponseDocument = responseDocument,
                IncomingDocument = incomingDocument,
                CommonNumberOfImportedObjects = commonNumberOfImportedObjects,
                PossibleTotalCountOfObjects = possibleTotalCountOfObjects
            };
        }

        public List<TaskDocumentInfoDto> GetTaskDocumentInfoList()
        {
            var query = new QSQuery
            {
                MainRegisterID = OMTask.GetRegisterId(),
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMInstance.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMTask.GetColumn(x => x.DocumentId),
                            RightOperand = OMInstance.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
            };
            query.AddColumn(OMTask.GetColumn(x => x.Id, nameof(TaskDocumentInfoDto.TaskId)));
            query.AddColumn(OMTask.GetColumn(x => x.TourId, nameof(TaskDocumentInfoDto.TourId)));
            query.AddColumn(OMTask.GetColumn(x => x.EstimationDate, nameof(TaskDocumentInfoDto.EstimationDate)));
            query.AddColumn(OMInstance.GetColumn(x => x.CreateDate, nameof(TaskDocumentInfoDto.DocumentCreateDate)));
            query.AddColumn(OMInstance.GetColumn(x => x.RegNumber, nameof(TaskDocumentInfoDto.DocumentRegNumber)));
            query.AddColumn(OMTask.GetColumn(x => x.NoteType, nameof(TaskDocumentInfoDto.KoNoteType)));

            return query.ExecuteQuery<TaskDocumentInfoDto>();
        }

        public List<TaskDocumentInfoDto> GetTasksByTour(long tourId)
        {
            return GetTaskDocumentInfoList().Where(x => x.TourId == tourId).ToList();
        }

        public List<TaskDocumentInfoDto> GetTasksByTour(List<long?> tourIds)
        {
            return GetTaskDocumentInfoList().Where(x => tourIds.Contains(x.TourId)).ToList();
        }

        public List<DataMappingDto> FetchGbuData(long objectId, OMTask task)
        {
            var list = new List<DataMappingDto>();
            var reg = Core.Register.RegisterCache.GetAttributeDataList(2);
            var attrTypes = new List<RegisterAttributeType>
                {RegisterAttributeType.STRING, RegisterAttributeType.DATE, RegisterAttributeType.DECIMAL};
            foreach (var attribute in reg)
            {
                if (!attrTypes.Contains(attribute.Type))
                    continue;

                var postfix = attribute.Id;

                string sql = $@"select DISTINCT ON (object_id, s, ot) object_id, value, s, ot from gbu_source2_a_{postfix}
                    where object_id={objectId} and change_doc_id={task.DocumentId} order by ot desc limit 1
                ";

                DbCommand command = DBMngr.Main.GetSqlStringCommand(sql);
                DataTable dt = DBMngr.Main.ExecuteDataSet(command).Tables[0];

                if (dt.Rows.Count == 0) continue;

                var newRecords = new List<DataMappingDto>();
                foreach (DataRow row in dt.Rows)
                {
                    newRecords.Add(new DataMappingDto
                    {
                        ObjectId = row["object_id"].ParseToLong(),
                        AttributeId = postfix.ParseToLong(),
                        Value = row["value"].ParseToString()
                    });
                }

                //предыдущие значения
                DateTime oldDate = dt.Rows[0]["ot"].ParseToDateTime();

                foreach (var attributeClass in newRecords)
                {
                    attributeClass.Attribute = Core.Register.RegisterCache
                        .GetAttributeData((int) attributeClass.AttributeId).Name;

                    sql = $@"select value from gbu_source2_a_{postfix}
                        where object_id={objectId} 
                            and ot < to_timestamp('{oldDate.ToString("dd-MM-yyyy HH:mm:ss")}','dd-mm-yyyy HH24:MI:ss')
                            and change_doc_id<>{task.DocumentId}
                        order by ot desc
                        limit 1;";
                    command = DBMngr.Main.GetSqlStringCommand(sql);
                    attributeClass.OldValue = DBMngr.Main.ExecuteScalar(command)?.ToString();

                    list.Add(attributeClass);
                }
            }

            return list;
        }

        public Stream TaskAttributeChangesToExcel(long taskId)
        {
            var gbuObjectService = new GbuObjectService();
            OMTask task = OMTask.Where(x => x.Id == taskId)
                .SelectAll()
                .ExecuteFirstOrDefault();

            if (task == null)
            {
                throw new Exception("Не найдено задание на оценку с ИД=" + taskId);
            }

            var units = OMUnit.Where(x => x.TaskId == taskId).SelectAll().Execute();

            var objectIdList = units.Where(x => x.ObjectId.HasValue).Select(x=>x.ObjectId.Value).ToList();
            // список кадастровых номеров
            var objectCadNumList = units.Select(x => new {x.ObjectId, x.CadastralNumber});
            // список всех изменений атрибутов по списку объектов
            var attributesList = gbuObjectService.GetAllAttributes(objectIdList);
            // атрибуты с привязкой к КН
            var res = objectCadNumList.Join(attributesList, x => x.ObjectId, y => y.ObjectId, (x, y) => new
            {
                x.CadastralNumber,
                x.ObjectId,
                Value = GetVal(y.NumValue, y.DtValue, y.StringValue),
                y.AttributeId,
                AttrName = y.AttributeData.Name,
                y.Ot,
                y.S,
                y.ChangeDocId,
                y.ChangeDate
            });

            // лист с данными по выбранному документу
            var changeDoc = res.Where(x => x.ChangeDocId == task.DocumentId).ToList();

            var doc = OMInstance.Where(x => x.Id == task.DocumentId).SelectAll().ExecuteFirstOrDefault();

            // лист с данными по всем атрибутам, фильтр до даты документа
            var allAttrib = res.Where(x => x.Ot < (doc.ApproveDate ?? doc.CreateDate))
                .Where(x=>x.Value!=null)
                .OrderByDescending(x=>x.Ot)
                .GroupBy(x=>x.AttributeId)
                .Select(x=>x.FirstOrDefault())
                .ToList();

            var history =
                (from cDoc in changeDoc
                join attr in allAttrib
                    on (cDoc.AttributeId, cDoc.ObjectId) equals (attr.AttributeId, attr.ObjectId)
                    into intermediateResult
                from inRes in intermediateResult.DefaultIfEmpty()
                select new
                {
                    cDoc,
                    OldValue = inRes?.Value ?? String.Empty
                }).ToList();

            var excelFile = new ExcelFile();
            excelFile.Worksheets.Add("Изменения");
            var sheet = excelFile.Worksheets[0];

            DataExportCommon.AddRow(sheet,0,new object[]{"КН","Атрибут","Старое значение", "Новое значение"});
            var rowCounter = 1;

            foreach (var attribute in history)
            {
                DataExportCommon.AddRow(sheet,rowCounter,new object[]
                {
                    attribute.cDoc.CadastralNumber,
                    attribute.cDoc.AttrName,
                    attribute.OldValue,
                    attribute.cDoc.Value
                });
                rowCounter++;
            }

            var ms = new MemoryStream();
            excelFile.Save(ms,SaveOptions.XlsxDefault);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;

            string GetVal(decimal? dec, DateTime? dt, string str)
            {
                if (dec.HasValue) return dec.ToString();
                if (dt.HasValue) return dt.ToString();
                return str;
            }
        }

        public bool CheckIfInitial(long taskId)
        {
            var task = OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();
            return task.NoteType_Code == KoNoteType.Initial;
        }

        public string GetTemplateForTaskName(TaskDocumentInfoDto x)
        {
            return GetTemplateForTaskName(x.EstimationDate, x.DocumentCreateDate, x.DocumentRegNumber, x.KoNoteType);
        }

        public string GetTemplateForTaskName(long taskId)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMTask.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.Equal, taskId)
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMInstance.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMTask.GetColumn(x => x.DocumentId),
                            RightOperand = OMInstance.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
            };
            query.AddColumn(OMTask.GetColumn(x => x.Id, nameof(TaskDocumentInfoDto.TaskId)));
            query.AddColumn(OMTask.GetColumn(x => x.TourId, nameof(TaskDocumentInfoDto.TourId)));
            query.AddColumn(OMTask.GetColumn(x => x.EstimationDate, nameof(TaskDocumentInfoDto.EstimationDate)));
            query.AddColumn(OMInstance.GetColumn(x => x.CreateDate, nameof(TaskDocumentInfoDto.DocumentCreateDate)));
            query.AddColumn(OMInstance.GetColumn(x => x.RegNumber, nameof(TaskDocumentInfoDto.DocumentRegNumber)));
            query.AddColumn(OMTask.GetColumn(x => x.NoteType, nameof(TaskDocumentInfoDto.KoNoteType)));

            var taskList =  query.ExecuteQuery<TaskDocumentInfoDto>();
            if (taskList.IsEmpty())
            {
                throw new Exception($"Не найдено задание на оценку с ИД {taskId}");
            }

            return GetTemplateForTaskName(taskList.First());
        }

        public Dictionary<long, string> GetTemplatesForTaskName(List<long> taskIds)
        {
            if(taskIds.IsEmpty())
                return new Dictionary<long, string>();

            var query = new QSQuery
            {
                MainRegisterID = OMTask.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskIds.Distinct().Select(x => (double) x).ToList())
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMInstance.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMTask.GetColumn(x => x.DocumentId),
                            RightOperand = OMInstance.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
            };
            query.AddColumn(OMTask.GetColumn(x => x.Id, nameof(TaskDocumentInfoDto.TaskId)));
            query.AddColumn(OMTask.GetColumn(x => x.TourId, nameof(TaskDocumentInfoDto.TourId)));
            query.AddColumn(OMTask.GetColumn(x => x.EstimationDate, nameof(TaskDocumentInfoDto.EstimationDate)));
            query.AddColumn(OMInstance.GetColumn(x => x.CreateDate, nameof(TaskDocumentInfoDto.DocumentCreateDate)));
            query.AddColumn(OMInstance.GetColumn(x => x.RegNumber, nameof(TaskDocumentInfoDto.DocumentRegNumber)));
            query.AddColumn(OMTask.GetColumn(x => x.NoteType, nameof(TaskDocumentInfoDto.KoNoteType)));

            var taskList = query.ExecuteQuery<TaskDocumentInfoDto>();

            return taskList.ToDictionary(x => x.TaskId, GetTemplateForTaskName);
        }

        public static string GetTemplateForTaskName(DateTime? estimationDate, DateTime? documentCreationDate, string documentRegNumber, string koNoteType)
        {
            return $"{estimationDate?.ToShortDateString()}, {documentCreationDate?.ToShortDateString()}, {documentRegNumber}, {koNoteType}";
        }

        public void UpdateTaskData(TaskDto dto)
        {
            var task = OMTask.Where(x => x.Id == dto.Id).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
            {
                throw new Exception($"Не найдено задание на оценку с ИД {dto.Id}");
            }
            if (dto.Tour?.Id != null && !OMTour.Where(x => x.Id == dto.Tour.Id).ExecuteExists())
            {
                throw new Exception($"Не найден тур с ИД {dto.Tour.Id}");
            }

            using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.RequiresNew))
            {
                task.CreationDate = dto.CreationDate;
                task.EstimationDate = dto.EstimationDate;
                task.NoteType_Code = dto.NoteType.GetValueOrDefault();
                task.Status_Code = dto.StatusCode.GetValueOrDefault();
                task.TourId = dto.Tour?.Id;

                var document = OMInstance.Where(x => x.Id == task.DocumentId).SelectAll().ExecuteFirstOrDefault();
                if (document == null)
                {
                    var documentDto = new Documents.Dto.DocumentDto
                    {
                        RegNumber = dto.IncomingDocument.RegNumber,
                        Description = dto.IncomingDocument.Description,
                        CreateDate = dto.IncomingDocument.CreationDate,
                        ApproveDate = dto.IncomingDocument.ApproveDate
                    };
                    var documentId = DocumentService.AddDocument(documentDto);
                    task.DocumentId = documentId;
                }
                else
                {
                    document.RegNumber = dto.IncomingDocument.RegNumber;
                    document.Description = dto.IncomingDocument.Description;
                    document.CreateDate = dto.IncomingDocument.CreationDate.GetValueOrDefault();
                    document.ApproveDate = dto.IncomingDocument.ApproveDate.GetValueOrDefault();
                    document.Save();
                }
                task.Save();

                ts.Complete();
            }
        }

        #region Data Comparing

        public TaskDataComparingDtoResponse TryGetTaskDataComparingDto(long taskId)
        {
	        try
	        {
		        var task = GetTask(taskId);
		        var dto = new TaskDataComparingDto
		        {
			        Id = taskId,
			        NoteType = task.NoteType_Code,
			        DataComparingTaskChangesStatusCode = task.DataChangesComparingStatus_Code,
			        DataComparingCadastralCostStatusCode = task.CadastralCostComparingStatus_Code
		        };

		        if (task.CadastralCostComparingStatus_Code ==
		            KoDataComparingCadastralCostStatus.ThereAreUnitCostsInconsistencies)
			        dto.ContainsFdFilesComparingResult =
				        CadastralCostDataComparingStorageManager.ContainsResultFdFile(task);

		        dto.IsTaskChangesPkkoFileUploaded =
			        TaskChangesDataComparingStorageManager.IsTaskChangesPkkoFileUploaded(task);
		        dto.AreCostPkkoFilesUploaded = CadastralCostDataComparingStorageManager.AreCostPkkoFilesUploaded(task);
		        dto.AreFdPkkoFilesUploaded = CadastralCostDataComparingStorageManager.AreFdPkkoFilesUploaded(task);

		        return new TaskDataComparingDtoResponse {Success = true, TaskDataComparingDto = dto};
	        }
	        catch (DataComparingException ex)
	        {
		        _log.Error(ex, "Не удалось получить данные о сравнении для задания на оценку {TaskId}", taskId);
		        return new TaskDataComparingDtoResponse { Success = false, ErrorMessage = ex.Message};
            }
	        catch (Exception ex)
	        {
		        _log.Error(ex, "Не удалось получить данные о сравнении для задания на оценку {TaskId}", taskId);
		        return new TaskDataComparingDtoResponse { Success = false };
            }
        }

        public FileStream DownloadTaskChangesDataComparingResult(long taskId)
        {
            var task = GetTask(taskId);
            return TaskChangesDataComparingStorageManager.GetResultFile(task);
        }

        public FileStream DownloadTaskCadastralCostDataComparingResult(long taskId, bool downloadFDResult = false)
        {
            var task = GetTask(taskId);
            return CadastralCostDataComparingStorageManager.GetResultFile(task, downloadFDResult);
        }

        public void UploadDataComparingTaskChangesPkkoFile(long taskId, Stream stream)
        {
            var task = GetTask(taskId);
            TaskChangesDataComparingStorageManager.SaveTaskChangesPkkoFile(stream, task);
        }

        public void UploadDataComparingCostPkkoFiles(long taskId, DisposableList<Stream> streamList)
        {
            var task = GetTask(taskId);
            CadastralCostDataComparingStorageManager.AddNewPkkoCostFiles(task, streamList);
        }

        public void UploadDataComparingFdPkkoFiles(long taskId, DisposableList<Stream> streamList)
        {
            var task = GetTask(taskId);
            CadastralCostDataComparingStorageManager.AddNewPkkoFdFiles(task, streamList);
        }

        public GbuReportService.ReportFile DownloadTaskDataComparingPkkoFile(long taskId, DataComparingFileType downloadType)
        {
            var task = GetTask(taskId);

            GbuReportService.ReportFile file;
            if (downloadType == DataComparingFileType.TaskChangesPkkoFile)
            {
                file = TaskChangesDataComparingStorageManager.GetTaskChangesPkkoFile(task);
            }
            else
            {
                file = downloadType == DataComparingFileType.CostPkkoFiles
                    ? CadastralCostDataComparingStorageManager.GetTaskPkkoFiles(task, loadFdFiles: false)
                    : CadastralCostDataComparingStorageManager.GetTaskPkkoFiles(task, loadCostFiles: false);
            }

            return file;
        }

        #endregion Data Comparing

        #region Support Methods

        private DocumentDto GetDocumentById(long? documentId)
        {
            if (documentId == null)
                return null;

            var document = OMInstance.Where(x => x.Id == documentId).SelectAll().ExecuteFirstOrDefault();
            if (document == null)
                return null;

            return new DocumentDto
            {
                Id = document.Id,
                RegNumber = document.RegNumber,
                Description = document.Description,
                CreationDate = document.CreateDate,
                ApproveDate = document.ApproveDate
            };
        }

        private TourDto GetTourById(long? tourId)
        {
            if (tourId == null)
                return null;

            var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
                return null;

            return new TourDto
            {
                Id = tour.Id,
                Year = tour.Year
            };
        }

        private static void FillNumbersOfImportedAndPossibleTotalObjects(long taskId, out long? commonNumberOfImportedObjects,
            out long? possibleTotalCountOfObjects)
        {
            var taskObjectImports = OMImportDataLog
                .Where(x => x.RegisterId == OMTask.GetRegisterId() && x.ObjectId == taskId)
                .SelectAll()
                .Execute()
                .Where(x => x.FileExtension == "xlsx" || x.FileExtension == "xml")
                .ToList();

            commonNumberOfImportedObjects = taskObjectImports.Sum(x => x.NumberOfImportedObjects);

            if (taskObjectImports.Count == 0)
            {
                possibleTotalCountOfObjects = 0;
            }
            else
            {
                var importsWithKnownTotalNumberOfObjects = taskObjectImports.Where(x => x.TotalNumberOfObjects != null).ToList();
                if (importsWithKnownTotalNumberOfObjects.Count == 0)
                {
                    possibleTotalCountOfObjects = 0;
                }
                else if (importsWithKnownTotalNumberOfObjects.Count < taskObjectImports.Count)
                {
                    var averageTotalObjectCountPerFile = importsWithKnownTotalNumberOfObjects.Sum(x => x.TotalNumberOfObjects) /
                                                         importsWithKnownTotalNumberOfObjects.Count;
                    var countOfImportsWithUnknownTotalNumber =
                        taskObjectImports.Count - importsWithKnownTotalNumberOfObjects.Count;

                    possibleTotalCountOfObjects = importsWithKnownTotalNumberOfObjects.Sum(x => x.TotalNumberOfObjects)
                                                          + averageTotalObjectCountPerFile * countOfImportsWithUnknownTotalNumber;
                }
                else
                {
                    possibleTotalCountOfObjects = taskObjectImports.Sum(x => x.TotalNumberOfObjects);
                }
            }
        }

        private OMTask GetTask(long taskId)
        {
	        var task = OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();
	        if (task == null)
		        throw new Exception($"Не найдено задание на оценку с ИД {taskId}");

	        return task;
        }

        #endregion

        public bool CanTaskBeDeleted(long taskId)
        {
            var task = OMTask.Where(x => x.Id == taskId).Select(x => x.Status_Code).ExecuteFirstOrDefault();

            return task != null && task.Status_Code != KoTaskStatus.InWork;
        }

        public void DeleteTask(long taskId, int userId)
        {
	        var dto = GetTaskById(taskId);
	        if (dto == null)
	        {
		        throw new Exception($"Задание на оценку с ИД {taskId} не найдено");
	        }
	        var taskName = GetTemplateForTaskName(dto.EstimationDate, dto.IncomingDocument.CreationDate,
		        dto.IncomingDocument.RegNumber, dto.NoteType.GetEnumDescription());
	        if (dto.StatusCode == KoTaskStatus.InWork)
	        {
		        throw new Exception($"Задание на оценку '{taskName}' не может быть удалено, т.к. имеет статус '{KoTaskStatus.InWork.GetEnumDescription()}'");
	        }

	        var registerObjectsList = new List<RecycleBinService.RegisterObjects>();

	         var query = new QSQuery
	        {
		        MainRegisterID = OMUnit.GetRegisterId(),
		        Condition = new QSConditionSimple(OMUnit.GetColumn(x => x.TaskId), QSConditionType.Equal, taskId),
	        };
	        query.AddColumn(OMUnit.GetColumn(x => x.ObjectId));
	        query.AddPKColumn = false;
	        registerObjectsList.Add(new RecycleBinService.GbuRegisterObjects((int)Registers.GbuRegistersServices.RosreestrRegisterService.Id, dto.IncomingDocument.Id, query.GetSql()));

            var oksFactorRegister = TourFactorService.GetTourRegister(dto.Tour.Id, ObjectType.Oks);
	        var zuFactorRegister = TourFactorService.GetTourRegister(dto.Tour.Id, ObjectType.ZU);
            if(oksFactorRegister != null)
				registerObjectsList.Add(new RecycleBinService.RegisterObjects((int)oksFactorRegister.RegisterId, OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code != PropertyTypes.Stead).GetSql()));
            if(zuFactorRegister != null)
				registerObjectsList.Add(new RecycleBinService.RegisterObjects((int)zuFactorRegister.RegisterId, OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == PropertyTypes.Stead).GetSql()));
	        
	        registerObjectsList.Add(new RecycleBinService.RegisterObjects(OMUnitChange.GetRegisterId(), OMUnitChange.Where(x => x.ParentUnit.TaskId == taskId).GetSql()));
	        registerObjectsList.Add(new RecycleBinService.RegisterObjects(OMCostRosreestr.GetRegisterId(), OMCostRosreestr.Where(x => x.ParentUnit.TaskId == taskId).GetSql()));


            registerObjectsList.Add(new RecycleBinService.RegisterObjects(ObjectModel.Core.Shared.OMAttachment.GetRegisterId(), $@"select att_main.id from core_attachment att_main where att_main.id in (
            select distinct at1.id 
            from core_attachment at1
                left join core_attachment_object at_obj on at1.id=at_obj.attachment_id
            where at_obj.register_id=203 and at_obj.object_id={taskId} 
                and (at1.is_deleted=0 or at1.is_deleted is null)
                and (at_obj.is_deleted=0 or at_obj.is_deleted is null)
                and not exists (select * 
                                from core_attachment at2 
                                    join core_attachment_object at_obj2 on at2.id=at_obj2.attachment_id
                                where at1.id=at2.id and (at_obj2.register_id<>203 or at_obj2.object_id<>{taskId}) 
                                    and (at2.is_deleted=0 or at2.is_deleted is null)
                                    and (at_obj2.is_deleted=0 or at_obj2.is_deleted is null)))"));

            registerObjectsList.Add(new RecycleBinService.RegisterObjects(OMAttachmentObject.GetRegisterId(), $@"
select id from  core_attachment_object
where register_id=203 and object_id={taskId} and (is_deleted=0 or is_deleted is null)
"));


            var importDataLogIds = OMImportDataLog
		        .Where(x => x.RegisterId == OMTask.GetRegisterId() && x.ObjectId == taskId)
		        .Execute().Select(x => x.Id).ToList();

	        var queueIds = new List<long>();
	        if (importDataLogIds.IsNotEmpty())
	        {
		        var importDataLogIdNullable = importDataLogIds.Select(y => (long?) y).ToList();
		        queueIds = OMQueue
			        .Where(x => x.ParentProcessType.ProcessName == DataImporterGknLongProcess.LongProcessName &&
			                    importDataLogIdNullable.Contains(x.ObjectId))
			        .Execute().Select(x => x.Id).ToList();
            }
		       

            if(queueIds.IsNotEmpty())
				registerObjectsList.Add(new RecycleBinService.RegisterObjects(OMQueue.GetRegisterId(), queueIds));
            if(importDataLogIds.IsNotEmpty())
				registerObjectsList.Add(new RecycleBinService.RegisterObjects(OMImportDataLog.GetRegisterId(), importDataLogIds));

            registerObjectsList.Add(new RecycleBinService.RegisterObjects(OMUnit.GetRegisterId(), OMUnit.Where(x => x.TaskId == taskId).GetSql()));
	        registerObjectsList.Add(new RecycleBinService.RegisterObjects(OMTask.GetRegisterId(), new List<long>{taskId}));

	        RecycleBinService.MoveObjectsToRecycleBin(registerObjectsList, OMTask.GetRegisterId(),
		        $"Задание на оценку '{taskName}' Тура '{dto.Tour.Year}'", userId);

        }
    }
}
