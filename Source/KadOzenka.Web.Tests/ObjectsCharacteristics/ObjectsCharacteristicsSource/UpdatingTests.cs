using Core.Shared.Extensions;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Web.Models.ObjectsCharacteristics;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.ObjectsCharacteristics.ObjectsCharacteristicsSource
{
	public class UpdatingTests : BaseObjectCharacteristicsTests
	{
		[Test]
		public void Can_Edit_Source()
		{
			var model = new SourceModel
			{
				RegisterId = RandomGenerator.GenerateRandomInteger(),
				Name = RandomGenerator.GetRandomString()
			};

			var result = ObjectsCharacteristicsController.EditSource(model);
			var message = GetPropertyFromJson(result, "Message")?.ParseToStringNullable();

			ObjectsCharacteristicsSourceService.Verify(x => x.AddSource(It.IsAny<SourceDto>()), Times.Never);
			ObjectsCharacteristicsSourceService.Verify(x => x.EditSource(It.IsAny<SourceDto>()), Times.Once);
			Assert.AreEqual("Источник успешно обновлен", message);
		}
	}
}
