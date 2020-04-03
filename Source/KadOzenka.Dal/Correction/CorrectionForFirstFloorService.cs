using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using DevExpress.DataProcessing;
using KadOzenka.Dal.Correction.Dto;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionForFirstFloorService
    {

        public void UpdateMarketObjectsPrice(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void GatherData()
        {
            var firstFloorsStats = GetFloorStats(true);
            var upperFloorsStats = GetFloorStats();
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var combinedStats =
                from f in firstFloorsStats
                join u in upperFloorsStats
                    on new { f.CadastralNumber, f.Segment }
                    equals new { u.CadastralNumber, u.Segment }
                select new
                {
                    f.CadastralNumber,
                    f.Segment,
                    firstUnitCost = f.UnitCost,
                    upperUnitCost = u.UnitCost,
                    firstToUpperRatio = f.UnitCost / u.UnitCost
                };
            var list = combinedStats.ToList();

            //todo: сохранение в бд
            List<OMCoefficientsForFirstFloorCorr> corrList = new List<OMCoefficientsForFirstFloorCorr>();
            list.ForEach(obj=>
                corrList.Add(new OMCoefficientsForFirstFloorCorr()
                {
                    BuildingCadastralNumber = obj.CadastralNumber,
                    FirstToUpperFloorRate = obj.firstToUpperRatio,
                    MarketSegment_Code = obj.Segment,
                    StatsDate = date
                }));
            corrList.ForEach(corr=>corr.Save());
        }

        public List<Rates> GetRates(long marketSegmentCode)
        {
            var stats = OMCoefficientsForFirstFloorCorr
                .Where(w =>
                    w.MarketSegment_Code == (MarketSegment) marketSegmentCode
                    && !w.IsExcludedFromCalculation)
                .GroupBy(g => g.StatsDate)
                .ExecuteSelect(s => new
                {
                    s.StatsDate,
                    Count = s.Count(seg=>seg.Id),
                    Min = s.Min(seg => seg.FirstToUpperFloorRate),
                    Max = s.Max(seg => seg.FirstToUpperFloorRate),
                    Rate = s.Avg(seg => seg.FirstToUpperFloorRate)
                })
                .Select(r => new Rates
                {
                    StatsDate = r.StatsDate,
                    FirstToUpperRate = r.Rate,
                    MinFirstToUpperRate = r.Min,
                    MaxFirstToUpperRate = r.Max,
                    Count = r.Count
                })
                .ToList();

            return stats;
        }

        private List<FloorStats> GetFloorStats(bool firstFloor = false)
        {
            var query = OMCoreObject
                .Where(o =>
                    o.DealType_Code == DealType.SaleSuggestion
                    && o.PropertyMarketSegment_Code != MarketSegment.NoSegment
                    && o.PropertyMarketSegment_Code != MarketSegment.None
                    && o.Price > 1 // Отсекаем пустые цены и цены в 1 (частое явление)
                    && o.CadastralNumber != null
                    && o.CadastralNumber != "");
            var result =
                (firstFloor ? query.And(o => o.FloorNumber == 1) : query.And(o => o.FloorNumber > 1))
                .GroupBy(o => new
                {
                    o.CadastralNumber,
                    o.PropertyMarketSegment_Code
                })
                .ExecuteSelect(obj => new
                {
                    obj.CadastralNumber,
                    Segment = obj.PropertyMarketSegment_Code,
                    // todo: Использовать PriceAfterCorrectionByRooms, после стабилизации сервиса рассчета коэффициентов для комнат
                    // UnitCost = obj.Sum(ff => ff.PriceAfterCorrectionByRooms ?? ff.Price) / obj.Sum(ff => ff.Area)
                    UnitCost = obj.Sum(ff => ff.Price) / obj.Sum(ff => ff.Area)
                })
                // ExecuteSelect не позволяет сразу привести к нужному типу
                .Select(obj => new FloorStats
                {
                    CadastralNumber = obj.CadastralNumber,
                    Segment = obj.Segment,
                    UnitCost = obj.UnitCost
                })
                .ToList();
            return result;
        }

        public class Rates
        {
            public DateTime StatsDate;
            public decimal FirstToUpperRate;
            public decimal MinFirstToUpperRate;
            public decimal MaxFirstToUpperRate;
            public long Count;
        }

        private class FloorStats
        {
            public string CadastralNumber;
            public MarketSegment Segment;
            public decimal UnitCost;
        }

        public List<CorrectionForFirstFloorDto> GetDetailsForSegmentAtDate(long marketSegmentCode, DateTime date)
        {
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
                    .OrderBy(o=>o.FirstFloorCoefficient)
                    .ThenByDescending(o=>o.IsExcludedFromCalculation)
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
                var recordFromDb = OMCoefficientsForFirstFloorCorr.Where(x => x.Id == record.Id).SelectAll().ExecuteFirstOrDefault();
                if (recordFromDb == null)
                    return;

                if (recordFromDb.IsExcludedFromCalculation != record.IsExcludedFromCalculation)
                    isDataUpdated = true;

                recordFromDb.IsExcludedFromCalculation = record.IsExcludedFromCalculation;
                recordFromDb.Save();
            });

            return isDataUpdated;
        }
    }
}