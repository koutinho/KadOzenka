using System;
using System.Configuration;
using Core.Shared.Extensions;
using KadOzenka.Dal.Extentions;
using Newtonsoft.Json.Linq;
using ObjectModel.Directory;
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
					case nameof(YandexEntranceType.SEPARATE):
						marketObject.EntranceType = YandexEntranceType.SEPARATE.GetEnumDescription();
						isObjectUpdated = true;
						break;
					case nameof(YandexEntranceType.COMMON):
						marketObject.EntranceType = YandexEntranceType.COMMON.GetEnumDescription();
						isObjectUpdated = true;
						break;
				}
			}

			if (!obj.SelectToken("cards.offers.apartment.quality").IsNullOrEmpty())
			{
				switch (obj.SelectToken("cards.offers.apartment.quality")?.Value<string>())
				{
					case nameof(YandexQualityType.EXCELLENT):
						marketObject.Quality = YandexQualityType.EXCELLENT.GetEnumDescription();
						isObjectUpdated = true;
						break;
					case nameof(YandexQualityType.GOOD):
						marketObject.Quality = YandexQualityType.GOOD.GetEnumDescription();
						isObjectUpdated = true;
						break;
					case nameof(YandexQualityType.NORMAL):
						marketObject.Quality = YandexQualityType.NORMAL.GetEnumDescription();
						isObjectUpdated = true;
						break;
					case nameof(YandexQualityType.POOR):
						marketObject.Quality = YandexQualityType.POOR.GetEnumDescription();
						isObjectUpdated = true;
						break;
					case nameof(YandexQualityType.AWFUL):
						marketObject.Quality = YandexQualityType.AWFUL.GetEnumDescription();
						isObjectUpdated = true;
						break;
				}
			}

			if (!obj.SelectToken("cards.offers.apartment.renovation").IsNullOrEmpty())
			{
				switch (obj.SelectToken("cards.offers.apartment.renovation")?.Value<string>())
				{
					case nameof(YandexRenovationType.DESIGNER_RENOVATION):
						marketObject.Renovation = YandexRenovationType.DESIGNER_RENOVATION.GetEnumDescription();
						isObjectUpdated = true;
						break;
					case nameof(YandexRenovationType.NEEDS_RENOVATION):
						marketObject.Renovation = YandexRenovationType.NEEDS_RENOVATION.GetEnumDescription();
						isObjectUpdated = true;
						break;
					case nameof(YandexRenovationType.NON_GRANDMOTHER):
						marketObject.Renovation = YandexRenovationType.NON_GRANDMOTHER.GetEnumDescription();
						isObjectUpdated = true;
						break;
					case nameof(YandexRenovationType.COSMETIC_DONE):
						marketObject.Renovation = YandexRenovationType.COSMETIC_DONE.GetEnumDescription();
						isObjectUpdated = true;
						break;
					case nameof(YandexRenovationType.PRIME_RENOVATION):
						marketObject.Renovation = YandexRenovationType.PRIME_RENOVATION.GetEnumDescription();
						isObjectUpdated = true;
						break;
					case nameof(YandexRenovationType.EURO):
						marketObject.Renovation = YandexRenovationType.EURO.GetEnumDescription();
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
					case nameof(VatType.NDS):
						marketObject.Vat_Code = VatType.NDS;
						break;
					case nameof(VatType.USN):
						marketObject.Vat_Code = VatType.USN;
						break;
					default:
						marketObject.Vat_Code = VatType.None;
						break;
				}
				isObjectUpdated = true;
			}
			else
			{
				marketObject.Vat_Code = VatType.None;
				isObjectUpdated = true;
			}

			return isObjectUpdated;
		}
	}
}
