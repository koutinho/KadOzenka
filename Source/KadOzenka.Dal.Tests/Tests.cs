using KadOzenka.Dal.Tests.Modeling.Models;

namespace KadOzenka.Dal.Tests
{
	public class Tests
	{
		public void Setup()
		{
			//���� �� ���������� ������ ����� partial
			var tmp = nameof(OmModelTests.Check_A0_For_Model);

			//���� �� ������������ ����������
			tmp = nameof(GettingTests.If_Model_Id_Is_Empty_Throw_Exception);
		}
	}
}