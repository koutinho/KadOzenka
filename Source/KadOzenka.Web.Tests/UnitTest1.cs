using KadOzenka.Dal.Tours.Repositories;
using KadOzenka.Web.Controllers;
using KadOzenka.Web.Models.Tour;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test1()
		{
			var tourRepository = new Mock<ITourRepository>();
			var tourController = new TourController(tourRepository.Object);
			var result = tourController.TourEstimates(new TourModel { Id = -1, Year = 2017 });
			var jsonResult = result as JsonResult;

			Assert.IsNotNull(jsonResult);
			Assert.IsNotNull(jsonResult.Value);
		}
	}
}