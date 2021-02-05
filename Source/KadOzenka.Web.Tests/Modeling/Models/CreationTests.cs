using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Web.Controllers;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Modeling.Models
{
	public class UpdatingTests : BaseModelingTests
	{
		[Test]
		public void CanNot_Update_Automatic_Model_If_Model_State_Is_Invalid()
		{
			var controller = (ModelingController) Provider.GetService(typeof(ModelingController));
			controller.ModelState.AddModelError(RandomGenerator.GetRandomString(), RandomGenerator.GetRandomString());

			var result = controller.AutomaticModelCard(new AutomaticModelingModel());
			var errors = GetPropertyFromJson(result, "Errors");

			Assert.IsNotNull(errors);
			Assert.That(controller.ModelState.IsValid, Is.False);
			ModelingService.Verify(x => x.UpdateAutomaticModel(It.IsAny<ModelingModelDto>()), Times.Never);
		}
	}
}