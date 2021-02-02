using KadOzenka.Dal.Tests.Modeling.Models;

namespace KadOzenka.Dal.Tests
{
	public class Examples
	{
		//doc https://github.com/Moq/moq4/wiki/Quickstart

		public void Setup()
		{
			//���������� ������ ����� partial
			var tmp = nameof(OmModelTests.Check_A0_For_Model);

			//������������ ������ ���������� (Exception)
			tmp = nameof(GettingTests.If_Model_Id_Is_Empty_Throw_Exception);

			//���� ����������� � ������������ ������������ ����������
			tmp = nameof(GettingTests.If_Model_Not_Found_By_Id_Throw_Exception);

			//��������, ��� ������ �� ����������� (�� �������� ����� �����������)
			tmp = nameof(CreationTests.CanNot_Create_Model_With_Empty_Name);

			//��������, ��� ������ ����������� (�������� ����� ����������� � ������������� �������� �����������)
			tmp = nameof(UpdatingTests.Can_Make_Manual_Model_Active_If_There_No_Other_Models_For_Group);
		}
	}
}