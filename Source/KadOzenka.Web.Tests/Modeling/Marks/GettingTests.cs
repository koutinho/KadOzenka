using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Modeling.Marks
{
	public class GettingTests : BaseModelingTests
	{
		[Test]
		public void Can_Get_View_With_Marks_Grid()
		{
			var groupId = RandomGenerator.GenerateRandomInteger();
			var factorId = RandomGenerator.GenerateRandomInteger();

			var result = ModelingController.MarksGrid(groupId, factorId);
			var view = result as ViewResult;

			Assert.IsNotNull(view);
			Assert.That(view.ViewData["GroupId"], Is.EqualTo(groupId));
			Assert.That(view.ViewData["FactorId"], Is.EqualTo(factorId));
		}
	}
}
