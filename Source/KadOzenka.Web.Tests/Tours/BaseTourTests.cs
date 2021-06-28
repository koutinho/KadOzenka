using KadOzenka.Web.Controllers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.Tours
{
	public class BaseTourTests : BaseTests
	{
		protected TourController TourController => Provider.GetService<TourController>();


		[SetUp]
		public void BaseTourSetUp()
		{

		}



		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<TourController>();
		}
	}
}