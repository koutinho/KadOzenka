using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class NumberOfObjectsByGroupsService
	{
		private readonly StatisticalDataService _statisticalDataService;

		public NumberOfObjectsByGroupsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
		}

        public List<NumberOfObjectsByGroupsDto> GetNumberOfObjectsByGroups(long[] taskList, bool isOksReportType)
        {
	        var conditions = new List<QSCondition>();
	        if (isOksReportType)
            {
	            conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
			            QSConditionType.NotEqual, (long) PropertyTypes.Stead));
	            conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
			            QSConditionType.NotEqual, (long) PropertyTypes.None));
            }
	        else
	        {
		        conditions.Add(new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code),
				        QSConditionType.Equal, (long)PropertyTypes.Stead));
            }

	        var groupJoin = new QSJoin
	        {
		        RegisterId = OMGroup.GetRegisterId(),
		        JoinCondition = new QSConditionSimple
		        {
			        ConditionType = QSConditionType.Equal,
			        LeftOperand = OMUnit.GetColumn(x => x.GroupId),
			        RightOperand = OMGroup.GetColumn(x => x.Id)
		        },
		        JoinType = QSJoinType.Left
	        };

            var query = _statisticalDataService.GetQueryForUnitsByTasks(taskList, conditions,
		        new List<QSJoin>() {groupJoin});

            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "Group"));

            var subQuery = new QSQuery(OMGroup.GetRegisterId())
            {
                Columns = new List<QSColumn>
                {
                    OMGroup.GetColumn(x => x.GroupName)
                },
                Condition = new QSConditionGroup(QSConditionGroupType.And)
                {
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(
                            OMGroup.GetColumn(x => x.Id),
                            QSConditionType.Equal,
                            OMGroup.GetColumn(x => x.ParentId)){
                            RightOperandLevel = 1
                        }
                    }
                }
            };
            query.AddColumn(subQuery, "ParentGroup");

            var table = query.ExecuteQuery();

            var result = new List<NumberOfObjectsByGroupsDto>();
            if (table.Rows.Count != 0)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
	                var group = table.Rows[i]["Group"].ParseToStringNullable();
	                var parentGroup = table.Rows[i]["ParentGroup"].ParseToStringNullable();

                    var dto = new NumberOfObjectsByGroupsDto
                    {
                        PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
                        Group = string.IsNullOrEmpty(group) ? "Без группы" : group,
                        HasGroup = !string.IsNullOrEmpty(group),
                        ParentGroup = string.IsNullOrEmpty(parentGroup) ? "Без группы" : parentGroup,
                        HasParentGroup = !string.IsNullOrEmpty(parentGroup)
                    };
                    result.Add(dto);
                }
            }

            result =
                result.GroupBy(x => new { x.PropertyType, x.Group, x.HasGroup, x.ParentGroup, x.HasParentGroup }).Select(
                group => new NumberOfObjectsByGroupsDto
                {
                    PropertyType = group.Key.PropertyType,
                    Group = group.Key.Group,
                    HasGroup = group.Key.HasGroup,
                    ParentGroup = group.Key.ParentGroup,
                    HasParentGroup = group.Key.HasParentGroup,
                    Count = group.ToList().Count
                }).OrderBy(x =>  x.HasParentGroup).ThenBy(x => x.HasGroup).ToList();

            return result;
        }
    }
}
