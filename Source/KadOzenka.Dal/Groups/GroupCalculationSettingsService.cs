using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Groups.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Groups
{
	public class GroupCalculationSettingsService
	{
		public List<GroupCalculationSettingsDto> GetCalculationSettings(long tourId, bool isParcel)
		{
			var calculationSettings = GetGroupCalculationSettings(tourId, isParcel);
			var tourSubGroups = OMGroup.GetListGroupTour(tourId, isParcel ? KoGroupAlgoritm.MainParcel : KoGroupAlgoritm.MainOKS);
			foreach (var tourSubGroup in tourSubGroups)
			{
				if (calculationSettings.All(x => x.GroupId != tourSubGroup.Id))
				{
					var setting = CreateCalculationSettingsForGroup(tourSubGroup.Id, calculationSettings);
					if (setting != null)
					{
						calculationSettings.Add(new GroupCalculationSettingsDto
						{
							Id = setting.Id,
							GroupId = tourSubGroup.Id,
							GroupName = tourSubGroup.GroupName,
							GroupNumber = tourSubGroup.Number,
							Priority = (int)setting.NumberPriority,
							Stage1 = setting.CalcStage1,
							Stage2 = setting.CalcStage2,
							Stage3 = setting.CalcStage3
						});
					}
				}
			}

			return calculationSettings.OrderBy(x => x.Priority).ToList();
		}

		public OMAutoCalculationSettings CreateCalculationSettingsForGroup(long groupId, List<GroupCalculationSettingsDto> existingCalculationSettings = null)
		{
			OMAutoCalculationSettings setting = null;
			var group = OMGroup.Where(x => x.Id == groupId).SelectAll().ExecuteFirstOrDefault();
			if (group != null)
			{
				OMGroup parentGroup = OMGroup.Where(x => x.Id == group.ParentId).SelectAll().ExecuteFirstOrDefault();
				if (parentGroup != null && (parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainOKS || parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainParcel))
				{
					var tourId = OMTourGroup.Where(x => x.GroupId == groupId).Select(x => x.TourId)
						.ExecuteFirstOrDefault().TourId;
					if (existingCalculationSettings == null)
					{
						existingCalculationSettings = GetGroupCalculationSettings(tourId, parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainParcel);
					}

					setting = new OMAutoCalculationSettings
					{
						IdGroup = groupId,
						IdTour = tourId,
						CalcParcel = parentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.MainParcel,
						CalcStage1 = false,
						CalcStage2 = false,
						CalcStage3 = false
					};
					var priority = GetGroupSettingPriority(group.Number, existingCalculationSettings);
					setting.NumberPriority = priority;
					setting.Save();

					UpdateCalculationSettingsPriorities(existingCalculationSettings, priority);
				}
			}

			return setting;
		}

		public void SaveCalculationSettings(List<GroupCalculationSettingsDto> settings)
		{
			if (settings.Count == 0)
				return;

			var ids = settings.Select(x => x.Id);
			var omSettings = OMAutoCalculationSettings.Where(x => ids.Contains(x.Id)).SelectAll().Execute();
			omSettings.ForEach(currentSetting =>
			{
				var dto = settings.First(x => x.Id == currentSetting.Id);

				currentSetting.CalcStage1 = dto.Stage1;
				currentSetting.CalcStage2 = dto.Stage2;
				currentSetting.CalcStage3 = dto.Stage3;
				currentSetting.NumberPriority = dto.Priority;
				currentSetting.Save();
			});
		}

		private List<GroupCalculationSettingsDto> GetGroupCalculationSettings(long tourId, bool isParcel)
		{
			var query = new QSQuery
			{
				MainRegisterID = OMAutoCalculationSettings.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition>
					{
						new QSConditionSimple(OMAutoCalculationSettings.GetColumn(x => x.IdTour), QSConditionType.Equal, tourId),
						new QSConditionSimple
						{
							LeftOperand = OMAutoCalculationSettings.GetColumn(x => x.CalcParcel.Coalesce(false)),
							ConditionType = QSConditionType.Equal,
							RightOperand = new QSColumnConstant(isParcel)
						}
					}
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMGroup.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMGroup.GetColumn(x => x.Id),
							RightOperand = OMAutoCalculationSettings.GetColumn(x => x.IdGroup)
						},
						JoinType = QSJoinType.Inner
					}
				}
			};
			query.AddColumn(OMGroup.GetColumn(x => x.GroupName, nameof(GroupCalculationSettingsDto.GroupName)));
			query.AddColumn(OMGroup.GetColumn(x => x.Id, nameof(GroupCalculationSettingsDto.GroupId)));
			query.AddColumn(OMGroup.GetColumn(x => x.Number, nameof(GroupCalculationSettingsDto.GroupNumber)));
			query.AddColumn(OMAutoCalculationSettings.GetColumn(x => x.Id, nameof(GroupCalculationSettingsDto.Id)));
			query.AddColumn(OMAutoCalculationSettings.GetColumn(x => x.NumberPriority, nameof(GroupCalculationSettingsDto.Priority)));
			query.AddColumn(OMAutoCalculationSettings.GetColumn(x => x.CalcStage1, nameof(GroupCalculationSettingsDto.Stage1)));
			query.AddColumn(OMAutoCalculationSettings.GetColumn(x => x.CalcStage2, nameof(GroupCalculationSettingsDto.Stage2)));
			query.AddColumn(OMAutoCalculationSettings.GetColumn(x => x.CalcStage3, nameof(GroupCalculationSettingsDto.Stage3)));

			var table = query.ExecuteQuery();
			var result = new List<GroupCalculationSettingsDto>();
			if (table.Rows.Count != 0)
			{
				for (var i = 0; i < table.Rows.Count; i++)
				{
					var dto = new GroupCalculationSettingsDto
					{
						Id = table.Rows[i][nameof(GroupCalculationSettingsDto.Id)].ParseToLong(),
						Priority = table.Rows[i][nameof(GroupCalculationSettingsDto.Priority)].ParseToInt(),
						Stage1 = table.Rows[i][nameof(GroupCalculationSettingsDto.Stage1)].ParseToBoolean(),
						Stage2 = table.Rows[i][nameof(GroupCalculationSettingsDto.Stage2)].ParseToBoolean(),
						Stage3 = table.Rows[i][nameof(GroupCalculationSettingsDto.Stage3)].ParseToBoolean(),
						GroupId = table.Rows[i][nameof(GroupCalculationSettingsDto.GroupId)].ParseToLong(),
						GroupName = table.Rows[i][nameof(GroupCalculationSettingsDto.GroupName)].ParseToString(),
						GroupNumber = table.Rows[i][nameof(GroupCalculationSettingsDto.GroupNumber)].ParseToString()
					};

					result.Add(dto);
				}
			}

			return result;
		}

		private int GetGroupSettingPriority(string combinedNumber, List<GroupCalculationSettingsDto> existingCalculationSettings)
		{
			var priority = (int?)null;

			var subgroupNumber = GetSubGroupNumber(combinedNumber);
			var parentGroupNumber = GetParentGroupNumber(combinedNumber);
			if (parentGroupNumber.HasValue)
			{
				var settingsByParentGroupsDict = existingCalculationSettings.GroupBy(x => x.ParentGroupNumberInt ?? int.MinValue)
					.OrderBy(x => x.Key)
					.ToDictionary(x => x.Key, x => x.OrderBy(y => y.GroupNumberInt).ToList());
				if (settingsByParentGroupsDict.ContainsKey(parentGroupNumber.Value))
				{
					if (subgroupNumber.HasValue)
					{
						var groupSettingsWithNumbers = settingsByParentGroupsDict[parentGroupNumber.Value]
							.Where(x => x.GroupNumberInt.HasValue).ToList();

						if (groupSettingsWithNumbers.IsEmpty() ||
							subgroupNumber < groupSettingsWithNumbers.First().GroupNumberInt)
						{
							priority = settingsByParentGroupsDict[parentGroupNumber.Value].Min(x => x.Priority);
						}
						else
						{
							var prevNumber = (int?)null;
							var prevPriority = (int?)null;
							foreach (var calculationSettingsDto in groupSettingsWithNumbers)
							{
								if (subgroupNumber == calculationSettingsDto.GroupNumberInt)
								{
									priority = calculationSettingsDto.Priority + 1;
									break;
								}

								if (subgroupNumber > prevNumber &&
									subgroupNumber < calculationSettingsDto.GroupNumberInt)
								{
									priority = prevPriority + 1;
									break;
								}

								prevNumber = calculationSettingsDto.GroupNumberInt;
								prevPriority = calculationSettingsDto.Priority;
							}

							if (!priority.HasValue)
							{
								priority = groupSettingsWithNumbers.Last().Priority + 1;
							}
						}
					}
					else
					{
						priority =
							settingsByParentGroupsDict[parentGroupNumber.Value].Max(x => x.Priority) + 1;
					}
				}
				else
				{
					var parentGroupKeysWithNumbers =
						settingsByParentGroupsDict.Keys.Where(x => x != int.MinValue).ToList();

					if (parentGroupKeysWithNumbers.IsEmpty() ||
						parentGroupNumber < parentGroupKeysWithNumbers.First())
					{
						priority = 0;
					}
					else
					{
						var prevKey = (int?)null;
						foreach (var currentKey in parentGroupKeysWithNumbers)
						{
							if (parentGroupNumber > prevKey && parentGroupNumber < currentKey)
							{
								priority = settingsByParentGroupsDict[prevKey.Value].Max(x => x.Priority) + 1;
								break;
							}

							prevKey = currentKey;
						}

						if (!priority.HasValue)
						{
							priority = settingsByParentGroupsDict[parentGroupKeysWithNumbers.Last()].Max(x => x.Priority) + 1;
						}
					}
				}
			}
			else
			{
				var lastPriority = existingCalculationSettings.OrderBy(x => x.Priority).LastOrDefault()?.Priority;
				priority = lastPriority + 1 ?? 0;
			}

			return priority.Value;
		}

		private void UpdateCalculationSettingsPriorities(List<GroupCalculationSettingsDto> calculationSettings, int newSettingPriority)
		{
			foreach (var groupSetting in calculationSettings)
			{
				if (groupSetting.Priority >= newSettingPriority)
				{
					groupSetting.Priority++;
					var omSettings = OMAutoCalculationSettings.Where(x => x.Id == groupSetting.Id).SelectAll()
						.ExecuteFirstOrDefault();
					omSettings.NumberPriority = groupSetting.Priority;
					omSettings.Save();
				}
			}
		}

		private int? GetSubGroupNumber(string fullNumber)
		{
			var subGroupNumberStr = fullNumber?.Split('.')?.ElementAtOrDefault(1);
			if (int.TryParse(subGroupNumberStr, out var result))
			{
				return result;
			}
			return null;
		}

		private int? GetParentGroupNumber(string fullNumber)
		{
			var groupNumberStr = fullNumber?.Split('.')?.ElementAtOrDefault(0);
			if (int.TryParse(groupNumberStr, out var result))
			{
				return result;
			}
			return null;
		}
	}
}
