using CIPJS.DAL.Building;
using CIPJS.DAL.Dictionaries;
using CIPJS.Models.Building;
using Core.Register.DAL;
using Core.Register.Enums;
using Core.Shared.Extensions;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using ObjectModel.Bti;
using ObjectModel.Core.Register;
using ObjectModel.Ehd;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using Kendo.Mvc.UI;
using Platform.Web.Models;
using Core.Shared.Misc;

namespace CIPJS.Controllers
{
    public class BuildingController : BaseController
    {
        public BuildingService _buildingService { get; set; }

        public BuildingController()
        {
            _buildingService = new BuildingService();
        }

        public ActionResult GetById(long? id)
        {
            var omBuilding = _buildingService.GetById(id);
            var building = BuildingDetails.OMMap(omBuilding);

            return Content(JsonConvert.SerializeObject(building), "application/json");
        }

        public ActionResult GetBuildingWithInsurance(long? id)
        {
            OMBuilding building = _buildingService.GetById(id);
            var buildingModel = BuildingDetails.OMMap(building);

            OMInsuranceOrganization insurance = new OMInsuranceOrganization();
            decimal? btiBuildingOverallArea = _buildingService.GetEpl(building.LinkBtiFsks);
            decimal? oplBti = OMBtiBuilding
                .Where(x => x.LinkBuildBti[0].IdInsurBuild == building.EmpId)
                .Select(x => x.Opl)
                .ExecuteFirstOrDefault()?.Opl;
            decimal? oplEgrn = building.LinkEgrnBild.HasValue ?
                OMBuildParcel.Where(x => x.EmpId == building.LinkEgrnBild.Value)
                .Select(x => x.Area)
                .ExecuteFirstOrDefault()?.Area :
                null;

            if (building.OkrugId.HasValue)
            {
                insurance = DictionaryService.GetIncuranceCompanyByOkrug(building.OkrugId.Value);
            }

            return Content(JsonConvert.SerializeObject(new
            {
                building = buildingModel,
                insurance = new
                {
                    Id = insurance != null ? insurance.Id.ToString() : string.Empty,
                    Name = insurance != null ? insurance.FullName : string.Empty
                },
                btiBuildingOverallArea,
                oplBti,
                oplEgrn
            }), "application/json");
        }

        public ActionResult Select(long? id)
        {
            return View();
        }

        public ActionResult GetByName(string name)
        {
            string commandText = $"select emp_id as \"Id\", unom as \"Unom\" from insur_building_q where actual=1 and CAST(unom AS TEXT) like '{name}%' order by unom";
            DbCommand dbCommand = DBMngr.Realty.GetSqlStringCommand(commandText);
            DataTable dataTable = DBMngr.Realty.ExecuteDataSet(dbCommand).Tables[0];
            return Content(JsonConvert.SerializeObject(dataTable), "application/json");
        }

        /// <summary>
        /// Получение списка МКД у которых изменялся признака страхования за последний день
        /// </summary>
        /// <returns></returns>
        public ContentResult GetMkdFlagInsurCalculatedEveryDay(string fromDate, string toDate)
        {
            DateTime fromDateTime;
            DateTime toDateTime;
            if (!(DateTime.TryParse(fromDate, out fromDateTime) && DateTime.TryParse(toDate, out toDateTime)))
                return ErrorResponse("Ошибка приобразования"); 

            string commandText = $"select distinct ib.emp_id, ib.unom, ib.cadastr_num, a.short_address, ib.flag_insur_calculated as \"new_flag_insur\", ibp.flag_insur_calculated as \"old_flag_insur\", ib.changes_date, u.username  " +
                $"from insur_building_q ib " +
                $"join insur_address a on ib.address_id = a.emp_id " +
                $"join core_srd_user u on ib.changes_user_id = u.id " +
                $"join insur_building_q ibp " +
                $"on ibp.emp_id = ib.emp_id and ib.actual = 1 and date_trunc('day', ibp.po_) = date_trunc('day', ib.s_) " +
                $"and ib.changes_date between({CrossDBSQL.ToDate(fromDateTime)}) " +
                $"and ({CrossDBSQL.ToDate(toDateTime)} + interval '1 day')  " +
                $"and ib.flag_insur_calculated != ibp.flag_insur_calculated";
            DbCommand dbCommand = DBMngr.Realty.GetSqlStringCommand(commandText);
            DataTable dataTable = DBMngr.Realty.ExecuteDataSet(dbCommand).Tables[0];
            return Content(JsonConvert.SerializeObject(dataTable), "application/json");
        }

        [HttpGet]
        public FileContentResult BtiPremiseExportToExcel(long? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("Не передан обязательный параметр - идентификатор объекта");
            }

            OMBuilding building = _buildingService.GetById(id);

            if (building == null)
            {
                throw new Exception("Не удалось определить объект для экспорта");
            }

            StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtensiton, out string contentType);

            OMLayout layout = LayoutEditorDAL.GetLayoutWithDetails(1002541);

            DataTable premiseData;

            if (building.LinkBtiFsks.HasValue)
            {
                //OMPremase.Where(x => x.ParentFloor.BuildingId == building.LinkBtiFsks.Value).SelectAll(false).ExecuteDt();
                DbCommand command = DBMngr.Realty.GetSqlStringCommand(
$@"select bp.* from bti_premase bp
join bti_floor_q bfq on bfq.emp_id = bp.floor_id and bfq.actual = 1
where bfq.building_id = {building.LinkBtiFsks.Value} order by bp.emp_id");
                premiseData = DBMngr.Realty.ExecuteDataSet(command).Tables[0];
            }
            else
            {
                premiseData = new DataTable();
            }

            MemoryStream stream = RegistersExport.CreateExportStream(premiseData, layout, RegistersExportType.Xlsx);
            Response.Headers.Add("content-disposition", $"attachment; filename={id.Value}_{DateTime.Now:dd.MM.yyyy_HHmmss}.{fileExtensiton}");

            return new FileContentResult(stream.ToArray(), contentType);
        }

        /// <summary>
        /// Получаем все актуальные начисления квартир в доме.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<long> GetBuildInputNachs(long? id)
        {
            var response = _buildingService.GetBuildingActualInputNachs(id);
            return response;
        }

        [HttpGet]
        public ActionResult FindUnoms()
        {
            var searchString = Request.Query["filter[filters][0][value]"].FirstOrDefault();
            if (searchString.IsNullOrEmpty())
                return new EmptyResult();
            DbCommand dbCommand = DBMngr.Realty.GetSqlStringCommand(
$@"select b.emp_id, b.unom, a.short_address
from insur_building_q b
join insur_address a on a.emp_id=b.address_id
where b.actual=1
    and CAST(b.unom AS TEXT) like '{searchString}%'
order by b.unom
limit 20");
            DataTable dataTable = DBMngr.Realty.ExecuteDataSet(dbCommand).Tables[0];
            return Content(JsonConvert.SerializeObject(dataTable), "application/json");
        }
    }
}