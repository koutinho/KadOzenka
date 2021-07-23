using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CommonSdks.Excel;
using Core.SRD;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using NUnit.Framework;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Task.Import
{
	public class BaseImportTests : BaseTaskTests
	{
		protected void StartImport(long taskId, string fileName, List<ColumnToAttributeMapping> columnsMapping = null)
		{
			using (var stream = File.OpenRead($"{PathToFileFolder}{fileName}"))
			{
				var import = DataImporterGknLongProcess.SaveImportDataLog(fileName, stream, taskId, columnsMapping);

				new DataImporterGknLongProcess().StartProcess(new OMProcessType(),
					new OMQueue
					{
						Status_Code = Status.Added,
						UserId = SRDSession.GetCurrentUserId(),
						ObjectId = import.Id
					}, new CancellationTokenSource().Token);
			}
		}

		protected void CheckDataLog(long taskId, int totalObjectsCount, int importedObjectsCount)
		{
			var dataLog = OMImportDataLog.Where(x => x.ObjectId == taskId).SelectAll().ExecuteFirstOrDefault();
			Assert.That(dataLog.Status_Code, Is.EqualTo(ObjectModel.Directory.Common.ImportStatus.Completed), dataLog.ResultMessage);
			Assert.That(dataLog.TotalNumberOfObjects, Is.EqualTo(totalObjectsCount));
			Assert.That(dataLog.NumberOfImportedObjects, Is.EqualTo(importedObjectsCount));
		}

		protected void CheckCreatedUnits(long taskId, int numberOfUnits, PropertyTypes firstUnitType)
		{
			var units = OMUnit.Where(x => x.TaskId == taskId).SelectAll().Execute();
			Assert.That(units.Count, Is.EqualTo(numberOfUnits));
			Assert.That(units.First().PropertyType_Code, Is.EqualTo(firstUnitType));
		}
	}
}
