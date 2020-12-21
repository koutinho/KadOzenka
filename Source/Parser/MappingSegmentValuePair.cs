using System;
using System.Collections.Generic;
using System.Text;
using ObjectModel.Directory;

namespace Parser
{

    struct SegmentValuePair
    {
        public PropertyTypesCIPJS propertyType;
        public MarketSegment marketSegment;
        public string urlMainPart;
        public string urlExectPart;
        public string asParameter;
        public UrlType urlType;
        public string comment;
    }

}
