using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using ModelingBusiness.Dictionaries.Entities;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Modeling.Exceptions;
using ModelingBusiness.Objects.Entities;
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
			ModelObjectsRepository.Setup(x => x.GetIncludedModelObjects(_modelId, IncludedObjectsMode.Training, It.IsAny<Expression<Func<OMModelToMarketObjects, object>>>())).Returns(new List<OMModelToMarketObjects>());
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure> {factor});

			Assert.Throws<CanNotCreateMarksBecauseNoMarketObjectsException>(() => ModelingService.CreateMarks(_modelId));
		}

		[Test]
		public void Can_Not_Create_Marks_Without_Model_Factors()
		{
			var modelObject = new ModelObjectBuilder().Build();
			ModelObjectsRepository.Setup(x => x.GetIncludedModelObjects(_modelId, IncludedObjectsMode.Training, It.IsAny<Expression<Func<OMModelToMarketObjects, object>>>()))
				.Returns(new List<OMModelToMarketObjects> {modelObject});
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure>());

			Assert.Throws<CanNotCreateMarksBecauseNoFactorsException>(() => ModelingService.CreateMarks(_modelId));
		}

		[Test]
		public void Can_Not_Create_Marks_For_Factor_Without_DictionaryId()
		{
			var modelObject = new ModelObjectBuilder().Build();
			var factor = new ModelFactorRelationPureBuilder().MarkType(MarkType.Default).DictionaryId(null).Build();
			ModelObjectsRepository.Setup(x => x.GetIncludedModelObjects(_modelId, IncludedObjectsMode.Training, It.IsAny<Expression<Func<OMModelToMarketObjects, object>>>()))
				.Returns(new List<OMModelToMarketObjects> { modelObject });
			ModelFactorsService.Setup(x => x.GetGeneralModelFactors(_modelId)).Returns(new List<ModelFactorRelationPure> { factor });

			Assert.Throws<CanNotCreateMarksBecauseNoDictionaryException>(() => ModelingService.CreateMarks(_modelId));
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
