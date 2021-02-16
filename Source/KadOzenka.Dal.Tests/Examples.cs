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
			//пример теста с входными и выходными параметрами
			var tmp = nameof(OmModelTests.Check_A0_For_Model);

			//выбрасывание общего исключения (Exception)
			tmp = nameof(GettingTests.If_Model_Id_Is_Empty_Throw_Exception);

			//стаб репозитория и выбрасывание специального исключения
			tmp = nameof(GettingTests.If_Model_Not_Found_By_Id_Throw_Exception);

			//проверка, что данные не сохранились (не вызвался метод репозитория)
			tmp = nameof(Tours.CreationTests.CanNot_Create_Tour_Without_Year);

			//проверка, что данные сохранились (вызвался метод репозитория с определенными входными параметрами)
			tmp = nameof(Tours.CreationTests.Can_Create_Tour);


			//примеры тестов для длительных процессов
			tmp = nameof(ModelDictionaryImportFromExcelLongProcessTest); //простой
			tmp = nameof(TaskForCodLongProcessTest); //сложнее, возможно, подобный код не нужно тестить
		}
	}
}