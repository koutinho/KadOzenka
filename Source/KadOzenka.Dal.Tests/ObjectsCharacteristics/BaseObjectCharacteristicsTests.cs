using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.ObjectsCharacteristics;
using KadOzenka.Dal.ObjectsCharacteristics.Repositories;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.ObjectsCharacteristics
{
	[TestFixture]
	public class BaseObjectCharacteristicsTests : BaseTests
	{
		protected IObjectsCharacteristicsService ObjectsCharacteristicsService => Provider.GetService<IObjectsCharacteristicsService>();
		protected IObjectsCharacteristicsSourceService ObjectsCharacteristicsSourceService => Provider.GetService<IObjectsCharacteristicsSourceService>();
		protected Mock<IObjectCharacteristicsRepository> ObjectCharacteristicsRepository { get; set; }
        protected Mock<IRegisterAttributeService> RegisterAttributeService { get; set; }
		protected Mock<ISRDSessionWrapper> SRDSessionWrapper { get; set; }
		protected Mock<IRegisterConfiguratorWrapper> RegisterConfiguratorWrapper { get; set; }


		[SetUp]
		public void BaseObjectCharacteristicsSetUp()
		{
			ObjectCharacteristicsRepository = new Mock<IObjectCharacteristicsRepository>();
			RegisterService = new Mock<IRegisterService>();
			RegisterAttributeService = new Mock<IRegisterAttributeService>();
			SRDSessionWrapper = new Mock<ISRDSessionWrapper>();
			RegisterConfiguratorWrapper = new Mock<IRegisterConfiguratorWrapper>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient(typeof(IObjectsCharacteristicsService), typeof(ObjectsCharacteristicsService));
			container.AddTransient(typeof(IObjectsCharacteristicsSourceService), typeof(ObjectsCharacteristicsSourceService));
			container.AddTransient(typeof(IObjectCharacteristicsRepository), sp => ObjectCharacteristicsRepository.Object);
			container.AddTransient(typeof(IRegisterService), sp => RegisterService.Object);
			container.AddTransient(typeof(IRegisterAttributeService), sp => RegisterAttributeService.Object);
			container.AddTransient(typeof(ISRDSessionWrapper), sp => SRDSessionWrapper.Object);
			container.AddTransient(typeof(IRegisterConfiguratorWrapper), sp => RegisterConfiguratorWrapper.Object);
		}
	}
}
