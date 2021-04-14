using KadOzenka.Dal.Registers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Tests
{
	public class BaseTests
	{
		protected ServiceProvider Provider { get; set; }
        protected Mock<IRegisterService> RegisterService { get; set; }

		[OneTimeSetUp]
		public void BaseTestsSetUp()
		{
			ConfigureServices();
            RegisterService = new Mock<IRegisterService>();
		}

		protected virtual void AddServicesToContainer(ServiceCollection container)
		{

		}

		protected void MapRegisterCreation(OMRegister register)
        {
			RegisterService.Setup(x => x.CreateRegister(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<long>())).Returns(register);
		}

		
		#region Support Methods

		private void ConfigureServices()
		{
			var container = new ServiceCollection();

			AddServicesToContainer(container);

            container.AddTransient(typeof(IRegisterService), sp => RegisterService.Object);

			Provider = container.BuildServiceProvider();
		}

		#endregion
	}
}
