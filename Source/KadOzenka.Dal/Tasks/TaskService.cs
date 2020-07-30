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
using System.Linq;
using System.Transactions;
using Core.Shared.Misc;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.Models.Task;
using ObjectModel.Common;

namespace KadOzenka.Dal.Tasks
{
    public class TaskService
    {
        public DocumentService DocumentService { get; set; }

        public TaskService()
        {
            DocumentService = new DocumentService();
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

        public void FetchGbuData(List<DataMappingDto> list, long objectId, OMTask task, string postfix)
        {
            string sql = $@"select DISTINCT object_id, attribute_id, value, ot from gbu_source2_a_{postfix}
				where object_id={objectId} and change_doc_id={task.DocumentId}
				";

            DbCommand command = DBMngr.Main.GetSqlStringCommand(sql);
            DataTable dt = DBMngr.Main.ExecuteDataSet(command).Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DataMappingDto
                {
                    ObjectId = row["object_id"].ParseToLong(),
                    AttributeId = row["attribute_id"].ParseToLong(),
                    Value = row["value"].ParseToStringNullable()
                });
            }

            //предыдущие значения
            DateTime oldDate = dt.Rows[0]["ot"].ParseToDateTime();

            foreach (var attributeClass in list)
            {
                attributeClass.Attribute = Core.Register.RegisterCache.GetAttributeData((int)attributeClass.AttributeId).Name;

                sql = $@"select value from gbu_source2_a_{postfix}
						where object_id={objectId} and attribute_id={attributeClass.AttributeId}
							and ot < to_date('{oldDate.ToString("dd-MM-yyyy")}','dd-mm-yyyy')
						order by ot desc
						limit 1;";
                command = DBMngr.Main.GetSqlStringCommand(sql);
                attributeClass.OldValue = DBMngr.Main.ExecuteScalar(command)?.ToString();
            }
        }

        public string GetTemplateForTaskName(TaskDocumentInfoDto x)
        {
            return GetTemplateForTaskName(x.DocumentCreateDate, x.DocumentRegNumber, x.KoNoteType);
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

        public string GetTemplateForTaskName(DateTime? documentCreationDate, string documentRegNumber, string koNoteType)
        {
            return $"{documentCreationDate?.ToShortDateString()}, {documentRegNumber}, {koNoteType}";
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
                        CreateDate = dto.IncomingDocument.CreationDate
                    };
                    var documentId = DocumentService.AddDocument(documentDto);
                    task.DocumentId = documentId;
                }
                else
                {
                    document.RegNumber = dto.IncomingDocument.RegNumber;
                    document.Description = dto.IncomingDocument.Description;
                    document.CreateDate = dto.IncomingDocument.CreationDate.GetValueOrDefault();
                    document.Save();
                }
                task.Save();

                ts.Complete();
            }
        }

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
                CreationDate = document.CreateDate
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

        #endregion
    }
}
