using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.XmlParser
{

    class XMLPolyLineDictionary
    {

        public static string getCorrectNameForDistrict(string name)
        {
            name = name.Replace("  ", " ");
            switch (name)
            {
                case "Центральный": return "ЦАО";
                case "Северный": return "САО";
                case "Северо-Восточный": return "СВАО";
                case "Восточный": return "ВАО";
                case "Юго-Восточный": return "ЮВАО";
                case "Южный": return "ЮАО";
                case "Юго-Западный": return "ЮЗАО";
                case "Западный": return "ЗАО";
                case "Северо-Западный": return "СЗАО";
                case "Зеленоградский": return "ЗелАО";
                case "Новомосковский": return "НАО";
                case "Троицкий": return "ТАО";
            }
            return name;
        }

        public static string getCorrectNameForRegion(string name)
        {
            switch (name)
            {
                case "Дегунино Западное": return "Западное Дегунино";
                case "Савеловский": return "Савёловский";
                case "Хорошевский": return "Хорошёвский";
                case "Медведково северное": return "Северное Медведково";
                case "Медведково южное": return "Южное Медведково";
                case "Восточное измайлово": return "Восточное Измайлово";
                case "Косино-ухтомский": return "Косино-Ухтомский";
                case "Бирюлево Восточное": return "Бирюлёво Восточное";
                case "Бирюлево Западное": return "Бирюлёво Западное";
                case "Москворечье-сабурово": return "Москворечье-Сабурово";
                case "Нагатинский затон": return "Нагатинский Затон";
                case "Теплый стан": return "Тёплый Стан";
                case "Черемушки": return "Черёмушки";
                case "Новопеределкино": return "Ново-Переделкино";
                case "Очаково-матвеевское": return "Очаково-Матвеевское";
                case "Проспект вернадского": return "Проспект Вернадского";
                case "Филевский парк": return "Филёвский Парк";
                case "Фили-давыдково": return "Фили-Давыдково";
                case "Тушино северное": return "Северное Тушино";
                case "Хорошево-мневники": return "Хорошёво-Мнёвники";
                case "Тушино южное": return "Южное Тушино";
                case "Савелки": return "Савёлки";
                case "СП Внуковское": return "Внуковское";
                case "СП Воскресенское": return "Воскресенское";
                case "СП Десеновское": return "Десёновское";
                case "СП Кокошкино": return "Кокошкино";
                case "СП Марушкинское": return "Марушкинское";
                case "СП Московский": return "Московский";
                case "СП \"Мосрентген\"": return "Мосрентген";
                case "СП Рязановское": return "Рязановское";
                case "СП Сосенское": return "Сосенское";
                case "СП Филимонковское": return "Филимонковское";
                case "ГО Щербинка": return "Щербинка";
                case "СП Вороновское": return "Вороновское";
                case "СП Киевский": return "Киевский";
                case "СП Кленовское": return "Клёновское";
                case "СП Краснопахорское": return "Краснопахорское";
                case "СП Михайлово-Ярцевское": return "Михайлово-Ярцевское";
                case "СП Новофедоровское": return "Новофёдоровское";
                case "СП Первомайское": return "Первомайское";
                case "СП Роговское": return "Роговское";
                case "ГО Троицк": return "Троицк";
                case "СП Щаповское": return "Щаповское";
            }
            return name;
        }

        public static string getColorForDistrict(string number)
        {
            switch (number)
            {
                case "ЦАО": return "#ffb2b6";
                case "САО": return "#ffe9b2";
                case "СВАО": return "#feffcc";
                case "ВАО": return "#ccffb2";
                case "ЮВАО": return "#b2fff1";
                case "ЮАО": return "#cce5ff";
                case "ЮЗАО": return "#d2cefa";
                case "ЗАО": return "#ffbffc";
                case "СЗАО": return "#ffccb2";
                case "ЗелАО": return "#ffb073";
                case "НАО": return "#aaffcc";
                case "ТАО": return "#ddff55";
                default: return "#00FF00";
            }
        }

        public static string getColorForRegion(string number)
        {
            switch (number)
            {
                //ЦАО
                case "Арбат": return "#ffb2b6";
                case "Басманный": return "#ffb2b6";
                case "Замоскворечье": return "#ffb2b6";
                case "Красносельский": return "#ffb2b6";
                case "Мещанский": return "#ffb2b6";
                case "Пресненский": return "#ffb2b6";
                case "Таганский": return "#ffb2b6";
                case "Тверской": return "#ffb2b6";
                case "Хамовники": return "#ffb2b6";
                case "Якиманка": return "#ffb2b6";
                //САО
                case "Аэропорт": return "#ffe9b2";
                case "Беговой": return "#ffe9b2";
                case "Бескудниковский": return "#ffe9b2";
                case "Войковский": return "#ffe9b2";
                case "Головинский": return "#ffe9b2";
                case "Восточное Дегунино": return "#ffe9b2";
                case "Западное Дегунино": return "#ffe9b2";
                case "Дмитровский": return "#ffe9b2";
                case "Коптево": return "#ffe9b2";
                case "Левобережный": return "#ffe9b2";
                case "Молжаниновский": return "#ffe9b2";
                case "Савёловский": return "#ffe9b2";
                case "Сокол": return "#ffe9b2";
                case "Тимирязевский": return "#ffe9b2";
                case "Ховрино": return "#ffe9b2";
                case "Хорошёвский": return "#ffe9b2";
                //СВАО
                case "Алексеевский": return "#feffcc";
                case "Алтуфьевский": return "#feffcc";
                case "Бабушкинский": return "#feffcc";
                case "Бибирево": return "#feffcc";
                case "Бутырский": return "#feffcc";
                case "Лианозово": return "#feffcc";
                case "Лосиноостровский": return "#feffcc";
                case "Марфино": return "#feffcc";
                case "Марьина роща": return "#feffcc";
                case "Останкинский": return "#feffcc";
                case "Отрадное": return "#feffcc";
                case "Ростокино": return "#feffcc";
                case "Свиблово": return "#feffcc";
                case "Северное Медведково": return "#feffcc";
                case "Северный": return "#feffcc";
                case "Южное Медведково": return "#feffcc";
                case "Ярославский": return "#feffcc";
                //ВАО
                case "Богородское": return "#ccffb2";
                case "Вешняки": return "#ccffb2";
                case "Восточное Измайлово": return "#ccffb2";
                case "Восточный": return "#ccffb2";
                case "Гольяново": return "#ccffb2";
                case "Ивановское": return "#ccffb2";
                case "Измайлово": return "#ccffb2";
                case "Косино-Ухтомский": return "#ccffb2";
                case "Метрогородок": return "#ccffb2";
                case "Новогиреево": return "#ccffb2";
                case "Новокосино": return "#ccffb2";
                case "Перово": return "#ccffb2";
                case "Преображенское": return "#ccffb2";
                case "Северное Измайлово": return "#ccffb2";
                case "Соколиная гора": return "#ccffb2";
                case "Сокольники": return "#ccffb2";
                //ЮВАО
                case "Выхино-Жулебино": return "#b2fff1";
                case "Капотня": return "#b2fff1";
                case "Кузьминки": return "#b2fff1";
                case "Лефортово": return "#b2fff1";
                case "Люблино": return "#b2fff1";
                case "Марьино": return "#b2fff1";
                case "Некрасовка": return "#b2fff1";
                case "Нижегородский": return "#b2fff1";
                case "Печатники": return "#b2fff1";
                case "Рязанский": return "#b2fff1";
                case "Текстильщики": return "#b2fff1";
                case "Южнопортовый": return "#b2fff1";
                //ЮАО
                case "Бирюлёво Восточное": return "#cce5ff";
                case "Бирюлёво Западное": return "#cce5ff";
                case "Братеево": return "#cce5ff";
                case "Даниловский": return "#cce5ff";
                case "Донской": return "#cce5ff";
                case "Зябликово": return "#cce5ff";
                case "Москворечье-Сабурово": return "#cce5ff";
                case "Нагатино-Садовники": return "#cce5ff";
                case "Нагатинский Затон": return "#cce5ff";
                case "Нагорный": return "#cce5ff";
                case "Орехово-Борисово Северное": return "#cce5ff";
                case "Орехово-Борисово Южное": return "#cce5ff";
                case "Царицыно": return "#cce5ff";
                case "Чертаново Северное": return "#cce5ff";
                case "Чертаново Центральное": return "#cce5ff";
                case "Чертаново Южное": return "#cce5ff";
                //ЮЗАО
                case "Академический": return "#d2cefa";
                case "Гагаринский": return "#d2cefa";
                case "Зюзино": return "#d2cefa";
                case "Коньково": return "#d2cefa";
                case "Котловка": return "#d2cefa";
                case "Ломоносовский": return "#d2cefa";
                case "Обручевский": return "#d2cefa";
                case "Северное Бутово": return "#d2cefa";
                case "Тёплый Стан": return "#d2cefa";
                case "Черёмушки": return "#d2cefa";
                case "Южное Бутово": return "#d2cefa";
                case "Ясенево": return "#d2cefa";
                //ЗАО
                case "Внуково": return "#ffbffc";
                case "Дорогомилово": return "#ffbffc";
                case "Крылатское": return "#ffbffc";
                case "Кунцево": return "#ffbffc";
                case "Можайский": return "#ffbffc";
                case "Ново-Переделкино": return "#ffbffc";
                case "Очаково-Матвеевское": return "#ffbffc";
                case "Проспект Вернадского": return "#ffbffc";
                case "Раменки": return "#ffbffc";
                case "Солнцево": return "#ffbffc";
                case "Тропарево-Никулино": return "#ffbffc";
                case "Филёвский Парк": return "#ffbffc";
                case "Фили-Давыдково": return "#ffbffc";
                //СЗАО
                case "Куркино": return "#ffccb2";
                case "Митино": return "#ffccb2";
                case "Покровское-Стрешнево": return "#ffccb2";
                case "Северное Тушино": return "#ffccb2";
                case "Строгино": return "#ffccb2";
                case "Хорошёво-Мнёвники": return "#ffccb2";
                case "Щукино": return "#ffccb2";
                case "Южное Тушино": return "#ffccb2";
                //ЗелАО
                case "Матушкино": return "#ffb073";
                case "Савёлки": return "#ffb073";
                case "Старое Крюково": return "#ffb073";
                case "Силино": return "#ffb073";
                case "Крюково": return "#ffb073";
                //НАО
                case "Внуковское": return "#aaffcc";
                case "Воскресенское": return "#aaffcc";
                case "Десёновское": return "#aaffcc";
                case "Кокошкино": return "#aaffcc";
                case "Марушкинское": return "#aaffcc";
                case "Московский": return "#aaffcc";
                case "Мосрентген": return "#aaffcc";
                case "Рязановское": return "#aaffcc";
                case "Сосенское": return "#aaffcc";
                case "Филимонковское": return "#aaffcc";
                case "Щербинка": return "#aaffcc";
                //ТАО
                case "Вороновское": return "#ddff55";
                case "Киевский": return "#ddff55";
                case "Клёновское": return "#ddff55";
                case "Краснопахорское": return "#ddff55";
                case "Михайлово-Ярцевское": return "#ddff55";
                case "Новофёдоровское": return "#ddff55";
                case "Первомайское": return "#ddff55";
                case "Роговское": return "#ddff55";
                case "Троицк": return "#ddff55";
                case "Щаповское": return "#ddff55";
                default: return "#00FF00";
            }
        }

        public static string getColorForZone(string name)
        {
            if (name.Contains("Зона 1")) return "#ffb2b6";
            else if (name.Contains("Зона 2")) return "#ffe9b2";
            else if (name.Contains("Зона 3")) return "#feffcc";
            else if (name.Contains("Зона 4")) return "#ccffb2";
            else if (name.Contains("Зона 5")) return "#b2fff1";
            return "#00FF00";
        }

    }

}
