using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests
{
	public class BaseTests
	{
		protected ServiceProvider Provider { get; set; }
		

		[OneTimeSetUp]
		public void BaseTestsSetUp()
		{
			ConfigureServices();
		}

		protected virtual void AddServicesToContainer(ServiceCollection container)
		{

		}

		
		#region Support Methods

		private void ConfigureServices()
		{
			var container = new ServiceCollection();

			AddServicesToContainer(container);

			Provider = container.BuildServiceProvider();
		}

		#endregion
	}
}
