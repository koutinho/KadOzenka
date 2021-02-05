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