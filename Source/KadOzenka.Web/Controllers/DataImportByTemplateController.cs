using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using Core.ErrorManagment;
using Core.Shared.Extensions;
using Core.UI.Registers.CoreUI.Registers;
using Core.Register;
using Core.Register.RegisterEntities;
using ObjectModel.Core.Shared;
using static Core.UI.Registers.CoreUI.Registers.RegistersCommon;
using System.Globalization;
using System.IO;
using System.Reflection;
using Core.Register.Enums;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.Correction;
using KadOzenka.Dal.DataImport.DataImporterByTemplate;
using KadOzenka.Dal.LongProcess.DataImport;
using KadOzenka.Dal.Tasks;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Models.DataImportByTemplate;
using Microsoft.Practices.ObjectBuilder2;
using ObjectModel.KO;
using ObjectModel.Market;
using ConfigurationManager = KadOzenka.Dal.ConfigurationManagers.ConfigurationManager;
using Consts = MarketPlaceBusiness.Common.Consts;


namespace KadOzenka.Web.Controllers
{
    public class DataImportByTemplateController : KoBaseController
	{
		private readonly int _dataCountForBackgroundLoading = 1000;

	    public TaskService TaskService { get; set; }
        private ICodDictionaryService CodDictionaryService { get; }

		public DataImportByTemplateController(TaskService taskService, ICodDictionaryService codDictionaryService)
	    {
	        TaskService = taskService;
            CodDictionaryService = codDictionaryService;
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

            ViewData["Documents"] = GetDocumentsForPartialView();

            return View();
		}

        [SRDFunction(Tag = "")]
		public IActionResult GetTreeAttributes()
		{			
			var source = new List<object>();

			var availableRegisters = ObjectModel.KO.OMObjectsCharacteristicsRegister.Where(x => true)
				.Select(x => x.RegisterId).Execute().Select(x => x.RegisterId.GetValueOrDefault()).ToList();

			var attributesList = BuildAttributesTreeInternal(availableRegisters);

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
			if (model.MainRegisterId != Consts.RegisterId && !model.Document.IsNewDocument && model.Document.IdDocument == null)
			{
				throw new Exception("Поле Документ обязательно для заполнения");
			}

			var columns = model.Columns.Select(x => new DataExportColumn
            {
                AttributrId = x.AttributeId,
                ColumnName = x.ColumnName,
                IsKey = x.IsKey
            }).ToList();
			var dataImporter =
				DataImporterByTemplateFactory.CreateDataImporterByTemplate(model.MainRegisterId);
			dataImporter.ValidateColumns(columns);

			model.Document.ProcessDocument();

            if (model.IsBackgroundDownload)
            {
                long importDataLogId;
                using (var stream = model.File.OpenReadStream())
                {
                    importDataLogId = DataImporterByTemplateLongProcess.AddImportToQueue(model.MainRegisterId,
                        model.RegisterViewId, model.File.FileName, stream, columns, model.Document.IdDocument);
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

                var result = dataImporter.ImportDataFromExcel(excelFile, columns, model.Document.IdDocument);

                var resultFileName = $"{Path.GetFileNameWithoutExtension(model.File.FileName)}_Result{Path.GetExtension(model.File.FileName)}";
                HttpContext.Session.Set(resultFileName, ((MemoryStream)result.ResultFile).ToArray());

                var message = result.Status.GetEnumDescription();
                return Content(JsonConvert.SerializeObject(new { Message = message, Success = result.Status != DataImportStatus.Failed, ResultFileName = resultFileName }), "application/json");
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
            var dictionary = CodDictionaryService.GetDictionary(codId);

            ViewBag.MainRegisterId = dictionary.RegisterId;
            ViewBag.DictionaryId = codId;
            ViewBag.DataCountForBackgroundLoading = _dataCountForBackgroundLoading;

			return View();
        }

		[HttpGet]
		[SRDFunction(Tag = "")]
        public JsonResult BuildAttributesTreeForCod(long registerId)
        {
            var availableRegisters = new List<long> {registerId};

            var attributesTree = BuildAttributesTreeInternal(availableRegisters, withoutPrimaryKeys: true);

			return new JsonResult(attributesTree);
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

            var dataImporter =
	            DataImporterByTemplateFactory.CreateDataImporterByTemplate(model.MainRegisterId);
            dataImporter.ValidateColumns(columns);
			if (model.IsBackgroundDownload)
            {
                long importDataLogId;
                using (var excelStream = new MemoryStream())
                {
                    excelFile.Save(excelStream, SaveOptions.XlsxDefault);
                    excelStream.Seek(0, SeekOrigin.Begin);
					importDataLogId = DataImporterByTemplateLongProcess.AddImportToQueue(model.MainRegisterId,
						"GbuCodJob", model.File.FileName, excelStream, columns, null);
                }

                return new JsonResult(new
                    {Message = "Фоновая загрузка начата.", Success = true, ImportDataLogId = importDataLogId});
            }
            else
            {
	            var result = dataImporter.ImportDataFromExcel(excelFile, columns);

                var resultFileName =
                    $"{Path.GetFileNameWithoutExtension(model.File.FileName)}_Result{Path.GetExtension(model.File.FileName)}";
                HttpContext.Session.Set(resultFileName, ((MemoryStream)result.ResultFile).ToArray());
                var message = result.Status.GetEnumDescription();
                return Content(
                    JsonConvert.SerializeObject(new
                        {Message = message, Success = result.Status != DataImportStatus.Failed, ResultFileName = resultFileName}), "application/json");
            }
        }

		#endregion


		#region Импорт Excel для Задания на оценку

        [HttpGet]
		[SRDFunction(Tag = "")]
        public JsonResult BuildAttributesTreeForTaskDocument()
        {
	        var availableAttributeIds = GetAttributeIdsForTaskCreationFromConfig();
            var availableRegisters = RegisterCache.RegisterAttributes.Where(x => availableAttributeIds.Contains(x.Key))
                .Select(x => (long) x.Value.RegisterId).Distinct().ToList();

	        var unitRegisterId = OMUnit.GetRegisterId();
	        var requiredAttributeIds = Dal.DataImport.DataImporterGknNew.RequiredFieldsForExcelMapping.RequiredAttributeIds;

	        availableRegisters.Add(unitRegisterId);
	        availableAttributeIds.AddRange(requiredAttributeIds);

	        var attributesTree = BuildAttributesTreeInternal(availableRegisters, availableAttributeIds, true);
			attributesTree.Where(x => requiredAttributeIds.Contains(x.AttributeId)).ForEach(x => x.IsRequired = true);
			var sortedAttributes = attributesTree.OrderByDescending(x => x.IsRequired).ThenBy(x => x.Description).ToList();

			return Json(sortedAttributes);
        }


        #region Support Methods

        private List<long> GetAttributeIdsForTaskCreationFromConfig()
        {
            var dataImporterGknConfig = ConfigurationManager.KoConfig.DataImporterGknConfig.GknDataAttributes;

            var allAttributeIdsFromConfig = new List<long>();
            var properties = dataImporterGknConfig.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var section = prop.GetValue(dataImporterGknConfig, null);
                GetIdsFromSection(section, allAttributeIdsFromConfig);
            }

            return allAttributeIdsFromConfig;
        }

        private void GetIdsFromSection(object section, List<long> attributeIds)
		{
	        var properties = section.GetType().GetProperties().Where(x => x.PropertyType != typeof(string)).ToList();
	        foreach (PropertyInfo prop in properties)
	        {
		        if (prop.PropertyType.IsArray)
		        {
			        var array = prop.GetValue(section) as Array;
			        if (array == null)
				        continue;

			        for (var i = 0; i < array.Length; i++)
			        {
				        var arrayElement = array.GetValue(i);
				        GetIdsFromSection(arrayElement, attributeIds);
			        }
		        }
		        else if (prop.PropertyType.IsClass)
		        {
			        var sectionInSection = prop.GetValue(section, null);
			        GetIdsFromSection(sectionInSection, attributeIds);
		        }
		        else
		        {
			        var value = prop.GetValue(section, null)?.ToString();
			        var id = long.TryParse(value, out var val) ? val : (long?)null;
			        if (id != null)
			        {
				        attributeIds.Add(id.Value);
			        }
		        }
	        }
        }

        #endregion

        #endregion


        #region Support Methods

        private List<AttributeInfoForMapping> BuildAttributesTreeInternal(List<long> availableRegisters,
            List<long> availableAttributeIds = null, bool withoutPrimaryKeys = false)
        {
            var attributesTree = new List<AttributeInfoForMapping>();

            foreach (var registerId in availableRegisters)
            {
	            var registerData = RegisterCache.GetRegisterData(registerId.ParseToInt());

	            var registerNode = new AttributeInfoForMapping
	            {
		            ItemId = registerData.Id.ToString(CultureInfo.InvariantCulture),
		            Description = registerData.Description
	            };
	            attributesTree.Add(registerNode);

	            attributesTree.AddRange(RegisterCache.RegisterAttributes.Values
		            .Where(x =>
		            {
			            var pkCondition = true;
			            if (withoutPrimaryKeys)
			            {
				            pkCondition = x.IsPrimaryKey == false;
			            }

			            var attributeIdsCondition = true;
			            if (availableAttributeIds != null)
			            {
				            attributeIdsCondition = availableAttributeIds.Contains(x.Id);
			            }

			            return x.RegisterId == registerData.Id && pkCondition && attributeIdsCondition;

		            }).Select(attributeData => new AttributeInfoForMapping
		            {
			            ItemId = registerData.Id + "_" + attributeData.Id,
			            ParentId = registerData.Id.ToString(CultureInfo.InvariantCulture),
			            Description = attributeData.Name,
			            DocumentType = (int)attributeData.Type,
			            AttributeId = attributeData.Id,
			            ReferenceId = attributeData.ReferenceId ?? 0
		            }).OrderBy(x => x.Description));
            }

            return attributesTree;
        }

        #endregion
    }
}
