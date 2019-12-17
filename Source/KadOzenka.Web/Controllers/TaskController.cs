﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Core.SRD;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Controllers
{
	public class TaskController : Controller
    {
		#region Туры

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
				return Json(new { Error = "Поле не должно быть пустым" });
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

			return Json(new { Success = "Сохранение выполненно", Id = idSave });
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
			var groups = OMGroup.Where(x => x).SelectAll().Execute().Select(x => new
			{
				x.Id,
				ParentId = (x.ParentId == -1) ? null : x.ParentId,				
				x.GroupName
			}).ToList();			

			return Json(groups);
		}

		public ActionResult EditGroup(long? id)
		{
			GroupDto dto = new GroupDto();

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

				KoGroupAlgoritm objType;
				if (group.ParentId != -1 && group.ParentId != null)
				{
					while (true)
					{
						OMGroup parent = OMGroup.Where(x => x.Id == group.ParentId)
							.SelectAll().ExecuteFirstOrDefault();
						if (parent == null)
						{
							objType = KoGroupAlgoritm.MainOKS;
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
		public ActionResult EditGroup(GroupDto dto)
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

		public JsonResult GetParentGroup(string type)
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

			var groups = OMGroup.Where(x => x.GroupAlgoritm_Code == groupAlgoritm)
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

		#endregion
	}

	public class GroupDto
	{
		public long? Id { get; set; }
		public long? RatingTourId { get; set; }
	
		public string ObjType { get; set; }
		public long? ParentGroupId { get; set; }

		public long? GroupingMechanismId { get; set; }
		public string Name { get; set; }
	}
}