using System;
using System.Collections.Generic;
using System.Linq;
using ObjectModel.Directory;
using ObjectModel.Market;
using Core.Register.QuerySubsystem;
using DealType = ObjectModel.Directory.DealType;
using Core.Shared.Extensions;
using KadOzenka.Dal.Correction.Requests;
using KadOzenka.Dal.Correction.Dto.CorrectionSettings;

namespace KadOzenka.Dal.Correction
{
    public abstract class CorrectionByBargain<T> where T : CorrectionByBargainRequest
    {
        protected CorrectionSettings CorrectionSettings { get; set; }

        protected CorrectionByBargain(CorrectionSettings correctionSettings)
        {
            CorrectionSettings = correctionSettings;
        }

        protected virtual void ValidateRequest(T request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.MarketSegments.Count == 0)
            {
                throw new ArgumentException($"Выберите хотя бы один сегмент");
            }
            if (request.DateFrom.HasValue && request.DateTo.HasValue && request.DateTo < request.DateFrom)
            {
                throw new ArgumentException($"Некорректный временной интервал: с '{request.DateFrom.Value.ToShortDateString()}' по '{request.DateTo.Value.ToShortDateString()}'");
            }
        }

        protected virtual QSQuery<OMCoreObject> PrepareMarketObjectsQuery(T request)
        {
            var query = OMCoreObject
                .Where(x => (x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal) &&
                            (x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed));

            if (request.MarketSegments.Count < System.Enum.GetNames(typeof(MarketSegment)).Length)
            {
                query.And(x =>
                    request.MarketSegments.Contains(x.PropertyMarketSegment_Code));
            }

            return query;
        }

        protected virtual List<OMCoreObject> GetMarketObjects(QSQuery<OMCoreObject> marketObjectsQuery)
        {
            var objects = marketObjectsQuery
                .Select(x => x.Id)
                .Select(x => x.ProcessType_Code)
                .Select(x => x.DealType_Code)
                .Select(x => x.CadastralNumber)
                .Select(x => x.Address)
                .Select(x => x.BuildingCadastralNumber)
                .Select(x => x.PropertyMarketSegment_Code)
                .Select(x => x.District_Code)
                .Select(x => x.Neighborhood_Code)
                .Select(x => x.Zone)
                .Select(x => x.CadastralQuartal)
                .Select(x => x.Price)
                .Select(x => x.Area)
                .Select(x => x.PriceAfterCorrectionByBargain)
                .Select(x => x.LastDateUpdate)
                .Select(x => x.ParserTime)
                .SelectAll()
                .Execute();

            return objects;
        }

        protected List<KeyValuePair<OMCoreObject, decimal?>> GetSuggestionObjectsWithPriceCoefficients(CoefficientCoverageAreaType сoverageAreaType,
            List<OMCoreObject> objects)
        {
            var suggestionObjectsWithPriceCoefficients = new List<KeyValuePair<OMCoreObject, decimal?>>();
            if (сoverageAreaType == CoefficientCoverageAreaType.City)
            {
                FillResultCollection(objects, suggestionObjectsWithPriceCoefficients);
            }
            else
            {
                var groupByPropName =
                    сoverageAreaType.GetAttributeValue<CoveragePropertyNameAttribute>(
                        nameof(CoveragePropertyNameAttribute.PropertyName));
                var propertyInfo = typeof(OMCoreObject).GetProperty(groupByPropName);

                var objectsGroupsByCoverageAreaType = objects
                    .GroupBy(x => propertyInfo.GetValue(x, null) == null ? -1 : propertyInfo.GetValue(x, null))
                    .ToDictionary(g => g.Key, g => g.ToList());
                foreach (var objectsGroupByCoverageAreaType in objectsGroupsByCoverageAreaType)
                {
                    FillResultCollection(objectsGroupByCoverageAreaType.Value, suggestionObjectsWithPriceCoefficients);
                }
            }

            return suggestionObjectsWithPriceCoefficients;
        }

        private void FillResultCollection(List<OMCoreObject> objects, List<KeyValuePair<OMCoreObject, decimal?>> suggestionObjectsWithPriceCoefficients)
        {
            var coefficientsBySegments = CalculateCoefficientsBySegments(objects);
            foreach (var omCoreObject in objects.Where(x => x.DealType_Code == DealType.SaleSuggestion))
            {
                suggestionObjectsWithPriceCoefficients.Add(new KeyValuePair<OMCoreObject, decimal?>(omCoreObject,
                    coefficientsBySegments[omCoreObject.PropertyMarketSegment_Code]));
            }
        }

        private Dictionary<MarketSegment, decimal?> CalculateCoefficientsBySegments(List<OMCoreObject> objects)
        {
            var coefficientsBySegments = new Dictionary<MarketSegment, decimal?>();

            var objectsGroupsBySegment = objects
                .GroupBy(x => x.PropertyMarketSegment_Code)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var objectsGroupBySegment in objectsGroupsBySegment)
            {
                var objectsBySegment = objectsGroupBySegment.Value;

                var objectsGroupsByBuildingQuarter = objectsBySegment
                    .Where(x => !string.IsNullOrEmpty(x.BuildingCadastralNumber))
                    .GroupBy(x => x.BuildingCadastralNumber)
                    .ToDictionary(g => g.Key, g => g.ToList());

                var coefficientsByBuildingQuarterList = new List<decimal>();
                foreach (var objectsGroupByBuildingQuarter in objectsGroupsByBuildingQuarter)
                {
                    var objectsByBuildingQuarter = objectsGroupByBuildingQuarter.Value;
                    var suggestionObjectsByBuildingQuarter = objectsByBuildingQuarter
                        .Where(x => x.DealType_Code == DealType.SaleSuggestion).ToList();
                    var dealObjectsByBuildingQuarter = objectsByBuildingQuarter
                        .Where(x => x.DealType_Code == DealType.SaleDeal).ToList();
                    if (suggestionObjectsByBuildingQuarter.Count == 0 ||
                        dealObjectsByBuildingQuarter.Count == 0)
                    {
                        continue;
                    }

                    var suggestionObjectsPrice =
                        suggestionObjectsByBuildingQuarter.Sum(x => x.Price);
                    var suggestionObjectsArea =
                        suggestionObjectsByBuildingQuarter.Sum(x => x.Area);
                    var suggestionObjectsPricePerArea =
                        suggestionObjectsPrice / suggestionObjectsArea;

                    var dealObjectsPrice =
                        dealObjectsByBuildingQuarter.Sum(x => x.Price);
                    var dealObjectsArea =
                        dealObjectsByBuildingQuarter.Sum(x => x.Area);
                    var dealObjectsPricePerArea =
                        dealObjectsPrice / dealObjectsArea;

                    var coefficientByBuildingQuarter = dealObjectsPricePerArea /
                                              suggestionObjectsPricePerArea;
                    if (IsCoefficientIncludedInCalculation(coefficientByBuildingQuarter))
                    {
                        coefficientsByBuildingQuarterList.Add(coefficientByBuildingQuarter.Value);
                    }
                }

                var coefficientBySegment = coefficientsByBuildingQuarterList.Count > 0
                    ? Math.Round(
                        coefficientsByBuildingQuarterList.Sum(x => x) / coefficientsByBuildingQuarterList.Count, Consts.PrecisionForCoefficients)
                    : (decimal?) null;
                coefficientsBySegments.Add(objectsGroupBySegment.Key, coefficientBySegment);
            }

            return coefficientsBySegments;
        }

        private bool IsCoefficientIncludedInCalculation(decimal? coefficientByBuildingQuarter)
        {
            return coefficientByBuildingQuarter.HasValue && (!CorrectionSettings.LowerLimitForCoefficient.HasValue || coefficientByBuildingQuarter >= CorrectionSettings.LowerLimitForCoefficient.Value) 
                                                         && (!CorrectionSettings.UpperLimitForCoefficient.HasValue || coefficientByBuildingQuarter <= CorrectionSettings.UpperLimitForCoefficient.Value);
        }

        protected decimal? GetPriceAfterCorrectionByBargain(decimal? price, decimal? bargainCoefficient)
        {
            if (price.HasValue && bargainCoefficient.HasValue)
            {
                return Math.Round(price.Value * bargainCoefficient.Value, Consts.PrecisionForPrice);
            }

            return (decimal?) null;
        }
    }
}
