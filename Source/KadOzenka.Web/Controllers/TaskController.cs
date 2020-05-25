using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using KadOzenka.Web.Models.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Directory;
using ObjectModel.KO;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.Model;
using KadOzenka.Dal.Oks;
using KadOzenka.Dal.Tasks;
using KadOzenka.Web.Models.DataImporterLayout;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ObjectModel.Core.Register;
using ObjectModel.Gbu.ExportAttribute;
using KadOzenka.Dal.Models.Task;
using KadOzenka.Dal.Tours;

namespace KadOzenka.Web.Controllers
{
	public class TaskController : KoBaseController
	{
		public TaskService TaskService { get; set; }
		public ModelService ModelService { get; set; }
		public DataImporterService DataImporterService { get; set; }
		public GbuObjectService GbuObjectService { get; set; }
		public TourFactorService TourFactorService { get; set; }
        public GroupService GroupService { get; set; }

        public TaskController()
		{
			TaskService = new TaskService();
			ModelService = new ModelService();
			DataImporterService = new DataImporterService();
			GbuObjectService = new GbuObjectService();
		    TourFactorService = new TourFactorService();
            GroupService = new GroupService();

        }

		#region Карточка задачи

		[HttpGet]
		public ActionResult TaskCard(long taskId)
		{
			var taskDto = TaskService.GetTaskById(taskId);
			if (taskDto == null)
				return NotFound();

			var taskModel = TaskEditModel.ToEditModel(taskDto);

			return View(taskModel);
		}

	    [HttpPost]
	    public ActionResult TaskCard(TaskEditModel model)
	    {
	        if (!ModelState.IsValid)
	        {
	            return GenerateMessageNonValidModel();
	        }

            try
            {
                TaskService.UpdateTaskData(model.ToDto());
            }
            catch (Exception e)
            {
                return SendErrorMessage(e.Message);
            }

            return Json(new { Success = "Сохранено успешно" });
	    }

        [HttpGet]
		public ActionResult GetXmlDocuments([DataSourceRequest]DataSourceRequest request, long taskId)
		{
			var importDataLogsDto = DataImporterService.GetCommonDataLog(OMTask.GetRegisterId(), taskId);

			var result = new List<DataImporterLayoutDto>();
			importDataLogsDto.ForEach(x =>
			{
				result.Add(new DataImporterLayoutDto
				{
					Id = x.Id,
					DateCreated = x.CreationDate,
					UserName = x.Author,
					TemplateFileName = x.FileName,
				    NumberOfImportedObjects = x.NumberOfImportedObjects,
				    TotalNumberOfObjects = x.TotalNumberOfObjects,
                });
			});

			return Json(result.ToDataSourceResult(request));
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

        #endregion

        #region Перенос атрибутов

        [HttpGet]
		public ActionResult TransferAttributes(bool create = false)
		{
			var model = new ExportAttributesModel();

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

			ViewData["KoAttributes"] = new List<string>();

			model.CreateAttributes = create;

			return View(model);
		}

		public JsonResult GetTasksByTour(long tourId)
		{
			var tasks = TaskService.GetTasksByTour(tourId);

			var models = tasks.Select(x => new SelectListItem
			{
				Value = x.TaskId.ToString(),
				Text = TaskService.GetTemplateForTaskName(x)
			});

			return Json(models);
		}

		public JsonResult GetKoAttributes(long tourId, int objectType, List<long> exceptedAttributes)
		{
			var koAttributes = TourFactorService.GetTourAttributes(tourId, (ObjectType)objectType);
			if (exceptedAttributes != null && exceptedAttributes.Count > 0)
			{
				koAttributes = koAttributes.Where(x => !exceptedAttributes.Contains(x.Id)).ToList();
			}

			var models = koAttributes.Select(x => new
			{
				Value = x.Id,
				Text = x.Name
			}).AsEnumerable();

			return Json(models);
		}

		[HttpPost]
		public void TransferAttributes(ExportAttributesModel model)
		{
			if (model.TaskFilter == null || model.TaskFilter.Count == 0)
				throw new ArgumentException("Не выбрано задание на оценку, операция прервана");

			GbuExportAttributeSettings settings;
			if (model.CreateAttributes)
			{
				settings = model.ToGbuExportAndCreateAttributeSettings();
			}
			else
			{
				settings = model.ToGbuExportAttributeSettings();
				ValidateExportAttributeItems(settings.Attributes);
			}

            ExportAttributeToKoProcess.AddProcessToQueue(settings);
        }		

		public ActionResult GetRowExport([FromForm] int rowNumber, [FromForm] long tourId, [FromForm] int objectType, [FromForm] bool create)
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

			var koAttributes = TourFactorService.GetTourAttributes(tourId, (ObjectType)objectType) ?? new List<OMAttribute>();

			ViewData["KoAttributes"] = koAttributes.Select(x => new
			{
				Value = x.Id,
				Text = x.Name
			}).AsEnumerable();

			ViewData["RowNumber"] = rowNumber.ToString();
			ViewData["CreateAttributes"] = create;

			return PartialView("/Views/Task/PartialTransferAttributeRow.cshtml", new PartialExportAttributesRowModel());
		}


		#region Support Methods

		public void ValidateExportAttributeItems(List<ExportAttributeItem> item)
		{
			var message = new StringBuilder("Один из параметров не выбран, строки №:");
			var withErrors = false;

			for (var i = 0; i < item.Count; i++)
			{
				var current = item[i];

				if (!((current.IdAttributeGBU == 0 && current.IdAttributeKO == 0) ||
					  (current.IdAttributeGBU != 0 && current.IdAttributeKO != 0)))
				{
					message.Append($"{i + 1}, ");
					withErrors = true;
				}
			}

			if (withErrors)
				throw new ArgumentException(message.ToString());
		}

		#endregion

		#endregion

		#region Модель

		public ActionResult Model(long groupId, bool isPartial)
		{
			var modelDto = ModelService.GetModelByGroupId(groupId);
			var model = ModelModel.ToModel(modelDto);

			if (isPartial)
			{
				model.IsPartial = true;
				return PartialView(model);
			}
			
			return View(model);
		}

		[HttpPost]
		public ActionResult Model(ModelModel model)
		{
			var omModel = ModelService.GetModelById(model.Id);

			omModel.Name = model.Name;
			omModel.Description = model.Description;
			omModel.AlgoritmType_Code = model.AlgorithmTypeCode;
			omModel.A0 = model.A0;

			omModel.CalculationMethod_Code = model.CalculationTypeCode == KoCalculationType.Comparative
				? model.CalculationMethodCode
				: KoCalculationMethod.None;

			omModel.CalculationType_Code = model.CalculationTypeCode;
			omModel.Formula = omModel.GetFormulaFull(true);
			omModel.Save();
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

		#region Единица оценки

		public ActionResult Unit(long objectId)
		{
			OMUnit unit = OMUnit.Where(x => x.ObjectId == objectId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			UnitDto dto = UnitDto.ToDto(unit);

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
				//task.DocumentId = dto.DocumentId;
				task.CreationDate = dto.TaskCreationDate;
				//task.ResponseDocId = dto.ResponseDocId;

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

        #region Расчет кадастровой стоимости

        [HttpGet]
        public ActionResult CalculateCadastralPrice()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CalculateCadastralPrice(CadastralPriceCalculationModel model)
        {
            if (model.TaskFilter == null || model.TaskFilter.Count == 0)
                throw new ArgumentException("Не выбраны задания на оценку");

            if (!model.IsAllGroups && (model.SubGroups == null || model.SubGroups.Count == 0))
                throw new ArgumentException("Не выбраны группы");

            var settings = CadastralPriceCalculationModel.UnMap(model);
            OMGroup.CalculateSelectGroup(settings);

            return Json(new {Message = "Операция выполнена успешно"});
        }

        [HttpGet]
        public ActionResult CalculationOrderSettings()
        {
            return View();
        }

        public JsonResult GetCalculationOrderSettings(long tourId, bool isParcel)
        {
            var settings = GroupService.GetCalculationSettings(tourId, isParcel);
            var models = settings.Select(CadastralPriceCalculationSettingsModel.ToModel).ToList();

            return Json(models);
        }

        #endregion


        #region Загрузка графических факторов из РЕОН

        [HttpGet]
        public ActionResult DownloadGraphicFactorsFromReon(long taskId)
        {
            KoFactorsFromReon.AddProcessToQueue(taskId);
            return View();
        }

        #endregion


        public ActionResult DataMapping(long taskId)
		{
			OMTask task = OMTask.Where(x => x.Id == taskId)				
				.ExecuteFirstOrDefault();

			if (task == null)
			{
				throw new Exception("Не найдено задание на оценку с ИД=" + taskId);
			}

			return View(taskId);
		}

		public JsonResult GetTaskObjects(long taskId)
		{
			List<OMUnit> unitList = OMUnit.Where(x => x.TaskId == taskId)
				.Select(x => x.ObjectId)
				.Select(x => x.CadastralNumber)
				.Execute();

			var objectList = unitList.Select(x => new { x.ObjectId, x.CadastralNumber }).ToList();
			return Json(objectList);
		}

		public JsonResult GetDataMapping(long taskId, long objectId)
		{			
			OMTask task = OMTask.Where(x => x.Id == taskId)
				.Select(x => x.CreationDate)
				.Select(x => x.EstimationDate)
				.Select(x => x.DocumentId)
				.ExecuteFirstOrDefault();
					
			List<DataMappingDto> list = new List<DataMappingDto>();

			TaskService.FetchGbuData(list, objectId, task, "num");
			TaskService.FetchGbuData(list, objectId, task, "dt");
			TaskService.FetchGbuData(list, objectId, task, "txt");

			list = list.Where(x => !Object.Equals(x.Value, x.OldValue)).ToList();

			return Json(list);
		}
	}	
}