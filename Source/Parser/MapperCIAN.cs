using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ObjectModel.Directory;

namespace Parser
{

    class MapperCIAN : IMapperLinkFormer
    {

        private readonly Dictionary<string, ValuePair> _dealTypes = new Dictionary<string, ValuePair>
        {
            { "Sale", new ValuePair { dealType = DealType.SaleSuggestion, urlPart = "deal_type=sale", comment = "продажа" } },
            { "Rent", new ValuePair { dealType = DealType.RentSuggestion, urlPart = "deal_type=rent", comment = "аренда" } }
        };

        private readonly Dictionary<string, SegmentValuePair> _segments = new Dictionary<string, SegmentValuePair>
        {
            {
                "Flat",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.MZHS,
                    urlMainPart = "offer_type=flat",
                    urlType = UrlType.Root,
                    comment = "квартира"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&offer_type=flat&region=1
            },
            {
                "FlatPart",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.MZHS,
                    urlMainPart = "offer_type=flat",
                    urlExectPart = "room8=1",
                    urlType = UrlType.Ordinar,
                    comment = "доля квартиры"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&offer_type=flat&region=1&room8=1
            },
            {
                "House",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Buildings,
                    marketSegment = MarketSegment.IZHS,
                    urlMainPart = "offer_type=suburban",
                    urlExectPart = "object_type%5B0%5D=1",
                    urlType = UrlType.Ordinar,
                    comment = "дом"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=rent&engine_version=2&object_type%5B0%5D=1&offer_type=suburban&region=1
            },
            {
                "HousePart",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Buildings,
                    marketSegment = MarketSegment.IZHS,
                    urlMainPart = "offer_type=suburban",
                    urlExectPart = "object_type%5B0%5D=2",
                    urlType = UrlType.Ordinar,
                    comment = "часть дома"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&object_type%5B0%5D=2&offer_type=suburban&region=1&p=5
            },
            {
                "TownHouse",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Buildings,
                    marketSegment = MarketSegment.IZHS,
                    urlMainPart = "offer_type=suburban",
                    urlExectPart = "object_type%5B0%5D=4",
                    urlType = UrlType.Ordinar,
                    comment = "таунхаус"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=rent&engine_version=2&object_type%5B0%5D=4&offer_type=suburban&region=1&p=5
            },
            {
                "Stead",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.LandArea,
                    marketSegment = MarketSegment.IZHS,
                    urlMainPart = "offer_type=suburban",
                    urlExectPart = "object_type%5B0%5D=3",
                    urlType = UrlType.Ordinar,
                    comment = "участок"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&object_type%5B0%5D=3&offer_type=suburban&p=3&region=1
            },
            {
                "Office",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Office,
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=1",
                    urlType = UrlType.Ordinar,
                    comment = "офис"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=rent&engine_version=2&offer_type=offices&office_type%5B0%5D=1&region=1
            },
            {
                "Halls",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Trading,
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=2",
                    urlType = UrlType.Ordinar,
                    comment = "торговая площадь"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&offer_type=offices&office_type%5B0%5D=2&region=1&p=5
            },
            {
                "Warehouse",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Factory,
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=3",
                    urlType = UrlType.Ordinar,
                    comment = "склад"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=rent&engine_version=2&offer_type=offices&office_type%5B0%5D=3&p=3&region=1&p=5
            },
            {
                "MultipurposeRetail",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.NoSegment,
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=5",
                    urlType = UrlType.Ordinar,
                    comment = "помещение свободного назначения"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&offer_type=offices&office_type%5B0%5D=5&region=1
            },
            {
                "PublicCatering",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.PublicCatering,
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=4",
                    urlType = UrlType.Ordinar,
                    comment = "общепит"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=rent&engine_version=2&offer_type=offices&office_type%5B0%5D=4&region=1
            },
            {
                "Garage",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Parking, //<- Может быть изменено! Смотреть заголовок (Гараж или машиноместо)
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=6",
                    urlType = UrlType.Ordinar,
                    comment = "гараж"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&offer_type=offices&office_type%5B0%5D=6&region=1&p=5
            },
            {
                "Manufacture",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.OtherAndMore, //<- Может быть изменено! Смотреть описание (Помещение, здание или сооружение)
                    marketSegment = MarketSegment.Factory,
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=7",
                    urlType = UrlType.Ordinar,
                    comment = "производство"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=rent&engine_version=2&offer_type=offices&office_type%5B0%5D=7&region=1&p=5
            },
            {
                "CarService",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements, //<- Может быть изменено! Смотреть описание (Помещение, здание или сооружение)
                    marketSegment = MarketSegment.Trading,
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=9",
                    urlType = UrlType.Ordinar,
                    comment = "автосервис"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&offer_type=offices&office_type%5B0%5D=9&region=1
            },
            {
                "Business",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,     //<- Может быть изменено! Смотреть описание и заголовок
                    marketSegment = MarketSegment.Trading,            //<- Может быть изменено! Смотреть описание 
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=10",
                    urlType = UrlType.Ordinar,
                    comment = "готовый бизнес"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=rent&engine_version=2&offer_type=offices&office_type%5B0%5D=10&region=1
            },
            {
                "Building",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Buildings,
                    marketSegment = MarketSegment.NoSegment,          //<- Может быть изменено! Смотреть описание
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=11",
                    urlType = UrlType.Ordinar,
                    comment = "здание"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=sale&engine_version=2&offer_type=offices&office_type%5B0%5D=11&region=1&p=5
            },
            {
                "ConsumerServices",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Trading,
                    urlMainPart = "offer_type=offices",
                    urlExectPart = "office_type%5B0%5D=12",
                    urlType = UrlType.Ordinar,
                    comment = "бытовые услуги"
                }
                //Пример: https://www.cian.ru/cat.php?deal_type=rent&engine_version=2&offer_type=offices&office_type%5B0%5D=12&region=1&p=5
            }
        };

        private readonly List<DistrictValuePair> _districtsList = new List<DistrictValuePair>
        {
            //Восточный
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Bogorodskoye, parameterValue = "56", parameterUrlType = "district%5B0%5D", parameterName = "Богородское" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Veshnyaki, parameterValue = "57", parameterUrlType = "district%5B0%5D", parameterName = "Вешняки" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.VostochnoyeIzmaylovo, parameterValue = "59", parameterUrlType = "district%5B0%5D", parameterName = "Восточное Измайлово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Vostochny, parameterValue = "58", parameterUrlType = "district%5B0%5D", parameterName = "Восточный" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Golyanovo, parameterValue = "60", parameterUrlType = "district%5B0%5D", parameterName = "Гольяново" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Ivanovskoye, parameterValue = "61", parameterUrlType = "district%5B0%5D", parameterName = "Ивановское" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Izmaylovo, parameterValue = "62", parameterUrlType = "district%5B0%5D", parameterName = "Измайлово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.KosinoUkhtomsky, parameterValue = "63", parameterUrlType = "district%5B0%5D", parameterName = "Косино-Ухтомский" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Metrogorodok, parameterValue = "64", parameterUrlType = "district%5B0%5D", parameterName = "Метрогородок" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Novogireyevo, parameterValue = "65", parameterUrlType = "district%5B0%5D", parameterName = "Новогиреево" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Novokosino, parameterValue = "66", parameterUrlType = "district%5B0%5D", parameterName = "Новокосино" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Perovo, parameterValue = "67", parameterUrlType = "district%5B0%5D", parameterName = "Перово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Preobrazhenskoye, parameterValue = "68", parameterUrlType = "district%5B0%5D", parameterName = "Преображенское" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.SevernoyeIzmaylovo, parameterValue = "69", parameterUrlType = "district%5B0%5D", parameterName = "Северное Измайлово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.SokolinayaGora, parameterValue = "70", parameterUrlType = "district%5B0%5D", parameterName = "Соколиная гора" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Sokolniki, parameterValue = "71", parameterUrlType = "district%5B0%5D", parameterName = "Сокольники" },
            //Западный
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Vnukovo, parameterValue = "112", parameterUrlType = "district%5B0%5D", parameterName = "Внуково" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Dorogomilovo, parameterValue = "113", parameterUrlType = "district%5B0%5D", parameterName = "Дорогомилово" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Krylatskoye, parameterValue = "114", parameterUrlType = "district%5B0%5D", parameterName = "Крылатское" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Kuntsevo, parameterValue = "115", parameterUrlType = "district%5B0%5D", parameterName = "Кунцево" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Mozhaysky, parameterValue = "116", parameterUrlType = "district%5B0%5D", parameterName = "Можайский" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.NovoPeredelkino, parameterValue = "117", parameterUrlType = "district%5B0%5D", parameterName = "Ново-Переделкино" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.OchakovoMatveyevskoye, parameterValue = "118", parameterUrlType = "district%5B0%5D", parameterName = "Очаково-Матвеевское" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.ProspektVernadskogo, parameterValue = "119", parameterUrlType = "district%5B0%5D", parameterName = "Проспект Вернадского" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Ramenki, parameterValue = "120", parameterUrlType = "district%5B0%5D", parameterName = "Раменки" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Solntsevo, parameterValue = "121", parameterUrlType = "district%5B0%5D", parameterName = "Солнцево" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.TroparyovoNikulino, parameterValue = "122", parameterUrlType = "district%5B0%5D", parameterName = "Тропарево-Никулино" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.FilyovskyPark, parameterValue = "123", parameterUrlType = "district%5B0%5D", parameterName = "Филевский парк" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.FiliDavydkovo, parameterValue = "124", parameterUrlType = "district%5B0%5D", parameterName = "Фили-Давыдково" },
            //new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Konezavod, parameterValue = "349", parameterUrlType = "district%5B0%5D", parameterName = "Конезавод" },
            //new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.RublyovoArhangelskoe, parameterValue = "348", parameterUrlType = "district%5B0%5D", parameterName = "Рублёво-Архангельское" },
            //new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Skolkovo, parameterValue = "350", parameterUrlType = "district%5B0%5D", parameterName = "Сколково" },
            //Зеленоградский
            new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Kryukovo, parameterValue = "154", parameterUrlType = "district%5B0%5D", parameterName = "Крюково" },
            new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Matushkino, parameterValue = "355", parameterUrlType = "district%5B0%5D", parameterName = "Матушкино" },
            new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Savyolki, parameterValue = "356", parameterUrlType = "district%5B0%5D", parameterName = "Савёлки" },
            new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Silino, parameterValue = "357", parameterUrlType = "district%5B0%5D", parameterName = "Силино" },
            new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.StaroyeKryukovo, parameterValue = "358", parameterUrlType = "district%5B0%5D", parameterName = "Старое Крюково" },
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.MatushkinoSavyolki, parameterValue = "152", parameterUrlType = "district%5B0%5D", parameterName = "Матушкино-Савёлки" },
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Panfilovskij, parameterValue = "153", parameterUrlType = "district%5B0%5D", parameterName = "Панфиловский" },
            //Новомосковский
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Vnukovskoye, parameterValue = "328", parameterUrlType = "district%5B0%5D", parameterName = "Внуковское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Voskresenskoye, parameterValue = "327", parameterUrlType = "district%5B0%5D", parameterName = "Воскресенское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Desyonovskoye, parameterValue = "329", parameterUrlType = "district%5B0%5D", parameterName = "Десёновское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Kokoshkino, parameterValue = "330", parameterUrlType = "district%5B0%5D", parameterName = "Кокошкино" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Marushkinskoye, parameterValue = "331", parameterUrlType = "district%5B0%5D", parameterName = "Марушкинское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Moskovsky, parameterValue = "332", parameterUrlType = "district%5B0%5D", parameterName = "Московский" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Mosrentgen, parameterValue = "333", parameterUrlType = "district%5B0%5D", parameterName = "Мосрентген" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Ryazanovskoye, parameterValue = "334", parameterUrlType = "district%5B0%5D", parameterName = "Рязановское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Sosenskoye, parameterValue = "335", parameterUrlType = "district%5B0%5D", parameterName = "Сосенское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Filimonkovskoye, parameterValue = "336", parameterUrlType = "district%5B0%5D", parameterName = "Филимонковское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Shcherbinka, parameterValue = "337", parameterUrlType = "district%5B0%5D", parameterName = "Щербинка" },
            //Северный
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Aeroport, parameterValue = "23", parameterUrlType = "district%5B0%5D", parameterName = "Аэропорт" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Begovoy, parameterValue = "24", parameterUrlType = "district%5B0%5D", parameterName = "Беговой" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Beskudnikovsky, parameterValue = "25", parameterUrlType = "district%5B0%5D", parameterName = "Бескудниковский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Voykovsky, parameterValue = "26", parameterUrlType = "district%5B0%5D", parameterName = "Войковский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.VostochnoyeDegunino, parameterValue = "27", parameterUrlType = "district%5B0%5D", parameterName = "Восточное Дегунино" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Golovinsky, parameterValue = "28", parameterUrlType = "district%5B0%5D", parameterName = "Головинский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Dmitrovsky, parameterValue = "29", parameterUrlType = "district%5B0%5D", parameterName = "Дмитровский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.ZapadnoyeDegunino, parameterValue = "30", parameterUrlType = "district%5B0%5D", parameterName = "Западное Дегунино" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Koptevo, parameterValue = "31", parameterUrlType = "district%5B0%5D", parameterName = "Коптево" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Levoberezhny, parameterValue = "32", parameterUrlType = "district%5B0%5D", parameterName = "Левобережный" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Molzhaninovsky, parameterValue = "33", parameterUrlType = "district%5B0%5D", parameterName = "Молжаниновский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Savyolovsky, parameterValue = "34", parameterUrlType = "district%5B0%5D", parameterName = "Савеловский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Sokol, parameterValue = "35", parameterUrlType = "district%5B0%5D", parameterName = "Сокол" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Timiryazevsky, parameterValue = "36", parameterUrlType = "district%5B0%5D", parameterName = "Тимирязевский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Khovrino, parameterValue = "37", parameterUrlType = "district%5B0%5D", parameterName = "Ховрино" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Khoroshyovsky, parameterValue = "38", parameterUrlType = "district%5B0%5D", parameterName = "Хорошевский" },
            //Северо-Восточный
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Alexeyevsky, parameterValue = "39", parameterUrlType = "district%5B0%5D", parameterName = "Алексеевский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Altufyevsky, parameterValue = "40", parameterUrlType = "district%5B0%5D", parameterName = "Алтуфьевский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Babushkinsky, parameterValue = "41", parameterUrlType = "district%5B0%5D", parameterName = "Бабушкинский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Bibirevo, parameterValue = "42", parameterUrlType = "district%5B0%5D", parameterName = "Бибирево" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Butyrsky, parameterValue = "43", parameterUrlType = "district%5B0%5D", parameterName = "Бутырский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Lianozovo, parameterValue = "44", parameterUrlType = "district%5B0%5D", parameterName = "Лианозово" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Losinoostrovsky, parameterValue = "45", parameterUrlType = "district%5B0%5D", parameterName = "Лосиноостровский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Marfino, parameterValue = "46", parameterUrlType = "district%5B0%5D", parameterName = "Марфино" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.MaryinaRoshcha, parameterValue = "47", parameterUrlType = "district%5B0%5D", parameterName = "Марьина роща" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Ostankinsky, parameterValue = "48", parameterUrlType = "district%5B0%5D", parameterName = "Останкинский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Otradnoye, parameterValue = "49", parameterUrlType = "district%5B0%5D", parameterName = "Отрадное" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Rostokino, parameterValue = "50", parameterUrlType = "district%5B0%5D", parameterName = "Ростокино" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Sviblovo, parameterValue = "51", parameterUrlType = "district%5B0%5D", parameterName = "Свиблово" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.SevernoyeMedvedkovo, parameterValue = "53", parameterUrlType = "district%5B0%5D", parameterName = "Северное Медведково" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Severny, parameterValue = "52", parameterUrlType = "district%5B0%5D", parameterName = "Северный" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.YuzhnoyeMedvedkovo, parameterValue = "54", parameterUrlType = "district%5B0%5D", parameterName = "Южное Медведково" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Yaroslavsky, parameterValue = "55", parameterUrlType = "district%5B0%5D", parameterName = "Ярославский" },
            //Северо-Западный
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Kurkino, parameterValue = "125", parameterUrlType = "district%5B0%5D", parameterName = "Куркино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Mitino, parameterValue = "126", parameterUrlType = "district%5B0%5D", parameterName = "Митино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.PokrovskoyeStreshnevo, parameterValue = "127", parameterUrlType = "district%5B0%5D", parameterName = "Покровское-Стрешнево" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.SevernoyeTushino, parameterValue = "128", parameterUrlType = "district%5B0%5D", parameterName = "Северное Тушино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Strogino, parameterValue = "129", parameterUrlType = "district%5B0%5D", parameterName = "Строгино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.KhoroshyovoMnyovniki, parameterValue = "130", parameterUrlType = "district%5B0%5D", parameterName = "Хорошево-Мневники" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Shchukino, parameterValue = "131", parameterUrlType = "district%5B0%5D", parameterName = "Щукино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.YuzhnoyeTushino, parameterValue = "132", parameterUrlType = "district%5B0%5D", parameterName = "Южное Тушино" },
            //Троицкий
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Voronovskoye, parameterValue = "338", parameterUrlType = "district%5B0%5D", parameterName = "Вороновское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Kiyevsky, parameterValue = "339", parameterUrlType = "district%5B0%5D", parameterName = "Киевский" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Klenovskoye, parameterValue = "340", parameterUrlType = "district%5B0%5D", parameterName = "Клёновское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Krasnopakhorskoye, parameterValue = "341", parameterUrlType = "district%5B0%5D", parameterName = "Краснопахорское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.MikhaylovoYartsevskoye, parameterValue = "342", parameterUrlType = "district%5B0%5D", parameterName = "Михайлово-Ярцевское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Novofyodorovskoye, parameterValue = "343", parameterUrlType = "district%5B0%5D", parameterName = "Новофёдоровское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Pervomayskoye, parameterValue = "344", parameterUrlType = "district%5B0%5D", parameterName = "Первомайское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Rogovskoye, parameterValue = "345", parameterUrlType = "district%5B0%5D", parameterName = "Роговское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Troitsky, parameterValue = "346", parameterUrlType = "district%5B0%5D", parameterName = "Троицк" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Shchapovskoye, parameterValue = "347", parameterUrlType = "district%5B0%5D", parameterName = "Щаповское" },
            //Центральный
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Arbat, parameterValue = "13", parameterUrlType = "district%5B0%5D", parameterName = "Арбат" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Basmanny, parameterValue = "14", parameterUrlType = "district%5B0%5D", parameterName = "Басманный" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Zamoskvorechye, parameterValue = "15", parameterUrlType = "district%5B0%5D", parameterName = "Замоскворечье" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Krasnoselsky, parameterValue = "16", parameterUrlType = "district%5B0%5D", parameterName = "Красносельский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Meshchansky, parameterValue = "17", parameterUrlType = "district%5B0%5D", parameterName = "Мещанский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Presnensky, parameterValue = "18", parameterUrlType = "district%5B0%5D", parameterName = "Пресненский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Tagansky, parameterValue = "19", parameterUrlType = "district%5B0%5D", parameterName = "Таганский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Tverskoy, parameterValue = "20", parameterUrlType = "district%5B0%5D", parameterName = "Тверской" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Khamovniki, parameterValue = "21", parameterUrlType = "district%5B0%5D", parameterName = "Хамовники" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Yakimanka, parameterValue = "22", parameterUrlType = "district%5B0%5D", parameterName = "Якиманка" },
            //Юго-Восточный
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.VykhinoZhulebino, parameterValue = "72", parameterUrlType = "district%5B0%5D", parameterName = "Выхино-Жулебино" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Kapotnya, parameterValue = "73", parameterUrlType = "district%5B0%5D", parameterName = "Капотня" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Kuzminki, parameterValue = "74", parameterUrlType = "district%5B0%5D", parameterName = "Кузьминки" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Lefortovo, parameterValue = "75", parameterUrlType = "district%5B0%5D", parameterName = "Лефортово" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Lyublino, parameterValue = "76", parameterUrlType = "district%5B0%5D", parameterName = "Люблино" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Maryino, parameterValue = "77", parameterUrlType = "district%5B0%5D", parameterName = "Марьино" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Nekrasovka, parameterValue = "78", parameterUrlType = "district%5B0%5D", parameterName = "Некрасовка" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Nizhegorodsky, parameterValue = "79", parameterUrlType = "district%5B0%5D", parameterName = "Нижегородский" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Pechatniki, parameterValue = "80", parameterUrlType = "district%5B0%5D", parameterName = "Печатники" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Ryazansky, parameterValue = "81", parameterUrlType = "district%5B0%5D", parameterName = "Рязанский" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Tekstilshchiki, parameterValue = "82", parameterUrlType = "district%5B0%5D", parameterName = "Текстильщики" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Yuzhnoportovy, parameterValue = "83", parameterUrlType = "district%5B0%5D", parameterName = "Южнопортовый" },
            //Юго-Западный
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Akademichesky, parameterValue = "100", parameterUrlType = "district%5B0%5D", parameterName = "Академический" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Gagarinsky, parameterValue = "101", parameterUrlType = "district%5B0%5D", parameterName = "Гагаринский" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Zyuzino, parameterValue = "102", parameterUrlType = "district%5B0%5D", parameterName = "Зюзино" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Konkovo, parameterValue = "103", parameterUrlType = "district%5B0%5D", parameterName = "Коньково" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Kotlovka, parameterValue = "104", parameterUrlType = "district%5B0%5D", parameterName = "Котловка" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Lomonosovsky, parameterValue = "105", parameterUrlType = "district%5B0%5D", parameterName = "Ломоносовский" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Obruchevsky, parameterValue = "106", parameterUrlType = "district%5B0%5D", parameterName = "Обручевский" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.SevernoyeButovo, parameterValue = "107", parameterUrlType = "district%5B0%5D", parameterName = "Северное Бутово" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.TyoplyStan, parameterValue = "108", parameterUrlType = "district%5B0%5D", parameterName = "Теплый Стан" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Cheryomushki, parameterValue = "109", parameterUrlType = "district%5B0%5D", parameterName = "Черемушки" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.YuzhnoyeButovo, parameterValue = "110", parameterUrlType = "district%5B0%5D", parameterName = "Южное Бутово" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Yasenevo, parameterValue = "111", parameterUrlType = "district%5B0%5D", parameterName = "Ясенево" },
            //Южный
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.BiryulyovoVostochnoye, parameterValue = "84", parameterUrlType = "district%5B0%5D", parameterName = "Бирюлево Восточное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.BiryulyovoZapadnoye, parameterValue = "85", parameterUrlType = "district%5B0%5D", parameterName = "Бирюлево Западное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Brateyevo, parameterValue = "86", parameterUrlType = "district%5B0%5D", parameterName = "Братеево" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Danilovsky, parameterValue = "87", parameterUrlType = "district%5B0%5D", parameterName = "Даниловский" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Donskoy, parameterValue = "88", parameterUrlType = "district%5B0%5D", parameterName = "Донской" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Zyablikovo, parameterValue = "89", parameterUrlType = "district%5B0%5D", parameterName = "Зябликово" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.MoskvorechyeSaburovo, parameterValue = "90", parameterUrlType = "district%5B0%5D", parameterName = "Москворечье-Сабурово" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.NagatinoSadovniki, parameterValue = "91", parameterUrlType = "district%5B0%5D", parameterName = "Нагатино-Садовники" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.NagatinskyZaton, parameterValue = "92", parameterUrlType = "district%5B0%5D", parameterName = "Нагатинский затон" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Nagorny, parameterValue = "93", parameterUrlType = "district%5B0%5D", parameterName = "Нагорный" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.OrekhovoBorisovoSevernoye, parameterValue = "94", parameterUrlType = "district%5B0%5D", parameterName = "Орехово-Борисово Северное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.OrekhovoBorisovoYuzhnoye, parameterValue = "95", parameterUrlType = "district%5B0%5D", parameterName = "Орехово-Борисово Южное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Tsaritsyno, parameterValue = "96", parameterUrlType = "district%5B0%5D", parameterName = "Царицыно" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.ChertanovoSevernoye, parameterValue = "97", parameterUrlType = "district%5B0%5D", parameterName = "Чертаново Северное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.ChertanovoTsentralnoye, parameterValue = "98", parameterUrlType = "district%5B0%5D", parameterName = "Чертаново Центральное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.ChertanovoYuzhnoye, parameterValue = "99", parameterUrlType = "district%5B0%5D", parameterName = "Чертаново Южное" }
        };

        public int _onPageCounter = 28;
        public int _maximumPageCounter = 54;

        private MarketTypes _marketType = MarketTypes.Cian;
        private ProcessStep _processStep = ProcessStep.DoNotProcessed;

        private string[] _rentExceptions = { "доля квартиры" };

        private string _ordinarTemplate = "https://www.cian.ru/cat.php?engine_version=2&region=1&{0}&{1}&{2}&{3}={4}&p={5}";
        private string _rootTemplate = "https://www.cian.ru/cat.php?engine_version=2&region=1&{0}&{1}&{2}={3}&p={4}";

        public string GetLink(ValuePair dealType, SegmentValuePair segment, DistrictValuePair district, int pageNumber)
        {
            switch (segment.urlType)
            {
                case UrlType.Ordinar: return string.Format(_ordinarTemplate, dealType.urlPart, segment.urlMainPart, segment.urlExectPart, district.parameterUrlType, district.parameterValue, pageNumber);
                case UrlType.Root: return string.Format(_rootTemplate, dealType.urlPart, segment.urlMainPart, district.parameterUrlType, district.parameterValue, pageNumber);
                default: return string.Format(_ordinarTemplate, dealType.urlPart, segment.urlMainPart, segment.urlExectPart, district.parameterUrlType, district.parameterValue, pageNumber);
            }
        }

        public List<MappingMetaInfo> FormLinks(Dictionary<string, ValuePair> dealTypes, Dictionary<string, SegmentValuePair> segments, List<DistrictValuePair> districtsList)
        {
            List<MappingMetaInfo> result = new List<MappingMetaInfo>();
            foreach (KeyValuePair<string, ValuePair> dealType in dealTypes) 
                foreach (KeyValuePair<string, SegmentValuePair> segment in segments)
                    foreach (DistrictValuePair district in districtsList)
                        if(dealType.Value.dealType != DealType.RentSuggestion || !_rentExceptions.Contains(segment.Value.comment))
                            result.Add(new MappingMetaInfo 
                            {
                                marketType = _marketType,
                                dealType = dealType.Value.dealType,
                                hunted = district.hunted,
                                district = district.district,
                                propertyType = segment.Value.propertyType,
                                marketSegment = segment.Value.marketSegment,
                                processStep = _processStep,
                                urlAddress = $"{GetLink(dealType.Value, segment.Value, district, 1)}"
                            });
            return result;
        }

        public List<MappingMetaInfo> GetLinks() => FormLinks(_dealTypes, _segments, _districtsList);

    }

}
