using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests
{
	public class BaseLongProcessTests : BaseTests
	{
		protected Mock<INotificationSender> NotificationSender { get; set; }
		protected Mock<IFileStorageManagerWrapper> FileStorageManagerWrapper { get; set; }
		protected Mock<IRosreestrRegisterService> RosreestrRegisterService { get; set; }
		protected Mock<IGbuObjectService> GbuObjectService { get; set; }
		protected Mock<IGbuReportService> ReportService { get; set; }


		[SetUp]
		public void BaseLongProcessTestsSetUp()
		{
			NotificationSender = new Mock<INotificationSender>();
			FileStorageManagerWrapper = new Mock<IFileStorageManagerWrapper>();
			RosreestrRegisterService = new Mock<IRosreestrRegisterService>();
			GbuObjectService = new Mock<IGbuObjectService>();
			ReportService = new Mock<IGbuReportService>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			var workerCommonWrapper = new Mock<IWorkerCommonWrapper>();
			var longProcessProgressLogger = new Mock<ILongProcessProgressLogger>();
			container.AddTransient(typeof(IWorkerCommonWrapper), sp => workerCommonWrapper.Object);
			container.AddTransient(typeof(ILongProcessProgressLogger), sp => longProcessProgressLogger.Object);
			container.AddTransient(typeof(INotificationSender), sp => NotificationSender.Object);
			container.AddTransient(typeof(IRosreestrRegisterService), sp => RosreestrRegisterService.Object);
			container.AddTransient(typeof(IFileStorageManagerWrapper), sp => FileStorageManagerWrapper.Object);
			container.AddTransient(typeof(IGbuObjectService), sp => GbuObjectService.Object);
			container.AddTransient(typeof(IGbuReportService), sp => ReportService.Object);
		}
	}
}
