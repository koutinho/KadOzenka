using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GemBox.Spreadsheet;
using KadOzenka.Web.Models.DataUpload;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using ObjectModel.KO;
using KadOzenka.Web.Models.Task;
using ObjectModel.Common;
using ObjectModel.Core.TD;
using ObjectModel.Directory.Common;
using Core.Register;
using Core.Register.RegisterEntities;
using ObjectModel.Core.Shared;
using static Core.UI.Registers.CoreUI.Registers.RegistersCommon;
using System.Globalization;

namespace KadOzenka.Web.Controllers
{
    public class DataImportController : BaseController
	{
		private readonly int _dataCountForBackgroundLoading = 1000;

		[HttpGet]
		public IActionResult DataImport(string registerViewId, long? mainRegisterId)
		{
			if (!string.IsNullOrEmpty(registerViewId))
			{
				ViewBag.RegisterViewId = registerViewId;
			}
			if (mainRegisterId.HasValue)
			{
				ViewBag.MainRegisterId = mainRegisterId;
			}
			ViewBag.DataCountForBackgroundLoading = _dataCountForBackgroundLoading;

			return View();
		}
		
		public IActionResult GetTreeAttributes()
		{			
			var source = new List<object>();

			List<RegistersCommon.RegisterTemplateColumn> attributesList = BuildAttributesTree();
					   
			if (attributesList.Any())
			{
				foreach (var attributeItem in attributesList)
				{
					RegisterAttribute attribute = RegisterCache.RegisterAttributes.ContainsKey(attributeItem.AttributeId) ?
								RegisterCache.RegisterAttributes[attributeItem.AttributeId] : null;
					OMReference reference = attribute != null && attribute.ReferenceId.HasValue ?
						OMReference.Where(r => r.ReferenceId == attribute.ReferenceId.Value).Select(r => r.Description).Execute().FirstOrDefault() : null;
					RegisterRelation relation = attribute != null ?
						RegisterCache.RegisterRelations.Select(r => r.Value).FirstOrDefault(r => r.RegAttributeID == attribute.Id) : null;

					var type = Enum.GetName(typeof(RegisterAttributeType), attributeItem.DocumentType);
					var description = attributeItem.ParentId == null ? attributeItem.Description + $" ({attributeItem.ItemId})" : attributeItem.Description;
					source.Add(new
					{
						attributeItem.AttributeId,
						Description = description,
						DescriptionAttribute = attribute != null ? attribute.Description : string.Empty,
						attributeItem.ParentId,
						attributeItem.ItemId,
						ReferenceId = (attributeItem.ReferenceId != 0 ? attributeItem.ReferenceId : (int?)null),
						Type = type,
						DataTypeName = attribute != null ? attribute.Type.GetEnumDescription() : string.Empty,
						ReferenceName = reference != null ? reference.Description : string.Empty,
						IsPrimaryKey = attribute?.IsPrimaryKey ?? false,
						ForeignKey = (relation != null && RegisterCache.Registers[relation.ParentRegID] != null) ?
									RegisterCache.Registers[relation.ParentRegID].Description : string.Empty,
						IsVirtual = attribute?.IsVirtual ?? false,
						ColumnDbName = attribute != null ? (attribute.ValueField + (attribute.CodeField.IsNotEmpty() ?
									string.Format(" ({0})", attribute.CodeField) : string.Empty)) : string.Empty
					});
				}
			}

			return Json(source);
		}

		public static List<RegisterTemplateColumn> BuildAttributesTree()
		{
			var attributesTree = new List<RegisterTemplateColumn>();

			List<long> avaliableRegisters = ObjectModel.KO.OMObjectsCharacteristicsRegister.Where(x => true)
			.Select(x => x.RegisterId).Execute().Select(x => x.RegisterId).Cast<long>().ToList();

			foreach (long registerId in avaliableRegisters)
			{
				try
				{
					RegisterData registerData = RegisterCache.GetRegisterData(registerId.ParseToInt());

					RegisterTemplateColumn registerNode = new RegisterTemplateColumn
					{
						ItemId = registerData.Id.ToString(CultureInfo.InvariantCulture),
						Description = registerData.Description
					};

					attributesTree.Add(registerNode);

					attributesTree.AddRange(RegisterCache.RegisterAttributes.Values
						.Where(x => x.RegisterId == registerData.Id).Select(attributeData => new RegisterTemplateColumn
						{
							ItemId = registerData.Id + "_" + attributeData.Id,
							ParentId = registerData.Id.ToString(CultureInfo.InvariantCulture),
							Description = attributeData.Name,
							DocumentType = (int)attributeData.Type,
							AttributeId = attributeData.Id,
							ReferenceId = attributeData.ReferenceId.HasValue ? attributeData.ReferenceId.Value : 0,
						}).OrderBy(x => x.Description));
				}
				catch (Exception ex)
				{
					ErrorManager.LogError(ex);
					continue;
				}
			}

			return attributesTree;
		}

		[HttpPost]
		public ActionResult ParseFileColumns(List<IFormFile> files)
		{
			if (files == null || files.Count == 0)
			{
				return EmptyResponse();
			}

			try
			{
				var file = files.FirstOrDefault();
				var columnsNames = new List<object>();
				int dataRowCount;
				using (var stream = file.OpenReadStream())
				{
					var excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
					var ws = excelFile.Worksheets[0];
					var headerRow = ws.Rows[0];
					dataRowCount = ws.Rows.Count - 1;
					columnsNames
						.AddRange(headerRow.AllocatedCells.Where(x => x.Value != null)
							.Select((x, index) => new { Id = index, Name = x.Value.ToString() }));
					if (columnsNames.Count == 0)
					{
						throw new Exception("Выбран пустой файл");
					}
				}

				return Content(JsonConvert.SerializeObject(new {DataCount = dataRowCount, ColumnsNames = columnsNames}),
					"application/json");
			}
			catch (Exception ex)
			{
				return Content(ex.Message);
			}
		}

		[HttpPost]
		public IActionResult ImportDataFromExcel(int mainRegisterId, IFormFile file, List<DataColumnDto> columns)
		{

			ExcelFile excelFile;
			using (var stream = file.OpenReadStream())
			{
				excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
			}

			DataImporterCommon.ImportDataFromExcel(mainRegisterId, excelFile, columns.Select(x => new DataExportColumn
				{ AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey }).ToList());

			return NoContent();
		}

		[HttpPost]
		public IActionResult AddImportToQueue(int mainRegisterId, string registerViewId, IFormFile file, List<DataColumnDto> columns)
		{

			using (var stream = file.OpenReadStream())
			{
				DataImporterCommon.AddImportToQueue(mainRegisterId, registerViewId, file.FileName, stream,
					columns.Select(x => new DataExportColumn
						{ AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey }).ToList());
			}

			return NoContent();
		}

		[HttpGet]
		public ActionResult ImportGkn()
		{
			TaskModel dto = new TaskModel();
			return View(dto);
		}

		[HttpPost]
		[RequestSizeLimit(2000000000)]
        public ActionResult ImportGkn(List<IFormFile> files, TaskModel dto)
		{
			//SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_IMPORT, true, false, true);

			OMInstance instance = new OMInstance
			{
				RegNumber = dto.IncomingDocumentRegNumber,
				Description = dto.IncomingDocumentDescription,
				CreateDate = dto.IncomingDocumentDate ?? DateTime.Now
			};
			instance.Save();

			OMTask task = new OMTask
			{
				TourId = dto.TourYear,
				DocumentId = instance.Id,
				CreationDate = DateTime.Now,
				EstimationDate = dto.EstimationDate,
				NoteType_Code = dto.NoteType ?? ObjectModel.Directory.KoNoteType.None,
				Status_Code = ObjectModel.Directory.KoTaskStatus.InWork
			};
			task.Save();

			try
			{
				foreach (var file in files)
				{
					using (var stream = file.OpenReadStream())
					{
						DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", file.FileName, stream, OMTask.GetRegisterId(), task.Id);
					}
				}
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			string Msg = "Задание на оценку успешно создано. ";
			if (files.Any())
			{
				Msg += "Загрузка добавлена в очередь, по результатам загрузки будет отправлено сообщение";
			}

			return Json(new { Msg });
		}

		[HttpPost]
		[RequestSizeLimit(2000000000)]
        public ActionResult ImportGknFromTask(List<IFormFile> files, long taskId)
		{
			try
			{
				foreach (var file in files)
				{
				    using (var stream = file.OpenReadStream())
				    {
				        DataImporterGknLongProcess.AddImportToQueue(OMTask.GetRegisterId(), "Tasks", file.FileName, stream, OMTask.GetRegisterId(), taskId);
                    }
                }
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return NoContent();
		}

	    [HttpGet]
	    [ActionName("RestartGknImports")]
	    public ActionResult RestartGknImportsConfirm(long? importId)
	    {
	        var currentImportsId = RegistersVariables.CurrentList?.ToList();
	        if (currentImportsId == null || currentImportsId.Count == 0)
	        {
	            currentImportsId = importId.HasValue
	                ? new List<long> { importId.Value }
	                : new List<long>();
            }
	           
	        if (currentImportsId.Count == 0)
	        {
	            return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
	            {
	                Message = "Не выбраны записи для перезапуска.",
	                Icon = ModalDialogDetails.IconType.Warning,
	                Buttons = ModalDialogDetails.ButtonType.Ok,
	                Action = ModalDialogDetails.ActionType.Reload
	            });
	        }

            var addedOrRunningSelectedImports = OMImportDataLog
	            .Where(x => currentImportsId.Contains(x.Id) && (x.Status_Code == ImportStatus.Added || x.Status_Code == ImportStatus.Running))
                .Select(x => x.Status)
                .Select(x => x.Status_Code)
                .Execute();
            if (addedOrRunningSelectedImports.Count > 0)
	        {
	            return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
	            {
	                Message = $"Выбраны записи со статусом '{ImportStatus.Added.GetEnumDescription()}' или '{ImportStatus.Running.GetEnumDescription()}'",
	                Icon = ModalDialogDetails.IconType.Warning,
	                Buttons = ModalDialogDetails.ButtonType.Ok,
	                Action = ModalDialogDetails.ActionType.Reload
	            });
	        }

            var model = new ModalDialogDetails
            {
                Message = "Вы уверены, что хотите перезапустить выбранные записи импорта?",
            };

	        return View("~/Views/Shared/ModalDialogDetails.cshtml", model);
	    }

        [HttpPost]
        [ActionName("RestartGknImports")]
        public ActionResult RestartGknImports(long? importId)
	    {
	        var currentImportsId = RegistersVariables.CurrentList?.ToList();
	        if (currentImportsId == null || currentImportsId.Count == 0)
	        {
	            currentImportsId = importId.HasValue
	                ? new List<long> { importId.Value }
	                : new List<long>();
	        }
            try
            {
                if (currentImportsId.Count == 0)
                {
                    throw new Exception("Не выбраны записи для перезапуска.");
                }

                using (var ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (var id in currentImportsId)
                    {
                        DataImporterGknLongProcess.RestartImport(id);
                    }

                    ts.Complete();
                }
            }
	        catch (Exception e)
	        {
	            long errorId = ErrorManager.LogError(e);
                return Json(new
	            {
	                type = "Error",
	                message =  $"{e.Message} (Подробнее в журнале № {errorId})"
                });
            }

	        return Json(new
	        {
	            type = "Success",
	            message = "Выбранные записи импорта успешно перезапущены",
	            reload = true
	        });
        }
    }
}
