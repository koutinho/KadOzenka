using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using DevExpress.DataProcessing;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
	public class GeneralizedIndicatorsService
	{
		private readonly StatisticalDataService _statisticalDataService;

		public GeneralizedIndicatorsService(StatisticalDataService statisticalDataService)
		{
			_statisticalDataService = statisticalDataService;
		}

		public List<GeneralizedIndicatorsDto> GetData(long[] taskIdList, StatisticDataAreaDivisionType areaDivisionType)
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
			var query = _statisticalDataService.GetQueryForUnitsByTasks(taskIdList, additionalJoins:
				new List<QSJoin> { quartalDictionaryJoin, groupJoin });

			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.CadastralQuartal, "CadastralQuartal"));
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.District_Code, "District_Code"));
			query.AddColumn(OMQuartalDictionary.GetColumn(x => x.Region_Code, "RegionCode"));
			query.AddColumn(OMUnit.GetColumn(x => x.Upks, "ObjectUpks"));

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
							OMGroup.GetColumn(x => x.ParentId))
						{
							RightOperandLevel = 1
						}
					}
				}
			};
			query.AddColumn(subQuery, "ParentGroup");

			var table = query.ExecuteQuery();
			var data = new List<GeneralizedIndicatorsObjectDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new GeneralizedIndicatorsObjectDto
					{
						GroupName = string.IsNullOrEmpty(table.Rows[i]["ParentGroup"].ParseToStringNullable())
							? "Без группы"
							: table.Rows[i]["ParentGroup"].ParseToStringNullable(),
						HasGroup = !string.IsNullOrEmpty(table.Rows[i]["ParentGroup"].ParseToStringNullable()),
						ObjectValue = table.Rows[i]["ObjectUpks"].ParseToDecimalNullable(),
						//TODO: ObjectWeigth MUST BE CLARIFIED
						ObjectWeigth = 1
					};
					switch (areaDivisionType)
					{
						case StatisticDataAreaDivisionType.Districts:
							dto.Name = ((Hunteds)table.Rows[i]["District_Code"].ParseToLong()).GetShortTitle();
							break;
						case StatisticDataAreaDivisionType.Regions:
							dto.Name = ((Districts)table.Rows[i]["RegionCode"].ParseToLong()).GetEnumDescription();
							break;
						case StatisticDataAreaDivisionType.RegionNumbers:
							dto.Name = _statisticalDataService.GetRegionNumberByCadastralQuarter(
								table.Rows[i]["CadastralQuartal"].ParseToString());
							break;
						case StatisticDataAreaDivisionType.Quarters:
							dto.AdditionalName = _statisticalDataService.GetRegionNumberByCadastralQuarter(
								table.Rows[i]["CadastralQuartal"].ParseToString());
							dto.Name = table.Rows[i]["CadastralQuartal"].ParseToString();
							break;
						default:
							throw new NotSupportedException();
					}

					data.Add(dto);
				}
			}

			var result = new List<GeneralizedIndicatorsDto>();
			var upksCalcTypes = System.Enum.GetValues(typeof(UpksCalcType)).Cast<UpksCalcType>().Where(x => x != UpksCalcType.Average);

			var dataByNameGrouped = data.GroupBy(x => x.Name );
			foreach (var nameGroup in dataByNameGrouped)
			{
				var nameGroupValues = nameGroup.ToList();
				nameGroupValues.GroupBy(x => new{x.GroupName, x.HasGroup})
					.OrderBy(x => x.Key.HasGroup)
					.ForEach(x =>
					{
						foreach (var upksCalcType in upksCalcTypes)
						{
							var dto = new GeneralizedIndicatorsDto
							{
								Name = nameGroup.Key,
								AdditionalName = nameGroupValues.FirstOrDefault()?.AdditionalName,
								ObjectsCount = nameGroupValues.Count,
								GroupName = x.Key.GroupName,
								UpksCalcType = upksCalcType,
								UpksCalcValue =
									_statisticalDataService.GetCalcValue(upksCalcType, x.ToList())
							};
							result.Add(dto);
						}
					});
			}

			var dataByGroupNameGrouped = data.GroupBy(x => new { x.GroupName, x.HasGroup })
				.OrderBy(x => x.Key.HasGroup);
			foreach (var groupNameGroup in dataByGroupNameGrouped)
			{
				var groupValues = groupNameGroup.ToList();
				foreach (var upksCalcType in upksCalcTypes)
				{
					var dto = new GeneralizedIndicatorsDto
					{
						Name = "Итого",
						AdditionalName = "Итого",
						ObjectsCount = data.Count,
						GroupName = groupNameGroup.Key.GroupName,
						UpksCalcType = upksCalcType,
						UpksCalcValue =
							_statisticalDataService.GetCalcValue(upksCalcType, groupValues)
					};
					result.Add(dto);
				}
			}

			return result;
		}
	}
}
