using System;
using System.Collections.Generic;
using System.Text;
using ObjectModel.Directory;

namespace Parser
{

    struct MappingMetaInfo
    {
        public MarketTypes marketType;
        public DealType dealType;
        public Hunteds hunted;
        public Districts district;
        public PropertyTypesCIPJS propertyType;
        public MarketSegment marketSegment;
        public ProcessStep processStep;
        public string urlAddress;
    }

}
