using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Core.Shared.Extensions;
using MarketPlaceBusiness.Repositories;
using ObjectModel.Directory;
using ObjectModel.Market;
using Serilog;

namespace KadOzenka.Dal.YRLParser
{
    public class YrlFeedParser
    {
        private ILogger Log { get; } = Serilog.Log.ForContext<YrlFeedParser>();
        private realtyfeed Feed { get; }

        private readonly object _lock = new();

        private long _counterSuccessful;

        private long _counterSkipped;

        private readonly MarketObjectsRepository _repository = new();

        public YrlFeedParser(Stream stream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(realtyfeed));
            using TextReader tr = new StreamReader(stream);
            Feed = (realtyfeed) xs.Deserialize(tr);
        }

        public void ParseAndCreate()
        {
            Log.Information("В фиде найдено {OfferCount} объявлений", Feed.offer.Length);

            Parallel.ForEach(Feed.offer, new ParallelOptions {MaxDegreeOfParallelism = 8}, ImportOffer);

            Log.Information("Всего импортировано {ImportedCount} объектов, проигнорировано {IgnoredCount} объектов",
                _counterSuccessful, _counterSkipped);
        }

        private void ImportOffer(realtyfeedOffer offer)
        {
            var newObject = offer switch
            {
                // Исключаем часть выбросов на этапе загрузки данных
                {price: var price} when price.currency is "EUR" or "USD" => null,

                // Определяем тип объявления по описанию
                {propertytype: "living" or "жилая", category: "продажа" or "аренда"} => CreateLiving(offer),
                {category: "commercial" or "коммерческая"} => CreateCommercial(offer),
                {category: "garage" or "гараж"} => CreateGarage(offer),
                _ => null
            };

            if (newObject != null)
            {
                IncreaseAndLogSuccessful();
                _repository.Save(newObject);
            }
            else
            {
                IncreaseSkipped();
                Log.ForContext("OfferObject", offer, destructureObjects: true)
                    .Warning("Объект фида сформирован неверно");
            }
        }

        #region Helpers

        private void IncreaseSkipped()
        {
            lock (_lock)
            {
                _counterSkipped++;
            }
        }

        private void IncreaseAndLogSuccessful()
        {
            lock (_lock)
            {
                _counterSuccessful++;
                if (_counterSuccessful % 1000 == 0)
                {
                    Log.Information("Импортировано {ImportedCount} объектов", _counterSuccessful);
                }
            }
        }

        #endregion

        #region Resolvers

        private FinishingCondition ResolveConditionType(string offerRenovation)
        {
            return offerRenovation switch
            {
                "дизайнерский" => FinishingCondition.Design,
                "евро" => FinishingCondition.Typical, // ?
                "с отделкой" => FinishingCondition.Finishing,
                "требует ремонта" => FinishingCondition.MajorRepairsRequired,
                "хороший" => FinishingCondition.Typical,
                "частичный ремонт" => FinishingCondition.CosmeticRepairsRequired,
                "черновая отделка" => FinishingCondition.CosmeticRepairsRequired, // ?
                _ => FinishingCondition.None
            };
        }

        private readonly List<string> _truthy = new() {"да", "true", "1", "+"};
        private readonly List<string> _falsy = new() {"нет", "false", "0", "-"};

        public bool ResolveBoolean(string input)
        {
            if (_truthy.Contains(input)) return true;
            if (_falsy.Contains(input)) return false;
            Log.Verbose("Значение '{BooleanInput}' не является допустимым для булевого поля", input);
            return false;
        }

        private MarketSegment ResolveMarketSegment(string input)
        {
            return input switch
            {
                "дача" or "коттедж" or "cottage" => MarketSegment.Cottage,
                "участок" or "lot" => MarketSegment.Land,
                "часть дома" => MarketSegment.HouseShare,
                "квартира" or "flat" => MarketSegment.Flat,
                "комната" or "room" => MarketSegment.Room,
                "таунхаус" or "townhouse" => MarketSegment.Townhouse,
                "гараж" or "garage" => MarketSegment.Garage,
                "дом" or "house" => MarketSegment.House,
                "дом с участком" or "house with lot" => // ?
                    MarketSegment.House,
                "дуплекс" or "duplex" => //?
                    MarketSegment.House,
                _ => MarketSegment.None
            };
        }

        private DealType ResolveDealType(string input)
        {
            return input switch
            {
                "продажа" => DealType.SaleSuggestion,
                "аренда" => DealType.RentSuggestion,
                _ => DealType.None
            };
        }

        private QualityClass ResolveQualityCLass(string input)
        {
            return input switch
            {
                "A" or "А" => QualityClass.A,
                "A+" or "А+" => QualityClass.Aplus,
                "B" or "В" => QualityClass.B,
                "B+" or "В+" => QualityClass.Bplus,
                "C" or "С" => QualityClass.C,
                "C+" or "С+" => QualityClass.C, // B-, B, B+, C ? Нет C+ класса в enum'e
                _ => QualityClass.None
            };
        }

        private string ResolveAddress(realtyfeedOfferLocation location)
        {
            var partsList = new List<string>();
            if (location.country.IsNotEmpty()) partsList.Add(location.country);
            if (location.region.IsNotEmpty()) partsList.Add(location.region);
            if (location.district.IsNotEmpty()) partsList.Add(location.district);
            if (location.localityname.IsNotEmpty()) partsList.Add(location.localityname);
            if (location.sublocalityname.IsNotEmpty()) partsList.Add(location.sublocalityname);
            if (location.address.IsNotEmpty()) partsList.Add(location.address);
            return partsList.Count == 0 ? "-" : string.Join(", ", partsList);
        }

        private MarketTypes ResolveMarketType(string input)
        {
            if (input.Contains("cian.ru")) return MarketTypes.Cian;
            return MarketTypes.None;
        }

        private HouseType ResolveHouseType(string input)
        {
            return input switch
            {
                "блочный" => HouseType.Block,
                "деревянный" => HouseType.Wood,
                "кирпичный" => HouseType.Brick,
                "кирпично-монолитный" => HouseType.MonolithBrick,
                "монолит" => HouseType.Monolith,
                "панельный" => HouseType.Panel,
                _ => HouseType.None
            };
        }

        private string ResolveDeveloper(realtyfeedOfferSalesagent salesagent)
        {
            return salesagent.category is "developer" or "застройщик" ? salesagent.organization : null;
        }

        private decimal ResolveArea(realtyfeedOffer offer)
        {
            decimal area;
            if (offer.category is "дом с участком" or "house with lot" or "участок" or "lot")
            {
                var unit = ResolveAreaUnit(offer.lotarea.unit);
                area = offer.lotarea.value.ParseToDecimal() * unit;
            }
            else
            {
                var unit = ResolveAreaUnit(offer.area.unit);
                area = offer.area.value.ParseToDecimal() * unit;
            }

            return area;
        }

        private decimal ResolveAreaUnit(string unit)
        {
            return unit switch
            {
                "кв. м" or "sq. m" => 1,
                "сотка" => 100,
                "гектар" or "hectare" => 10000,
                _ => 0
            };
        }

        #endregion

        #region Creators

        // Разделение на случай необходимости правок по одному из типов обозначенных Яндексом
        private OMCoreObject CreateLiving(realtyfeedOffer offer)
        {
            return CreateBase(offer);
        }

        private OMCoreObject CreateGarage(realtyfeedOffer offer)
        {
            return CreateBase(offer);
        }

        private OMCoreObject CreateCommercial(realtyfeedOffer offer)
        {
            return CreateBase(offer);
        }

        private OMCoreObject CreateBase(realtyfeedOffer offer)
        {
            OMCoreObject newObject = OMCoreObject.CreateEmpty();
            newObject.Market_Code = ResolveMarketType(offer.url);
            newObject.Price = offer.price?.value.ParseToDecimal() ?? 0;
            newObject.Address = ResolveAddress(offer.location);
            newObject.DealType_Code = ResolveDealType(offer.type);
            newObject.FloorNumber = offer.floor.ParseToLong();

            newObject.Area = ResolveArea(offer);
            newObject.CadastralNumber = offer.cadastralnumber;
            newObject.PropertyMarketSegment_Code = ResolveMarketSegment(offer.category);
            newObject.QualityClass_Code = ResolveQualityCLass(offer.officeclass);
            newObject.PropertyTypesCIPJS_Code = PropertyTypesCIPJS.None;

            newObject.DownloadDate = offer.lastupdatedate.ParseToDateTime();
            newObject.ExternalAdvertisementId = string.Empty;
            newObject.AdvertisementDescription = offer.description;
            newObject.AreaFrom = null;
            newObject.Name = offer.buildingname;

            newObject.FlatNumber = offer.location?.apartment.ParseToLongNullable();
            newObject.SectionNumber = null;
            newObject.FlatType = null;
            newObject.HouseLine_Code = HouseLineType.None;
            newObject.Developer = ResolveDeveloper(offer.salesagent);

            newObject.FinishingCondition_Code = ResolveConditionType(offer.renovation);
            newObject.HouseType_Code = ResolveHouseType(offer.buildingtype);
            newObject.Layout_Code = Layout.None;
            newObject.PermittedUseType_Code = PermittedUseType.None;
            newObject.DrivewayType_Code = DrivewayType.None;

            newObject.Latitude = offer.location?.latitude.ParseToDecimalNullable();
            newObject.Longitude = offer.location?.longitude.ParseToDecimalNullable();
            return newObject;
        }

        #endregion
    }
}