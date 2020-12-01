using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.TD;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataComparing
{
	public class FileInfo
	{
		public FileInfo(string fileName)
		{
			FileName = System.IO.Path.GetFileName(fileName);
			FileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(FileName);
			SystemType = FileNameWithoutExtension.EndsWith(HandleFileName(DataComparingConfig.Current.RsmSuffix))
				? SystemType.Rsm
				: FileNameWithoutExtension.EndsWith(HandleFileName(DataComparingConfig.Current.PkkoSuffix))
					? SystemType.Pkko
					: SystemType.Unsupported;
			if (SystemType != SystemType.Unsupported)
				FileNameWithoutSystemSuffix = FileNameWithoutExtension.TrimEnd(SystemType == SystemType.Rsm
					? DataComparingConfig.Current.NameSeparator + HandleFileName(DataComparingConfig.Current.RsmSuffix)
					: DataComparingConfig.Current.NameSeparator + HandleFileName(DataComparingConfig.Current.PkkoSuffix));
		}

		public string FileName { get; }
		public string FileNameWithoutExtension { get; }
		public SystemType SystemType { get; }
		public string FileNameWithoutSystemSuffix { get; }

		public OMTask GetTaskInfoFromFileName()
		{
			var filePrefix = HandleFileName(DataComparingConfig.Current.FilePrefix);
			var fileWithoutPrefix = FileNameWithoutSystemSuffix.TrimStart(filePrefix + DataComparingConfig.Current.NameSeparator);

			var fileNameParts = fileWithoutPrefix.Split(DataComparingConfig.Current.NameSeparator, StringSplitOptions.RemoveEmptyEntries).ToList();
			if (fileNameParts.Count < 3)
				throw new Exception(
					"Не удалось получить сведения о задании на оценку из имени файла: название файла не удовлетворяет правилам формирования");

			var taskNumber = string.Join(DataComparingConfig.Current.NameSeparator, fileNameParts.Take(fileNameParts.Count -2));
			var estimationDateString = fileNameParts[fileNameParts.Count - 2];
			var approveDateString = fileNameParts[fileNameParts.Count - 1];
			DateTime.TryParseExact(estimationDateString, DataComparingConfig.DateFormat,
				System.Globalization.CultureInfo.InvariantCulture,
				System.Globalization.DateTimeStyles.None, out var estimationDate);
			DateTime.TryParseExact(approveDateString, DataComparingConfig.DateFormat,
				System.Globalization.CultureInfo.InvariantCulture,
				System.Globalization.DateTimeStyles.None, out var approveDate);

			var task = GetTask(taskNumber, estimationDate, approveDate);
			if (task == null)
			{
				throw new Exception(
					"Не удалось получить сведения о задании на оценку из имени файла: задание на оценку не найдено");
			}

			return task;
		}

		private OMTask GetTask(string taskNum, DateTime estimationDate, DateTime approveDate)
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

		public static string GetFileName(OMTask task, OMInstance document)
		{
			var fileName = DataComparingConfig.Current.FilePrefix + DataComparingConfig.Current.NameSeparator + HandleFileName(document.RegNumber) + DataComparingConfig.Current.NameSeparator +
				   task.EstimationDate?.ToString(DataComparingConfig.DateFormat) + DataComparingConfig.Current.NameSeparator +
				   document.ApproveDate?.ToString(DataComparingConfig.DateFormat) + DataComparingConfig.Current.NameSeparator + DataComparingConfig.Current.RsmSuffix + ".xlsx";

			return HandleFileName(fileName);
		}

		private static string HandleFileName(string fileName)
		{
			string invalid = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
			foreach (char c in invalid)
			{
				fileName = fileName.Replace(c.ToString(), DataComparingConfig.Current.NameSeparator);
			}

			return fileName;
		}
	}
}
