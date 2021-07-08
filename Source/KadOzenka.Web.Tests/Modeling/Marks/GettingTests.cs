using KadOzenka.Common.Tests;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.Modeling.Marks
{
	public class GettingTests : BaseMarksTests
	{
		[Test]
		public void Can_Get_View_With_Marks_Grid()
		{
			var isReadOnly = true;
			var dictionaryId = RandomGenerator.GenerateRandomInteger();

			var result = ModelingController.MarksGrid(isReadOnly, dictionaryId);
			var view = result as PartialViewResult;

			Assert.IsNotNull(view);
			Assert.That(view.ViewData["IsReadOnly"], Is.EqualTo(isReadOnly));
			Assert.That(view.ViewData["DictionaryId"], Is.EqualTo(dictionaryId));
		}
	}
}
