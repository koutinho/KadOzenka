using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using MarketPlaceBusiness.Dto.Modeling;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
    //TODO возможно, объединить с основным
	public class MarketObjectsForModelingService : IMarketObjectsForModelingService
	{
		//TODO KOMO-33 убрать long segment, сделать через enum
        public List<MarketObjectPure> GetObjectsForFormation(bool isOks, long segment)
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
                .Select(x => new MarketObjectPure
                {
                    Id = x.Max(y => y.Id),
                    CadastralNumber = x.Key.CadastralNumber,
                    PricePerMeter = x.Key.PricePerMeter.GetValueOrDefault()
                }).ToList();
        }

        public CorrelationDto GetObjectsForCorrelation(List<long> objectIds, List<OMAttribute> attributes)
        {
            var columnNameFroPrice = "PriceForService";
	        var query = new QSQuery
	        {
		        MainRegisterID = OMCoreObject.GetRegisterId(),
		        Condition = new QSConditionSimple
		        {
			        ConditionType = QSConditionType.In,
			        LeftOperand = OMCoreObject.GetColumn(x => x.Id),
			        RightOperand = new QSColumnConstant(objectIds)
		        }
	        };
	        query.AddColumn(OMCoreObject.GetColumn(x => x.Price, columnNameFroPrice));
	        attributes.ForEach(attribute =>
	        {
		        query.AddColumn(attribute.Id, attribute.Id.ToString());
	        });

	        var request = new CorrelationDto();
	        request.AttributeNames.AddRange(attributes.Select(x => x.Name));
	        request.AttributeNames.Add("Цена");
	        var table = query.ExecuteQuery();
	        for (var i = 0; i < table.Rows.Count; i++)
	        {
		        var row = table.Rows[i];
		        var coefficients = new List<decimal?>();
		        attributes.ForEach(attribute =>
		        {
			        var val = row[attribute.Id.ToString()].ParseToDecimalNullable();
			        coefficients.Add(val);
		        });

		        if (coefficients.All(x => x != null))
		        {
			        var priceForService = row[columnNameFroPrice].ParseToDecimalNullable();
                    coefficients.Add(priceForService.GetValueOrDefault());
			        request.Coefficients.Add(coefficients);
		        }
	        }

	        return request;
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
