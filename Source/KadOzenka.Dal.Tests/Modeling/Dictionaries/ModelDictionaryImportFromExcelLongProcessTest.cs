using System;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using Core.Shared.Extensions;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.CommonFunctions.Repositories;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Dto;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using ObjectModel.Common;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Dal.Tests.Modeling.Dictionaries
{
	[TestFixture]
	public class ModelDictionaryImportFromExcelLongProcessTest : BaseLongProcessTests
	{
		private ModelDictionaryImportFromExcelLongProcess LongProcess => Provider.GetService<ModelDictionaryImportFromExcelLongProcess>();
		private Mock<IDictionaryService> DictionaryService { get; set; }
		private Mock<IImportDataLogRepository> ImportDataLogRepository { get; set; }


		[SetUp]
		public void SetUp()
		{
			DictionaryService = new Mock<IDictionaryService>();
			ImportDataLogRepository = new Mock<IImportDataLogRepository>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			base.AddServicesToContainer(container);

			container.AddTransient<ModelDictionaryImportFromExcelLongProcess>();

			container.AddTransient(typeof(IDictionaryService), sp => DictionaryService.Object);
			container.AddTransient(typeof(IImportDataLogRepository), sp => ImportDataLogRepository.Object);
		}



		[Test]
		public void If_ImportDataLog_Not_Found_Do_Nothing()
		{
			var importDataLogId = RandomGenerator.GenerateRandomInteger();
			MoqImportDataLog(importDataLogId, null);

			StartProcess(importDataLogId, null);

			CheckCreateDictionaryWasCalled(Times.Never());
			CheckUpdateDictionaryWasCalled(Times.Never());
			NotificationSender.Verify(foo => foo.SendNotification(It.IsAny<OMQueue>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void Can_Create_New_Dictionary()
		{
			var importDataLogId = RandomGenerator.GenerateRandomInteger();
			MoqImportDataLog(importDataLogId, new OMImportDataLog());

			StartProcess(importDataLogId, new DictionaryImportFileFromExcelDto {IsNewDictionary = true});

			CheckCreateDictionaryWasCalled(Times.Once());
			CheckUpdateDictionaryWasCalled(Times.Never());
		}

		[Test]
		public void Can_Update_Dictionary()
		{
			var importDataLogId = RandomGenerator.GenerateRandomInteger();
			MoqImportDataLog(importDataLogId, new OMImportDataLog());

			StartProcess(importDataLogId, new DictionaryImportFileFromExcelDto { IsNewDictionary = false });

			CheckCreateDictionaryWasCalled(Times.Never());
			CheckUpdateDictionaryWasCalled(Times.Once());
		}



		#region Support Methods

		private void MoqImportDataLog(long importDataLogId, OMImportDataLog output)
		{
			ImportDataLogRepository
				.Setup(x => x.GetById(importDataLogId, It.IsAny<Expression<Func<OMImportDataLog, object>>>()))
				.Returns(output);
		}

		private void CheckCreateDictionaryWasCalled(Times times)
		{
			DictionaryService.Verify(x => x.CreateDictionaryFromExcel(It.IsAny<Stream>(),
				It.IsAny<DictionaryImportFileInfoDto>(),
				It.IsAny<string>(), It.IsAny<OMImportDataLog>()), times);
		}

		private void CheckUpdateDictionaryWasCalled(Times times)
		{
			DictionaryService.Verify(x => x.UpdateDictionaryFromExcel(It.IsAny<Stream>(),
				It.IsAny<DictionaryImportFileInfoDto>(),
				It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<OMImportDataLog>()), times);
		}

		private void StartProcess(long importId, DictionaryImportFileFromExcelDto inputParameters)
		{
			LongProcess.StartProcess(new OMProcessType(), new OMQueue
			{
				Status_Code = Status.Added,
				ObjectId = importId,
				UserId = RandomGenerator.GenerateRandomInteger(),
				Parameters = inputParameters?.SerializeToXml()
			}, new CancellationToken());
		}

		#endregion
	}
}
