using System;
using System.Linq;
using KadOzenka.Common.Tests;
using KadOzenka.Web.Models.Tour;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ObjectModel.Directory;

namespace KadOzenka.Web.UnitTests.Tours
{
	public class GroupSegmentTests : BaseTourTests
	{
		[Test]
		public void CanNot_Save_Relation_If_MarketSegment_Is_Empty()
		{
			var model = new GroupSegmentSettingsModel
			{
				GroupId = RandomGenerator.GenerateRandomInteger(),
				MarketSegment = MarketSegment.None
			};

			var errors = ModelValidator.Validate(model);

			Assert.That(errors.Count, Is.EqualTo(1));
			Assert.IsTrue(errors.First().ErrorMessage.Contains(GroupSegmentSettingsModel.NoSegmentErrorMessage),
				errors.GetAllErrorMessagesAsOneString());
		}

		[Test]
		public void CanNot_Save_Relation_If_Model_State_Is_Invalid()
		{
			var controller = TourController;
			var method = new Func<GroupSegmentSettingsModel, IActionResult>(controller.GroupSegmentSettingsSubCard);

			CheckMethodValidateModelState(controller, method);
		}
	}
}
