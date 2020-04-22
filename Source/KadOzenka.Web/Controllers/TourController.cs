using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.Enums;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Groups.Dto.Consts;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Models.Tour;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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

        public TourController()
        {
            TourService = new TourService();
            GroupService = new GroupService();
            TourFactorService = new TourFactorService();
        }

        #region Карточка тура

        [HttpGet]
        public ActionResult TourCard(long? tourId, long? parentGroupId, long? groupId)
        {
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
                groupModels.Add(GroupTreeModel.ToModel(x));
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

			try
		    {
			    ExcelFile excelFile;
			    using (var stream = viewModel.File.OpenReadStream())
			    {
				    excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
					excelFile.DocumentProperties.Custom["FileName"] = viewModel.File.FileName;
				}

			    if (viewModel.IsUnitStatusUsed)
			    {
				    DataImporterKO.ImportDataGroupNumberFromExcel(excelFile, "KoTours", OMTour.GetRegisterId(),
					    viewModel.TourId.GetValueOrDefault(), viewModel.UnitStatus.GetValueOrDefault());
				}
			    else
			    {
				    DataImporterKO.ImportDataGroupNumberFromExcel(excelFile, "KoTours", OMTour.GetRegisterId(),
					    viewModel.TourId.GetValueOrDefault(), viewModel.TaskFilter);
				}
		    }
		    catch (Exception ex)
		    {
			    ErrorManager.LogError(ex);
			    return BadRequest();
		    }
			return NoContent();
		}

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
			var allGroups = new GroupService().GetGroups();
			var allGroupsInTour = allGroups.Where(x => x.TourId == tourId).ToList();
			if(allGroupsInTour.Count == 0)
				return Json(string.Empty);

			var subgroups = GetSubgroups(allGroupsInTour);
			var groupsWithSubGroups = GetGroupsWithSubgroups(subgroups, allGroups);
			var groupsWithSubGroupsIds = groupsWithSubGroups.Select(x => x.Id).Distinct().ToList();
			var groupsWithoutSubGroups = allGroupsInTour.Where(x =>
				(x.ParentId == (long) KoGroupAlgoritm.MainOKS || x.ParentId == (long) KoGroupAlgoritm.MainParcel) &&
				!groupsWithSubGroupsIds.Contains(x.Id)).ToList();
			
			var allTourGroups = new List<GroupTreeDto>();
			allTourGroups.AddRange(groupsWithoutSubGroups);
			allTourGroups.AddRange(groupsWithSubGroups);

			var models = new List<GroupTreeModel>();
			var mainGroups = GetMainGroups();
			mainGroups.ForEach(mainGroup =>
			{
				var groups = allTourGroups.Where(group => group.ParentId == mainGroup.Id).Select(group =>
					new GroupTreeModel
					{
						Id = group.Id,
						GroupName = group.GroupName,
						UrlForEdit = Url.Action("GroupSubCard", "Tour", new {groupId = group.Id, tourId = tourId}),
						GroupType = group.GroupType,
						Items = subgroups.Where(subGroup => subGroup.ParentId == group.Id).Select(subGroup =>
							new GroupTreeModel
							{
								Id = subGroup.Id,
								GroupName = subGroup.GroupName,
								UrlForEdit = Url.Action("GroupSubCard", "Tour",
									new {groupId = subGroup.Id, tourId = tourId}),
								GroupType = subGroup.GroupType
							}).ToList()
					}).ToList();

				if (groups.Count > 0)
				{
					models.Add(new GroupTreeModel
					{
						Id = mainGroup.Id,
						GroupName = mainGroup.GroupName,
						UrlForEdit = Url.Action("GroupSubCard", "Tour", new { groupId = mainGroup.Id, tourId = tourId }),
						GroupType = mainGroup.GroupType,
						Items = groups
					});
				}
			});

			return Json(models);
		}

		public JsonResult GetGroupsByTourAndMainGroup(long tourId, bool isParcel)
        {
            var allGroups = new GroupService().GetGroups();
            var allGroupsInTour = allGroups.Where(x => x.TourId == tourId).ToList();

            var subgroups = GetSubgroups(allGroupsInTour);
            var groupsWithSubgroups = GetGroupsWithSubgroups(subgroups, allGroups);

            var mainGroupId = isParcel ? (long)KoGroupAlgoritm.MainParcel : (long)KoGroupAlgoritm.MainOKS;

            var models = groupsWithSubgroups.Where(group => group.ParentId == mainGroupId)
                .Select(group =>
                {
                    return new DropDownTreeItemModel
                    {
                        Id = group.Id.ToString(),
                        Value = group.Id.ToString(),
                        Text = group.GroupName,
                        Items = subgroups.Where(subgroup => subgroup.ParentId == group.Id).Select(subgroup =>
                            new DropDownTreeItemModel
                            {
                                Id = subgroup.Id.ToString(),
                                Value = subgroup.Id.ToString(),
                                Text = subgroup.GroupName
                            }).ToList()
                    };
                }).AsEnumerable();

            return Json(models);
        }

		#region Support Methods

		private List<GroupTreeDto> GetSubgroups(List<GroupTreeDto> allGroupsInTour)
		{
			return allGroupsInTour.Where(x => x.ParentId != (long)KoGroupAlgoritm.MainOKS && x.ParentId != (long)KoGroupAlgoritm.MainParcel).ToList();
		}

		private static List<GroupTreeDto> GetGroupsWithSubgroups(List<GroupTreeDto> subgroups, List<GroupTreeDto> allGroups)
		{
			var groupsIds = subgroups.Select(x => x.ParentId).Distinct().ToList();
			var groups = allGroups.Where(x => groupsIds.Contains(x.Id)).ToList();
			return groups;
		}

		private static List<GroupTreeDto> GetMainGroups()
		{
			var mainGroups = new List<GroupTreeDto>();

			var oks = new GroupTreeDto
			{
				Id = (long)KoGroupAlgoritm.MainOKS,
				GroupName = "Основная группа ОКС",
				GroupType = GroupType.Main
			};
			mainGroups.Add(oks);

			var parcel = new GroupTreeDto
			{
				Id = (long)KoGroupAlgoritm.MainParcel,
				GroupName = "Основная группа Участки",
				GroupType = GroupType.Main
			};
			mainGroups.Add(parcel);

			return mainGroups;
		}

		#endregion

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