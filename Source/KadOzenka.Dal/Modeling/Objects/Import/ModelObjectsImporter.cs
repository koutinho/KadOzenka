using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Core.ErrorManagment;
using Core.Register;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport.DataImportKoFactory.ImportKoFactoryCommon;
using KadOzenka.Dal.Modeling.Entities;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Modeling;
using Serilog;

namespace KadOzenka.Dal.Modeling.Objects.Import
{
	public class ModelObjectsImporter
	{
		protected readonly ILogger _log = Log.ForContext<ModelObjectsImporter>();
		private object _locker;
		protected const string  LoggerBasePhrase = "Импорт объектов моделирования:";
		public int MaxRowsCountInFileForUpdating { get; set; } = 1;
		public int CurrentRowIndexInFileForUpdating { get; set; }


		public ModelObjectsImporter()
		{
			_locker = new object();
		}

		
		public Stream ChangeObjects(bool isUpdating, ExcelFile file, ModelObjectsConstructor modelObjectsConstructor)
		{
			_log.Debug("{LoggerBasePhrase} старт", LoggerBasePhrase);

			var sheet = file.Worksheets[0];
			var maxColumnIndex = DataExportCommon.GetLastUsedColumnIndex(sheet) + 1;
			sheet.Rows[0].Cells[maxColumnIndex].SetValue("Результат обработки");

			var objectsFromExcel = GetObjectsFromFile(sheet, modelObjectsConstructor);
			_log.Debug("{LoggerBasePhrase} в файле {RowsCount} строк", LoggerBasePhrase, MaxRowsCountInFileForUpdating);

			IModelObjectsImporter importer;
			if (isUpdating)
				importer = new ModelObjectsImporterForUpdating(objectsFromExcel);
			else
				importer = new ModelObjectsImporterForCreation(modelObjectsConstructor.ModelId);

			var objectTypes = System.Enum.GetValues(typeof(PropertyTypes)).Cast<PropertyTypes>()
				.Select(x => new ObjectTypeInfo
				{
					EnumValue = x,
					Str = x.GetEnumDescription()
				}).ToList();

			var cancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 20
			};
			Parallel.ForEach(objectsFromExcel, options, objectFromExcel =>
			{
				try
				{
					lock (_locker)
					{
						CurrentRowIndexInFileForUpdating++;
					}
					if (CurrentRowIndexInFileForUpdating % 1000 == 0)
						_log.Debug("{LoggerBasePhrase} обрабатывается объект № {CurrentCount}", LoggerBasePhrase, CurrentRowIndexInFileForUpdating);

					var omModelToMarketObject = importer.CreateRegisterObject(objectFromExcel.Id);
					
					var coefficientsStr = omModelToMarketObject.AttributesValues[OMModelToMarketObjects.GetColumnAttributeId(x => x.Coefficients)].Value?.ToString();
					var coefficientsFromDb = string.IsNullOrWhiteSpace(coefficientsStr) 
						? new List<CoefficientForObject>() 
						: JsonConvert.DeserializeObject<List<CoefficientForObject>>(coefficientsStr);

					bool isForControl = false, isForTraining = false;
					objectFromExcel.Columns.ForEach(column =>
					{
						if (column.AttributeId == OMModelToMarketObjects.GetColumnAttributeId(x => x.Id))
							return;

						if (column.AttributeId != 0)
						{
							//платформа не может обновлять атрибуты типа Reference
							if (column.AttributeId == OMModelToMarketObjects.GetColumnAttributeId(x => x.UnitPropertyType_Code))
							{
								var type = GetObjectTypeInfo(objectTypes, column.ValueToUpdate?.ToString());
								omModelToMarketObject.SetAttributeValue((int)column.AttributeId, type.Str, referenceItemId: (int)type.EnumValue);
								return;
							}

							omModelToMarketObject.SetAttributeValue((int)column.AttributeId, column.ValueToUpdate);

							if (column.AttributeId == OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForControl))
							{
								isForControl = column.ValueToUpdate?.ToString()?.ToLower() == "да";
							}
							if (column.AttributeId == OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForTraining))
							{
								isForTraining = column.ValueToUpdate?.ToString()?.ToLower() == "да";
							}
						}
						else
						{
							//из нормализованного атрибута вида ххх_1 вытаскиваем ххх (ИД)
							var match = Regex.Match(column.AttributeStr, @$"^[^{Consts.PrefixForFactor}]*");
							var attributeIdStr = match.Groups[0].Value;
							long.TryParse(attributeIdStr, out var attributeId);

							var coefficientFromDb = importer.GetCoefficient(coefficientsFromDb, attributeId);
							
							//если фактор нормализованный
							if (column.AttributeStr.Contains(Consts.PrefixForValueInNormalizedColumn))
							{
								coefficientFromDb.Value = column.ValueToUpdate.ParseToStringNullable();
							}
							else if (column.AttributeStr.Contains(Consts.PrefixForCoefficientInNormalizedColumn))
							{
								coefficientFromDb.Coefficient = column.ValueToUpdate.ParseToDecimalNullable();
							}
							//если фактор не нормализованный
							else
							{
								coefficientFromDb.Value = column.ValueToUpdate.ParseToStringNullable();
								coefficientFromDb.Coefficient = column.ValueToUpdate.ParseToDecimalNullable();
							}

							omModelToMarketObject.SetAttributeValue(
								(int)OMModelToMarketObjects.GetColumnAttributeId(c => c.Coefficients),
								coefficientsFromDb.SerializeCoefficient());
						}
					});

					if (importer.IsValidateObject(omModelToMarketObject, isForControl, isForTraining))
						throw new Exception("Объект не может быть в контрольной и обучающей выборках одновременно");

					RegisterStorage.Save(omModelToMarketObject);
					sheet.Rows[objectFromExcel.RowIndexInFile].Cells[maxColumnIndex].SetValue("Обработано");
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					ImportKoCommon.AddErrorCell(sheet, objectFromExcel.RowIndexInFile, maxColumnIndex,
						$"{ex.Message} (подробно в журнале №{errorId})");
				}
			});

			var stream = new MemoryStream();
			file.Save(stream, SaveOptions.XlsxDefault);
			stream.Seek(0, SeekOrigin.Begin);

			return stream;
		}

		
		#region Support Methods

		private List<ModelObjectsFromExcelData> GetObjectsFromFile(ExcelWorksheet sheet, ModelObjectsConstructor config)
		{
			var rows = sheet.Rows;
			MaxRowsCountInFileForUpdating = DataExportCommon.GetLastUsedRowIndex(sheet);
			var modelObjectsFromExcel = new List<ModelObjectsFromExcelData>();

			var columnsMappingWithoutPrimaryKey = config.ColumnsMapping.Where(x =>
				x.AttributeId != OMModelToMarketObjects.GetColumnAttributeId(y => y.Id).ToString()).ToList();

			for (var i = 1; i <= MaxRowsCountInFileForUpdating; i++)
			{
				var cells = rows[i].Cells;

				var columnsWithValues = new List<Column>();
				columnsMappingWithoutPrimaryKey.ForEach(x =>
				{
					long.TryParse(x.AttributeId, out var attributeNumber);
					columnsWithValues.Add(new Column
					{
						AttributeStr = x.AttributeId,
						AttributeId = attributeNumber,
						ValueToUpdate = cells[x.ColumnIndex].Value
					});
				});

				modelObjectsFromExcel.Add(new ModelObjectsFromExcelData
				{
					Id = config.IdColumnIndex == null ? null : cells[config.IdColumnIndex.Value].Value.ParseToLongNullable(),
					RowIndexInFile = i,
					Columns = columnsWithValues
				});
			}

			return modelObjectsFromExcel;
		}

		private ObjectTypeInfo GetObjectTypeInfo(List<ObjectTypeInfo> descriptions, string typeFromFile)
		{
			if (string.IsNullOrWhiteSpace(typeFromFile))
				throw new Exception("Не указан тип объекта");

			var enumInfo = descriptions.FirstOrDefault(x => x.Str == typeFromFile);
			if (enumInfo == null)
				throw new Exception($"Указан недопустимый тип объекта '{typeFromFile}'");

			return enumInfo;
		}


		#endregion


		#region Entities

		public class ModelObjectsFromExcelData
		{
			public long? Id { get; set; }
			public int RowIndexInFile { get; set; }
			public List<Column> Columns { get; set; }

			public ModelObjectsFromExcelData()
			{
				Columns = new List<Column>();
			}
		}

		public class Column
		{
			public string AttributeStr { get; set; }
			public long AttributeId { get; set; }
			public object ValueToUpdate { get; set; }
		}

		private class ObjectTypeInfo
		{
			public PropertyTypes EnumValue { get; set; }
			public string Str { get; set; }
		}

		#endregion
	}
}
