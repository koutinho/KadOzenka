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

        // public List<bool> UseDictionary { get; set; }
        //
        // public List<long?> DictionaryId { get; set; }
        //
        // public List<string> DictionaryValue { get; set; }
        //
        // public List<Filters> GroupFilters { get; set; }
        //
        // public List<long?> KoAttributes { get; set; }

        public TourGroupGroupingSettingsModel()
        {
            Settings = new List<TourGroupGroupingSettingsPartialModel>();
            // GroupFilters = new List<Filters>();
            // KoAttributes = new List<long?>();
            // DictionaryId = new List<long?>();
            // DictionaryValue = new List<string>();
            // UseDictionary = new List<bool>();
        }

        public TourGroupGroupingSettingsDto ToDto()
        {
            return new()
            {
                GroupId = GroupId,
                // GroupFilterSetting = GroupFilters,
                // KoAttributes = KoAttributes,
                // DictionaryId = DictionaryId,
                // DictionaryValue = DictionaryValue
            };
        }

        public static TourGroupGroupingSettingsModel FromDto(TourGroupGroupingSettingsDto dto)
        {
            return new()
            {
                GroupId = dto.GroupId,
                // GroupFilters = dto.GroupFilterSetting,
                // KoAttributes = dto.KoAttributes,
                // DictionaryId = dto.DictionaryId,
                // DictionaryValue = dto.DictionaryValue
            };
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