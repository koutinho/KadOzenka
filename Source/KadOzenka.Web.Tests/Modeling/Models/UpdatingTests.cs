using System;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Modeling.Models
{
	public class UpdatingTests : BaseModelingTests
	{
		[Test]
		public void CanNot_Update_Automatic_Model_If_Model_State_Is_Invalid()
		{
			var controller = ModelingController;
			var method = new Func<AutomaticModelingModel, IActionResult>(controller.AutomaticModelCard);
			
			CheckMethodValidateModelState(controller, method);
		}
	}
}