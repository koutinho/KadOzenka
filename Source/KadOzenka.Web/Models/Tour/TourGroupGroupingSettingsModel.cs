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

        public List<TourGroupGroupingSettingsPartialModel> Settings { get; set; }

        public TourGroupGroupingSettingsModel()
        {
            Settings = new List<TourGroupGroupingSettingsPartialModel>();
        }

        public List<OMTourGroupGroupingSettings> ToObjectModel()
        {
            var om = new List<OMTourGroupGroupingSettings>();

            foreach (var setting in Settings)
            {
                var useDict = setting.UseDictionary;
                if (useDict)
                {
                    om.Add(new OMTourGroupGroupingSettings
                    {
                        GroupId = GroupId,
                        KoAttributeId = setting.KoAttributes,
                        Filter = setting.GroupFilters.SerializeToXml(),
                        DictionaryId = setting.DictionaryId,
                        DictionaryValues = setting.DictionaryValue
                    });
                }
                else
                {
                    om.Add(new OMTourGroupGroupingSettings
                    {
                        GroupId = GroupId,
                        KoAttributeId = setting.KoAttributes,
                        Filter = setting.GroupFilters.SerializeToXml()
                    });
                }
            }

            return om;
        }
    }
}