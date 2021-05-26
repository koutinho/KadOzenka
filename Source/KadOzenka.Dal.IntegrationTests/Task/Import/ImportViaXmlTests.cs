using System;
using System.IO;
using System.Linq;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.DataImport;
using NUnit.Framework;
using ObjectModel.Common;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Task.Import
{
	public class ImportViaXmlTests : BaseTaskTests
	{
		[Test]
		public void Can_Import_Task_Document_Via_Xml()
		{
			var fileName = "one_building_success.xml";

			var document = new OMInstance
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				CreateDate = DateTime.Now,
				ApproveDate = RandomGenerator.GenerateRandomDate()
			};
			document.Save();

			var task = new OMTask
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				CreationDate = DateTime.Now,
				TourId = RandomGenerator.GenerateRandomInteger(),
				NoteType_Code = KoNoteType.Day,
				Status_Code = KoTaskStatus.InWork,
				DocumentId = document.Id,
				EstimationDate = RandomGenerator.GenerateRandomDate()
			};
			task.Save();

			using (var fs = File.OpenRead($"{PathToFileFolder}{fileName}"))
			{
				DataImporterGknLongProcess.AddImportToQueue(fileName, fs, task.Id);
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
