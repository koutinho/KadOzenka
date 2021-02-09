using KadOzenka.Dal.Groups;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Tours.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests.Tours
{
	[TestFixture]
	public class BaseTourTests : BaseTests
	{
		protected TourService TourService => Provider.GetService<TourService>();
		protected Mock<ITourRepository> TourRepository { get; set; }


		[SetUp]
		public void BaseTourSetUp()
		{
			TourRepository = new Mock<ITourRepository>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<TourService>();
			container.AddTransient<TourFactorService>();
			container.AddTransient<GroupService>();
			container.AddTransient<RecycleBinService>();
			container.AddTransient(typeof(ITourRepository), sp => TourRepository.Object);
		}
	}
}