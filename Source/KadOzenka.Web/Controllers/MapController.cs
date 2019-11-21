using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Market;
using Newtonsoft.Json;

namespace KadOzenka.Web.Controllers
{
    public class MapController : BaseController
    {


        // GET: Map
        public ActionResult Index() 
        { 
            return View(); 
        }

        public JsonResult Objects()
        {
            List<object> point = new List<object>();
            var analogItem = OMCoreObject
                .Where(x => x.Category == "Коммерческая недвижимость" && 
                            x.ProcessType_Code == ObjectModel.Directory.ProcessStep.InProcess && 
                            x.Market_Code != ObjectModel.Directory.MarketTypes.Rosreestr && 
                            x.DealType_Code == ObjectModel.Directory.DealType.SaleSuggestion)
                .Select(x => new { x.Lat, x.Lng, x.Category, x.Subcategory, x.PropertyType_Code })
                .Execute().Take(2000).ToList();
            Console.WriteLine(analogItem.Count);
            analogItem.ForEach(x => point.Add(new { points = new[] { x.Lat, x.Lng }, type = FormType(x.Category, x.Subcategory, x.PropertyType_Code), id = x.Id }));
            return Json(point);
        }

        public JsonResult RequiredInfo()
        {
            List<long> ids = JsonConvert.DeserializeObject<List<long>>(new StreamReader(HttpContext.Request.Body).ReadToEnd());
            List<object> allData = new List<object>();
            OMCoreObject.Where(x => ids.Contains(x.Id)).SelectAll().Execute().Take(20).ToList().ForEach(x => {
                allData.Add(new { 
                    type = x.Subcategory, 
                    price = x.Price, 
                    area = x.Area, 
                    link = x.Url, 
                    metro = x.Metro, 
                    address = x.Address,
                    images = x.Images,
                    id = x.Id, 
                    parserTime = x.ParserTime?.ToString("dd.MM.yyyy"), 
                    lastUpdateDate = x.LastDateUpdate?.ToString("dd.MM.yyyy")
                });
            });
            return Json(allData);
        }

        private int FormType(string category, string subCategory, ObjectModel.Directory.PropertyTypes propertyType)
        {
            if (propertyType == ObjectModel.Directory.PropertyTypes.UncompletedBuilding) return 7;
            else if(category == "Коммерческая недвижимость")
            {
                switch (subCategory)
                {
                    case "Складская": return 0;
                    case "Гараж": return 1;
                    case "Торговая": return 2;
                    case "Свободного назначения": return 3;
                    case "Офисная": return 4;
                    case "Готовый бизнес": return 5;
                    case "Производственная": return 6;
                    case "Здание": return 8;
                }
            }
            return 9;
        }

    }
}