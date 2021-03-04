using Core.UI.Registers.Controllers;
using Core.UI.Registers.Services;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Documents;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Groups;
using KadOzenka.Dal.LongProcess.Common;
using KadOzenka.Dal.LongProcess.Reports;
using KadOzenka.Dal.ManagementDecisionSupport;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Repositories;
using KadOzenka.Dal.ObjectsCharacteristics;
using KadOzenka.Dal.ObjectsCharacteristics.Repositories;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.ScoreCommon;
using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Helpers;
using KadOzenka.Web.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Platform.Web.SignalR.Messages;

namespace KadOzenka.Web.Tests
{
	public class BaseTests
	{
		protected ServiceProvider Provider { get; set; }
		protected Mock<ITourService> TourService { get; set; }
		protected Mock<IModelingService> ModelingService { get; set; }
		protected Mock<IGbuObjectService> GbuObjectService { get; set; }
		protected Mock<ILongProcessService> LongProcessService { get; set; }
		protected Mock<IObjectsCharacteristicsService> ObjectsCharacteristicsService { get; set; }
		protected Mock<IObjectsCharacteristicsSourceService> ObjectsCharacteristicsSourceService { get; set; }
		protected Mock<IRegisterAttributeService> RegisterAttributeService { get; set; }


		protected delegate IActionResult ControllerMethod<T>(T input) where T : class, new();


		[OneTimeSetUp]
		public void BaseTestsSetUp()
		{
			TourService = new Mock<ITourService>();
			ModelingService = new Mock<IModelingService>();
			GbuObjectService = new Mock<IGbuObjectService>();
			LongProcessService = new Mock<ILongProcessService>();
			ObjectsCharacteristicsService = new Mock<IObjectsCharacteristicsService>();
			ObjectsCharacteristicsSourceService = new Mock<IObjectsCharacteristicsSourceService>();
			RegisterAttributeService = new Mock<IRegisterAttributeService>();

			ConfigureServices();
		}

		protected virtual void AddServicesToContainer(ServiceCollection container)
		{

		}

		protected void CheckMethodValidateModelState<TMethodInputParameterType>(BaseController controller,
			ControllerMethod<TMethodInputParameterType> method) where TMethodInputParameterType : class, new()
		{
			controller.ModelState.AddModelError(RandomGenerator.GetRandomString(), RandomGenerator.GetRandomString());

			var result = method.Invoke(new TMethodInputParameterType());

			var errors = GetPropertyFromJson(result, "Errors");

			Assert.IsNotNull(errors);
			Assert.That(controller.ModelState.IsValid, Is.False);
		}

		protected object GetPropertyFromJson(IActionResult result, string propertyName)
		{
			var jsonResult = result as JsonResult;

			return jsonResult?.Value?.GetType().GetProperty(propertyName)?.GetValue(jsonResult.Value, null);
		}


		#region Support Methods

		private void ConfigureServices()
		{
			var container = new ServiceCollection();

			container.AddTransient<CoreUiService>();
			container.AddTransient<RegistersService>();
			container.AddTransient<DashboardService>();

			container.AddTransient<GbuObjectService>();
			container.AddTransient<TaskService>();
			container.AddTransient<TourFactorService>();
			container.AddTransient<GbuLongProcessesService>();
			container.AddSingleton<GbuCurrentLongProcessesListenerService>();
			container.AddTransient<ScoreCommonService>();
			container.AddTransient<ExpressScoreService>();
			container.AddTransient<ExpressScoreReferenceService>();
			container.AddTransient<ViewRenderService>();
			container.AddTransient<ModelingService>();
			container.AddTransient<MapBuildingService>();
			container.AddTransient<DashboardWidgetService>();
			container.AddTransient<StatisticsReportsWidgetService>();
			container.AddTransient<StatisticsReportsWidgetExportService>();
			container.AddTransient<TourService>();
			container.AddTransient<SystemAttributeSettingsService>();
			container.AddTransient<TemplateService>();
			container.AddTransient<GroupService>();
			container.AddTransient<DocumentService>();
			container.AddTransient<ModelFactorsService>();
			container.AddSingleton<KoUnloadResultsListenerService>();
			container.AddSingleton<OutliersCheckingListenerService>();
			container.AddSingleton<DictionaryService>();
			container.AddSingleton<EsHubService>();
			container.AddSingleton<SignalRMessageService>();
			container.AddSingleton<StatisticalDataService>();
			container.AddSingleton<CustomReportsService>();
			container.AddTransient(typeof(IModelObjectsRepository), typeof(ModelObjectsRepository));
			container.AddTransient(typeof(ITourService), sp => TourService.Object);
			container.AddTransient(typeof(IModelingService), sp => ModelingService.Object);
			container.AddTransient(typeof(IGbuObjectService), sp => GbuObjectService.Object);
			container.AddTransient(typeof(IModelingRepository), typeof(ModelingRepository));
			container.AddTransient(typeof(IModelObjectsService), typeof(ModelObjectsService));
			container.AddTransient(typeof(ILongProcessService), sp => LongProcessService.Object);
			container.AddTransient(typeof(IRecycleBinService), typeof(RecycleBinService));
			container.AddTransient(typeof(IObjectsCharacteristicsService), sp => ObjectsCharacteristicsService.Object);
			container.AddTransient(typeof(IObjectsCharacteristicsSourceService),
				sp => ObjectsCharacteristicsSourceService.Object);
			container.AddTransient(typeof(IRegisterAttributeService), sp => RegisterAttributeService.Object);

			AddServicesToContainer(container);

			Provider = container.BuildServiceProvider();
		}

		#endregion
	}
}
