using KadOzenka.Common.Tests;
using KadOzenka.Web.Models.ObjectsCharacteristics;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.ObjectsCharacteristics.ObjectsCharacteristics
{
	public class DeletionTests : BaseObjectCharacteristicsTests
	{
		[Test]
		public void Can_Delete_Characteristic()
		{
			var model = new CharacteristicModel
			{
				Id = RandomGenerator.GenerateRandomInteger()
			};

			ObjectsCharacteristicsController.DeleteCharacteristic(model);

			ObjectsCharacteristicsService.Verify(x => x.DeleteCharacteristic(model.Id), Times.Once);
		}
	}
}
