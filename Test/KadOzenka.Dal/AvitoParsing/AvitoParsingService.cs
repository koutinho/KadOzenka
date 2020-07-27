using KadOzenka.Dal.AvitoParsing.Parsers;

namespace KadOzenka.Dal.AvitoParsing
{
    public class AvitoParsingService
    {
        public void ParseAllObjects()
        {
            new AvitoCommercialEstateParser().HandleObjects();
            new AvitoLandParser().HandleObjects();
            new AvitoLandParser().HandleObjects();
        }

        public void ParseCommercialEstateObjects()
        {
            new AvitoCommercialEstateParser().HandleObjects();
        }

        public void ParseLandObjects()
        {
            new AvitoLandParser().HandleObjects();
        }

        public void ParseParkingObjects()
        {
            new AvitoParkingParser().HandleObjects();
        }
    }
}
