using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.Tasks.Dto;
using ObjectModel.Core.TD;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tasks
{
	public class TaskService
    {
        public TaskDto GetTaskById(long taskId)
        {
            var task = OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();
            if (task == null)
                return null;

            var responseDocument = GetDocumentById(task.ResponseDocId);
            var incomingDocument = GetDocumentById(task.DocumentId);
            var tour = GetTourById(task.TourId);

            return new TaskDto
            {
                Id = task.Id,
                CreationDate = task.CreationDate,
                EstimationDate = task.EstimationDate,
                Tour = tour,
                NoteType = task.NoteType_Code,
                Status = task.Status,
                ResponseDocument = responseDocument,
                IncomingDocument = incomingDocument
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
		    query.AddColumn(OMInstance.GetColumn(x => x.CreateDate, nameof(TaskDocumentInfoDto.DocumentCreateDate)));
		    query.AddColumn(OMInstance.GetColumn(x => x.RegNumber, nameof(TaskDocumentInfoDto.DocumentRegNumber)));

		    return query.ExecuteQuery<TaskDocumentInfoDto>();
		}

        public List<TaskDto> GetTasksByTour(long tourId)
        {
            var tasks = OMTask.Where(x => x.TourId == tourId)
                .Select(x => x.Id)
                .Select(x => x.DocumentId)
                .Execute();

            var result = new List<TaskDto>();

            tasks.ForEach(x => result.Add(new TaskDto
            {
                Id = x.Id,
                IncomingDocument = GetDocumentById(x.DocumentId)
            }));

            return result;
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

        #endregion
    }
}
