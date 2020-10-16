using System.Threading;
using KadOzenka.Dal.ManagementDecisionSupport;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess.MapBuilding
{
	public class UpdateHeatmapDataLongProcess : LongProcess
	{
		public const string LongProcessName = nameof(UpdateHeatmapDataLongProcess);

		private static readonly ILogger Log = Serilog.Log.ForContext<UpdateHeatmapDataLongProcess>();
		private MapBuildingService MapBuildingService { get; }
		

		public UpdateHeatmapDataLongProcess()
		{
			MapBuildingService = new MapBuildingService();
		}

		public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			Log.Information("Старт фонового процесса: {Description}.", processType.Description);
			MapBuildingService.UpdateHeatMapData();
			Log.Information("Завершение фонового процесса: {Description}.", processType.Description);
		}
	}
}
