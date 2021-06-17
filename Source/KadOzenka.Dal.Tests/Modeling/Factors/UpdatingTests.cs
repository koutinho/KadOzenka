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
	public class UpdatingTests : BaseModelTests
	{
		[Test]
		public void CanNot_Update_Automatic_Factor_With_Empty_Mark_Type()
		{
			var factorDto = new AutomaticFactorDtoBuilder().Type(MarkType.None).Build();
			
			Assert.Throws<EmptyMarkTypeForFactor>(() => ModelFactorsService.UpdateAutomaticFactor(factorDto));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}

		[Test]
		public void CanNot_Update_Manual_Factor_With_Empty_Mark_Type()
		{
			var factorDto = new ManualFactorDtoBuilder().Type(MarkType.None).Build();

			Assert.Throws<EmptyMarkTypeForFactor>(() => ModelFactorsService.UpdateManualFactor(factorDto));

			ModelFactorsRepository.Verify(foo => foo.Save(It.IsAny<OMModelFactor>()), Times.Never);
		}
	}
}