using System;
using System.Text;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

using ObjectModel.Market;

using GemBox.Spreadsheet;
using ObjectModel.Directory;

namespace KadOzenka.Dal.ExcelParser
{

    public class FormRegionTable
    {

        public static void parseExcelRegionsData()
        {
            ExcelFile excelFile = ExcelFile.Load(ConfigurationManager.AppSettings["InitialRegionsFile"]);
            ExcelWorksheet ws = excelFile.Worksheets[0];
            foreach (var row in ws.Rows.Skip(1))
            {
                OMQuartalDictionary note = OMQuartalDictionary.Where(x => x.CadastralQuartal == row.Cells[0].Value.ToString()).SelectAll().Execute().FirstOrDefault();
                if (note == null) note = new OMQuartalDictionary();
                note.CadastralQuartal = row.Cells[0].Value.ToString();
                note.District_Code = getDistrict(row.Cells[2].Value.ToString());
                note.Region_Code = getRegions(row.Cells[3].Value.ToString().TrimEnd());
                note.Zone = long.Parse(row.Cells[4].Value.ToString());
                note.ZoneRegion = row.Cells[5].Value.ToString();
                Console.WriteLine($"{row.Cells[0].Value.ToString()}\t{row.Cells[1].Value.ToString()}\t{row.Cells[2].Value.ToString()}\t\t{row.Cells[3].Value.ToString()}");
                note.Save();
            }
        }

        private static Hunteds getDistrict(string district) {
            switch (district)
            {
                case "Восточный": return Hunteds.VAO;
                case "Западный": return Hunteds.ZAO;
                case "Зеленоградский": return Hunteds.ZelAO;
                case "НАО": return Hunteds.NAO;
                case "Северный": return Hunteds.SAO;
                case "Северо-Восточный": return Hunteds.SVAO;
                case "Северо-Западный": return Hunteds.SZAO;
                case "ТАО": return Hunteds.TAO;
                case "Центральный": return Hunteds.CAO;
                case "Юго-Восточный": return Hunteds.YVAO;
                case "Юго-Западный": return Hunteds.YZAO;
                case "Южный": return Hunteds.YAO;
            }
            return Hunteds.None;
        }

        private static Districts getRegions(string region)
        {
            switch (region)
            {
                case "Академический": return Districts.Akademichesky;
                case "Алексеевский": return Districts.Alexeyevsky;
                case "Алтуфьевский": return Districts.Altufyevsky;
                case "Арбат": return Districts.Arbat;
                case "Аэропорт": return Districts.Aeroport;
                case "Бабушкинский": return Districts.Babushkinsky;
                case "Басманный": return Districts.Basmanny;
                case "Беговой": return Districts.Begovoy;
                case "Бескудниковский": return Districts.Beskudnikovsky;
                case "Бибирево": return Districts.Bibirevo;
                case "Бирюлёво Восточное": return Districts.BiryulyovoVostochnoye;
                case "Бирюлёво Западное": return Districts.BiryulyovoZapadnoye;
                case "Богородское": return Districts.Bogorodskoye;
                case "Братеево": return Districts.Brateyevo;
                case "Бутырский": return Districts.Butyrsky;
                case "Вешняки": return Districts.Veshnyaki;
                case "Внуково": return Districts.Vnukovo;
                case "Внуковское, поселение": return Districts.Vnukovskoye;
                case "Войковский": return Districts.Voykovsky;
                case "Вороновское, поселение": return Districts.Voronovskoye;
                case "Воскресенское, поселение": return Districts.Voskresenskoye;
                case "Восточное Дегунино": return Districts.VostochnoyeDegunino;
                case "Восточное Измайлово": return Districts.VostochnoyeIzmaylovo;
                case "Восточный": return Districts.Vostochny;
                case "Выхино-Жулебино": return Districts.VykhinoZhulebino;
                case "Гагаринский": return Districts.Gagarinsky;
                case "Головинский": return Districts.Golovinsky;
                case "Гольяново": return Districts.Golyanovo;
                case "Городской округ Троицк": return Districts.Troitsky;
                case "Городской округ Щербинка": return Districts.Shcherbinka;
                case "Даниловский": return Districts.Danilovsky;
                case "Десёновское, поселение": return Districts.Desyonovskoye;
                case "Дмитровский": return Districts.Dmitrovsky;
                case "Донской": return Districts.Donskoy;
                case "Дорогомилово": return Districts.Dorogomilovo;
                case "Замоскворечье": return Districts.Zamoskvorechye;
                case "Западное Дегунино": return Districts.ZapadnoyeDegunino;
                case "Зюзино": return Districts.Zyuzino;
                case "Зябликово": return Districts.Zyablikovo;
                case "Ивановское": return Districts.Ivanovskoye;
                case "Измайлово": return Districts.Izmaylovo;
                case "Капотня": return Districts.Kapotnya;
                case "Киевский, поселение": return Districts.Kiyevsky;
                case "Клёновское, поселение": return Districts.Klenovskoye;
                case "Кокошкино, поселение": return Districts.Kokoshkino;
                case "Коньково": return Districts.Konkovo;
                case "Коптево": return Districts.Koptevo;
                case "Косино-Ухтомский": return Districts.KosinoUkhtomsky;
                case "Котловка": return Districts.Kotlovka;
                case "Краснопахорское, поселение": return Districts.Krasnopakhorskoye;
                case "Красносельский": return Districts.Krasnoselsky;
                case "Крылатское": return Districts.Krylatskoye;
                case "Крюково": return Districts.Kryukovo;
                case "Кузьминки": return Districts.Kuzminki;
                case "Кунцево": return Districts.Kuntsevo;
                case "Куркино": return Districts.Kurkino;
                case "Левобережный": return Districts.Levoberezhny;
                case "Лефортово": return Districts.Lefortovo;
                case "Лианозово": return Districts.Lianozovo;
                case "Ломоносовский": return Districts.Lomonosovsky;
                case "Лосиноостровский": return Districts.Losinoostrovsky;
                case "Люблино": return Districts.Lyublino;
                case "Марушкинское, поселение": return Districts.Marushkinskoye;
                case "Марфино": return Districts.Marfino;
                case "Марьина роща": return Districts.MaryinaRoshcha;
                case "Марьино": return Districts.Maryino;
                case "Матушкино": return Districts.Matushkino;
                case "Метрогородок": return Districts.Metrogorodok;
                case "Мещанский": return Districts.Meshchansky;
                case "Митино": return Districts.Mitino;
                case "Михайлово-Ярцевское, поселение": return Districts.MikhaylovoYartsevskoye;
                case "Можайский": return Districts.Mozhaysky;
                case "Молжаниновский": return Districts.Molzhaninovsky;
                case "Москворечье-Сабурово": return Districts.MoskvorechyeSaburovo;
                case "Москворечье-сабурово": return Districts.MoskvorechyeSaburovo;
                case "Московский, поселение": return Districts.Moskovsky;
                case "Мосрентген, поселение": return Districts.Mosrentgen;
                case "Нагатино-Садовники": return Districts.NagatinoSadovniki;
                case "Нагатинский Затон": return Districts.NagatinskyZaton;
                case "Нагорный": return Districts.Nagorny;
                case "Некрасовка": return Districts.Nekrasovka;
                case "Нижегородский": return Districts.Nizhegorodsky;
                case "Новогиреево": return Districts.Novogireyevo;
                case "Новокосино": return Districts.Novokosino;
                case "Ново-Переделкино": return Districts.NovoPeredelkino;
                case "Новофёдоровское, поселение": return Districts.Novofyodorovskoye;
                case "Обручевский": return Districts.Obruchevsky;
                case "Орехово-Борисово Северное": return Districts.OrekhovoBorisovoSevernoye;
                case "Орехово-Борисово Южное": return Districts.OrekhovoBorisovoYuzhnoye;
                case "Останкинский": return Districts.Ostankinsky;
                case "Отрадное": return Districts.Otradnoye;
                case "Очаково-Матвеевское": return Districts.OchakovoMatveyevskoye;
                case "Первомайское, поселение": return Districts.Pervomayskoye;
                case "Перово": return Districts.Perovo;
                case "Печатники": return Districts.Pechatniki;
                case "Покровское-Стрешнево": return Districts.PokrovskoyeStreshnevo;
                case "Преображенское": return Districts.Preobrazhenskoye;
                case "Пресненский": return Districts.Presnensky;
                case "Проспект Вернадского": return Districts.ProspektVernadskogo;
                case "Раменки": return Districts.Ramenki;
                case "Роговское, поселение": return Districts.Rogovskoye;
                case "Ростокино": return Districts.Rostokino;
                case "Рязановское, поселение": return Districts.Ryazanovskoye;
                case "Рязанский": return Districts.Ryazansky;
                case "Савёлки": return Districts.Savyolki;
                case "Савёловский": return Districts.Savyolovsky;
                case "Свиблово": return Districts.Sviblovo;
                case "Северное Бутово": return Districts.SevernoyeButovo;
                case "Северное Измайлово": return Districts.SevernoyeIzmaylovo;
                case "Северное Медведково": return Districts.SevernoyeMedvedkovo;
                case "Северное Тушино": return Districts.SevernoyeTushino;
                case "Северный": return Districts.Severny;
                case "Силино": return Districts.Silino;
                case "Сокол": return Districts.Sokol;
                case "Соколиная Гора": return Districts.SokolinayaGora;
                case "Сокольники": return Districts.Sokolniki;
                case "Солнцево": return Districts.Solntsevo;
                case "Сосенское, поселение": return Districts.Sosenskoye;
                case "СП Кленовское": return Districts.Klenovskoye;
                case "СП Кокошкино": return Districts.Kokoshkino;
                case "СП Марушкинское": return Districts.Marushkinskoye;
                case "СП Первомайское": return Districts.Pervomayskoye;
                case "Старое Крюково": return Districts.StaroyeKryukovo;
                case "Строгино": return Districts.Strogino;
                case "Таганский": return Districts.Tagansky;
                case "Тверской": return Districts.Tverskoy;
                case "Текстильщики": return Districts.Tekstilshchiki;
                case "Тёплый Стан": return Districts.TyoplyStan;
                case "Тимирязевский": return Districts.Timiryazevsky;
                case "Тропарёво-Никулино": return Districts.TroparyovoNikulino;
                case "Филёвский Парк": return Districts.FilyovskyPark;
                case "Фили-Давыдково": return Districts.FiliDavydkovo;
                case "Филимонковское, поселение": return Districts.Filimonkovskoye;
                case "Хамовники": return Districts.Khamovniki;
                case "Ховрино": return Districts.Khovrino;
                case "Хорошёво-Мнёвники": return Districts.KhoroshyovoMnyovniki;
                case "Хорошёвский": return Districts.Khoroshyovsky;
                case "Царицыно": return Districts.Tsaritsyno;
                case "Черёмушки": return Districts.Cheryomushki;
                case "Чертаново Северное": return Districts.ChertanovoSevernoye;
                case "Чертаново Центральное": return Districts.ChertanovoTsentralnoye;
                case "Чертаново Южное": return Districts.ChertanovoYuzhnoye;
                case "Щаповское, поселение": return Districts.Shchapovskoye;
                case "Щукино": return Districts.Shchukino;
                case "Южное Бутово": return Districts.YuzhnoyeButovo;
                case "Южное Медведково": return Districts.YuzhnoyeMedvedkovo;
                case "Южное Тушино": return Districts.YuzhnoyeTushino;
                case "Южнопортовый": return Districts.Yuzhnoportovy;
                case "Якиманка": return Districts.Yakimanka;
                case "Ярославский": return Districts.Yaroslavsky;
                case "Ясенево": return Districts.Yasenevo;
                default: return Districts.None;
            }
        }

    }

}
