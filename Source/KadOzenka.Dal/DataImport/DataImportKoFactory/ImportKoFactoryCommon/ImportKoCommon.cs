using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using GemBox.Spreadsheet;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImportKoFactory.ImportKoFactoryCommon
{
	public static class ImportKoCommon
	{
		public static QSCondition GetConditionByTaskAndKn(List<long> tasksList, string kn = "")
		{
			var qsGroup = new QSConditionGroup(QSConditionGroupType.And)
				.And(new QSConditionSimple
			{
				LeftOperand = OMUnit.GetColumn(x => x.CadastralNumber),
				RightOperand = new QSColumnConstant(kn),
				ConditionType = QSConditionType.Equal
			});


			var qsGroupTask = new QSConditionGroup(QSConditionGroupType.Or);
			foreach (var task in tasksList)
			{
				qsGroupTask.Add(new QSConditionSimple
				{
					LeftOperand = OMUnit.GetColumn(x => x.TaskId),
					RightOperand = new QSColumnConstant(task),
					ConditionType = QSConditionType.Equal
				});
			}
			qsGroup.Add(qsGroupTask);

			return qsGroup;
		}
	}
}