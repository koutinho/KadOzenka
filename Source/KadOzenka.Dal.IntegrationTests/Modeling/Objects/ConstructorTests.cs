using System;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Integration._Builders.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.Modeling.Entities;
using KadOzenka.Dal.Modeling.Objects;
using KadOzenka.Dal.Modeling.Objects.Exceptions;
using KadOzenka.Dal.Modeling.Objects.Import;
using KadOzenka.Dal.Modeling.Objects.Import.Entities;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.IntegrationTests.Modeling.Objects
{
	public class ConstructorTests : BaseModelingTests
	{
		private long _addressAttributeId;
		private long _squareAttributeId;
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
			_addressAttributeId = RandomGenerator.GenerateRandomId();
			_squareAttributeId = RandomGenerator.GenerateRandomId();
			
			_model = new ModelBuilder().Build();
		}



		[Test]
		public void Can_Update_Object()
		{
			var coefficients = GetCoefficients();
			var firstObject = new ModelObjectBuilder().Id(1).Model(_model).Coefficients(coefficients).Build();

			var excelFile = GetFile();
			var config = GetConfig(0);
			ModelObjectsImporter.ChangeObjects(true, excelFile, config);

			CheckUpdatedObject(firstObject.Id, _firstExcelRow);
		}

		[Test]
		public void CanNot_Update_Object_ForTraining_If_It_Was_ForControl()
		{
			var coefficients = GetCoefficients();
			var secondObject = new ModelObjectBuilder().Id(2).Model(_model).ForControl(true).Coefficients(coefficients).Build();

			var excelFile = GetFile();
			var config = new ModelObjectsConstructor
			{
				IdColumnIndex = 0,
				ModelId = _model.Id,
				ColumnsMapping = new List<ColumnToAttributeMapping>
				{
					new(1, GetAttributeId(x => x.IsForTraining))
				}
			};

			ModelObjectsImporter.ChangeObjects(true, excelFile, config);

			CheckObjectWasNotUpdated(secondObject.Id, secondObject);
		}

		[Test]
		public void Can_Create_Object()
		{
			var excelFile = GetFile();
			var config = GetConfig(null);
			ModelObjectsImporter.ChangeObjects(false, excelFile, config);

			var createdObject = OMModelToMarketObjects.Where(x => x.ModelId == _model.Id && x.CadastralNumber == _firstExcelRow.CadastralNumber).SelectAll().ExecuteFirstOrDefault();
			Assert.That(createdObject, Is.Not.Null);
			CheckUpdatedObject(createdObject.Id, _firstExcelRow);
		}

		[Test]
		public void CanNot_Create_Object_ForTraining_And_ForControl_At_The_Same_Time()
		{
			var importer = new ModelObjectsImporterForCreation(_model.Id, _modelIdAttributeId, _coefficientAttributeId);
			var excelData = new ModelObjectsFromExcelData
			{
				Columns = new List<Column>
				{
					new() {AttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForTraining), AttributeStr = GetAttributeId(x => x.IsForTraining), ValueToUpdate = "Да"},
					new() {AttributeId = OMModelToMarketObjects.GetColumnAttributeId(x => x.IsForControl), AttributeStr = GetAttributeId(x => x.IsForControl), ValueToUpdate = "Да"}
				}
			};

			Assert.Throws<ObjectIsForControlAndForTrainingAtTheSameTimeException>(() => ModelObjectsImporter.ProcessObjectFromExcel(importer, excelData));
		}

		#region Support Methods

		private List<CoefficientForObject> GetCoefficients()
		{
			return new()
			{
				new(_addressAttributeId)
				{
					Coefficient = RandomGenerator.GenerateRandomDecimal(),
					Value = RandomGenerator.GetRandomString()
				},
				new(_squareAttributeId)
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

		private ModelObjectsConstructor GetConfig(int? idColumnIndex)
		{
			return new ModelObjectsConstructor
			{
				IdColumnIndex = idColumnIndex,
				ModelId = _model.Id,
				ColumnsMapping = new List<ColumnToAttributeMapping>
				{
					new(1, GetAttributeId(x => x.IsForTraining)),
					new(2, GetAttributeId(x => x.IsForControl)),
					new(3, GetAttributeId(x => x.IsExcluded)),
					new(4, GetAttributeId(x => x.CadastralNumber)),
					new(5, GetAttributeId(x => x.UnitPropertyType)),
					new(6, GetAttributeId(x => x.Price)),
					new(7, GetAttributeId(x => x.PriceFromModel)),
					new(9, $"{_addressAttributeId}{Consts.PrefixForValueInNormalizedColumn}"),
					new(10, $"{_addressAttributeId}{Consts.PrefixForCoefficientInNormalizedColumn}"),
					new(11, $"{_squareAttributeId}{Consts.PrefixForFactor}")
				}
			};
		}

		private string GetAttributeId(Expression<Func<OMModelToMarketObjects, object>> expression)
		{
			return OMModelToMarketObjects.GetColumnAttributeId(expression).ToString();
		}

		private void CheckUpdatedObject(long id, ExcelRow row)
		{
			var updatedObject = OMModelToMarketObjects.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			
			Assert.That(updatedObject.IsForTraining, Is.EqualTo(row.IsForTraining));
			Assert.That(updatedObject.IsForControl, Is.EqualTo(row.IsForControl));
			Assert.That(updatedObject.IsExcluded, Is.EqualTo(row.IsExcluded));
			Assert.That(updatedObject.CadastralNumber, Is.EqualTo(row.CadastralNumber));
			Assert.That(updatedObject.UnitPropertyType, Is.EqualTo(row.UnitPropertyType));
			Assert.That(updatedObject.UnitPropertyType_Code, Is.EqualTo(row.UnitPropertyTypeCode));
			Assert.That(updatedObject.Price, Is.EqualTo(row.Price));
			Assert.That(updatedObject.PriceFromModel, Is.EqualTo(row.PriceFromModel));

			var coefficients = updatedObject.DeserializeCoefficient();
			Assert.That(coefficients.Count, Is.EqualTo(2));
			
			var addressAttribute = coefficients.First(x => x.AttributeId == _addressAttributeId);
			Assert.That(addressAttribute.Value, Is.EqualTo(row.AddressValue));
			Assert.That(addressAttribute.Coefficient, Is.EqualTo(row.AddressCoefficient));

			var squareAttribute = coefficients.First(x => x.AttributeId == _squareAttributeId);
			Assert.That(squareAttribute.Value, Is.EqualTo(row.SquareCoefficient.ToString()));
			Assert.That(squareAttribute.Coefficient, Is.EqualTo(row.SquareCoefficient));
		}

		private void CheckObjectWasNotUpdated(long id, OMModelToMarketObjects initial)
		{
			var updatedObject = OMModelToMarketObjects.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();

			Assert.That(updatedObject.IsForTraining, Is.EqualTo(initial.IsForTraining));
			Assert.That(updatedObject.IsForControl, Is.EqualTo(initial.IsForControl));
			Assert.That(updatedObject.IsExcluded, Is.EqualTo(initial.IsExcluded));
			Assert.That(updatedObject.CadastralNumber, Is.EqualTo(initial.CadastralNumber));
			Assert.That(updatedObject.UnitPropertyType, Is.EqualTo(initial.UnitPropertyType));
			Assert.That(updatedObject.UnitPropertyType_Code, Is.EqualTo(initial.UnitPropertyType_Code));
			Assert.That(updatedObject.Price, Is.EqualTo(initial.Price));
			Assert.That(updatedObject.PriceFromModel, Is.EqualTo(initial.PriceFromModel));

			var updatedCoefficients = updatedObject.DeserializeCoefficient();
			var initialCoefficients = initial.DeserializeCoefficient();
			Assert.That(updatedCoefficients.Count, Is.EqualTo(2));

			var updatedAddressAttribute = updatedCoefficients.First(x => x.AttributeId == _addressAttributeId);
			var initialAddressAttribute = initialCoefficients.First(x => x.AttributeId == _addressAttributeId);
			Assert.That(updatedAddressAttribute.Value, Is.EqualTo(initialAddressAttribute.Value));
			Assert.That(updatedAddressAttribute.Coefficient, Is.EqualTo(initialAddressAttribute.Coefficient));

			var updatedSquareAttribute = updatedCoefficients.First(x => x.AttributeId == _squareAttributeId);
			var initialSquareAttribute = initialCoefficients.First(x => x.AttributeId == _squareAttributeId);
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