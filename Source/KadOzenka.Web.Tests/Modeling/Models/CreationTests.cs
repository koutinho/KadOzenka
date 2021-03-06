using System;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.Modeling.Models
{
	public class CreationTests : BaseModelingTests
	{
		[Test]
		public void CanNot_Add_Model_If_Model_State_Is_Invalid()
		{
			var controller = ModelingController;
			var method = new Func<GeneralModelingModel, IActionResult>(controller.AddModel);
			
			CheckMethodValidateModelState(controller, method);
		}
	}
}