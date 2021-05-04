using System;
using System.Linq;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Threading;
using KadOzenka.Dal.Correction;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.LongProcess
{
    public class CorrectionByDateForMarketObjectsLongProcess : LongProcess
    {
        public const string LongProcessName = nameof(CorrectionByDateForMarketObjectsLongProcess);

        public static void AddProcessToQueue()
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, registerId: OMCoreObject.GetRegisterId());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            WorkerCommon.SetProgress(processQueue, 0);

            var service = new CorrectionByDateService();

            var startDate = new DateTime(2017, 01, 01);
            var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            var coefficients = service.GetCoefficients();
            var excludedBuildings = coefficients.Where(x => x.IsExcluded.GetValueOrDefault()).Select(x => x.BuildingCadastralNumber).ToList();

            var objectsGroupedBySegment = OMCoreObject.Where(x =>
                    (x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal) &&
                    x.BuildingCadastralNumber != null && x.BuildingCadastralNumber != "" &&
                    x.PropertyMarketSegment != null &&
                    ((x.ParserTime != null && x.ParserTime >= startDate && x.ParserTime <= endDate) || 
                    (x.LastDateUpdate != null && x.LastDateUpdate >= startDate && x.LastDateUpdate <= endDate)))
                .SelectAll(false)
                .Execute()
                .GroupBy(x => x.PropertyMarketSegment_Code)
                .ToList();

            WorkerCommon.SetProgress(processQueue, 30);

            objectsGroupedBySegment.ForEach(groupBySegment =>
            {
                var objectsGroupedByBuilding = groupBySegment
                    .Where(x => !excludedBuildings.Contains(x.BuildingCadastralNumber))
                    .GroupBy(x => x.BuildingCadastralNumber).ToList();

                objectsGroupedByBuilding.ForEach(groupByBuilding =>
                {
                    var objectsInBuilding = groupByBuilding.ToList();

                    for (var i = startDate; i <= endDate; i = i.AddMonths(1))
                    {
                        var currentPeriod = i;
                        var previousPeriod = i.AddYears(-1);

                        var objectFromCurrentPeriod = objectsInBuilding.Where(x => x.LastDateUpdate == null
                                ? x.ParserTime?.Year == currentPeriod.Year && x.ParserTime?.Month == currentPeriod.Month
                                : x.LastDateUpdate?.Year == currentPeriod.Year && x.LastDateUpdate?.Month == currentPeriod.Month)
                            .ToList();

                        var objectFromPreviousPeriod = objectsInBuilding.Where(x => x.LastDateUpdate == null
                            ? x.ParserTime?.Year == previousPeriod.Year && x.ParserTime?.Month == previousPeriod.Month
                            : x.LastDateUpdate?.Year == previousPeriod.Year && x.LastDateUpdate?.Month == previousPeriod.Month).ToList();

                        var isBuildingContainSalesInTwoPeriods = objectFromCurrentPeriod.Count > 0 && objectFromPreviousPeriod.Count > 0;

                        if (isBuildingContainSalesInTwoPeriods)
                        {
                            var averagePriceForObjectsFromCurrentPeriod = objectFromCurrentPeriod.Average(x => x.PricePerMeter.GetValueOrDefault());
                            var averagePriceForObjectsFromPreviousPeriod = objectFromPreviousPeriod.Average(x => x.PricePerMeter.GetValueOrDefault());

                            var coefficient = averagePriceForObjectsFromPreviousPeriod == 0
                                ? 0
                                : Math.Round(averagePriceForObjectsFromCurrentPeriod / averagePriceForObjectsFromPreviousPeriod,
                                    Correction.Consts.PrecisionForCoefficients);

                            service.SaveCoefficients(coefficients, currentPeriod, groupByBuilding.Key, groupBySegment.Key, coefficient);
                        }
                    }
                });
            });

            WorkerCommon.SetProgress(processQueue, 60);

            service.CalculatePriceAfterCorrectionByDate();

            WorkerCommon.SetProgress(processQueue, 100);
        }
    }
}
