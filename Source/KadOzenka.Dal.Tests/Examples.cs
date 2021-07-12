using KadOzenka.Dal.UnitTests.Modeling.Dictionaries;
using KadOzenka.Dal.UnitTests.Modeling.Models;
using KadOzenka.Dal.UnitTests.Tasks.LongProcess;
using CreationTests = KadOzenka.Dal.UnitTests.Tours.CreationTests;

namespace KadOzenka.Dal.Tests
{
	public class Examples
	{
		//doc https://github.com/Moq/moq4/wiki/Quickstart

		public void Setup()
		{
			//������ ����� � �������� � ��������� �����������
			var tmp = nameof(OmModelTests.Check_A0_For_Model);

			//������������ ������ ���������� (Exception)
			tmp = nameof(GettingTests.If_Model_Id_Is_Empty_Throw_Exception);

			//���� ����������� � ������������ ������������ ����������
			tmp = nameof(GettingTests.If_Model_Not_Found_By_Id_Throw_Exception);

			//��������, ��� ������ �� ����������� (�� �������� ����� �����������)
			tmp = nameof(CreationTests.CanNot_Create_Tour_Without_Year);

			//��������, ��� ������ ����������� (�������� ����� ����������� � ������������� �������� �����������)
			tmp = nameof(CreationTests.Can_Create_Tour);


			//������� ������ ��� ���������� ���������
			tmp = nameof(TaskForCodLongProcessTest); //�������, ��������, �������� ��� �� ����� �������
		}
	}
}