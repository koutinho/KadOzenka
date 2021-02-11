using System.Linq;
using KadOzenka.Web.Models.Tour;
using NUnit.Framework;
using ObjectModel.Directory;

namespace KadOzenka.Web.Tests.Tours
{
	public class GroupSegmentTests : BaseTourTests
	{
		[Test]
		public void CanNot_Save_Relation_If_MarketSegment_Is_Empty()
		{
			var model = new GroupSegmentSettingsModel
			{
				GroupId = RandomGenerator.GenerateRandomInteger(),
				MarketSegment = MarketSegment.None,
				TerritoryType = TerritoryType.Main
			};

			var errors = ModelValidator.Validate(model);

			Assert.That(errors.Count, Is.EqualTo(1));
			Assert.IsTrue(errors.First().ErrorMessage.Contains(GroupSegmentSettingsModel.NoSegmentErrorMessage),
				errors.GetAllErrorMessagesAsOneString());
		}

		[Test]
		public void CanNot_Save_Relation_If_Territory_Is_Empty()
		{
			var model = new GroupSegmentSettingsModel
			{
				GroupId = RandomGenerator.GenerateRandomInteger(),
				MarketSegment = MarketSegment.Appartment,
				TerritoryType = TerritoryType.None
			};

			var errors = ModelValidator.Validate(model);

			Assert.That(errors.Count, Is.EqualTo(1));
			Assert.IsTrue(errors.First().ErrorMessage.Contains(GroupSegmentSettingsModel.NoTerritoryErrorMessage),
				errors.GetAllErrorMessagesAsOneString());
		}

		[Test]
		public void CanNot_Save_Relation_If_Model_State_Is_Invalid()
		{
			var controller = TourController;
			var method = new ControllerMethod<GroupSegmentSettingsModel>(controller.GroupSegmentSettingsSubCard);

			CheckMethodValidateModelState(controller, method);
		}
	}
}
