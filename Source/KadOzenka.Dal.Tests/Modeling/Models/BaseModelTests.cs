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


		[OneTimeSetUp]
		public void BaseModelTestsOneTimeSetUp()
		{
			ModelingRepository = new Mock<IModelingRepository>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ModelingService>();
			container.AddTransient(typeof(IModelingRepository), sp => ModelingRepository.Object);
		}
	}
}