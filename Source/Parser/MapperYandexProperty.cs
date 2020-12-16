using System;
using System.Collections.Generic;
using System.Text;

using ObjectModel.Directory;

namespace Parser
{

    class MapperYandexProperty : IMapperLinkFormer
    {

        private readonly Dictionary<string, ValuePair> _dealTypes = new Dictionary<string, ValuePair>
        {
            { "Sale", new ValuePair { dealType = DealType.SaleSuggestion, urlPart = "kupit", comment = "продажа" } },
            { "Rent", new ValuePair { dealType = DealType.RentSuggestion, urlPart = "snyat", comment = "аренда" } }
        };

        private readonly Dictionary<string, SegmentValuePair> _segments = new Dictionary<string, SegmentValuePair>
        {
            {
                "Office",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Office,
                    urlMainPart = "kommercheskaya-nedvizhimost",
                    urlExectPart = "ofis",
                    asParameter = "commercialType=OFFICE",
                    urlType = UrlType.Ordinar,
                    comment = "офис"
                }
                //Пример: https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/ofis/
            },
            {
                "Trading",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Trading,
                    urlMainPart = "kommercheskaya-nedvizhimost",
                    urlExectPart = "torgovoe-pomeshchenie",
                    asParameter = "commercialType=RETAIL",
                    urlType = UrlType.Ordinar,
                    comment = "торговое помещение"
                }
                //Пример: https://realty.yandex.ru/moskva/snyat/kommercheskaya-nedvizhimost/torgovoe-pomeshchenie/
            },
            {
                "MultipurposeRetail",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Trading,
                    urlMainPart = "kommercheskaya-nedvizhimost",
                    urlExectPart = "pomeshchenie-svobodnogo-naznacheniya",
                    asParameter = "commercialType=FREE_PURPOSE",
                    urlType = UrlType.Ordinar,
                    comment = "помещение свободного назначения"
                }
                //Пример: https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/pomeshchenie-svobodnogo-naznacheniya/?page=5
            },
            {
                "Warehouse",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,       //<- Может быть изменено! Смотреть описание и заголовок
                    marketSegment = MarketSegment.Factory,
                    urlMainPart = "kommercheskaya-nedvizhimost",
                    urlExectPart = "sklad",
                    asParameter = "commercialType=WAREHOUSE",
                    urlType = UrlType.Ordinar,
                    comment = "складское помещение"
                }
                //Пример: https://realty.yandex.ru/moskva/snyat/kommercheskaya-nedvizhimost/sklad/?page=5
            },
            {
                "Manufacture",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,        //<- Может быть изменено! Смотреть заголовок (Помещение, здание или сооружение)
                    marketSegment = MarketSegment.Factory,
                    urlMainPart = "kommercheskaya-nedvizhimost",
                    urlExectPart = "proizvodstvennoe-pomeshchenie",
                    asParameter = "commercialType=MANUFACTURING",
                    urlType = UrlType.Ordinar,
                    comment = "производственное помещение"
                }
                //Пример: https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/?commercialType=MANUFACTURING
            },
            {
                "PublicCatering",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.PublicCatering,
                    urlMainPart = "kommercheskaya-nedvizhimost",
                    urlExectPart = "obshchepit",
                    asParameter = "commercialType=PUBLIC_CATERING",
                    urlType = UrlType.Ordinar,
                    comment = "общепит"
                }
                //Пример: https://realty.yandex.ru/moskva/snyat/kommercheskaya-nedvizhimost/?commercialType=PUBLIC_CATERING
            },
            {
                "CarService",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,       //<- Может быть изменено! Смотреть описание (Помещение, здание или сооружение)
                    marketSegment = MarketSegment.Trading,
                    urlMainPart = "kommercheskaya-nedvizhimost",
                    urlExectPart = "avtoservis",
                    asParameter = "commercialType=AUTO_REPAIR",
                    urlType = UrlType.Ordinar,
                    comment = "автосервис"
                }
                //Пример: https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/?commercialType=AUTO_REPAIR&page=5
            },
            {
                "Hotel",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Buildings,        //<- Может быть изменено! Смотреть заголовок (Помещение или здание)
                    marketSegment = MarketSegment.Hotel,
                    urlMainPart = "kommercheskaya-nedvizhimost",
                    urlExectPart = "gostinica",
                    asParameter = "commercialType=HOTEL",
                    urlType = UrlType.Ordinar,
                    comment = "гостиница"
                }
                //Пример: https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/?commercialType=HOTEL&page=5
            },
            {
                "Business",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,     //<- Может быть изменено! Смотреть описание и заголовок
                    marketSegment = MarketSegment.Trading,            //<- Может быть изменено! Смотреть описание 
                    urlMainPart = "kommercheskaya-nedvizhimost",
                    urlExectPart = "gotovyj-biznes",
                    asParameter = "commercialType=BUSINESS",
                    urlType = UrlType.Ordinar,
                    comment = "готовый бизнес"
                }
                //Пример: https://realty.yandex.ru/moskva/kupit/kommercheskaya-nedvizhimost/gotovyj-biznes/
            },
            {
                "Box",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Parking,
                    urlMainPart = "garazh",
                    asParameter = "garageType=BOX",
                    urlType = UrlType.AsParameter,
                    comment = "бокс"
                }
                //Пример: https://realty.yandex.ru/moskva/kupit/garazh/?garageType=BOX
            },
            {
                "Garage",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.Parking,
                    urlMainPart = "garazh",
                    asParameter = "garageType=GARAGE",
                    urlType = UrlType.AsParameter,
                    comment = "гараж"
                }
                //Пример: https://realty.yandex.ru/moskva/snyat/garazh/?garageType=GARAGE
            },
            {
                "CarParking",
                new SegmentValuePair
                {
                    propertyType = PropertyTypesCIPJS.Placements,
                    marketSegment = MarketSegment.CarParking,
                    urlMainPart = "garazh",
                    asParameter = "garageType=PARKING_PLACE",
                    urlType = UrlType.AsParameter,
                    comment = "машиноместо"
                }
                //Пример: https://realty.yandex.ru/moskva/kupit/garazh/?garageType=PARKING_PLACE&page=5
            }
        };

        private readonly List<DistrictValuePair> _districtsList = new List<DistrictValuePair>
        {
            //Восточный
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Bogorodskoye, parameterValue = "193391", parameterUrlType = "subLocality", parameterName = "Богородское" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Veshnyaki, parameterValue = "193283", parameterUrlType = "subLocality", parameterName = "Вешняки" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.VostochnoyeIzmaylovo, parameterValue = "193292", parameterUrlType = "subLocality", parameterName = "Восточное Измайлово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Vostochny, parameterValue = "193380", parameterUrlType = "subLocality", parameterName = "Восточный" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Golyanovo, parameterValue = "193296", parameterUrlType = "subLocality", parameterName = "Гольяново" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Ivanovskoye, parameterValue = "193305", parameterUrlType = "subLocality", parameterName = "Ивановское" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Izmaylovo, parameterValue = "193362", parameterUrlType = "subLocality", parameterName = "Измайлово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.KosinoUkhtomsky, parameterValue = "193288", parameterUrlType = "subLocality", parameterName = "Косино-Ухтомский" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Metrogorodok, parameterValue = "193394", parameterUrlType = "subLocality", parameterName = "Метрогородок" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Novogireyevo, parameterValue = "193358", parameterUrlType = "subLocality", parameterName = "Новогиреево" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Novokosino, parameterValue = "12447", parameterUrlType = "subLocality", parameterName = "Новокосино" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Perovo, parameterValue = "193363", parameterUrlType = "subLocality", parameterName = "Перово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Preobrazhenskoye, parameterValue = "193367", parameterUrlType = "subLocality", parameterName = "Преображенское" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.SevernoyeIzmaylovo, parameterValue = "193313", parameterUrlType = "subLocality", parameterName = "Северное Измайлово" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.SokolinayaGora, parameterValue = "12444", parameterUrlType = "subLocality", parameterName = "Соколиная гора" },
            new DistrictValuePair { hunted = Hunteds.VAO, district = Districts.Sokolniki, parameterValue = "12425", parameterUrlType = "subLocality", parameterName = "Сокольники" },
            //Западный
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Vnukovo, parameterValue = "193401", parameterUrlType = "subLocality", parameterName = "Внуково" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Dorogomilovo, parameterValue = "193300", parameterUrlType = "subLocality", parameterName = "Дорогомилово" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Krylatskoye, parameterValue = "193383", parameterUrlType = "subLocality", parameterName = "Крылатское" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Kuntsevo, parameterValue = "12445", parameterUrlType = "subLocality", parameterName = "Кунцево" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Mozhaysky, parameterValue = "193323", parameterUrlType = "subLocality", parameterName = "Можайский" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.NovoPeredelkino, parameterValue = "193359", parameterUrlType = "subLocality", parameterName = "Ново-Переделкино" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.OchakovoMatveyevskoye, parameterValue = "193286", parameterUrlType = "subLocality", parameterName = "Очаково-Матвеевское" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.ProspektVernadskogo, parameterValue = "12442", parameterUrlType = "subLocality", parameterName = "Проспект Вернадского" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Ramenki, parameterValue = "193370", parameterUrlType = "subLocality", parameterName = "Раменки" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Solntsevo, parameterValue = "12427", parameterUrlType = "subLocality", parameterName = "Солнцево" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.TroparyovoNikulino, parameterValue = "17367988", parameterUrlType = "subLocality", parameterName = "Тропарево-Никулино" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.FilyovskyPark, parameterValue = "196718", parameterUrlType = "subLocality", parameterName = "Филевский парк" },
            new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.FiliDavydkovo, parameterValue = "193320", parameterUrlType = "subLocality", parameterName = "Фили-Давыдково" },
            //new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Konezavod, parameterValue = "-", parameterUrlType = "subLocality", parameterName = "Конезавод" },
            //new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.RublyovoArhangelskoe, parameterValue = "-", parameterUrlType = "subLocality", parameterName = "Рублёво-Архангельское" },
            //new DistrictValuePair { hunted = Hunteds.ZAO, district = Districts.Skolkovo, parameterValue = "-", parameterUrlType = "subLocality", parameterName = "Сколково" },
            //Зеленоградский
            new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Kryukovo, parameterValue = "17367987", parameterUrlType = "subLocality", parameterName = "Крюково" },
            new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Matushkino, parameterValue = "17367989", parameterUrlType = "subLocality", parameterName = "Матушкино" },
            new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Savyolki, parameterValue = "17367990", parameterUrlType = "subLocality", parameterName = "Савёлки" },
            new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Silino, parameterValue = "17367992", parameterUrlType = "subLocality", parameterName = "Силино" },
            new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.StaroyeKryukovo, parameterValue = "17367991", parameterUrlType = "subLocality", parameterName = "Старое Крюково" },
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.MatushkinoSavyolki, parameterValue = "-", parameterUrlType = "subLocality", parameterName = "Матушкино-Савёлки" },
            //new DistrictValuePair { hunted = Hunteds.ZelAO, district = Districts.Panfilovskij, parameterValue = "-", parameterUrlType = "subLocality", parameterName = "Панфиловский" },
            //Новомосковский
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Vnukovskoye, parameterValue = "227292", parameterUrlType = "subLocality", parameterName = "Внуковское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Voskresenskoye, parameterValue = "227303", parameterUrlType = "subLocality", parameterName = "Воскресенское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Desyonovskoye, parameterValue = "230748", parameterUrlType = "subLocality", parameterName = "Десёновское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Kokoshkino, parameterValue = "17385366", parameterUrlType = "subLocality", parameterName = "Кокошкино" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Marushkinskoye, parameterValue = "230733", parameterUrlType = "subLocality", parameterName = "Марушкинское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Moskovsky, parameterValue = "17385368", parameterUrlType = "subLocality", parameterName = "Московский" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Mosrentgen, parameterValue = "230737", parameterUrlType = "subLocality", parameterName = "Мосрентген" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Ryazanovskoye, parameterValue = "17367500", parameterUrlType = "subLocality", parameterName = "Рязановское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Sosenskoye, parameterValue = "230739", parameterUrlType = "subLocality", parameterName = "Сосенское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Filimonkovskoye, parameterValue = "17385367", parameterUrlType = "subLocality", parameterName = "Филимонковское" },
            new DistrictValuePair { hunted = Hunteds.NAO, district = Districts.Shcherbinka, parameterValue = "17380912", parameterUrlType = "subLocality", parameterName = "Щербинка" },
            //Северный
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Aeroport, parameterValue = "193279", parameterUrlType = "subLocality", parameterName = "Аэропорт" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Begovoy, parameterValue = "193328", parameterUrlType = "subLocality", parameterName = "Беговой" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Beskudnikovsky, parameterValue = "193280", parameterUrlType = "subLocality", parameterName = "Бескудниковский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Voykovsky, parameterValue = "193290", parameterUrlType = "subLocality", parameterName = "Войковский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.VostochnoyeDegunino, parameterValue = "193291", parameterUrlType = "subLocality", parameterName = "Восточное Дегунино" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Golovinsky, parameterValue = "193295", parameterUrlType = "subLocality", parameterName = "Головинский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Dmitrovsky, parameterValue = "193298", parameterUrlType = "subLocality", parameterName = "Дмитровский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.ZapadnoyeDegunino, parameterValue = "193302", parameterUrlType = "subLocality", parameterName = "Западное Дегунино" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Koptevo, parameterValue = "193307", parameterUrlType = "subLocality", parameterName = "Коптево" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Levoberezhny, parameterValue = "12433", parameterUrlType = "subLocality", parameterName = "Левобережный" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Molzhaninovsky, parameterValue = "193379", parameterUrlType = "subLocality", parameterName = "Молжаниновский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Savyolovsky, parameterValue = "193373", parameterUrlType = "subLocality", parameterName = "Савеловский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Sokol, parameterValue = "193314", parameterUrlType = "subLocality", parameterName = "Сокол" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Timiryazevsky, parameterValue = "193319", parameterUrlType = "subLocality", parameterName = "Тимирязевский" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Khovrino, parameterValue = "193326", parameterUrlType = "subLocality", parameterName = "Ховрино" },
            new DistrictValuePair { hunted = Hunteds.SAO, district = Districts.Khoroshyovsky, parameterValue = "12441", parameterUrlType = "subLocality", parameterName = "Хорошевский" },
            //Северо-Восточный
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Alexeyevsky, parameterValue = "193393", parameterUrlType = "subLocality", parameterName = "Алексеевский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Altufyevsky, parameterValue = "193278", parameterUrlType = "subLocality", parameterName = "Алтуфьевский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Babushkinsky, parameterValue = "193347", parameterUrlType = "subLocality", parameterName = "Бабушкинский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Bibirevo, parameterValue = "12435", parameterUrlType = "subLocality", parameterName = "Бибирево" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Butyrsky, parameterValue = "193282", parameterUrlType = "subLocality", parameterName = "Бутырский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Lianozovo, parameterValue = "193377", parameterUrlType = "subLocality", parameterName = "Лианозово" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Losinoostrovsky, parameterValue = "193346", parameterUrlType = "subLocality", parameterName = "Лосиноостровский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Marfino, parameterValue = "193349", parameterUrlType = "subLocality", parameterName = "Марфино" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.MaryinaRoshcha, parameterValue = "193350", parameterUrlType = "subLocality", parameterName = "Марьина роща" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Ostankinsky, parameterValue = "193371", parameterUrlType = "subLocality", parameterName = "Останкинский" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Otradnoye, parameterValue = "193285", parameterUrlType = "subLocality", parameterName = "Отрадное" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Rostokino, parameterValue = "193392", parameterUrlType = "subLocality", parameterName = "Ростокино" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Sviblovo, parameterValue = "193374", parameterUrlType = "subLocality", parameterName = "Свиблово" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.SevernoyeMedvedkovo, parameterValue = "193340", parameterUrlType = "subLocality", parameterName = "Северное Медведково" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Severny, parameterValue = "17394073", parameterUrlType = "subLocality", parameterName = "Северный" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.YuzhnoyeMedvedkovo, parameterValue = "193341", parameterUrlType = "subLocality", parameterName = "Южное Медведково" },
            new DistrictValuePair { hunted = Hunteds.SVAO, district = Districts.Yaroslavsky, parameterValue = "193345", parameterUrlType = "subLocality", parameterName = "Ярославский" },
            //Северо-Западный
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Kurkino, parameterValue = "12432", parameterUrlType = "subLocality", parameterName = "Куркино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Mitino, parameterValue = "12451", parameterUrlType = "subLocality", parameterName = "Митино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.PokrovskoyeStreshnevo, parameterValue = "193366", parameterUrlType = "subLocality", parameterName = "Покровское-Стрешнево" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.SevernoyeTushino, parameterValue = "193352", parameterUrlType = "subLocality", parameterName = "Северное Тушино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Strogino, parameterValue = "193317", parameterUrlType = "subLocality", parameterName = "Строгино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.KhoroshyovoMnyovniki, parameterValue = "193327", parameterUrlType = "subLocality", parameterName = "Хорошево-Мневники" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.Shchukino, parameterValue = "12439", parameterUrlType = "subLocality", parameterName = "Щукино" },
            new DistrictValuePair { hunted = Hunteds.SZAO, district = Districts.YuzhnoyeTushino, parameterValue = "12448", parameterUrlType = "subLocality", parameterName = "Южное Тушино" },
            //Троицкий
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Voronovskoye, parameterValue = "17385372", parameterUrlType = "subLocality", parameterName = "Вороновское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Kiyevsky, parameterValue = "17385373", parameterUrlType = "subLocality", parameterName = "Киевский" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Klenovskoye, parameterValue = "230753", parameterUrlType = "subLocality", parameterName = "Клёновское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Krasnopakhorskoye, parameterValue = "17385369", parameterUrlType = "subLocality", parameterName = "Краснопахорское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.MikhaylovoYartsevskoye, parameterValue = "17385370", parameterUrlType = "subLocality", parameterName = "Михайлово-Ярцевское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Novofyodorovskoye, parameterValue = "17385374", parameterUrlType = "subLocality", parameterName = "Новофёдоровское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Pervomayskoye, parameterValue = "17385365", parameterUrlType = "subLocality", parameterName = "Первомайское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Rogovskoye, parameterValue = "17385375", parameterUrlType = "subLocality", parameterName = "Роговское" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Troitsky, parameterValue = "227307", parameterUrlType = "subLocality", parameterName = "Троицк" },
            new DistrictValuePair { hunted = Hunteds.TAO, district = Districts.Shchapovskoye, parameterValue = "17385371", parameterUrlType = "subLocality", parameterName = "Щаповское" },
            //Центральный
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Arbat, parameterValue = "193324", parameterUrlType = "subLocality", parameterName = "Арбат" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Basmanny, parameterValue = "193389", parameterUrlType = "subLocality", parameterName = "Басманный" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Zamoskvorechye, parameterValue = "193301", parameterUrlType = "subLocality", parameterName = "Замоскворечье" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Krasnoselsky, parameterValue = "193308", parameterUrlType = "subLocality", parameterName = "Красносельский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Meshchansky, parameterValue = "12437", parameterUrlType = "subLocality", parameterName = "Мещанский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Presnensky, parameterValue = "193368", parameterUrlType = "subLocality", parameterName = "Пресненский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Tagansky, parameterValue = "193318", parameterUrlType = "subLocality", parameterName = "Таганский" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Tverskoy, parameterValue = "12438", parameterUrlType = "subLocality", parameterName = "Тверской" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Khamovniki, parameterValue = "193321", parameterUrlType = "subLocality", parameterName = "Хамовники" },
            new DistrictValuePair { hunted = Hunteds.CAO, district = Districts.Yakimanka, parameterValue = "193344", parameterUrlType = "subLocality", parameterName = "Якиманка" },
            //Юго-Восточный
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.VykhinoZhulebino, parameterValue = "193293", parameterUrlType = "subLocality", parameterName = "Выхино-Жулебино" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Kapotnya, parameterValue = "12431", parameterUrlType = "subLocality", parameterName = "Капотня" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Kuzminki, parameterValue = "12434", parameterUrlType = "subLocality", parameterName = "Кузьминки" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Lefortovo, parameterValue = "193388", parameterUrlType = "subLocality", parameterName = "Лефортово" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Lyublino, parameterValue = "193348", parameterUrlType = "subLocality", parameterName = "Люблино" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Maryino, parameterValue = "193351", parameterUrlType = "subLocality", parameterName = "Марьино" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Nekrasovka, parameterValue = "193378", parameterUrlType = "subLocality", parameterName = "Некрасовка" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Nizhegorodsky, parameterValue = "193357", parameterUrlType = "subLocality", parameterName = "Нижегородский" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Pechatniki, parameterValue = "193364", parameterUrlType = "subLocality", parameterName = "Печатники" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Ryazansky, parameterValue = "12453", parameterUrlType = "subLocality", parameterName = "Рязанский" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Tekstilshchiki, parameterValue = "12452", parameterUrlType = "subLocality", parameterName = "Текстильщики" },
            new DistrictValuePair { hunted = Hunteds.YVAO, district = Districts.Yuzhnoportovy, parameterValue = "12449", parameterUrlType = "subLocality", parameterName = "Южнопортовый" },
            //Юго-Западный
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Akademichesky, parameterValue = "12446", parameterUrlType = "subLocality", parameterName = "Академический" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Gagarinsky, parameterValue = "193294", parameterUrlType = "subLocality", parameterName = "Гагаринский" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Zyuzino, parameterValue = "193303", parameterUrlType = "subLocality", parameterName = "Зюзино" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Konkovo, parameterValue = "193387", parameterUrlType = "subLocality", parameterName = "Коньково" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Kotlovka, parameterValue = "196717", parameterUrlType = "subLocality", parameterName = "Котловка" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Lomonosovsky, parameterValue = "193334", parameterUrlType = "subLocality", parameterName = "Ломоносовский" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Obruchevsky, parameterValue = "193360", parameterUrlType = "subLocality", parameterName = "Обручевский" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.SevernoyeButovo, parameterValue = "193338", parameterUrlType = "subLocality", parameterName = "Северное Бутово" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.TyoplyStan, parameterValue = "193375", parameterUrlType = "subLocality", parameterName = "Теплый Стан" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Cheryomushki, parameterValue = "193332", parameterUrlType = "subLocality", parameterName = "Черемушки" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.YuzhnoyeButovo, parameterValue = "193339", parameterUrlType = "subLocality", parameterName = "Южное Бутово" },
            new DistrictValuePair { hunted = Hunteds.YZAO, district = Districts.Yasenevo, parameterValue = "193386", parameterUrlType = "subLocality", parameterName = "Ясенево" },
            //Южный
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.BiryulyovoVostochnoye, parameterValue = "193384", parameterUrlType = "subLocality", parameterName = "Бирюлево Восточное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.BiryulyovoZapadnoye, parameterValue = "193385", parameterUrlType = "subLocality", parameterName = "Бирюлево Западное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Brateyevo, parameterValue = "193281", parameterUrlType = "subLocality", parameterName = "Братеево" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Danilovsky, parameterValue = "193297", parameterUrlType = "subLocality", parameterName = "Даниловский" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Donskoy, parameterValue = "193299", parameterUrlType = "subLocality", parameterName = "Донской" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Zyablikovo, parameterValue = "193304", parameterUrlType = "subLocality", parameterName = "Зябликово" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.MoskvorechyeSaburovo, parameterValue = "193289", parameterUrlType = "subLocality", parameterName = "Москворечье-Сабурово" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.NagatinoSadovniki, parameterValue = "12450", parameterUrlType = "subLocality", parameterName = "Нагатино-Садовники" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.NagatinskyZaton, parameterValue = "193355", parameterUrlType = "subLocality", parameterName = "Нагатинский затон" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Nagorny, parameterValue = "193356", parameterUrlType = "subLocality", parameterName = "Нагорный" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.OrekhovoBorisovoSevernoye, parameterValue = "193361", parameterUrlType = "subLocality", parameterName = "Орехово-Борисово Северное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.OrekhovoBorisovoYuzhnoye, parameterValue = "193284", parameterUrlType = "subLocality", parameterName = "Орехово-Борисово Южное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.Tsaritsyno, parameterValue = "12440", parameterUrlType = "subLocality", parameterName = "Царицыно" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.ChertanovoSevernoye, parameterValue = "12443", parameterUrlType = "subLocality", parameterName = "Чертаново Северное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.ChertanovoTsentralnoye, parameterValue = "193336", parameterUrlType = "subLocality", parameterName = "Чертаново Центральное" },
            new DistrictValuePair { hunted = Hunteds.YAO, district = Districts.ChertanovoYuzhnoye, parameterValue = "193337", parameterUrlType = "subLocality", parameterName = "Чертаново Южное" }
        };

        public int _onPageCounter = 25;
        public int _maximumPageCounter = 25; //от 0 до 24

        private MarketTypes _marketType = MarketTypes.YandexProterty;
        private ProcessStep _processStep = ProcessStep.DoNotProcessed;

        private string _ordinarTemplate = "https://realty.yandex.ru/moskva/{0}/{1}/{2}/?{3}={4}&page={5}";
        private string _asParameterTemplate = "https://realty.yandex.ru/moskva/{0}/{1}/?{2}&{3}={4}&page={5}";

        public string GetLink(ValuePair dealType, SegmentValuePair segment, DistrictValuePair district, int pageNumber)
        {
            switch (segment.urlType)
            {
                case UrlType.Ordinar: return string.Format(_ordinarTemplate, dealType.urlPart, segment.urlMainPart, segment.urlExectPart, district.parameterUrlType, district.parameterValue, pageNumber);
                case UrlType.AsParameter: return string.Format(_asParameterTemplate, dealType.urlPart, segment.urlMainPart, segment.asParameter, district.parameterUrlType, district.parameterValue, pageNumber);
                default: return string.Format(_ordinarTemplate, dealType.urlPart, segment.urlMainPart, segment.urlExectPart, district.parameterUrlType, district.parameterValue, pageNumber);
            }
        }

        public List<MappingMetaInfo> FormLinks(Dictionary<string, ValuePair> dealTypes, Dictionary<string, SegmentValuePair> segments, List<DistrictValuePair> districtsList)
        {
            List<MappingMetaInfo> result = new List<MappingMetaInfo>();
            foreach (KeyValuePair<string, ValuePair> dealType in dealTypes)
                foreach (KeyValuePair<string, SegmentValuePair> segment in segments)
                    foreach (DistrictValuePair district in districtsList)
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
