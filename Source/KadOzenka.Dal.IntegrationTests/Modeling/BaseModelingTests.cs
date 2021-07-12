using KadOzenka.Dal.Modeling;
using NUnit.Framework;

namespace KadOzenka.Dal.IntegrationTests.Modeling
{
	public class BaseModelingTests : BaseTests
	{
		protected string PathToFileFolder => @".\Modeling\_Files\";

		protected IModelService ModelService { get; set; }
		protected IModelDictionaryService ModelDictionaryService { get; set; }


		[OneTimeSetUp]
		protected void OneTimeSetUpForModeling()
		{
			ModelService = new ModelService();
			ModelDictionaryService = new ModelDictionaryService();
		}
	}
}
