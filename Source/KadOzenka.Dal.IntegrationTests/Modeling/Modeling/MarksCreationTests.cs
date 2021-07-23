﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.Integration._Builders.Model;
using KadOzenka.Dal.LongProcess.Modeling;
using ModelingBusiness.Modeling.Exceptions;
using ModelingBusiness.Objects.Entities;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.Directory.KO;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.IntegrationTests.Modeling.Modeling
{
	public class MarksCreationTests : BaseModelingTests
	{
		private OMModel _model;
		private OMModelFactor _addressFactor;
		private long _addressAttributeId;
		private long _squareAttributeId;
		private string _firstAddressValue;
		private string _secondAddressValue;
		private string _thirdAddressValue;
		private MarksCalculationLongProcess MarksCalculationLongProcess { get; set; }


		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_addressAttributeId = 48089615;
			_squareAttributeId = 48403152;

			_firstAddressValue = "адрес_1";
			_secondAddressValue = "адрес_2";
			_thirdAddressValue = "адрес_3";

			MarksCalculationLongProcess = new MarksCalculationLongProcess();
		}

		[SetUp]
		public void SetUp()
		{
			_model = new ModelBuilder().Automatic().IsActive(false).Build();
			
			var dictionary = new DictionaryBuilder().Type(ModelDictionaryType.String).Build();

			_addressFactor = new ModelFactorBuilder().Model(_model).FactorId(_addressAttributeId)
				.Dictionary(dictionary).MarkType(MarkType.Default).Build();
		}


		[Test]
		public void CanNot_Create_Marks_If_Model_Object_Is_Excluded()
		{
			new ModelObjectBuilder().Model(_model).ForControl(true).Excluded(true).Build();
			
			Assert.Throws<CanNotCreateMarksBecauseNoMarketObjectsException>(() => MarksCalculationLongProcess.CalculateMarks(_model.Id, new CancellationToken()));
		}

		[Test]
		public void Can_Create_Marks_For_Factor_With_Even_Values_Count()
		{
			var firstModelObject = CreateModelObject(_firstAddressValue);
			var secondModelObject = CreateModelObject(_firstAddressValue);
			var thirdModelObject = CreateModelObject(_secondAddressValue);
			var forthModelObject = CreateModelObject(_secondAddressValue);


			MarksCalculationLongProcess.CalculateMarks(_model.Id, new CancellationToken());


			var addressMarks = OMModelingDictionariesValues.Where(x => x.DictionaryId == _addressFactor.DictionaryId).SelectAll().Execute();
			Assert.That(addressMarks.Count, Is.EqualTo(2));

			var averagePriceForFirstAddressValue = (firstModelObject.Price + secondModelObject.Price) / 2;
			var averagePriceForSecondAddressValue = (thirdModelObject.Price + forthModelObject.Price) / 2;
			var averagePriceForAllAddressValues = (averagePriceForFirstAddressValue + averagePriceForSecondAddressValue) / 2;
			
			var expectedCalculationValueForFirstAddressValue = averagePriceForFirstAddressValue / averagePriceForAllAddressValues;
			var expectedCalculationValueForSecondAddressValue = averagePriceForSecondAddressValue / averagePriceForAllAddressValues;

			CheckMark(addressMarks, _firstAddressValue, expectedCalculationValueForFirstAddressValue);
			CheckMark(addressMarks, _secondAddressValue, expectedCalculationValueForSecondAddressValue);

			CheckModelObject(firstModelObject.Id, _addressAttributeId, expectedCalculationValueForFirstAddressValue, false);
			CheckModelObject(secondModelObject.Id, _addressAttributeId, expectedCalculationValueForFirstAddressValue, false);
			CheckModelObject(thirdModelObject.Id, _addressAttributeId, expectedCalculationValueForSecondAddressValue, false);
			CheckModelObject(forthModelObject.Id, _addressAttributeId, expectedCalculationValueForSecondAddressValue, false);
		}

		[Test]
		public void Can_Create_Marks_For_Factor_With_Odd_Values()
		{
			var firstModelObject = CreateModelObject(_firstAddressValue);
			var secondModelObject = CreateModelObject(_firstAddressValue);
			var thirdModelObject = CreateModelObject(_secondAddressValue);
			var forthModelObject = CreateModelObject(_secondAddressValue);
			var fifthModelObject = CreateModelObject(_thirdAddressValue);


			MarksCalculationLongProcess.CalculateMarks(_model.Id, new CancellationToken());


			var addressMarks = OMModelingDictionariesValues.Where(x => x.DictionaryId == _addressFactor.DictionaryId).SelectAll().Execute();
			Assert.That(addressMarks.Count, Is.EqualTo(3));

			var averagePriceForFirstAddressValue = (firstModelObject.Price + secondModelObject.Price) / 2;
			var averagePriceForSecondAddressValue = (thirdModelObject.Price + forthModelObject.Price) / 2;
			var averagePriceForThirdAddressValue = fifthModelObject.Price;
			var medianPriceForAllAddressValues = new List<decimal>
				{
					averagePriceForFirstAddressValue, averagePriceForSecondAddressValue,
					averagePriceForThirdAddressValue
				}
				.OrderBy(x => x).ElementAt(1);

			var expectedCalculationValueForFirstAddressValue = averagePriceForFirstAddressValue / medianPriceForAllAddressValues;
			var expectedCalculationValueForSecondAddressValue = averagePriceForSecondAddressValue / medianPriceForAllAddressValues;
			var expectedCalculationValueForThirdAddressValue = averagePriceForThirdAddressValue / medianPriceForAllAddressValues;
			
			CheckMark(addressMarks, _firstAddressValue, expectedCalculationValueForFirstAddressValue);
			CheckMark(addressMarks, _secondAddressValue, expectedCalculationValueForSecondAddressValue);
			CheckMark(addressMarks, _thirdAddressValue, expectedCalculationValueForThirdAddressValue);

			CheckModelObject(firstModelObject.Id, _addressAttributeId, expectedCalculationValueForFirstAddressValue, false);
			CheckModelObject(thirdModelObject.Id, _addressAttributeId, expectedCalculationValueForSecondAddressValue, false);
			CheckModelObject(fifthModelObject.Id, _addressAttributeId, expectedCalculationValueForThirdAddressValue, false);
		}

		[Test]
		public void Can_Create_Marks_For_Several_Factors()
		{
			var squareValue = RandomGenerator.GenerateRandomInteger();
			var coefficients = new List<CoefficientForObject>
			{
				CreateCoefficientForAddress(_firstAddressValue),
				CreateCoefficientForSquare(squareValue.ToString())
			};
			var modelObject = CreateModelObject(coefficients);
			var squareFactor = CreateSquareFactor();


			MarksCalculationLongProcess.CalculateMarks(_model.Id, new CancellationToken());


			var addressMark = OMModelingDictionariesValues.Where(x => x.DictionaryId == _addressFactor.DictionaryId).SelectAll().Execute();
			var expectedCalculationValueForAddress = modelObject.Price / modelObject.Price;
			Assert.That(addressMark.Count, Is.EqualTo(1));
			Assert.That(addressMark[0].Value, Is.EqualTo(_firstAddressValue));
			Assert.That(addressMark[0].CalculationValue, Is.EqualTo(expectedCalculationValueForAddress));

			var squareMark = OMModelingDictionariesValues.Where(x => x.DictionaryId == squareFactor.DictionaryId).SelectAll().Execute();
			var expectedCalculationValueForSquare = modelObject.Price / modelObject.Price;
			Assert.That(squareMark.Count, Is.EqualTo(1));
			Assert.That(squareMark[0].Value, Is.EqualTo(squareValue.ToString()));
			Assert.That(squareMark[0].CalculationValue, Is.EqualTo(expectedCalculationValueForSquare));

			CheckModelObject(modelObject.Id, _addressAttributeId, expectedCalculationValueForAddress, false);
			CheckModelObject(modelObject.Id, squareFactor.FactorId.GetValueOrDefault(), expectedCalculationValueForSquare, false);
		}

		[Test]
		public void CanNot_Process_Model_Object_With_Empty_Required_Factors()
		{
			var coefficients = new List<CoefficientForObject>
			{
				CreateCoefficientForAddress(_firstAddressValue),
				CreateCoefficientForSquare(null)
			};
			var modelObject = CreateModelObject(coefficients);
			var squareFactor = CreateSquareFactor();


			MarksCalculationLongProcess.CalculateMarks(_model.Id, new CancellationToken());


			var addressMark = OMModelingDictionariesValues.Where(x => x.DictionaryId == _addressFactor.DictionaryId).SelectAll().Execute();
			Assert.That(addressMark.Count, Is.EqualTo(0));

			var squareMark = OMModelingDictionariesValues.Where(x => x.DictionaryId == squareFactor.DictionaryId).SelectAll().Execute();
			Assert.That(squareMark.Count, Is.EqualTo(0));

			CheckModelObject(modelObject.Id, _addressFactor.FactorId.GetValueOrDefault(), coefficients[0].Coefficient, true);
			CheckModelObject(modelObject.Id, squareFactor.FactorId.GetValueOrDefault(), coefficients[1].Coefficient, true);
		}

		[Test]
		public void CanNot_Process_Model_Object_With_NotValid_Mark_Value()
		{
			var notValidSquareValue = RandomGenerator.GetRandomString();
			var coefficients = new List<CoefficientForObject>
			{
				CreateCoefficientForAddress(_firstAddressValue),
				CreateCoefficientForSquare(notValidSquareValue)
			};
			var firstModelObject = CreateModelObject(_secondAddressValue);
			var secondModelObject = CreateModelObject(coefficients);
			var squareFactor = CreateSquareFactor();


			MarksCalculationLongProcess.CalculateMarks(_model.Id, new CancellationToken());


			var addressMark = OMModelingDictionariesValues.Where(x => x.DictionaryId == _addressFactor.DictionaryId).SelectAll().Execute();
			Assert.That(addressMark.Count, Is.EqualTo(1));
			Assert.That(addressMark.First().Value, Is.EqualTo(_secondAddressValue));

			var squareMark = OMModelingDictionariesValues.Where(x => x.DictionaryId == squareFactor.DictionaryId).SelectAll().Execute();
			Assert.That(squareMark.Count, Is.EqualTo(0));

			CheckModelObject(secondModelObject.Id, _addressFactor.FactorId.GetValueOrDefault(), coefficients[0].Coefficient, true);
			CheckModelObject(secondModelObject.Id, squareFactor.FactorId.GetValueOrDefault(), coefficients[1].Coefficient, true);
		}



		#region Support Methods

		private OMModelToMarketObjects CreateModelObject(string addressValue = null)
		{
			var coefficients = new List<CoefficientForObject>
			{
				CreateCoefficientForAddress(addressValue)
			};

			return new ModelObjectBuilder().Model(_model).ForControl(true).Excluded(false).Coefficients(coefficients).Build();
		}

		private OMModelToMarketObjects CreateModelObject(List<CoefficientForObject> coefficients)
		{
			return new ModelObjectBuilder().Model(_model).ForControl(true).Excluded(false).Coefficients(coefficients).Build();
		}

		private CoefficientForObject CreateCoefficientForAddress(string addressValue = null)
		{
			return new CoefficientForObject(_addressAttributeId)
			{
				Coefficient = RandomGenerator.GenerateRandomDecimal(),
				Value = addressValue ?? RandomGenerator.GetRandomString()
			};
		}

		private CoefficientForObject CreateCoefficientForSquare(string squareValue)
		{
			return new CoefficientForObject(_squareAttributeId)
			{
				Coefficient = RandomGenerator.GenerateRandomDecimal(),
				Value = squareValue
			};
		}

		private OMModelFactor CreateSquareFactor()
		{
			return new ModelFactorBuilder().Model(_model).FactorId(_squareAttributeId)
				.Dictionary(new DictionaryBuilder().Type(ModelDictionaryType.Decimal).Build()).MarkType(MarkType.Default).Build();
		}

		private void CheckMark(List<OMModelingDictionariesValues> addressMarks, string value, decimal expectedCalculationValue)
		{
			var mark = addressMarks.FirstOrDefault(x => x.Value == value);
			Assert.That(mark, Is.Not.Null);
			Assert.That(mark.CalculationValue, Is.EqualTo(expectedCalculationValue));
		}

		private void CheckModelObject(long modelObjectId, long attributeId, decimal? expectedCalculationValue, bool isExcluded)
		{
			var updatedModelObject = OMModelToMarketObjects.Where(x => x.Id == modelObjectId).Select(x => new{x.Coefficients, x.IsExcluded}).ExecuteFirstOrDefault();
			Assert.That(updatedModelObject.IsExcluded, Is.EqualTo(isExcluded));

			var updatedCoefficient = updatedModelObject.DeserializedCoefficients.FirstOrDefault(x => x.AttributeId == attributeId);
			Assert.That(updatedCoefficient.Coefficient, Is.EqualTo(expectedCalculationValue));
		}

		#endregion
	}
}
