using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.GbuObject.Dto;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.CalculateSystem;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Model;
using KadOzenka.Dal.Models.Filters;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Tours.Dto;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.Tour;
using KadOzenka.Web.Models.Tour.EstimateGroup;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ObjectModel.Core.Register;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.Directory.ES;
using ObjectModel.KO;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
    public class TourController : KoBaseController
    {
        public ITourService TourService { get; set; }
        public GroupService GroupService { get; set; }
        public TourFactorService TourFactorService { get; set; }
        public TourComplianceImportService TourComplianceImportService { get; set; }
        public GroupingDictionaryService DictionaryService { get; set; }
        public IModelService ModelService { get; set; }

        public TourController(ITourService tourService, IGbuObjectService gbuObjectService,
	        IRegisterCacheWrapper registerCacheWrapper, IModelService modelService)
	        : base(gbuObjectService, registerCacheWrapper)
        {
            TourFactorService = new TourFactorService();
            GroupService = new GroupService();
            TourService = tourService;
            TourComplianceImportService = new TourComplianceImportService();
            DictionaryService = new GroupingDictionaryService();
            ModelService = modelService;
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

            var groupAlgorithms = Helpers.EnumExtensions.GetSelectList(typeof(KoGroupAlgoritm));
            ViewBag.GroupAlgorithms = groupAlgorithms;

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
                UrlForEdit = Url.Action("TourSubCard", "Tour", new {tourId = tour.Id}),
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
                case (long) KoGroupAlgoritm.MainParcel:
                    groupDto.Name = KoGroupAlgoritm.MainParcel.GetEnumDescription();
                    groupDto.GroupType = GroupType.Main;
                    isReadOnly = true;
                    break;

                case (long) KoGroupAlgoritm.MainOKS:
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

            groupModel.Models = ModelService.GetGroupModels(groupId)
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

            GroupService.UpdateGroupToMarketSegmentRelation(model.GroupId, model.MarketSegment);

            return new JsonResult(new {Message = "Обновление выполнено"});
        }

        #endregion

        #region Настройка для разъяснений

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult GroupExplanationSettingsSubCard(long groupId)
        {
            var settings = GroupService.GetGroupExplanationSettings(groupId);
            return PartialView("~/Views/Tour/Partials/GroupExplanationSettingsSubCard.cshtml",
                GroupExplanationSettingsModel.FromDto(settings));
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GroupExplanationSettingsSubCard(GroupExplanationSettingsModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            GroupService.UpdateGroupExplanationSettings(model.ToDto());

            return new JsonResult(new {Message = "Обновление выполнено"});
        }

        #endregion Настройка для разъяснений

        #region Настройка для акта определения

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult GroupCadastralCostDefinitionActSettingsSubCard(long groupId)
        {
            var settings = GroupService.GetGroupCadastralCostDefinitionActSettings(groupId);
            return PartialView("~/Views/Tour/Partials/GroupCadastralCostDefinitionActSettingsSubCard.cshtml",
                GroupCadastralCostDefinitionActSettingsModel.FromDto(settings));
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult GroupCadastralCostDefinitionActSettingsSubCard(
            GroupCadastralCostDefinitionActSettingsModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            GroupService.UpdateGroupCadastralCostDefinitionActSettings(model.ToDto());

            return new JsonResult(new {Message = "Обновление выполнено"});
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

                return Json(new {message = "Сохранение выполненно успешно", id});
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

            return Json(new {Success = true});
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

            return Json(new {Success = true});
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
                    RoomTypeColumnName = model.RoomTypeColumnName
                };

                using (Stream stream = file.OpenReadStream())
                {
                    TourComplianceImportService.ImportComplianceFromFile(stream, importFileDto, model.ObjectType,
                        model.TourId.GetValueOrDefault());
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
            ViewData["TreeAttributes"] = GetGbuAttributesTree();

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
            }
            catch (Exception e)
            {
                return SendErrorMessage(e.Message);
            }

            return Json(new {Success = "Сохранено успешно"});
        }

        #endregion Настройки атрибутов тура

        #region Настройка для группировки

        //TODO: Split and move to services
        private List<DropDownTreeItemModel> GetKoAttributes(long groupId)
        {
            void FillTreeItemModel(DropDownTreeItemModel treeItemModel, IEnumerable<OMAttribute> omAttributes, long attrId)
            {
                treeItemModel.Items.AddRange(omAttributes.Where(x => x.RegisterId == attrId).Select(x => new DropDownTreeItemModel
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList());
            }

            DropDownTreeItemModel CreateModel(string name)
            {
                return new DropDownTreeItemModel
                {
                    Text = name,
                    Value = null,
                    Items = new List<DropDownTreeItemModel>()
                };
            }

            var groupDto = GroupService.GetGroupById(groupId);

            var tourGroupRec = OMTourGroup.Where(x => x.GroupId == groupId).SelectAll().Execute().FirstOrDefault();

            var tourId = tourGroupRec?.TourId ?? 0;

            var tourGroupsInfo = GroupService.GetTourGroupsInfo(tourId, ObjectTypeExtended.Both);

            var Oks = tourGroupsInfo.OksSubGroups.FirstOrDefault(x => x.Id == groupId);
            var Zu = tourGroupsInfo.ZuSubGroups.FirstOrDefault(x => x.Id == groupId);
            ObjectTypeExtended objectType = ObjectTypeExtended.Both;
            if (Oks != null)
            {
                objectType = ObjectTypeExtended.Oks;
            }
            else if (Zu != null)
            {
                objectType = ObjectTypeExtended.Zu;
            }

            var koAttributes = TourFactorService.GetTourAttributes(tourId, objectType).Where(x=>x.IsDeleted != true).ToList();

            var regIds = koAttributes.Select(x => x.RegisterId).Distinct().ToList();
            if (regIds.Count > 1)
            {
                var oks = CreateModel("ОКС");
                var zu = CreateModel("ЗУ");

                foreach (var id in regIds)
                {
                    var type = OMTourFactorRegister.Where(x => x.RegisterId == id).SelectAll().ExecuteFirstOrDefault()
                        .ObjectType_Code;
                    FillTreeItemModel(type == PropertyTypes.Stead ? zu : oks, koAttributes, id);
                }
                var list = new List<DropDownTreeItemModel>{zu, oks};
                return list;
            }

            var models = koAttributes.Select(x => new DropDownTreeItemModel
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).AsEnumerable().ToList();
            return models;
        }

        private List<DropDownTreeItemModel> GetMergedAttributes(long groupId)
        {
            var mergedAttributes = new List<DropDownTreeItemModel>();
            var koAttributes = GetKoAttributes(groupId);
            mergedAttributes.AddRange(koAttributes);
            var gbuAttributes = GetGbuAttributesTree();
            mergedAttributes.AddRange(gbuAttributes);
            return mergedAttributes;
        }

        public List<SelectListItem> GetDictionariesForDropdown()
        {
            var dictionaries = DictionaryService.GetDictionaries().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            dictionaries.Insert(0, new SelectListItem("", ""));
            return dictionaries;
        }

        private TourGroupGroupingSettingsModel GetTourGroupSettingsModel(long groupId)
        {
            ViewData["KoAttributes"] = GetMergedAttributes(groupId);

            ViewData["Dictionaries"] = GetDictionariesForDropdown();

            var groupingSettingsList = OMTourGroupGroupingSettings.Where(x => x.GroupId == groupId).SelectAll().Execute();

            var model = new TourGroupGroupingSettingsModel();
            model.GroupId = groupId;
            model.Settings = new List<TourGroupGroupingSettingsPartialModel>();

            var ind = 0;
            foreach (var groupSetting in groupingSettingsList)
            {
                var settingModel = new TourGroupGroupingSettingsPartialModel();
                settingModel.Index = ind;
                settingModel.KoAttributes = groupSetting.KoAttributeId;
                settingModel.GroupFilters = groupSetting.Filter.DeserializeFromXml<Filters>();
                if (groupSetting.DictionaryId is 0 or null)
                {
                    settingModel.UseDictionary = false;
                }
                else
                {
                    settingModel.DictionaryId = groupSetting.DictionaryId;
                    settingModel.DictionaryValue = groupSetting.DictionaryValues;
                    settingModel.UseDictionary = true;
                }
                model.Settings.Add(settingModel);
                ind++;
            }

            return model;
        }

        [HttpGet]
        //[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public ActionResult TourGroupGroupingSettings(long groupId)
        {
            var model = GetTourGroupSettingsModel(groupId);

            return View("~/Views/Tour/Partials/TourGroupGroupingSettings.cshtml", model);
        }

        [HttpGet]
        public ActionResult TourGroupGroupingSettingsPartial(long groupId)
        {
            var model = GetTourGroupSettingsModel(groupId);
            return PartialView("~/Views/Tour/Partials/TourGroupGroupingSettings.cshtml", model);
        }

        [HttpGet]
        public ActionResult TourGroupGroupingSettingsPartialRow(int groupId, TourGroupGroupingSettingsPartialModel model)
        {
            ViewData["KoAttributes"] = GetMergedAttributes(groupId);

            return PartialView("~/Views/Tour/Partials/TourGroupGroupingSettingsPartial.cshtml", model);
        }

        [HttpGet]
        public ActionResult TourGroupGroupingSettingsPartialNewRow(int groupId, int index, string prefix, bool useDictionary)
        {
            ViewData["KoAttributes"] = GetMergedAttributes(groupId);
            ViewData.TemplateInfo.HtmlFieldPrefix = prefix;
            var model = new TourGroupGroupingSettingsPartialModel {Index = index, UseDictionary = useDictionary};
            return PartialView("~/Views/Tour/Partials/TourGroupGroupingSettingsPartial.cshtml", model);
        }

        [HttpPost]
        //[SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS)]
        public JsonResult TourGroupGroupingSettings(TourGroupGroupingSettingsModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            //return new JsonResult(new {Message = "Test"});
            var objectModel = model.ToObjectModel().Where(x=>x.KoAttributeId != null);
            var groupingSettingsList = OMTourGroupGroupingSettings.Where(x => x.GroupId == model.GroupId).SelectAll().Execute();

            // TODO: Поменять логику создания
            groupingSettingsList.ForEach(x=>x.Destroy());
            objectModel.Where(x=>x.KoAttributeId != 0).ToList().ForEach(x=>x.Save());

            return new JsonResult(new {Message = "Обновление выполнено"});
        }

        #endregion


        #region Словари групировки

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public ActionResult GroupingDictionaryCard(long dictionaryId, bool showItems = false)
        {
            var dictionary = OMGroupingDictionary.Where(x => x.Id == dictionaryId).SelectAll().ExecuteFirstOrDefault();
            var model = GroupingDictionaryModel.ToModel(dictionary, showItems);

            return View(model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public ActionResult GroupingDictionaryCard(GroupingDictionaryModel viewModel)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var id = viewModel.Id;
            if (id == -1)
                id = DictionaryService.CreateDictionary(viewModel.Name, viewModel.ValueType);
            else
                DictionaryService.UpdateDictionary(viewModel.Id, viewModel.Name, viewModel.ValueType);

            return Json(new { Success = "Сохранено успешно", Id = id });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public IActionResult GroupingDictionaryDelete(long dictionaryId)
        {
            try
            {
                var dictionary = DictionaryService.GetDictionaryById(dictionaryId);

                return View(GroupingDictionaryModel.ToModel(dictionary));
            }
            catch (Exception ex)
            {
                return SendErrorMessage(ex.Message);
            }
        }

        [HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public IActionResult DeleteGroupingDictionary(long dictionaryId)
        {
            try
            {
                DictionaryService.DeleteDictionary(dictionaryId);
            }
            catch (Exception ex)
            {
                return SendErrorMessage(ex.Message);
            }

            return Json(new { Success = true });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public IActionResult GroupingDictionaryImport()
        {
            ViewData["References"] = OMGroupingDictionary.Where(x => true).SelectAll().Execute().Select(x => new
            {
                Text = x.Name,
                Value = x.Id
            }).ToList();

            return View(new GroupingDictionaryImportModel());
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public IActionResult GroupingDictionaryImportPreconfigured(long attributeId, long groupId)
        {
            var dictionaries = OMGroupingDictionary.Where(x => true).SelectAll().Execute().Select(x => new
            {
                Text = x.Name,
                Value = x.Id
            }).ToList();

            ViewData["References"] = dictionaries;

            var model = new GroupingDictionaryImportModel();

            var attrib = RegisterCache.RegisterAttributes.FirstOrDefault(x => x.Key == attributeId);
            model.ValueType = attrib.Value.Type switch
            {
                RegisterAttributeType.INTEGER or RegisterAttributeType.DECIMAL => ReferenceItemCodeType.Number,
                RegisterAttributeType.DATE => ReferenceItemCodeType.Date,
                _ => model.ValueType
            };

            var dictName = "";
            var group = GroupService.GetGroupsByIds(new List<long> { groupId }).FirstOrDefault();
            if (group != null)
            {
                dictName += group.FullGroupName + " " + attrib.Value.Name;
            }

            var partialDict = new PartialGroupingDictionaryModel();
            var value = dictionaries?.FirstOrDefault(x => x?.Text == dictName)?.Value;
            if (value is not null or 0)
            {
                partialDict.IsNewDictionary = false;
                partialDict.DeleteOldValues = true;
                partialDict.DictionaryId = value;
            }
            else
            {
                partialDict.IsNewDictionary = true;
                partialDict.NewDictionaryName = dictName;
            }

            model.GroupingDictionary = partialDict;
            return View("GroupingDictionaryImport", model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public IActionResult GroupingDictionaryImport(IFormFile file, GroupingDictionaryImportModel viewModel)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            long? dictionaryId = null;
            object returnedData;
            try
            {
                using (var fileStream = file.OpenReadStream())
                {
                    var importInfo = new GroupingDictionaryImportFileInfoDto
                    {
                        FileName = file.FileName,
                        ValueColumnName = viewModel.Value,
                        CalcValueColumnName = viewModel.CalcValue,
                        ValueType = viewModel.ValueType
                    };

                    var import = GroupingDictionaryService.CreateDataFileImport(fileStream, importInfo.FileName);
                    fileStream.Seek(0, SeekOrigin.Begin);
                    if (DictionaryService.MustUseLongProcess(fileStream))
                    {
                        fileStream.Seek(0, SeekOrigin.Begin);
                        var inputParameters = new GroupingDictionaryImportFileFromExcelDto
                        {
                            DeleteOldValues = viewModel.GroupingDictionary.DeleteOldValues,
                            FileInfo = importInfo,
                            DictionaryId = viewModel.GroupingDictionary.DictionaryId.GetValueOrDefault(),
                            IsNewDictionary = viewModel.GroupingDictionary.IsNewDictionary,
                            NewDictionaryName = viewModel.GroupingDictionary.NewDictionaryName
                        };
                        ////TODO для тестирования
                        //new ModelDictionaryImportFromExcelLongProcess().StartProcess(new OMProcessType(), new OMQueue
                        //{
                        //	Status_Code = Status.Added,
                        //	UserId = SRDSession.GetCurrentUserId(),
                        //	ObjectId = import.Id,
                        //	Parameters = inputParameters.SerializeToXml()
                        //}, new CancellationToken());

                        GroupingDictionaryImportFromExcelLongProcess.AddProcessToQueue(fileStream, inputParameters, import);

                        returnedData = new
                        {
                            Success = true,
                            message = "Добавление справочника было поставленно в очередь долгих процессов. После добавления вы получите уведомление.",
                            isLongProcess = true
                        };
                    }
                    else
                    {
                        fileStream.Seek(0, SeekOrigin.Begin);

                        var dictionary = viewModel.GroupingDictionary;
                        if (dictionary.IsNewDictionary)
                        {
                            dictionaryId = DictionaryService.CreateDictionaryFromExcel(fileStream, importInfo,
                                dictionary.NewDictionaryName, import);
                        }
                        else
                        {
                            DictionaryService.UpdateDictionaryFromExcel(fileStream, importInfo,
                                dictionary.DictionaryId.GetValueOrDefault(-1), dictionary.DeleteOldValues, import);
                        }

                        returnedData = new
                        {
                            Success = true,
                            message = "Справочник успешно импортирован",
                            newDictionaryId = viewModel.GroupingDictionary.IsNewDictionary ? dictionaryId : null,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return SendErrorMessage(ex.Message);
            }

            return Json(returnedData);
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public ActionResult GroupingDictionaryValueCard(long dictionaryValueId, long dictionaryId)
        {
            var dictionaryValue = OMGroupingDictionariesValues.Where(x => x.Id == dictionaryValueId).SelectAll().ExecuteFirstOrDefault();
            dictionaryId = dictionaryValue == null ? dictionaryId : dictionaryValue.DictionaryId;
            var dictionary = DictionaryService.GetDictionaryById(dictionaryId);

            return View(GroupingDictionaryValueModel.ToModel(dictionaryValue, dictionary));
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public ActionResult GroupingDictionaryValueCard(GroupingDictionaryValueModel viewModel)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            var id = viewModel.Id;
            if (id == -1)
                id = DictionaryService.CreateDictionaryValue(viewModel.ToDto());
            else
                DictionaryService.UpdateDictionaryValue(viewModel.ToDto());

            return Json(new { Success = "Сохранено успешно", Id = id });
        }

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public IActionResult GroupingDictionaryValueDelete(long dictionaryValueId)
        {
            try
            {
                var dictionaryValue = DictionaryService.GetDictionaryValueById(dictionaryValueId);
                var dictionary = DictionaryService.GetDictionaryById(dictionaryValue.DictionaryId);

                return View(GroupingDictionaryValueModel.ToModel(dictionaryValue, dictionary));
            }
            catch (Exception ex)
            {
                return SendErrorMessage(ex.Message);
            }
        }

        [HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public IActionResult GroupingDeleteDictionaryValue(long dictionaryValueId)
        {
            try
            {
                DictionaryService.DeleteDictionaryValue(dictionaryValueId);
            }
            catch (Exception ex)
            {
                return SendErrorMessage(ex.Message);
            }

            return Json(new { Success = true });
        }

        [SRDFunction(Tag = SRDCoreFunctions.KO_GROUPING_DICT)]
        public JsonResult GetGroupingDictionaries()
        {
            var dictionaries = DictionaryService.GetDictionaries().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            dictionaries.Insert(0, new SelectListItem("", ""));

            return Json(dictionaries);
        }

        #endregion

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

            return Json(new {Id = id});
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public IActionResult CanTourBeDeleted(long id)
        {
            var canTourBeDeleted = TourService.CanTourBeDeleted(id);
            return Json(new {CanBeDeleted = canTourBeDeleted});
        }

        [HttpDelete]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_ESTIMATES)]
        public IActionResult TourEstimates(int id)
        {
            TourService.DeleteTour(id);
            return Json(new {Success = "Удаление выполнено"});
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
            groups.ForEach(x => { groupModels.Add(GroupTreeModel.ToModel(x, Url)); });

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
            return Json(new {CanBeDeleted = canGroupBeDeleted});
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public IActionResult CanGroupsBeDeleted(long tourId, bool isOks)
        {
            var canGroupsBeDeleted = GroupService.CanGroupsBeDeleted(tourId, isOks);
            return Json(new {CanBeDeleted = canGroupsBeDeleted});
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public IActionResult DeleteGroup(long id)
        {
            GroupService.DeleteGroup(id);
            return Json(new {Success = "Удаление выполнено"});
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.KO_DICT_TOURS_GROUPS)]
        public IActionResult DeleteGroups(long tourId, bool isOks)
        {
            GroupService.DeleteGroups(tourId, isOks);
            return Json(new {Success = "Удаление выполнено"});
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
            if (tourId == null)
                return Json(new List<SelectListItem>());

            var groupAlgorithm = type == ObjectTypeExtended.Oks ? KoGroupAlgoritm.MainOKS : KoGroupAlgoritm.MainParcel;

            var allGroups = GroupService.GetGroupsTreeForTour(tourId.Value);

            var groups = allGroups.Where(x => x.Id == (int) groupAlgorithm)
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
                algotitmItems = algotitmItems.Where(x => x.ItemId != (long) KoGroupAlgoritm.MainOKS
                                                         && x.ItemId != (long) KoGroupAlgoritm.MainParcel &&
                                                         x.Code != null).ToList();
            }
            else
            {
                algotitmItems = algotitmItems.Where(x => x.ItemId == (long) KoGroupAlgoritm.MainOKS
                                                         || x.ItemId == (long) KoGroupAlgoritm.MainParcel).ToList();
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

            var mainGroupId = isParcel ? (long) KoGroupAlgoritm.MainParcel : (long) KoGroupAlgoritm.MainOKS;
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
            var omRegister = OMRegister.Where(x => x.RegisterId == registerFactorId).Select(x => x.RegisterId)
                .ExecuteFirstOrDefault();

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
                    : (RegisterAttributeType) omAttribute.Type;
                model.ReferenceId = omAttribute.ReferenceId;
            }

            return View(model);
        }

        [HttpPost]
        [JsonExceptionHandler]
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
                var omRegister = OMRegister.Where(x => x.RegisterId == model.RegisterFactorId).Select(x => x.RegisterId)
                    .ExecuteFirstOrDefault();
                if (omRegister == null)
                {
                    omRegister = TourFactorService.CreateTourFactorRegister(model.TourId, model.IsSteadObjectType);
                    model.RegisterFactorId = omRegister.RegisterId;
                }

                if (model.Id == -1)
                {
                    model.Id = TourFactorService.CreateTourFactorRegisterAttribute(model.Name, omRegister.RegisterId,
                        model.Type, model.ReferenceId);
                }
                else
                {
                    TourFactorService.RenameTourFactorRegisterAttribute(id, model.Name);
                }

                ts.Complete();
            }

            return Json(new {Success = "Сохранено успешно", data = model});
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

            var attr = TourFactorService.CheckFactorUsage(id);
            var canDelete = attr.ApprovedModels.Count == 0;
            string additionalMessage = String.Empty;

            if (canDelete && attr.AffectedModels.Count > 0)
            {
                additionalMessage = attr.AffectedModels.Select(x => $"{x.Name} (Id={x.Id})")
                    .Aggregate((item1, item2) => item1 + ", " + item2);
            }

            ViewBag.CanDelete = canDelete;
            ViewBag.ShowAdditionalMessage = additionalMessage != String.Empty;
            ViewBag.AdditionalMessage = additionalMessage;

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
            var res = dtos.Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.CombinedName});

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
            bool isExists = OMCalcGroup.Where(x =>
                    x.GroupId == calcGroup.GroupId && x.ParentCalcGroupId == calcGroup.ParentCalcGroupId)
                .ExecuteExists();

            if (isExists)
                throw new Exception("Данная группа уже была выбрана ранее");

            calcGroup.Save();
            return Json(new {Success = "Изменения успешно сохранены"});
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
            return Json(new {Success = "Удаление выполненно"});
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