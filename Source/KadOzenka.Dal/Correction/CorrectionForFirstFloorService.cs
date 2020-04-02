using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
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

            //todo: сохранение в бд
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
                    UnitCost = obj.Sum(ff => ff.PriceAfterCorrectionByRooms ?? ff.Price) / obj.Sum(ff => ff.Area)
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

        private class FloorStats
        {
            public string CadastralNumber;
            public MarketSegment Segment;
            public decimal UnitCost;
        }
    }
}