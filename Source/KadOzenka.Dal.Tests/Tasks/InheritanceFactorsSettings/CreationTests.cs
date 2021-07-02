using Core.Register.RegisterEntities;
using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Common.Tests.Builders.Task;
using KadOzenka.Dal.Tasks.Exceptions;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Tasks.InheritanceFactorsSettings
{
	[TestFixture]
	public class CreationTests : BaseFactorsTests
	{
		[Test]
		public void CanNot_Create_Setting_Without_Factor()
		{
			var factor = new InheritanceFactorSettingsDtoBuilder().FactorId(0).Build();

			Assert.Throws<InheritanceEmptyFactorException>(() => FactorSettingsService.Add(factor));

			FactorSettingsRepository.Verify(x => x.Save(It.IsAny<OMFactorSettings>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Setting_Without_Correcting_Factor()
		{
			var factor = new InheritanceFactorSettingsDtoBuilder().CorrectFactorId(0).Build();

			Assert.Throws<InheritanceEmptyFactorException>(() => FactorSettingsService.Add(factor));

			FactorSettingsRepository.Verify(x => x.Save(It.IsAny<OMFactorSettings>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Setting_If_Both_Factors_Are_The_Same()
		{
			var id = RandomGenerator.GenerateRandomInteger();
			var factor = new InheritanceFactorSettingsDtoBuilder().FactorId(id).CorrectFactorId(id).Build();

			Assert.Throws<InheritanceFactorInSettingAreTheSameException>(() => FactorSettingsService.Add(factor));

			FactorSettingsRepository.Verify(x => x.Save(It.IsAny<OMFactorSettings>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Setting_If_Factor_Already_Was_Added()
		{
			var factor = new InheritanceFactorSettingsDtoBuilder().Build();
			FactorSettingsRepository.Setup(x => x.IsFactorExists(factor.FactorId)).Returns(true);
			FactorSettingsRepository.Setup(x => x.IsFactorExists(factor.CorrectFactorId)).Returns(false);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId)).Returns(new RegisterAttribute());

			Assert.Throws<InheritanceFactorAlreadyExistsException>(() => FactorSettingsService.Add(factor));

			FactorSettingsRepository.Verify(x => x.Save(It.IsAny<OMFactorSettings>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Setting_If_CorrectingFactor_Already_Was_Added()
		{
			var factor = new InheritanceFactorSettingsDtoBuilder().Build();
			FactorSettingsRepository.Setup(x => x.IsFactorExists(factor.FactorId)).Returns(false);
			FactorSettingsRepository.Setup(x => x.IsFactorExists(factor.CorrectFactorId)).Returns(true);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.CorrectFactorId)).Returns(new RegisterAttribute());

			Assert.Throws<InheritanceCorrectingFactorAlreadyExistsException>(() => FactorSettingsService.Add(factor));

			FactorSettingsRepository.Verify(x => x.Save(It.IsAny<OMFactorSettings>()), Times.Never);
		}

		[Test]
		public void Can_Create_Setting()
		{
			var factor = new InheritanceFactorSettingsDtoBuilder().Build();
			FactorSettingsRepository.Setup(x => x.IsFactorExists(factor.FactorId)).Returns(false);
			FactorSettingsRepository.Setup(x => x.IsFactorExists(factor.CorrectFactorId)).Returns(false);

			FactorSettingsService.Add(factor);

			FactorSettingsRepository.Verify(x => x.Save(It.IsAny<OMFactorSettings>()), Times.Once);
		}
	}
}