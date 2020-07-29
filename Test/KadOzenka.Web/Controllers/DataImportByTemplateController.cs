﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using Core.UI.Registers.CoreUI.Registers;
using Core.Register;
using Core.Register.RegisterEntities;
using ObjectModel.Core.Shared;
using static Core.UI.Registers.CoreUI.Registers.RegistersCommon;
using System.Globalization;
using System.IO;
using Core.Register.Enums;
using KadOzenka.Dal.Tasks;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.DataImportByTemplate;
using ObjectModel.Core.TD;
using ObjectModel.Market;

namespace KadOzenka.Web.Controllers
{
    public class DataImportByTemplateController : BaseController
	{
		private readonly int _dataCountForBackgroundLoading = 1000;

	    public TaskService TaskService { get; set; }

	    public DataImportByTemplateController(TaskService taskService)
	    {
	        TaskService = taskService;
	    }

        [HttpGet]
        [SRDFunction(Tag = "")]
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

            ViewData["Documents"] = OMInstance.Where(x => x)
                .Select(x => x.Description)
                .Select(x => x.Id)
                .Execute().Select(x => new
                {
                    Text = x.Description,
                    Value = x.Id
                }).ToList();

            return View();
		}

        [SRDFunction(Tag = "")]
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

        [SRDFunction(Tag = "")]
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
        [SRDFunction(Tag = "")]
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
        [SRDFunction(Tag = "")]
		public IActionResult ImportDataFromExcel(ImportGbuObjectModel model)
		{
			if (model.MainRegisterId != OMCoreObject.GetRegisterId() && !model.Document.IsNewDocument && model.Document.IdDocument == null)
			{
				throw new Exception("Поле Документ обязательно для заполнения");
			}

			var columns = model.Columns.Select(x => new DataExportColumn
            {
                AttributrId = x.AttributeId,
                ColumnName = x.ColumnName,
                IsKey = x.IsKey
            }).ToList();

			if (model.Document.IsNewDocument)
			{
				var idDocument = TaskService.CreateDocument(model.Document.NewDocumentRegNumber,
					model.Document.NewDocumentName, model.Document.NewDocumentDate);
				if (idDocument == 0)
				{
					throw new Exception("Не корректные данные для создания нового документа");
				}

				model.Document.IdDocument = idDocument;
			}
			var documentId =  model.Document.IdDocument;

            if (model.IsBackgroundDownload)
            {
                long importDataLogId;
                using (var stream = model.File.OpenReadStream())
                {
                    importDataLogId = DataImporterByTemplate.AddImportToQueue(model.MainRegisterId,
                        model.RegisterViewId, model.File.FileName, stream, columns, documentId);
                }

                return new JsonResult(new { Message = "Фоновая загрузка начата.", Success = true, ImportDataLogId = importDataLogId });
            }
            else
            {
                ExcelFile excelFile;
                using (var stream = model.File.OpenReadStream())
                {
                    excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
                }

                var resultFile = (MemoryStream)DataImporterByTemplate.ImportDataFromExcel(model.MainRegisterId, excelFile, columns, documentId, out var success);

                var resultFileName = $"{Path.GetFileNameWithoutExtension(model.File.FileName)}_Result{Path.GetExtension(model.File.FileName)}";
                HttpContext.Session.Set(resultFileName, resultFile.ToArray());

                var message = success ? "Загрузка завершена." : "Не удалось выполнить загрузку. Файл содержит некорректные данные";
                return Content(JsonConvert.SerializeObject(new { Message = message, Success = success, ResultFileName = resultFileName }), "application/json");
            }
        }

        [HttpGet]
        [SRDFunction(Tag = "")]
		public ActionResult DownloadExcelResultFile(string resultFileName)
        {
            var fileContent = HttpContext.Session.Get(resultFileName);
            if (fileContent == null)
            {
                return new EmptyResult();
            }

            HttpContext.Session.Remove(resultFileName);
            StringExtensions.GetFileExtension(RegistersExportType.Xlsx, out string fileExtensiton, out string contentType);

            return File(fileContent, contentType, resultFileName);
        }

		#region GbuCodDictionaryImport

        [HttpGet]
        [SRDFunction(Tag = "")]
		public IActionResult CodDictionaryImport(long codId)
        {
			ViewBag.RegisterViewId = "GbuCodDictionary";
            ViewBag.MainRegisterId = 214;
            ViewBag.DictionaryId = codId;
            ViewBag.DataCountForBackgroundLoading = _dataCountForBackgroundLoading;

			return View();
        }

        [HttpPost]
        [SRDFunction(Tag = "")]
        public IActionResult ImportCodDictionaryDataFromExcel(ImportGbuCodDictionaryModel model)
        {
            var columns = model.Columns.Select(x => new DataExportColumn
            {
                AttributrId = x.AttributeId,
                ColumnName = x.ColumnName,
                IsKey = x.IsKey
            }).ToList();

            ExcelFile excelFile;
            using (var stream = model.File.OpenReadStream())
            {
                excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
            }

            // Инжектим в файл шапку и номер выбранного справочника
            var cols = excelFile.Worksheets[0].CalculateMaxUsedColumns();
            excelFile.Worksheets[0].Rows[0].Cells[cols].SetValue($"Номер справочника");
            var cellRange = excelFile.Worksheets[0].GetUsedCellRange(true);
            var rows = cellRange.LastRowIndex;
            for (int curRow = 1; curRow < rows + 1; curRow++)
            {
                excelFile.Worksheets[0].Rows[curRow].Cells[cols].SetValue(model.DictionaryId);
            }

            // Добавляем связь колонки и атрибута
            columns.Add(new DataExportColumn
            {
                AttributrId = 21400200,
                ColumnName = "Номер справочника",
                IsKey = false
            });

            if (model.IsBackgroundDownload)
            {
                long importDataLogId;
                using (var excelStream = new MemoryStream())
                {
                    excelFile.Save(excelStream, SaveOptions.XlsxDefault);
                    excelStream.Seek(0, SeekOrigin.Begin);
					importDataLogId = DataImporterByTemplate.AddImportToQueue(model.MainRegisterId,
                        model.RegisterViewId, model.File.FileName, excelStream, columns, null);
                }

                return new JsonResult(new
                    {Message = "Фоновая загрузка начата.", Success = true, ImportDataLogId = importDataLogId});
            }
            else
            {
                var resultFile = (MemoryStream) DataImporterByTemplate.ImportDataFromExcel(model.MainRegisterId, excelFile,
                    columns, null, out var success);
                var resultFileName =
                    $"{Path.GetFileNameWithoutExtension(model.File.FileName)}_Result{Path.GetExtension(model.File.FileName)}";
                HttpContext.Session.Set(resultFileName, resultFile.ToArray());
                var message = success
                    ? "Загрузка завершена."
                    : "Не удалось выполнить загрузку. Файл содержит некорректные данные";
                return Content(
                    JsonConvert.SerializeObject(new
                        {Message = message, Success = success, ResultFileName = resultFileName}), "application/json");
            }
        }

        #endregion
	}
}