using KadOzenka.Common.Tests;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Dal.ObjectsCharacteristics.Exceptions;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;
using Moq;
using NUnit.Framework;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Tests.ObjectsCharacteristics.ObjectsCharacteristicsSource
{
	[TestFixture]
	public class AddSourceTests : BaseObjectCharacteristicsTests
	{
		public static readonly int RegisterStorageType = 5;

		[Test]
		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void CanNot_Create_Source_Without_Name(string name)
		{
			var dto = new SourceDto {RegisterDescription = name};

			var ex = Assert.Throws<EmptyCharacteristicSourceNameException>(() =>
				ObjectsCharacteristicsSourceService.AddSource(dto));

			Assert.AreEqual(Messages.EmptyCharacteristicSourceName, ex.Message);
		}

		[Test]
		public void Can_Create_Source()
		{
			var dto = new SourceDto { RegisterDescription = RandomGenerator.GetRandomString() };
			var numberOfExistingRegistersWithCharacteristics = RandomGenerator.GenerateRandomInteger();
			ObjectCharacteristicsRepository.Setup(x => x.GetNumberOfExistingRegistersWithCharacteristics())
				.Returns(numberOfExistingRegistersWithCharacteristics);
			RegisterService.Setup(x => x.CreateRegister(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
				It.IsAny<string>(), It.IsAny<long?>()))
				.Returns(new OMRegister());

			ObjectsCharacteristicsSourceService.AddSource(dto);

			RegisterService.Verify(x => x.CreateIdColumnForRegister(It.IsAny<long>()), Times.Once);
			RegisterConfiguratorWrapper.Verify(x => x.CreateDbTableForRegister(It.IsAny<long>()), Times.Once);
			ObjectCharacteristicsRepository.Verify(x => x.CreateObjectCharacteristics(It.IsAny<long>(), It.IsAny<bool>()), Times.Once);
			RegisterService.Verify(x => x.CreateRegister(string.Format(Fields.RegisterName, numberOfExistingRegistersWithCharacteristics + 1),
				string.Format(Fields.RegisterDescription, dto.RegisterDescription),
				Fields.QuantTable,
				string.Format(Fields.AllpriTable, numberOfExistingRegistersWithCharacteristics + 1),
				RegisterStorageType), Times.Once);
		}
	}
}
