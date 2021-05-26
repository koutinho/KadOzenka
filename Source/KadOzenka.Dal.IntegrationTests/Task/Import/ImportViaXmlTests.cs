using System.IO;
using System.Linq;
using System.Threading;
using Core.SRD;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.IntegrationTests.Task._Builders;
using NUnit.Framework;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Task.Import
{
	public class ImportViaXmlTests : BaseTaskTests
	{
		[Test]
		public void Can_Import_Task_Document_Via_Xml()
		{
			var fileName = "one_building_success.xml";
			var task = new TaskBuilder().Document(Document.Id).Build();

			StartImport(task.Id, fileName);

			CheckDataLog(task.Id, 1, 1);
			CheckCreatedUnits(task.Id, 1);
		}

		[Test]
		public void If_CanNot_Parse_Data_From_File_So_Include_Object_In_Total_Counter()
		{
			var fileName = "two_buildings_error.xml";
			var task = new TaskBuilder().Document(Document.Id).Build();

			StartImport(task.Id, fileName);

			CheckDataLog(task.Id, 2, 1);
			CheckCreatedUnits(task.Id, 1);
		}

		
		#region Support Methods

		private void StartImport(long taskId, string fileName)
		{
			using (var stream = File.OpenRead($"{PathToFileFolder}{fileName}"))
			{
				var import = DataImporterGknLongProcess.SaveImportDataLog(fileName, stream, taskId);

				new DataImporterGknLongProcess().StartProcess(new OMProcessType(),
					new OMQueue
					{
						Status_Code = Status.Added,
						UserId = SRDSession.GetCurrentUserId(),
						ObjectId = import.Id
					}, new CancellationTokenSource().Token);
			}
		}

		private void CheckDataLog(long taskId, int totalObjectsCount, int importedObjectsCount)
		{
			var dataLog = OMImportDataLog.Where(x => x.ObjectId == taskId).SelectAll().ExecuteFirstOrDefault();
			Assert.That(dataLog.Status_Code, Is.EqualTo(ObjectModel.Directory.Common.ImportStatus.Completed), dataLog.ResultMessage);
			Assert.That(dataLog.TotalNumberOfObjects, Is.EqualTo(totalObjectsCount));
			Assert.That(dataLog.NumberOfImportedObjects, Is.EqualTo(importedObjectsCount));
		}

		private void CheckCreatedUnits(long taskId, int numberOfUnits)
		{
			var units = OMUnit.Where(x => x.TaskId == taskId).SelectAll().Execute();
			Assert.That(units.Count, Is.EqualTo(numberOfUnits));
			Assert.That(units.First().PropertyType_Code, Is.EqualTo(PropertyTypes.Building));
		}

		#endregion
	}
}
