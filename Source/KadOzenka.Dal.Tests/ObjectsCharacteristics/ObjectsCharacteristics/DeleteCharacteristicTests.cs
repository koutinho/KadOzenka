using System;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests.ObjectsCharacteristics.ObjectsCharacteristics
{
	public class DeleteCharacteristicTests : BaseObjectCharacteristicsTests
	{
		[Test]
		public void Can_Delete_Characteristic()
		{
			var id = RandomGenerator.GenerateRandomInteger();

			ObjectsCharacteristicsService.DeleteCharacteristic(id);

			RegisterAttributeService.Verify(x => x.RemoveRegisterAttribute(It.IsAny<long>()), Times.Once);
			RegisterAttributeService.Verify(x => x.RemoveRegisterAttribute(id), Times.Once);
		}

		[Test]
		public void CanNot_Delete_Characteristic_If_User_HasNot_Admin_Permission()
		{
			SRDSessionWrapper.Setup(y => y.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.ADMIN, true, false, false))
				.Throws(new Exception());

			Assert.Throws<Exception>(() => ObjectsCharacteristicsService.DeleteCharacteristic(RandomGenerator.GenerateRandomInteger()));

			RegisterAttributeService.Verify(x => x.RemoveRegisterAttribute(It.IsAny<long>()), Times.Never);
			SRDSessionWrapper.Verify(x => x.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.ADMIN, true, false, false), Times.Once);
		}
	}
}
