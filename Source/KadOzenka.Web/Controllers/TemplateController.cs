using System;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.GbuObject;
using KadOzenka.Web.Models.Task;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Directory.Common;
using ObjectModel.KO;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
	public class TemplateController : KoBaseController
	{
		private TemplateService _templateService;
		private const string TourSeparator = "$";

		public TemplateController(TemplateService templateService, IRegisterCacheWrapper registerCacheWrapper,
			IGbuObjectService gbuObjectService)
			: base(gbuObjectService, registerCacheWrapper)
		{
			_templateService = templateService;
		}

		#region Getting Templates

		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public JsonResult GetTemplates(DataFormStorege formStorageType)
		{
			var result = _templateService.GetTemplates(formStorageType)
				.Select(x => new {Text = x.TemplateName ?? "", Value = x.Id.ToString(), IsCommon = x.IsCommon.GetValueOrDefault()}).OrderBy(x => x.Text).ToList();
			return Json(result);

		}

		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
		public JsonResult GetTemplatesForTransferAttributes(bool isCreateMode)
		{
			var formType = isCreateMode
				? DataFormStorege.TransferAttributesWithCreate
				: DataFormStorege.TransferAttributesWithoutCreate;
			var result = _templateService.GetTemplates(formType)
				.Select(x => new {Text  = x.TemplateName ?? string.Empty, Value = x.Id.ToString(), IsCommon = x.IsCommon.GetValueOrDefault() }).OrderBy(x => x.Text).ToList();

			return Json(result);
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public JsonResult GetTemplatesFactors(long taskId)
		{
			var task = OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();
			if (task == null) throw new ArgumentNullException($"Задача с id {taskId} не найдена");
			var result = _templateService.GetTemplates(DataFormStorege.ExportFactorsByTask)
				.Where(x => x.TemplateName.StartsWith(task.TourId + TourSeparator))
				.Select(x => new {Text = x.TemplateName?.Split(TourSeparator, 2)?[1] ?? "", Value = x.Id.ToString(), IsCommon = x.IsCommon.GetValueOrDefault() }).OrderBy(x => x.Text).ToList();

			return Json(result);
		}

		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public JsonResult GetTemplate(int id)
		{
			if (id == 0)
			{
				return Json(new { error = "Ид равен 0" });
			}

			try
			{
				var storage = OMDataFormStorage.Where(x =>
						x.Id == id)
					.SelectAll().ExecuteFirstOrDefault();

				if (storage != null && storage.FormType_Code == DataFormStorege.Normalisation)
				{
					var nObj = storage.Data.DeserializeFromXml<GroupingObject>();
					return Json(new { data = JsonConvert.SerializeObject(nObj) });
				}

				if (storage != null && storage.FormType_Code == DataFormStorege.Harmonization)
				{
					var hObj = storage.Data.DeserializeFromXml<HarmonizationViewModel>();
					return Json(new { data = JsonConvert.SerializeObject(hObj) });
				}

				if (storage != null && storage.FormType_Code == DataFormStorege.UnloadingFromDict)
				{
					var unObj = storage.Data.DeserializeFromXml<UnloadingFromDicViewModel>();
					return Json(new { data = JsonConvert.SerializeObject(unObj) });
				}

				if (storage != null && storage.FormType_Code == DataFormStorege.EstimatedGroup)
				{
					var unObj = storage.Data.DeserializeFromXml<EstimatedGroupViewModel>();
					return Json(new { data = JsonConvert.SerializeObject(unObj) });
				}

				if (storage != null && storage.FormType_Code == DataFormStorege.Inheritance)
				{
					var unObj = storage.Data.DeserializeFromXml<InheritanceViewModel>();
					return Json(new { data = JsonConvert.SerializeObject(unObj) });
				}

				if (storage != null && (storage.FormType_Code == DataFormStorege.TransferAttributesWithCreate || storage.FormType_Code == DataFormStorege.TransferAttributesWithoutCreate))
				{
					var unObj = storage.Data.DeserializeFromXml<ExportAttributesModel>();
					return Json(new { data = JsonConvert.SerializeObject(unObj) });
				}

				if (storage != null && storage.FormType_Code == DataFormStorege.ExportFactorsByTask)
				{
					var nObj = storage.Data.DeserializeFromXml<FactorsDownloadByTaskModel>();
					return Json(new { data = JsonConvert.SerializeObject(nObj) });
				}
			}
			catch (Exception e)
			{
				return Json(new { error = $"Ошибка: {e.Message}" });
			}

			return Json(new { error = "Не найдено соответсвующего типа формы" });
		}

		#endregion

		#region Save Template

		[HttpPost]
		[JsonExceptionHandler]
		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public JsonResult SaveTemplateGroupingObject(string nameTemplate, bool isCommon, [FromForm]GroupingObject model, long? id = null)
		{
			return SaveTemplate(nameTemplate, isCommon, DataFormStorege.Normalisation, model.SerializeToXml(), id);
		}

		[HttpPost]
		[JsonExceptionHandler]
		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public JsonResult SaveTemplateHarmonizationObject(string nameTemplate, bool isCommon, [FromForm]HarmonizationViewModel viewModel, long? id = null)
		{
			return SaveTemplate(nameTemplate, isCommon, DataFormStorege.Harmonization, viewModel.SerializeToXml(), id);
		}

		[HttpPost]
		[JsonExceptionHandler]
		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public JsonResult SaveTemplateUnloading(string nameTemplate, bool isCommon, [FromForm]UnloadingFromDicViewModel viewModel, long? id = null)
		{
			return SaveTemplate(nameTemplate, isCommon, DataFormStorege.UnloadingFromDict, viewModel.SerializeToXml(), id);
		}

		[HttpPost]
		[JsonExceptionHandler]
		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public JsonResult SaveTemplateEstimatedGroupObject(string nameTemplate, bool isCommon, [FromForm]EstimatedGroupViewModel model, long? id = null)
		{
			return SaveTemplate(nameTemplate, isCommon, DataFormStorege.EstimatedGroup, model.SerializeToXml(), id);
		}

		[HttpPost]
		[JsonExceptionHandler]
		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public JsonResult SaveTemplateInheritance(string nameTemplate, bool isCommon, [FromForm] InheritanceViewModel model, long? id = null)
		{
			return SaveTemplate(nameTemplate, isCommon, DataFormStorege.Inheritance, model.SerializeToXml(), id);
		}

		[HttpPost]
		[JsonExceptionHandler]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
		public JsonResult SaveTemplateForTransferAttributesWithCreate(string nameTemplate, bool isCommon, [FromForm]ExportAttributesModel viewModel, long? id = null)
		{
			return SaveTemplate(nameTemplate, isCommon, DataFormStorege.TransferAttributesWithCreate, viewModel.SerializeToXml(), id);
		}

		[HttpPost]
		[JsonExceptionHandler]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS_TRANSFER_ATTRIBUTES)]
		public JsonResult SaveTemplateForTransferAttributesWithoutCreate(string nameTemplate, bool isCommon, [FromForm]ExportAttributesModel viewModel, long? id = null)
		{
			return SaveTemplate(nameTemplate, isCommon, DataFormStorege.TransferAttributesWithoutCreate, viewModel.SerializeToXml(), id);
		}

		[HttpPost]
		[JsonExceptionHandler]
		[SRDFunction(Tag = SRDCoreFunctions.KO_TASKS)]
		public JsonResult SaveTemplateFactors(string nameTemplate, bool isCommon, [FromForm]long taskId, [FromForm]bool isOks, [FromForm]string[] selectedAttributes, long? id = null)
		{
			var task = OMTask.Where(x => x.Id == taskId).SelectAll().ExecuteFirstOrDefault();
			if (task == null) throw new ArgumentNullException($"Задача с id {taskId} не найдена");

			var obj = new FactorsDownloadByTaskModel { TaskId = taskId, isOks = isOks, Attributes = selectedAttributes };
			return SaveTemplate(task.TourId + TourSeparator + nameTemplate, isCommon, DataFormStorege.ExportFactorsByTask, obj.SerializeToXml(), id);
		}

		#endregion

		#region Remove Template

		[SRDFunction(Tag = SRDCoreFunctions.GBU_OBJECTS)]
		public JsonResult RemoveTemplate(int id)
		{
			try
			{
				_templateService.RemoveTemplate(id);
			}
			catch (Exception e)
			{
				return Json(new { error = $"Ошибка: {e.Message}" });
			}

			return Json(new { success = true });
		}

		#endregion Remove Template

		private JsonResult SaveTemplate(string nameTemplate, bool isCommon, DataFormStorege formType, string serializeData, long? id = null)
		{
			if (id.HasValue)
			{
				_templateService.UpdateTemplate(id.Value, nameTemplate, isCommon, formType, serializeData);
			}
			else
			{
				_templateService.CreateTemplate(nameTemplate, isCommon, formType, serializeData);
			}

			return Json(new { success = true });
		}
	}
}
