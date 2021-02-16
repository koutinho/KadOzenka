using KadOzenka.Dal.Tests.Modeling.Dictionaries;
using KadOzenka.Dal.Tests.Modeling.Models;
using KadOzenka.Dal.Tests.Tasks.LongProcess;

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
			tmp = nameof(Tours.CreationTests.CanNot_Create_Tour_Without_Year);

			//��������, ��� ������ ����������� (�������� ����� ����������� � ������������� �������� �����������)
			tmp = nameof(Tours.CreationTests.Can_Create_Tour);


			//������� ������ ��� ���������� ���������
			tmp = nameof(ModelDictionaryImportFromExcelLongProcessTest); //�������
			tmp = nameof(TaskForCodLongProcessTest); //�������, ��������, �������� ��� �� ����� �������
		}
	}
}