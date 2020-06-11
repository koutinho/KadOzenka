using KadOzenka.Web.Models.Task;
using KadOzenka.Web.Models.Unit;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.KO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.Enums;
using KadOzenka.Dal.DataExport;
using Microsoft.AspNetCore.Http;
using Core.Shared.Extensions;

namespace KadOzenka.Web.Controllers
{
	public class UnitController : Controller
	{
		[HttpGet]
		public ActionResult ObjectCard(long unitId)
		{
			OMUnit unit = OMUnit.Where(x => x.Id == unitId)
				.SelectAll()
				.ExecuteFirstOrDefault(); 

			if (unit == null)
			{
				throw new Exception("Не найдена единица оценки с ИД=" + unitId);
			}

			UnitDto dto = UnitDto.ToDto(unit);
			
			return View(dto);
		}

		[HttpGet]
		public ActionResult UnitHistory(long unitId)
		{
			return View(unitId);
		}

		[HttpGet]
		public JsonResult GetUnitHistory(long unitId)
		{
			OMUnit unit = OMUnit.Where(x => x.Id == unitId).Select(x => x.CadastralNumber).ExecuteFirstOrDefault();
			if (unit == null)
			{
				throw new Exception("Единица оценки не найдена");
			}

			List<HistoryUnit> historyUnits = HistoryUnit.GetHistory(unit.CadastralNumber);
			List<UnitHistoryDto> result = historyUnits.Select(x => new UnitHistoryDto
			{
				Id = x.Unit.Id,				
				CadastralNumber = x.Unit.CadastralNumber,
				CreationDate = x.Unit.CreationDate,
				NoteType = x.Task.NoteType,
				InputDoc = x.InputDoc?.RegNumber + " " + x.InputDoc?.CreateDate.ToShortDateString() + " " + x.InputDoc?.Description,
				CadastralCost = x.Unit.CadastralCost,
				Upks = x.Unit.Upks,
				GroupId = x.Unit.GroupId,
				OutputDoc = x.OutputDoc?.RegNumber + " " + x.OutputDoc?.CreateDate.ToShortDateString() + " " + x.OutputDoc?.Description,
				IsActual = x.IsActual,
				IsBad = x.IsBad
			}).ToList();

			List<long?> groupIds = result.Select(x => x.GroupId).Distinct().ToList();
			List<OMGroup> groups = OMGroup.Where(x => groupIds.Contains(x.Id)).Select(x => x.GroupName).Execute();

			result.ForEach(res =>
			{
				res.Group = groups.Find(x => x.Id == res.GroupId)?.GroupName;
			});

			return Json(result);
		}

        [HttpGet]
        public ActionResult FormClarification(long unitId)
        {
            var unit = OMUnit.Where(x => x.Id == unitId).SelectAll().ExecuteFirstOrDefault();
            if (unit == null)
                throw new Exception($"Не найдена единица оценки с Id = '{unitId}'");

            var fileStream = (MemoryStream)DEKODocOtvet.ExportToDoc(unit);

            HttpContext.Session.Set(unitId.ToString(), fileStream.ToArray());

            return Ok();
        }

        [HttpGet]
        public ActionResult DownloadClarification(string unitId)
        {
            var fileContent = HttpContext.Session.Get(unitId);
            if (fileContent == null)
                return new EmptyResult();

            HttpContext.Session.Remove(unitId);
            StringExtensions.GetFileExtension(RegistersExportType.Docx, out var fileExtensiton, out var contentType);

            var fileName = $"Предоставление разъяснений {unitId}.{fileExtensiton}";

            return File(fileContent, contentType, fileName);
        }
    }	
}
