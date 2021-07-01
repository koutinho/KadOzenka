using System.Collections.Generic;
using Core.Shared.Extensions;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Models.Filters;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Tour
{
    public class TourGroupGroupingSettingsModel
    {
        public long? GroupId { get; set; }

        public List<Filters> GroupFilters { get; set; }

        public List<long?> KoAttributes { get; set; }

        public TourGroupGroupingSettingsModel()
        {
            GroupFilters = new List<Filters>();
            KoAttributes = new List<long?>();
        }

        public TourGroupGroupingSettingsDto ToDto()
        {
            return new()
            {
                GroupId = GroupId,
                GroupFilterSetting = GroupFilters,
                KoAttributes = KoAttributes
            };
        }

        public static TourGroupGroupingSettingsModel FromDto(TourGroupGroupingSettingsDto dto)
        {
            return new()
            {
                GroupId = dto.GroupId,
                GroupFilters = dto.GroupFilterSetting,
                KoAttributes = dto.KoAttributes
            };
        }

        public List<OMTourGroupGroupingSettings> ToObjectModel()
        {
            var om = new List<OMTourGroupGroupingSettings>();
            for (int i = 0; i < GroupFilters.Count; i++)
            {
                om.Add(new OMTourGroupGroupingSettings
                    {Filter = GroupFilters[i].SerializeToXml(), GroupId = GroupId, KoAttributeId = KoAttributes[i]});
            }

            return om;
        }
    }
}