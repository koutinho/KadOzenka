using KadOzenka.Common.Tests;
using KadOzenka.Web.Exceptions;
using KadOzenka.Web.Models.Modeling;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.Modeling.Marks
{
	public class UpdatingTests : BaseMarksTests
	{
		[Test]
		public void CanNot_Update_Mark_For_Automatic_Model()
		{
			var dictionaryId = RandomGenerator.GenerateRandomId();
			MockFactorForAutomaticModel(dictionaryId);

			Assert.Throws<AutomaticModelMarkModificationException>(() => ModelingController.UpdateMark(dictionaryId, new MarkModel()));
		}
	}
}
