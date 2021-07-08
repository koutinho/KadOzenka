using KadOzenka.Dal.Modeling;
using NUnit.Framework;

namespace KadOzenka.Dal.IntegrationTests.Modeling
{
	public class BaseGbuObjectTests : BaseTests
	{
		protected IModelService ModelService { get; set; }


		[OneTimeSetUp]
		protected void OneTimeSetUpForModeling()
		{
			ModelService = new ModelService();
		}
	}
}
