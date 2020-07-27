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
	public class MinMaxAverageUPKSByCadastralQuartersService
	{
		private readonly StatisticalDataService _statisticalDataService;

        public MinMaxAverageUPKSByCadastralQuartersService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
		}

        public List<MinMaxAverageUPKSByCadastralQuartersDto> GetMinMaxAverageUPKS(long[] taskList)
        {
	        var quartalDictionaryJoin = new QSJoin
            {
	            RegisterId = OMQuartalDictionary.GetRegisterId(),
	            JoinCondition = new QSConditionSimple
	            {
		            ConditionType = QSConditionType.Equal,
		            LeftOperand = OMUnit.GetColumn(x => x.CadastralBlock),
		            RightOperand = OMQuartalDictionary.GetColumn(x => x.CadastralQuartal)
	            },
	            JoinType = QSJoinType.Inner
            };

            var query = _statisticalDataService.GetQueryForUnitsByTasks(taskList,
	            additionalJoins: new List<QSJoin> { quartalDictionaryJoin });

            query.AddColumn(OMQuartalDictionary.GetColumn(x => x.CadastralQuartal, "CadastralQuartal"));
            query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
            query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));
            query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "ObjectCost"));
            query.AddColumn(OMUnit.GetColumn(x => x.Square, "ObjectSquare"));

            var table = query.ExecuteQuery();

            var data = new List<MinMaxAverageUPKSByCadastralQuartersObjectDto>();
            if (table.Rows.Count != 0)
            {
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var dto = new MinMaxAverageUPKSByCadastralQuartersObjectDto
                    {
                        CadastralRegionNumber = _statisticalDataService.GetRegionNumberByCadastralQuarter(table.Rows[i]["CadastralQuartal"].ParseToString()),
                        CadastralQuater = table.Rows[i]["CadastralQuartal"].ParseToString(),
                        PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
                        ObjectValue = table.Rows[i]["ObjectUpks"].ParseToDecimalNullable(),
                        ObjectCost = table.Rows[i]["ObjectCost"].ParseToDecimalNullable(),
                        ObjectSquare = table.Rows[i]["ObjectSquare"].ParseToDecimalNullable(),
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
                        UpksCalcValue = _statisticalDataService.GetCalcValue(upksCalcType, groupValues)
                    };

                    result.Add(dto);
                }
            }

            return result;
        }
    }
}
