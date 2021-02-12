using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests.Modeling.Models
{
	[TestFixture]
	public class BaseModelTests : BaseTests
	{
		protected ModelingService ModelingService => Provider.GetService<ModelingService>();
		protected Mock<IModelingRepository> ModelingRepository { get; set; }
		protected Mock<IModelObjectsRepository> ModelObjectsRepository { get; set; }


		[SetUp]
		public void BaseModelTestsSetUp()
		{
			ModelingRepository = new Mock<IModelingRepository>();
			ModelObjectsRepository = new Mock<IModelObjectsRepository>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ModelingService>();
			container.AddTransient(typeof(IModelingRepository), sp => ModelingRepository.Object);
			container.AddTransient(typeof(IModelObjectsRepository), sp => ModelObjectsRepository.Object);
		}
	}
}