using KadOzenka.Web.Tests.Tours;

namespace KadOzenka.Web.Tests
{
	public class Examples
	{
		//doc Moq https://github.com/Moq/moq4/wiki/Quickstart
		//doc Controller Unit Tests https://docs.microsoft.com/ru-ru/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0

		public void Setup()
		{
			//���� ���������� ���������� Json
			var tmp = nameof(Tours.CreationTests.Can_Add_New_Tour);

			//��������, ��� ���������� ��������� ��������� ������ (ModelState.IsValid)
			tmp = nameof(Modeling.Models.UpdatingTests.CanNot_Update_Automatic_Model_If_Model_State_Is_Invalid);

			//�������� ������ Validate � �������, ����������� IValidatableObject
			tmp = nameof(GroupSegmentTests.CanNot_Save_Relation_If_MarketSegment_Is_Empty);
			tmp = nameof(GroupSegmentTests.CanNot_Save_Relation_If_Model_State_Is_Invalid);
		}
	}
}