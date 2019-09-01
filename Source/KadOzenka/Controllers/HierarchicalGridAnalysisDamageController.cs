using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIPJS.DAL.HierarchicalGridAnalysisDamage;
using CIPJS.DAL.HierarchicalGridAnalysisDamage.ModelBuilder.SepTable;
using CIPJS.DAL.HierarchicalGridAnalysisDamage.Models;
using Core.SRD;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Insur;

namespace CIPJS.Controllers
{
    public class HierarchicalGridAnalysisDamageController : Controller
    {
        private readonly HierarchicalGridAnalysisDamageService _HiGrService;

        public HierarchicalGridAnalysisDamageController(HierarchicalGridAnalysisDamageService service)
        {
            _HiGrService = service;
        }

        [HttpGet]
        public IActionResult Index(int? AdminIndex)
        {
            var generalPartial = new string[] { "_SepTable", "_SepTable1For2", "_SepTable2For2" };
            var h = new string[][]
            {
                new string[] { "_SepTable" },
                generalPartial,
                generalPartial,
                new string[] { "_Admin" }
            };
            var builder = _HiGrService.InitGroupSelect(h);

            var srdFuncs = GetUserSrdFuncs();
            string[] model = null;
            try
            {
                if (AdminIndex.HasValue)                
                    if (srdFuncs.Contains("Admin"))
                    {
                        model = h[AdminIndex.Value];
                        ViewBag.AdminIndex = AdminIndex;
                    }
                    else
                        throw new Exception();                
                else
                    model = builder.Serch(srdFuncs);
            }
            catch
            {
                throw new Exception($"Недостаточно прав или неправильная комбинация SRD фунций [ {string.Join(',', srdFuncs)} ]");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult GetDataST(int? Index, int? AdminIndex)
        {           
            //SRDSession.Current.CheckAccessToFunction(SRDCoreFunctions.CoreHolidaysChange, true, false, true);
            var srdFuncs = GetUserSrdFuncs();

            if (AdminIndex.HasValue && !srdFuncs.Contains("Admin"))
                throw new Exception($"Недостаточно прав или неправильная комбинация SRD фунций [ {string.Join(',', srdFuncs)} ]");

            var res = _HiGrService.GetDataST(SRDSession.Current.User, srdFuncs, Index.Value, AdminIndex);

            return Json(res);            
        }

        private string[] GetUserSrdFuncs()
        {
            var user = SRDSession.Current.User;
            var srdFuncs = user.AvailableFunctions;
            if (user.IsAdmin)
                srdFuncs.Add("Admin");

            return srdFuncs.ToArray();
        }
    }
}