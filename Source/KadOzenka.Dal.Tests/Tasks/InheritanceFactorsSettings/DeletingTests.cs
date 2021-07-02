using KadOzenka.Common.Tests;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Exceptions;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Tasks.InheritanceFactorsSettings
{
	[TestFixture]
	public class DeletingTests : BaseFactorsTests
	{
		[Test]
		public void CanNot_Delete_NotExisting_Setting()
		{
			var settingId = RandomGenerator.GenerateRandomId();
			FactorSettingsRepository.Setup(x => x.GetById(settingId, null)).Returns((OMFactorSettings) null);

			Assert.Throws<InheritanceFactorNotFoundException>(() => InheritanceInheritanceFactorSettingsService.DeleteSetting(settingId));

			FactorSettingsRepository.Verify(x => x.Save(It.IsAny<OMFactorSettings>()), Times.Never);
		}
	}
}