using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.IntegrationTests
{
	public class Tests
	{
		[Test]
		public void Can_Add_Gbu_Object()
		{
			var newObjectId = new OMMainObject
			{
				CadastralNumber = "asd",
				ObjectType_Code = PropertyTypes.Building
			}.Save();

			var savedObject = OMMainObject.Where(x => x.Id == newObjectId).SelectAll().ExecuteFirstOrDefault();
			Assert.That(savedObject, Is.Not.Null);
		}
	}
}