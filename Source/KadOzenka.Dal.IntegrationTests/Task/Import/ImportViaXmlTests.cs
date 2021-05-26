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

			using (var stream = File.OpenRead($"{PathToFileFolder}{fileName}"))
			{
				var import = DataImporterGknLongProcess.SaveImportDataLog(fileName, stream, task.Id);

				new DataImporterGknLongProcess().StartProcess(new OMProcessType(),
					new OMQueue
					{
						Status_Code = Status.Added,
						UserId = SRDSession.GetCurrentUserId(),
						ObjectId = import.Id
					}, new CancellationTokenSource().Token);
			}

			var dataLog = OMImportDataLog.Where(x => x.ObjectId == task.Id).SelectAll().ExecuteFirstOrDefault();
			Assert.That(dataLog.Status_Code, Is.EqualTo(ObjectModel.Directory.Common.ImportStatus.Completed), dataLog.ResultMessage);
			Assert.That(dataLog.TotalNumberOfObjects, Is.EqualTo(1));
			Assert.That(dataLog.NumberOfImportedObjects, Is.EqualTo(1));

			var units = OMUnit.Where(x => x.TaskId == task.Id).SelectAll().Execute();
			Assert.That(units.Count, Is.EqualTo(1));
			Assert.That(units.First().PropertyType_Code, Is.EqualTo(PropertyTypes.Building));
		}
	}
}
