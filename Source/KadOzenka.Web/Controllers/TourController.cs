using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Transactions;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register;
using Core.Register.Enums;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.CalculateSystem;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Tours.Dto;
using KadOzenka.Dal.Tours.Repositories;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.Tour;
using KadOzenka.Web.Models.Tour.EstimateGroup;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.Register;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.KO;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
    public class TourController : KoBaseController
    {
        public ITourService TourService { get; set; }
        public GroupService GroupService { get; set; }
        public TourFactorService TourFactorService { get; set; }
        public IGbuObjectService GbuObjectService { get; set; }
        public TourComplianceImportService TourComplianceImportService { get; set; }
        public GroupFactorService GroupFactorService { get; set; }

        public TourController(ITourService tourService, IGbuObjectService gbuObjectService)
        {
            TourFactorService = new TourFactorService();
            GroupService = new GroupService();
            TourService = tourService;
            GbuObjectService = gbuObjectService;
            TourComplianceImportService = new TourComplianceImportService();
            GroupFactorService = new GroupFactorService();
        }

        #region Карточка тура

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult TourCard(long? tourId, long? parentGroupId, long? groupId)
        {
            //нужно чтобы при обновлении данных поставить фокус на нужный элемент в дереве
            ViewBag.ChangedTourId = tourId ?? 0;
            ViewBag.ChangedGroupParentGroupId = parentGroupId ?? 0;
            ViewBag.ChangedGroupId = groupId ?? 0;
            return View();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GetAllTours()
        {
            var tours = OMTour.Where(x => true).OrderBy(x => x.Year).SelectAll().Execute();

            var models = tours.Select(tour => new GroupTreeModel
            {
                Id = tour.Id,
                GroupName = tour.Year.ToString(),
                UrlForEdit = Url.Action("TourSubCard", "Tour", new { tourId = tour.Id }),
                GroupType = GroupType.Undefined,
                Items = new List<GroupTreeModel>
                {
                    //нужно, чтобы у узла в UI была стрелка
                    new GroupTreeModel
                    {
                        GroupName = string.Empty
                    }
                }
            }).AsEnumerable();

            return Json(models);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult TourSubCard(long tourId)
        {
            var tour = TourService.GetTourById(tourId);
            var tourModel = TourModel.ToModel(tour);

            return PartialView("~/Views/Tour/Partials/TourSubCard.cshtml", tourModel);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult GroupSubCard(long groupId, long tourId)
        {
            var groupDto = new GroupDto {Id = groupId};
            var isReadOnly = false;
            switch (groupId)
            {
                case (long)KoGroupAlgoritm.MainParcel:
                    groupDto.Name = KoGroupAlgoritm.MainParcel.GetEnumDescription();
                    groupDto.GroupType = GroupType.Main;
                    isReadOnly = true;
                    break;

                case (long)KoGroupAlgoritm.MainOKS:
                    groupDto.Name = KoGroupAlgoritm.MainOKS.GetEnumDescription();
                    groupDto.GroupType = GroupType.Main;
                    isReadOnly = true;
                    break;

                default:
                    groupDto = GroupService.GetGroupById(groupId);
                    break;
            }

            groupDto.RatingTourId = tourId;
            var groupModel = GroupModel.ToModel(groupDto);
            groupModel.IsReadOnly = isReadOnly;

            groupModel.Models = OMModel.Where(x => x.GroupId == groupId)
                .OrderByDescending(x => x.IsActive.Coalesce(false)).OrderBy(x => x.Name)
                .Select(x => new
                {
                    x.Id,
                    x.Name
                })
                .Execute()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

            return PartialView("~/Views/Tour/Partials/GroupSubCard.cshtml", groupModel);
        }

        #endregion

        #region Настройка отношения групп и сегментов рынка

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult GroupSegmentSettingsSubCard(long groupId)
        {
            var settings = GroupService.GetGroupToMarketSegmentRelation(groupId);
            var model = settings == null
                ? new GroupSegmentSettingsModel()
                : GroupSegmentSettingsModel.FromEntity(settings);

            return PartialView("~/Views/Tour/Partials/GroupSegmentSettingsSubCard.cshtml", model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GroupSegmentSettingsSubCard(GroupSegmentSettingsModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            GroupService.UpdateGroupToMarketSegmentRelation(model.GroupId, model.MarketSegment, model.TerritoryType);

            return new JsonResult(new { Message = "Обновление выполнено" });
        }

        #endregion

        #region Настройка для разъяснений

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult GroupExplanationSettingsSubCard(long groupId)
        {
            var settings = GroupService.GetGroupExplanationSettings(groupId);
            return PartialView("~/Views/Tour/Partials/GroupExplanationSettingsSubCard.cshtml", GroupExplanationSettingsModel.FromDto(settings));
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GroupExplanationSettingsSubCard(GroupExplanationSettingsModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            GroupService.UpdateGroupExplanationSettings(model.ToDto());

            return new JsonResult(new { Message = "Обновление выполнено" });
        }

        #endregion Настройка для разъяснений

        #region Настройка для акта определения

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult GroupCadastralCostDefinitionActSettingsSubCard(long groupId)
        {
            var settings = GroupService.GetGroupCadastralCostDefinitionActSettings(groupId);
            return PartialView("~/Views/Tour/Partials/GroupCadastralCostDefinitionActSettingsSubCard.cshtml", GroupCadastralCostDefinitionActSettingsModel.FromDto(settings));
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GroupCadastralCostDefinitionActSettingsSubCard(GroupCadastralCostDefinitionActSettingsModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            GroupService.UpdateGroupCadastralCostDefinitionActSettings(model.ToDto());

            return new JsonResult(new { Message = "Обновление выполнено" });
        }

        #endregion Настройка для акта определения

        #region Настройки атрибутов тура
        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public ActionResult AddGroup(long tourId, long complianceId)
        {
            var compliance = OMComplianceGuide.Where(x => x.Id == complianceId).SelectAll().ExecuteFirstOrDefault();
            var model = new AddGroupViewModel();

            if (compliance != null)
            {
                model = AddGroupViewModel.ToModel(compliance);
            }

            model.TourId = tourId;
            return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public JsonResult AddGroup(AddGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }
            OMComplianceGuide compliance = null;

            if (model.Id != -1)
            {
                compliance = OMComplianceGuide.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();
            }

            try
            {
                compliance = AddGroupViewModel.ToEntity(compliance ?? new OMComplianceGuide(), model);
                var id = compliance.Save();

                return Json(new { message = "Сохранение выполненно успешно" , id});
            }
            catch (Exception e)
            {
                ErrorManager.LogError(e);
                Console.WriteLine(e);
                return SendErrorMessage("При сохранении возникли ошибки. Подробнее в журнале ошибок.");
            }

        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public ActionResult RemoveGroup(long complianceId)
        {
            var compliance = OMComplianceGuide.Where(x => x.Id == complianceId).SelectAll().ExecuteFirstOrDefault();
            return View(AddGroupViewModel.ToModel(compliance));
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public ActionResult RemoveGroup(AddGroupViewModel model)
        {
            if (model.Id == -1)
            {
                return SendErrorMessage("Не выбрана запись");
            }
            var compliance = OMComplianceGuide.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();

            compliance.Destroy();

            return Json(new { Success = true });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public ActionResult RemoveGroupsByTour(long tourId)
        {
            if (tourId == 0)
            {
                throw new Exception("Не задан тур");
            }
            ViewBag.TourId = tourId;
            return View();

        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public ActionResult RemoveGroupsByTour(AddGroupViewModel model)
        {
            if (model.TourId == 0)
            {
                return SendErrorMessage("Не задан Тур");
            }
            var compliance = OMComplianceGuide.Where(x => x.TourId == model.TourId).SelectAll().Execute();

            try
            {
                using (var ts = new TransactionScope())
                {
                    foreach (var item in compliance)
                    {
                        item.Destroy();
                    }
                    ts.Complete();
                }

            }
            catch (Exception e)
            {
                ErrorManager.LogError(e);
                Console.WriteLine(e);
                return SendErrorMessage("Во время удаления произошла ошибка. Подробнее в журнале ошибок");
            }

            return Json(new { Success = true });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public ActionResult DataImport(long tourId)
        {
            var model = new ImportGroupViewModel {TourId = tourId};
            return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public ActionResult DataImport(IFormFile file, ImportGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }

            try
            {
                var importFileDto = new ImportFileComplianceDto
                {
                    FileName = file.FileName,
                    CodeColumnName = model.CodeColumnName,
                    GroupColumnName = model.GroupColumnName,
                    RoomTypeColumnName = model.RoomTypeColumnName,
                    TerritoryTypeColumnName = model.TerritoryTypeColumnName
                };

                using (Stream stream = file.OpenReadStream())
                {
                    TourComplianceImportService.ImportComplianceFromFile(stream, importFileDto, model.ObjectType, model.TourId.GetValueOrDefault());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ErrorManager.LogError(e);
                return SendErrorMessage(e.Message);
            }
            return Json(new {message = "Данные успешно загружены"});
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public ActionResult TourAttributeSettings()
        {
            ViewData["TreeAttributes"] = GbuObjectService.GetGbuAttributesTree()
             .Select(x => new DropDownTreeItemModel
             {
                 Value = Guid.NewGuid().ToString(),
                 Text = x.Text,
                 Items = x.Items.Select(y => new DropDownTreeItemModel
                 {
                     Value = y.Value,
                     Text = y.Text
                 }).ToList()
             }).AsEnumerable();

            return View(new TourAttributeSettingsModel());
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public JsonResult GetTourGbuAttributeSettings(long tourId)
        {
            var models = TourFactorService.GetTourAttributesFromSettings(tourId);

            return Json(new {Data = models});
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ATTRIBUTE_SETTINGS)]
        public ActionResult TourAttributeSettings(TourAttributeSettingsModel model)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }

            try
            {
                TourService.UpdateTourAttributeSettings(new TourAttributeSettingsDto
                {
                    TourId = model.TourId.Value,
                    AttributeId = model.CodeGroupAttributeId,
                    KoAttributeUsingType = KoAttributeUsingType.CodeGroupAttribute
                });

                TourService.UpdateTourAttributeSettings(new TourAttributeSettingsDto
                {
                    TourId = model.TourId.Value,
                    AttributeId = model.CodeQuarterAttributeId,
                    KoAttributeUsingType = KoAttributeUsingType.CodeQuarterAttribute
                });

                TourService.UpdateTourAttributeSettings(new TourAttributeSettingsDto
                {
                    TourId = model.TourId.Value,
                    AttributeId = model.TerritoryTypeAttributeId,
                    KoAttributeUsingType = KoAttributeUsingType.TerritoryTypeAttribute
                });
            }
            catch (Exception e)
            {
                return SendErrorMessage(e.Message);
            }

            return Json(new { Success = "Сохранено успешно" });
        }

        #endregion Настройки атрибутов тура

        #region Туры

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ESTIMATES)]
        public IActionResult TourEstimates()
        {
            return View();
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ESTIMATES)]
        public IActionResult TourEstimates(TourModel tourModel)
        {
            if (tourModel == null)
                throw new Exception("Не передана модель для сохранения Тура");

            var tourDto = TourModel.FromModel(tourModel);

            var id = tourModel.Id == -1 
                ? TourService.AddTour(tourDto) 
                : TourService.UpdateTour(tourDto);

            return Json(new { Id = id });
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public IActionResult CanTourBeDeleted(long id)
        {
	        var canTourBeDeleted = TourService.CanTourBeDeleted(id);
	        return Json(new { CanBeDeleted = canTourBeDeleted });
        }

        [HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ESTIMATES)]
        public IActionResult TourEstimates(int id)
        {
	        TourService.DeleteTour(id);
            return Json(new { Success = "Удаление выполнено" });
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ESTIMATES)]
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

        #endregion

        #region Группы/подгруппы

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public ActionResult Groups()
        {
            return View();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public JsonResult GetGroups()
        {
            var groups = GroupService.GetGroups();

            var groupModels = new List<GroupTreeModel>();
            groups.ForEach(x =>
            {
                groupModels.Add(GroupTreeModel.ToModel(x, Url));
            });

            return Json(groupModels);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public JsonResult EditGroup(GroupModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var groupDto = GroupModel.FromModel(model);

            int id;
            if (model.Id.HasValue)
                id = GroupService.UpdateGroup(groupDto);
            else
                id = GroupService.AddGroup(groupDto);

            return Json(new {Id = id});
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public IActionResult CanGroupBeDeleted(long id)
        {
	        var canGroupBeDeleted = GroupService.CanGroupBeDeleted(id);
	        return Json(new { CanBeDeleted = canGroupBeDeleted });
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public IActionResult CanGroupsBeDeleted(long tourId, bool isOks)
        {
	        var canGroupsBeDeleted = GroupService.CanGroupsBeDeleted(tourId, isOks);
	        return Json(new { CanBeDeleted = canGroupsBeDeleted });
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public IActionResult DeleteGroup(long id)
        {
	        GroupService.DeleteGroup(id);
	        return Json(new { Success = "Удаление выполнено" });
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public IActionResult DeleteGroups(long tourId, bool isOks)
        {
	        GroupService.DeleteGroups(tourId, isOks);
	        return Json(new { Success = "Удаление выполнено" });
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public JsonResult GetRatingTours()
        {
            var tours = OMTour.Where(x => true).OrderBy(x => x.Year).SelectAll().Execute()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Year.ToString()
                });

            return Json(tours);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public JsonResult GetAllGroups()
        {
            var groups = OMGroup.Where(x => true).SelectAll().Execute()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{GroupService.ParseGroupNumber(x.ParentId, x.Number)}. {x.GroupName}"
                });

            return Json(groups);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public JsonResult GetParentGroup(ObjectTypeExtended type, long? tourId)
        {
            if(tourId == null)
                return Json(new List<SelectListItem>());

            var groupAlgorithm = type == ObjectTypeExtended.Oks ? KoGroupAlgoritm.MainOKS : KoGroupAlgoritm.MainParcel;

            var allGroups = GroupService.GetGroupsTreeForTour(tourId.Value);

            var groups = allGroups.Where(x => x.Id == (int)groupAlgorithm)
                .SelectMany(x => x.Items)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.GroupName
                });

            return Json(groups);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public JsonResult GetSubgroup(long? groupId, long? tourId)
        {
            if (groupId == null || tourId == null)
            {
                return Json(new List<SelectListItem> { });
            }

            var allGroups = GroupService.GetGroupsTreeForTour(tourId.Value);
            var subGroups = allGroups.SelectMany(x => x.Items).Where(x => x.Id == groupId).SelectMany(x => x.Items);

            var groups = subGroups
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.GroupName
                });

            return Json(groups);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public JsonResult GetGroupingMechanism(bool parentIsSet)
        {
            var algotitmItems = Core.RefLib.ReferencesCommon.GetItems(204);

            if (parentIsSet)
            {
                algotitmItems = algotitmItems.Where(x => x.ItemId != (long)KoGroupAlgoritm.MainOKS
                    && x.ItemId != (long)KoGroupAlgoritm.MainParcel && x.Code != null).ToList();
            }
            else
            {
                algotitmItems = algotitmItems.Where(x => x.ItemId == (long)KoGroupAlgoritm.MainOKS
                    || x.ItemId == (long)KoGroupAlgoritm.MainParcel).ToList();
            }

            var mechanism = algotitmItems.Select(x => new SelectListItem
            {
                Value = x.ItemId.ToString(),
                Text = x.Value
            });

            return Json(mechanism);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public JsonResult GetGroupsByTasks(List<long> taskIds)
        {
            var groups = GroupService.GetGroupsByTasks(taskIds);

            var items = groups.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.GroupName
            });

            return Json(items);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public JsonResult GetSortedGroupsWithNumbersByTasks(List<long> taskIds)
        {
            var groups = GroupService.GetSortedGroupsWithNumbersByTasks(taskIds);

            var items = groups.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.CombinedName
            });

            return Json(items);
        }

        #region Факторы группы

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public ActionResult GroupFactors(long groupId)
        {
            ViewBag.GroupId = groupId;
            return PartialView("~/Views/Tour/Partials/GroupFactors.cshtml");
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public JsonResult GetGroupFactors(long groupId)
        {
            var models = GroupFactorService.GetGroupFactors(groupId).Select(GroupFactorModel.FromDto).ToList();
            return Json(models);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public JsonResult GetTourFactors(long? groupId)
        {
            OMTourGroup tourGroup = OMTourGroup.Where(x => x.GroupId == groupId)
                .Select(x => x.TourId).ExecuteFirstOrDefault();

            if (tourGroup != null)
            {
                var tourAllAttributes = TourFactorService.GetTourAllAttributes(tourGroup.TourId);
                var result = tourAllAttributes.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).OrderBy(x => x.Text);

                return Json(result);
            }

            return Json(new List<SelectListItem>());
        }


        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public ActionResult EditGroupFactor(long? id, long groupId)
        {
            GroupFactorModel model;

            if (id.HasValue)
            {
                var dto = GroupFactorService.GetGroupFactor(id.Value);
                model = GroupFactorModel.FromDto(dto);
            }
            else
            {
                model = new GroupFactorModel(groupId);
            }

            return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public ActionResult EditGroupFactor(GroupFactorModel model)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }

            if (model.Id == -1)
            {
                var id = GroupFactorService.CreateGroupFactor(model.ToDto());
                model.Id = id;
            }
            else
            {
                GroupFactorService.UpdateGroupFactor(model.ToDto());
            }

            return Json(new { Success = "Сохранено успешно", Id = model.Id });
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public ActionResult DeleteGroupFactor(long id)
        {
            GroupFactorService.DeleteGroupFactor(id);
            return Json(new { Success = "Удаление выполненно" });
        }

        #endregion Факторы группы

        #region Импорт группы из Excel

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_IMPORT_GROUP_DATA_FROM_EXCEL)]
        public ActionResult ImportDataFromExcel()
        {
            return View(new ImportDataModel());
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_IMPORT_GROUP_DATA_FROM_EXCEL)]
        public ActionResult ImportDataFromExcel(ImportDataModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }

            object returnedData;
            try
            {
                using (var stream = viewModel.File.OpenReadStream())
                {
                    var settings = viewModel.ToDto();
                    if (DataImporterKO.UseLongProcessForImportDataGroup(stream))
                    {
                        ImportDataFromExcelLongProcess.AddProcessToQueue(stream, settings);

                        returnedData = new
                        {
                            isLongProcess = true
                        };
                    }
                    else
                    {
                        var importId = DataImporterKO.ImportDataFromExcel(stream, settings);

                        returnedData = new
                        {
                            isLongProcess = false,
                            importId
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return BadRequest();
            }

            return Content(JsonConvert.SerializeObject(returnedData), "application/json");
        }

        #endregion

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GetTours()
        {
            var tours = OMTour.Where(x => true).SelectAll().Execute()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Year.ToString()
                }).ToList();

            return Json(tours);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GetGroupsForTour(long tourId)
        {
            var groups = GroupService.GetGroupsTreeForTour(tourId, true);

            var models = groups.Select(x => GroupTreeModel.ToModel(x, Url)).ToList();

            return Json(models);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GetGroupsByTourAndMainGroup(long tourId, bool isParcel)
        {
            var allGroups = GroupService.GetGroupsTreeForTour(tourId);

            var mainGroupId = isParcel ? (long)KoGroupAlgoritm.MainParcel : (long)KoGroupAlgoritm.MainOKS;
            var mainGroups = allGroups.Where(x => x.Id == mainGroupId);
            var groups = mainGroups.SelectMany(x => x.Items);
            var models = groups.Where(x => x.Items?.Count > 0).Select(x => GroupTreeModel.ToModel(x, Url)).ToList();

            return Json(models);
        }

        #endregion

        #region Факторы

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult TourZuFactorsCard(long tourId)
        {
            var model = new TourFactorsModel
            {
                TourId = tourId,
                IsSteadObjectType = true
            };

            var existedTourFactorRegisters = OMTourFactorRegister
                .Where(x => x.TourId == tourId && x.ObjectType_Code == PropertyTypes.Stead)
                .SelectAll().Execute();
            if (existedTourFactorRegisters.Count != 0)
            {
                model.RegisterFactorId = existedTourFactorRegisters.First().RegisterId;
            }

            return View("TourFactorsCard", model);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult TourOksFactorsCard(long tourId)
        {
            var model = new TourFactorsModel
            {
                TourId = tourId,
                IsSteadObjectType = false
            };

            var existedTourFactorRegisters = OMTourFactorRegister
                .Where(x => x.TourId == tourId && x.ObjectType_Code != PropertyTypes.Stead)
                .SelectAll().Execute();
            if (existedTourFactorRegisters.Count != 0)
            {
                model.RegisterFactorId = existedTourFactorRegisters.First().RegisterId;
            }

            return View("TourFactorsCard", model);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult TourFactorObjectCard(long id, long tourId, bool isSteadObjectType, long registerFactorId)
        {
            var omAttribute = OMAttribute.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            var omRegister = OMRegister.Where(x => x.RegisterId == registerFactorId).Select(x => x.RegisterId).ExecuteFirstOrDefault();

            var model = new TourFactorObjectModel
            {
                Id = -1,
                TourId = tourId,
                IsSteadObjectType = isSteadObjectType,
                RegisterFactorId = omRegister?.RegisterId
            };
            if (omAttribute != null)
            {
                model.Id = omAttribute.Id;
                model.Name = omAttribute.Name;
                model.Type = omAttribute.ReferenceId.HasValue
                    ? RegisterAttributeType.REFERENCE
                    : (RegisterAttributeType)omAttribute.Type;
                model.ReferenceId = omAttribute.ReferenceId;
            }

            return View(model);
        }

        [HttpPost]
        [JsonExceptionHandler(Message = "Фактор с данным названием уже существует")]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult EditTourFactorObjectCard(TourFactorObjectModel model)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }

            var id = model.Id;
            using (var ts = new TransactionScope())
            {
                var omRegister = OMRegister.Where(x => x.RegisterId == model.RegisterFactorId).Select(x => x.RegisterId).ExecuteFirstOrDefault();
                if (omRegister == null)
                {
                    omRegister = TourFactorService.CreateTourFactorRegister(model.TourId, model.IsSteadObjectType);
                    model.RegisterFactorId = omRegister.RegisterId;
                }

                if (model.Id == -1)
                {
                    model.Id = TourFactorService.CreateTourFactorRegisterAttribute(model.Name, omRegister.RegisterId, model.Type, model.ReferenceId);
                }
                else
                {
                    TourFactorService.RenameTourFactorRegisterAttribute(id, model.Name);
                }

                ts.Complete();
            }

            return Json(new { Success = "Сохранено успешно", data = model });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public IActionResult DeleteTourFactorObject(long id)
        {
            var omAttribute = OMAttribute.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            if (omAttribute == null)
            {
                throw new Exception($"Фактор с ИД {id} не найден");
            }

            var model = new TourFactorObjectModel
            {
                Id = omAttribute.Id,
                Name = omAttribute.Name
            };

            return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public IActionResult DeleteTourFactorObject(TourFactorObjectModel model)
        {
            var omAttribute = OMAttribute.Where(x => x.Id == model.Id).SelectAll().ExecuteFirstOrDefault();
            if (omAttribute == null)
            {
                throw new Exception($"Фактор с ИД {model.Id} не найден");
            }

            TourFactorService.RemoveTourFactorRegisterAttribute(omAttribute.Id);

            return EmptyResponse();
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
        public JsonResult GetTourFactorsByType(long? tourId, ObjectTypeExtended type)
        {
            if (tourId == null)
                return Json(new List<SelectListItem>());

            var tourFactors = TourFactorService.GetTourAttributes(tourId.Value, type);

            var result = tourFactors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            return Json(result);
        }

        #endregion

        #region Зависимости при расчете подгрупп

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GetCalcGroups(long groupId)
        {
            var groupList = OMCalcGroup.Where(x => x.GroupId == groupId)
                .SelectAll().Execute();

            var groupIds = groupList.Select(x => x.ParentCalcGroupId).ToList();

            if (groupIds.Count == 0)
                return Json(string.Empty);

            var groups = OMGroup.Where(x => groupIds.Contains(x.Id))
                .Select(x => x.GroupName).Select(x => x.Number).Execute();

            var result = groupList.Join(groups,
                calc => calc.ParentCalcGroupId,
                group => group.Id,
                (calc, group) => new ParentCalcGroupModel
                {
                    Id = calc.Id,
                    GroupId = calc.GroupId,
                    ParentCalcGroupId = calc.ParentCalcGroupId,
                    Title = $"{group.Number}. {group.GroupName}"
                }).ToList();

            return Json(result);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GetSubgroups(long groupId)
        {
            var dtos = GroupService.GetOtherGroupsFromTreeLevelForTour(groupId);
            var res = dtos.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.CombinedName });

            return Json(res);
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult EditCalcGroup(long? id, long groupId)
        {
            OMCalcGroup calcGroup;

            if (id.HasValue)
            {
                calcGroup = OMCalcGroup.Where(x => x.Id == id)
                    .SelectAll().ExecuteFirstOrDefault();

                if (calcGroup == null)
                {
                    throw new Exception("Не найдена строка реестра расчета подгрупп с Id " + id);
                }
            }
            else
            {
                calcGroup = new OMCalcGroup
                {
                    GroupId = groupId
                };
            }

            return View(calcGroup);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult EditCalcGroup(OMCalcGroup calcGroup)
        {
            bool isExists = OMCalcGroup.Where(x => x.GroupId == calcGroup.GroupId && x.ParentCalcGroupId == calcGroup.ParentCalcGroupId)
                .ExecuteExists();

            if (isExists)
                throw new Exception("Данная группа уже была выбрана ранее");

            calcGroup.Save();
            return Json(new { Success = "Изменения успешно сохранены" });
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult DeleteCalcGroup(long id)
        {
            OMCalcGroup calcGroup = OMCalcGroup.Where(x => x.Id == id)
                .SelectAll().ExecuteFirstOrDefault();

            if (calcGroup == null)
            {
                throw new Exception("Не найдена строка реестра расчета подгрупп с Id " + id);
            }

            calcGroup.Destroy();
            return Json(new { Success = "Удаление выполненно" });
        }

        #endregion

        #region Выгрузка результатов оценки

        [SRDFunction(Tag = "")]
        public ActionResult UnloadSettings()
        {
            UnloadSettingsDto settings = new UnloadSettingsDto();
            ViewData["Documents"] = OMInstance.Where(x => x).SelectAll().Execute().Select(x => new
            {
                Text = $"{x.RegNumber} {x.Description}",
                Value = x.Id,
            }).ToList();

            return View(settings);
        }

        [HttpPost]
        [SRDFunction(Tag = "")]
        public ActionResult UnloadSettings(UnloadSettingsDto settings)
        {
            if (!ModelState.IsValid)
            {
                return GenerateMessageNonValidModel();
            }

            KOUnloadSettings settingsUnload = UnloadSettingsDto.Map(settings);

            //For testing
            //var koUnloadResults = KOUnloadResult.GetKoUnloadResultTypes(settingsUnload);
            //var unloadResultQueue = new OMUnloadResultQueue
            //{
            //	UserId = SRDSession.GetCurrentUserId().Value,
            //	DateCreated = DateTime.Now,
            //	Status_Code = ObjectModel.Directory.Common.ImportStatus.Added,
            //	UnloadTypesMapping = JsonConvert.SerializeObject(koUnloadResults),
            //	UnloadCurrentCount = 0,
            //	UnloadTotalCount = koUnloadResults.Count
            //};
            //unloadResultQueue.Save();
            //new KoDownloadResultProcess().StartProcess(new OMProcessType(), new OMQueue
            //{
            //	Status_Code = Status.Added,
            //	UserId = SRDSession.GetCurrentUserId(),
            //	Parameters = settingsUnload.SerializeToXml(),
            //	ObjectId = unloadResultQueue.Id
            //}, new CancellationToken());

            KoDownloadResultProcess.AddImportToQueue(settingsUnload);

            return Ok();
        }

        #endregion
    }
}