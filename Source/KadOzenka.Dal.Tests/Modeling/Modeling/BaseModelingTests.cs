using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Factors;
using ModelingBusiness.Modeling;
using ModelingBusiness.Objects;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.Modeling.Modeling
{
	public class BaseModelingTests : BaseTests
	{
		protected ModelingService ModelingService => Provider.GetService<ModelingService>();
		protected Mock<IModelObjectsService> ModelObjectsService { get; private set; }
		protected Mock<IModelFactorsService> ModelFactorsService { get; private set; }
		protected Mock<IModelDictionaryService> ModelDictionaryService { get; private set; }


		[SetUp]
		public void BaseModelingTestsSetUp()
		{
			ModelObjectsService = new Mock<IModelObjectsService>();
			ModelFactorsService = new Mock<IModelFactorsService>();
			ModelDictionaryService = new Mock<IModelDictionaryService>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ModelingService>();
			container.AddTransient(typeof(IModelObjectsService), x => ModelObjectsService.Object);
			container.AddTransient(typeof(IModelFactorsService), x => ModelFactorsService.Object);
			container.AddTransient(typeof(IModelDictionaryService), x => ModelDictionaryService.Object);
		}
	}
}