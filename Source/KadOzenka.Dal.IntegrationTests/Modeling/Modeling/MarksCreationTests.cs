using System.Collections.Generic;
using System.Linq;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.Integration._Builders.Model;
using ModelingBusiness.Modeling.Exceptions;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.IntegrationTests.Modeling.Modeling
{
	public class MarksCreationTests : BaseModelingTests
	{
		private OMModel _model;
		private OMModelingDictionary _dictionary;
		private OMModelFactor _addressFactor;
		private long _addressAttributeId;
		private long _squareAttributeId;
		private string _firstAddressValue;
		private string _secondAddressValue;
		private string _thirdAddressValue;


		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_addressAttributeId = 48089615;
			_squareAttributeId = 48403152;

			_firstAddressValue = "адрес_1";
			_secondAddressValue = "адрес_2";
			_thirdAddressValue = "адрес_3";
		}

		[SetUp]
		public void SetUp()
		{
			_model = new ModelBuilder().Automatic().Build();
			_dictionary = new DictionaryBuilder().Build();
			
			_addressFactor = new ModelFactorBuilder().Model(_model).FactorId(_addressAttributeId)
				.Dictionary(_dictionary).MarkType(MarkType.Default).Build();
		}


		[Test]
		public void CanNot_Create_Marks_If_Model_Object_Is_Excluded()
		{
			new ModelObjectBuilder().Model(_model).ForControl(true).Excluded(true).Build();
			
			Assert.Throws<CanNotCreateMarksBecauseNoMarketObjectsException>(() => ModelingService.CreateMarks(_model.Id));
		}

		[Test]
		public void Can_Create_Marks_For_Factor_With_Even_Values_Count()
		{
			var firstModelObject = CreateModelObject(_firstAddressValue);
			var secondModelObject = CreateModelObject(_firstAddressValue);
			var thirdModelObject = CreateModelObject(_secondAddressValue);
			var forthModelObject = CreateModelObject(_secondAddressValue);


			ModelingService.CreateMarks(_model.Id);


			var addressMarks = OMModelingDictionariesValues.Where(x => x.DictionaryId == _addressFactor.DictionaryId).SelectAll().Execute();
			Assert.That(addressMarks.Count, Is.EqualTo(2));

			var averagePriceForFirstAddressValue = (firstModelObject.Price + secondModelObject.Price) / 2;
			var averagePriceForSecondAddressValue = (thirdModelObject.Price + forthModelObject.Price) / 2;
			var averagePriceForAllAddressValues = (averagePriceForFirstAddressValue + averagePriceForSecondAddressValue) / 2;

			CheckMark(addressMarks, _firstAddressValue, averagePriceForFirstAddressValue / averagePriceForAllAddressValues);
			CheckMark(addressMarks, _secondAddressValue, averagePriceForSecondAddressValue / averagePriceForAllAddressValues);
		}

		[Test]
		public void Can_Create_Marks_For_Factor_With_Odd_Values()
		{
			var firstModelObject = CreateModelObject(_firstAddressValue);
			var secondModelObject = CreateModelObject(_firstAddressValue);
			var thirdModelObject = CreateModelObject(_secondAddressValue);
			var forthModelObject = CreateModelObject(_secondAddressValue);
			var fifthModelObject = CreateModelObject(_thirdAddressValue);


			ModelingService.CreateMarks(_model.Id);


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

			CheckMark(addressMarks, _firstAddressValue, averagePriceForFirstAddressValue / medianPriceForAllAddressValues);
			CheckMark(addressMarks, _secondAddressValue, averagePriceForSecondAddressValue / medianPriceForAllAddressValues);
			CheckMark(addressMarks, _thirdAddressValue, averagePriceForThirdAddressValue / medianPriceForAllAddressValues);
		}


		#region Support Methods

		private OMModelToMarketObjects CreateModelObject(string addressValue = null, int? squareValue = null)
		{
			var coefficients = new List<CoefficientForObject>
			{
				new(_addressAttributeId)
				{
					Coefficient = RandomGenerator.GenerateRandomDecimal(),
					Value = addressValue ?? RandomGenerator.GetRandomString()
				},
				new(_squareAttributeId)
				{
					Coefficient = RandomGenerator.GenerateRandomDecimal(),
					Value = squareValue?.ToString() ?? RandomGenerator.GenerateRandomInteger().ToString()
				}
			};

			return new ModelObjectBuilder().Model(_model).ForControl(true).Excluded(false).Coefficients(coefficients).Build();
		}

		private void CheckMark(List<OMModelingDictionariesValues> addressMarks, string value, decimal expectedCalculationValue)
		{
			var mark = addressMarks.FirstOrDefault(x => x.Value == value);
			Assert.That(mark, Is.Not.Null);
			Assert.That(mark.CalculationValue, Is.EqualTo(expectedCalculationValue));
		}

		#endregion
	}
}
