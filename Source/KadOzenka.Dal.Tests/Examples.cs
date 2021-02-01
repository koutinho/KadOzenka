using KadOzenka.Dal.Tests.Modeling.Models;

namespace KadOzenka.Dal.Tests
{
	public class Examples
	{
		//doc https://github.com/Moq/moq4/wiki/Quickstart

		public void Setup()
		{
			//расширения класса через partial
			var tmp = nameof(OmModelTests.Check_A0_For_Model);

			//выбрасывание исключения
			tmp = nameof(GettingTests.If_Model_Id_Is_Empty_Throw_Exception);

			//стаб репозитория
			tmp = nameof(GettingTests.If_Model_Not_Found_By_Id_Throw_Exception);
		}
	}
}