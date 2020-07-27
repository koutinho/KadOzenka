using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using ObjectModel.Directory;
using ObjectModel.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.Groups
{
    public class GroupService
    {
        public GroupDto GetGroupById(long? groupId)
        {
            var group = GetGroupByIdInternal(groupId);

            var groupType = GetGroupType(group.ParentId);

            return new GroupDto
            {
                Id = group.Id,
                Name = group.GroupName,
                Number = ParseGroupNumber(group.ParentId, group.Number),
				ParentGroupId = group.ParentId,
				GroupAlgorithmCode = group.GroupAlgoritm_Code,
				GroupingAlgorithmId = (long)group.GroupAlgoritm_Code,
                GroupType = groupType
			};
        }

        public List<GroupTreeDto> GetGroups(long? mainParentId = null)
        {
            var query = new QSQuery
            {
                MainRegisterID = OMGroup.GetRegisterId(),
                Joins = new List<QSJoin>
                {
                    new QSJoin
                    {
                        RegisterId = OMTourGroup.GetRegisterId(),
                        JoinCondition = new QSConditionSimple
                        {
                            ConditionType = QSConditionType.Equal,
                            LeftOperand = OMGroup.GetColumn(x => x.Id),
                            RightOperand = OMTourGroup.GetColumn(x => x.GroupId)
                        },
                        JoinType = QSJoinType.Inner
                    }
                }
            };

            query.AddColumn(OMGroup.GetColumn(x => x.ParentId, "ParentId"));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupAlgoritm_Code, "GroupAlgoritm"));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "GroupName"));
            query.AddColumn(OMGroup.GetColumn(x => x.Number, nameof(GroupTreeDto.CombinedNumber)));
            query.AddColumn(OMTourGroup.GetColumn(x => x.TourId, "TourId"));

            var table = query.ExecuteQuery();
            var groups = new List<GroupTreeDto>();

            var oks = new GroupTreeDto
            {
                Id = (long)KoGroupAlgoritm.MainOKS,
                ParentId = mainParentId,
                GroupName = "Основная группа ОКС",
                GroupType = GroupType.Main
            };
            groups.Add(oks);

            var parcel = new GroupTreeDto
            {
                Id = (long)KoGroupAlgoritm.MainParcel,
                ParentId = mainParentId,
                GroupName = "Основная группа Участки",
                GroupType = GroupType.Main
            };
            groups.Add(parcel);

            foreach (DataRow row in table.Rows)
            {
                var str = new GroupTreeDto();

                str.Id = long.Parse(row["Id"].ToString());

                long? parent = long.Parse(row["ParentId"].ToString());
                var groupAlgorithm = long.Parse(row["GroupAlgoritm"].ToString());
                if (parent == -1)
                {
                    if (groupAlgorithm == (long)KoGroupAlgoritm.MainOKS)
                    {
                        str.ParentId = oks.Id;
                    }
                    else if (groupAlgorithm == (long)KoGroupAlgoritm.MainParcel)
                    {
                        str.ParentId = parcel.Id;
                    }

                    str.GroupType = GroupType.Group;
                }
                else
                {
                    str.ParentId = parent;
					if(parent == (long)KoGroupAlgoritm.MainOKS || parent == (long)KoGroupAlgoritm.MainParcel)
						str.GroupType = GroupType.Group;
					else
						str.GroupType = GroupType.SubGroup;
                }

                str.GroupName = row["GroupName"].ToString();
                str.CombinedNumber = row[nameof(GroupTreeDto.CombinedNumber)].ParseToStringNullable();
                str.Number = ParseGroupNumber(str.ParentId, str.CombinedNumber);
                str.TourId = long.Parse(row["TourId"].ToString());

                groups.Add(str);
            }

            return groups;
        }

        public List<GroupTreeDto> GetGroupsTreeForTour(long tourId)
        {
	        var allGroups = GetGroups();
	        var allGroupsInTour = allGroups.Where(x => x.TourId == tourId).ToList();
	        if (allGroupsInTour.Count == 0)
		        return new List<GroupTreeDto>();

	        var subgroups = GetSubgroups(allGroupsInTour);
	        var groupsWithSubGroups = GetGroupsWithSubgroups(subgroups, allGroups);
	        var groupsWithSubGroupsIds = groupsWithSubGroups.Select(x => x.Id).Distinct().ToList();
	        var groupsWithoutSubGroups = allGroupsInTour.Where(x =>
		        (x.ParentId == (long)KoGroupAlgoritm.MainOKS || x.ParentId == (long)KoGroupAlgoritm.MainParcel) &&
		        !groupsWithSubGroupsIds.Contains(x.Id)).ToList();

	        var allTourGroups = new List<GroupTreeDto>();
	        allTourGroups.AddRange(groupsWithoutSubGroups);
	        allTourGroups.AddRange(groupsWithSubGroups);

	        var models = new List<GroupTreeDto>();
	        var mainGroups = GetMainGroups();
            mainGroups.ForEach(mainGroup =>
            {
                var groups = allTourGroups.Where(group => group.ParentId == mainGroup.Id)
                    .OrderBy(group => group.Number)
                    .Select(group =>
                        new GroupTreeDto
                        {
                            Id = group.Id,
                            GroupName = $"{group.CombinedNumber}. {group.GroupName}",
                            GroupType = group.GroupType,
                            TourId = tourId,
                            Items = subgroups.Where(subGroup => subGroup.ParentId == group.Id)
                                .OrderBy(subGroup => subGroup.Number)
                                .Select(subGroup => new GroupTreeDto
                                {
                                    Id = subGroup.Id,
                                    GroupName = $"{subGroup.CombinedNumber}. {subGroup.GroupName}",
                                    GroupType = subGroup.GroupType,
                                    TourId = tourId
                                }).ToList()
                        }).ToList();

                if (groups.Count > 0)
		        {
			        models.Add(new GroupTreeDto
					{
				        Id = mainGroup.Id,
				        GroupName = mainGroup.GroupName,
				        GroupType = mainGroup.GroupType,
				        TourId = tourId,
						Items = groups
			        });
		        }
	        });

	        return models;
        }

        public List<OMGroup> GetGroupsByTasks(List<long> taskIds)
        {
            if (taskIds.Count == 0)
                return new List<OMGroup>();

            var groupIds = OMUnit.Where(x => taskIds.Contains((long) x.TaskId) && x.GroupId != null)
                .GroupBy(x => x.GroupId)
                .Select(x => x.GroupId)
                .SetDistinct()
                .Execute()
                .Select(x => x.GroupId)
                .Distinct()
                .ToList();

            if (groupIds.Count == 0)
                return new List<OMGroup>();

            return OMGroup.Where(x => groupIds.Contains(x.Id)).Select(x => x.Id).Select(x => x.GroupName).Execute().ToList();
        }

        public int AddGroup(GroupDto groupDto)
        {
            ValidateTour(groupDto.RatingTourId);

            var group = new OMGroup();
			var tourGroup = new OMTourGroup();

			return SetGroupFields(groupDto, group, tourGroup);
		}

        public int UpdateGroup(GroupDto groupDto)
        {
            ValidateTour(groupDto.RatingTourId);

			var group = OMGroup.Where(x => x.Id == groupDto.Id)
			 .SelectAll().ExecuteFirstOrDefault();

			var tourGroup = OMTourGroup.Where(x => x.GroupId == groupDto.Id)
			 .SelectAll().ExecuteFirstOrDefault();

			return SetGroupFields(groupDto, group, tourGroup);
		}


        #region Calculation Settings To Calculate Cadastral Price

        public List<GroupCalculationSettingsDto> GetCalculationSettings(long tourId, bool isParcel)
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
            query.AddColumn(OMAutoCalculationSettings.GetColumn(x => x.NumberPriority, nameof(GroupCalculationSettingsDto.Priority)));
            query.AddColumn(OMAutoCalculationSettings.GetColumn(x => x.CalcStage1, nameof(GroupCalculationSettingsDto.Stage1)));
            query.AddColumn(OMAutoCalculationSettings.GetColumn(x => x.CalcStage2, nameof(GroupCalculationSettingsDto.Stage2)));
            query.AddColumn(OMAutoCalculationSettings.GetColumn(x => x.CalcStage3, nameof(GroupCalculationSettingsDto.Stage3)));
            query.OrderBy.Add(new QSOrder
            {
                Column = OMAutoCalculationSettings.GetColumn(x => x.NumberPriority),
                Order = QSOrderType.ASC
            });

            return query.ExecuteQuery<GroupCalculationSettingsDto>();
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

        #endregion


        #region Group To Market Segment Relation

        public OMGroupToMarketSegmentRelation GetGroupToMarketSegmentRelation(long groupId)
        {
            return GetOMGroupToMarketSegmentRelationByGroupId(groupId);
        }

        public void UpdateGroupToMarketSegmentRelation(long groupId, MarketSegment segment, TerritoryType territoryType)
        {
            var setting = GetOMGroupToMarketSegmentRelationByGroupId(groupId);
            if (setting == null)
                setting = new OMGroupToMarketSegmentRelation {GroupId = groupId};

            setting.MarketSegment_Code = segment;
            setting.TerritoryType_Code = territoryType;
            setting.Save();
        }

        private OMGroupToMarketSegmentRelation GetOMGroupToMarketSegmentRelationByGroupId(long groupId)
        {
            return OMGroupToMarketSegmentRelation.Where(x => x.GroupId == groupId).SelectAll()
                .ExecuteFirstOrDefault();
        }

        #endregion

        #region Group Explanation Settings

        public GroupExplanationSettingsDto GetGroupExplanationSettings(long groupId)
        {
	        var group = OMGroup.Where(x => x.Id == groupId)
		        .Select(x => x.Id)
		        .Select(x => x.AppliedApproachesInCadastralCost)
		        .Select(x => x.AppliedEvaluationMethodsInCadastralCost)
		        .Select(x => x.CadastralCostDetermingMethod)
		        .Select(x => x.ModelJustification)
		        .Select(x => x.ObjectsSegment)
		        .Select(x => x.ObjectsSubgroup)
		        .Select(x => x.CadastralCostCalculationOrderDescription)
		        .Select(x => x.PriceZoneCharacteristic)
		        .Select(x => x.MarketSegment)
		        .Select(x => x.MarketSegmentFunctioningFeatures)
		        .ExecuteFirstOrDefault();
	        if (group == null)
	        {
                throw new Exception($"Не найдена группа с ИД {groupId}");
	        }

            var result = new GroupExplanationSettingsDto
            {
                GroupId = group.Id,
                AppliedApproachesInCadastralCost = group.AppliedApproachesInCadastralCost,
                AppliedEvaluationMethodsInCadastralCost = group.AppliedEvaluationMethodsInCadastralCost,
                CadastralCostDetermingMethod = group.CadastralCostDetermingMethod,
                ModelJustification = group.ModelJustification,
                ObjectsSegment = group.ObjectsSegment,
                ObjectsSubgroup = group.ObjectsSubgroup,
                CadastralCostCalculationOrderDescription = group.CadastralCostCalculationOrderDescription,
                PriceZoneCharacteristic = group.PriceZoneCharacteristic,
                MarketSegment = group.MarketSegment,
                MarketSegmentFunctioningFeatures = group.MarketSegmentFunctioningFeatures
            };

            return result;
        }

        public void UpdateGroupExplanationSettings(GroupExplanationSettingsDto dto)
        {
	        var group = OMGroup.Where(x => x.Id == dto.GroupId)
		        .Select(x => x.Id)
		        .Select(x => x.AppliedApproachesInCadastralCost)
		        .Select(x => x.AppliedEvaluationMethodsInCadastralCost)
		        .Select(x => x.CadastralCostDetermingMethod)
		        .Select(x => x.ModelJustification)
		        .Select(x => x.ObjectsSegment)
		        .Select(x => x.ObjectsSubgroup)
		        .Select(x => x.CadastralCostCalculationOrderDescription)
		        .Select(x => x.PriceZoneCharacteristic)
		        .Select(x => x.MarketSegment)
		        .Select(x => x.MarketSegmentFunctioningFeatures)
                .ExecuteFirstOrDefault();
	        if (group == null)
	        {
		        throw new Exception($"Не найдена группа с ИД {dto.GroupId}");
	        }

	        group.AppliedApproachesInCadastralCost = dto.AppliedApproachesInCadastralCost;
            group.AppliedEvaluationMethodsInCadastralCost = dto.AppliedEvaluationMethodsInCadastralCost;
	        group.CadastralCostDetermingMethod = dto.CadastralCostDetermingMethod;
	        group.ModelJustification = dto.ModelJustification;
	        group.ObjectsSegment = dto.ObjectsSegment;
	        group.ObjectsSubgroup = dto.ObjectsSubgroup;
	        group.CadastralCostCalculationOrderDescription = dto.CadastralCostCalculationOrderDescription;
	        group.PriceZoneCharacteristic = dto.PriceZoneCharacteristic;
	        group.MarketSegment = dto.MarketSegment;
	        group.MarketSegmentFunctioningFeatures = dto.MarketSegmentFunctioningFeatures;
	        group.Save();
        }

        #endregion Group Explanation Settings

        #region Group Cadastral Cost Definition Act Settings

        public GroupCadastralCostDefinitionActSettingsDto GetGroupCadastralCostDefinitionActSettings(long groupId)
        {
            var group = OMGroup.Where(x => x.Id == groupId)
                .Select(x => x.Id)
                .Select(x => x.CadastralCostEstimationModelsReferences)
                .Select(x => x.AssumptionsReference)
                .Select(x => x.OtherCostRelatedInfo)
                .ExecuteFirstOrDefault();
            if (group == null)
            {
                throw new Exception($"Не найдена группа с ИД {groupId}");
            }

            var result = new GroupCadastralCostDefinitionActSettingsDto
            {
                GroupId = group.Id,
                CadastralCostEstimationModelsReferences = group.CadastralCostEstimationModelsReferences,
                AssumptionsReference = group.AssumptionsReference,
                OtherCostRelatedInfo = group.OtherCostRelatedInfo,
            };

            return result;
        }

        public void UpdateGroupCadastralCostDefinitionActSettings(GroupCadastralCostDefinitionActSettingsDto dto)
        {
	        var group = OMGroup.Where(x => x.Id == dto.GroupId)
		        .Select(x => x.Id)
		        .Select(x => x.CadastralCostEstimationModelsReferences)
		        .Select(x => x.AssumptionsReference)
		        .Select(x => x.OtherCostRelatedInfo)
		        .ExecuteFirstOrDefault();
            if (group == null)
            {
                throw new Exception($"Не найдена группа с ИД {dto.GroupId}");
            }

            group.CadastralCostEstimationModelsReferences = dto.CadastralCostEstimationModelsReferences;
            group.AssumptionsReference = dto.AssumptionsReference;
            group.OtherCostRelatedInfo = dto.OtherCostRelatedInfo;
            group.Save();
        }

        #endregion Group Explanation Settings


        #region Support Methods

        private void ValidateTour(long? tourId)
        {
            if (tourId == null)
                throw new Exception("Не передан идентификатор Тура для поиска");

            var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();
            if (tour == null)
                throw new Exception($"Не найден Тур с id='{tourId}'");
        }

        private int SetGroupFields(GroupDto groupDto, OMGroup group, OMTourGroup tourGroup)
		{
            if(string.IsNullOrWhiteSpace(groupDto.Name))
                throw new Exception("Не заполнено имя группы");
            if (groupDto.Number == null)
                throw new Exception($"У группы '{groupDto.Name}' не заполнен номер");
            if (groupDto.RatingTourId == null)
                throw new Exception($"У группы '{groupDto.Name}' не заполнен тур");

            int groupId;
            using (var ts = new TransactionScope())
            {
                string numberStr;

                var groupType = GetGroupType(groupDto.ParentGroupId);
                if (groupType == GroupType.Group)
                {
                    numberStr = groupDto.Number.ToString();
                    if(numberStr != group.Number)
                        UpdateSubgroupsNumber(groupDto.Id, numberStr);
                }
                else
                {
                    var parentGroupNumber = OMGroup.Where(x => x.Id == groupDto.ParentGroupId).Select(x => x.Number)
                        .ExecuteFirstOrDefault()?.Number;
                    numberStr = $"{parentGroupNumber}.{groupDto.Number}";
                }

                group.GroupName = groupDto.Name;
                group.Number = numberStr;
                group.ParentId = groupDto.ParentGroupId ?? -1;
                group.GroupAlgoritm_Code = (KoGroupAlgoritm) groupDto.GroupingAlgorithmId;
                groupId = group.Save();

                tourGroup.GroupId = group.Id;
                tourGroup.TourId = groupDto.RatingTourId.Value;
                tourGroup.Save();

                ts.Complete();
            }

            return groupId;
		}

        private GroupType GetGroupType(long? parentGroupId)
        {
            return parentGroupId != null && parentGroupId != -1 && 
                   parentGroupId != (long) KoGroupAlgoritm.MainOKS &&
                   parentGroupId != (long) KoGroupAlgoritm.MainParcel
                ? GroupType.SubGroup
                : GroupType.Group;
        }

        private void UpdateSubgroupsNumber(long? parentGroupId, string parentGroupNumber)
        {
            if (parentGroupId.GetValueOrDefault() == 0 || parentGroupId == -1)
                return;

            var subgroups = OMGroup.Where(x => x.ParentId == parentGroupId).SelectAll().Execute();

            subgroups.ForEach(x =>
            {
                var subGroupNumber = GetSubGroupNumber(x.Number);
                x.Number = $"{parentGroupNumber}.{subGroupNumber}";
                x.Save();
            });
        }

        private int? ParseGroupNumber(long? parentId, string combinedNumber)
        {
            if (string.IsNullOrWhiteSpace(combinedNumber))
                return null;

            var groupType = GetGroupType(parentId);

            var number = groupType == GroupType.Group
                ? combinedNumber.ParseToInt()
                : GetSubGroupNumber(combinedNumber);

            return number;
        }

        private int? GetSubGroupNumber(string fullNumber)
        {
            var subGroupNumberStr = fullNumber?.Split('.')?.ElementAtOrDefault(1);
            return subGroupNumberStr?.ParseToInt();
        }

        private OMGroup GetGroupByIdInternal(long? groupId)
        {
            if (groupId == null)
                throw new Exception("Не передан идентификатор Группы для поиска");

            var group = OMGroup.Where(x => x.Id == groupId).SelectAll().ExecuteFirstOrDefault();
            if (group == null)
                throw new Exception($"Не найдена Группа с id='{groupId}'");

            return group;
        }

        private List<GroupTreeDto> GetSubgroups(List<GroupTreeDto> allGroupsInTour)
        {
	        return allGroupsInTour.Where(x => x.ParentId != (long)KoGroupAlgoritm.MainOKS && x.ParentId != (long)KoGroupAlgoritm.MainParcel).ToList();
        }

        private static List<GroupTreeDto> GetGroupsWithSubgroups(List<GroupTreeDto> subgroups, List<GroupTreeDto> allGroups)
        {
	        var groupsIds = subgroups.Select(x => x.ParentId).Distinct().ToList();
	        var groups = allGroups.Where(x => groupsIds.Contains(x.Id)).ToList();
	        return groups;
        }

        private static List<GroupTreeDto> GetMainGroups()
        {
	        var mainGroups = new List<GroupTreeDto>();

	        var oks = new GroupTreeDto
	        {
		        Id = (long)KoGroupAlgoritm.MainOKS,
		        GroupName = "Основная группа ОКС",
		        GroupType = GroupType.Main
	        };
	        mainGroups.Add(oks);

	        var parcel = new GroupTreeDto
	        {
		        Id = (long)KoGroupAlgoritm.MainParcel,
		        GroupName = "Основная группа Участки",
		        GroupType = GroupType.Main
	        };
	        mainGroups.Add(parcel);

	        return mainGroups;
        }

        #endregion
    }
}
