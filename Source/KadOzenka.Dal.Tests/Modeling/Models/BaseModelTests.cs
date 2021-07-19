using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Dictionaries;
using KadOzenka.Dal.Modeling.Dictionaries.Repositories;
using KadOzenka.Dal.Modeling.Factors;
using KadOzenka.Dal.Modeling.Model;
using KadOzenka.Dal.Modeling.Model.Repositories;
using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
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
		protected Mock<IModelingRepository> ModelingRepository { get; set; }
		protected Mock<IModelObjectsRepository> ModelObjectsRepository { get; set; }
		protected Mock<IModelDictionaryRepository> ModelDictionaryRepository { get; set; }
		protected Mock<IModelMarksRepository> ModelMarksRepository { get; set; }


		[SetUp]
		public void BaseModelTestsSetUp()
		{
			ModelingRepository = new Mock<IModelingRepository>();
			ModelObjectsRepository = new Mock<IModelObjectsRepository>();
			ModelDictionaryRepository = new Mock<IModelDictionaryRepository>();
			ModelMarksRepository = new Mock<IModelMarksRepository>();
			ModelFactorsService = new Mock<IModelFactorsService>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ModelService>();
			container.AddTransient<ModelDictionaryService>();
			container.AddTransient(typeof(IModelingRepository), sp => ModelingRepository.Object);
			container.AddTransient(typeof(IModelObjectsRepository), sp => ModelObjectsRepository.Object);
			container.AddTransient(typeof(IModelDictionaryRepository), sp => ModelDictionaryRepository.Object);
			container.AddTransient(typeof(IModelMarksRepository), sp => ModelMarksRepository.Object);
			container.AddTransient(typeof(IModelFactorsService), sp => ModelFactorsService.Object);
		}
	}
}