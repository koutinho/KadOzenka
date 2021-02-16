using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Collections.Specialized;

namespace Parser
{

    class ParseData
    {

        private long PagesCount { get; set; }
        private long ObjectsCount { get; set; }
        private string AllData { get; set; }
        //private AgregatorsData AgregatorsData { get; set; }
        //private AgregatorsDataExtra AgregatorsDataExtra { get; set; }
        private JObject CheckCapchaData { get; set; }
        private JObject CheckEmptyData { get; set; }
        private JObject GetCount { get; set; }
        private HashCombined HashCombined { get; set; }

        public ParseData(HashCombined hashCombined)
        {
            HashCombined = hashCombined;
            if (HashCombined.AgregatorDataExtra.Regex_script_check_capcha != null) CheckCapchaData = JObject.Parse(HashCombined.AgregatorDataExtra.Regex_script_check_capcha);
            CheckEmptyData = JObject.Parse(HashCombined.AgregatorDataExtra.Regex_script_check_for_empty_data);
            GetCount = JObject.Parse(HashCombined.AgregatorDataExtra.Regex_script_get_count);
        }

        public void Parse()
        {
            Console.WriteLine($"URL адрес: {HashCombined.Url}");
            AllData = ReadAllData(HashCombined.Url, HashCombined.AgregatorDataExtra.User_agent, HashCombined.AgregatorDataExtra.Cookies);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(AllData);
            if (HashCombined.AgregatorDataExtra.Regex_script_check_capcha != null && CheckCapcha(htmlDoc))
            {
                if (HashCombined.AgregatorDataExtra.Agregator_id == 1)
                {
                    Console.WriteLine("Результат парсинга: Капча");
                    SolveCapcha(HashCombined.Url, HashCombined.AgregatorDataExtra, Parse);
                }
                else
                {
                    Console.WriteLine(AllData);
                }
            }
            else if (CheckForEmpty(htmlDoc)) Console.WriteLine("Результат парсинга: По заданному запросу нет объявления");
            else
            {
                long objectsCount = GetObjectsCount(htmlDoc);
                long pagesCount = (long)((objectsCount / HashCombined.AgregatorDataExtra.On_page_counter) < HashCombined.AgregatorDataExtra.Maximum_page_counter ? 
                    objectsCount % HashCombined.AgregatorDataExtra.On_page_counter > 0 ?
                    objectsCount / HashCombined.AgregatorDataExtra.On_page_counter + 1 :
                    objectsCount / HashCombined.AgregatorDataExtra.On_page_counter :
                    HashCombined.AgregatorDataExtra.Maximum_page_counter);

                Console.WriteLine($"Результат парсинга: {GetObjectsCount(htmlDoc)}. Всего объектов {objectsCount}, всего страниц {pagesCount} при {HashCombined.AgregatorDataExtra.On_page_counter} объектах на странице.");
                
                ParseStringData(HashCombined.Url, AllData);

                //if (pagesCount > 1)
                //    for (int i = 2; i < pagesCount + 1; i++)
                //        ParseStringData($"{HashCombined.Url.Substring(0, HashCombined.Url.Length - 2)}{i}", ReadAllData($"{HashCombined.Url.Substring(0, HashCombined.Url.Length - 2)}{i}", HashCombined.AgregatorDataExtra.User_agent, HashCombined.AgregatorDataExtra.Cookies));
            }
        }

        private void ParseStringData(string url, string Alldata)
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(AllData);

            //Компоненты на простыне
            HtmlNodeCollection collection = htmlDoc.DocumentNode
                .SelectSingleNode(appSettings[$"{HashCombined.AgregatorData.Name}_{HashCombined.PropertyTypeForScripts}_all_outer_element"])
                .SelectNodes(appSettings[$"{HashCombined.AgregatorData.Name}_{HashCombined.PropertyTypeForScripts}_all_inner_element"]);

            Console.WriteLine($"{url}: {Alldata.Length} {collection.Count}\n{string.Join(", ", collection.ToList().Select(x => x.InnerLength))}\n");

            foreach (HtmlNode element in collection)
            {
                /*Ссылка*/
                string link = GetDataAttribute(element, appSettings, "link_outer_element", "link_inner_element", "href");
                /*Ссылка на изображение*/
                string images = GetDataAttribute(element, appSettings, "image_galery_element", "images_element", "src");
                /*Текст заголовка*/
                string title = GetData(element, appSettings, "title_element");
                /*Текст подзаголовка*/
                string subTitle = GetData(element, appSettings, "subtitle_element");
                /*Название комплекса*/
                string residentialComplex = GetData(element, appSettings, "residential_complex_outer_element", "residential_complex_inner_element");
                /*Ссылка на комплекс*/
                string residentialComplexLink = GetDataAttribute(element, appSettings, "residential_complex_link_outer_element", "residential_complex_link_inner_element", "href");
                /*Станция метро*/
                string metroStations = GetData(element, appSettings, "metro_stations_outer_element", "metro_stations_inner_element");
                /*Расстояние до станции метро*/
                string metroStationSpace = GetData(element, appSettings, "metro_station_space_outer_element", "metro_station_space_inner_element");
                /*Неформализованный адрес*/
                string notFormalizedAddress = GetData(element, appSettings, "address_element");
                /*Текстовая цены*/
                string textPrice = GetData(element, appSettings, "text_price");
                /*Удельная текстовая цена*/
                string textPricePerArea = GetData(element, appSettings, "text_price_per_area");
                /*Описание*/
                string description = GetData(element, appSettings, "description");
                Console.WriteLine(
                    $"Ссылка: {link}\nИзображение: {images}\nЗаголовок: {title}\nПодзаголовок: {subTitle}\n" +
                    $"Жилой комплекс: {residentialComplex}\nСсылка на жилой комплекс: {residentialComplexLink}\n" +
                    $"Станции метро: {metroStations}\nРастояние до станции метро: {metroStationSpace}\n" +
                    $"Неформализованный адрес: {notFormalizedAddress}\nТекстовая цена: {textPrice}\n" +
                    $"Удельная текстовая цена: {textPricePerArea}\nОписание: {description}\n" + 
                    $"Длина описания: {description.Length}\n");
            }
        }

        private string GetData(HtmlNode node, NameValueCollection appSettings, string elementName)
        {
            HtmlNode resultNode = node.SelectSingleNode(appSettings[$"{HashCombined.AgregatorData.Name}_{HashCombined.PropertyTypeForScripts}_{elementName}"]);
            return resultNode == null ? "-" : resultNode.InnerText;
        }

        private string GetData(HtmlNode node, NameValueCollection appSettings, string elementOuterName, string elementInnerName)
        {
            HtmlNode resultOuterNode = node.SelectSingleNode(appSettings[$"{HashCombined.AgregatorData.Name}_{HashCombined.PropertyTypeForScripts}_{elementOuterName}"]), resultInnerNode = null;
            if (resultOuterNode != null) resultInnerNode = resultOuterNode.SelectSingleNode(appSettings[$"{HashCombined.AgregatorData.Name}_{HashCombined.PropertyTypeForScripts}_{elementInnerName}"]);
            return resultInnerNode == null ? "-" : resultInnerNode.InnerText;
        }

        private string GetDataAttribute(HtmlNode node, NameValueCollection appSettings, string elementName, string attributeName)
        {
            HtmlNode resultNode = node.SelectSingleNode(appSettings[$"{HashCombined.AgregatorData.Name}_{HashCombined.PropertyTypeForScripts}_{elementName}"]);
            HtmlAttribute attribute = null;
            if (resultNode != null) attribute = resultNode.Attributes[attributeName];
            return attribute == null ? "-" : attribute.Value;
        }

        private string GetDataAttribute(HtmlNode node, NameValueCollection appSettings, string elementOuterName, string elementInnerName, string attributeName)
        {
            HtmlNode resultOuterNode = node.SelectSingleNode(appSettings[$"{HashCombined.AgregatorData.Name}_{HashCombined.PropertyTypeForScripts}_{elementOuterName}"]), resultInnerNode = null;
            HtmlAttribute attribute = null;
            if (resultOuterNode != null) resultInnerNode = resultOuterNode.SelectSingleNode(appSettings[$"{HashCombined.AgregatorData.Name}_{HashCombined.PropertyTypeForScripts}_{elementInnerName}"]);
            if (resultInnerNode != null) attribute = resultInnerNode.Attributes[attributeName];
            return attribute == null ? "-" : attribute.Value;
        }

        public void SolveCapcha(string url, AgregatorsDataExtra agregatorsDataExtra, Action parserFunction)
        {
            string cookies = new CapchaChecker(url, Program._hash.CapchaSettingsHash.First(x => x.Agregator_id == agregatorsDataExtra.Agregator_id)).GetCapcha();
            new DBRefresher().SetCookies(cookies, agregatorsDataExtra);
            parserFunction();
        }

        private bool CheckCapcha(HtmlDocument htmlDoc) => 
            htmlDoc.DocumentNode
                .Descendants(CheckCapchaData.GetValue("tag_name").ToString())
                .Any(x => x.GetAttributeValue(CheckCapchaData.GetValue("attribute_name").ToString(), "") == CheckCapchaData.GetValue("attribute_value").ToString());

        private bool CheckForEmpty(HtmlDocument htmlDoc)
        {
            if (htmlDoc.DocumentNode
                    .SelectNodes($"//{CheckEmptyData.GetValue("outer_element_tag_name")}[contains(@{CheckEmptyData.GetValue("outer_element_attribute_name")}, '{CheckEmptyData.GetValue("outer_element_attribute_value")}')]") != null &&
                htmlDoc.DocumentNode
                    .SelectSingleNode($"//{CheckEmptyData.GetValue("outer_element_tag_name")}[contains(@{CheckEmptyData.GetValue("outer_element_attribute_name")}, '{CheckEmptyData.GetValue("outer_element_attribute_value")}')]")
                    .SelectSingleNode($".//{CheckEmptyData.GetValue("inner_element_tag_name")}[contains(@{CheckEmptyData.GetValue("inner_element_attribute_name")}, '{CheckEmptyData.GetValue("inner_element_attribute_value")}')]") != null)
            {
                return htmlDoc.DocumentNode
                    .SelectSingleNode($"//{CheckEmptyData.GetValue("outer_element_tag_name")}[contains(@{CheckEmptyData.GetValue("outer_element_attribute_name")}, '{CheckEmptyData.GetValue("outer_element_attribute_value")}')]")
                    .SelectSingleNode($".//{CheckEmptyData.GetValue("inner_element_tag_name")}[contains(@{CheckEmptyData.GetValue("inner_element_attribute_name")}, '{CheckEmptyData.GetValue("inner_element_attribute_value")}')]").InnerHtml != string.Empty;
            }
            else return false;
        }

        private long GetObjectsCount(HtmlDocument htmlDoc)
        {
            if (htmlDoc.DocumentNode.SelectNodes($"//{GetCount.GetValue("outer_element_tag_name")}[contains(@{GetCount.GetValue("outer_element_attribute_name")}, '{GetCount.GetValue("outer_element_attribute_value")}')]") != null)
            {
                return 
                    Int64.Parse(
                        string.Concat(
                            htmlDoc.DocumentNode
                                .SelectSingleNode($"//{GetCount.GetValue("outer_element_tag_name")}[contains(@{GetCount.GetValue("outer_element_attribute_name")}, '{GetCount.GetValue("outer_element_attribute_value")}')]")
                                .SelectSingleNode($".//{GetCount.GetValue("inner_element_tag_name")}[contains(@{GetCount.GetValue("inner_element_attribute_name")}, '{GetCount.GetValue("inner_element_attribute_value")}')]")
                                .InnerHtml
                                .Where(char.IsDigit)
                        )
                    );
            }
            else return 0;
        }

        private string ReadAllData(string url, string userAgent = null, string cookies = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Proxy = null;
            if (userAgent != null) request.UserAgent = userAgent;
            if (cookies != null) request.Headers.Add("cookie", cookies);

            Console.WriteLine($"Куки: {(cookies != null ? $"{cookies.Substring(0, 100)}..." : "-")}");

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using Stream stream = response.GetResponseStream();
                    using StreamReader reader = new StreamReader(stream);
                        return reader.ReadToEnd();
        }

    }

}
