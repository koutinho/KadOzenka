using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.Extentions;
using Newtonsoft.Json.Linq;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.AvitoParsing.Parsers
{
    public class AvitoLandParser : AvitoParser
    {
        public AvitoLandParser()
        {
            Url = "https://www.avito.ru/moskva/zemelnye_uchastki";
            ObjectTypeList = new List<ObjectCategoryCorrelation>
            {
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Поселений (ИЖС)",
                    MarketSegment = MarketSegment.NoSegment,
                    PropertyType = PropertyTypesCIPJS.LandArea
                },
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Сельхозназначения (СНТ, ДНП)",
                    MarketSegment = MarketSegment.Garden,
                    PropertyType = PropertyTypesCIPJS.LandArea
                },
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Промназначения",
                    MarketSegment = MarketSegment.NoSegment,
                    PropertyType = PropertyTypesCIPJS.LandArea
                }
            };
        }

        protected override void FillObjectLandInfo(OMCoreObject marketObject, JObject jObject)
        {
            var areaString = !jObject.SelectToken("area").IsNullOrEmpty()
                ? jObject.SelectToken("area").Value<string>()
                : !jObject.SelectToken("siteArea").IsNullOrEmpty()
                    ? jObject.SelectToken("siteArea").Value<string>()
                    : null;

            if (!string.IsNullOrWhiteSpace(areaString))
            {
                var areaStringParts = areaString.Split(' ');
                if (!decimal.TryParse(areaStringParts[0], out var area))
                {
                    area = decimal.Parse(areaStringParts[1]);
                }
                var unit = areaString.Split(' ').Last().ToLower().Trim();

                var landCoef = 1.0m;
                switch (unit)
                {
                    case "м²":
                        landCoef = 0.01m;
                        break;
                    case "сот.":
                        landCoef = 1;
                        break;
                    case "га":
                        landCoef = 100;
                        break;
                }

                marketObject.AreaLand = area * landCoef;
            }
        }
    }
}
