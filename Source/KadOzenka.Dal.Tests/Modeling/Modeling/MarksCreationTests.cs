using System.Collections.Generic;
using System.Linq;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using ModelingBusiness.Dictionaries.Entities;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Modeling.Exceptions;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.UnitTests.Modeling.Modeling
{
	public class MarksCreationTests : BaseModelingTests
	{
		private long _modelId;
		private long _addressAttributeId;

		[SetUp]
		public void SetUp()
		{
			_addressAttributeId = RandomGenerator.GenerateRandomId();

			_modelId = RandomGenerator.GenerateRandomId();
		}


		[Test]
		public void Can_Not_Create_Marks_Without_Model_Objects()
		{
			var factor = new ModelFactorRelationPureBuilder().MarkType(MarkType.Default).Build();
			ModelObjectsService.Setup(x => x.GetModelObjects(_modelId)).Returns(new List<OMModelToMarketObjects>());
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure> {factor});

			Assert.Throws<CanNotCreateMarksBecauseNoMarketObjectsException>(() => ModelingService.CreateMarks(_modelId));
		}

		[Test]
		public void Can_Not_Create_Marks_Without_Model_Factors()
		{
			var modelObject = new ModelObjectBuilder().Build();
			ModelObjectsService.Setup(x => x.GetModelObjects(_modelId)).Returns(new List<OMModelToMarketObjects>{ modelObject });
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure>());

			Assert.Throws<CanNotCreateMarksBecauseNoFactorsException>(() => ModelingService.CreateMarks(_modelId));
		}

		[Test]
		public void Can_Not_Create_Marks_For_Factor_Without_DictionaryId()
		{
			var modelObject = new ModelObjectBuilder().Build();
			var factor = new ModelFactorRelationPureBuilder().MarkType(MarkType.Default).DictionaryId(null).Build();
			ModelObjectsService.Setup(x => x.GetModelObjects(_modelId)).Returns(new List<OMModelToMarketObjects> { modelObject });
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure> { factor });

			Assert.Throws<CanNotCreateMarksBecauseNoDictionaryException>(() => ModelingService.CreateMarks(_modelId));
		}

		[Test]
		public void Can_Create_Marks_For_Coded_Factor_With_Even_Values_Count()
		{
			var createdMarks = new List<DictionaryMarkDto>();
			var firstObjectAddressValue = "адрес_1";
			var secondObjectAddressValue = "адрес_2";
			var firstModelObject = new ModelObjectBuilder().Coefficients(GetCoefficients(firstObjectAddressValue)).Price(1).Build();
			var secondModelObject = new ModelObjectBuilder().Coefficients(GetCoefficients(secondObjectAddressValue)).Price(2).Build();
			var factor = new ModelFactorRelationPureBuilder().AttributeId(_addressAttributeId).MarkType(MarkType.Default).Build();

			ModelObjectsService.Setup(x => x.GetModelObjects(_modelId)).Returns(new List<OMModelToMarketObjects> { firstModelObject, secondModelObject });
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure> { factor });
			ModelDictionaryService.Setup(x => x.CreateMark(It.IsAny<DictionaryMarkDto>())).Callback<DictionaryMarkDto>(x => createdMarks.Add(x));


			ModelingService.CreateMarks(_modelId);


			Assert.That(createdMarks.Count, Is.EqualTo(2));
			Assert.That(createdMarks.All(x => x.DictionaryId == factor.DictionaryId));

			var averagePrice = (firstModelObject.Price + secondModelObject.Price) / 2;
			CheckCreatedMark(createdMarks, firstObjectAddressValue, averagePrice, firstModelObject.Price);
			CheckCreatedMark(createdMarks, secondObjectAddressValue, averagePrice, secondModelObject.Price);
		}

		[Test]
		public void Can_Create_Marks_For_Coded_Factor_With_Odd_Values_Count()
		{
			var createdMarks = new List<DictionaryMarkDto>();
			var addressValue = "адрес_1";
			var firstModelObject = new ModelObjectBuilder().Coefficients(GetCoefficients(addressValue)).Price(1).Build();
			var secondModelObject = new ModelObjectBuilder().Coefficients(GetCoefficients(addressValue)).Price(2).Build();
			var thirdModelObject = new ModelObjectBuilder().Coefficients(GetCoefficients(addressValue)).Price(3).Build();
			var factor = new ModelFactorRelationPureBuilder().AttributeId(_addressAttributeId).MarkType(MarkType.Default).Build();

			ModelObjectsService.Setup(x => x.GetModelObjects(_modelId)).Returns(new List<OMModelToMarketObjects> { firstModelObject, secondModelObject, thirdModelObject });
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure> { factor });
			ModelDictionaryService.Setup(x => x.CreateMark(It.IsAny<DictionaryMarkDto>())).Callback<DictionaryMarkDto>(x => createdMarks.Add(x));


			ModelingService.CreateMarks(_modelId);


			Assert.That(createdMarks.Count, Is.EqualTo(1));
			Assert.That(createdMarks.All(x => x.DictionaryId == factor.DictionaryId));

			var median = 2;
			var averagePriceForThreeObjects = 2;
			CheckCreatedMark(createdMarks, addressValue, median, averagePriceForThreeObjects);
		}


		#region Support Methods

		private List<CoefficientForObject> GetCoefficients(string addressValue)
		{
			return new()
			{
				new(_addressAttributeId)
				{
					Coefficient = RandomGenerator.GenerateRandomDecimal(),
					Value = addressValue ?? RandomGenerator.GetRandomString()
				}
			};
		}

		private void CheckCreatedMark(List<DictionaryMarkDto> createdMarks, string markValue, decimal divider,
			decimal objectPrice)
		{
			var mark = createdMarks.FirstOrDefault(x => x.Value == markValue);
			Assert.That(mark, Is.Not.Null);

			var expectedCalculationValue = objectPrice / divider;
			Assert.That(mark.CalculationValue, Is.EqualTo(expectedCalculationValue));
		}

		#endregion
	}
}
