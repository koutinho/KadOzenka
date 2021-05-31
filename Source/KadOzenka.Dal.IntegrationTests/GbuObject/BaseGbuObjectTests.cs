using KadOzenka.Dal.IntegrationTests;
using KadOzenka.Dal.Modeling;
using NUnit.Framework;

namespace KadOzenka.Dal.Integration.GbuObject
{
	public class BaseGbuObjectTests : BaseTests
	{
		protected IModelingService ModelingService { get; set; }


		[OneTimeSetUp]
		protected void OneTimeSetUpForGbuObject()
		{
			ModelingService = new ModelingService();
		}
	}
}
