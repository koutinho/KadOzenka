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

			//выбрасывание общего исключения (Exception)
			tmp = nameof(GettingTests.If_Model_Id_Is_Empty_Throw_Exception);

			//стаб репозитория и выбрасывание специального исключения
			tmp = nameof(GettingTests.If_Model_Not_Found_By_Id_Throw_Exception);

			//проверка, что данные не сохранились (не вызвался метод репозитория)
			tmp = nameof(CreationTests.CanNot_Create_Model_With_Empty_Name);

			//проверка, что данные сохранились (вызвался метод репозитория с определенными входными параметрами)
			tmp = nameof(UpdatingTests.Can_Make_Manual_Model_Active_If_There_No_Other_Models_For_Group);
		}
	}
}