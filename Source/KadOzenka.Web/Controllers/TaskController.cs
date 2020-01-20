using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Core.Register.QuerySubsystem;
using KadOzenka.Web.Models.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.SRD;
using ObjectModel.Directory;
using ObjectModel.KO;
using Core.Shared.Extensions;
using KadOzenka.Dal.Tasks;

namespace KadOzenka.Web.Controllers
{
	public class TaskController : Controller
    {
        public TaskService TaskService { get; set; }

        public TaskController()
        {
            TaskService = new TaskService();
        }

        #region Task Card

        [HttpGet]
        public ActionResult TaskCard(long taskId)
        {
            var taskModel = new TaskModel
            {
                Id = taskId
            };

            return View(taskModel);
        }

        [HttpGet]
        public ActionResult TaskMainInfo(long taskId)
        {
            var taskDto = TaskService.GetTaskById(taskId);
            if (taskDto == null)
                return NotFound();

            var taskModel = TaskModel.ToModel(taskDto);

            return PartialView("~/Views/Task/Partials/TaskMainInfo.cshtml", taskModel);
        }

        [HttpGet]
        public ActionResult Objects()
        {
            return new EmptyResult();
        }

        #endregion

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
			QSQuery query = new QSQuery
			{
				MainRegisterID = OMGroup.GetRegisterId(),
			};
			query.Joins = new List<QSJoin>()
			{
				new QSJoin()
				{
					RegisterId = OMTourGroup.GetRegisterId(),
					JoinCondition = new QSConditionSimple()
					{
						ConditionType = QSConditionType.Equal,
							LeftOperand = OMGroup.GetColumn(x => x.Id),
							RightOperand = OMTourGroup.GetColumn(x => x.GroupId)
					},
					JoinType = QSJoinType.Inner
				}
			};
			
			query.AddColumn(OMGroup.GetColumn(x => x.ParentId, "ParentId"));
			query.AddColumn(OMGroup.GetColumn(x => x.GroupAlgoritm_Code, "GroupAlgoritm"));
			query.AddColumn(OMGroup.GetColumn(x => x.GroupName, "GroupName"));
			query.AddColumn(OMTourGroup.GetColumn(x => x.TourId, "TourId"));

			DataTable table = query.ExecuteQuery();
			List<GroupTreeDto> groups = new List<GroupTreeDto>();

			GroupTreeDto oks = new GroupTreeDto
			{
				Id = (long)KoGroupAlgoritm.MainOKS,
				ParentId = null,
				GroupName = "Основная группа ОКС"
			};
			groups.Add(oks);

			GroupTreeDto parcel = new GroupTreeDto
			{
				Id = (long)KoGroupAlgoritm.MainParcel,
				ParentId = null,
				GroupName = "Основная группа Участки"
			};
			groups.Add(parcel);

			foreach (DataRow row in table.Rows)
			{
				GroupTreeDto str = new GroupTreeDto();

				str.Id = long.Parse(row["Id"].ToString());

				long? parent = long.Parse(row["ParentId"].ToString());
				long groupAlgoritm = long.Parse(row["GroupAlgoritm"].ToString());
				if (parent == -1)
				{
					if (groupAlgoritm == (long)KoGroupAlgoritm.MainOKS)
					{
						str.ParentId = oks.Id;
					}
					else if (groupAlgoritm == (long)KoGroupAlgoritm.MainParcel)
					{
						str.ParentId = parcel.Id;
					}
				}
				else
				{
					str.ParentId = parent;
				}
								
				str.GroupName = row["GroupName"].ToString();
				str.TourId = long.Parse(row["TourId"].ToString());

				groups.Add(str);
			}				

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

		#endregion

		#region Модель

		public ActionResult Model(long groupId)
		{
			OMModel model = OMModel.Where(x => x.GroupId == groupId).SelectAll().ExecuteFirstOrDefault();

			if (model == null)
			{
				throw new Exception("Модель не найдена");
			}

			return View(model); 
		}

		[HttpPost]
		public ActionResult Model(OMModel model)
		{			
			model.Formula = model.GetFormulaFull(true);
			model.Save();
			return Ok();
		}

		public JsonResult GetFormula(long modelId, long algType)
		{	
			OMModel model = OMModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
			model.AlgoritmType_Code = (KoAlgoritmType)algType;
			string formula = model.GetFormulaFull(true);
			return Json(new { formula });
		}

		public JsonResult GetModelFactors(long modelId)
		{
			List<ModelFactorDto> factors = OMModelFactor.Where(x => x.ModelId == modelId)
				.SelectAll()
				.Execute()
				.Select(factor => new ModelFactorDto
				{
					Id = factor.Id,
					ModelId = factor.ModelId,
					FactorId = factor.FactorId,
					MarkerId = factor.MarkerId,
					Weight = factor.Weight,
					B0 = factor.B0,
					SignDiv = factor.SignDiv,
					SignAdd = factor.SignAdd,
					SignMarket = factor.SignMarket
				}).ToList();

			List<long?> factorIds = factors.Select(x => x.FactorId).ToList();
			if (factorIds.Count == 0)
			{
				return Json(factors);
			}

			var sqlResult = GetModelFactorNameSql(factorIds);
			foreach (ModelFactorDto factorDto in factors)
			{
				factorDto.Factor = sqlResult[factorDto.FactorId];
			}
					   
			return Json(factors);
		}

		public JsonResult GetFactors(long? tourId)
		{
			List<OMTourFactorRegister> tfrList = OMTourFactorRegister.Where(x => x.TourId == tourId)
				.Select(x => x.RegisterId).Execute();

			List<long?> ids = tfrList.Select(x => x.RegisterId).ToList();

			if (ids.Count == 0)
			{
				return Json(new List<SelectListItem> { });
			}
						
			var result = GetModelFactorNameSql(ids, true).Select(x => new SelectListItem
			{
				Value = x.Key.ToString(),
				Text = x.Value
			});

			return Json(result);
		}

		public ActionResult EditModelFactor(long? id, long modelId)
		{
			ModelFactorDto factorDto;

			if (id.HasValue)
			{
				OMModelFactor factor = OMModelFactor.Where(x => x.Id == id)
					.SelectAll().ExecuteFirstOrDefault();

				factorDto = new ModelFactorDto();
				factorDto.Id = factor.Id;
				factorDto.ModelId = factor.ModelId;
				factorDto.FactorId = factor.FactorId;
				factorDto.MarkerId = factor.MarkerId;
				factorDto.Weight = factor.Weight;
				factorDto.B0 = factor.B0;
				factorDto.SignDiv = factor.SignDiv;
				factorDto.SignAdd = factor.SignAdd;
				factorDto.SignMarket = factor.SignMarket;

				var sqlResult = GetModelFactorNameSql(new List<long?> { factorDto.FactorId });
				factorDto.Factor = sqlResult[factorDto.FactorId];
			}
			else
			{
				factorDto = new ModelFactorDto()
				{
					Id = -1,
					ModelId = modelId,
					FactorId = -1,
					MarkerId = -1
				};
			}
			return View(factorDto);
		}

		private static Dictionary<long?, string> GetModelFactorNameSql(List<long?> idList, bool byRegisterIds = false)
		{
			string ids = string.Join(",", idList.Where(x => x.HasValue));
			string sql = $@"select cra.id, cra.name from core_register_attribute cra ";

			if (byRegisterIds)
			{
				sql += $@"where cra.registerid in ({ids});";
			}
			else
			{
				sql += $@"where cra.id in ({ids});";
			}

			DbCommand command = DBMngr.Main.GetSqlStringCommand(sql);
			DataTable dt = DBMngr.Main.ExecuteDataSet(command).Tables[0];

			Dictionary<long?, string> result = new Dictionary<long?, string>();
			foreach (DataRow dataRow in dt.Rows)
			{
				result.Add(dataRow[0].ParseToLong(), dataRow[1].ParseToString());
			}

			return result;
		}

		public JsonResult GetModelFactorName(long? modelId)
		{			
			OMModel model = OMModel.Where(x => x.Id == modelId)
				.Select(x => x.GroupId).ExecuteFirstOrDefault();

			if (model != null)
			{
				OMTourGroup tourGroup = OMTourGroup.Where(x => x.GroupId == model.GroupId)
					.Select(x => x.TourId).ExecuteFirstOrDefault();

				if (tourGroup != null)
				{					
					List<OMTourFactorRegister> tfrList = OMTourFactorRegister.Where(x => x.TourId == tourGroup.TourId)
						.Select(x => x.RegisterId).Execute();

					List<long?> ids = tfrList.Select(x => x.RegisterId).ToList();
					if (ids.Count == 0)
					{
						return Json(new List<SelectListItem>());
					}

					var result = GetModelFactorNameSql(ids, true).Select(x => new SelectListItem
					{
						Value = x.Key.ToString(),
						Text = x.Value
					});

					return Json(result);
				}
			}

			return Json(new List<SelectListItem>());
		}

		[HttpPost]
		public ActionResult EditModelFactor(OMModelFactor factor)
		{			
			factor.Save();

			OMModel model = OMModel.Where(x => x.Id == factor.ModelId).SelectAll().ExecuteFirstOrDefault();
			model.Formula = model.GetFormulaFull(true);
			model.Save();

			return Ok();
		}

		[HttpPost]
		public ActionResult DeleteModelFactor(long? id)
		{
			OMModelFactor factor = OMModelFactor.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			factor.Destroy();

			OMModel model = OMModel.Where(x => x.Id == factor.ModelId).SelectAll().ExecuteFirstOrDefault();
			model.Formula = model.GetFormulaFull(true);
			model.Save();

			return Json(new { Success = "Удаление выполненно" });
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

		#endregion

		#region Единица оценки

		public ActionResult Unit(long objectId)
		{
			UnitDto dto = new UnitDto();
			dto.ObjectId = objectId;

			OMUnit unit = OMUnit.Where(x => x.ObjectId == objectId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (unit != null)
			{
				dto.Id = unit.Id;

				dto.CadastralNumber = unit.CadastralNumber;
				dto.CadastralBlock = unit.CadastralBlock;
				dto.PropertyType = unit.PropertyType_Code;			
				dto.Square = unit.Square;
				dto.Status = unit.Status_Code;				
				dto.UnitCreationDate = unit.CreationDate;

				dto.UpksPre = unit.UpksPre;
				dto.CadastralCostPre = unit.CadastralCostPre;
				dto.Upks = unit.Upks;
				dto.CadastralCost = unit.CadastralCost;
				dto.StatusRepeatCalc = unit.StatusRepeatCalc_Code;			
				dto.StatusResultCalc = unit.StatusResultCalc_Code;			
				dto.ParentCalcType = unit.ParentCalcType_Code;			
				dto.ParentCalcNumber = unit.ParentCalcNumber;

				OMTask task = OMTask.Where(x => x.Id == unit.TaskId)
					.SelectAll()
					.ExecuteFirstOrDefault();

				if (task != null)
				{
					dto.TaskId = task.Id;

					dto.TourId = task.TourId;
					dto.NoteType = task.NoteType_Code;					
					dto.DocumentId = task.DocumentId;
					dto.TaskCreationDate = task.CreationDate;
					
					dto.ResponseDocId = task.ResponseDocId;

					OMTour tour = OMTour.Where(x => x.Id == dto.TourId)
						.Select(x => x.Year)
						.ExecuteFirstOrDefault();
					dto.Tour = tour?.Year;
				}

				OMGroup group = OMGroup.Where(x => x.Id == unit.GroupId)
					.SelectAll()
					.ExecuteFirstOrDefault();

				if (group != null)
				{
					dto.GroupName = group.GroupName;
					dto.GroupId = group.Id;
				}

				OMCostRosreestr costRosreestr = OMCostRosreestr.Where(x => x.IdObject == unit.Id)
					.SelectAll()
					.ExecuteFirstOrDefault();

				if (costRosreestr != null)
				{
					dto.CostRosreestrId = costRosreestr.Id;
					dto.Datevaluation = costRosreestr.Datevaluation;
					dto.CostValue = costRosreestr.Costvalue;
					dto.DocName = costRosreestr.Docname;
					dto.DocNumber = costRosreestr.Docnumber;
					dto.DocDate = costRosreestr.Docdate;
				}
			}

			bool isEditPermission = true;
			ViewBag.IsEditPermission = isEditPermission;

			return View(dto);
		}

		[HttpPost]
		public ActionResult Unit(UnitDto dto)
		{
			OMUnit unit = OMUnit.Where(x => x.Id == dto.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (unit != null)
			{
				unit.Id = dto.Id.Value;

				unit.CadastralNumber = dto.CadastralNumber;
				unit.CadastralBlock = dto.CadastralBlock;
				unit.PropertyType_Code = dto.PropertyType ?? PropertyTypes.None;
				unit.Square = dto.Square;
				unit.Status_Code = dto.Status ?? KoUnitStatus.None;
				unit.CreationDate = dto.UnitCreationDate;

				unit.UpksPre = dto.UpksPre;
				unit.CadastralCostPre = dto.CadastralCostPre;
				unit.Upks = dto.Upks;
				unit.CadastralCost = dto.CadastralCost;
				unit.StatusRepeatCalc_Code = dto.StatusRepeatCalc ?? KoStatusRepeatCalc.None;
				unit.StatusResultCalc_Code = dto.StatusResultCalc ?? KoStatusResultCalc.None;
				unit.ParentCalcType_Code = dto.ParentCalcType ?? KoParentCalcType.None;
				unit.ParentCalcNumber = dto.ParentCalcNumber;

				unit.GroupId = dto.GroupId;
				unit.Save();
			}

			OMTask task = OMTask.Where(x => x.Id == dto.TaskId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (task != null)
			{
				task.Id = dto.Id.Value;

				task.TourId = dto.TourId;
				task.NoteType_Code = dto.NoteType ?? KoNoteType.None;
				task.DocumentId = dto.DocumentId;
				task.CreationDate = dto.TaskCreationDate;
				task.ResponseDocId = dto.ResponseDocId;

				task.Save();		
			}			

			OMCostRosreestr costRosreestr = OMCostRosreestr.Where(x => x.IdObject == dto.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (costRosreestr != null)
			{
				costRosreestr.Id = dto.CostRosreestrId.Value;
				costRosreestr.Datevaluation = dto.Datevaluation;
				costRosreestr.Costvalue = dto.CostValue;
				costRosreestr.Docname = dto.DocName;
				costRosreestr.Docnumber = dto.DocNumber;
				costRosreestr.Docdate = dto.DocDate;

				costRosreestr.Save();
			}			

			return Json(new { Success = "Успешно сохранено" });
		}

		public JsonResult GetExplication(long id)
		{
			List<UnitExplicationDto> result = new List<UnitExplicationDto>();

			List<OMExplication> explications = OMExplication.Where(x => x.ObjectId == id)
				.SelectAll()
				.Execute();

			if (explications.Any())
			{
				List<long> groupIds = explications.Select(x => x.GroupId).ToList();

				List<OMGroup> groups = OMGroup.Where(x => groupIds.Contains(x.Id))
					.Select(x => x.GroupName)
					.Execute();

				if (groups.Any())
				{
					result = explications.Select(x => new UnitExplicationDto
					{
						Id = x.Id,
						GroupId = x.GroupId,
						Group = groups.Find(y => y.Id == x.GroupId).GroupName,
						Square = x.Square,
						Upks = x.Upks,
						Kc = x.Kc,
						Analog = x.NameAnalog.ToString()
					}).ToList();
				}
			}
			return Json(result);
		}

		public JsonResult GetUnitFactors(long id)
		{
			OMUnit unit = OMUnit.Where(x => x.Id == id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (unit != null)
			{
				OMModel model = OMModel.Where(x => x.Id == unit.ModelId)
					.SelectAll()
					.ExecuteFirstOrDefault();

				if (model != null)
				{
					if (model.ModelFactor.Count == 0)
						model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id).SelectAll().Execute();

					List<UnitFactorsDto> factors = model.ModelFactor.Select(x => new UnitFactorsDto
					{
						Id = x.Id,
						FactorId = x.FactorId,
						Weight = x.Weight
					}).ToList();

					List<long?> factorIds = factors.Select(x => x.FactorId).ToList();
					if (factorIds.Any())
					{
						var sqlResult = GetModelFactorNameSql(factorIds);
						foreach (var factorDto in factors)
						{
							factorDto.Factor = sqlResult[factorDto.FactorId];
						}						
					}

					return Json(factors);
				}
			}

			return Json(new List<UnitFactorsDto>());
		}

		public JsonResult GetFactorsList(long id)
		{
			OMUnit unit = OMUnit.Where(x => x.Id == id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (unit != null)
			{
				var result = GetModelFactorName(unit.ModelId);
				return result;
			}

			return Json(new List<SelectListItem>());
		}

		#endregion
	}

}