using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class HashCombined
    {

        public long Id { get; set; }
        public string Type { get; set; }
        public AgregatorsData AgregatorData { get; set; }
        public AgregatorsDataExtra AgregatorDataExtra { get; set; }
        public CIPJSPropertyTypes PropertyType { get; set; }
        public Segments Segment { get; set; }
        public string Comment { get; set; }
        public UrlType UrlType { get; set; }
        public string UrlMainPart { get; set; }
        public string UrlExectPart { get; set; }
        public string UrlAsParameterType { get; set; }
        public string UrlAsParameterValue { get; set; }
        public string PropertyTypeForScripts { get; set; }
        public HashDealTypes DealTypes { get; set; }
        public HashCustomDistricts CustomDistricts { get; set; }
        public HashCustomRegions CustomRegions { get; set; }
        public string Url { get; set; }

        public override string ToString() => $"{Comment} {PropertyTypeForScripts} {Segment.Value} {DealTypes.Value} {CustomRegions.Value}: {Url}";

    }

}
