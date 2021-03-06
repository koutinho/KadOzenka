using CommonSdks.RecycleBin;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.Tests;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Tours.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.Tours
{
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
			container.AddTransient(typeof(IRecycleBinService), typeof(RecycleBinService));
			container.AddTransient(typeof(ITourRepository), sp => TourRepository.Object);
		}
	}
}