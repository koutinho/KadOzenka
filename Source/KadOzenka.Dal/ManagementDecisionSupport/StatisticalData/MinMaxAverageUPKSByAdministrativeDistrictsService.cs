using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class MinMaxAverageUPKSByAdministrativeDistrictsService
	{
		private readonly StatisticalDataService _statisticalDataService;

		public MinMaxAverageUPKSByAdministrativeDistrictsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
		}

		public List<MinMaxAverageUPKSByAdministrativeDistrictsDto> GetMinMaxAverageUPKSByAdministrativeDistricts(long[] taskList, MinMaxAverageUPKSByAdministrativeDistrictsType reportType)
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
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.Region_Code, "Region_Code"));
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.District_Code, "District_Code"));
			query.AddColumn(OMUnit.GetColumn(x => x.PropertyType, "PropertyType"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));

			var table = query.ExecuteQuery();

			var data = new List<MinMaxAverageUPKSByAdministrativeDistrictsObjectDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new MinMaxAverageUPKSByAdministrativeDistrictsObjectDto
					{
						PropertyType = table.Rows[i]["PropertyType"].ParseToString(),
						ObjectValue = table.Rows[i]["ObjectUpks"].ParseToDecimalNullable(),
						//TODO: ObjectWeigth MUST BE CLARIFIED
						ObjectWeigth = 1
					};

					switch (reportType)
					{
						case MinMaxAverageUPKSByAdministrativeDistrictsType.ByDistricts:
							dto.Name = ((Hunteds)table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle();
							break;
						case MinMaxAverageUPKSByAdministrativeDistrictsType.ByCarastralRegions:
							dto.Name = _statisticalDataService.GetRegionNumberByCadastralQuarter(table.Rows[i]["CadastralQuartal"].ParseToString());
							break;
						case MinMaxAverageUPKSByAdministrativeDistrictsType.ByRegions:
							dto.Name = ((Districts)table.Rows[i]["Region_Code"].ParseToLong()).GetEnumDescription();
							dto.AdditionalName = ((Hunteds)table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle();
							break;
					}

					data.Add(dto);
				}
			}

			var dataGrouped =
				data.GroupBy(x => new { x.Name, x.PropertyType });

			var result = new List<MinMaxAverageUPKSByAdministrativeDistrictsDto>();
			foreach (var @group in dataGrouped)
			{
				var groupValues = @group.ToList();
				var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>();
				foreach (var upksCalcType in upksCalcTypes)
				{
					var dto = new MinMaxAverageUPKSByAdministrativeDistrictsDto
					{
						AdditionalName = groupValues.First().AdditionalName,
						Name = @group.Key.Name,
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
