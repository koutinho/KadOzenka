using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Units.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Tasks.LongProcess
{
	[TestFixture]
	public class TaskForCodLongProcessTest : BaseLongProcessTests
	{
		private TaskForCodLongProcess LongProcess => Provider.GetService<TaskForCodLongProcess>();
		private Mock<ITaskService> TaskService { get; set; }
		private Mock<IUnitRepository> UnitRepository { get; set; }


		[SetUp]
		public void SetUp()
		{
			TaskService = new Mock<ITaskService>();
			UnitRepository = new Mock<IUnitRepository>();

			TaskService.Setup(x => x.GetTemplateForTaskName(It.IsAny<long>())).Returns(RandomGenerator.GetRandomString("taskName_"));
			RosreestrRegisterService.Setup(x => x.GetPFsAttribute()).Returns(new RegisterAttribute());
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			base.AddServicesToContainer(container);

			container.AddTransient<TaskForCodLongProcess>();

			container.AddTransient(typeof(ITaskService), sp => TaskService.Object);
			container.AddTransient(typeof(IUnitRepository), sp => UnitRepository.Object);
		}



		[Test]
		public void If_After_Filtration_No_Units_Left_Report_Must_Have_Only_Headers()
		{
			MoqUnitRepository(RandomGenerator.CreateListWithRepeatedElements(new OMUnit(), 10));
			MockGbuObjectServiceGetAllAttributes(new List<GbuObjectAttribute>());

			StartProcess();

			ReportService.Verify(foo => foo.AddHeaders(It.IsAny<List<string>>()), Times.Once);
			ReportService.Verify(foo => foo.GetCurrentRow(), Times.Never);
			ReportService.Verify(foo => foo.SaveReport(), Times.Once);
		}

		[Test]
		public void If_After_Filtration_Some_Units_Left_Must_Create_Report()
		{
			var units = RandomGenerator.CreateListWithRepeatedElements(new OMUnit{ObjectId = RandomGenerator.GenerateRandomInteger()}, 10);
			var filteredUnits = new List<GbuObjectAttribute> { new GbuObjectAttribute { ObjectId = units[0].ObjectId.Value, NumValue = 1 } };
			MoqUnitRepository(units);
			MockGbuObjectServiceGetAllAttributes(filteredUnits);

			StartProcess();

			ReportService.Verify(foo => foo.AddHeaders(It.IsAny<List<string>>()), Times.Once);
			ReportService.Verify(foo => foo.GetCurrentRow(), Times.AtLeast(filteredUnits.Count));
			ReportService.Verify(foo => foo.SaveReport(), Times.Once);
		}


		#region Support Methods

		protected void MockGbuObjectServiceGetAllAttributes(List<GbuObjectAttribute> output)
		{
			GbuObjectService.Setup(x => x.GetAllAttributes(It.IsAny<List<long>>(), It.IsAny<List<long>>(),
					It.IsAny<List<long>>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool>()))
				.Returns(output);
		}

		private void MoqUnitRepository(List<OMUnit> output)
		{
			UnitRepository.Setup(x => x.GetEntitiesByCondition(It.IsAny<Expression<Func<OMUnit, bool>>>(),
				It.IsAny<Expression<Func<OMUnit, object>>>())).Returns(output);
		}

		private void StartProcess()
		{
			LongProcess.StartProcess(new OMProcessType(), new OMQueue
			{
				Status_Code = Status.Added,
				ObjectId = RandomGenerator.GenerateRandomInteger(),
				UserId = RandomGenerator.GenerateRandomInteger()
			}, new CancellationToken());
		}

		#endregion
	}
}
