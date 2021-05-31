using KadOzenka.Dal.Integration._Builders.Task;
using NUnit.Framework;
using ObjectModel.Directory;

namespace KadOzenka.Dal.IntegrationTests.Task.Import
{
	public class XmlTests : BaseImportTests
	{
		[Test]
		public void Can_Import_Task_Document()
		{
			var fileName = "one_building_success.xml";
			var task = new TaskBuilder().Document(Document.Id).Build();

			StartImport(task.Id, fileName);

			CheckDataLog(task.Id, 1, 1);
			CheckCreatedUnits(task.Id, 1, PropertyTypes.Building);
		}

		[Test]
		public void If_CanNot_Parse_Data_From_File_So_Include_Object_In_Total_Counter()
		{
			var fileName = "two_buildings_error.xml";
			var task = new TaskBuilder().Document(Document.Id).Build();

			StartImport(task.Id, fileName);

			CheckDataLog(task.Id, 2, 1);
			CheckCreatedUnits(task.Id, 1, PropertyTypes.Building);
		}
	}
}
