using System.Text.RegularExpressions;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.Tasks
{
	public class BaseTasksTests : BaseTests
	{
		//protected ModelingService ModelingService => Provider.GetService<ModelingService>();
		//protected Mock<IModelFactorsService> ModelFactorsService { get; set; }


		[SetUp]
		public void BaseTasksTestsSetUp()
		{
			//ModelFactorsService = new Mock<IModelFactorsService>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			//container.AddTransient<ModelingService>();
			//container.AddTransient(typeof(IModelingRepository), sp => ModelingRepository.Object);
			//container.AddTransient(typeof(IModelObjectsRepository), sp => ModelObjectsRepository.Object);
			//container.AddTransient(typeof(IModelFactorsService), sp => ModelFactorsService.Object);
		}
	}
}