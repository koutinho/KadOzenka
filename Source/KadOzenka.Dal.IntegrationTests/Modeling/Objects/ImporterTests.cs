﻿using System;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Integration._Builders.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using CommonSdks.Excel;
using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Consts;
using ModelingBusiness.Objects.Entities;
using ModelingBusiness.Objects.Exceptions;
using ModelingBusiness.Objects.Import;
using ModelingBusiness.Objects.Import.Entities;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.IntegrationTests.Modeling.Objects
{
	public class ImporterTests : BaseModelingTests
	{
		private long _isForTrainingAttributeId;
		private long _isForControlAttributeId;
		private OMModel _model;
		private ExcelRow _firstExcelRow;
		private ExcelRow _secondExcelRow;
		private long _modelIdAttributeId;
		private long _coefficientAttributeId;


		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_modelIdAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.ModelId);
			_coefficientAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.Coefficients);
			_isForTrainingAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForTraining);
			_isForControlAttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForControl);

			_firstExcelRow = new ExcelRow
			{
				Id = 1,
				IsForTraining = false,
				IsForControl = false,
				IsExcluded = false,
				CadastralNumber = "77:00:0000000:1151",
				UnitPropertyTypeCode = PropertyTypes.Pllacement,
				UnitPropertyType = "Помещение",
				Price = 10,
				PriceFromModel = 11,
				AddressValue = "Адрес 1",
				AddressCoefficient = 12,
				SquareCoefficient = 13
			};

			_secondExcelRow = new ExcelRow
			{
				Id = 1,
				IsForTraining = true,
				IsForControl = false,
				IsExcluded = false,
				CadastralNumber = "test_error_for_control_and_training",
				UnitPropertyTypeCode = PropertyTypes.Pllacement,
				UnitPropertyType = "Помещение",
				Price = 20,
				PriceFromModel = 21,
				AddressValue = "Адрес 2",
				AddressCoefficient = 22,
				SquareCoefficient = 23
			};
		}

		[SetUp]
		public void SetUp()
		{
			_model = new ModelBuilder().Build();
			
			new ModelFactorBuilder().Model(_model).FactorId(Tour2018OksFactorsAttributeIds.AddressAttributeId)
				.MarkType(MarkType.Default).Build();
			
			new ModelFactorBuilder().Model(_model).FactorId(Tour2018OksFactorsAttributeIds.SquareAttributeId)
				.MarkType(MarkType.Reverse).Build();
		}



		[Test]
		public void Can_Update_Object()
		{
			var coefficients = GetCoefficients();
			var firstObject = new ModelObjectBuilder().Id(1).Model(_model).Coefficients(coefficients).Build();

			var excelFile = GetFile();
			var config = GetConfig(0);
			ModelObjectsImporter.ChangeObjects(excelFile, config);

			CheckUpdatedObject(firstObject.Id, _firstExcelRow);
		}

		[Test]
		public void CanNot_Update_Object_ForTraining_If_It_Was_ForControl()
		{
			var coefficients = GetCoefficients();
			var secondObject = new ModelObjectBuilder().Id(2).Model(_model).ForControl(true).Coefficients(coefficients).Build();

			var excelFile = GetFile();
			var config = new ModelObjectsImporterInfo
			{
				IdColumnIndex = 0,
				ModelId = _model.Id,
				ColumnsMapping = new List<ColumnToAttributeMapping>
				{
					new(1, GetAttributeId(x => x.IsForTraining))
				}
			};

			ModelObjectsImporter.ChangeObjects(excelFile, config);

			CheckObjectWasNotUpdated(secondObject.Id, secondObject);
		}

		[Test]
		public void Can_Create_Object()
		{
			var excelFile = GetFile();
			var config = GetConfig(null);
			ModelObjectsImporter.ChangeObjects(excelFile, config);

			var createdObject = OMModelToMarketObjects.Where(x => x.ModelId == _model.Id && x.MarketObjectInfo == _firstExcelRow.CadastralNumber).SelectAll().ExecuteFirstOrDefault();
			Assert.That(createdObject, Is.Not.Null);
			CheckUpdatedObject(createdObject.Id, _firstExcelRow);
		}

		[Test]
		public void CanNot_Create_Object_ForTraining_And_ForControl_At_The_Same_Time()
		{
			var importer = new ModelObjectsImporterForCreation(_model.Id, _modelIdAttributeId, _coefficientAttributeId, _isForTrainingAttributeId, _isForControlAttributeId);
			var excelData = new ModelObjectsFromExcelData
			{
				Columns = new List<Column>
				{
					new() {AttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForTraining), ValueToUpdate = "Да"},
					new() {AttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForControl), ValueToUpdate = "Да"}
				}
			};

			Assert.Throws<ObjectIsForControlAndForTrainingAtTheSameTimeException>(() => ModelObjectsImporter.ProcessObjectFromExcel(importer, excelData, new HashSet<long>()));
		}


		#region Support Methods

		private List<CoefficientForObject> GetCoefficients()
		{
			return new()
			{
				new(Tour2018OksFactorsAttributeIds.AddressAttributeId)
				{
					Coefficient = RandomGenerator.GenerateRandomDecimal(),
					Value = RandomGenerator.GetRandomString()
				},
				new(Tour2018OksFactorsAttributeIds.SquareAttributeId)
				{
					Coefficient = RandomGenerator.GenerateRandomDecimal(),
					Value = RandomGenerator.GetRandomString()
				}
			};
		}

		private ExcelFile GetFile()
		{
			var fileName = "objects.xlsx";
			using (var stream = File.OpenRead($"{PathToFileFolder}{fileName}"))
			{
				return ExcelFile.Load(stream, new XlsxLoadOptions());
			}
		}

		private ModelObjectsImporterInfo GetConfig(int? idColumnIndex)
		{
			return new ModelObjectsImporterInfo
			{
				IdColumnIndex = idColumnIndex,
				ModelId = _model.Id,
				ColumnsMapping = new List<ColumnToAttributeMapping>
				{
					new(1, GetAttributeId(x => x.IsForTraining)),
					new(2, GetAttributeId(x => x.IsForControl)),
					new(3, GetAttributeId(x => x.IsExcluded)),
					new(4, GetAttributeId(x => x.MarketObjectInfo)),
					new(5, GetAttributeId(x => x.UnitPropertyType)),
					new(6, GetAttributeId(x => x.Price)),
					new(7, GetAttributeId(x => x.PriceFromModel)),
					new(9, Tour2018OksFactorsAttributeIds.AddressAttributeId),
					new(11, Tour2018OksFactorsAttributeIds.SquareAttributeId)
				}
			};
		}

		private long GetAttributeId(Expression<Func<OMModelToMarketObjects, object>> expression)
		{
			return OMModelToMarketObjects.GetColumnAttributeId(expression);
		}

		private void CheckUpdatedObject(long id, ExcelRow row)
		{
			var updatedObject = OMModelToMarketObjects.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			
			Assert.That(updatedObject.IsForTraining, Is.EqualTo(row.IsForTraining));
			Assert.That(updatedObject.IsForControl, Is.EqualTo(row.IsForControl));
			Assert.That(updatedObject.IsExcluded, Is.EqualTo(row.IsExcluded));
			Assert.That(updatedObject.MarketObjectInfo, Is.EqualTo(row.CadastralNumber));
			Assert.That(updatedObject.UnitPropertyType, Is.EqualTo(row.UnitPropertyType));
			Assert.That(updatedObject.UnitPropertyType_Code, Is.EqualTo(row.UnitPropertyTypeCode));
			Assert.That(updatedObject.Price, Is.EqualTo(row.Price));
			Assert.That(updatedObject.PriceFromModel, Is.EqualTo(row.PriceFromModel));

			var coefficients = updatedObject.DeserializedCoefficients;
			Assert.That(coefficients.Count, Is.EqualTo(2));
			
			var addressAttribute = coefficients.First(x => x.AttributeId == Tour2018OksFactorsAttributeIds.AddressAttributeId);
			Assert.That(addressAttribute.Value, Is.EqualTo(row.AddressValue));
			Assert.That(addressAttribute.Coefficient, Is.EqualTo(addressAttribute.Coefficient));

			var squareAttribute = coefficients.First(x => x.AttributeId == Tour2018OksFactorsAttributeIds.SquareAttributeId);
			Assert.That(squareAttribute.Value, Is.EqualTo(row.SquareCoefficient.ToString()));
			Assert.That(squareAttribute.Coefficient, Is.EqualTo(row.SquareCoefficient));
		}

		private void CheckObjectWasNotUpdated(long id, OMModelToMarketObjects initial)
		{
			var updatedObject = OMModelToMarketObjects.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();

			Assert.That(updatedObject.IsForTraining, Is.EqualTo(initial.IsForTraining));
			Assert.That(updatedObject.IsForControl, Is.EqualTo(initial.IsForControl));
			Assert.That(updatedObject.IsExcluded, Is.EqualTo(initial.IsExcluded));
			Assert.That(updatedObject.MarketObjectInfo, Is.EqualTo(initial.MarketObjectInfo));
			Assert.That(updatedObject.UnitPropertyType, Is.EqualTo(initial.UnitPropertyType));
			Assert.That(updatedObject.UnitPropertyType_Code, Is.EqualTo(initial.UnitPropertyType_Code));
			Assert.That(updatedObject.Price, Is.EqualTo(initial.Price));
			Assert.That(updatedObject.PriceFromModel, Is.EqualTo(initial.PriceFromModel));

			var updatedCoefficients = updatedObject.DeserializedCoefficients;
			var initialCoefficients = initial.DeserializedCoefficients;
			Assert.That(updatedCoefficients.Count, Is.EqualTo(2));

			var updatedAddressAttribute = updatedCoefficients.First(x => x.AttributeId == Tour2018OksFactorsAttributeIds.AddressAttributeId);
			var initialAddressAttribute = initialCoefficients.First(x => x.AttributeId == Tour2018OksFactorsAttributeIds.AddressAttributeId);
			Assert.That(updatedAddressAttribute.Value, Is.EqualTo(initialAddressAttribute.Value));
			Assert.That(updatedAddressAttribute.Coefficient, Is.EqualTo(initialAddressAttribute.Coefficient));

			var updatedSquareAttribute = updatedCoefficients.First(x => x.AttributeId == Tour2018OksFactorsAttributeIds.SquareAttributeId);
			var initialSquareAttribute = initialCoefficients.First(x => x.AttributeId == Tour2018OksFactorsAttributeIds.SquareAttributeId);
			Assert.That(updatedSquareAttribute.Value, Is.EqualTo(initialSquareAttribute.Value));
			Assert.That(updatedSquareAttribute.Coefficient, Is.EqualTo(initialSquareAttribute.Coefficient));
		}


		private class ExcelRow
		{
			public long Id { get; set; }
			public bool IsForTraining { get; set; }
			public bool IsForControl { get; set; }
			public bool IsExcluded { get; set; }
			public string CadastralNumber { get; set; }
			public string UnitPropertyType { get; set; }
			public PropertyTypes UnitPropertyTypeCode { get; set; }
			public decimal Price { get; set; }
			public decimal PriceFromModel { get; set; }
			public string AddressValue { get; set; }
			public decimal AddressCoefficient { get; set; }
			public decimal SquareCoefficient { get; set; }
		}

		#endregion
	}
}