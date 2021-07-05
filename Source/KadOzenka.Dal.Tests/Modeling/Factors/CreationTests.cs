using Core.Register;
using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.Modeling.Exceptions.Factors;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Factors
{
	[TestFixture]
	public class CreationTests : BaseFactorsTests
	{
		[TestCase(MarkType.Straight)]
		[TestCase(MarkType.Reverse)]
		public void CanNot_Create_Manual_Factor_Of_Special_MarkType_Without_CorrectionTerm(MarkType markType)
		{
			var factor = PrepareManualModelFactorForCRUD(markType, null, RandomGenerator.GenerateRandomDecimal());

			Assert.Throws<EmptyCorrectTermForFactorException>(() => ModelFactorsService.AddManualFactor(factor));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[TestCase(MarkType.Straight)]
		[TestCase(MarkType.Reverse)]
		public void CanNot_Create_Manual_Factor_Of_Special_MarkType_Without_K(MarkType markType)
		{
			var factor = PrepareManualModelFactorForCRUD(markType, RandomGenerator.GenerateRandomDecimal(), null);

			Assert.Throws<EmptyKForFactorException>(() => ModelFactorsService.AddManualFactor(factor));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Manual_Factor_Of_Straight_MarkType_With_Zero_K()
		{
			var factor = PrepareManualModelFactorForCRUD(MarkType.Straight, RandomGenerator.GenerateRandomDecimal(), 0);

			Assert.Throws<EmptyKForFactorWithStraightMarkException>(() => ModelFactorsService.AddManualFactor(factor));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Manual_Factor_Of_Default_Mark_Without_Dictionary()
		{
			var factor = new ManualFactorDtoBuilder().Type(MarkType.Default).Dictionary(null).Build();
			ModelFactorsRepository.Setup(x => x.IsTheSameAttributeExists(factor.Id, factor.FactorId.Value, factor.GeneralModelId.Value, factor.Type)).Returns(false);
			var attribute = new RegisterAttributeBuilder().Id(factor.FactorId).Type(RegisterAttributeType.STRING).Build();
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.GetValueOrDefault())).Returns(attribute);

			Assert.Throws<EmptyDictionaryForFactorWithDefaultMarkException>(() => ModelFactorsService.AddManualFactor(factor));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[TestCase(MarkType.None)]
		[TestCase(MarkType.Straight)]
		[TestCase(MarkType.Reverse)]
		public void CanNot_Create_Manual_Factor_With_NotNumber_Type_Without_Default_Mark(MarkType markType)
		{
			var factor = new ManualFactorDtoBuilder().Type(markType).Build();
			ModelFactorsRepository.Setup(x => x.IsTheSameAttributeExists(factor.Id, factor.FactorId.Value, factor.GeneralModelId.Value, factor.Type)).Returns(false);
			var attribute = new RegisterAttributeBuilder().Id(factor.FactorId).Type(RegisterAttributeType.STRING).Build();
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.GetValueOrDefault())).Returns(attribute);

			Assert.Throws<WrongFactorTypeException>(() => ModelFactorsService.AddManualFactor(factor));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[Test]
		public void Can_Create_Manual_Factor_With_NotNumber_Type_With_Default_Mark()
		{
			var factor = new ManualFactorDtoBuilder().Type(MarkType.Default).Build();
			ModelFactorsRepository.Setup(x => x.IsTheSameAttributeExists(factor.Id, factor.FactorId.Value, factor.GeneralModelId.Value, factor.Type)).Returns(false);
			var attribute = new RegisterAttributeBuilder().Id(factor.FactorId).Type(RegisterAttributeType.STRING).Build();
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.GetValueOrDefault())).Returns(attribute);

			ModelFactorsService.AddManualFactor(factor);

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Once);
		}
	}
}