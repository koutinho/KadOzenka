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
using ObjectModel.Directory;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.IntegrationTests.Modeling.Objects
{
	public class ConstructorTests : BaseModelingTests
	{
		[Test]
		public void Can_Update_Objects()
		{
			var model = new ModelBuilder().Build();
			var addressAttributeId = RandomGenerator.GenerateRandomId();
			var squareAttributeId = RandomGenerator.GenerateRandomId();
			var coefficients = new List<CoefficientForObject>
			{
				new(addressAttributeId)
				{
					Coefficient = RandomGenerator.GenerateRandomDecimal(),
					Value = RandomGenerator.GetRandomString()
				},
				new(squareAttributeId)
				{
					Coefficient = RandomGenerator.GenerateRandomDecimal(),
					Value = RandomGenerator.GetRandomString()
				}
			};
			var firstObject = new ModelObjectBuilder().Id(1).Model(model).Coefficients(coefficients).Build();

			ExcelFile excelFile;
			var fileName = "objects.xlsx";
			using (var stream = File.OpenRead($"{PathToFileFolder}{fileName}"))
			{
				excelFile = ExcelFile.Load(stream, new XlsxLoadOptions());
			}

			var config = new ModelObjectsConstructor
			{
				IdColumnIndex = 0,
				ColumnsMapping = new List<ColumnToAttributeMapping>
				{
					new(1, GetAttributeId(x => x.IsForTraining)),
					new(2, GetAttributeId(x => x.IsForControl)),
					new(3, GetAttributeId(x => x.IsExcluded)),
					new(4, GetAttributeId(x => x.CadastralNumber)),
					new(5, GetAttributeId(x => x.UnitPropertyType)),
					new(6, GetAttributeId(x => x.Price)),
					new(7, GetAttributeId(x => x.PriceFromModel)),
					new(9, $"{addressAttributeId}{ModelObjectsService.PrefixForValueInNormalizedColumn}"),
					new(10, $"{addressAttributeId}{ModelObjectsService.PrefixForCoefficientInNormalizedColumn}"),
					new(11, $"{squareAttributeId}{ModelObjectsService.PrefixForFactor}")
				}
			};

			ModelObjectsService.ChangeModelObjects(excelFile, config);

			var firstExcelRow = new ExcelRow
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
			CheckUpdatedObject(firstObject.Id, addressAttributeId, squareAttributeId, firstExcelRow);
		}
		

		#region Support Methods

		private string GetAttributeId(Expression<Func<OMModelToMarketObjects, object>> expression)
		{
			return OMModelToMarketObjects.GetColumnAttributeId(expression).ToString();
		}

		private void CheckUpdatedObject(long id, long addressAttributeId, long squareAttributeId, ExcelRow row)
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
			
			var addressAttribute = coefficients.First(x => x.AttributeId == addressAttributeId);
			Assert.That(addressAttribute.Value, Is.EqualTo(row.AddressValue));
			Assert.That(addressAttribute.Coefficient, Is.EqualTo(row.AddressCoefficient));

			var squareAttribute = coefficients.First(x => x.AttributeId == squareAttributeId);
			Assert.That(squareAttribute.Value, Is.EqualTo(row.SquareCoefficient.ToString()));
			Assert.That(squareAttribute.Coefficient, Is.EqualTo(row.SquareCoefficient));
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