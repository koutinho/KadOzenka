using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Correction.Dto;
using ObjectModel.Directory;
using ObjectModel.Directory.MarketObjects;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionForFirstFloorService : CorrectionBaseService
    {
        private const int PricePrecision = 2;

        private const int RatioPrecision = 4;

        // TODO: убрать флаг и оставить только расчет с учетом комнат
        private const bool IncludeCorrectionByRooms = true;

        public static List<MarketSegment> CalculatedMarketSegments => new List<MarketSegment>() { MarketSegment.Office, MarketSegment.Trading, MarketSegment.MZHS };
        public CorrectionByRoomService CorrectionByRoomService { get; protected set; }


        public CorrectionForFirstFloorService()
        {
	        CorrectionByRoomService = new CorrectionByRoomService();
        }


        //TODO добавь логирование прогресса в LongProcess, когда закончишь задачу
        public void MakeCorrections(DateTime date, MarketSegment? segment = null)
        {
            if(segment != null && !CalculatedMarketSegments.Contains(segment.Value))
            {
                throw new Exception($"Данная корректировка определяется только для сегментов: {string.Join(", ", CalculatedMarketSegments.Select(x => x.GetEnumDescription()).ToList())}");
            }

            var dateMonth = DateToMonth(date);

            // Если это новый месяц, по которому статистика не собрана
            // Предназначено для ежемесячного вызова по расписанию
            if (dateMonth == DateToMonth(DateTime.Now)
                && !CheckStatsExistence(dateMonth))
                GatherData();

            var firstFloors = MarketObjectsService.GetFirstFloorsForCorrectionByFirstFloor(CalculatedMarketSegments, segment);
            var rates = GetRatesByDate(dateMonth);

            var correctionHistory =
                OMPriceForFirstFloorHistory
                    .Where(obj => obj.StatsDate == dateMonth)
                    .SelectAll()
                    .Execute();
            var objectIds = firstFloors.Select(o => o.Id);
            var marketPriceHistory =
                OMPriceHistory
                    .Where(o =>
                        o.ChangingDate >= dateMonth
                        && o.ParentCoreObject.DealType_Code == DealType.SaleSuggestion
                        && o.ParentCoreObject.FloorNumber == 1
                        && o.PriceValueFrom > 1
                        && o.ParentCoreObject.PropertyMarketSegment_Code != MarketSegment.NoSegment)
                    .And(o => objectIds.Contains(o.InitialId))
                    .SelectAll()
                    .Execute();

            var objectsWithHistoricalPrice = RewindHistory(firstFloors, marketPriceHistory);

            List<RoomRates> roomRatesList;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (IncludeCorrectionByRooms)
                roomRatesList = CorrectionByRoomService.GetAverageCoefficients(dateMonth)
                    .Select(x => new RoomRates
                    {
                        Segment = x.MarketSegment,
                        OneRoomRate = x.OneRoomCoefficient,
                        ThreeRoomRate = x.ThreeRoomsCoefficient
                    }).ToList();

            foreach (var rate in rates)
            {
                var segmentRate = Math.Round(rate.FirstToUpperRate, RatioPrecision);
                var roomRates = roomRatesList.FirstOrDefault(o => o.Segment == rate.Segment);

                var objectsInSegment =
                    objectsWithHistoricalPrice.Where(o => o.PropertyMarketSegment_Code == rate.Segment);
                foreach (var coreObject in objectsInSegment)
                {
                    decimal? roomRate = null;
                    // ReSharper disable once RedundantLogicalConditionalExpressionOperand
                    if (IncludeCorrectionByRooms && roomRates != null)
                    {
                        if (coreObject.RoomsCount == 1) roomRate = roomRates.OneRoomRate;
                        if (coreObject.RoomsCount == 3) roomRate = roomRates.ThreeRoomRate;
                    }

                    UpdateCorrection(correctionHistory, coreObject, dateMonth, segmentRate, roomRate);
                }
            }
        }


        public List<Rates> GetRatesBySegment(long marketSegmentCode)
        {
            if (!CalculatedMarketSegments.Contains((MarketSegment)marketSegmentCode))
            {
                throw new Exception($"Данная корректировка определяется только для сегментов: {string.Join(", ", CalculatedMarketSegments.Select(x => x.GetEnumDescription()).ToList())}");
            }

            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByStage);
            var query = OMCoefficientsForFirstFloorCorr
                .Where(w =>
                    w.MarketSegment_Code == (MarketSegment) marketSegmentCode
                    && !w.IsExcludedFromCalculation && (
                        (!settings.LowerLimitForCoefficient.HasValue || w.FirstToUpperFloorRate >= settings.LowerLimitForCoefficient.Value)
                        && (!settings.UpperLimitForCoefficient.HasValue ||w.FirstToUpperFloorRate <= settings.UpperLimitForCoefficient.Value)));

            return GetRates(query);
        }

        public List<CorrectionForFirstFloorDto> GetDetailsForSegmentAtDate(long marketSegmentCode, DateTime date)
        {
            if (!CalculatedMarketSegments.Contains((MarketSegment)marketSegmentCode))
            {
                throw new Exception($"Данная корректировка определяется только для сегментов: {string.Join(", ", CalculatedMarketSegments.Select(x => x.GetEnumDescription()).ToList())}");
            }

            var result =
                OMCoefficientsForFirstFloorCorr
                    .Where(c =>
                        c.StatsDate == date
                        && c.MarketSegment_Code == (MarketSegment) marketSegmentCode)
                    .SelectAll()
                    .Execute()
                    .Select(obj => new CorrectionForFirstFloorDto
                    {
                        Id = obj.Id,
                        BuildingCadastralNumber = obj.BuildingCadastralNumber,
                        FirstFloorCoefficient = obj.FirstToUpperFloorRate,
                        MarketSegmentCode = obj.MarketSegment_Code,
                        StatsDate = obj.StatsDate,
                        IsExcludedFromCalculation = obj.IsExcludedFromCalculation
                    })
                    .OrderBy(o => o.FirstFloorCoefficient)
                    .ThenByDescending(o => o.IsExcludedFromCalculation)
                    .ToList();
            return result;
        }

        public bool ChangeBuildingsStatusInCalculation(List<CorrectionForFirstFloorDto> coefficients)
        {
            if (coefficients.Count == 0)
                return false;

            var isDataUpdated = false;
            coefficients.ForEach(record =>
            {
                var recordFromDb = OMCoefficientsForFirstFloorCorr.Where(x => x.Id == record.Id).SelectAll()
                    .ExecuteFirstOrDefault();
                if (recordFromDb == null)
                    return;

                if (recordFromDb.IsExcludedFromCalculation != record.IsExcludedFromCalculation)
                    isDataUpdated = true;

                recordFromDb.IsExcludedFromCalculation = record.IsExcludedFromCalculation;
                recordFromDb.Save();
            });

            return isDataUpdated;
        }

        public bool IsCoefIncludedInCalculationLimit(decimal? coefficient)
        {
            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByStage);
            var result = (!settings.LowerLimitForCoefficient.HasValue || coefficient >= settings.LowerLimitForCoefficient.Value)
                         && (!settings.UpperLimitForCoefficient.HasValue || coefficient <= settings.UpperLimitForCoefficient.Value);

            return result;
        }

        #region Helper Methods

        private List<Rates> GetRatesByDate(DateTime date)
        {
            var settings = CorrectionSettingsService.GetCorrectionSettings(CorrectionTypes.CorrectionByStage);
            var query = OMCoefficientsForFirstFloorCorr
                .Where(w =>
                    w.StatsDate == date
                    && !w.IsExcludedFromCalculation
                    && CalculatedMarketSegments.Contains(w.MarketSegment_Code)
                    && ((!settings.LowerLimitForCoefficient.HasValue || w.FirstToUpperFloorRate >= settings.LowerLimitForCoefficient.Value)
                        && (!settings.UpperLimitForCoefficient.HasValue || w.FirstToUpperFloorRate <= settings.UpperLimitForCoefficient.Value)));
            return GetRates(query);
        }

        private List<Rates> GetRates(QSQuery<OMCoefficientsForFirstFloorCorr> query)
        {
            return query
                .GroupBy(g => new {g.StatsDate, g.MarketSegment_Code})
                .ExecuteSelect(s => new
                {
                    s.StatsDate,
                    s.MarketSegment_Code,
                    Count = s.Count(seg => seg.Id),
                    Min = s.Min(seg => seg.FirstToUpperFloorRate),
                    Max = s.Max(seg => seg.FirstToUpperFloorRate),
                    Rate = s.Avg(seg => seg.FirstToUpperFloorRate)
                })
                .Select(r => new Rates
                {
                    StatsDate = r.StatsDate,
                    Segment = r.MarketSegment_Code,
                    FirstToUpperRate = r.Rate,
                    MinFirstToUpperRate = r.Min,
                    MaxFirstToUpperRate = r.Max,
                    Count = r.Count
                })
                .OrderByDescending(o => o.StatsDate)
                .ToList();
        }

        private void GatherData()
        {
            var firstFloorsStats = MarketObjectsService.GetFloorStatsForCorrectionByFirstFloor(IncludeCorrectionByRooms, true);
            var upperFloorsStats = MarketObjectsService.GetFloorStatsForCorrectionByFirstFloor(IncludeCorrectionByRooms);
            var date = DateToMonth(DateTime.Now);
            var combinedStats =
                from f in firstFloorsStats
                join u in upperFloorsStats
                    on new {f.CadastralNumber, f.Segment}
                    equals new {u.CadastralNumber, u.Segment}
                select new
                {
                    f.CadastralNumber,
                    f.Segment,
                    firstUnitCost = f.UnitCost,
                    upperUnitCost = u.UnitCost,
                    firstToUpperRatio = f.UnitCost / u.UnitCost
                };
            var list = combinedStats.ToList();

            var corrList = new List<OMCoefficientsForFirstFloorCorr>();
            list.ForEach(obj =>
                corrList.Add(new OMCoefficientsForFirstFloorCorr
                {
                    BuildingCadastralNumber = obj.CadastralNumber,
                    FirstToUpperFloorRate = obj.firstToUpperRatio,
                    MarketSegment_Code = obj.Segment,
                    StatsDate = date,
                    IsExcludedFromCalculation = false
                }));

            // Подтягиваем данные по исключенным зданиям за старый период
            var previouslyExcluded =
                OMCoefficientsForFirstFloorCorr
                    .Where(c =>
                        c.StatsDate == DateToMonth(DateTime.Now.AddMonths(-1))
                        && c.IsExcludedFromCalculation)
                    .SelectAll()
                    .Execute();

            previouslyExcluded.ForEach(corr =>
            {
                var correspondingRecord =
                    corrList.FirstOrDefault(c =>
                        c.MarketSegment_Code == corr.MarketSegment_Code
                        && c.BuildingCadastralNumber == corr.BuildingCadastralNumber);
                if (correspondingRecord != null)
                    correspondingRecord.IsExcludedFromCalculation = true;
            });

            corrList.ForEach(corr => corr.Save());
        }

        private static bool CheckStatsExistence(DateTime date)
        {
            var checkStatsExistence =
                OMCoefficientsForFirstFloorCorr
                    .Where(o => o.StatsDate == DateToMonth(date))
                    .ExecuteExists();
            return checkStatsExistence;
        }

        private static void UpdateCorrection(List<OMPriceForFirstFloorHistory> history, OMCoreObject coreObject,
            DateTime dateMonth, decimal segmentRate, decimal? roomRate = null)
        {
            var resultingPrice = Math.Round(coreObject.Price.GetValueOrDefault() * segmentRate, PricePrecision);
            if (roomRate != null)
                resultingPrice *= roomRate.GetValueOrDefault();

            var record = history.FirstOrDefault(x => x.ObjectId == coreObject.Id && x.StatsDate == dateMonth);
            if (record == null)
            {
                new OMPriceForFirstFloorHistory
                {
                    ObjectId = coreObject.Id,
                    StatsDate = dateMonth,
                    PriceWithCorrectionForFirstFloor = resultingPrice
                }.Save();
            }
            else
            {
                record.PriceWithCorrectionForFirstFloor =
                    coreObject.PriceAfterCorrectionByRooms.GetValueOrDefault();
                record.Save();
            }

            // Не обновляем данные в market_core_object если это правка прошлого месяца
            if (dateMonth != DateToMonth(DateTime.Now)) return;

            coreObject.PriceAfterCorrectionForFirstFloor = resultingPrice;
            coreObject.Save();
        }

        private static List<OMCoreObject> RewindHistory(List<OMCoreObject> objects, List<OMPriceHistory> history)
        {
            var firstEntries =
                history
                    .GroupBy(o => o.InitialId)
                    .Select(s => s.OrderByDescending(d => d.ChangingDate).FirstOrDefault())
                    .ToList();


            firstEntries.ForEach(entry =>
            {
                var obj = objects.FirstOrDefault(x => x.Id == entry.InitialId);
                if (obj != null)
                    obj.Price = entry.PriceValueFrom;
            });
            return objects;
        }

        private static DateTime DateToMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        #endregion

        #region Support Classes

        public class Rates
        {
            public long Count;
            public decimal FirstToUpperRate;
            public decimal MaxFirstToUpperRate;
            public decimal MinFirstToUpperRate;
            public MarketSegment Segment;
            public DateTime StatsDate;
        }

        #endregion

        #region RoomCorrection

        private List<RoomRates> GetRoomRates(DateTime date)
        {
            var result =
                OMCoefficientsForCorrectionByRooms
                    .Where(x =>
                        (x.IsExcluded == false || x.IsExcluded == null)
                        && x.ChangingDate == date)
                    .OrderByDescending(x => x.ChangingDate)
                    .SelectAll().Execute()
                    .GroupBy(x => new {x.MarketSegment_Code}).Select(
                        group => new RoomRates
                        {
                            Segment = group.Key.MarketSegment_Code,
                            OneRoomRate = Math.Round(
                                group.ToList().DefaultIfEmpty().Average(x => x.OneRoomCoefficient),
                                RatioPrecision),
                            ThreeRoomRate = Math.Round(
                                group.ToList().DefaultIfEmpty().Average(x => x.ThreeRoomsCoefficient),
                                RatioPrecision)
                        }).ToList();
            return result;
        }

        private class RoomRates
        {
            public decimal? OneRoomRate;
            public MarketSegment Segment;
            public decimal? ThreeRoomRate;
        }

        #endregion
    }
}