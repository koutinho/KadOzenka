using KadOzenka.Common.Tests.Builders.Task;
using KadOzenka.Dal.Tasks.Exceptions;
using KadOzenka.Dal.Tours.Dto;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Tasks.InheritanceFactorsSettings
{
	[TestFixture]
	public class UpdatingTests : BaseFactorsTests
	{
		[Test]
		public void CanNot_Update_NotExisting_Setting()
		{
			var factor = new InheritanceFactorSettingsDtoBuilder().Build();
			FactorSettingsRepository.Setup(x => x.GetById(factor.Id, null)).Returns((OMFactorSettings) null);
			FactorSettingsRepository.Setup(x => x.IsFactorExists(factor.FactorId)).Returns(false);
			FactorSettingsRepository.Setup(x => x.IsFactorExists(factor.CorrectFactorId)).Returns(false);
			TourFactorService.Setup(x => x.GetAllTourAttributes(factor.TourId)).Returns(new TourAttributesDto());

			Assert.Throws<InheritanceFactorNotFoundException>(() => InheritanceInheritanceFactorSettingsService.UpdateFactor(factor));

			FactorSettingsRepository.Verify(x => x.Save(It.IsAny<OMFactorSettings>()), Times.Never);
		}
	}
}