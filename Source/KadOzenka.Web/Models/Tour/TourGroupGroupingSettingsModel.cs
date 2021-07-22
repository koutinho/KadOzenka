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

        public bool CheckModelFactorsValues { get; set; }

        public List<TourGroupGroupingSettingsPartialModel> Settings { get; set; }

        public TourGroupGroupingSettingsModel()
        {
            Settings = new List<TourGroupGroupingSettingsPartialModel>();
        }

        public List<OMTourGroupGroupingSettings> ToObjectModel()
        {
            var objectModelList = new List<OMTourGroupGroupingSettings>();

            foreach (var setting in Settings)
            {
                var useDict = setting.UseDictionary;
                var om = new OMTourGroupGroupingSettings()
                {
                    GroupId = GroupId,
                    KoAttributeId = setting.KoAttributes,
                    Filter = setting.GroupFilters.SerializeToXml()
                };
                if (useDict)
                {
                    om.DictionaryId = setting.DictionaryId;
                    om.DictionaryValues = setting.DictionaryValue;
                }
                objectModelList.Add(om);
            }

            return objectModelList;
        }
    }
}