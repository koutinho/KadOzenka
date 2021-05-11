using System;
using System.Linq;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using KadOzenka.Dal.Correction;
using KadOzenka.Dal.LongProcess.InputParameters;
using Core.Shared.Extensions;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces.Corrections;
using Consts = MarketPlaceBusiness.Common.Consts;

namespace KadOzenka.Dal.LongProcess
{
    public class CorrectionByRoomForMarketObjectsLongProcess : LongProcess
    {
        private IMarketObjectsForCorrectionByRoom MarketObjectsService { get; }
        public const string LongProcessName = nameof(CorrectionByRoomForMarketObjectsLongProcess);


        public CorrectionByRoomForMarketObjectsLongProcess()
        {
	        MarketObjectsService = new MarketObjectsForCorrectionsService();
        }


        public static void AddProcessToQueue(CorrectionByRoomRequest request)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, Consts.RegisterId, parameters: request.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);

            var date = PrepareDate(processQueue);

            var correctionByRoomService = new CorrectionByRoomService();

            var numberOfRooms = new long?[] { 1, 2, 3 };

            var coefficients = correctionByRoomService.GetCoefficients(date);
            var excludedBuildings = coefficients.Where(x => x.IsExcluded.GetValueOrDefault()).Select(x => x.BuildingCadastralNumber).ToList();
            var objectsGroupedBySegment =
	            MarketObjectsService.GetObjectsGroupedBySegment(
		            correctionByRoomService.CalculatedMarketSegments, numberOfRooms);

            WorkerCommon.SetProgress(processQueue, 30);

            objectsGroupedBySegment.ForEach(groupBySegment =>
            {
                var objectsGroupedByBuilding = groupBySegment
                    .Where(x => !excludedBuildings.Contains(x.BuildingCadastralNumber))
                    .GroupBy(x => x.BuildingCadastralNumber).ToList();

                objectsGroupedByBuilding.ForEach(groupByBuilding =>
                {
                    var objectsInBuilding = groupByBuilding.ToList();

                    if (MarketObjectsService.IsBuildingContainAllRoomsTypes(objectsInBuilding))
                    {
                        var oneRoomAveragePricePerMeter = MarketObjectsService.GetAveragePricePerMeter(objectsInBuilding, 1);
                        var twoRoomsAveragePricePerMeter = MarketObjectsService.GetAveragePricePerMeter(objectsInBuilding, 2);
                        var threeRoomsAveragePricePerMeter = MarketObjectsService.GetAveragePricePerMeter(objectsInBuilding, 3);

                        var oneRoomCoefficient = oneRoomAveragePricePerMeter == 0
                            ? 0
                            : Math.Round(twoRoomsAveragePricePerMeter / oneRoomAveragePricePerMeter,
                                CorrectionByRoomService.PrecisionForCoefficients);
                        var threeRoomsCoefficient = threeRoomsAveragePricePerMeter == 0
                            ? 0
                            : Math.Round(twoRoomsAveragePricePerMeter / threeRoomsAveragePricePerMeter,
                                CorrectionByRoomService.PrecisionForCoefficients);

                        correctionByRoomService.SaveCoefficients(coefficients, date, groupByBuilding.Key,
                            groupBySegment.Key.Segment, oneRoomCoefficient, threeRoomsCoefficient);
                    }
                });
            });

            WorkerCommon.SetProgress(processQueue, 60);

            correctionByRoomService.CalculatePriceAfterCorrectionByRooms(date);

            WorkerCommon.SetProgress(processQueue, 100);
        }

        private static DateTime PrepareDate(OMQueue processQueue)
        {
            DateTime date;
            var firstMonthDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            if (!string.IsNullOrWhiteSpace(processQueue.Parameters))
            {
                var request = processQueue.Parameters.DeserializeFromXml<CorrectionByRoomRequest>();
                date = request?.Date ?? firstMonthDate;
            }
            else
            {
                date = firstMonthDate;
            }

            return date;
        }
    }
}
