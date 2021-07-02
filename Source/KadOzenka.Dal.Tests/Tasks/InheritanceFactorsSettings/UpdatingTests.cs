using KadOzenka.Common.Tests.Builders.Task;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Exceptions;
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
			var setting = new InheritanceFactorSettingsDtoBuilder().Build();
			FactorSettingsRepository.Setup(x => x.GetById(setting.Id, null)).Returns((OMFactorSettings) null);
			FactorSettingsRepository.Setup(x => x.IsFactorExists(setting.Id, setting.FactorId)).Returns(false);
			FactorSettingsRepository.Setup(x => x.IsFactorExists(setting.Id, setting.CorrectFactorId)).Returns(false);
			TourFactorService.Setup(x => x.GetAllTourAttributes(setting.TourId)).Returns(new TourAttributesDto());

			Assert.Throws<InheritanceFactorNotFoundException>(() => InheritanceInheritanceFactorSettingsService.UpdateFactor(setting));

			FactorSettingsRepository.Verify(x => x.Save(It.IsAny<OMFactorSettings>()), Times.Never);
		}
	}
}