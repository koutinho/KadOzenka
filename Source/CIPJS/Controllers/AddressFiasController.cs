using CIPJS.Extensions;
using Core.Shared.Extensions;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CIPJS.Controllers
{
    public class AddressFiasController : BaseController
    {
        public ActionResult GetRegions()
        {
            string commandText =
"SELECT FORMALNAME, SHORTNAME FROM FIAS_ADDROBJ WHERE ACTSTATUS=1 AND AOLEVEL=1 AND REGIONCODE='77'";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
            var result = DBMngr.Realty.ExecuteDataSet(command).Tables[0].AsEnumerable()
                .Select(x =>
                {
                    var p = (string)x["SHORTNAME"] == "г" ? "." : "";
                    return new
                    {
                        Name = $" {x["SHORTNAME"]}{p} {x["FORMALNAME"]}",
                        Value = "77"
                    };
                }).ToList();

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult GetCities()
        {
            string commandText =
"SELECT CITYCODE, FORMALNAME, SHORTNAME FROM FIAS_ADDROBJ WHERE ACTSTATUS=1 AND AOLEVEL=4 AND REGIONCODE='77' ORDER BY FORMALNAME";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
            var result = DBMngr.Realty.ExecuteDataSet(command).Tables[0].AsEnumerable()
                .Select(x =>
                {
                    string shortName = string.Empty;

                    switch ((string)x["SHORTNAME"])
                    {
                        case "г":
                        case "тер":
                            shortName = (string)x["SHORTNAME"] + ".";
                            break;
                        default:
                            shortName = (string)x["SHORTNAME"];
                            break;
                    }

                    return new
                    {
                        Name = $"{shortName} {x["FORMALNAME"]}",
                        Value = x["CITYCODE"].ToString()
                    };
                }).ToList();

            result.Insert(0, new { Name = "", Value = "" });

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult GetStreets(string cityCode)
        {
            if (cityCode.IsNullOrEmpty()) cityCode = "000";

            string filter = Request.Query["filter[filters][0][value]"].ToString();
            string commandText =
$"SELECT STREETCODE, FORMALNAME, SHORTNAME FROM FIAS_ADDROBJ WHERE ACTSTATUS=1 AND AOLEVEL=7 AND REGIONCODE='77' AND CITYCODE='{cityCode}' ORDER BY FORMALNAME";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
            var result = DBMngr.Realty.ExecuteDataSet(command).Tables[0].AsEnumerable()
                .Select(x =>
                {
                    string shortName = string.Empty;

                    switch ((string)x["SHORTNAME"])
                    {
                        case "ул":
                        case "пер":
                        case "тер":
                        case "наб":
                        case "ш":
                        case "мкр":
                        case "д":
                        case "туп":
                        case "ал":
                        case "платф":
                        case "пл":
                            shortName = (string)x["SHORTNAME"] + ".";
                            break;
                        case "проезд":
                            shortName = "пр-д";
                            break;
                        case "аллея":
                            shortName = "ал.";
                            break;
                        default:
                            shortName = (string)x["SHORTNAME"];
                            break;
                    }

                    return new
                    {
                        Name = $"{x["FORMALNAME"]} {shortName}",
                        Value = x["STREETCODE"].ToString()
                    };
                }
            ).DistinctBy(x => x.Name).ToList();
            result.Insert(0, new { Name = "", Value = "" });

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        /// <summary>
        /// Получаем "Населенный пункт" из FIAS.
        /// </summary>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public ActionResult GetLocality(string cityCode)
        {
            if (cityCode.IsNullOrEmpty()) cityCode = "000";

            string filter = Request.Query["filter[filters][0][value]"].ToString();
            string commandText =
$"SELECT STREETCODE, FORMALNAME, SHORTNAME FROM FIAS_ADDROBJ WHERE ACTSTATUS=1 AND AOLEVEL=6 AND REGIONCODE='77' AND CITYCODE='{cityCode}' ORDER BY FORMALNAME";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);
            var result = DBMngr.Realty.ExecuteDataSet(command).Tables[0].AsEnumerable()
                .Select(x => new
                {
                    Name = $"{x["FORMALNAME"]}",
                    Value = x["STREETCODE"].ToString()
                }
            ).DistinctBy(x => x.Name).ToList();
            result.Insert(0, new { Name = "", Value = "" });

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        /// <summary>
        /// Получаем типы домов.
        /// </summary>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public ActionResult GetTypeHouse()
        {
            
            List<object> result = new List<object>() {
                new { Name = "Дом", Value = "д." },
                new { Name = "Владение", Value = "влд." },
                new { Name = "Домовладение", Value = "двлд." },
            };

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }


    }
}