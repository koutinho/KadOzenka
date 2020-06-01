using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class MinMaxAverageUPKSByCadastralQuartersService : StatisticalDataService
	{
        public List<MinMaxAverageUPKSByCadastralQuartersDto> GetMinMaxAverageUPKS(long[] taskList)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = new List<QSCondition>
                    {
                        new QSConditionSimple(OMTask.GetColumn(x => x.Id), QSConditionType.In, taskList.Select(x => (double)x).ToList())
                    }
                },
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMTask.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.TaskId),
                            RightOperand = OMTask.GetColumn(x => x.Id)
                        },
                        JoinType = QSJoinType.Inner
                    },
                    new QSJoin
                    {
                        RegisterId = OMQuartalDictionary.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMUnit.GetColumn(x => x.CadastralBlock),
                            RightOperand = OMQuartalDictionary.GetColumn(x => x.CadastralQuartal)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
            };

            query.AddColumn(OMQuartalDictionary.GetColumn(x => x.CadastralQuartal, "CadastralQuartal"));
            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
            query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));

            var table = query.ExecuteQuery();

            var data = new List<MinMaxAverageUPKSByCadastralQuartersObjectDto>();
            if (table.Rows.Count != 0)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var dto = new MinMaxAverageUPKSByCadastralQuartersObjectDto
                    {
                        CadastralRegionNumber = GetRegionNumberByCadastralQuarter(table.Rows[i]["CadastralQuartal"].ParseToString()),
                        CadastralQuater = table.Rows[i]["CadastralQuartal"].ParseToString(),
                        PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
                        ObjectUpks = table.Rows[i]["ObjectUpks"].ParseToDecimalNullable(),
                        //TODO: ObjectWeigth MUST BE CLARIFIED
                        ObjectWeigth = 1
                    };

                    data.Add(dto);
                }
            }

            var dataGrouped =
                data.GroupBy(x => new { x.CadastralQuater, x.PropertyType });

            var result = new List<MinMaxAverageUPKSByCadastralQuartersDto>();
            foreach (var @group in dataGrouped)
            {
                var groupValues = @group.ToList();
                var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>();
                foreach (var upksCalcType in upksCalcTypes)
                {
                    var dto = new MinMaxAverageUPKSByCadastralQuartersDto
                    {
                        CadastralRegionNumber = groupValues.First().CadastralRegionNumber,
                        CadastralQuater = @group.Key.CadastralQuater,
                        ObjectsCount = groupValues.Count,
                        PropertyType = @group.Key.PropertyType,
                        UpksCalcType = upksCalcType,
                        UpksCalcValue = GetUpksCalcValue(upksCalcType, groupValues)
                    };

                    result.Add(dto);
                }
            }

            return result;
        }
    }
}
