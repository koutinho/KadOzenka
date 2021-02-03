using KadOzenka.Dal.Tours;
using KadOzenka.Web.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Tours
{
	[TestFixture]
	public class BaseTourTests : BaseTests
	{
		protected TourController TourController => Provider.GetService<TourController>();
		protected Mock<ITourService> TourService { get; set; }


		[SetUp]
		public void BaseTourSetUp()
		{
			TourService = new Mock<ITourService>();
		}



		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<TourController>();
			container.AddTransient(typeof(ITourService), sp => TourService.Object);
		}
	}
}