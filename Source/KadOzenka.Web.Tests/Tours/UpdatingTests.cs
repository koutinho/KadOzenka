using Core.Shared.Extensions;
using KadOzenka.Common.Tests;
using KadOzenka.Web.Models.Tour;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.Tours
{
	public class UpdatingTests : BaseTourTests
	{
		[Test]
		public void Can_Update_Tour()
		{
			var updatedTouId = RandomGenerator.GenerateRandomInteger();
			TourService.Setup(x => x.UpdateTour(It.IsAny<Dal.Tours.Dto.TourDto>())).Returns(updatedTouId);

			var result = TourController.TourEstimates(new TourModel { Id = updatedTouId, Year = 2017 });
			var id = GetPropertyFromJson(result, "Id")?.ParseToLongNullable();

			Assert.IsNotNull(result);
			Assert.That(id, Is.EqualTo(updatedTouId));
			TourService.Verify(x => x.UpdateTour(It.IsAny<Dal.Tours.Dto.TourDto>()), Times.Once);
			TourService.Verify(x => x.AddTour(It.IsAny<Dal.Tours.Dto.TourDto>()), Times.Never);
		}
	}
}