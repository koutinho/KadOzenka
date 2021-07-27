using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Consts;
using KadOzenka.Dal.Integration._Builders.Model;
using KadOzenka.Dal.IntegrationTests.Modeling;
using KadOzenka.Dal.LongProcess.Modeling;
using ModelingBusiness.Factors.Exceptions.AutomaticModelParametersCalculation;
using ModelingBusiness.Modeling.Exceptions;
using ModelingBusiness.Objects.Entities;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.Directory.KO;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.IntegrationTests.Factors.Modeling
{
	[Ignore("Не реализовано")]
	public class AutomaticModelParametersCalculationTests : BaseModelingTests
	{
		private OMModel _model;
		private OMModelFactor _squareFactor;
		private string _firstAddressValue;
		private string _secondAddressValue;
		private string _thirdAddressValue;
		private AutomaticModelParametersCalculationLongProcess CalculationLongProcess { get; set; }


		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_firstAddressValue = "адрес_1";
			_secondAddressValue = "адрес_2";
			_thirdAddressValue = "адрес_3";

			CalculationLongProcess = new AutomaticModelParametersCalculationLongProcess();
		}

		[SetUp]
		public void SetUp()
		{
			_model = new ModelBuilder().Automatic().IsActive(false).Build();
			
			var dictionary = new DictionaryBuilder().Type(ModelDictionaryType.String).Build();

			_squareFactor = new ModelFactorBuilder().Model(_model).FactorId(Tour2018OksFactorsAttributeIds.SquareAttributeId)
				.Dictionary(dictionary).MarkType(MarkType.Straight).Build();
		}


		[Test]
		public void CanNot_Create_Marks_If_Model_Object_Is_Excluded()
		{
			new ModelObjectBuilder().Model(_model).ForControl(true).Excluded(true).Build();
			
			Assert.Throws<CanNotCalculateParametersBecauseNoMarketObjectsException>(() => CalculationLongProcess.CalculateParameters(_model.Id, new CancellationToken()));
		}

		//[Test]
		//public void Can_Create_Marks_For_Factor_With_Even_Values_Count()
		//{
		//	var firstModelObject = CreateModelObject(_firstAddressValue);
		//	var secondModelObject = CreateModelObject(_firstAddressValue);
		//	var thirdModelObject = CreateModelObject(_secondAddressValue);
		//	var forthModelObject = CreateModelObject(_secondAddressValue);


		//	CalculationLongProcess.CalculateParameters(_model.Id, new CancellationToken());


		//	var addressMarks = OMModelingDictionariesValues.Where(x => x.DictionaryId == _squareFactor.DictionaryId).SelectAll().Execute();
		//	Assert.That(addressMarks.Count, Is.EqualTo(2));

		//	var averagePriceForFirstAddressValue = (firstModelObject.Price + secondModelObject.Price) / 2;
		//	var averagePriceForSecondAddressValue = (thirdModelObject.Price + forthModelObject.Price) / 2;
		//	var averagePriceForAllAddressValues = (averagePriceForFirstAddressValue + averagePriceForSecondAddressValue) / 2;

		//	var expectedCalculationValueForFirstAddressValue = averagePriceForFirstAddressValue / averagePriceForAllAddressValues;
		//	var expectedCalculationValueForSecondAddressValue = averagePriceForSecondAddressValue / averagePriceForAllAddressValues;

		//	CheckMark(addressMarks, _firstAddressValue, expectedCalculationValueForFirstAddressValue);
		//	CheckMark(addressMarks, _secondAddressValue, expectedCalculationValueForSecondAddressValue);

		//	CheckModelObject(firstModelObject.Id, Tour2018OksFactorsAttributeIds.AddressAttributeId, expectedCalculationValueForFirstAddressValue, false);
		//	CheckModelObject(secondModelObject.Id, Tour2018OksFactorsAttributeIds.AddressAttributeId, expectedCalculationValueForFirstAddressValue, false);
		//	CheckModelObject(thirdModelObject.Id, Tour2018OksFactorsAttributeIds.AddressAttributeId, expectedCalculationValueForSecondAddressValue, false);
		//	CheckModelObject(forthModelObject.Id, Tour2018OksFactorsAttributeIds.AddressAttributeId, expectedCalculationValueForSecondAddressValue, false);
		//}

		//[Test]
		//public void Can_Create_Marks_For_Factor_With_Odd_Values()
		//{
		//	var firstModelObject = CreateModelObject(_firstAddressValue);
		//	var secondModelObject = CreateModelObject(_firstAddressValue);
		//	var thirdModelObject = CreateModelObject(_secondAddressValue);
		//	var forthModelObject = CreateModelObject(_secondAddressValue);
		//	var fifthModelObject = CreateModelObject(_thirdAddressValue);


		//	CalculationLongProcess.CalculateParameters(_model.Id, new CancellationToken());


		//	var addressMarks = OMModelingDictionariesValues.Where(x => x.DictionaryId == _squareFactor.DictionaryId).SelectAll().Execute();
		//	Assert.That(addressMarks.Count, Is.EqualTo(3));

		//	var averagePriceForFirstAddressValue = (firstModelObject.Price + secondModelObject.Price) / 2;
		//	var averagePriceForSecondAddressValue = (thirdModelObject.Price + forthModelObject.Price) / 2;
		//	var averagePriceForThirdAddressValue = fifthModelObject.Price;
		//	var medianPriceForAllAddressValues = new List<decimal>
		//		{
		//			averagePriceForFirstAddressValue, averagePriceForSecondAddressValue,
		//			averagePriceForThirdAddressValue
		//		}
		//		.OrderBy(x => x).ElementAt(1);

		//	var expectedCalculationValueForFirstAddressValue = averagePriceForFirstAddressValue / medianPriceForAllAddressValues;
		//	var expectedCalculationValueForSecondAddressValue = averagePriceForSecondAddressValue / medianPriceForAllAddressValues;
		//	var expectedCalculationValueForThirdAddressValue = averagePriceForThirdAddressValue / medianPriceForAllAddressValues;

		//	CheckMark(addressMarks, _firstAddressValue, expectedCalculationValueForFirstAddressValue);
		//	CheckMark(addressMarks, _secondAddressValue, expectedCalculationValueForSecondAddressValue);
		//	CheckMark(addressMarks, _thirdAddressValue, expectedCalculationValueForThirdAddressValue);

		//	CheckModelObject(firstModelObject.Id, Tour2018OksFactorsAttributeIds.AddressAttributeId, expectedCalculationValueForFirstAddressValue, false);
		//	CheckModelObject(thirdModelObject.Id, Tour2018OksFactorsAttributeIds.AddressAttributeId, expectedCalculationValueForSecondAddressValue, false);
		//	CheckModelObject(fifthModelObject.Id, Tour2018OksFactorsAttributeIds.AddressAttributeId, expectedCalculationValueForThirdAddressValue, false);
		//}

		//[Test]
		//public void Can_Create_Marks_For_Several_Factors()
		//{
		//	var squareValue = RandomGenerator.GenerateRandomInteger();
		//	var coefficients = new List<CoefficientForObject>
		//	{
		//		CreateCoefficientForAddress(_firstAddressValue),
		//		CreateCoefficientForSquare(squareValue.ToString())
		//	};
		//	var modelObject = CreateModelObject(coefficients);
		//	var squareFactor = CreateSquareFactor();


		//	CalculationLongProcess.CalculateParameters(_model.Id, new CancellationToken());


		//	var addressMark = OMModelingDictionariesValues.Where(x => x.DictionaryId == _squareFactor.DictionaryId).SelectAll().Execute();
		//	var expectedCalculationValueForAddress = modelObject.Price / modelObject.Price;
		//	Assert.That(addressMark.Count, Is.EqualTo(1));
		//	Assert.That(addressMark[0].Value, Is.EqualTo(_firstAddressValue));
		//	Assert.That(addressMark[0].CalculationValue, Is.EqualTo(expectedCalculationValueForAddress));

		//	var squareMark = OMModelingDictionariesValues.Where(x => x.DictionaryId == squareFactor.DictionaryId).SelectAll().Execute();
		//	var expectedCalculationValueForSquare = modelObject.Price / modelObject.Price;
		//	Assert.That(squareMark.Count, Is.EqualTo(1));
		//	Assert.That(squareMark[0].Value, Is.EqualTo(squareValue.ToString()));
		//	Assert.That(squareMark[0].CalculationValue, Is.EqualTo(expectedCalculationValueForSquare));

		//	CheckModelObject(modelObject.Id, Tour2018OksFactorsAttributeIds.AddressAttributeId, expectedCalculationValueForAddress, false);
		//	CheckModelObject(modelObject.Id, squareFactor.FactorId.GetValueOrDefault(), expectedCalculationValueForSquare, false);
		//}

		[Test]
		public void CanNot_Process_Model_Object_With_Empty_Required_Factors()
		{
			var coefficients = new List<CoefficientForObject>
			{
				CreateCoefficientForDistanceToMkad(),
				CreateCoefficientForSquare(null)
			};
			var modelObject = CreateModelObject(coefficients);
			CreateDistanceToMkadFactor();
			
			CalculationLongProcess.CalculateParameters(_model.Id, new CancellationToken());
			
			CheckModelObject(modelObject.Id, true);
		}


		#region Support Methods

		private OMModelToMarketObjects CreateModelObject(decimal? square = null)
		{
			var coefficients = new List<CoefficientForObject>
			{
				CreateCoefficientForSquare(square ?? RandomGenerator.GenerateRandomDecimal())
			};

			return new ModelObjectBuilder().Model(_model).ForControl(true).Excluded(false).Coefficients(coefficients).Build();
		}

		private OMModelToMarketObjects CreateModelObject(List<CoefficientForObject> coefficients)
		{
			return new ModelObjectBuilder().Model(_model).ForControl(true).Excluded(false).Coefficients(coefficients).Build();
		}

		private CoefficientForObject CreateCoefficientForSquare(decimal? square = null)
		{
			return new CoefficientForObject(Tour2018OksFactorsAttributeIds.SquareAttributeId)
			{
				Coefficient = square,
				Value = square?.ToString()
			};
		}

		private CoefficientForObject CreateCoefficientForDistanceToMkad(decimal? distance = null)
		{
			var value = distance ?? RandomGenerator.GenerateRandomDecimal();
			return new CoefficientForObject(Tour2018OksFactorsAttributeIds.DistanceToMkadAttributeId)
			{
				Coefficient = value,
				Value = value.ToString()
			};
		}

		private OMModelFactor CreateDistanceToMkadFactor()
		{
			return new ModelFactorBuilder().Model(_model)
				.FactorId(Tour2018OksFactorsAttributeIds.DistanceToMkadAttributeId)
				.MarkType(MarkType.Straight).Build();
		}

		private void CheckMark(List<OMModelingDictionariesValues> addressMarks, string value, decimal expectedCalculationValue)
		{
			var mark = addressMarks.FirstOrDefault(x => x.Value == value);
			Assert.That(mark, Is.Not.Null);
			Assert.That(mark.CalculationValue, Is.EqualTo(expectedCalculationValue));
		}

		private void CheckModelObject(long modelObjectId, bool isExcluded)
		{
			var updatedModelObject = OMModelToMarketObjects.Where(x => x.Id == modelObjectId).Select(x => new{x.Coefficients, x.IsExcluded}).ExecuteFirstOrDefault();
			Assert.That(updatedModelObject.IsExcluded, Is.EqualTo(isExcluded));
		}

		#endregion
	}
}
