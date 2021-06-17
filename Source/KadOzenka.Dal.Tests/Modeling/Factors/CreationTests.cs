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
	public class CreationTests : BaseModelTests
	{
		[Test]
		public void CanNot_Create_Automatic_Factor_Without_Mark_Type()
		{
			var factorDto = new AutomaticFactorDtoBuilder().Type(MarkType.None).Build();
			
			Assert.Throws<EmptyMarkTypeForFactor>(() => ModelFactorsService.AddAutomaticFactor(factorDto));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Manual_Factor_Without_Mark_Type()
		{
			var factorDto = new ManualFactorDtoBuilder().Type(MarkType.None).Build();

			Assert.Throws<EmptyMarkTypeForFactor>(() => ModelFactorsService.AddManualFactor(factorDto));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}
	}
}