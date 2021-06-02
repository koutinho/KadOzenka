using KadOzenka.Dal.Modeling;
using NUnit.Framework;

namespace KadOzenka.Dal.IntegrationTests.Modeling
{
	public class BaseGbuObjectTests : BaseTests
	{
		protected IModelingService ModelingService { get; set; }


		[OneTimeSetUp]
		protected void OneTimeSetUpForModeling()
		{
			ModelingService = new ModelingService();
		}
	}
}
