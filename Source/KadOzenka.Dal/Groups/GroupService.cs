using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using CommonSdks.RecycleBin;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using ModelingBusiness.Model;
using ObjectModel.Common;
using ObjectModel.Directory;
using ObjectModel.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.Groups
{
    public class GroupService : IGroupService
    {
        private ModelService ModelService { get; }
        private RecycleBinService RecycleBinService { get; }

        public GroupService()
        {
	        ModelService = new ModelService();
	        RecycleBinService = new RecycleBinService();
        }

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

        public List<OMGroup> GetGroupsByIds(List<long> groupIds)
        {
           if(groupIds == null || groupIds.Count == 0)
               return new List<OMGroup>();

           return OMGroup.Where(x => groupIds.Contains(x.Id)).SelectAll().Execute();
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
            query.AddColumn(OMGroup.GetColumn(x => x.CheckModelFactorsValues, "CheckModelFactorsValues"));
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
                str.CheckModelFactorsValues = row["CheckModelFactorsValues"].ParseToBoolean();
                str.CombinedNumber = row[nameof(GroupTreeDto.CombinedNumber)].ParseToStringNullable();
                str.Number = ParseGroupNumber(str.ParentId, str.CombinedNumber);
                str.TourId = long.Parse(row["TourId"].ToString());

                groups.Add(str);
            }

            return groups;
        }

        public List<GroupTreeDto> GetGroupsTreeForTour(long tourId, bool addEmptyOksZuMainGroups = false)
        {
	        var allGroups = GetGroups();
	        var allGroupsInTour = allGroups.Where(x => x.TourId == tourId).ToList();
	        if (allGroupsInTour.Count == 0)
	        {
		        if (!addEmptyOksZuMainGroups) return new List<GroupTreeDto>();
		        var emptyMainGroups = GetMainGroups();

		        return emptyMainGroups.Select(mainGroup => new GroupTreeDto
			        {
				        Id = mainGroup.Id,
				        GroupName = mainGroup.GroupName,
				        GroupType = mainGroup.GroupType,
				        TourId = tourId,
				        CheckModelFactorsValues = mainGroup.CheckModelFactorsValues,
				        Items = new List<GroupTreeDto>()
			        })
			        .ToList();
	        }

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
                            CombinedNumber = group.CombinedNumber,
                            GroupType = group.GroupType,
                            CheckModelFactorsValues = group.CheckModelFactorsValues,
                            TourId = tourId,
                            Items = subgroups.Where(subGroup => subGroup.ParentId == group.Id)
                                .OrderBy(subGroup => subGroup.Number)
                                .Select(subGroup => new GroupTreeDto
                                {
                                    Id = subGroup.Id,
                                    GroupName = $"{subGroup.CombinedNumber}. {subGroup.GroupName}",
                                    CombinedNumber = subGroup.CombinedNumber,
                                    GroupType = subGroup.GroupType,
                                    CheckModelFactorsValues = subGroup.CheckModelFactorsValues,
                                    TourId = tourId
                                }).ToList()
                        }).ToList();

                if (groups.Count > 0 || addEmptyOksZuMainGroups)
		        {
			        models.Add(new GroupTreeDto
					{
				        Id = mainGroup.Id,
				        GroupName = mainGroup.GroupName,
				        GroupType = mainGroup.GroupType,
				        TourId = tourId,
				        CheckModelFactorsValues = mainGroup.CheckModelFactorsValues,
						Items = groups
			        });
		        }
	        });

	        return models;
        }

        public TourGroupsInfo GetTourGroupsInfo(long tourId, ObjectTypeExtended objectType)
        {
	        var allGroupsInTour = GetGroupsTreeForTour(tourId);

            var oksGroups = objectType == ObjectTypeExtended.Oks || objectType == ObjectTypeExtended.Both 
	            ? allGroupsInTour.Where(x => x.Id == (int)KoGroupAlgoritm.MainOKS).SelectMany(x => x.Items).ToList()
	            : null;
            var zuGroups = objectType == ObjectTypeExtended.Zu || objectType == ObjectTypeExtended.Both
	            ? allGroupsInTour.Where(x => x.Id == (int) KoGroupAlgoritm.MainParcel).SelectMany(x => x.Items).ToList()
	            : null;

	        return new TourGroupsInfo
	        {
		        OksGroups = oksGroups,
		        OksSubGroups = oksGroups?.SelectMany(x => x.Items).ToList(),
		        ZuGroups = zuGroups,
		        ZuSubGroups = zuGroups?.SelectMany(x => x.Items).ToList()
	        };
        }

        public List<OMGroup> GetGroupsByTasks(List<long> taskIds)
        {
            if (taskIds.Count == 0)
                return new List<OMGroup>();

            var groupIds = GetGroupIdsByTasts(taskIds);

            if (groupIds.Count == 0)
                return new List<OMGroup>();

            return OMGroup.Where(x => groupIds.Contains(x.Id)).Select(x => x.Id).Select(x => x.GroupName).Select(x => x.Number).Execute().ToList();
        }

        public List<GroupNumberInfoDto> GetSortedGroupsWithNumbersByTasks(List<long> taskIds)
        {
	        var result = new List<GroupNumberInfoDto>();
            if (taskIds.Count == 0)
		        return result;

	        var groupIds = GetGroupIdsByTasts(taskIds);
	        if (groupIds.Count == 0)
		        return result;

            var groups = OMGroup.Where(x => groupIds.Contains(x.Id))
		        .Select(x => x.Id)
		        .Select(x => x.ParentId)
                .Select(x => x.GroupName)
		        .Select(x => x.Number).Execute().ToList();

            return groups.Select(x => GroupNumberInfoDto.FromOMGroup(x, this))
		        .OrderBy(x => x.ParentNumber)
		        .ThenBy(x => x.Number)
		        .ToList();
        }

        public List<GroupNumberInfoDto> GetOtherGroupsFromTreeLevelForTour(long groupId)
        {
            if(groupId == (int)KoGroupAlgoritm.MainOKS || groupId == (int)KoGroupAlgoritm.MainParcel)
                return new List<GroupNumberInfoDto>();

            var group = OMGroup.Where(x => x.Id == groupId)
	            .Select(x => x.ParentId)
	            .Select(x => x.GroupAlgoritm_Code)
                .ExecuteFirstOrDefault();
            var groupType = GetGroupType(group.ParentId);
            KoGroupAlgoritm algoritm;
            if (groupType == GroupType.Group)
            {
	            algoritm = group.GroupAlgoritm_Code;
            }
            else
            {
	            algoritm = OMGroup.Where(x => x.Id == group.ParentId)
	                .Select(x => x.GroupAlgoritm_Code)
	                .ExecuteFirstOrDefault().GroupAlgoritm_Code;
            }

            var allGroupsInfo = GetGroupsInfoForTour(groupId);
            var finalGroups = groupType == GroupType.Group
	            ? allGroupsInfo.Where(x => x.ParentId == -1 && x.GroupAlgoritm == algoritm && x.Id != groupId).ToList()
	            : allGroupsInfo.Where(x => x.ParentId != -1 && x.ParentGroupAlgoritm == algoritm && x.Id != groupId).ToList();

            return finalGroups.Select(x => GroupNumberInfoDto.FromGroupInfoDto(x, this))
	            .OrderBy(x => x.ParentNumber)
	            .ThenBy(x => x.Number)
	            .ToList();
        }

        public int AddGroup(GroupDto groupDto)
        {
            ValidateTour(groupDto.RatingTourId);

            var group = new OMGroup();
			var tourGroup = new OMTourGroup();

			var groupId =  SetGroupFields(groupDto, group, tourGroup);
			if (GetGroupType(groupDto.ParentGroupId) == GroupType.SubGroup)
			{
				new GroupCalculationSettingsService().CreateCalculationSettingsForGroup(groupId);
			}

			return groupId;
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

        public bool CanGroupBeDeleted(long groupId, bool checkChildGroups = true)
        {
            var result = !OMUnit.Where(x => x.GroupId == groupId).ExecuteExists();
            if (result && checkChildGroups)
            {
	            List<long?> childGroupIds = OMGroup.Where(x => x.ParentId == groupId).Execute().Select(x => (long?)x.Id).ToList();
	            if (childGroupIds.IsNotEmpty())
	            {
                    result = !OMUnit.Where(x => childGroupIds.Contains(x.GroupId)).ExecuteExists();
                }
            }

            return result;
        }

        public bool CanGroupsBeDeleted(long tourId, bool isOks)
        {
	        var result = true;
	        var groups = GetTourGroupsInfo(tourId, isOks ? ObjectTypeExtended.Oks : ObjectTypeExtended.Zu);
	        var areGroupsNotEmpty = isOks
		        ? groups.OksGroups.Count > 0 || groups.OksSubGroups.Count > 0
		        : groups.ZuGroups.Count > 0 || groups.ZuSubGroups.Count > 0;

	        if (areGroupsNotEmpty)
	        {
		        List<long?> groupIds;
		        if (isOks)
		        {
			        groupIds = groups.OksGroups.Select(x => x.Id).ToList();
			        groupIds.AddRange(groups.OksSubGroups.Select(x => x.Id).ToList());
		        }
                else
                {
                    groupIds = groups.ZuGroups.Select(x => x.Id).ToList();
                    groupIds.AddRange(groups.ZuSubGroups.Select(x => x.Id).ToList());
                }

		        result = !OMUnit.Where(x => groupIds.Contains(x.GroupId)).ExecuteExists();
            }

	        return result;
        }

        public void DeleteGroups(long tourId, bool isOks, long? eventId = null)
        {
	        var groups = GetTourGroupsInfo(tourId, isOks ? ObjectTypeExtended.Oks : ObjectTypeExtended.Zu);
	        var tour = OMTour.Where(x => x.Id == tourId).SelectAll().ExecuteFirstOrDefault();
	        List<long?> groupIds;
	        if (isOks)
	        {
		        groupIds = groups.OksGroups.Select(x => x.Id).ToList();
	        }
	        else
	        {
		        groupIds = groups.ZuGroups.Select(x => x.Id).ToList();
	        }

            using (var ts = new TransactionScope())
	        {
		        if (eventId == null)
		        {
			        var recycleBinRecord = new OMRecycleBin
			        {
				        DeletedTime = DateTime.Now,
				        UserId = SRDSession.GetCurrentUserId().Value,
				        ObjectRegisterId = OMGroup.GetRegisterId(),
				        Description = isOks
					        ? $"Группы категории 'Основная группа ОКС' Тура '{tour.Year}'"
					        : $"Группы категории 'Основная группа Участки' Тура '{tour.Year}'"
			        };
			        eventId = recycleBinRecord.Save();
                }
		        
                foreach (var groupId in groupIds)
		        {
			        DeleteGroup(groupId.Value, tour.Year, eventId);
		        }

		        ts.Complete();
            }
        }

        public void DeleteGroup(long groupId, long? tourYear=null, long? eventId = null)
        {
	        OMGroup group = OMGroup.Where(x => x.Id == groupId)
		        .Select(x => new { x.Number, x.GroupName }).ExecuteFirstOrDefault();

	        if (!CanGroupBeDeleted(groupId, false))
		        throw new Exception($"Группа '{group?.Number}. {group?.GroupName}' не может быть удалена, т.к. имеются связанные единицы оценки");

	        List<OMGroup> childGroups = OMGroup.Where(x => x.ParentId == groupId).Execute();
	        OMTourGroup tourGroup = OMTourGroup.Where(x => x.GroupId == groupId).SelectAll().ExecuteFirstOrDefault();
	        if (!tourYear.HasValue && tourGroup != null)
	        {
		        tourYear = OMTour.Where(x => x.Id == tourGroup.TourId)
			        .Select(x => x.Year).ExecuteFirstOrDefault()?.Year;
	        }
            OMAutoCalculationSettings calculationSettings = OMAutoCalculationSettings.Where(x => x.IdGroup == groupId).ExecuteFirstOrDefault();
	        List<OMModel> models = OMModel.Where(x => x.GroupId == groupId).Execute();
	        List<OMCalcGroup> calcGroups = OMCalcGroup.Where(x => x.GroupId == groupId || x.ParentCalcGroupId == groupId).Execute();
	        OMGroupToMarketSegmentRelation groupToMarketSegmentRelation = GetOMGroupToMarketSegmentRelationByGroupId(groupId);
	        using (var ts = new TransactionScope())
	        {
		        if (eventId == null)
		        {
			        var recycleBinRecord = new OMRecycleBin
			        {
				        DeletedTime = DateTime.Now,
				        UserId = SRDSession.GetCurrentUserId().Value,
				        ObjectRegisterId = OMGroup.GetRegisterId(),
				        Description = tourYear.HasValue
					        ? $"Группа '{@group.Number}. {@group.GroupName}' Тура '{tourYear}'"
                            : $"Группа '{@group.Number}. {@group.GroupName}'"
                    };
			        eventId = recycleBinRecord.Save();
		        }

		        foreach (var childGroup in childGroups)
		        {
			        DeleteGroup(childGroup.Id, tourYear, eventId);
		        }

		        if (calculationSettings != null)
                    RecycleBinService.MoveObjectToRecycleBin(calculationSettings.Id, OMAutoCalculationSettings.GetRegisterId(), eventId.Value);

		        foreach (var model in models)
		        {
			        ModelService.DeleteModelLogically(model.Id, eventId.Value);
		        }


	            RecycleBinService.MoveObjectsToRecycleBin(calcGroups.Select(x => x.Id).ToList(), OMCalcGroup.GetRegisterId(), eventId.Value);

	            if (groupToMarketSegmentRelation != null)
		            RecycleBinService.MoveObjectToRecycleBin(groupToMarketSegmentRelation.Id, OMGroupToMarketSegmentRelation.GetRegisterId(), eventId.Value);

	            if (group != null)
		            RecycleBinService.MoveObjectToRecycleBin(group.Id, OMGroup.GetRegisterId(), eventId.Value);

                if (tourGroup != null)
	                RecycleBinService.MoveObjectToRecycleBin(tourGroup.Id, OMTourGroup.GetRegisterId(), eventId.Value);

                ts.Complete();
	        }
        }

        #region Group To Market Segment Relation

        public OMGroupToMarketSegmentRelation GetGroupToMarketSegmentRelation(long groupId)
        {
            return GetOMGroupToMarketSegmentRelationByGroupId(groupId);
        }

        public void UpdateGroupToMarketSegmentRelation(long groupId, MarketSegment segment)
        {
            var setting = GetOMGroupToMarketSegmentRelationByGroupId(groupId);
            if (setting == null)
                setting = new OMGroupToMarketSegmentRelation {GroupId = groupId};

            setting.MarketSegment_Code = segment;
            setting.TerritoryType_Code = TerritoryType.No;
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

            var isTourExists = OMTour.Where(x => x.Id == tourId).ExecuteExists();
            if (!isTourExists)
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
                KoGroupAlgoritm groupAlgorithm;
                var groupType = GetGroupType(groupDto.ParentGroupId);
                if (groupType == GroupType.Group)
                {
	                groupAlgorithm = groupDto.ParentGroupId == (long) KoGroupAlgoritm.MainOKS
		                ? KoGroupAlgoritm.MainOKS
		                : KoGroupAlgoritm.MainParcel;
	                numberStr = groupDto.Number.ToString();
                    if(numberStr != group.Number)
                        UpdateSubgroupsNumber(groupDto.Id, numberStr);
                }
                else
                {
	                groupAlgorithm = groupDto.GroupAlgorithmCode;
	                var parentGroupNumber = OMGroup.Where(x => x.Id == groupDto.ParentGroupId).Select(x => x.Number)
                        .ExecuteFirstOrDefault()?.Number;
                    numberStr = $"{parentGroupNumber}.{groupDto.Number}";
                }

                group.GroupName = groupDto.Name;
                group.Number = numberStr;
                group.ParentId = groupDto.ParentGroupId ?? -1;
                group.GroupAlgoritm_Code = groupAlgorithm;
                groupId = group.Save();

                tourGroup.GroupId = group.Id;
                tourGroup.TourId = groupDto.RatingTourId.Value;
                tourGroup.Save();

                ts.Complete();
            }

            return groupId;
		}

        public GroupType GetGroupType(long? parentGroupId)
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

        public int? ParseGroupNumber(long? parentId, string combinedNumber)
        {
            if (string.IsNullOrWhiteSpace(combinedNumber))
                return null;

            var groupType = GetGroupType(parentId);

            var number = groupType == GroupType.Group
                ? combinedNumber.ParseToInt()
                : GetSubGroupNumber(combinedNumber);

            return number;
        }

        public int? GetSubGroupNumber(string fullNumber)
        {
            var subGroupNumberStr = fullNumber?.Split('.')?.ElementAtOrDefault(1);
            return subGroupNumberStr?.ParseToInt();
        }

        public int? GetParentGroupNumber(string fullNumber)
        {
	        var subGroupNumberStr = fullNumber?.Split('.')?.ElementAtOrDefault(0);
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

        private List<long?> GetGroupIdsByTasts(List<long> taskIds)
        {
	        var groupIds = OMUnit.Where(x => taskIds.Contains((long)x.TaskId) && x.GroupId != null)
		        .GroupBy(x => x.GroupId)
		        .Select(x => x.GroupId)
		        .SetDistinct()
		        .Execute()
		        .Select(x => x.GroupId)
		        .Distinct()
		        .ToList();
	        return groupIds;
        }

        private List<GroupAlgorithmInfoDto> GetGroupsInfoForTour(long groupId)
        {
	        var tg = OMTourGroup.Where(x => x.GroupId == groupId)
		        .Select(x => x.TourId)
		        .ExecuteFirstOrDefault();
	        if (tg == null)
		        throw new Exception("Не найден тур для выбранной группы");

            var query = new QSQuery
	        {
		        MainRegisterID = OMGroup.GetRegisterId(),
		        Condition = new QSConditionGroup
		        {
			        Type = QSConditionGroupType.And,
			        Conditions = new List<QSCondition>
			        {
				        new QSConditionSimple(OMTourGroup.GetColumn(x => x.TourId), QSConditionType.Equal, tg.TourId)
			        }
                },
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

	        query.AddColumn(OMGroup.GetColumn(x => x.Number, nameof(GroupAlgorithmInfoDto.Number)));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupName, nameof(GroupAlgorithmInfoDto.Name)));
            query.AddColumn(OMGroup.GetColumn(x => x.GroupAlgoritm_Code, nameof(GroupAlgorithmInfoDto.GroupAlgoritm)));
            query.AddColumn(OMGroup.GetColumn(x => x.ParentId, nameof(GroupAlgorithmInfoDto.ParentId)));

            var subQuery = new QSQuery(OMGroup.GetRegisterId())
            {
	            Columns = new List<QSColumn>
	            {
		            OMGroup.GetColumn(x => x.GroupAlgoritm_Code)
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
            query.AddColumn(subQuery, nameof(GroupAlgorithmInfoDto.ParentGroupAlgoritm));

            return query.ExecuteQuery<GroupAlgorithmInfoDto>();
        }

        #endregion
    }
}
