using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    interface IMapperLinkFormer
    {
        List<MappingMetaInfo> FormLinks(Dictionary<string, ValuePair> dealTypes, Dictionary<string, SegmentValuePair> segments, List<DistrictValuePair> districtsList);
        string GetLink(ValuePair dealType, SegmentValuePair segment, DistrictValuePair district, int pageNumber);
        List<MappingMetaInfo> GetLinks();
    }

}
