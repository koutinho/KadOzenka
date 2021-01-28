using KadOzenka.Dal.Tests.Modeling.Models;

namespace KadOzenka.Dal.Tests
{
	public class Tests
	{
		public void Setup()
		{
			//тест на расширения класса через partial
			var tmp = nameof(OmModelTests.Check_A0_For_Model);

			//тест на выбрасывание исключения
			tmp = nameof(GettingTests.If_Model_Id_Is_Empty_Throw_Exception);
		}
	}
}