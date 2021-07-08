using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Repositories;
using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.Modeling.Models
{
	public class BaseModelTests : BaseTests
	{
		protected ModelService ModelService => Provider.GetService<ModelService>();
		protected Mock<IModelFactorsService> ModelFactorsService { get; set; }
		protected Mock<IModelingRepository> ModelingRepository { get; set; }
		protected Mock<IModelObjectsRepository> ModelObjectsRepository { get; set; }


		[SetUp]
		public void BaseModelTestsSetUp()
		{
			ModelingRepository = new Mock<IModelingRepository>();
			ModelObjectsRepository = new Mock<IModelObjectsRepository>();
			ModelFactorsService = new Mock<IModelFactorsService>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ModelService>();
			container.AddTransient(typeof(IModelingRepository), sp => ModelingRepository.Object);
			container.AddTransient(typeof(IModelObjectsRepository), sp => ModelObjectsRepository.Object);
			container.AddTransient(typeof(IModelFactorsService), sp => ModelFactorsService.Object);
		}
	}
}