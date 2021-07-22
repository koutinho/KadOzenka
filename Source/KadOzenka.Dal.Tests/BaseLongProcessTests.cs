using CommonSdks;
using CommonSdks.Excel;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.LongProcess.Common;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Serilog;

namespace KadOzenka.Dal.Tests
{
	public class BaseLongProcessTests : BaseTests
	{
		protected Mock<INotificationSender> NotificationSender { get; set; }
		protected Mock<ILongProcessProgressLogger> LongProcessProgressLogger { get; set; }
		protected Mock<IWorkerCommonWrapper> WorkerCommonWrapper { get; set; }
		protected Mock<IFileStorageManagerWrapper> FileStorageManagerWrapper { get; set; }
		protected Mock<IRosreestrRegisterService> RosreestrRegisterService { get; set; }
		protected Mock<IGbuObjectService> GbuObjectService { get; set; }
		protected Mock<IGbuReportService> ReportService { get; set; }
		protected Mock<ILongProcessService> LongProcessService { get; set; }
		protected Mock<ILogger> SeqLogger { get; set; }


		[SetUp]
		public void BaseLongProcessTestsSetUp()
		{
			NotificationSender = new Mock<INotificationSender>();
			LongProcessProgressLogger = new Mock<ILongProcessProgressLogger>();
			WorkerCommonWrapper = new Mock<IWorkerCommonWrapper>();
			FileStorageManagerWrapper = new Mock<IFileStorageManagerWrapper>();
			RosreestrRegisterService = new Mock<IRosreestrRegisterService>();
			GbuObjectService = new Mock<IGbuObjectService>();
			ReportService = new Mock<IGbuReportService>();
			LongProcessService = new Mock<ILongProcessService>();
			SeqLogger = new Mock<ILogger>();
			SeqLogger.Setup(x => x.ForContext(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()))
				.Returns(SeqLogger.Object);
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient(typeof(IWorkerCommonWrapper), sp => WorkerCommonWrapper.Object);
			container.AddTransient(typeof(ILongProcessProgressLogger), sp => LongProcessProgressLogger.Object);
			container.AddTransient(typeof(INotificationSender), sp => NotificationSender.Object);
			container.AddTransient(typeof(IRosreestrRegisterService), sp => RosreestrRegisterService.Object);
			container.AddTransient(typeof(IFileStorageManagerWrapper), sp => FileStorageManagerWrapper.Object);
			container.AddTransient(typeof(IGbuObjectService), sp => GbuObjectService.Object);
			container.AddTransient(typeof(IGbuReportService), sp => ReportService.Object);
			container.AddTransient(typeof(ILongProcessService), sp => LongProcessService.Object);
			container.AddTransient(typeof(ILogger), sp => SeqLogger.Object);
		}
	}
}
