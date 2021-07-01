using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using Microsoft.AspNetCore.Mvc;

namespace KadOzenka.Web.Models.Tour
{
    public class GroupTreeModel
    {
        public long? Id { get; set; }
        public long? ParentId { get; set; }
        public string GroupName { get; set; }
        public long? TourId { get; set; }
        public string UrlForEdit { get; set; }
        public string UrlForGroupingSettings { get; set; }
        public string UrlForSegmentSettings { get; set; }
        public string UrlForExplanationSettings { get; set; }
        public string UrlForCadastralCostDefinitionActSettings { get; set; }
        public GroupType GroupType { get; set; }
        public bool HasChildren { get; set; }
        public List<GroupTreeModel> Items { get; set; }


        public static GroupTreeModel ToModel(GroupTreeDto tree, IUrlHelper urlHelper)
        {
            return new()
            {
                Id = tree.Id,
                ParentId = tree.ParentId,
                GroupName = tree.GroupName,
                TourId = tree.TourId,
                GroupType = tree.GroupType,
                UrlForEdit = urlHelper.Action("GroupSubCard", "Tour", new {groupId = tree.Id, tourId = tree.TourId}),
                UrlForGroupingSettings = urlHelper.Action("TourGroupGroupingSettingsPartial", "Tour", new { groupId = tree.Id }),
                UrlForSegmentSettings = urlHelper.Action("GroupSegmentSettingsSubCard", "Tour", new { groupId = tree.Id }),
                UrlForExplanationSettings = urlHelper.Action("GroupExplanationSettingsSubCard", "Tour", new { groupId = tree.Id }),
                UrlForCadastralCostDefinitionActSettings = urlHelper.Action("GroupCadastralCostDefinitionActSettingsSubCard", "Tour", new { groupId = tree.Id }),
                Items = tree.Items?.Select(x => ToModel(x, urlHelper)).ToList()
            };
        }
    }
}
