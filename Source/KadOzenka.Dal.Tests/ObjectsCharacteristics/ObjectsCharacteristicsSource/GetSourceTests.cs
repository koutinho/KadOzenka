using KadOzenka.Dal.ObjectsCharacteristics.Exceptions;
using KadOzenka.Dal.ObjectsCharacteristics.Resources;
using NUnit.Framework;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Tests.ObjectsCharacteristics.ObjectsCharacteristicsSource
{
	[TestFixture]
	public class GetSourceTests: BaseObjectCharacteristicsTests
	{
		[Test]
		public void Can_Get_Source()
		{
			var registerId = RandomGenerator.GenerateRandomInteger();
			var registerDescription = RandomGenerator.GetRandomString();
			RegisterService.Setup(x => x.GetRegister(registerId))
				.Returns(new OMRegister {RegisterId = registerId, RegisterDescription = registerDescription});

			var source = ObjectsCharacteristicsSourceService.GetSource(registerId);

			Assert.AreEqual(registerId, source.RegisterId);
			Assert.AreEqual(registerDescription, source.RegisterDescription);
		}

		[Test]
		public void CanNot_Get_Source_For_NonExisted_Register()
		{
			var registerId = RandomGenerator.GenerateRandomInteger();
			RegisterService.Setup(x => x.GetRegister(registerId))
				.Returns((OMRegister) null);

			var ex = Assert.Throws<SourceDoesNotExistException>(() => ObjectsCharacteristicsSourceService.GetSource(registerId));

			Assert.AreEqual(string.Format(Messages.SourceDoesNotExist, registerId), ex.Message);
		}
	}
}
