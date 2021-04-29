using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.OutputEntities;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class ModelingServiceOutSide
	{
        public static readonly long RegisterId = OMCoreObject.GetRegisterId();


        //public энамы и классы приводят к путанице в проектах, к которым подключена библиотека. нужно делать internal?
        //MarketSegment проблема в "разных" enum, нужны врапперы. если объектов много, то такие врапперы съедят память

        public List<MarketObjectPureOutSide> GetMarketObjects(bool isOks, long segment)
        {
	        //TODO ждем выполнения CIPJSKO-307
            //var territoryCondition = ModelingService.GetConditionForTerritoryType(groupToMarketSegmentRelation.TerritoryType_Code);

            var baseQuery = OMCoreObject.Where(x =>
                x.PropertyMarketSegment_Code == (MarketSegment)segment &&
                x.CadastralNumber != null &&
                x.ProcessType_Code != ProcessStep.Excluded);

            var type = isOks ? QSConditionType.NotEqual : QSConditionType.Equal;
            baseQuery.And(new QSConditionSimple(OMCoreObject.GetColumn(x => x.PropertyTypesCIPJS_Code),
                type, (int)PropertyTypesCIPJS.LandArea));

            baseQuery.Select(x => new
            {
                x.CadastralNumber,
                x.PricePerMeter
            });

            ////TODO для тестирования
            //baseQuery.SetPackageIndex(0).SetPackageSize(100);

            return baseQuery
                //TODO для тестирования расчета МС и Процента
                //return OMCoreObject.Where(x => x.CadastralNumber == "77:06:0004004:9714")
                //TODO ждем выполнения CIPJSKO-307
                //.And(territoryCondition)
                .Execute()
                .GroupBy(x => new
                {
                    x.CadastralNumber,
                    x.PricePerMeter
                })
                .Select(x => new MarketObjectPureOutSide
                {
                    Id = x.Max(y => y.Id),
                    CadastralNumber = x.Key.CadastralNumber,
                    PricePerMeter = x.Key.PricePerMeter.GetValueOrDefault()
                }).ToList();
        }

        //private Expression<Func<OMCoreObject, bool>> GetConditionForTerritoryType(TerritoryType territoryType)
        //{
        //    switch (territoryType)
        //    {
        //        case TerritoryType.Main:
        //            Expression<Func<OMCoreObject, bool>> mainTerritoryCondition = x => x.Address == "Main";
        //            return mainTerritoryCondition;
        //        case TerritoryType.Additional:
        //            Expression<Func<OMCoreObject, bool>> additionalTerritoryCondition = x => x.Address == "Additional";
        //            return additionalTerritoryCondition;
        //        case TerritoryType.MainAndAdditional:
        //            Expression<Func<OMCoreObject, bool>> bothTerritoryCondition = x => x.Address == "MainAndAdditional";
        //            return bothTerritoryCondition;
        //        default:
        //            Expression<Func<OMCoreObject, bool>> unknownTerritoryCondition = x => x.Address == "default";
        //            return unknownTerritoryCondition;
        //    }
        //}
    }
}
