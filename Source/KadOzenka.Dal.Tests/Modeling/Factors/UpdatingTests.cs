using Core.Register;
using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.Modeling.Factors.Exceptions;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Factors
{
	[TestFixture]
	public class UpdatingTests : BaseFactorsTests
	{
		[TestCase(MarkType.Straight)]
		[TestCase(MarkType.Reverse)]
		public void CanNot_Update_Manual_Factor_Of_Special_MarkType_Without_CorrectionTerm(MarkType markType)
		{
			var factor = PrepareManualModelFactorForCRUD(markType, null, RandomGenerator.GenerateRandomDecimal());

			Assert.Throws<EmptyCorrectTermForFactorException>(() => ModelFactorsService.UpdateManualFactor(factor));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[TestCase(MarkType.Straight)]
		[TestCase(MarkType.Reverse)]
		public void CanNot_Update_Manual_Factor_Of_Special_MarkType_Without_K(MarkType markType)
		{
			var factor = PrepareManualModelFactorForCRUD(markType, RandomGenerator.GenerateRandomDecimal(), null);

			Assert.Throws<EmptyKForFactorException>(() => ModelFactorsService.UpdateManualFactor(factor));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[Test]
		public void CanNot_Updated_Manual_Factor_Of_Straight_MarkType_With_Zero_K()
		{
			var factor = PrepareManualModelFactorForCRUD(MarkType.Straight, RandomGenerator.GenerateRandomDecimal(), 0);

			Assert.Throws<EmptyKForFactorWithStraightMarkException>(() => ModelFactorsService.UpdateManualFactor(factor));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[TestCase(MarkType.None)]
		[TestCase(MarkType.Straight)]
		[TestCase(MarkType.Reverse)]
		public void CanNot_Create_Manual_Factor_With_NotNumber_Type_Without_Default_Mark(MarkType markType)
		{
			var factor = new ManualFactorDtoBuilder().Type(markType).Build();
			ModelFactorsRepository.Setup(x => x.IsTheSameAttributeExists(factor.Id, factor.FactorId.Value, factor.ModelId.Value, factor.Type)).Returns(false);
			var attribute = new RegisterAttributeBuilder().Id(factor.FactorId).Type(RegisterAttributeType.STRING).Build();
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.GetValueOrDefault())).Returns(attribute);

			Assert.Throws<WrongFactorTypeException>(() => ModelFactorsService.UpdateManualFactor(factor));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[Test]
		public void Can_Update_Manual_Factor_With_NotNumber_Type_With_Default_Mark()
		{
			var factor = new ManualFactorDtoBuilder().Type(MarkType.Default).Build();
			ModelFactorsRepository.Setup(x => x.IsTheSameAttributeExists(factor.Id, factor.FactorId.Value, factor.ModelId.Value, factor.Type)).Returns(false);
			ModelFactorsRepository.Setup(x => x.GetById(factor.Id, null)).Returns(new OMModelFactor());
			var attribute = new RegisterAttributeBuilder().Id(factor.FactorId).Type(RegisterAttributeType.STRING).Build();
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.GetValueOrDefault())).Returns(attribute);

			ModelFactorsService.UpdateManualFactor(factor);

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Once);
		}
	}
}