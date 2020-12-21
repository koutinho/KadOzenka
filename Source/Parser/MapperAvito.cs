using System;
using System.Collections.Generic;
using System.Text;

using ObjectModel.Directory;

namespace Parser
{

    class MapperAvito : IMapperLinkFormer
    {

        private readonly Dictionary<string, ValuePair> _dealTypes = new Dictionary<string, ValuePair>
        {
            { "Sale", new ValuePair { dealType = DealType.SaleSuggestion, urlPart = "prodam", comment = "продажа" } },
            { "Rent", new ValuePair { dealType = DealType.RentSuggestion, urlPart = "sdam", comment = "аренда" } }
        };

        private readonly Dictionary<string, SegmentValuePair> _segments = new Dictionary<string, SegmentValuePair>
        {
            {
                "Office",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Office,
                    urlMainPart = "kommercheskaya_nedvizhimost",
                    urlExectPart = "ofis",                              //"ofis-ASgBAQICAUSwCNJWAUCGCRSKXQ"
                    urlType = UrlType.Ordinar,
                    comment = "офис"
                }
                //Пример: https://www.avito.ru/moskva/kommercheskaya_nedvizhimost/prodam/ofis-ASgBAQICAUSwCNJWAUCGCRSKXQ
            },
            {
                "MultipurposeRetail",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,       //<- Может быть изменено! Смотреть заголовок
                    marketSegment = MarketSegment.Trading,
                    urlMainPart = "kommercheskaya_nedvizhimost",
                    urlExectPart = "drugoe",                            //"drugoe-ASgBAQICAUSwCNJWAUCGCRSOXQ"
                    urlType = UrlType.Ordinar,
                    comment = "свободного назначения"
                }
                //Пример: https://www.avito.ru/moskva/kommercheskaya_nedvizhimost/sdam/drugoe-ASgBAQICAUSwCNRWAUDUCBS6WQ
            },
            {
                "Halls",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Trading,
                    urlMainPart = "kommercheskaya_nedvizhimost",
                    urlExectPart = "magazin",                           //"magazin-ASgBAQICAUSwCNJWAUCGCRSQXQ"
                    urlType = UrlType.Ordinar,
                    comment = "торговая площадь"
                }
                //Пример: https://www.avito.ru/moskva/kommercheskaya_nedvizhimost/prodam/magazin-ASgBAQICAUSwCNJWAUCGCRSQXQ?p=5
            },
            {
                "Warehouse",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,       //<- Может быть изменено! Смотреть описание и заголовок
                    marketSegment = MarketSegment.Factory,
                    urlMainPart = "kommercheskaya_nedvizhimost",
                    urlExectPart = "sklad",                             //"sklad" "sklad-ASgBAQICAUSwCNJWAUCGCRSSXQ"
                    urlType = UrlType.Ordinar,
                    comment = "склад"
                }
                //Пример: https://www.avito.ru/moskva/kommercheskaya_nedvizhimost/sdam/sklad-ASgBAQICAUSwCNJWAUCGCRSSXQ?p=5
            },
            {
                "Manufacture",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.OtherAndMore,     //<- Может быть изменено! Смотреть заголовок (Помещение, здание или сооружение)
                    marketSegment = MarketSegment.Factory,
                    urlMainPart = "kommercheskaya_nedvizhimost",
                    urlExectPart = "proizvodstvo",                      //"proizvodstvo-ASgBAQICAUSwCNJWAUCGCRSMXQ"
                    urlType = UrlType.Ordinar,
                    comment = "производство"
                }
                //Пример: https://www.avito.ru/moskva/kommercheskaya_nedvizhimost/prodam/proizvodstvo-ASgBAQICAUSwCNJWAUCGCRSMXQ
            },
            {
                "PublicCatering",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.PublicCatering,
                    urlMainPart = "kommercheskaya_nedvizhimost",
                    urlExectPart = "obshestvennoe_pitanie",             //"obshestvennoe_pitanie-ASgBAQICAUSwCNJWAUCGCRTA_wE"
                    urlType = UrlType.Ordinar,
                    comment = "общепит"
                }
                //Пример: https://www.avito.ru/moskva/kommercheskaya_nedvizhimost/sdam/obshestvennoe_pitanie-ASgBAQICAUSwCNJWAUCGCRTA_wE
            },
            {
                "Hotel",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Buildings,        //<- Может быть изменено! Смотреть заголовок (Помещение или здание)
                    marketSegment = MarketSegment.Hotel,
                    urlMainPart = "kommercheskaya_nedvizhimost",
                    urlExectPart = "gostinicy",                         //"gostinicy-ASgBAQICAUSwCNJWAUCGCRSKrAE"
                    urlType = UrlType.Ordinar,
                    comment = "гостиница"
                }
                //Пример: https://www.avito.ru/moskva/kommercheskaya_nedvizhimost/prodam/gostinicy-ASgBAQICAUSwCNJWAUCGCRSKrAE?p=5
            },
            {
                "Garage",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Parking,
                    urlMainPart = "garazhi_i_mashinomesta",
                    urlExectPart = "garazh",                            //"garazh-ASgBAgICAkSYA~QQqAjsVQ"
                    urlType = UrlType.Ordinar,
                    comment = "гараж"
                }
                //Пример: https://www.avito.ru/moskva/garazhi_i_mashinomesta/sdam/garazh-ASgBAgICAkSYA~QQqAjsVQ?p=5
            },
            {
                "CarParking",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.CarParking,
                    urlMainPart = "garazhi_i_mashinomesta",
                    urlExectPart = "mashinomesto",                      //"mashinomesto-ASgBAgICAkSYA~QQqAjuVQ"
                    urlType = UrlType.Ordinar,
                    comment = "машиноместо"
                }
                //Пример: https://www.avito.ru/moskva/garazhi_i_mashinomesta/prodam/mashinomesto-ASgBAgICAkSYA~QQqAjuVQ
            }
        };

        private readonly List<DistrictValuePair> _districtsList = new List<DistrictValuePair>
        {
            //Восточный
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Bogorodskoye, parameterValue = "628", parameterUrlType = "district", parameterName = "Богородское" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Veshnyaki, parameterValue = "631", parameterUrlType = "district", parameterName = "Вешняки" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.VostochnoyeIzmaylovo, parameterValue = "635", parameterUrlType = "district", parameterName = "Восточное Измайлово" },
            //new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Vostochny, parameterValue = "-", parameterUrlType = "district", parameterName = "Восточный" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Golyanovo, parameterValue = "640", parameterUrlType = "district", parameterName = "Гольяново" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Ivanovskoye, parameterValue = "649", parameterUrlType = "district", parameterName = "Ивановское" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Izmaylovo, parameterValue = "650", parameterUrlType = "district", parameterName = "Измайлово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.KosinoUkhtomsky, parameterValue = "654", parameterUrlType = "district", parameterName = "Косино-Ухтомский" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Metrogorodok, parameterValue = "672", parameterUrlType = "district", parameterName = "Метрогородок" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Novogireyevo, parameterValue = "683", parameterUrlType = "district", parameterName = "Новогиреево" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Novokosino, parameterValue = "684", parameterUrlType = "district", parameterName = "Новокосино" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Perovo, parameterValue = "692", parameterUrlType = "district", parameterName = "Перово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Preobrazhenskoye, parameterValue = "695", parameterUrlType = "district", parameterName = "Преображенское" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.SevernoyeIzmaylovo, parameterValue = "705", parameterUrlType = "district", parameterName = "Северное Измайлово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.SokolinayaGora, parameterValue = "711", parameterUrlType = "district", parameterName = "Соколиная гора" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Sokolniki, parameterValue = "712", parameterUrlType = "district", parameterName = "Сокольники" },
            //Западный
            //new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Vnukovo, parameterValue = "-", parameterUrlType = "district", parameterName = "Внуково" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Dorogomilovo, parameterValue = "644", parameterUrlType = "district", parameterName = "Дорогомилово" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Krylatskoye, parameterValue = "657", parameterUrlType = "district", parameterName = "Крылатское" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Kuntsevo, parameterValue = "660", parameterUrlType = "district", parameterName = "Кунцево" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Mozhaysky, parameterValue = "675", parameterUrlType = "district", parameterName = "Можайский" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.NovoPeredelkino, parameterValue = "685", parameterUrlType = "district", parameterName = "Ново-Переделкино" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.OchakovoMatveyevskoye, parameterValue = "691", parameterUrlType = "district", parameterName = "Очаково-Матвеевское" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.ProspektVernadskogo, parameterValue = "697", parameterUrlType = "district", parameterName = "Проспект Вернадского" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Ramenki, parameterValue = "698", parameterUrlType = "district", parameterName = "Раменки" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Solntsevo, parameterValue = "713", parameterUrlType = "district", parameterName = "Солнцево" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.TroparyovoNikulino, parameterValue = "721", parameterUrlType = "district", parameterName = "Тропарево-Никулино" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.FilyovskyPark, parameterValue = "722", parameterUrlType = "district", parameterName = "Филевский парк" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.FiliDavydkovo, parameterValue = "723", parameterUrlType = "district", parameterName = "Фили-Давыдково" },
            //new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Konezavod, parameterValue = "-", parameterUrlType = "district", parameterName = "Конезавод" },
            //new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.RublyovoArhangelskoe, parameterValue = "-", parameterUrlType = "district", parameterName = "Рублёво-Архангельское" },
            //new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Skolkovo, parameterValue = "-", parameterUrlType = "district", parameterName = "Сколково" },
            //Зеленоградский
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Kryukovo, parameterValue = "-", parameterUrlType = "district", parameterName = "Крюково" },
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Matushkino, parameterValue = "-", parameterUrlType = "district", parameterName = "Матушкино" },
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Savyolki, parameterValue = "-", parameterUrlType = "district", parameterName = "Савёлки" },
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Silino, parameterValue = "-", parameterUrlType = "district", parameterName = "Силино" },
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.StaroyeKryukovo, parameterValue = "-", parameterUrlType = "district", parameterName = "Старое Крюково" },
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.MatushkinoSavyolki, parameterValue = "-", parameterUrlType = "district", parameterName = "Матушкино-Савёлки" },
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Panfilovskij, parameterValue = "-", parameterUrlType = "district", parameterName = "Панфиловский" },
            //Новомосковский
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Vnukovskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Внуковское" },
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Voskresenskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Воскресенское" },
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Desyonovskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Десёновское" },
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Kokoshkino, parameterValue = "-", parameterUrlType = "district", parameterName = "Кокошкино" },
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Marushkinskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Марушкинское" },
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Moskovsky, parameterValue = "-", parameterUrlType = "district", parameterName = "Московский" },
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Mosrentgen, parameterValue = "-", parameterUrlType = "district", parameterName = "Мосрентген" },
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Ryazanovskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Рязановское" },
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Sosenskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Сосенское" },
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Filimonkovskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Филимонковское" },
            //new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Shcherbinka, parameterValue = "-", parameterUrlType = "district", parameterName = "Щербинка" },
            //Северный
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Aeroport, parameterValue = "620", parameterUrlType = "district", parameterName = "Аэропорт" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Begovoy, parameterValue = "623", parameterUrlType = "district", parameterName = "Беговой" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Beskudnikovsky, parameterValue = "624", parameterUrlType = "district", parameterName = "Бескудниковский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Voykovsky, parameterValue = "633", parameterUrlType = "district", parameterName = "Войковский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.VostochnoyeDegunino, parameterValue = "634", parameterUrlType = "district", parameterName = "Восточное Дегунино" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Golovinsky, parameterValue = "639", parameterUrlType = "district", parameterName = "Головинский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Dmitrovsky, parameterValue = "642", parameterUrlType = "district", parameterName = "Дмитровский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.ZapadnoyeDegunino, parameterValue = "646", parameterUrlType = "district", parameterName = "Западное Дегунино" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Koptevo, parameterValue = "653", parameterUrlType = "district", parameterName = "Коптево" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Levoberezhny, parameterValue = "662", parameterUrlType = "district", parameterName = "Левобережный" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Molzhaninovsky, parameterValue = "676", parameterUrlType = "district", parameterName = "Молжаниновский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Savyolovsky, parameterValue = "702", parameterUrlType = "district", parameterName = "Савеловский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Sokol, parameterValue = "710", parameterUrlType = "district", parameterName = "Сокол" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Timiryazevsky, parameterValue = "720", parameterUrlType = "district", parameterName = "Тимирязевский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Khovrino, parameterValue = "725", parameterUrlType = "district", parameterName = "Ховрино" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Khoroshyovsky, parameterValue = "727", parameterUrlType = "district", parameterName = "Хорошевский" },
            //Северо-Восточный
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Alexeyevsky, parameterValue = "617", parameterUrlType = "district", parameterName = "Алексеевский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Altufyevsky, parameterValue = "618", parameterUrlType = "district", parameterName = "Алтуфьевский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Babushkinsky, parameterValue = "621", parameterUrlType = "district", parameterName = "Бабушкинский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Bibirevo, parameterValue = "625", parameterUrlType = "district", parameterName = "Бибирево" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Butyrsky, parameterValue = "630", parameterUrlType = "district", parameterName = "Бутырский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Lianozovo, parameterValue = "664", parameterUrlType = "district", parameterName = "Лианозово" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Losinoostrovsky, parameterValue = "666", parameterUrlType = "district", parameterName = "Лосиноостровский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Marfino, parameterValue = "668", parameterUrlType = "district", parameterName = "Марфино" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.MaryinaRoshcha, parameterValue = "669", parameterUrlType = "district", parameterName = "Марьина роща" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Ostankinsky, parameterValue = "689", parameterUrlType = "district", parameterName = "Останкинский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Otradnoye, parameterValue = "690", parameterUrlType = "district", parameterName = "Отрадное" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Rostokino, parameterValue = "699", parameterUrlType = "district", parameterName = "Ростокино" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Sviblovo, parameterValue = "703", parameterUrlType = "district", parameterName = "Свиблово" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.SevernoyeMedvedkovo, parameterValue = "706", parameterUrlType = "district", parameterName = "Северное Медведково" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Severny, parameterValue = "708", parameterUrlType = "district", parameterName = "Северный" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.YuzhnoyeMedvedkovo, parameterValue = "735", parameterUrlType = "district", parameterName = "Южное Медведково" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Yaroslavsky, parameterValue = "739", parameterUrlType = "district", parameterName = "Ярославский" },
            //Северо-Западный
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Kurkino, parameterValue = "661", parameterUrlType = "district", parameterName = "Куркино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Mitino, parameterValue = "674", parameterUrlType = "district", parameterName = "Митино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.PokrovskoyeStreshnevo, parameterValue = "694", parameterUrlType = "district", parameterName = "Покровское-Стрешнево" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.SevernoyeTushino, parameterValue = "707", parameterUrlType = "district", parameterName = "Северное Тушино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Strogino, parameterValue = "715", parameterUrlType = "district", parameterName = "Строгино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.KhoroshyovoMnyovniki, parameterValue = "726", parameterUrlType = "district", parameterName = "Хорошево-Мневники" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Shchukino, parameterValue = "733", parameterUrlType = "district", parameterName = "Щукино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.YuzhnoyeTushino, parameterValue = "736", parameterUrlType = "district", parameterName = "Южное Тушино" },
            //Троицкий
            //new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Voronovskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Вороновское" },
            //new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Kiyevsky, parameterValue = "-", parameterUrlType = "district", parameterName = "Киевский" },
            //new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Klenovskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Клёновское" },
            //new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Krasnopakhorskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Краснопахорское" },
            //new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.MikhaylovoYartsevskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Михайлово-Ярцевское" },
            //new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Novofyodorovskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Новофёдоровское" },
            //new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Pervomayskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Первомайское" },
            //new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Rogovskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Роговское" },
            //new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Troitsky, parameterValue = "-", parameterUrlType = "district", parameterName = "Троицк" },
            //new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Shchapovskoye, parameterValue = "-", parameterUrlType = "district", parameterName = "Щаповское" },
            //Центральный
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Arbat, parameterValue = "619", parameterUrlType = "district", parameterName = "Арбат" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Basmanny, parameterValue = "622", parameterUrlType = "district", parameterName = "Басманный" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Zamoskvorechye, parameterValue = "645", parameterUrlType = "district", parameterName = "Замоскворечье" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Krasnoselsky, parameterValue = "656", parameterUrlType = "district", parameterName = "Красносельский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Meshchansky, parameterValue = "673", parameterUrlType = "district", parameterName = "Мещанский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Presnensky, parameterValue = "696", parameterUrlType = "district", parameterName = "Пресненский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Tagansky, parameterValue = "716", parameterUrlType = "district", parameterName = "Таганский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Tverskoy, parameterValue = "717", parameterUrlType = "district", parameterName = "Тверской" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Khamovniki, parameterValue = "724", parameterUrlType = "district", parameterName = "Хамовники" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Yakimanka, parameterValue = "738", parameterUrlType = "district", parameterName = "Якиманка" },
            //Юго-Восточный
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.VykhinoZhulebino, parameterValue = "637", parameterUrlType = "district", parameterName = "Выхино-Жулебино" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Kapotnya, parameterValue = "651", parameterUrlType = "district", parameterName = "Капотня" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Kuzminki, parameterValue = "659", parameterUrlType = "district", parameterName = "Кузьминки" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Lefortovo, parameterValue = "663", parameterUrlType = "district", parameterName = "Лефортово" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Lyublino, parameterValue = "667", parameterUrlType = "district", parameterName = "Люблино" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Maryino, parameterValue = "670", parameterUrlType = "district", parameterName = "Марьино" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Nekrasovka, parameterValue = "681", parameterUrlType = "district", parameterName = "Некрасовка" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Nizhegorodsky, parameterValue = "682", parameterUrlType = "district", parameterName = "Нижегородский" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Pechatniki, parameterValue = "693", parameterUrlType = "district", parameterName = "Печатники" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Ryazansky, parameterValue = "700", parameterUrlType = "district", parameterName = "Рязанский" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Tekstilshchiki, parameterValue = "718", parameterUrlType = "district", parameterName = "Текстильщики" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Yuzhnoportovy, parameterValue = "737", parameterUrlType = "district", parameterName = "Южнопортовый" },
            //Юго-Западный
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Akademichesky, parameterValue = "616", parameterUrlType = "district", parameterName = "Академический" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Gagarinsky, parameterValue = "638", parameterUrlType = "district", parameterName = "Гагаринский" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Zyuzino, parameterValue = "647", parameterUrlType = "district", parameterName = "Зюзино" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Konkovo, parameterValue = "652", parameterUrlType = "district", parameterName = "Коньково" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Kotlovka, parameterValue = "655", parameterUrlType = "district", parameterName = "Котловка" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Lomonosovsky, parameterValue = "665", parameterUrlType = "district", parameterName = "Ломоносовский" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Obruchevsky, parameterValue = "686", parameterUrlType = "district", parameterName = "Обручевский" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.SevernoyeButovo, parameterValue = "704", parameterUrlType = "district", parameterName = "Северное Бутово" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.TyoplyStan, parameterValue = "719", parameterUrlType = "district", parameterName = "Теплый Стан" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Cheryomushki, parameterValue = "729", parameterUrlType = "district", parameterName = "Черемушки" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.YuzhnoyeButovo, parameterValue = "734", parameterUrlType = "district", parameterName = "Южное Бутово" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Yasenevo, parameterValue = "740", parameterUrlType = "district", parameterName = "Ясенево" },
            //Южный
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.BiryulyovoVostochnoye, parameterValue = "626", parameterUrlType = "district", parameterName = "Бирюлево Восточное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.BiryulyovoZapadnoye, parameterValue = "627", parameterUrlType = "district", parameterName = "Бирюлево Западное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Brateyevo, parameterValue = "629", parameterUrlType = "district", parameterName = "Братеево" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Danilovsky, parameterValue = "641", parameterUrlType = "district", parameterName = "Даниловский" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Donskoy, parameterValue = "643", parameterUrlType = "district", parameterName = "Донской" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Zyablikovo, parameterValue = "648", parameterUrlType = "district", parameterName = "Зябликово" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.MoskvorechyeSaburovo, parameterValue = "677", parameterUrlType = "district", parameterName = "Москворечье-Сабурово" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.NagatinoSadovniki, parameterValue = "678", parameterUrlType = "district", parameterName = "Нагатино-Садовники" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.NagatinskyZaton, parameterValue = "679", parameterUrlType = "district", parameterName = "Нагатинский затон" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Nagorny, parameterValue = "680", parameterUrlType = "district", parameterName = "Нагорный" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.OrekhovoBorisovoSevernoye, parameterValue = "687", parameterUrlType = "district", parameterName = "Орехово-Борисово Северное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.OrekhovoBorisovoYuzhnoye, parameterValue = "688", parameterUrlType = "district", parameterName = "Орехово-Борисово Южное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Tsaritsyno, parameterValue = "728", parameterUrlType = "district", parameterName = "Царицыно" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.ChertanovoSevernoye, parameterValue = "730", parameterUrlType = "district", parameterName = "Чертаново Северное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.ChertanovoTsentralnoye, parameterValue = "731", parameterUrlType = "district", parameterName = "Чертаново Центральное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.ChertanovoYuzhnoye, parameterValue = "732", parameterUrlType = "district", parameterName = "Чертаново Южное" }
        };

        public int _onPageCounter = 50; // 50  + 1 VIP (1x3)
        public int _maximumPageCounter = 100;

        private MarketTypes _marketType = MarketTypes.Avito;
        private ProcessStep _processStep = ProcessStep.DoNotProcessed;

        private string _ordinarTemplate = "https://www.avito.ru/moskva/{0}/{1}/{2}?{3}={4}&p={5}";

        public string GetLink(ValuePair dealType, SegmentValuePair segment, DistrictValuePair districtValuePair, int pageNumber) => 
            string.Format(_ordinarTemplate, segment.urlMainPart, dealType.urlPart, segment.urlExectPart, districtValuePair.parameterUrlType, districtValuePair.parameterValue, pageNumber);

        public List<MappingMetaInfo> FormLinks(Dictionary<string, ValuePair> dealTypes, Dictionary<string, SegmentValuePair> segments, List<DistrictValuePair> districtsList)
        {
            List<MappingMetaInfo> result = new List<MappingMetaInfo>();
            foreach (KeyValuePair<string, ValuePair> dealType in dealTypes)
                foreach (KeyValuePair<string, SegmentValuePair> segment in segments)
                    foreach(DistrictValuePair district in districtsList)
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
