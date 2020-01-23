using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.Shared.Extensions;
using DevExpress.DataProcessing;
using KadOzenka.Dal.Groups.Dto;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Tours.Dto;
using KadOzenka.Web.Models.Tour;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
	public class TourController : Controller
    {
        public TourService TourService { get; set; }
        public GroupService GroupService { get; set; }

        public TourController()
        {
            TourService = new TourService();
            GroupService = new GroupService();
        }

        #region Карточка тура

        [HttpGet]
        public ActionResult TourCard(long tourId)
        {
            ViewBag.TourId = tourId;
            return View();
        }

        public JsonResult GetGroupsForTour(long tourId)
        {
            var tour = TourService.GetTourById(tourId);
            var newParent = new GroupTreeModel
            {
                Id = 0,
                GroupName = tour.Year.ToString(),
                TourId = tour.Id,
                UrlForEdit = Url.Action("TourSubCard", "Tour", new { tourId = tour.Id })
            };
            var groupModels = new List<GroupTreeModel> {newParent};

            var groups = GroupService.GetGroups(newParent.Id);
            groups.ForEach(x =>
            {
                var treeModel = GroupTreeModel.ToModel(x);
                treeModel.UrlForEdit = Url.Action("GroupSubCard", "Tour", new { groupId = treeModel.Id});
                groupModels.Add(treeModel);
            });

            return Json(groupModels);
        }

        [HttpGet]
        public ActionResult TourSubCard(long tourId)
        {
            var tour = TourService.GetTourById(tourId);
            var tourModel = TourModel.ToModel(tour);

            return PartialView("~/Views/Tour/Partials/TourSubCard.cshtml", tourModel);
        }

        [HttpGet]
        public ActionResult GroupSubCard(long groupId)
        {
            var groupDto = new GroupDto{Id = groupId};
            switch (groupId)
            {
                case (long)KoGroupAlgoritm.MainParcel:
                    groupDto.Name = KoGroupAlgoritm.MainParcel.GetEnumDescription();
                    break;

                case (long)KoGroupAlgoritm.MainOKS:
                    groupDto.Name = KoGroupAlgoritm.MainOKS.GetEnumDescription();
                    break;

                default:
                    groupDto = GroupService.GetGroupById(groupId);
                    break;
            }

            var groupModel = GroupModel.ToModel(groupDto);

            return PartialView("~/Views/Tour/Partials/GroupSubCard.cshtml", groupModel);
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
				dto.GroupingMechanismId = (long)group.GroupAlgoritm_Code;
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
		public ActionResult EditGroup(GroupModel dto)
		{
			OMGroup group = null;
			OMTourGroup tourGroup = null;

			if (dto.Id.HasValue)
			{
				group = OMGroup.Where(x => x.Id == dto.Id.Value)
					.SelectAll().ExecuteFirstOrDefault();
				tourGroup = OMTourGroup.Where(x => x.GroupId == dto.Id.Value)
					.SelectAll().ExecuteFirstOrDefault();
			}
			else
			{
				group = new OMGroup();
				tourGroup = new OMTourGroup();
			}

			group.GroupName = dto.Name;
			group.ParentId = dto.ParentGroupId.HasValue ? dto.ParentGroupId : -1;
			group.GroupAlgoritm_Code = (KoGroupAlgoritm)dto.GroupingMechanismId;
			group.Save();

			tourGroup.GroupId = group.Id;
			tourGroup.TourId = dto.RatingTourId.Value;
			tourGroup.Save();	

			return Ok();
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

        
        #region Support Methods

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

		#endregion
    }
}