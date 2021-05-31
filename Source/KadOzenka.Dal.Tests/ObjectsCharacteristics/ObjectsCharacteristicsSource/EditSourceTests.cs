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
	public class EditSourceTests : BaseObjectCharacteristicsTests
	{
		[Test]
		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void CanNot_Edit_Source_Without_Name(string name)
		{
			var dto = new SourceDto { RegisterDescription = name };

			var ex = Assert.Throws<EmptyCharacteristicSourceNameException>(() =>
				ObjectsCharacteristicsSourceService.EditSource(dto));

			Assert.AreEqual(Messages.EmptyCharacteristicSourceName, ex.Message);
		}

		[Test]
		public void CanNot_Edit_Source_For_NonExisted_Register()
		{
			var registerId = RandomGenerator.GenerateRandomInteger();
			var dto = new SourceDto { RegisterDescription = RandomGenerator.GetRandomString(), RegisterId = registerId};
			RegisterService.Setup(x => x.GetRegister(It.IsAny<long?>())).Returns((OMRegister)null);

			var ex = Assert.Throws<SourceDoesNotExistException>(() => ObjectsCharacteristicsSourceService.EditSource(dto));

			Assert.AreEqual(string.Format(Messages.SourceDoesNotExist, registerId), ex.Message);
		}

		[Test]
		public void Can_Edit_Source()
		{
			var registerId = RandomGenerator.GenerateRandomInteger();
			var dto = new SourceDto { RegisterDescription = RandomGenerator.GetRandomString(), RegisterId = registerId };
			var register = new OMRegister {RegisterId = registerId};
			RegisterService.Setup(x => x.GetRegister(registerId))
				.Returns(register);

			ObjectsCharacteristicsSourceService.EditSource(dto);

			Assert.AreEqual(register.RegisterDescription, dto.RegisterDescription);
			ObjectCharacteristicsRepository.Verify(x => x.SaveRegister(register, dto.DisableAttributeEditing), Times.Once);
		}
	}
}
