using System;
using System.Configuration;
using KadOzenka.Dal.Extentions;
using Newtonsoft.Json.Linq;
using ObjectModel.Market;
using OpenQA.Selenium.Chrome;

namespace KadOzenka.Dal.YandexParsing
{
	public class YandexParserUtils
	{
		public static void CheckCapcha(ChromeDriver driver)
		{
			var isCapcha = driver.ExecuteScript(ConfigurationManager.AppSettings["isYandexCapchaScreen"]).ToString();
			if (bool.Parse(isCapcha))
			{
				Console.WriteLine($"\nОбнаружена капча! Обработайте запрос со страницы браузера и нажмите любую клавишу для продолжения парсинга...");
				Console.ReadKey();
				Console.WriteLine($"Парсинг возобновлен...");
			}
		}

		public static bool FillAdditionalData(JObject obj, OMCoreObject marketObject)
		{
			var isObjectUpdated = false;
			if (!obj.SelectToken("cards.offers.house.entranceType").IsNullOrEmpty())
			{
				switch (obj.SelectToken("cards.offers.house.entranceType")?.Value<string>())
				{
					case "SEPARATE":
						marketObject.EntranceType = "Отдельный";
						isObjectUpdated = true;
						break;
					case "COMMON":
						marketObject.EntranceType = "Общий";
						isObjectUpdated = true;
						break;
				}
			}

			if (!obj.SelectToken("cards.offers.apartment.quality").IsNullOrEmpty())
			{
				switch (obj.SelectToken("cards.offers.apartment.quality")?.Value<string>())
				{
					case "EXCELLENT":
						marketObject.Quality = "Отличное";
						isObjectUpdated = true;
						break;
					case "GOOD":
						marketObject.Quality = "Хорошее";
						isObjectUpdated = true;
						break;
					case "NORMAL":
						marketObject.Quality = "Нормальное";
						isObjectUpdated = true;
						break;
					case "POOR":
						marketObject.Quality = "Плохое";
						isObjectUpdated = true;
						break;
					case "AWFUL":
						marketObject.Quality = "Ужасное";
						isObjectUpdated = true;
						break;
				}
			}

			if (!obj.SelectToken("cards.offers.utilitiesIncluded").IsNullOrEmpty())
			{
				marketObject.IsUtilitiesIncluded = obj.SelectToken("cards.offers.utilitiesIncluded")?.Value<bool>();
				isObjectUpdated = true;
			}

			if (!obj.SelectToken("cards.offers.taxationForm").IsNullOrEmpty())
			{
				switch (obj.SelectToken("cards.offers.taxationForm")?.Value<string>())
				{
					case "NDS":
						marketObject.Quality = "НДС включен";
						isObjectUpdated = true;
						break;
					case "USN":
						marketObject.Quality = "УСН";
						isObjectUpdated = true;
						break;
				}
			}

			return isObjectUpdated;
		}
	}
}
