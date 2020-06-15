using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class AdditionalFormsService
	{
		private readonly StatisticalDataService _statisticalDataService;

		public AdditionalFormsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
		}

		public List<ChangesUploadingDto> GetChangesUploadingData(long[] taskIdList)
		{
			var unitChangeJoin = new QSJoin
			{
				RegisterId = OMUnitChange.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.Id),
					RightOperand = OMUnitChange.GetColumn(x => x.UnitId)
				},
				JoinType = QSJoinType.Inner
			};
			var taskJoin = new QSJoin
			{
				RegisterId = OMUnitChange.GetRegisterId(),
				JoinCondition = new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMUnit.GetColumn(x => x.TaskId),
					RightOperand = OMTask.GetColumn(x => x.Id)
				},
				JoinType = QSJoinType.Inner
			};

			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, additionalJoins: new List<QSJoin> {unitChangeJoin, taskJoin});
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralNumber, "CadastralNumber"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.StatusRepeatCalc, "StatusRepeatCalc"));
			query.AddColumn(OMTask.GetColumn(x => x.CreationDate, "CreationDate"));
			query.AddColumn(OMUnitChange.GetColumn(x => x.OldValue, "OldValue"));
			query.AddColumn(OMUnitChange.GetColumn(x => x.NewValue, "NewValue"));
			query.AddColumn(OMUnitChange.GetColumn(x => x.ChangeStatus, "ChangeStatus"));

			var table = query.ExecuteQuery();
			var result = new List<ChangesUploadingDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new ChangesUploadingDto
					{
						CadastralNumber = table.Rows[i]["CadastralNumber"].ParseToString(),
						ChangedDate = table.Rows[i]["CreationDate"].ParseToDateTimeNullable(),
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						Status = table.Rows[i]["StatusRepeatCalc"].ParseToString(),
						OldValue = table.Rows[i]["OldValue"].ParseToString(),
						NewValue = table.Rows[i]["NewValue"].ParseToString(),
						Changing = table.Rows[i]["ChangeStatus"].ParseToString(),
					};

					result.Add(dto);
				}
			}

			return result;
		}
	}
}
