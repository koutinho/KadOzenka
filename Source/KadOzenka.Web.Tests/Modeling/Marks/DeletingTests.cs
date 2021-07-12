using KadOzenka.Common.Tests;
using KadOzenka.Web.Exceptions;
using KadOzenka.Web.Models.Modeling;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.Modeling.Marks
{
	public class DeletingTests : BaseMarksTests
	{
		[Test]
		public void CanNot_Delete_Mark_For_Automatic_Model()
		{
			var markId = RandomGenerator.GenerateRandomId();
			var dictionaryId = RandomGenerator.GenerateRandomId();
			MockFactorForAutomaticModel(dictionaryId);

			Assert.Throws<AutomaticModelMarkModificationException>(() => ModelingController.DeleteMark(dictionaryId, markId));
		}
	}
}
