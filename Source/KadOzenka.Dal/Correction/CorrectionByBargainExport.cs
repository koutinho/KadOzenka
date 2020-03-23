using System;
using System.Collections.Generic;
using ObjectModel.Market;
using System.Linq;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.Correction.Requests;
using KadOzenka.Dal.Correction.Dto;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByBargainExport : CorrectionByBargain<CorrectionByBargainExportRequest>
    {
        public List<CorrectionByBargainDto> GetBargainCorrectionData(CorrectionByBargainExportRequest request)
        {
            ValidateRequest(request);

            var query = PrepareMarketObjectsQuery(request);
            var objects = GetMarketObjects(query);
            var suggestionObjectsWithPriceCoefficients = GetSuggestionObjectsWithPriceCoefficients(request.CoverageAreaType, objects);

            return suggestionObjectsWithPriceCoefficients.Select(x => new CorrectionByBargainDto
            {
                CadastralNumber = x.Key.CadastralNumber,
                Address = x.Key.Address,
                Date = x.Key.LastDateUpdate.HasValue
                    ? x.Key.LastDateUpdate
                    : x.Key.ParserTime,
                MarketSegment = x.Key.PropertyMarketSegment_Code,
                Price = x.Key.Price,
                BargainCoefficient = x.Value,
                PriceAfterCorrectionByBargain = GetPriceAfterCorrectionByBargain(x.Key.Price, x.Value)
            }).ToList();
        }

        #region Override Methods

        protected override void ValidateRequest(CorrectionByBargainExportRequest request)
        {
            base.ValidateRequest(request);

            if (request.Zone.HasValue && (request.Zone.Value < 1 || request.Zone > 5))
            {
                throw new ArgumentException($"Передано некорректное значение для Зоны: {request.Zone.Value}");
            }
            if (!string.IsNullOrEmpty(request.CadastralQuarter))
            {
                var omQuarter = OMQuartalDictionary.Where(x => x.CadastralQuartal == request.CadastralQuarter)
                    .Select(x => x.CadastralQuartal)
                    .ExecuteFirstOrDefault();
                if (omQuarter == null)
                {
                    throw new ArgumentException($"Переданv неизвестный кадастровый квартал: {request.CadastralQuarter}");
                }
            }
        }

        protected override List<OMCoreObject> GetMarketObjects(QSQuery<OMCoreObject> marketObjectsQuery)
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

        protected override QSQuery<OMCoreObject> PrepareMarketObjectsQuery(CorrectionByBargainExportRequest request)
        {
            var query = base.PrepareMarketObjectsQuery(request);

            if (request.Distinct.HasValue)
            {
                query.And(x =>
                    x.District_Code == request.Distinct);
            }

            if (request.Region.HasValue)
            {
                query.And(x =>
                    x.Neighborhood_Code == request.Region);
            }

            if (request.Zone.HasValue)
            {
                query.And(x =>
                    x.Zone == request.Zone);
            }

            if (!string.IsNullOrEmpty(request.CadastralQuarter))
            {
                query.And(x =>
                    x.CadastralQuartal == request.CadastralQuarter);
            }

            if (request.DateFrom.HasValue)
            {
                query.And(x =>
                    (x.LastDateUpdate != null && x.LastDateUpdate >= request.DateFrom) ||
                    (x.LastDateUpdate == null && x.ParserTime >= request.DateFrom));
            }

            if (request.DateTo.HasValue)
            {
                query.And(x =>
                    (x.LastDateUpdate != null && x.LastDateUpdate <= request.DateTo) ||
                    (x.LastDateUpdate == null && x.ParserTime <= request.DateTo));
            }

            return query;
        }

        #endregion Override Methods
    }
}
