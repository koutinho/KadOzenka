﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register;
using Core.Register.Enums;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Tours.Dto;
using KadOzenka.Web.Models.Tour;
using KadOzenka.Web.Models.Tour.EstimateGroup;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
	public class TourController : KoBaseController
    {
        public TourService TourService { get; set; }
        public GroupService GroupService { get; set; }
        public TourFactorService TourFactorService { get; set; }
        public GbuObjectService GbuObjectService { get; set; }
		public TourComplianceImportService TourComplianceImportService { get; set; }

		public TourController()
        {
            TourFactorService = new TourFactorService();
            TourService = new TourService(TourFactorService);
            GroupService = new GroupService();
            GbuObjectService = new GbuObjectService();
            TourComplianceImportService = new TourComplianceImportService();

		}

        #region Карточка тура

        [HttpGet]
        public ActionResult TourCard(long? tourId, long? parentGroupId, long? groupId)
        {
            //нужно чтобы при обновлении данных поставить фокус на нужный элемент в дереве
	        ViewBag.ChangedTourId = tourId ?? 0;
	        ViewBag.ChangedGroupParentGroupId = parentGroupId ?? 0;
			ViewBag.ChangedGroupId = groupId ?? 0;
			return View();
        }

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
        public ActionResult TourSubCard(long tourId)
        {
            var tour = TourService.GetTourById(tourId);
            var tourModel = TourModel.ToModel(tour);

            return PartialView("~/Views/Tour/Partials/TourSubCard.cshtml", tourModel);
        }

        [HttpGet]
        public ActionResult GroupSubCard(long groupId, long tourId)
        {
	        var groupDto = new GroupDto {Id = groupId};
            var isReadOnly = false;
            switch (groupId)
            {
                case (long)KoGroupAlgoritm.MainParcel:
                    groupDto.Name = KoGroupAlgoritm.MainParcel.GetEnumDescription();
                    isReadOnly = true;
                    break;

                case (long)KoGroupAlgoritm.MainOKS:
                    groupDto.Name = KoGroupAlgoritm.MainOKS.GetEnumDescription();
                    isReadOnly = true;
                    break;

                default:
                    groupDto = GroupService.GetGroupById(groupId);
                    break;
            }

            groupDto.RatingTourId = tourId;
			var groupModel = GroupModel.ToModel(groupDto);
            groupModel.IsReadOnly = isReadOnly;

            return PartialView("~/Views/Tour/Partials/GroupSubCard.cshtml", groupModel);
        }

        [HttpGet]
        public ActionResult MarksGrid(long groupId, long factorId)
        {
            //TODO переделать на модель
            ViewBag.GroupId = groupId;
            ViewBag.FactorId = factorId;
            return View();
        }

        #endregion

        #region Настройка отношения групп и сегментов рынка

        [HttpGet]
        public ActionResult GroupSegmentSettingsSubCard(long groupId)
        {
            var settings = GroupService.GetGroupToMarketSegmentRelation(groupId);
            var model = settings == null
                ? new GroupSegmentSettingsModel()
                : GroupSegmentSettingsModel.FromEntity(settings);

            return PartialView("~/Views/Tour/Partials/GroupSegmentSettingsSubCard.cshtml", model);
        }

        [HttpPost]
        public JsonResult GroupSegmentSettingsSubCard(GroupSegmentSettingsModel model)
        {
            if (!ModelState.IsValid)
                return GenerateMessageNonValidModel();

            GroupService.UpdateGroupToMarketSegmentRelation(model.GroupId, model.MarketSegment, model.TerritoryType);

            return new JsonResult(new { Message = "Обновление выполнено" });
        }

		#endregion

		#region Настройки атрибутов тура
		[HttpGet]
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
		public ActionResult RemoveGroup(long complianceId)
		{
			var compliance = OMComplianceGuide.Where(x => x.Id == complianceId).SelectAll().ExecuteFirstOrDefault();
			return View(AddGroupViewModel.ToModel(compliance));
		}

		[HttpPost]
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
		public ActionResult DataImport(long tourId)
		{
			var model = new ImportGroupViewModel {TourId = tourId};
			return View(model);
		}

		[HttpPost]
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
					FileName = Path.GetFileNameWithoutExtension(file.FileName),
					CodeColumnName = model.CodeColumnName,
					GroupColumnName = model.GroupColumnName,
					RoomTypeColumnName = model.RoomTypeColumnName
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
		public FileContentResult DownloadImportedFile(int idFile)
		{
			var import = OMImportDataLog.Where(x => x.Id == idFile).SelectAll().ExecuteFirstOrDefault();

			if (import == null)
			{
				throw new Exception("Указанный файл не найден.");
			}

			var templateFile = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated,
				import.Id.ToString());
			var bytes = new byte[templateFile.Length];
			templateFile.Read(bytes);
			StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtension, out string contentType);

			return File(bytes, contentType, $"{import.DataFileName}.{fileExtension}");
		}

		[HttpGet]
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
        public JsonResult GetTourGbuAttributeSettings(long tourId)
        {
	        var models = TourFactorService.GetTourAttributesFromSettings(tourId);

			return Json(new {Data = models});
        }

        [HttpPost]
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
                    AttributeId = model.TypeRoomAttributeId,
                    KoAttributeUsingType = KoAttributeUsingType.TypeRoomAttribute
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
		public IActionResult TourEstimates()
		{
			return View();
		}

		[HttpPost]
		public IActionResult TourEstimates(TourModel tourModel)
        {
            if (tourModel == null)
                throw new Exception("Не передана модель для сохранения Тура");

			var tourDto = TourModel.FromModel(tourModel);

            var id = tourModel.Id == -1 
                ? TourService.AddTour(tourDto) 
                : TourService.UpdateTour(tourDto);

            return Json(new { Success = "Сохранение выполненно", Id = id });
		}


		[HttpDelete]
		public IActionResult TourEstimates(int id)
		{
			var tour = OMTour.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();


			if (tour == null)
			{
				return Json(new { Error = "Тур с указыным ид не найден" });
			}

			using (var ts = new TransactionScope())
			{
			    TourFactorService.RemoveTourFactorRegisters(tour.Id);
                tour.Destroy();
				ts.Complete();
			}

			return Json(new { Success = "Удаление выполненно" });
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

		#endregion

		#region Группы/подгруппы

		public ActionResult Groups()
		{
			return View();
		}

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

        [HttpGet]
		public ActionResult EditGroup(long? id)
		{
			GroupModel dto = new GroupModel();

			if (id.HasValue)
			{
				OMGroup group = OMGroup.Where(x => x.Id == id.Value)
					.SelectAll().ExecuteFirstOrDefault();
				OMTourGroup tourGroup = OMTourGroup.Where(x => x.GroupId == id.Value)
					.SelectAll().ExecuteFirstOrDefault();

				dto.Id = id;
				dto.Name = group.GroupName;
				dto.ParentGroupId = group.ParentId;
				dto.GroupingAlgorithmId = (long)group.GroupAlgoritm_Code;
				dto.RatingTourId = tourGroup.TourId;

				KoGroupAlgoritm objType = KoGroupAlgoritm.MainOKS;
				if (group.ParentId != -1 && group.ParentId != null)
				{
					while (true)
					{
						OMGroup parent = OMGroup.Where(x => x.Id == group.ParentId)
							.SelectAll().ExecuteFirstOrDefault();
						if (parent == null)
						{							
							break;
						}
						if (group.ParentId == parent.ParentId)
						{
							break;
						}
						if (parent.ParentId == -1 || parent.ParentId == null)
						{
							objType = parent.GroupAlgoritm_Code;
							break;
						}
						
						group = parent;
					}
				}
				else
				{
					objType = group.GroupAlgoritm_Code;
				}

				dto.ObjType = objType == KoGroupAlgoritm.MainOKS ? "OKS" : "Parcel";
			}

			return View(dto);			
		}

		[HttpPost]
		public JsonResult EditGroup(GroupModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                throw new Exception("Не заполнено имя группы");

            var groupDto = GroupModel.FromModel(model);

            int id;
            if (model.Id.HasValue)
                id = GroupService.UpdateGroup(groupDto);
            else
                id = GroupService.AddGroup(groupDto);

            return Json(new {Id = id});
		}

		[HttpPost]
		public IActionResult DeleteGroup(long id)
		{
			OMGroup group = OMGroup.Where(x => x.Id == id)
				.SelectAll().ExecuteFirstOrDefault();
			OMTourGroup tourGroup = OMTourGroup.Where(x => x.GroupId == id)
				.SelectAll().ExecuteFirstOrDefault();

			using (var ts = new TransactionScope())
			{
				group.Destroy();
				tourGroup.Destroy();
				ts.Complete();
			}

			return Json(new { Success = "Удаление выполненно" });
		}

		public JsonResult GetRatingTours()
		{
			var tours = OMTour.Where(x => true).SelectAll().Execute()
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Year.ToString()
				});

			return Json(tours);
		}

		public JsonResult GetAllGroups()
		{
			var groups = OMGroup.Where(x => true).SelectAll().Execute()
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.GroupName.ToString()
				});

			return Json(groups);
		}

		public JsonResult GetParentGroup(string type, long? id)
		{
			KoGroupAlgoritm groupAlgoritm;

			switch (type)
			{
				case "OKS":
					groupAlgoritm = KoGroupAlgoritm.MainOKS;
					break;
				case "Parcel":
					groupAlgoritm = KoGroupAlgoritm.MainParcel;
					break;
				default:
					throw new Exception("Не выбран тип объекта");
			}			

			var groups = OMGroup.Where(x => x.GroupAlgoritm_Code == groupAlgoritm
					&& x.Id != id)
				.SelectAll().Execute()
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.GroupName.ToString()
				});

			return Json(groups);
		}

		public JsonResult GetSubgroup(long? groupId)
		{
			if (groupId == null)
			{
				return Json(new List<SelectListItem> { });
			}

			var groups = OMGroup.Where(x => x.ParentId == groupId)
				.SelectAll().Execute()
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.GroupName.ToString()
				});

			return Json(groups);
		}

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

        #region Импорт группы из Excel

        [HttpGet]
        public ActionResult ImportGroupDataFromExcel()
        {
            return View(new ImportGroupDataModel());
        }

        [HttpPost]
        public ActionResult ImportGroupDataFromExcel(ImportGroupDataModel viewModel)
        {
            var errors = viewModel.Validate();
            if (errors.Count > 0)
            {
                return Json(new
                {
                    Errors = errors.Select(x => new
                    {
                        Control = x.MemberNames.FirstOrDefault(),
                        Message = x.ErrorMessage
                    })
                });
            }

            var fileName = string.Empty;
            try
            {
                ExcelFile excelFile;
                using (var stream = viewModel.File.OpenReadStream())
                {
                    excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
                    excelFile.DocumentProperties.Custom["FileName"] = viewModel.File.FileName;
                }

                MemoryStream resultStream;
                if (viewModel.IsUnitStatusUsed)
                {
                    resultStream = (MemoryStream)DataImporterKO.ImportDataGroupNumberFromExcel(excelFile, "KoTours", OMTour.GetRegisterId(),
                        viewModel.TourId.GetValueOrDefault(), viewModel.UnitStatus.GetValueOrDefault());
                }
                else
                {
                    resultStream = (MemoryStream)DataImporterKO.ImportDataGroupNumberFromExcel(excelFile, "KoTours", OMTour.GetRegisterId(),
                        viewModel.TourId.GetValueOrDefault(), viewModel.TaskFilter);
                }

                if (resultStream != null)
                {
                    fileName = viewModel.File.FileName;
                    HttpContext.Session.Set(fileName, resultStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return BadRequest();
            }

            return Content(JsonConvert.SerializeObject(new { FileName = fileName }), "application/json");
        }

        [HttpGet]
        public ActionResult DownloadImportGroupResult(string fileName)
        {
            var fileInfo = GetFileFromSession(fileName, RegistersExportType.Xlsx);
            if (fileInfo == null)
                return new EmptyResult();

            return File(fileInfo.FileContent, fileInfo.ContentType, $"Результат импорта группы.{fileInfo.FileExtension}");
        }

        #endregion

        [HttpGet]
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

		public JsonResult GetGroupsForTour(long tourId)
		{
			var groups = GroupService.GetGroupsTreeForTour(tourId);

			var models = groups.Select(x => GroupTreeModel.ToModel(x, Url)).ToList();

			return Json(models);
		}

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

		#region Метки

		public ActionResult MarkCatalog()
		{			
			return View();
		}

        public JsonResult GetMarkCatalog(long? groupId, long? factorId)
		{			
			List<OMMarkCatalog> markCatalog = OMMarkCatalog.Where(x => x.GroupId == groupId && x.FactorId == factorId)
				.SelectAll().Execute();			

			return Json(markCatalog);
		}

		[HttpPost]
		public ActionResult CreateMark(OMMarkCatalog markCatalog)
		{
			markCatalog.Save();
			return Json(markCatalog);
		}

		[HttpPost]
		public ActionResult UpdateMark(OMMarkCatalog markCatalog)
		{
			markCatalog.Save();
			return Json(markCatalog);
		}

		[HttpPost]
		public ActionResult DeleteMark(OMMarkCatalog markCatalog)
		{
			markCatalog.Destroy();
			return Json(markCatalog);
		}

        public FileResult DownloadMarksCatalog(long groupId, long factorId)
        {
            var fileStream = DataExporterKO.ExportMarkerListToExcel(groupId, factorId);

            return File(fileStream, Helpers.Consts.ExcelContentType, "Справочник меток (выгрузка)" + ".xlsx");
        }

        public ActionResult UploadMarksCatalog(IFormFile file, long groupId, long factorId, bool isDeleteOld)
        {
            if (file == null)
                throw new Exception("Не выбран файл для загрузки");
            if (!(file.FileName.EndsWith(".xlsx") || file.FileName.EndsWith(".xls")))
                throw new Exception("Загружен файл неправильного формата. Допустимые форматы: .xlsx и .xls");

            using (var stream = file.OpenReadStream())
            {
                var excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
                excelFile.DocumentProperties.Custom["FileName"] = file.FileName;

                var fileStream = DataImporterKO.ImportDataMarkerFromExcel(excelFile, nameof(OMMarkCatalog),
                    OMMarkCatalog.GetRegisterId(), groupId, factorId, isDeleteOld);

                var fileName = "Справочник меток (загрузка) " + file.FileName;
                HttpContext.Session.Set(fileName, fileStream.ToByteArray());

                return Content(JsonConvert.SerializeObject(new { success = true, fileName = fileName }), "application/json");
            }
        }

        [HttpGet]
        public ActionResult DownloadExcelFile(string fileName)
        {
            var fileContent = HttpContext.Session.Get(fileName);
            if (fileContent == null)
            {
                return new EmptyResult();
            }

            HttpContext.Session.Remove(fileName);
            StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtensiton, out string contentType);

            return File(fileContent, contentType, fileName);
        }

        #endregion

        #region Факторы

        [HttpGet]
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

		#endregion

		#region Зависимости при расчете подгрупп

		public JsonResult GetCalcGroups(long groupId)
		{
            var groupList = OMCalcGroup.Where(x => x.GroupId == groupId)
                .SelectAll().Execute();

            var groupIds = groupList.Select(x => x.ParentCalcGroupId).ToList();

            var groups = OMGroup.Where(x => groupIds.Contains(x.Id))
                .Select(x => x.GroupName).Execute();

            var result = groupList.Join(groups,
                calc => calc.ParentCalcGroupId,
                group => group.Id,
                (calc, group) => new ParentCalcGroupModel
                {
                    Id = calc.Id,
                    GroupId = calc.GroupId,
                    ParentCalcGroupId = calc.ParentCalcGroupId,
                    Title = group.GroupName
                }).ToList();

            return Json(result);
        }

		public JsonResult GetSubgroups(long groupId)
		{
			var group = OMGroup.Where(x => x.Id == groupId)
				.Select(x => x.ParentId)				
				.ExecuteFirstOrDefault();
			long? parentId = group.ParentId;

			var tg = OMTourGroup.Where(x => x.GroupId == groupId)
				.Select(x => x.TourId)
				.ExecuteFirstOrDefault();

			if (tg == null)
				throw new Exception("Не найден тур для выбранной группы");

			var sameGroups = OMTourGroup.Where(x => x.TourId == tg.TourId)
				.Select(x => x.GroupId)
				.Execute()
				.Select(x => x.GroupId).ToList();

			var subGroups = OMGroup.Where(x => x.ParentId == parentId && sameGroups.Contains(x.Id) && x.Id != groupId)
				.Select(x => x.GroupName).Execute();
			var res = subGroups.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.GroupName });

			return Json(res);
		}

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
	}
}