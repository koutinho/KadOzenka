using KadOzenka.Common.Tests;
using KadOzenka.Dal.Modeling.Exceptions.Factors;
using KadOzenka.Dal.Tests.Modeling.Models;
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
	}
}