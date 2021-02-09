using System;
using System.Collections.Generic;
using System.Text;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Modeling.Models
{
	public class GettingTests : BaseModelingTests
	{
		[Test]
		public void If_Model_Group_Is_Not_Found_Redirect_To_NoGroup_View()
		{
			var modelId = RandomGenerator.GenerateRandomInteger();
			ModelingService.Setup(x => x.IsModelGroupExist(modelId)).Returns(false);

			var result = ModelingController.ModelCard(modelId);
			var redirect = result as RedirectToActionResult;

			Assert.IsNotNull(redirect);
			Assert.Null(redirect.ControllerName);
			Assert.That(redirect.ActionName, Is.EqualTo(nameof(ModelingController.ModelWithDeletedGroupCard)));
		}
	}
}
