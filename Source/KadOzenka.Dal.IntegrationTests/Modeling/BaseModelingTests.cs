using ModelingBusiness.Dictionaries;
using ModelingBusiness.Model;
using ModelingBusiness.Objects;
using ModelingBusiness.Objects.Import;
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
