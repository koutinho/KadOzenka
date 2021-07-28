using KadOzenka.Dal.LongProcess.Modeling;
using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Factors;
using ModelingBusiness.Factors.Repositories;
using ModelingBusiness.Model;
using ModelingBusiness.Modeling;
using ModelingBusiness.Objects;
using ModelingBusiness.Objects.Repositories;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.Modeling.Modeling
{
	public class BaseModelingTests : BaseTests
	{
		protected ModelingService ModelingService => Provider.GetService<ModelingService>();
		protected Mock<IModelService> ModelService { get; private set; }
		protected Mock<IModelObjectsService> ModelObjectsService { get; private set; }
		protected Mock<IModelFactorsService> ModelFactorsService { get; private set; }
		protected Mock<IModelDictionaryService> ModelDictionaryService { get; private set; }
		protected Mock<IModelObjectsRepository> ModelObjectsRepository { get; private set; }
		protected Mock<IModelFactorsRepository> ModelFactorsRepository { get; private set; }



		[SetUp]
		public void BaseModelingTestsSetUp()
		{
			ModelService = new Mock<IModelService>();
			ModelObjectsService = new Mock<IModelObjectsService>();
			ModelFactorsService = new Mock<IModelFactorsService>();
			ModelDictionaryService = new Mock<IModelDictionaryService>();

			ModelObjectsRepository = new Mock<IModelObjectsRepository>();
			ModelFactorsRepository = new Mock<IModelFactorsRepository>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ModelingService>();
			container.AddTransient<MarksCalculationLongProcess>();
			container.AddTransient<AutomaticModelParametersCalculationLongProcess>();
			container.AddTransient(typeof(IModelService), x => ModelService.Object);
			container.AddTransient(typeof(IModelObjectsService), x => ModelObjectsService.Object);
			container.AddTransient(typeof(IModelFactorsService), x => ModelFactorsService.Object);
			container.AddTransient(typeof(IModelDictionaryService), x => ModelDictionaryService.Object);
			container.AddTransient(typeof(IModelObjectsRepository), x => ModelObjectsRepository.Object);
			container.AddTransient(typeof(IModelFactorsRepository), x => ModelFactorsRepository.Object);
		}
	}
}