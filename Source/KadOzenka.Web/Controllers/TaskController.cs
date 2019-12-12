using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Core.SRD;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
	public class TaskController : Controller
    {
	    [HttpGet]
		public IActionResult TourEstimates()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TourEstimates([FromForm]int id, [FromForm]string year)
        {
	        if (string.IsNullOrEmpty(year))
	        {
		        return Json(new {Error = "Поле не должно быть пустым"});
	        }

	        var tour = OMTour.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();

	        if (tour == null)
	        {
		        tour = new OMTour();
	        }

	        tour.Year = int.Parse(year);

	        int idSave; 
			using (var ts = new TransactionScope())
	        {
		        idSave = tour.Save();
		        ts.Complete();
	        }

			return Json(new {Success = "Сохранение выполненно", Id = idSave });
        }


        [HttpDelete]
        public IActionResult TourEstimates(int id)
        {
	        var tour = OMTour.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();

			
	        if (tour == null)
	        {
		        return Json(new {Error = "Тур с указыным ид не найден"});
	        }

	        using (var ts = new TransactionScope())
	        {
		        tour.Destroy();
		        ts.Complete();
	        }

	        return Json(new { Success = "Удаление выполненно"});
        }

		public JsonResult GetTourEstimations()
        {
            var tours = OMTour.Where(x => x).SelectAll().Execute()
                .Select(x => new
                {
                    x.Id,
                    Text = x.Year
                });

           return Json(tours);
        }
    }
}