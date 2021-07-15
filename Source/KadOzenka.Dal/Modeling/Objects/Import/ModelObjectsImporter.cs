﻿using System;
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
using KadOzenka.Dal.Modeling.Objects.Import.Entities;
using Serilog;

namespace KadOzenka.Dal.Modeling.Objects.Import
{
	public class ModelObjectsImporter
	{
		private const string LoggerBasePhrase = "Импорт объектов моделирования:";
		private readonly ILogger _log = Log.ForContext<ModelObjectsImporter>();
		private readonly object _locker;
		private readonly long _coefficientsAttributeId;
		private readonly long _idAttributeId;
		private readonly long _unitPropertyTypeAttributeId;
		private readonly long _isForTrainingAttributeId;
		private readonly long _isForControlAttributeId;

		public int MaxRowsCount;
		public int CurrentRowCount;
		
		
		public ModelObjectsImporter()
		{
			_locker = new object();

			_coefficientsAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.Coefficients);
			_idAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.Id);
			_unitPropertyTypeAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.UnitPropertyType_Code);
			_isForTrainingAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForTraining);
			_isForControlAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForControl);
		}

		
		public Stream ChangeObjects(bool isUpdating, ExcelFile file, ModelObjectsConstructor modelObjectsConstructor)
		{
			_log.Debug("{LoggerBasePhrase} старт", LoggerBasePhrase);

			var sheet = file.Worksheets[0];
			var maxColumnIndex = DataExportCommon.GetLastUsedColumnIndex(sheet) + 1;
			sheet.Rows[0].Cells[maxColumnIndex].SetValue("Результат обработки");

			var objectsFromExcel = GetObjectsFromFile(sheet, modelObjectsConstructor);
			_log.Debug("{LoggerBasePhrase} в файле {RowsCount} строк", LoggerBasePhrase, MaxRowsCount);

			IModelObjectsImporter importer;
			if (isUpdating)
				importer = new ModelObjectsImporterForUpdating(objectsFromExcel, _log);
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
					Interlocked.Increment(ref CurrentRowCount);
					if (CurrentRowCount % 1000 == 0)
						_log.Debug("{LoggerBasePhrase} обрабатывается объект №{CurrentCount} из {MaxCount}", LoggerBasePhrase, CurrentRowCount, MaxRowsCount);

					var modelToMarketObject = importer.CreateObject(objectFromExcel.Id);
					
					var coefficientsStr = modelToMarketObject.AttributesValues[_coefficientsAttributeId].Value?.ToString();
					var coefficientsFromDb = string.IsNullOrWhiteSpace(coefficientsStr) 
						? new List<CoefficientForObject>() 
						: JsonConvert.DeserializeObject<List<CoefficientForObject>>(coefficientsStr);

					objectFromExcel.Columns.ForEach(column =>
					{
						if (column.AttributeId == _idAttributeId)
							return;

						if (column.AttributeId != 0)
						{
							//платформе нужен referenceItemId для обновления атрибута типа Reference
							if (column.AttributeId == _unitPropertyTypeAttributeId)
							{
								var type = GetObjectTypeInfo(objectTypes, column.ValueToUpdate?.ToString());
								modelToMarketObject.SetAttributeValue((int)column.AttributeId, type.Str, referenceItemId: (int)type.EnumValue);
							}
							else
							{
								modelToMarketObject.SetAttributeValue((int)column.AttributeId, column.ValueToUpdate);
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

							modelToMarketObject.SetAttributeValue((int)_coefficientsAttributeId, coefficientsFromDb.SerializeCoefficient());
						}
					});

					if (IsInValidObject(modelToMarketObject))
						throw new Exception("Объект не может быть в контрольной и обучающей выборках одновременно");

					RegisterStorage.Save(modelToMarketObject);
					
					lock (_locker)
					{
						sheet.Rows[objectFromExcel.RowIndexInFile].Cells[maxColumnIndex].SetValue("Обработано");
					}
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					lock (_locker)
					{
						ImportKoCommon.AddErrorCell(sheet, objectFromExcel.RowIndexInFile, maxColumnIndex,
							$"{ex.Message} (подробно в журнале №{errorId})");
					}
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
			MaxRowsCount = DataExportCommon.GetLastUsedRowIndex(sheet);
			var modelObjectsFromExcel = new List<ModelObjectsFromExcelData>();

			var columnsMappingWithoutPrimaryKey = config.ColumnsMapping.Where(x =>
				x.AttributeId != OMModelToMarketObjects.GetColumnAttributeId(y => y.Id).ToString()).ToList();

			for (var i = 1; i <= MaxRowsCount; i++)
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

		private bool IsInValidObject(RegisterObject modelToMarketObject)
		{
			var isForTraining = modelToMarketObject.AttributesValues[_isForTrainingAttributeId].Value?.ParseToBooleanNullable();
			var isForControl = modelToMarketObject.AttributesValues[_isForControlAttributeId].Value?.ParseToBooleanNullable();

			return isForTraining.GetValueOrDefault() && isForControl.GetValueOrDefault();
		}
		
		#endregion


		#region Entities

		private class ObjectTypeInfo
		{
			public PropertyTypes EnumValue { get; init; }
			public string Str { get; init; }
		}

		#endregion
	}
}
