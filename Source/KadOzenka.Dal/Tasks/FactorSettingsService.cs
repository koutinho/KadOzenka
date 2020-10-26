using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Tasks.Dto;
using ObjectModel.Core.Register;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tasks
{
	public class FactorSettingsService
	{
		public List<FactorSettingsDto> GetFactorSettings()
		{
			var query = new QSQuery
			{
				MainRegisterID = OMFactorSettings.GetRegisterId(),
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMAttribute.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMFactorSettings.GetColumn(x => x.FactorId),
							RightOperand = OMAttribute.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					}
				}
			};
			query.AddColumn(OMAttribute.GetColumn(x => x.Name, nameof(FactorSettingsDto.FactorName)));
			query.AddColumn(OMFactorSettings.GetColumn(x => x.Id, nameof(FactorSettingsDto.Id)));
			query.AddColumn(OMFactorSettings.GetColumn(x => x.Source, nameof(FactorSettingsDto.Source)));
			query.AddColumn(OMFactorSettings.GetColumn(x => x.Inheritance_Code, nameof(FactorSettingsDto.FactorInheritance)));

			var correctFactorNameSubQuery = new QSQuery(OMAttribute.GetRegisterId())
			{
				Columns = new List<QSColumn>
				{
					OMAttribute.GetColumn(x => x.Name)
				},
				Condition = new QSConditionGroup(QSConditionGroupType.And)
				{
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(
							OMAttribute.GetColumn(x => x.Id),
							QSConditionType.Equal,
							OMFactorSettings.GetColumn(x => x.CorrectFactorId)){
							RightOperandLevel = 1
						}
					}
				}
			};
			query.AddColumn(correctFactorNameSubQuery, nameof(FactorSettingsDto.CorrectFactorName));

			var result = new List<FactorSettingsDto>();
			var table = query.ExecuteQuery();
			for (var i = 0; i < table.Rows.Count; i++)
			{
				var row = table.Rows[i];
				result.Add(new FactorSettingsDto
				{
					Id = row[nameof(FactorSettingsDto.Id)].ParseToLong(),
					FactorName = row[nameof(FactorSettingsDto.FactorName)].ParseToStringNullable(),
					FactorInheritance = (FactorInheritance)row[nameof(FactorSettingsDto.FactorInheritance)].ParseToLong(),
					Source = row[nameof(FactorSettingsDto.Source)].ParseToStringNullable(),
					CorrectFactorName = row[nameof(FactorSettingsDto.CorrectFactorName)].ParseToStringNullable(),
				});
			}

			return result;
		}
	}
}
