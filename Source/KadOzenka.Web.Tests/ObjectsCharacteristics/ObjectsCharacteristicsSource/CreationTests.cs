using Core.Shared.Extensions;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;
using KadOzenka.Web.Models.ObjectsCharacteristics;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.ObjectsCharacteristics.ObjectsCharacteristicsSource
{
	public class CreationTests : BaseObjectCharacteristicsTests
	{
		[Test]
		public void Can_Add_Source()
		{
			var model = new SourceModel
			{
				RegisterId = -1,
				Name = RandomGenerator.GetRandomString()
			};

			var result = ObjectsCharacteristicsController.EditSource(model);
			var message = GetPropertyFromJson(result, "Message")?.ParseToStringNullable();

			ObjectsCharacteristicsSourceService.Verify(x => x.AddSource(It.IsAny<SourceDto>()), Times.Once);
			ObjectsCharacteristicsSourceService.Verify(x => x.EditSource(It.IsAny<SourceDto>()), Times.Never);
			Assert.AreEqual("Источник успешно сохранен", message);
		}

		[Test]
		public void CanNot_Add_Source_If_Model_State_Is_Invalid()
		{
			var controller = ObjectsCharacteristicsController;
			var method = new ControllerMethod<SourceModel>(controller.EditSource);

			CheckMethodValidateModelState(controller, method);
		}
	}
}
