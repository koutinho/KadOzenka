using System.Collections.Generic;
using System.Configuration;
using KadOzenka.Dal.Extentions;
using Newtonsoft.Json.Linq;
using ObjectModel.Directory;
using ObjectModel.Market;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KadOzenka.Dal.AvitoParsing.Parsers
{
    public class AvitoParkingParser : AvitoParser
    {

        public AvitoParkingParser()
        {
            Url = "https://www.avito.ru/moskva/garazhi_i_mashinomesta";
            ObjectTypeList = new List<ObjectCategoryCorrelation>
            {
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Гараж",
                    MarketSegment = MarketSegment.Parking,
                    PropertyType = PropertyTypesCIPJS.Buildings
                },
                new ObjectCategoryCorrelation
                {
                    AvitoName = "Машиноместо",
                    MarketSegment = MarketSegment.Parking,
                    PropertyType = PropertyTypesCIPJS.Placements
                }
            };
        }

        protected override void SelectObjectTypeCategory(IWebDriver driver, string objectTypeName)
        {
            if (((ChromeDriver) driver).ExecuteScript(ConfigurationManager.AppSettings["getAvitoObjectTypeSelectItem"]) is IWebElement @select)
            {
                var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(select);
                selectElement.SelectByText(objectTypeName);
            }
        }

        protected override void FillObjectWallMaterialInfo(OMCoreObject marketObject, JObject jObject)
        {
            var val = !jObject.SelectToken("garage_type").IsNullOrEmpty()
                ? jObject.SelectToken("garage_type").Value<string>()
                : null;
            if (!string.IsNullOrWhiteSpace(val))
            {
                WallMaterial? result = null;
                switch (val)
                {
                    case "Кирпичный":
                        result = WallMaterial.Brick;
                        break;
                    case "Железобетонный":
                        result = WallMaterial.Monolit;
                        break;
                    default:
                        result = WallMaterial.Other;
                        break;
                }

                marketObject.WallMaterial_Code = result.Value;
            }
        }
    }
}
