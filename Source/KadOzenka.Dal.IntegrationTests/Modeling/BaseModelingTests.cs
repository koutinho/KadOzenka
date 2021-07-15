using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Dictionaries;
using KadOzenka.Dal.Modeling.Model;
using KadOzenka.Dal.Modeling.Objects;
using KadOzenka.Dal.Modeling.Objects.Import;
using NUnit.Framework;

namespace KadOzenka.Dal.IntegrationTests.Modeling
{
	public class BaseModelingTests : BaseTests
	{
		protected string PathToFileFolder => @".\Modeling\_Files\";

		protected IModelService ModelService { get; set; }
		protected IModelObjectsService ModelObjectsService { get; set; }
		protected ModelObjectsImporter ModelObjectsImporter { get; set; }
		protected IModelDictionaryService ModelDictionaryService { get; set; }


		[OneTimeSetUp]
		protected void OneTimeSetUpForModeling()
		{
			ModelService = new ModelService();
			ModelObjectsService = new ModelObjectsService();
			ModelDictionaryService = new ModelDictionaryService();
			ModelObjectsImporter = new ModelObjectsImporter();
		}
	}
}
