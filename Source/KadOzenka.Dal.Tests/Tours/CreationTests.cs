using System;
using System.Linq.Expressions;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.Tours.Dto;
using KadOzenka.Dal.Tours.Exceptions;
using KadOzenka.Dal.Tours.Resources;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Tours
{
	[TestFixture]
	public class CreationTests : BaseTourTests
	{
		[TestCase(0)]
		[TestCase(null)]
		public void CanNot_Create_Tour_Without_Year(long? tourYear)
		{
			var tourDto = new TourDto {Year = tourYear};

			var exception = Assert.Throws<Exception>(() => TourService.AddTour(tourDto));

			StringAssert.Contains(Messages.EmptyTourYear, exception.Message);
			TourRepository.Verify(foo => foo.Save(It.IsAny<OMTour>()), Times.Never);
		}


		[Test]
		public void CanNot_Create_Tour_If_Tour_With_The_Same_Year_Already_Exists()
		{
			var tourDto = new TourDto { Year = RandomGenerator.GenerateRandomInteger() };
			TourRepository.Setup(y => y.IsExists(x => x.Year == tourDto.Year && x.Id != tourDto.Id)).Returns(true);

			Assert.Throws<TourAlreadyExistsException>(() => TourService.AddTour(tourDto));

			TourRepository.Verify(foo => foo.Save(It.IsAny<OMTour>()), Times.Never);
		}

		[Test]
		public void Can_Create_Tour()
		{
			OMTour inputTour = null;
			var tourDto = new TourDto {Year = RandomGenerator.GenerateRandomInteger() };
			TourRepository.Setup(x => x.IsExists(It.IsAny<Expression<Func<OMTour, bool>>>())).Returns(false);
			TourRepository.Setup(x => x.Save(It.IsAny<OMTour>())).Callback<OMTour>(x => inputTour = x);

			TourService.AddTour(tourDto);

			TourRepository.Verify(foo => foo.Save(It.IsAny<OMTour>()), Times.Once);
			Assert.That(inputTour.Year, Is.EqualTo(tourDto.Year));
		}
	}
}