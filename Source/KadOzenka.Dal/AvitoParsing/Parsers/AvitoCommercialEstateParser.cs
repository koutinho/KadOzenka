using System.Collections.Generic;
using ObjectModel.Directory;

namespace KadOzenka.Dal.AvitoParsing.Parsers
{
    public class AvitoCommercialEstateParser : AvitoParser
    {
        public AvitoCommercialEstateParser()
        {
            Url = "https://www.avito.ru/moskva/kommercheskaya_nedvizhimost";
            ObjectTypeList = new List<ObjectCategoryCorrelation>
            {
                new ObjectCategoryCorrelation
                {
                    AvitoName =  "Гостиница",
                    MarketSegment = MarketSegment.Hotel,
                    PropertyType = PropertyTypesCIPJS.Placements
                },
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Офисное помещение",
                    MarketSegment = MarketSegment.Office,
                    PropertyType = PropertyTypesCIPJS.Placements
                },
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Помещение общественного питания",
                    MarketSegment = MarketSegment.PublicCatering,
                    PropertyType = PropertyTypesCIPJS.Placements
                },
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Помещение свободного назначения",
                    MarketSegment = MarketSegment.NoSegment,
                    PropertyType = PropertyTypesCIPJS.Placements
                },
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Производственное помещение",
                    MarketSegment = MarketSegment.Factory,
                    PropertyType = PropertyTypesCIPJS.Placements
                },
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Складское помещение",
                    MarketSegment = MarketSegment.Factory,
                    PropertyType = PropertyTypesCIPJS.Placements
                },
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Торговое помещение",
                    MarketSegment = MarketSegment.Trading,
                    PropertyType = PropertyTypesCIPJS.Placements
                },
            };
        }
    }
}
