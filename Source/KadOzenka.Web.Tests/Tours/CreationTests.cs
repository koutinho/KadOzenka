using Core.Shared.Extensions;
using KadOzenka.Common.Tests;
using KadOzenka.Web.Models.Tour;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Tours
{
	public class CreationTests : BaseTourTests
	{
		[Test]
		public void Can_Add_New_Tour()
		{
			var createdTourId = RandomGenerator.GenerateRandomInteger();
			TourService.Setup(x => x.AddTour(It.IsAny<Dal.Tours.Dto.TourDto>())).Returns(createdTourId);

			var result = TourController.TourEstimates(new TourModel { Id = -1, Year = 2017 });
			var id = GetPropertyFromJson(result, "Id")?.ParseToLongNullable();

			Assert.IsNotNull(result);
			Assert.That(id, Is.EqualTo(createdTourId));
			TourService.Verify(x => x.AddTour(It.IsAny<Dal.Tours.Dto.TourDto>()), Times.Once);
			TourService.Verify(x => x.UpdateTour(It.IsAny<Dal.Tours.Dto.TourDto>()), Times.Never);
		}
	}
}