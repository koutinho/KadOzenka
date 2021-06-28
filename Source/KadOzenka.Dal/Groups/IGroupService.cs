using System.Collections.Generic;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using ObjectModel.Directory;
using ObjectModel.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.Groups
{
	public interface IGroupService
	{
		GroupDto GetGroupById(long? groupId);
		List<OMGroup> GetGroupsByIds(List<long> groupIds);
		List<GroupTreeDto> GetGroups(long? mainParentId = null);
		List<GroupTreeDto> GetGroupsTreeForTour(long tourId, bool addEmptyOksZuMainGroups = false);
		TourGroupsInfo GetTourGroupsInfo(long tourId, ObjectTypeExtended objectType);
		List<OMGroup> GetGroupsByTasks(List<long> taskIds);
		List<GroupNumberInfoDto> GetSortedGroupsWithNumbersByTasks(List<long> taskIds);
		List<GroupNumberInfoDto> GetOtherGroupsFromTreeLevelForTour(long groupId);
		int AddGroup(GroupDto groupDto);
		int UpdateGroup(GroupDto groupDto);
		bool CanGroupBeDeleted(long groupId, bool checkChildGroups = true);
		bool CanGroupsBeDeleted(long tourId, bool isOks);
		void DeleteGroups(long tourId, bool isOks, long? eventId = null);
		void DeleteGroup(long groupId, long? tourYear=null, long? eventId = null);
		OMGroupToMarketSegmentRelation GetGroupToMarketSegmentRelation(long groupId);
		void UpdateGroupToMarketSegmentRelation(long groupId, MarketSegment segment, TerritoryType territoryType);
		GroupExplanationSettingsDto GetGroupExplanationSettings(long groupId);
		void UpdateGroupExplanationSettings(GroupExplanationSettingsDto dto);
		GroupCadastralCostDefinitionActSettingsDto GetGroupCadastralCostDefinitionActSettings(long groupId);
		void UpdateGroupCadastralCostDefinitionActSettings(GroupCadastralCostDefinitionActSettingsDto dto);
		GroupType GetGroupType(long? parentGroupId);
		int? ParseGroupNumber(long? parentId, string combinedNumber);
		int? GetSubGroupNumber(string fullNumber);
		int? GetParentGroupNumber(string fullNumber);
	}
}