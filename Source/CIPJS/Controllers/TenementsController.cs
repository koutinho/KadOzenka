using CIPJS.DAL.Balance;
using CIPJS.DAL.Building;
using CIPJS.DAL.DamageAnalysis;
using CIPJS.DAL.Flat;
using CIPJS.DAL.Fsp;
using CIPJS.DAL.InsuranceNoPay;
using CIPJS.DAL.InsurancePayTo;
using CIPJS.DAL.User;
using CIPJS.Models.Building;
using CIPJS.Models.Fsp;
using CIPJS.Models.Tenements;
using Core.Register;
using Core.Register.DAL;
using Core.Register.LongProcessManagment;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using ObjectModel.Bti;
using ObjectModel.Core.Register;
using ObjectModel.Core.SRD;
using ObjectModel.Directory;
using ObjectModel.Ehd;
using ObjectModel.Ehd.Elements;
using ObjectModel.Ehd.Exploitation;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using OMRegister = ObjectModel.Ehd.OMRegister;

namespace CIPJS.Controllers
{
    public class TenementsController : BaseController
    {
        private readonly FspService _fspService;
        private readonly BuildingService _buildingService;
        private readonly FlatService _flatService;
        private readonly DamageAnalysisService _damageAnalysisService;
        private readonly InsurancePayService _insurancePayService;
        private readonly InsuranceNoPayService _insuranceNoPayService;
        private readonly BalanceService _balanceService;

        public TenementsController(FspService fspService, BuildingService buildingService, FlatService flatService,
                                   InsurancePayService insurancePayService, DamageAnalysisService damageAnalysisService,
                                   InsuranceNoPayService insuranceNoPayService, BalanceService balanceService)
        {
            _fspService = fspService;
            _buildingService = buildingService;
            _flatService = flatService;
            _insurancePayService = insurancePayService;
            _damageAnalysisService = damageAnalysisService;
            _insuranceNoPayService = insuranceNoPayService;
            _balanceService = balanceService;
        }

        public IActionResult InsuranceObject(long id)
        {
            OMBuilding omBuilding = _buildingService.GetByIdWithAddress(id);

            if (omBuilding == null) throw new Exception($"Объект страхования МКД не найден (ИД={id})");

            ViewBag.BtiCount = Math.Max(OMLinkBuildBti.Where(x => x.IdInsurBuild == id).Execute().Count, 1);

            bool flagInsur = BuildingService.CalculateFlagInsur(omBuilding.EmpId, out string message);
            if (!flagInsur)
                ViewBag.CalculatedFlagMessage = $" ({message})";
            else
                ViewBag.CalculatedFlagMessage = string.Empty;
            var response = InsuranceObjectsDto.Map(omBuilding);
            return View(response);
        }

        [HttpPost]
        public IActionResult InsuranceObject(InsuranceObjectsDto model)
        {
            _buildingService.Update(InsuranceObjectsDto.Map(model));

            return JsonResponse(1);
        }

        /// <summary>
        /// Подсчет жилищных помещений в здании.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetMkdFlatsCount(long? id)
        {
            if (id == null)
            {
                return 0;
            }
            var flats = OMFlat.Where(x => x.LinkObjectMkd == id && x.SpecialActual == 1).Execute();

            return flats.Count;
        }

        public IActionResult Bti(long id)
        {
            OMLinkBuildBti omLinkBuildBti = OMLinkBuildBti.Where(x => x.IdInsurBuild == id && x.FlagDublUnom != true)
                .Select(x => x.IdBtiFsks).Execute().FirstOrDefault();

            if (omLinkBuildBti == null) return Content("Данные БТИ для объекта страхования не найдены");

            return RedirectToAction("Index", "LinkedRegisterObjects",
                new { objectId = omLinkBuildBti.IdBtiFsks, registerId = ObjectModel.Bti.OMBtiBuilding.GetRegisterId() });
        }

        public IActionResult Egrn(long id)
        {
            OMBuilding omBuilding = OMBuilding.Where(x => x.EmpId == id).Select(x => x.LinkEgrnBild).Execute().FirstOrDefault();

            if (omBuilding == null) throw new Exception($"Объект страхования МКД не найден (ИД={id})");

            if (!omBuilding.LinkEgrnBild.HasValue) return Content("Данные ЕГРН для объекта страхования не найдены");

            return RedirectToAction("Index", "LinkedRegisterObjects",
                new { objectId = omBuilding.LinkEgrnBild, registerId = ObjectModel.Ehd.OMBuildParcel.GetRegisterId() });
        }

        public IActionResult Contract(long id)
        {
            //INSUR_ALL_PROPERTY.LINK_BUILD = INSUR_BUILD.EMP_ID
            OMAllProperty omAllProperty = OMAllProperty.Where(x => x.EmpId == id).Execute().FirstOrDefault();

            if (omAllProperty == null) return Content("Договор страхования общего имущества для объекта страхования не найден");

            return RedirectToAction("Index", "LinkedRegisterObjects",
                new { objectId = omAllProperty.EmpId, registerId = OMAllProperty.GetRegisterId() });
        }

        public IActionResult LivingSpace(long id)
        {
            OMFlat omFlat = _flatService.Get(id);

            if (omFlat == null) throw new Exception($"Объект страхования - помещение не найден (ИД={id})");

            omFlat.ParentBuilding = _buildingService.GetByIdWithAddress(omFlat.LinkObjectMkd, addFspData: false);

            LivingSpaceDto model = LivingSpaceDto.Map(omFlat);
            bool fspFlagManyObj;
            model.Fsps = _fspService.GetByFlatId(id, out fspFlagManyObj, true, true, true).Select(FspDetails.OMMap).ToList();
            model.FspFlagManyObj = fspFlagManyObj;

            return View(model);
        }

        [HttpPost]
        public IActionResult LivingSpace(LivingSpaceDto model)
        {
            _flatService.Update(LivingSpaceDto.Map(model));

            return JsonResponse(1);
        }

        public IActionResult PartialBuildingConsolidatedData(long id)
        {
            int btiCount = Math.Max(OMLinkBuildBti.Where(x => x.IdInsurBuild == id).Execute().Count, 1);

            ViewBag.BtiCount = btiCount;

            return PartialView("BuildingConsolidatedData", id);
        }

        public IActionResult BuildingConsolidatedData(long id)
        {
            int btiCount = Math.Max(OMLinkBuildBti.Where(x => x.IdInsurBuild == id).Execute().Count, 1);

            ViewBag.BtiCount = btiCount;

            return View(id);
        }

        public IActionResult BuildingConsolidatedData_Read(long id)
        {
            List<BuildingConsolidatedDataDto> models = new List<BuildingConsolidatedDataDto>(39);
            DataTable building = _buildingService.GetAllDataById(id);

            if (building.Rows.Count == 0) throw new Exception($"Объект страхования МКД не найден (ИД={id})");

            Dictionary<string, string> sources = GetAttributeSources(building, "SOURCE_ATRIB");
            if (sources.IsEmpty())
                sources = new Dictionary<string, string>();

            if (!sources.ContainsKey("FULL_ADDRESS"))
                sources = sources.Concat(GetAddressSources(building)).ToDictionary(x => x.Key, x => x.Value);

            long egrnId = building.Rows[0]["LINK_EGRN_BILD"].ParseToLong();
            DataTable egrn = egrnId > 0 ? _buildingService.GetEhdAllDataById(egrnId) : new DataTable();
            List<long> btiIds = OMLinkBuildBti
                .Where(x => x.IdInsurBuild == id && x.IdBtiFsks != null)
                .Select(x => x.IdBtiFsks)
                .OrderBy(x => x.FlagDublUnom)
                .Execute()
                .Select(x => x.IdBtiFsks.Value)
                .ToList();
            List<DataTable> btis = new List<DataTable>(btiIds.Count);

            foreach (long btiId in btiIds)
            {
                btis.Add(_buildingService.GetBtiAllDataById(btiId));
            }

            List<OMInputNach> mfcSources = GetOMInputNachesByBuilding(id);
            OMInputNach mfcSource = mfcSources.FirstOrDefault();

            List<OMBuildingSvodDataCalculated> svod = OMBuildingSvodDataCalculated
                .Where(x => x.LinkMkd == id)
                .SelectAll()
                .Execute();
            long mfcFlatCount = 0;
            long? kolGpEgrn = null;
            long? kolGpEgrnId = null;
            long? kolGpBti = null;
            if (svod.Count > 0)
            {
                mfcFlatCount = svod.First().KolGpMfc;
                kolGpEgrn = svod.First().KolGpEgrn;
                kolGpBti = svod.First().KolGpBti;
                kolGpEgrnId = svod.First().Id;
                foreach (var btiTable in btis)
                {
                    long btiId = btiTable.Rows[0]["EMP_ID"].ParseToLong();
                    long? kvNom = svod.FirstOrDefault(x => x.LinkBti == btiId)?.KolGpBti;
                    if (kvNom.HasValue)
                    {
                        btiTable.Rows[0]["KWQ"] = kvNom;
                    }
                }
            }

            AddRowBuildingConstantData constData = new AddRowBuildingConstantData
            {
                Models = models,
                Sources = sources,
                BuildingTable = building,
                EgrnTable = egrn,
                BtiTables = btis,
                MfcSources = mfcSources
            };

            constData.Group = "1. Основные данные";
            MakeFirstBuildingGroup(constData);

            constData.Group = "2. Параметры МКД, определяющие признак Подлежит страхованию";
            MakeSecondBuildingGroup(constData, mfcFlatCount, kolGpEgrnId, kolGpEgrn, kolGpBti);

            constData.Group = "3. Адрес объекта";
            MakeThirdBuildingGroup(constData);

            constData.Group = "4. Технические характеристики";
            MakeFourthBuildingGroup(constData);

            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        public IActionResult GetHistoryAttributeDate(int reestrId, int objectId, int attributeId)
        {
            List<HistoryModelDto> models = new List<HistoryModelDto>();
            DataTable dt = RegisterStorage.GetAttribute(objectId, reestrId, attributeId);
            RegisterAttribute attribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(p => p.Id == attributeId && p.RegisterId == reestrId);
            foreach (DataRow row in dt.Rows)
            {
                string value = row["TEXT_VALUE"].ToString();
                switch (attribute.Type)
                {
                    case RegisterAttributeType.BOOLEAN:
                        value = row["NUMBER_VALUE"].ParseToBoolean().ToString();
                        break;
                    case RegisterAttributeType.DECIMAL:
                    case RegisterAttributeType.INTEGER:
                        value = row["NUMBER_VALUE"].ToString();
                        break;
                    case RegisterAttributeType.DATE:
                        value = row["DATE_VALUE"].ToString();
                        break;
                }
                DateTime dateTimeSValue = row["S"].ParseToDateTime();
                DateTime dateTimePoValue = row["PO"].ParseToDateTime();
                int userId = row["CHANGE_USER_ID"].ParseToInt();
                string username = (userId > 0) ? OMUser.Where(x => x.Id == userId).Select(x => x.Username).Execute().FirstOrDefault()?.Username : string.Empty;

                models.Add(new HistoryModelDto
                {
                    Value = value,
                    From = (dateTimeSValue == DateTime.MaxValue || dateTimeSValue == DateTime.MinValue) ? (DateTime?)DateTime.Now : dateTimeSValue,
                    To = (dateTimePoValue == DateTime.MaxValue || dateTimePoValue == DateTime.MinValue) ? (DateTime?)null : dateTimePoValue,
                    UserName = string.IsNullOrWhiteSpace(username) ? "Система" : username
                });
            }

            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        public IActionResult Contracts_Read(long id)
        {
            var result = OMAllProperty.Where(x => x.ObjId == id).SelectAll()
                .Select(x => x.ParentInsuranceOrganization.ShortName)
                .Select(x => x.ParentSubject.SubjectName)
                .Execute()
                .Select(x => new
                {
                    Id = x.EmpId,
                    Number = x.Ndog,
                    Date = x.Ndogdat,
                    Company = x.ParentInsuranceOrganization?.ShortName,
                    Subjet = x.ParentSubject?.SubjectName,
                    Amount = x.RasPripay
                }).ToList();

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public IActionResult Dwelling_Read(long id)
        {
            List<OMFlat> flatList = OMFlat.Where(x => x.LinkObjectMkd == id && x.KlassFlat_Code == Assftp_cd.Dwelling)
                .Select(x => x.Kvnom)
                .Select(x => x.CadastrNum)
                .Select(x => x.Fopl)
                .Select(x => x.KolGp)
                .Select(x => x.Fsp[0].EmpId)
                .Select(x => x.ParentBuilding.ParentBuildParcel.Area)
                .Select(x => x.LinkInsurEgrn)
                .Select(x => x.ParentPremase.TotalArea)
                .Select(x => x.ParentPremase.RoomsCount)
                .Select(x => x.StatusEgrn_Code)
                .OrderBy(x => x.Kvnom)
                .Execute();
            List<long> linkInsurEgrns = flatList.Where(x => x.LinkInsurEgrn.HasValue).Select(x => x.LinkInsurEgrn.Value).ToList();
            List<ObjectModel.Ehd.OMBuildParcel> omBuildParcels = linkInsurEgrns.Any()
                ? ObjectModel.Ehd.OMBuildParcel.Where(x => linkInsurEgrns.Contains(x.EmpId)).Select(x => x.Area).Execute()
                : new List<ObjectModel.Ehd.OMBuildParcel>();

            Dictionary<long, List<OMInputNach>> inputNachesList = GetOMInputNachesByFlat(flatList.Select(x => x.EmpId).ToList());
            DateTime currentPeriod = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var result = flatList.Select(x =>
                {
                    List<OMInputNach> mfcSources = inputNachesList.ContainsKey(x.EmpId) ? inputNachesList[x.EmpId] : new List<OMInputNach>();
                    OMInputNach mfcSource = mfcSources.FirstOrDefault();
                    List<OMFsp> omFsps = OMFsp.Where(m => m.ObjId == x.EmpId).Select(m => m.OplKodpl).Execute();
                    List<long?> fspIds = omFsps.Select(m => (long?)m.EmpId).ToList();
                    string insuredInCurrentPeriod = "-";

                    if (fspIds.Any())
                    {
                        List<OMBalance> omBalances = OMBalance.Where(m => fspIds.Contains(m.FspId) && m.PeriodRegDate == currentPeriod)
                            .Select(m => m.FlagInsur).Execute();
                        string flatType = mfcSource?.ParentFlatType?.Name?.ToLower();

                        if (omBalances.Any(m => m.FlagInsur == true))
                        {
                            if (flatType == "коммунальная квартира" || flatType == "отдельная квартира в долевой собственности")
                            {
                                if (omBalances.Any(m => m.FlagInsur != true)) insuredInCurrentPeriod = "?";
                                else if (x.Fopl == omFsps.Sum(m => m.OplKodpl)) insuredInCurrentPeriod = "+";
                                else insuredInCurrentPeriod = "?";
                            }
                            else
                            {
                                insuredInCurrentPeriod = "+";
                            }
                        }
                    }

                    return new
                    {
                        Id = x.EmpId,
                        FlatNumber = x.Kvnom,
                        Ordinal = x.Kvnom.ParseToInt(),
                        CadastralNumber = x.CadastrNum,
                        RoomsCount = x.KolGp,
                        TotalAreaEgrn = omBuildParcels.FirstOrDefault(y => y.EmpId == x.LinkInsurEgrn)?.Area,
                        FspCount = x.Fsp.Count,
                        TypeFlatMfc = mfcSource?.ParentFlatType?.Name,
                        StatusFlatMfc = mfcSource?.ParentFlatStatus?.Name,
                        RoomsCountMfc = mfcSource?.Kolgp,
                        TotalAreaMfc = GetMfcFlatArea(mfcSources, x.Fopl),
                        TotalAreaBti = x.ParentPremase?.TotalArea,
                        RoomsCountBti = x.ParentPremase?.RoomsCount,
                        InsuranceArea = mfcSources.Sum(m => m.Opl),
                        FillColor = x.StatusEgrn_Code == State.RemovedFromRegister || x.StatusEgrn_Code == State.Cancelled,
                        InsuredInCurrentPeriod = insuredInCurrentPeriod
                    };
                })
                .OrderBy(x => x.Ordinal)
                .ToList();

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public IActionResult FlatConsolidatedData(long id)
        {
            return View(id);
        }

        public IActionResult PartialFlatConsolidatedData(long id)
        {
            return PartialView("FlatConsolidatedData", id);
        }

        public IActionResult FlatConsolidatedData_Read(long id)
        {
            List<FlatConsolidatedDataDto> models = new List<FlatConsolidatedDataDto>(23);
            DataTable flat = _flatService.GetAllDataById(id);

            if (flat.Rows.Count == 0) throw new Exception($"Объект страхования не найден (ИД={id})");

            Dictionary<string, string> sources = GetAttributeSources(flat, "SOURCE_ATRIB");
            long egrnId = flat.Rows[0]["LINK_INSUR_EGRN"].ParseToLong();
            long btiId = flat.Rows[0]["LINK_BTI_FLAT"].ParseToLong();
            long buildingId = flat.Rows[0]["LINK_OBJECT_MKD"].ParseToLong();
            DataTable egrn = egrnId > 0 ? _buildingService.GetEhdAllDataById(egrnId) : new DataTable();
            DataTable bti = btiId > 0 ? _flatService.GetBtiAllDataById(btiId) : new DataTable();
            string unoms = string.Join(", ", _buildingService.GetAllBtiUnoms(buildingId));
            long? separateFlatTypeId = OMFlatType.Where(x => x.Code == 0).ExecuteFirstOrDefault()?.Id;
            List<OMInputNach> mfcSources = _damageAnalysisService.GetOMInputNachesByFlat(id);
            OMInputNach mfcSource = mfcSources.FirstOrDefault();

            string group = "1. Основные данные";
            models.Add(new FlatConsolidatedDataDto { Group = group });
            AddRow(models, sources, group, "Дата обновления данных", flat, "LOAD_DATE", egrn, "LOAD_DATE", bti, "UPDATE_DATE", typeof(DateTime), mfcSource?.PeriodRegDate);
            AddRow(models, sources, group, "Статус участия в программе", flat, "FLAG_INSUR", null, null, null, null, typeof(bool), null, null, true);
            AddRow(models, sources, group, "Назначение помещения", flat, "KLASS_FLAT", egrn, "R.ASSFTP_CD", bti, "CLASS_NAME");
            AddRow(models, sources, group, "Тип помещения", flat, "TYPE_FLAT", egrn, "R.ASSFTP1", bti, "TYPE_NAME", mfcValue: mfcSource?.ParentFlatType?.Name);
            AddRow(models, sources, group, "Статус объекта", flat, "STATUS_EGRN", egrn, "R.STATE", null, null, mfcValue: mfcSource?.ParentFlatStatus?.Name);
            AddRow(models, sources, group, "Кадастровый номер", flat, "CADASTR_NUM", egrn, "R.OBJECT_ID", bti, "KADASTR");
            AddRow(models, sources, group, "Дата постановки на кадастровый учет", flat, "CADASTR_DATE", egrn, "R.DATE_CREATED", null, null, typeof(DateTime));
            AddRow(models, sources, group, "Дата снятия с кадастрового учета", flat, "CADASTR_REMOVE", egrn, "R.DATE_REMOVED", null, null, typeof(DateTime));
            AddRow(models, sources, group, "Кол-во комнат", flat, "KOL_GP", null, null, bti, "ROOMS_COUNT", mfcValue: mfcSource?.Kolgp);
            AddRow(models, sources, group, "Общая площадь", flat, "FOPL", egrn, "AREA", bti, "TOTAL_AREA", typeof(decimal), GetMfcFlatArea(mfcSources, GetFlatArea(flat)), mfcSource?.PeriodRegDate);
            AddRow(models, sources, group, "Площадь страхования", null, null, null, null, null, null, typeof(decimal), mfcSource?.ParentFlatType?.Id == separateFlatTypeId ? mfcSources.FirstOrDefault(x => x.Opl.HasValue)?.Opl : mfcSources.Sum(x => x.Opl));

            group = "2. Адрес объекта";
            models.Add(new FlatConsolidatedDataDto { Group = group });
            AddRow(models, sources, group, "Округ", flat, "OKRUG", null, null, bti, "OKRUG_NAME", mfcValue: mfcSource?.ParentDistrict?.ParentOkrug?.ShortName);
            AddRow(models, sources, group, "Район", flat, "DISTRICT", egrn, "DISTRICT", bti, "DISTRICT_NAME", mfcValue: mfcSource?.ParentDistrict?.Name);
            AddRow(models, sources, group, "UNOM МКД", flat, "UNOM", null, null, null, null, mfcValue: mfcSource?.Unom);
            models.Last().Bti.Value = unoms;
            AddRow(models, sources, group, "Код ФИАС", flat, "GUID", null, null, bti, "CODE_FIAS");
            AddRow(models, sources, group, "Субъект РФ", flat, "REGION", egrn, "L.REGION", bti, "SUBJECT_RF_NAME");
            AddRow(models, sources, group, "Город", flat, "CITY", egrn, "L.LOCALITY", bti, "TOWN_NAME");
            AddRow(models, sources, group, "Поселение", null, null, null, null, bti, "SETTLEMENT_NAME");
            AddRow(models, sources, group, "Улица", flat, "STREET", egrn, "L.STREET", bti, "STREET_NAME");
            AddRow(models, sources, group, "Дом", flat, "HOUSE", egrn, "L.LEVEL1", bti, "HOUSE_NUMBER");
            AddRow(models, sources, group, "Корпус", flat, "CORPUS", egrn, "L.LEVEL2", bti, "KORPUS_NUMBER");
            AddRow(models, sources, group, "Строение", flat, "STRUCTURE", egrn, "L.LEVEL3", bti, "STRUCTURE_NUMBER");
            AddRow(models, sources, group, "Номер квартиры", flat, "KVNOM", egrn, "L.APARTMENT", bti, "KVNOM", mfcValue: mfcSource?.Nom);
            string flatNumber = GetValues(bti, "KVNOM")?.FirstOrDefault(x => x.To.Year == 9999)?.Value;

            AddRow(models, sources, group, "Адрес основной", flat, "FULL_ADDRESS", egrn, "L.ADDRESS_TOTAL", bti, "FULL_NAME", mfcValue: mfcSource?.AdresT);
            if (flatNumber.IsNotEmpty() && models.Last().Bti.Value.IsNotEmpty()) models.Last().Bti.Value += $", кв. {flatNumber}";

            AddRow(models, sources, group, "Адрес архивный", null, null, null, null, bti, "ARCHIVE");
            if (flatNumber.IsNotEmpty() && models.Last().Bti.Value.IsNotEmpty()) models.Last().Bti.Value += $",  кв. {flatNumber}";

            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        /// <summary>
        /// Получить список начислений для помещений, указанных в списке идентификаторов
        /// </summary>
        /// <param name="ids">список идентификаторов помещений</param>
        /// <returns>список начислений</returns>
        private Dictionary<long, List<OMInputNach>> GetOMInputNachesByFlat(List<long> ids)
        {
            var result = new Dictionary<long, List<OMInputNach>>();

            if (ids.IsNotEmpty())
            {
                List<OMInputNach> omInputNachs = OMInputNach.Where(x => ids.Contains((long)x.ParentFsp.ObjId)
                  && x.ParentFsp.ObjReestrId == OMFlat.GetRegisterId()
                  && x.TypeSource_Code == InsuranceSourceType.Mfc)
                  .Select(x => x.PeriodRegDate)
                  .Select(x => x.ParentFsp.ObjId)
                  .Select(x => x.ParentFlatType.Name)
                  .Select(x => x.ParentFlatStatus.Name)
                  .Select(x => x.Kolgp)
                  .Select(x => x.Fopl)
                  .Select(x => x.Opl)
                  .Select(x => x.ParentDistrict.ParentOkrug.ShortName)
                  .Select(x => x.ParentDistrict.Name)
                  .Select(x => x.Unom)
                  .Select(x => x.Nom)
                  .Select(x => x.AdresT)
                  .Execute();

                result = omInputNachs.GroupBy(x => x.ParentFsp.ObjId)
                    .Select(x =>
                    {
                        DateTime? maxPeriod = x.Max(y => y.PeriodRegDate);
                        return new { Key = x.Key.Value, Value = x.Where(y => y.PeriodRegDate == maxPeriod).ToList() };
                    })
                    .ToDictionary(x => x.Key, y => y.Value);
            }

            return result;
        }

        private List<OMInputNach> GetOMInputNachesByBuilding(long buildingId)
        {
            List<OMInputNach> omInputNachs = OMInputNach.Where(x => x.ParentFsp.ParentFlat.LinkObjectMkd == buildingId
                && x.ParentFsp.ObjReestrId == OMFlat.GetRegisterId()
                && x.TypeSource_Code == InsuranceSourceType.Mfc)
                .Select(x => x.PeriodRegDate)
                .Select(x => x.Unom)
                .Select(x => x.Fopl)
                .Select(x => x.Opl)
                .Select(x => x.ParentDistrict.ParentOkrug.ShortName)
                .Select(x => x.ParentDistrict.Name)
                .Select(x => x.AdresT)
                .Select(x => x.Kvnom)
                .Execute();

            DateTime? maxPeriod = omInputNachs.Max(x => x.PeriodRegDate);

            return omInputNachs.Where(x => x.PeriodRegDate == maxPeriod).ToList();
        }

        public IActionResult EgrnRights_Read(long flatId)
        {
            List<ObjectModel.Ehd.OMRegister> omRegisters = new List<ObjectModel.Ehd.OMRegister>();
            List<ObjectModel.Ehd.OMBuildParcel> omBuildParcels = new List<ObjectModel.Ehd.OMBuildParcel>();
            List<ObjectModel.Ehd.OMEgrp> omEgrns = new List<ObjectModel.Ehd.OMEgrp>();
            List<ObjectModel.Ehd.OMRight> omRights = new List<ObjectModel.Ehd.OMRight>();
            _flatService.GetEgrnRights(flatId, ref omRegisters, ref omBuildParcels, ref omEgrns, ref omRights);

            List<EgrnRightDto> models = new List<EgrnRightDto>();

            // Действующие права ЕГРН
            models.AddRange(omRights.Select(x =>
            {
                ObjectModel.Ehd.OMEgrp omEgrn = omEgrns.First(y => y.EhdEgrnId == x.EgrpId);
                ObjectModel.Ehd.OMBuildParcel tempOMBuildParcel = omBuildParcels.First(y => y.ObjectId == omEgrn.NumCadnum);
                ObjectModel.Ehd.OMRegister omRegister = omRegisters.First(y => y.BuildingParcelId == tempOMBuildParcel.BuildingParcelId);

                return new EgrnRightDto
                {
                    Type = omRegister.AssftpCd,
                    Area = omEgrn.Area.ToString()?.Replace(".", ","),
                    CadastralNumber = omEgrn.NumCadnum,
                    RightKind = x.RighttpCd,
                    Number = x.RegOpenRegnum,
                    BeginDate = x.RegOpenRegdt == DateTime.MinValue ? null : x.RegOpenRegdt,
                    EndDate = x.RegCloseRegdt == DateTime.MinValue ? null : x.RegCloseRegdt,
                    ShareSize = $"{x.ShareNum}/{x.ShareDen}",
                    ShareSizeText = x.ShareText,
                    ShareArea = ((x.ShareDen.HasValue && x.ShareDen != 0) ? ((omEgrn.Area ?? 0) * (x.ShareNum ?? 0) / x.ShareDen.Value) : 0m).ToString()?.Replace(".", ","),
                    Note = omEgrn.Name,
                };
            }).Where(x => x.EndDate == null).OrderByDescending(x => x.BeginDate).ToList());

            // Оконченные права ЕГРН
            models.AddRange(omRights.Select(x =>
            {
                ObjectModel.Ehd.OMEgrp omEgrn = omEgrns.First(y => y.EhdEgrnId == x.EgrpId);
                ObjectModel.Ehd.OMBuildParcel tempOMBuildParcel = omBuildParcels.First(y => y.ObjectId == omEgrn.NumCadnum);
                ObjectModel.Ehd.OMRegister omRegister = omRegisters.First(y => y.BuildingParcelId == tempOMBuildParcel.BuildingParcelId);

                return new EgrnRightDto
                {
                    Type = omRegister.AssftpCd,
                    Area = omEgrn.Area.ToString()?.Replace(".", ","),
                    CadastralNumber = omEgrn.NumCadnum,
                    RightKind = x.RighttpCd,
                    Number = x.RegOpenRegnum,
                    BeginDate = x.RegOpenRegdt == DateTime.MinValue ? null : x.RegOpenRegdt,
                    EndDate = x.RegCloseRegdt == DateTime.MinValue ? null : x.RegCloseRegdt,
                    ShareSize = $"{x.ShareNum}/{x.ShareDen}",
                    ShareSizeText = x.ShareText,
                    ShareArea = ((x.ShareDen.HasValue && x.ShareDen != 0) ? ((omEgrn.Area ?? 0) * (x.ShareNum ?? 0) / x.ShareDen.Value) : 0m).ToString()?.Replace(".", ","),
                    Note = omEgrn.Name,
                };
            }).Where(x => x.EndDate != null).OrderByDescending(x => x.EndDate).ToList());

            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        public IActionResult Damages_Read(long flatId)
        {
            List<LivingSpaceDamageDto> damages = _damageAnalysisService.Get(flatId).Select(x => LivingSpaceDamageDto.Map(x)).OrderByDescending(x => x.CaseDate).ToList();

            return Content(JsonConvert.SerializeObject(damages), "application/json");
        }

        public IActionResult PayTos_Read(long flatId)
        {
            List<LivingSpacePayToDto> paysTos = _insurancePayService.Get(flatId).Select(x => LivingSpacePayToDto.Map(x)).OrderByDescending(x => x.SkDate).ToList();

            return Content(JsonConvert.SerializeObject(paysTos), "application/json");
        }

        public IActionResult NoPays_Read(long flatId)
        {
            List<LivingSpaceNoPayDto> noPays = _insuranceNoPayService.Get(flatId).Select(x => LivingSpaceNoPayDto.Map(x)).OrderByDescending(x => x.EventDate).ToList();

            return Content(JsonConvert.SerializeObject(noPays), "application/json");
        }

        public IActionResult CreateInsuranceObject()
        {
            ViewBag.Title = "Создание объекта МКД";

            return View(InsuranceObjectsDto.Empty());
        }

        [HttpPost]
        public IActionResult CreateInsuranceObject(InsuranceObjectsDto model)
        {
            return JsonResponse(_buildingService.Create(InsuranceObjectsDto.Map(model)));
        }

        public IActionResult CreateLivingSpace()
        {
            ViewBag.Title = "Создание жилого помещения";

            return View(LivingSpaceDto.Empty());
        }

        [HttpPost]
        public IActionResult CreateLivingSpace(LivingSpaceDto model)
        {
            return JsonResponse(_flatService.Create(LivingSpaceDto.Map(model)));
        }

        public IActionResult GetInfoOfObject(long id)
        {
            OMBuilding omBuilding = _buildingService.GetByIdWithAddress(id);

            return JsonResponse(new
            {
                omBuilding?.EmpId,
                omBuilding?.Unom,
                Okrug = omBuilding?.ParentBtiOkrug?.Name,
                District = omBuilding?.ParentBtiDistrict?.Name,
                Address = InsuranceObjectAddressDto.Map(omBuilding)
            });
        }


        #region LinkMKD
        [HttpGet]
        public IActionResult LinkMKD()
        {
            SetUniqueSessionKey();

            if (CurrentUniqueSessionKey.IsNullOrEmpty())
            {
                throw new Exception("Не передан уникальный ключ сессии");
            }

            var list = RegistersCommon.CurrentList;

            if (list != null && list.Count > 0)
            {
                //CIPJS-800:Ручная связка объектов
                if (list.Count == 2)
                {
                    string mes = "";
                    List<OMBuilding> buildings = _buildingService.GetTwoBuildingForLink(list.ToList()[0], list.ToList()[1], ref mes);
                    ViewBag.Message = mes;
                    return View("~/Views/Tenements/LinkTwoMKD.cshtml", buildings.Select(x => new InsuranceObjectsDto
                    {
                        EmpId = x.EmpId,
                        Unom = x.Unom,
                        CadastralNumber = x.CadasrNum,
                        Address = new InsuranceObjectAddressDto
                        {
                            FullName = x.ParentAddress?.FullAddress
                        }
                    }).ToList());
                }
                ViewBag.SelectList = string.Join(',', list);
            }

            ViewBag.UniqueSessionKey = HttpContextHelper.HttpContext.Request.Query["UniqueSessionKey"];
            return View();
        }

        [HttpPost]
        public void LinkTwoMKD(long firstId, long secondId)
        {
            HashSet<long> list = new HashSet<long>() { firstId, secondId };

            // Временное решение.
            long listId = RegisterListDAL.CreateTempList(OMBuilding.GetRegisterId(),
                list,
                "ListOfLinkMkdProcess",
                $"Связать МКД: {firstId}, {secondId}");

            LongProcessManager.AddTaskToQueue("LinkMkdProcess", OMList.GetRegisterId(), listId);

            // Существующее решение. 
            //_buildingService.LinkTwoBuilding(firstId, secondId);
        }

        [HttpGet]
        public IActionResult LinkMKDAuto(string list)
        {
            if (list.IsNotEmpty())
            {
                ViewBag.SelectList = list;
            }

            ViewBag.UniqueSessionKey = HttpContextHelper.HttpContext.Request.Query["UniqueSessionKey"];
            return View();
        }

        public ActionResult NoBtiMkdRead([DataSourceRequest] DataSourceRequest request, string list)
        {
            List<long> selectList = new List<long>();
            if (list.IsNotEmpty())
            {
                var elems = list.Split(',');
                foreach (var elem in elems)
                {
                    selectList.Add(elem.ParseToLong());
                }
            }
            List<InsuranceObjectsForLinkDto> omBuildings = _buildingService.GetBuildingWithoutBti(selectList).Select(InsuranceObjectsForLinkDto.Map).ToList();

            return Content(JsonConvert.SerializeObject(omBuildings), "application/json");
        }

        public ActionResult NoFiasMkdRead([DataSourceRequest] DataSourceRequest request, long? id)
        {
            OMBuilding building = _buildingService.GetById(id);
            List<InsuranceObjectsForLinkDto> omBuildings = _buildingService.GetBuildingWithoutFsks(building).Select(x => InsuranceObjectsForLinkDto.Map(x, building)).ToList();

            return Content(JsonConvert.SerializeObject(omBuildings), "application/json");
        }

        public ActionResult SingelLinkBtiFiasMkdRead([DataSourceRequest] DataSourceRequest request, string list)
        {
            List<long> selectList = new List<long>();
            if (list.IsNotEmpty())
            {
                var elems = list.Split(',');
                foreach (var elem in elems)
                {
                    selectList.Add(elem.ParseToLong());
                }
            }
            List<InsuranceObjectsForLinkDto> omBuildings = _buildingService.GetBuildingWithoutBtiWithSingleLink(selectList).Select(InsuranceObjectsForLinkDto.Map).ToList();

            return Content(JsonConvert.SerializeObject(omBuildings), "application/json");
        }

        [HttpPost]
        public ActionResult NoBtiMkdUpdate([Bind(Prefix = "models")]string noBtiMkdJson, string reason, long? selectedId)
        {
            var results = new List<InsuranceObjectsForLinkDto>();
            List<InsuranceObjectsForLinkDto> insuranceObjects = JsonConvert.DeserializeObject<List<InsuranceObjectsForLinkDto>>(noBtiMkdJson);

            OMBuilding building = _buildingService.GetByIdWithAddress(selectedId);

            if (insuranceObjects != null && ModelState.IsValid)
            {
                foreach (var insur in insuranceObjects)
                {
                    if (building != null)
                    {
                        results.Add(InsuranceObjectsForLinkDto.Map(_buildingService.UpdateBuildingByLink(InsuranceObjectsForLinkDto.Map(insur), reason), building));
                    }
                    else
                    {
                        results.Add(InsuranceObjectsForLinkDto.Map(_buildingService.UpdateBuildingByLink(InsuranceObjectsForLinkDto.Map(insur), reason)));
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(results), "application/json");
        }

        public IActionResult GetBuildingById(long? id, long? selectedId)
        {
            OMBuilding omBuilding = _buildingService.GetByIdWithAddress(id);
            OMBuilding comparerBuilding = _buildingService.GetByIdWithAddress(selectedId);
            if (comparerBuilding == null)
            {
                return null;
            }
            return Content(JsonConvert.SerializeObject(InsuranceObjectsForLinkDto.Map(omBuilding, comparerBuilding)), "application/json");
        }

        public void BuildingNotActual(long? id)
        {
            _buildingService.BuildingNotActual(id);
        }

        public void LinkMKDSave(long? fiasId, long? btiId)
        {
            _buildingService.LinkBuilding(fiasId, btiId, false);
        }

        public ActionResult CheckFlats(long? fiasId, long? btiId)
        {
            string result = _flatService.CheckLinkFlats(fiasId, btiId).Replace("\n", "<br />");
            int count = Regex.Matches(result, @"\<br \/\>").Count;
            return Content(JsonConvert.SerializeObject(new { Count = count, Result = result }), "application/json");
        }

        public void LinkMKDArraySave(List<InsuranceObjectsForLinkSaveDto> arrayIds)
        {
            if (arrayIds != null)
            {
                foreach (InsuranceObjectsForLinkSaveDto elem in arrayIds)
                {
                    _buildingService.LinkBuilding(elem.FiasId, elem.BtiId, true);
                }
            }
        }
        #endregion

        #region UnLinkMkd
        [HttpGet]
        public IActionResult UnLinkMKD()
        {
            SetUniqueSessionKey();

            if (CurrentUniqueSessionKey.IsNullOrEmpty())
            {
                throw new Exception("Не передан уникальный ключ сессии");
            }

            var list = RegistersCommon.CurrentList;
            List<UnLinkBuildingDTO> buildings = new List<UnLinkBuildingDTO>();
            if (list != null && list.Count > 0)
            {
                buildings = OMBuilding.Where(x => list.Contains(x.EmpId) && x.SpecialActual == 1)
                    .Select(x => x.EmpId)
                    .Select(x => x.Unom)
                    .Select(x => x.CadasrNum)
                    .Select(x => x.YearStroi)
                    .Execute()
                    .Select(x => new UnLinkBuildingDTO() { EmpId = x.EmpId, Unom = x.Unom, CadasrNum = x.CadasrNum, YearStroi = x.YearStroi }).ToList();
            }

            ViewBag.UniqueSessionKey = HttpContextHelper.HttpContext.Request.Query["UniqueSessionKey"];
            return View(buildings);
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult UnLinkMKD([FromBody]IEnumerable<long> ids)
        {
            try
            {
                if (ids != null && ids.Count() > 0)
                {
                    long listId = RegisterListDAL.CreateTempList(OMBuilding.GetRegisterId(),
                        ids.ToHashSet(),
                        "ListOfUnLinkMkdProcess",
                        $"Отвязать МКД");

                    LongProcessManager.AddTaskToQueue("UnLinkMkdProcess", OMList.GetRegisterId(), listId);
                    return Json("Обработка успешно запущена!");
                }
                else
                {
                    return BadRequest($"Не найдены выбранные МКД для обработки!");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка: {ex.Message} StackTrace: {ex.StackTrace}");
            }
        }

        #endregion


        #region LinkLivingSpace
        public IActionResult LinkLivingSpace(LinkMKDLivingSpacesDto model)
        {
            ViewBag.UniqueSessionKey = HttpContextHelper.HttpContext.Request.Query["UniqueSessionKey"];
            return View(model);
        }

        public ActionResult NoBtiFlatRead([DataSourceRequest] DataSourceRequest request, LinkMKDLivingSpacesDto model)
        {
            List<LivingSpacesLinkDto> omFlats = _flatService.GetFlatWithoutBti(model.NoBtiMkdId, model.NoFiasMkdId).Select(LivingSpacesLinkDto.Map).ToList();

            return Content(JsonConvert.SerializeObject(omFlats), "application/json");
        }

        public ActionResult NoFiasFlatRead([DataSourceRequest] DataSourceRequest request, LinkMKDLivingSpacesDto model, long? id)
        {
            OMFlat flat = _flatService.Get(id);
            List<LivingSpacesLinkDto> omFlats = _flatService.GetFlatWithoutFsks(flat, model.NoFiasMkdId).Select(x => LivingSpacesLinkDto.Map(x, flat)).ToList();

            return Content(JsonConvert.SerializeObject(omFlats), "application/json");
        }

        public ActionResult SingelLinkBtiFiasFlatRead([DataSourceRequest] DataSourceRequest request, LinkMKDLivingSpacesDto model)
        {
            List<LivingSpacesLinkDto> omFlats = _flatService.GetFlatWithoutBtiWithSingleLink(model.NoBtiMkdId, model.NoFiasMkdId).Select(LivingSpacesLinkDto.Map).ToList();

            return Content(JsonConvert.SerializeObject(omFlats), "application/json");
        }

        [HttpPost]
        public ActionResult NoBtiFlatUpdate([Bind(Prefix = "models")]string noBtiFlatJson, string reason, long? selectedId)
        {
            var results = new List<LivingSpacesLinkDto>();
            List<LivingSpacesLinkDto> insuranceObjects = JsonConvert.DeserializeObject<List<LivingSpacesLinkDto>>(noBtiFlatJson);

            OMFlat flat = _flatService.Get(selectedId);

            if (insuranceObjects != null && ModelState.IsValid)
            {
                foreach (var insur in insuranceObjects)
                {
                    if (flat != null)
                    {
                        results.Add(LivingSpacesLinkDto.Map(_flatService.UpdateLink(LivingSpacesLinkDto.Map(insur), reason), flat));
                    }
                    else
                    {
                        results.Add(LivingSpacesLinkDto.Map(_flatService.UpdateLink(LivingSpacesLinkDto.Map(insur), reason)));
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(results), "application/json");
        }

        public IActionResult GetFlatById(long? id, long? selectedId)
        {
            OMFlat omFlat = _flatService.Get(id);
            OMFlat comparerFlat = _flatService.Get(selectedId);
            if (comparerFlat == null)
            {
                return null;
            }
            return Content(JsonConvert.SerializeObject(LivingSpacesLinkDto.Map(omFlat, comparerFlat)), "application/json");
        }

        public void FlatNotActual(long? id)
        {
            _flatService.FlatNotActual(id);
        }

        public void LinkFlatSave(long? fiasId, long? btiId)
        {
            _flatService.LinkFlat(fiasId, btiId);
        }

        public void LinkFlatArraySave(List<InsuranceObjectsForLinkSaveDto> arrayIds)
        {
            if (arrayIds != null)
            {
                foreach (InsuranceObjectsForLinkSaveDto elem in arrayIds)
                {
                    _flatService.LinkFlat(elem.FiasId, elem.BtiId);
                }
            }
        }
        #endregion

        [HttpGet]
        public IActionResult InsuranceObjectNote(long id)
        {
            OMBuilding omBuilding = _buildingService.GetBuildingNote(id);

            var changes = _buildingService.GetBuildNoteChangesData(id);
            string changeUserName = string.Empty;
            if (!string.IsNullOrEmpty(changes.changeUser))
            {
                changeUserName = new UserService().GetUserNameById(changes.changeUser.ParseToLongNullable());
            }

            return View(new InsuranceObjectNoteDto
            {
                EmpId = id,
                Note = omBuilding.Note,
                ChangeUser = changeUserName,
                ChangeDateTime = changes.changeDate
            });
        }

        [HttpPost]
        public void InsuranceObjectNote(InsuranceObjectNoteDto model)
        {
            OMBuilding omBuilding = new OMBuilding
            {
                EmpId = model.EmpId,
                Note = model.Note
            };
            _buildingService.UpdateNote(omBuilding);
        }

        /// <summary>
        /// Предыдущее значение не равное null из коллекции 
        /// </summary>
        /// <param name="index">индекс элемента, для которого нужно получить предыдущее значение</param>
        /// <param name="list">коллекция значений (отсотированная в обратном порядке)</param>
        /// <returns>Значение элемента</returns>
        private static decimal getLastValue(int index, List<decimal?> list)
        {
            decimal result = 0M;

            if (list.IsNotEmpty() && list.Count - index > 0)
            {
                for (int i = index + 1; i < list.Count - 1; i++)
                {
                    if (list[i].HasValue)
                    {
                        result = list[i].Value;
                        break;
                    }
                }
            }

            return result;
        }

        private void MakeFirstBuildingGroup(AddRowBuildingConstantData constData)
        {
            constData.Models.Add(new BuildingConsolidatedDataDto { Group = constData.Group });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Дата обновления данных",
                BuildingColumn = "LOAD_DATE",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.LoadDate),
                EgrnColumn = "LOAD_DATE",
                EgrnReestrId = OMBuildParcel.GetRegisterId(),
                EgrnObjectId = "EMP_ID",
                EgrnAttributeId = OMBuildParcel.GetColumnAttributeId(x => x.LoadDate),
                BtiColumn = "DOWNLOAD_DATE",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.DownloadDate),
                Type = typeof(DateTime),
                MfcValue = constData?.MfcSources?.FirstOrDefault()?.PeriodRegDate,
                MfcData = constData?.MfcSources?.FirstOrDefault()?.PeriodRegDate
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "UNOM",
                BuildingColumn = "UNOM",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.Unom),
                EgrnColumn = "OLD_NUMBER",
                EgrnReestrId = OMOldNumber.GetRegisterId(),
                EgrnObjectId = "A4_ID",
                EgrnAttributeId = OMOldNumber.GetColumnAttributeId(x => x.Number),
                BtiColumn = "UNOM",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Unom),
                MfcValue = constData?.MfcSources?.FirstOrDefault()?.Unom,
                MfcData = constData?.MfcSources?.FirstOrDefault()?.PeriodRegDate
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Год постройки",
                BuildingColumn = "YEAR_STROI",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.YearStroi),
                EgrnColumn = "EHD_YEAR_BUILT",
                EgrnReestrId = OMChar.GetRegisterId(),
                EgrnObjectId = "A1_ID",
                EgrnAttributeId = OMChar.GetColumnAttributeId(x => x.YearBuilt),
                BtiColumn = "GDPOSTR",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.GdPostr)
            });

            AddRow(constData, new AddRowBuildingData
            {
                Name = "Общая площадь",
                BuildingColumn = "OPL",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.Opl),
                EgrnColumn = "AREA",
                EgrnReestrId = OMBuildParcel.GetRegisterId(),
                EgrnObjectId = "EMP_ID",
                EgrnAttributeId = OMBuildParcel.GetColumnAttributeId(x => x.Area),
                BtiColumn = "OPL",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Opl),
                Type = typeof(decimal)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Кадастровый номер",
                BuildingColumn = "CADASTR_NUM",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.CadasrNum),
                EgrnColumn = "OBJECT_ID",
                EgrnReestrId = OMBuildParcel.GetRegisterId(),
                EgrnObjectId = "EMP_ID",
                EgrnAttributeId = OMBuildParcel.GetColumnAttributeId(x => x.ObjectId),
                BtiColumn = "KAD_N",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.KadN)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Дата постановки на кадастровый учет",
                BuildingColumn = "CADASTR_DATE",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.CadastrDate),
                EgrnColumn = "DATE_CREATED",
                EgrnReestrId = OMRegister.GetRegisterId(),
                EgrnObjectId = "R_EMP_ID",
                EgrnAttributeId = OMRegister.GetColumnAttributeId(x => x.DateCreated),
                Type = typeof(DateTime)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Дата снятия с кадастрового учета",
                BuildingColumn = "CADASTR_REMOVE",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.CadastrRemove),
                EgrnColumn = "DATE_REMOVED",
                EgrnReestrId = OMRegister.GetRegisterId(),
                EgrnObjectId = "R_EMP_ID",
                EgrnAttributeId = OMRegister.GetColumnAttributeId(x => x.DateRemoved),
                Type = typeof(DateTime)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Статус дома ГБУ",
                BuildingColumn = "TYPE_MKD",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.TypeMkd)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Статус объекта",
                BuildingColumn = "STATUS_EGRN",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.StatusEgrn),
                EgrnColumn = "STATE",
                EgrnReestrId = OMRegister.GetRegisterId(),
                EgrnObjectId = "R_EMP_ID",
                EgrnAttributeId = OMRegister.GetColumnAttributeId(x => x.State)
            });
            string diapKv = _buildingService.GetBuildingDiapKv(constData?.BuildingTable?.Rows[0]?["EMP_ID"]?.ParseToLongNullable());
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Диапазон квартир",
                Type = typeof(string),
                MkdValue = diapKv,
                BtiValue = diapKv
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Общая площадь квартир",
                Type = typeof(decimal),
                MfcValue = constData?.MfcSources?.Sum(x => x.Fopl),
                MfcData = constData?.MfcSources?.FirstOrDefault()?.PeriodRegDate
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Площадь страхования",
                Type = typeof(decimal),
                MfcValue = constData?.MfcSources?.Sum(x => x.Opl),
                MfcData = constData?.MfcSources?.FirstOrDefault()?.PeriodRegDate
            });
        }

        private void MakeSecondBuildingGroup(AddRowBuildingConstantData constData, long mfcFlatCount, long? kolGpEgrnId, long? kolGpEgrn, long? kolGpBti)
        {
            bool noInsurFlag = constData.BuildingTable.Rows.Count == 0 || constData.BuildingTable?.Rows[0]?["FLAG_INSUR"]?.ParseToShort() == 0;
            constData.Models.Add(new BuildingConsolidatedDataDto { Group = constData.Group });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Статус участия в программе",
                BuildingColumn = "FLAG_INSUR",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.FlagInsur),
                Type = typeof(bool),
                MfcValue = null
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Назначение объекта",
                BuildingColumn = "PURPOSE_NAME",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.PurposeName),
                EgrnColumn = "ASSIGNATION_CODE",
                EgrnReestrId = OMBuildParcel.GetRegisterId(),
                EgrnObjectId = "EMP_ID",
                EgrnAttributeId = OMBuildParcel.GetColumnAttributeId(x => x.AssignationCode),
                BtiColumn = "NAZ",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Naz),
                CheckInsurFlag = noInsurFlag
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Класс объекта",
                BtiColumn = "KL",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Kl),
                CheckInsurFlag = noInsurFlag
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Аварийное строение",
                BtiColumn = "AVARZD",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Avarzd),
                Type = typeof(bool),
                CheckInsurFlag = noInsurFlag
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Состояние отселение корпуса",
                BtiColumn = "OTSKORP",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Otskorp),
                CheckInsurFlag = noInsurFlag
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Состояние статкарты",
                BuildingColumn = "STATUS_SOST_BTI",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.StatusSostBti),
                BtiColumn = "SOST",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Sost),
                CheckInsurFlag = noInsurFlag
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Материал стен",
                EgrnColumn = "EHD_WALL",
                EgrnReestrId = OMConstruct.GetRegisterId(),
                EgrnObjectId = "A3_ID",
                EgrnAttributeId = OMConstruct.GetColumnAttributeId(x => x.Wall),
                BtiColumn = "MST",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Mst),
                CheckInsurFlag = noInsurFlag
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Количество квартир",
                MkdValue = GetMkdFlatsCount(constData.BuildingTable?.Rows[0]?["EMP_ID"]?.ParseToLongNullable()),
                EgrnColumn = "COUNT",
                EgrnReestrId = OMBuildingSvodDataCalculated.GetRegisterId(),
                EgrnObjectId = kolGpEgrnId.HasValue ? kolGpEgrnId.Value.ToString() : string.Empty,
                EgrnAttributeId = (int?)kolGpEgrn,
                BtiValue = kolGpBti,
                MfcValue = mfcFlatCount,
                MfcData = constData?.MfcSources?.FirstOrDefault()?.PeriodRegDate,
                CheckInsurFlag = noInsurFlag
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Категория строения",
                BtiColumn = "KAT",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Kat),
                CheckInsurFlag = noInsurFlag
            });
        }

        private void MakeThirdBuildingGroup(AddRowBuildingConstantData constData)
        {
            constData.Models.Add(new BuildingConsolidatedDataDto { Group = constData.Group });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Округ",
                BuildingColumn = "OKRUG",
                BuildingReestrId = OMBtiOkrug.GetRegisterId(),
                BuildingObjectId = "OKRUG_ID",
                BuildingAttributeId = OMBtiOkrug.GetColumnAttributeId(x => x.Name),
                BtiColumn = "OKRUG_ID",
                BtiReestrId = OMBtiOkrug.GetRegisterId(),
                BtiObjectId = "O_OKRUG_ID",
                BtiAttributeId = OMBtiOkrug.GetColumnAttributeId(x => x.Name),
                MfcValue = constData?.MfcSources?.FirstOrDefault()?.ParentDistrict?.ParentOkrug?.ShortName,
                MfcData = constData?.MfcSources?.FirstOrDefault()?.PeriodRegDate
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Район",
                BuildingColumn = "DISTRICT",
                BuildingReestrId = OMBtiDistrict.GetRegisterId(),
                BuildingObjectId = "DISTRICT_ID",
                BuildingAttributeId = OMBtiDistrict.GetColumnAttributeId(x => x.Name),
                EgrnColumn = "DISTRICT",
                EgrnReestrId = OMLocation.GetRegisterId(),
                EgrnObjectId = "L_EMP_ID",
                EgrnAttributeId = OMLocation.GetColumnAttributeId(x => x.District),
                BtiColumn = "DISTRICT_ID",
                BtiReestrId = OMBtiDistrict.GetRegisterId(),
                BtiObjectId = "D_DISTRICT_ID",
                BtiAttributeId = OMBtiDistrict.GetColumnAttributeId(x => x.Name),
                MfcValue = constData?.MfcSources?.FirstOrDefault()?.ParentDistrict?.Name,
                MfcData = constData?.MfcSources?.FirstOrDefault()?.PeriodRegDate
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Код ФИАС",
                BuildingColumn = "GUID_FIAS_MKD",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.GuidFiasMkd),
                BtiColumn = "CODE_FIAS",
                BtiReestrId = OMADDRESS.GetRegisterId(),
                BtiObjectId = "A_EMP_ID",
                BtiAttributeId = OMADDRESS.GetColumnAttributeId(x => x.CodeFias)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Субъект РФ",
                BuildingColumn = "REGION",
                BuildingReestrId = OMAddress.GetRegisterId(),
                BuildingObjectId = "ADDRESS_ID",
                BuildingAttributeId = OMAddress.GetColumnAttributeId(x => x.Region),
                EgrnColumn = "REGION",
                EgrnReestrId = OMLocation.GetRegisterId(),
                EgrnObjectId = "L_EMP_ID",
                EgrnAttributeId = OMLocation.GetColumnAttributeId(x => x.Region),
                BtiColumn = "SUBJECT_RF_NAME",
                BtiReestrId = OMADDRESS.GetRegisterId(),
                BtiObjectId = "A_EMP_ID",
                BtiAttributeId = OMADDRESS.GetColumnAttributeId(x => x.SubjectRfName)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Город",
                BuildingColumn = "CITY",
                BuildingReestrId = OMAddress.GetRegisterId(),
                BuildingObjectId = "ADDRESS_ID",
                BuildingAttributeId = OMAddress.GetColumnAttributeId(x => x.City),
                EgrnColumn = "LOCALITY",
                EgrnReestrId = OMLocation.GetRegisterId(),
                EgrnObjectId = "L_EMP_ID",
                EgrnAttributeId = OMLocation.GetColumnAttributeId(x => x.Locality),
                BtiColumn = "TOWN_NAME",
                BtiReestrId = OMADDRESS.GetRegisterId(),
                BtiObjectId = "A_EMP_ID",
                BtiAttributeId = OMADDRESS.GetColumnAttributeId(x => x.TownName)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Поселение",
                BtiColumn = "SETTLEMENT_NAME",
                BtiReestrId = OMADDRESS.GetRegisterId(),
                BtiObjectId = "A_EMP_ID",
                BtiAttributeId = OMADDRESS.GetColumnAttributeId(x => x.SettlementName)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Улица",
                BuildingColumn = "STREET",
                BuildingReestrId = OMAddress.GetRegisterId(),
                BuildingObjectId = "ADDRESS_ID",
                BuildingAttributeId = OMAddress.GetColumnAttributeId(x => x.Street),
                EgrnColumn = "STREET",
                EgrnReestrId = OMLocation.GetRegisterId(),
                EgrnObjectId = "L_EMP_ID",
                EgrnAttributeId = OMLocation.GetColumnAttributeId(x => x.Street),
                BtiColumn = "STREET_NAME",
                BtiReestrId = OMADDRESS.GetRegisterId(),
                BtiObjectId = "A_EMP_ID",
                BtiAttributeId = OMADDRESS.GetColumnAttributeId(x => x.StreetName)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Дом",
                BuildingColumn = "HOUSE",
                BuildingReestrId = OMAddress.GetRegisterId(),
                BuildingObjectId = "ADDRESS_ID",
                BuildingAttributeId = OMAddress.GetColumnAttributeId(x => x.House),
                EgrnColumn = "LEVEL1",
                EgrnReestrId = OMLocation.GetRegisterId(),
                EgrnObjectId = "L_EMP_ID",
                EgrnAttributeId = OMLocation.GetColumnAttributeId(x => x.Level1),
                BtiColumn = "HOUSE_NUMBER",
                BtiReestrId = OMADDRESS.GetRegisterId(),
                BtiObjectId = "A_EMP_ID",
                BtiAttributeId = OMADDRESS.GetColumnAttributeId(x => x.HouseNumber)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Корпус",
                BuildingColumn = "CORPUS",
                BuildingReestrId = OMAddress.GetRegisterId(),
                BuildingObjectId = "ADDRESS_ID",
                BuildingAttributeId = OMAddress.GetColumnAttributeId(x => x.Corpus),
                EgrnColumn = "LEVEL2",
                EgrnReestrId = OMLocation.GetRegisterId(),
                EgrnObjectId = "L_EMP_ID",
                EgrnAttributeId = OMLocation.GetColumnAttributeId(x => x.Level2),
                BtiColumn = "KORPUS_NUMBER",
                BtiReestrId = OMADDRESS.GetRegisterId(),
                BtiObjectId = "A_EMP_ID",
                BtiAttributeId = OMADDRESS.GetColumnAttributeId(x => x.KorpusNumber)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Строение",
                BuildingColumn = "STRUCTURE",
                BuildingReestrId = OMAddress.GetRegisterId(),
                BuildingObjectId = "ADDRESS_ID",
                BuildingAttributeId = OMAddress.GetColumnAttributeId(x => x.Structure),
                EgrnColumn = "LEVEL3",
                EgrnReestrId = OMLocation.GetRegisterId(),
                EgrnObjectId = "L_EMP_ID",
                EgrnAttributeId = OMLocation.GetColumnAttributeId(x => x.Level3),
                BtiColumn = "STRUCTURE_NUMBER",
                BtiReestrId = OMADDRESS.GetRegisterId(),
                BtiObjectId = "A_EMP_ID",
                BtiAttributeId = OMADDRESS.GetColumnAttributeId(x => x.StructureNumber)
            });

            string mainAddress = _buildingService.GetBuildingBtiMainAddress(constData?.BuildingTable?.Rows[0]?["EMP_ID"]?.ParseToLongNullable());
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Адрес основной",
                BuildingColumn = "FULL_ADDRESS",
                BuildingReestrId = OMAddress.GetRegisterId(),
                BuildingObjectId = "ADDRESS_ID",
                BuildingAttributeId = OMAddress.GetColumnAttributeId(x => x.FullAddress),
                EgrnColumn = "ADDRESS_TOTAL",
                EgrnReestrId = OMLocation.GetRegisterId(),
                EgrnObjectId = "L_EMP_ID",
                EgrnAttributeId = OMLocation.GetColumnAttributeId(x => x.AddressTotal),
                BtiValue = mainAddress,
                MfcValue = constData?.MfcSources?.FirstOrDefault()?.AdresT,
                MfcData = constData?.MfcSources?.FirstOrDefault()?.PeriodRegDate
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Адрес архивный",
                BtiColumn = "ARCHIVE",
                BtiReestrId = OMADDRESS.GetRegisterId(),
                BtiObjectId = "BA_EMP_ID",
                BtiAttributeId = OMADDRESS.GetColumnAttributeId(x => x.FullName)
            });
        }

        private void MakeFourthBuildingGroup(AddRowBuildingConstantData constData)
        {
            constData.Models.Add(new BuildingConsolidatedDataDto { Group = constData.Group });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Кол-во этажей",
                BuildingColumn = "COUNT_FLOOR",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.CountFloor),
                EgrnColumn = "EHD_FLOORS",
                EgrnReestrId = OMFloors.GetRegisterId(),
                EgrnObjectId = "A2_ID",
                EgrnAttributeId = OMFloors.GetColumnAttributeId(x => x.Floors),
                BtiColumn = "ET",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Et)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Площадь жилых помещений",
                BuildingColumn = "OPL_G",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.OplG),
                BtiColumn = "OPL_G",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.OplG),
                Type = typeof(decimal)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Площадь нежилых помещений",
                BuildingColumn = "OPL_N",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.OplN),
                BtiColumn = "OPL_N",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.OplN),
                Type = typeof(decimal)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Площадь балконов",
                BuildingColumn = "BPL",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.Bpl),
                BtiColumn = "BPL",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Bpl),
                Type = typeof(decimal)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Площадь холодных помещений",
                BuildingColumn = "HPL",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.Hpl),
                BtiColumn = "HPL",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Hpl),
                Type = typeof(decimal)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Площадь лоджий",
                BuildingColumn = "LPL",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.Lpl),
                BtiColumn = "LPL",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Lpl),
                Type = typeof(decimal)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Площадь кровли",
                BuildingColumn = "KROVPL",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.Krovpl),
                BtiColumn = "KROVPL",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Krovpl),
                Type = typeof(decimal)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Кол-во лифтов пассажирских",
                BuildingColumn = "LFPQ",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.Lfpq),
                BtiColumn = "LFPQ",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Lfpq)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Кол-во лифтов грузопассажирских",
                BuildingColumn = "LFGPQ",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.Lfgpq),
                BtiColumn = "LFGPQ",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Lfgpq)
            });
            AddRow(constData, new AddRowBuildingData
            {
                Name = "Кол-во лифтов грузовых",
                BuildingColumn = "LFGQ",
                BuildingReestrId = OMBuilding.GetRegisterId(),
                BuildingObjectId = "EMP_ID",
                BuildingAttributeId = OMBuilding.GetColumnAttributeId(x => x.Lfgq),
                BtiColumn = "LFGQ",
                BtiReestrId = OMBtiBuilding.GetRegisterId(),
                BtiObjectId = "EMP_ID",
                BtiAttributeId = OMBtiBuilding.GetColumnAttributeId(x => x.Lfgq)
            });
        }

        private Dictionary<string, string> GetAddressSources(DataTable dataTable)
        {
            string sourceAtrib = GetValues(dataTable, "SOURCE_ADDRESS_CODE")?.FirstOrDefault(x => x.To.Year == 9999)?.Value;
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(sourceAtrib))
            {
                var addressSource = (AddressSource)EnumExtensions.GetEnumByCode(typeof(AddressSource), sourceAtrib);
                var source = string.Empty;

                if (addressSource == AddressSource.Bti) source = "Б";
                else if (addressSource == AddressSource.Egrn) source = "Е";
                else if (addressSource == AddressSource.Fias) source = "Р";

                result.Add("FULL_ADDRESS", source);
            }

            return result;
        }

        private Dictionary<string, string> GetAttributeSources(DataTable dataTable, string column)
        {
            string sourceAtrib = GetValues(dataTable, column)?.FirstOrDefault(x => x.To.Year == 9999)?.Value;
            Dictionary<string, SourceInput> sources = null;
            Dictionary<string, string> result = null;

            try
            {
                sources = JsonConvert.DeserializeObject<Dictionary<string, SourceInput>>(sourceAtrib);
            }
            catch { }
            finally
            {
                if (sources != null)
                {
                    result = new Dictionary<string, string>(sources?.Count ?? 0);

                    foreach (var item in sources)
                    {
                        string source = string.Empty;

                        if (item.Value == SourceInput.Bti) source = "Б";
                        else if (item.Value == SourceInput.Egrn) source = "Е";
                        else if (item.Value == SourceInput.InsuranceCompany) source = "С";
                        else if (item.Value == SourceInput.ManualInput) source = "Р";
                        else if (item.Value == SourceInput.Mfc) source = "М";
                        else if (item.Value == SourceInput.None) source = "Р";

                        result.Add(item.Key, source);
                    }
                }
            }

            return result;
        }

        private void AddRow(AddRowBuildingConstantData constData, AddRowBuildingData data)
        {
            var dto = new BuildingConsolidatedDataDto
            {
                Group = constData.Group,
                Name = data.Name,
                Source = data.BuildingColumn.IsNotEmpty() ? constData.Sources != null ? constData.Sources.GetValueOrDefault(data.BuildingColumn, string.Empty) : string.Empty : string.Empty,
                Mkd = data.MkdValue == null ?
                new ConsolidatedDataValueDto
                {
                    Value = GetValue(constData.BuildingTable, data.BuildingColumn, data.Type),
                    ReestrId = data.BuildingReestrId,
                    ObjectId = constData.BuildingTable.Rows.Count > 0 && data.BuildingObjectId.IsNotEmpty() ? constData.BuildingTable.Rows[0][data.BuildingObjectId].ParseToLong() : -1,
                    AttributeId = data.BuildingAttributeId,
                    HaveHistory = data.BuildingColumn.IsNotEmpty() ? constData.BuildingTable.AsEnumerable().Select(x => x[data.BuildingColumn].ToString()).Distinct().Count() > 1 : false
                } :
                new ConsolidatedDataValueDto
                {
                    Value = GetFormattedValue(data.MkdValue, data.Type, true),
                    HaveHistory = false
                },
                Egrn = data.EgrnValue == null ?

                (data.EgrnColumn == "COUNT" ? new ConsolidatedDataValueDto
                { // особый случай для COUNT, так как рассчитывается вне таблицы
                    Value = data.EgrnAttributeId.HasValue ? data.EgrnAttributeId.Value.ToString() : string.Empty,
                    ReestrId = data.EgrnReestrId,
                    ObjectId = constData.EgrnTable.Rows.Count > 0 && data.EgrnObjectId.IsNotEmpty() ? data.EgrnObjectId.ParseToLong() : -1,
                    AttributeId = OMBuildingSvodDataCalculated.GetColumnAttributeId(x => x.KolGpEgrn),
                    HaveHistory = true // TODO
                } :
                new ConsolidatedDataValueDto
                {
                    Value = GetValue(constData.EgrnTable, data.EgrnColumn, data.Type),
                    ReestrId = data.EgrnReestrId,
                    ObjectId = constData.EgrnTable.Rows.Count > 0 && data.EgrnObjectId.IsNotEmpty() ? constData.EgrnTable.Rows[0][data.EgrnObjectId].ParseToLong() : -1,
                    AttributeId = data.EgrnAttributeId,
                    HaveHistory = data.EgrnColumn.IsNotEmpty() ? constData.EgrnTable.AsEnumerable().Select(x => x[data.EgrnColumn].ToString()).Distinct().Count() > 1 : false
                }) :
                  new ConsolidatedDataValueDto
                  {
                      Value = GetFormattedValue(data.EgrnValue, data.Type, true),
                      HaveHistory = false
                  },
                Btis = data.BtiValue == null ? constData.BtiTables?.Select(x => new ConsolidatedDataValueDto
                {
                    Value = GetValue(x, data.BtiColumn, data.Type),
                    ReestrId = data.BtiReestrId,
                    ObjectId = x.Rows.Count > 0 && data.BtiObjectId.IsNotEmpty() ? x.Rows[0][data.BtiObjectId].ParseToLong() : -1,
                    AttributeId = data.BtiAttributeId,
                    HaveHistory = data.BtiColumn.IsNotEmpty() ? x.AsEnumerable().Select(r => r[data.BtiColumn].ToString()).Distinct().Count() > 1 : false
                })?.ToList() : new List<ConsolidatedDataValueDto>(){
                    new ConsolidatedDataValueDto
                    {
                        Value = GetFormattedValue(data.BtiValue, data.Type, true),
                        HaveHistory = false
                    }
                },
                Mfc = new ConsolidatedDataValueDto
                {
                    Value = GetFormattedValue(data.MfcValue, data.Type, true),
                    PeriodRegDate = data.MfcData
                },
                OrdinalNumber = constData.Models.Count
            };
            constData.Models.Add(dto);
            if (constData?.BtiTables?.Count != null && constData?.BtiTables?.Count > 0 && data?.BtiColumn != null)
            {
                if (data.CheckInsurFlag && !_buildingService.CheckFlagInsurCondition(constData.BtiTables.FirstOrDefault(), data.BtiColumn))
                {
                    dto.NameClass = "bg-red";
                }
            }
        }

        private void AddRow(List<FlatConsolidatedDataDto> models, Dictionary<string, string> sources, string group, string name,
            DataTable flatTable, string flatColumn, DataTable egrnTable, string egrnColumn, DataTable btiTable, string btiColumn,
            Type type = null, object mfcValue = null, DateTime? periodRegDate = null, bool showHistory = false)
        {
            List<ConsolidatedDataHistoryValueDto> flatValues = GetValues(flatTable, flatColumn, type);
            List<ConsolidatedDataHistoryValueDto> egrnValues = GetValues(egrnTable, egrnColumn, type);
            List<ConsolidatedDataHistoryValueDto> btiValues = GetValues(btiTable, btiColumn, type);
            int countHistory = Math.Max(Math.Max(flatValues?.Count ?? 0, egrnValues?.Count ?? 0), btiValues?.Count ?? 0);

            models.Add(new FlatConsolidatedDataDto
            {
                Group = group,
                Name = name,
                Source = (flatColumn.IsNotEmpty() && sources != null) ? sources.GetValueOrDefault(flatColumn, string.Empty) : string.Empty,
                Flat = new ConsolidatedDataValueDto
                {
                    Value = flatValues?.FirstOrDefault(x => x.To.Year == 9999)?.Value,
                    HistoryValues = flatValues?.OrderByDescending(x => x.To)?.ToList(),
                    HaveHistory = flatValues?.Where(x => x.To.Year != 9999)?.Count() > 0 && showHistory
                },
                Egrn = new ConsolidatedDataValueDto
                {
                    Value = egrnValues?.FirstOrDefault(x => x.To.Year == 9999)?.Value,
                    //HistoryValues = egrnValues?.Where(x => x.To.Year != 9999)?.OrderByDescending(x => x.To)?.ToList()
                },
                Bti = new ConsolidatedDataValueDto
                {
                    Value = btiValues?.FirstOrDefault(x => x.To.Year == 9999)?.Value,
                    // HistoryValues = btiValues?.Where(x => x.To.Year != 9999)?.OrderByDescending(x => x.To)?.ToList()
                },
                Mfc = new ConsolidatedDataValueDto
                {
                    Value = GetFormattedValue(mfcValue, type, true),
                    PeriodRegDate = periodRegDate,
                    // HistoryValues = new List<ConsolidatedDataHistoryValueDto>()
                },
                OrdinalNumber = models.Count
            });
        }

        private string GetValue(DataTable dataTable, string column, Type type = null)
        {
            if (column.IsEmpty())
            {
                return string.Empty;
            }

            string predicate = string.Empty;

            if (column != null && column.IndexOf('.') == 1)
            {
                predicate = column[0].ToString();
                column = column.Substring(2);
            }

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return string.Empty;
            }

            return type != null ? GetFormattedValue(dataTable.Rows[0][column], type) : dataTable.Rows[0][column].ToString();
        }

        /// <summary>
        /// Возвращает список моделей значений с датами изменений для колонки из DataTable
        /// </summary>
        /// <param name="dataTable">Исходная таблица</param>
        /// <param name="column">Название колонки</param>
        /// <param name="type">Тип данных для форматирования значение, если необходимо</param>
        private List<ConsolidatedDataHistoryValueDto> GetValues(DataTable dataTable, string column, Type type = null)
        {
            string predicate = string.Empty;

            if (column != null && column.IndexOf('.') == 1)
            {
                predicate = column[0].ToString();
                column = column.Substring(2);
            }

            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                return new List<ConsolidatedDataHistoryValueDto>();
            }

            var histories =  dataTable.AsEnumerable().Select(x =>
            {
                DateTime from = dataTable.Columns.Contains($"{predicate}S_") && x[$"{predicate}S_"] != DBNull.Value ? x[$"{predicate}S_"].ParseToDateTime() : DateTime.MinValue;
                DateTime to = dataTable.Columns.Contains($"{predicate}PO_") && x[$"{predicate}PO_"] != DBNull.Value ? x[$"{predicate}PO_"].ParseToDateTime() : DateTime.MaxValue;
                int? userId = dataTable.Columns.Contains($"{predicate}CHANGES_USER_ID") && x[$"{predicate}CHANGES_USER_ID"] != DBNull.Value ? x[$"{predicate}CHANGES_USER_ID"].ParseToInt() : -1;
                string username = (userId > 0) ? OMUser.Where(user => user.Id == userId).Select(user => user.Username).Execute().FirstOrDefault()?.Username : string.Empty;

                return new ConsolidatedDataHistoryValueDto
                {
                    From = from,
                    To = to,
                    Value = type != null ? GetFormattedValue(x[column], type) : x[column].ToString(),
                    ChangeUser = string.IsNullOrEmpty(username) ? "Система" : username
                };
            }).OrderBy(x=>x.To).ToList();

            var result = new List<ConsolidatedDataHistoryValueDto>();
            foreach (var history in histories)
            {
                bool contains = result.LastOrDefault()?.Value == history.Value;
                if (contains)
                {
                    result.LastOrDefault().To = history.To;
                }
                else
                {
                    result.Add(history);
                }
            }

            return result;


        }

        /// <summary>
        /// Возвращает отформатированное значение, согласно типу данных
        /// </summary>
        /// <param name="value">Исходное значение</param>
        /// <param name="type">Тип данных</param>
        private string GetFormattedValue(object value, Type type, bool mfc = false)
        {
            if (type == typeof(bool))
            {
                return value.ParseToBoolean() ? "ДА" : (mfc ? string.Empty : "НЕТ");
            }

            if (value == null || value == DBNull.Value)
            {
                return string.Empty;
            }

            if (type == typeof(DateTime))
            {
                DateTime dateTimeValue = value.ParseToDateTime();

                if (dateTimeValue == DateTime.MaxValue || dateTimeValue == DateTime.MinValue) return string.Empty;

                return dateTimeValue.ToString("dd.MM.yyyy", CultureRu);
            }

            if (type == typeof(decimal))
            {
                return value.ParseToDecimal().ToString("N2", CultureRu);
            }

            return value.ToString();
        }

        /// <summary>
        /// Возвращает площадь помещения по данным МФЦ, вычисляемую по формуле:
        /// - если fopl1+...fopln = fopl (объекта страхования в карточке ЖП), то fopl по данным МФЦ = fopl1...+fopln;
        /// - если fopl1+...+fopln != fopl (объекта страхования в карточке ЖП), то fopl по данным МФЦ = fopl1
        /// </summary>
        /// <param name="mfcSources">Список начислений для помещения</param>
        /// <param name="area">Площадь объекта страхования</param>
        private decimal? GetMfcFlatArea(List<OMInputNach> mfcSources, decimal? area)
        {
            OMInputNach mfcSource = mfcSources.FirstOrDefault();
            string flatType = mfcSource?.ParentFlatType?.Name?.ToLower();

            // CIPJS-486
            //if (flatType != "коммунальная квартира" && flatType != "отдельная квартира в долевой собственности")
            //    return mfcSource?.Fopl;

            decimal? fopl = mfcSources.Sum(x => x.Fopl);
            if (!area.HasValue || !fopl.HasValue) return fopl;
            else if (Math.Abs(area.Value - fopl.Value) < 0.001m) return fopl;
            else return mfcSource?.Fopl;
        }

        /// <summary>
        /// Возвращает площадь объекта страхования
        /// </summary>
        private decimal? GetFlatArea(DataTable data) => data
            .AsEnumerable()
            .OrderByDescending(x => (DateTime)x["PO_"])
            .Select(x => x["FOPL"] == DBNull.Value ? null : (decimal?)x["FOPL"])
            .FirstOrDefault();

        public IActionResult Monitoring()
        {
            return View();
        }

        public IActionResult Monitoring_Read()
        {
            List<MonitoringDto> models = new List<MonitoringDto>();
            DataTable egrnBuilding = GetEgrnBuilding();
            DataTable egrnFlat = GetEgrnFlat();
            DataTable btiBuilding = GetBtiBuilding();
            DataTable btiFlat = GetBtiFlat();
            DataTable building = GetBuilding();
            DataTable flat = GetFlat();

            models.Add(new MonitoringDto { GroupName = "Общие:" });
            models.Add(new MonitoringDto
            {
                OrdinalNumber = 1,
                Name = "Обновлено объектов всего",
                EgrnBuilding = egrnBuilding.Rows.Count,
                EgrnFlat = egrnFlat.Rows.Count,
                BtiBuilding = btiBuilding.Rows.Count,
                BtiFlat = btiFlat.Rows.Count,
                Building = building.Rows.Count,
                Flat = flat.Rows.Count
            });
            models.Add(new MonitoringDto
            {
                OrdinalNumber = 2,
                Name = "Общая площадь",
                EgrnBuilding = egrnBuilding.FilteringAndSortingTable("AREA = 0").Rows.Count,
                EgrnFlat = egrnFlat.FilteringAndSortingTable("AREA = 0").Rows.Count,
                BtiBuilding = btiBuilding.FilteringAndSortingTable("OPL = 0").Rows.Count,
                BtiFlat = btiFlat.FilteringAndSortingTable("TOTAL_AREA = 0").Rows.Count,
                Building = building.FilteringAndSortingTable("OPL = 0").Rows.Count,
                Flat = flat.FilteringAndSortingTable("OPL = 0").Rows.Count
            });
            models.Add(new MonitoringDto
            {
                OrdinalNumber = 3,
                Name = "Назначение объекта",
                EgrnBuilding = egrnBuilding.FilteringAndSortingTable("ASSIGNATION_NAME_ID = 0").Rows.Count,
                EgrnFlat = egrnFlat.FilteringAndSortingTable("ASSIGNATION_NAME_ID = 0").Rows.Count,
                BtiBuilding = btiBuilding.FilteringAndSortingTable("NAZ_CODE = 0").Rows.Count,
                Building = building.FilteringAndSortingTable("PURPOSE_NAME_CODE = 0").Rows.Count
            });

            models.Add(new MonitoringDto { GroupName = "Индивидуальные:" });
            models.Add(new MonitoringDto
            {
                OrdinalNumber = 4,
                Name = "Класс объекта",
                BtiBuilding = btiBuilding.FilteringAndSortingTable("KL_CODE = 0").Rows.Count,
                BtiFlat = btiFlat.FilteringAndSortingTable("CLASS_ID = 0").Rows.Count,
                Flat = flat.FilteringAndSortingTable("KLASS_FLAT_CODE = 0").Rows.Count
            });
            models.Add(new MonitoringDto
            {
                OrdinalNumber = 5,
                Name = "Состояние стат. карты",
                BtiBuilding = btiBuilding.FilteringAndSortingTable("SOST_CODE = 0").Rows.Count,
                Building = building.FilteringAndSortingTable("STATUS_SOST_BTI_CODE = 0").Rows.Count
            });
            models.Add(new MonitoringDto
            {
                OrdinalNumber = 6,
                Name = "Аварийность здания",
                BtiBuilding = btiBuilding.FilteringAndSortingTable("AVARZD = 0").Rows.Count
            });
            models.Add(new MonitoringDto
            {
                OrdinalNumber = 7,
                Name = "Количество квартир",
                BtiBuilding = btiBuilding.FilteringAndSortingTable("KWQ = 0").Rows.Count,
                Building = building.FilteringAndSortingTable("KOL_GP = 0").Rows.Count
            });
            models.Add(new MonitoringDto
            {
                OrdinalNumber = 8,
                Name = "Тип ЖП",
                BtiFlat = btiFlat.FilteringAndSortingTable("TYPE_ID = 0").Rows.Count,
                Flat = flat.FilteringAndSortingTable("TYPE_FLAT_CODE = 0").Rows.Count
            });
            models.Add(new MonitoringDto
            {
                OrdinalNumber = 9,
                Name = "Исключено из программы страхования",
                Building = building.FilteringAndSortingTable("FLAG_INSUR_CALCULATED = -1").Rows.Count,
                Flat = flat.FilteringAndSortingTable("FLAG_INSUR = -1").Rows.Count
            });
            models.Add(new MonitoringDto
            {
                OrdinalNumber = 10,
                Name = "Включено в программу страхования",
                Building = building.FilteringAndSortingTable("FLAG_INSUR_CALCULATED = 1").Rows.Count,
                Flat = flat.FilteringAndSortingTable("FLAG_INSUR = 1").Rows.Count
            });

            return Content(JsonConvert.SerializeObject(models), "application/json");
        }

        private DataTable GetEgrnBuilding()
        {
            string commandText =
@"SELECT C.EMP_ID,
	CASE WHEN COALESCE(C.AREA,0)=COALESCE(P.AREA,0) THEN 1 ELSE 0 END AS AREA,
    CASE WHEN COALESCE(C.ASSIGNATION_NAME_ID,0)=COALESCE(P.ASSIGNATION_NAME_ID,0) THEN 1 ELSE 0 END AS ASSIGNATION_NAME_ID
FROM EHD_BUILD_PARCEL_Q C
LEFT JOIN EHD_BUILD_PARCEL_Q P ON P.EMP_ID=C.EMP_ID AND P.S_=
	(SELECT MAX(D.S_) FROM INSUR_FLAT_Q D WHERE D.EMP_ID=C.EMP_ID AND D.ACTUAL=0)
WHERE DATE_PART('DAY',NOW()-C.S_)=1 AND C.ACTUAL=1 AND LOWER(C.TYPE)='здание' AND LOWER(C.NAME)='жилой дом'";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        private DataTable GetEgrnFlat()
        {
            string commandText =
@"SELECT C.EMP_ID,
	CASE WHEN COALESCE(C.AREA,0)=COALESCE(P.AREA,0) THEN 1 ELSE 0 END AS AREA,
    CASE WHEN COALESCE(C.ASSIGNATION_NAME_ID,0)=COALESCE(P.ASSIGNATION_NAME_ID,0) THEN 1 ELSE 0 END AS ASSIGNATION_NAME_ID
FROM EHD_BUILD_PARCEL_Q C
LEFT JOIN EHD_BUILD_PARCEL_Q P ON P.EMP_ID=C.EMP_ID AND P.S_=
	(SELECT MAX(D.S_) FROM EHD_BUILD_PARCEL_Q D WHERE D.EMP_ID=C.EMP_ID AND D.ACTUAL=0)
WHERE DATE_PART('DAY',NOW()-C.S_)=1 AND C.ACTUAL=1 AND LOWER(C.TYPE)='помещение' AND LOWER(C.NAME)='помещение'";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        private DataTable GetBtiBuilding()
        {
            string commandText =
@"SELECT C.EMP_ID,
	CASE WHEN COALESCE(C.OPL,-1)=COALESCE(P.OPL,-1) THEN 1 ELSE 0 END AS OPL,
    CASE WHEN COALESCE(C.NAZ_CODE,0)=COALESCE(P.NAZ_CODE,0) THEN 1 ELSE 0 END AS NAZ_CODE,
    CASE WHEN COALESCE(C.KL_CODE,0)=COALESCE(P.KL_CODE,0) THEN 1 ELSE 0 END AS KL_CODE,
    CASE WHEN COALESCE(C.SOST_CODE,0)=COALESCE(P.SOST_CODE,0) THEN 1 ELSE 0 END AS SOST_CODE,
    CASE WHEN COALESCE(C.AVARZD,-1)=COALESCE(P.AVARZD,-1) THEN 1 ELSE 0 END AS AVARZD,
    CASE WHEN COALESCE(C.KWQ,-1)=COALESCE(P.KWQ,-1) THEN 1 ELSE 0 END AS KWQ
FROM BTI_BUILDING_Q C
LEFT JOIN BTI_BUILDING_Q P ON P.EMP_ID=C.EMP_ID AND P.S_=
	(SELECT MAX(D.S_) FROM BTI_BUILDING_Q D WHERE D.EMP_ID=C.EMP_ID AND D.ACTUAL=0)
WHERE DATE_PART('DAY',NOW()-C.S_)=1 AND C.ACTUAL=1";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        private DataTable GetBtiFlat()
        {
            string commandText =
@"SELECT C.EMP_ID,
    CASE WHEN COALESCE(C.TOTAL_AREA,-1)=COALESCE(P.TOTAL_AREA,-1) THEN 1 ELSE 0 END AS TOTAL_AREA,
    CASE WHEN COALESCE(C.CLASS_ID,0)=COALESCE(P.CLASS_ID,0) THEN 1 ELSE 0 END AS CLASS_ID,
    CASE WHEN COALESCE(C.TYPE_ID,0)=COALESCE(P.TYPE_ID,0) THEN 1 ELSE 0 END AS TYPE_ID
FROM BTI_PREMASE C
LEFT JOIN BTI_PREMASE P ON P.EMP_ID=C.EMP_ID AND P.S_=
    (SELECT MAX(D.S_) FROM BTI_PREMASE D WHERE D.EMP_ID=C.EMP_ID AND D.ACTUAL=0)
WHERE DATE_PART('DAY',NOW()-C.S_)=1 AND C.ACTUAL=1";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        private DataTable GetBuilding()
        {
            string commandText =
@"SELECT C.EMP_ID,
	CASE WHEN COALESCE(C.OPL,-1)=COALESCE(P.OPL,-1) THEN 1 ELSE 0 END AS OPL,
    CASE WHEN COALESCE(C.PURPOSE_NAME_CODE,0)=COALESCE(P.PURPOSE_NAME_CODE,0) THEN 1 ELSE 0 END AS PURPOSE_NAME_CODE,
    CASE WHEN COALESCE(C.STATUS_SOST_BTI_CODE,0)=COALESCE(P.STATUS_SOST_BTI_CODE,0) THEN 1 ELSE 0 END AS STATUS_SOST_BTI_CODE,
    CASE WHEN COALESCE(C.KOL_GP,-1)=COALESCE(P.KOL_GP,-1) THEN 1 ELSE 0 END AS KOL_GP,
    CASE WHEN (C.FLAG_INSUR_CALCULATED=1 AND P.FLAG_INSUR_CALCULATED=0) THEN 1 ELSE 
     (CASE WHEN (C.FLAG_INSUR_CALCULATED=0 AND P.FLAG_INSUR_CALCULATED=1) THEN -1 ELSE 0 END) END AS FLAG_INSUR_CALCULATED
FROM INSUR_BUILDING_Q C
LEFT JOIN INSUR_BUILDING_Q P ON P.EMP_ID=C.EMP_ID AND P.S_=
	(SELECT MAX(D.S_) FROM INSUR_BUILDING_Q D WHERE D.EMP_ID=C.EMP_ID AND D.ACTUAL=0)
WHERE DATE_PART('DAY',NOW()-C.S_)=1 AND C.ACTUAL=1";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        private DataTable GetFlat()
        {
            string commandText =
@"SELECT C.EMP_ID, 
	CASE WHEN COALESCE(C.OPL,-1)=COALESCE(P.OPL,-1) THEN 1 ELSE 0 END AS OPL,
    CASE WHEN COALESCE(C.KLASS_FLAT_CODE,0)=COALESCE(P.KLASS_FLAT_CODE,0) THEN 1 ELSE 0 END AS KLASS_FLAT_CODE,
    CASE WHEN COALESCE(C.TYPE_FLAT_CODE,0)=COALESCE(P.TYPE_FLAT_CODE,0) THEN 1 ELSE 0 END AS TYPE_FLAT_CODE,
    CASE WHEN(C.FLAG_INSUR=1 AND P.FLAG_INSUR=0) THEN 1 ELSE 
     (CASE WHEN(C.FLAG_INSUR=0 AND P.FLAG_INSUR=1) THEN - 1 ELSE 0 END) END AS FLAG_INSUR
FROM INSUR_FLAT_Q C
LEFT JOIN INSUR_FLAT_Q P ON P.EMP_ID=C.EMP_ID AND P.S_=
 (SELECT MAX(D.S_) FROM INSUR_FLAT_Q D WHERE D.EMP_ID=C.EMP_ID AND D.ACTUAL=0)
WHERE DATE_PART('DAY',NOW()-C.S_)=1 AND C.ACTUAL=1";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }
    }
}