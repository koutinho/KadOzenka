using KadOzenka.Common.Tests;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.Modeling.Models
{
	public class GettingTests : BaseModelingTests
	{
		[Test]
		public void If_Model_Group_Is_Not_Found_Redirect_To_NoGroup_View()
		{
			var modelId = RandomGenerator.GenerateRandomInteger();
			ModelService.Setup(x => x.IsModelGroupExist(modelId)).Returns(false);

			var result = ModelingController.ModelCard(modelId);
			var redirect = result as RedirectToActionResult;

			Assert.IsNotNull(redirect);
			Assert.Null(redirect.ControllerName);
			Assert.That(redirect.ActionName, Is.EqualTo(nameof(ModelingController.ModelWithDeletedGroupCard)));
		}
	}
}
