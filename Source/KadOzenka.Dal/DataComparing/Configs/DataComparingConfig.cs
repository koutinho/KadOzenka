using System;
using System.Collections.Generic;
using System.Data;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataComparing.Exceptions;
using KadOzenka.Dal.Tasks;
using ObjectModel.Core.TD;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataComparing.Configs
{
	public class DataComparingConfig
	{
		public const string DateFormat = "ddMMyyyy";

		public string TaskChangesComparingFolder { get; set; }
		public string CadastralCostComparingFolder { get; set; }

		public string NameSeparator => "_";
		public string RsmSuffix => "РСМ";
		public string PkkoSuffix => "ПККО";
		public string ResultSuffix => "Результат";

		public string HandleFileName(string fileName)
		{
			string invalid = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
			foreach (char c in invalid)
			{
				fileName = fileName.Replace(c.ToString(), NameSeparator);
			}

			return fileName;
		}

		protected virtual string ComposeName(OMTask task, OMInstance document, string fileSuffix = null) { throw new InvalidOperationException(); }

		protected string GetNameFromTask(OMTask task, string fileSuffix = null)
		{
			if (!task.EstimationDate.HasValue)
				throw new DataComparingException("Не заполнена Дата оценки.");

			var document = OMInstance.Where(x => x.Id == task.DocumentId).SelectAll().ExecuteFirstOrDefault();
			if (document == null)
				throw new DataComparingException("Не найден связанный с заданием на оценку документ.");
			if (!document.ApproveDate.HasValue)
				throw new DataComparingException("Не заполнена Дата выпуска документа.");

			var name = ComposeName(task, document, fileSuffix);
			return HandleFileName(name);
		}

		protected OMTask GetTask(string taskNum, DateTime estimationDate, DateTime approveDate)
		{
			var query = new QSQuery
			{
				MainRegisterID = OMTask.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(OMTask.GetColumn(x => x.EstimationDate), QSConditionType.GreaterOrEqual,
							estimationDate.Date),
						new QSConditionSimple(OMTask.GetColumn(x => x.EstimationDate), QSConditionType.LessOrEqual,
							estimationDate.GetEndOfTheDay()),
						new QSConditionSimple(OMInstance.GetColumn(x => x.ApproveDate), QSConditionType.GreaterOrEqual,
							approveDate.Date),
						new QSConditionSimple(OMInstance.GetColumn(x => x.ApproveDate), QSConditionType.LessOrEqual,
							approveDate.GetEndOfTheDay()),
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
			query.AddColumn(OMTask.GetColumn(x => x.Id, "TaskId"));
			query.AddColumn(OMInstance.GetColumn(x => x.RegNumber, "DocumentRegNumber"));

			var resultTable = query.ExecuteQuery();
			long taskId = -1;
			foreach (DataRow row in resultTable.Rows)
			{
				var documentRegNumber = row["DocumentRegNumber"].ParseToString();
				if (taskNum == HandleFileName(documentRegNumber))
				{
					taskId = row["TaskId"].ParseToLong();
					break;
				}
			}

			return OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();
		}

		protected string GetTaskTemplateName(OMTask task)
		{
			var document = OMInstance.Where(x => x.Id == task.DocumentId).Select(x => new
			{
				x.CreateDate,
				x.RegNumber
			}).ExecuteFirstOrDefault();
			var taskName = TaskService.GetTemplateForTaskName(task.EstimationDate, document?.CreateDate,
				document?.RegNumber, task.NoteType);
			return taskName;
		}
	}
}
