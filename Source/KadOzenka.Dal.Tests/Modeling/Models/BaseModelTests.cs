using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Dictionaries.Repositories;
using ModelingBusiness.Factors;
using ModelingBusiness.Factors.Repositories;
using ModelingBusiness.Model;
using ModelingBusiness.Model.Repositories;
using ModelingBusiness.Objects.Repositories;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.Modeling.Models
{
	public class BaseModelTests : BaseTests
	{
		protected ModelService ModelService => Provider.GetService<ModelService>();
		protected ModelDictionaryService ModelDictionaryService => Provider.GetService<ModelDictionaryService>();
		protected Mock<IModelFactorsService> ModelFactorsService { get; set; }
		protected Mock<IModelRepository> ModelingRepository { get; set; }
		protected Mock<IModelObjectsRepository> ModelObjectsRepository { get; set; }
		protected Mock<IModelDictionaryRepository> ModelDictionaryRepository { get; set; }
		protected Mock<IModelMarksRepository> ModelMarksRepository { get; set; }


		[SetUp]
		public void BaseModelTestsSetUp()
		{
			ModelingRepository = new Mock<IModelRepository>();
			ModelObjectsRepository = new Mock<IModelObjectsRepository>();
			ModelDictionaryRepository = new Mock<IModelDictionaryRepository>();
			ModelMarksRepository = new Mock<IModelMarksRepository>();
			ModelFactorsService = new Mock<IModelFactorsService>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ModelService>();
			container.AddTransient<ModelDictionaryService>();
			container.AddTransient(typeof(IModelRepository), sp => ModelingRepository.Object);
			container.AddTransient(typeof(IModelObjectsRepository), sp => ModelObjectsRepository.Object);
			container.AddTransient(typeof(IModelDictionaryRepository), sp => ModelDictionaryRepository.Object);
			container.AddTransient(typeof(IModelMarksRepository), sp => ModelMarksRepository.Object);
			container.AddTransient(typeof(IModelFactorsService), sp => ModelFactorsService.Object);
		}
	}
}