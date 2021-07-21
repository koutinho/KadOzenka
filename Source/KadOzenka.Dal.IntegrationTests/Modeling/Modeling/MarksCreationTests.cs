using System.Collections.Generic;
using System.Linq;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.Integration._Builders.Model;
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
		private long _addressAttributeId;
		private long _squareAttributeId;


		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_addressAttributeId = 48089615;
			_squareAttributeId = 48403152;
		}

		[SetUp]
		public void SetUp()
		{
			_model = new ModelBuilder().Automatic().Build();
			_dictionary = new DictionaryBuilder().Build();
		}



		[Test]
		public void Can_Create_Marks_For_Factor_With_Even_Values_Count()
		{
			var firstAddressValue = "адрес_1";
			var secondAddressValue = "адрес_2";
			var firstModelObject = new ModelObjectBuilder().Model(_model).Coefficients(GetCoefficients(firstAddressValue)).Build();
			var secondModelObject = new ModelObjectBuilder().Model(_model).Coefficients(GetCoefficients(firstAddressValue)).Build();
			var thirdModelObject = new ModelObjectBuilder().Model(_model).Coefficients(GetCoefficients(secondAddressValue)).Build();
			var forthModelObject = new ModelObjectBuilder().Model(_model).Coefficients(GetCoefficients(secondAddressValue)).Build();
			var addressFactor = new ModelFactorBuilder().Model(_model).FactorId(_addressAttributeId).Dictionary(_dictionary).MarkType(MarkType.Default).Build();


			ModelingService.CreateMarks(_model.Id);


			var addressMarks = OMModelingDictionariesValues.Where(x => x.DictionaryId == addressFactor.DictionaryId).SelectAll().Execute();
			Assert.That(addressMarks.Count, Is.EqualTo(2));

			var averagePriceForFirstAddressValue = (firstModelObject.Price + secondModelObject.Price) / 2;
			var averagePriceForSecondAddressValue = (thirdModelObject.Price + forthModelObject.Price) / 2;
			var averagePriceForAllAddressValues = (averagePriceForFirstAddressValue + averagePriceForSecondAddressValue) / 2;

			var firstAddressMark = addressMarks.FirstOrDefault(x => x.Value == firstAddressValue);
			Assert.That(firstAddressMark, Is.Not.Null);
			Assert.That(firstAddressMark.CalculationValue, Is.EqualTo(averagePriceForFirstAddressValue / averagePriceForAllAddressValues));

			var secondAddressMark = addressMarks.FirstOrDefault(x => x.Value == secondAddressValue);
			Assert.That(secondAddressMark, Is.Not.Null);
			Assert.That(firstAddressMark.CalculationValue, Is.EqualTo(averagePriceForFirstAddressValue / averagePriceForAllAddressValues));
		}
		

		#region Support Methods

		private List<CoefficientForObject> GetCoefficients(string addressValue = null, int? squareValue = null)
		{
			return new()
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
		}

		#endregion
	}
}
