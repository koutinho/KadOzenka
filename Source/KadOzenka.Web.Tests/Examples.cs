using KadOzenka.Web.UnitTests.Modeling.Marks;
using KadOzenka.Web.UnitTests.Tours;
using CreationTests = KadOzenka.Web.UnitTests.Modeling.Factors.CreationTests;
using UpdatingTests = KadOzenka.Web.UnitTests.Modeling.Models.UpdatingTests;

namespace KadOzenka.Web.UnitTests
{
	public class Examples
	{
		//doc Moq https://github.com/Moq/moq4/wiki/Quickstart
		//doc Controller Unit Tests https://docs.microsoft.com/ru-ru/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0

		public void Setup()
		{
			//если контроллер возвращает Json
			var tmp = nameof(UnitTests.Tours.CreationTests.Can_Add_New_Tour);

			//если контроллер возвращает View
			tmp = nameof(CreationTests.Can_Return_View_During_Automatic_Factor_Addition);

			//если контроллер возвращает редирект (RedirectToAction)
			tmp = nameof(UnitTests.Modeling.Models.GettingTests.If_Model_Group_Is_Not_Found_Redirect_To_NoGroup_View);

			//если контроллер заполняет ViewBag
			tmp = nameof(GettingTests.Can_Get_View_With_Marks_Grid);

			//если контроллер заполняет ViewData
			tmp = nameof(AttributesTests.Can_Get_View_With_Attributes_Settings);

			//проверка, что контроллер проверяет состояние модели (ModelState.IsValid)
			tmp = nameof(UpdatingTests.CanNot_Update_Automatic_Model_If_Model_State_Is_Invalid);

			//проверка метода Validate у моделей, реализующих IValidatableObject
			tmp = nameof(GroupSegmentTests.CanNot_Save_Relation_If_MarketSegment_Is_Empty);
			tmp = nameof(GroupSegmentTests.CanNot_Save_Relation_If_Model_State_Is_Invalid);
		}
	}
}