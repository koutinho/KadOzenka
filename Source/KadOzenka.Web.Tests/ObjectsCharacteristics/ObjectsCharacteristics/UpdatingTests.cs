using Core.Shared.Extensions;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Web.Models.ObjectsCharacteristics;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.ObjectsCharacteristics.ObjectsCharacteristics
{
	public class UpdatingTests : BaseObjectCharacteristicsTests
	{
		[Test]
		public void Can_Update_Characteristic()
		{
			var model = new CharacteristicModel
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				Name = RandomGenerator.GetRandomString()
			};

			var result = ObjectsCharacteristicsController.EditCharacteristic(model);
			var message = GetPropertyFromJson(result, "Message")?.ParseToStringNullable();

			ObjectsCharacteristicsService.Verify(x => x.AddCharacteristic(It.IsAny<CharacteristicDto>()), Times.Never);
			ObjectsCharacteristicsService.Verify(x => x.EditCharacteristic(It.IsAny<CharacteristicDto>()), Times.Once);
			Assert.AreEqual("Характеристика успешно обновлена", message);
		}
	}
}
