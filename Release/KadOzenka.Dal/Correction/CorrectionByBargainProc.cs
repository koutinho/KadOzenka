using KadOzenka.Dal.Correction.Dto.CorrectionSettings;
using KadOzenka.Dal.Correction.Requests;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionByBargainProc : CorrectionByBargain<CorrectionByBargainRequest>
    {
        public CorrectionByBargainProc(CorrectionSettings correctionSettings) : base(correctionSettings)
        {
        }

        public void PerformBargainCorrectionProc(CorrectionByBargainRequest request)
        {
            ValidateRequest(request);

            var query = PrepareMarketObjectsQuery(request);
            var objects = GetMarketObjects(query);
            var suggestionObjectsWithPriceCoefficients = GetSuggestionObjectsWithPriceCoefficients(request.CoverageAreaType, objects);

            foreach (var objectWithCoefficient in suggestionObjectsWithPriceCoefficients)
            {
                if (objectWithCoefficient.Key.Price.HasValue && objectWithCoefficient.Value.HasValue)
                {
                    objectWithCoefficient.Key.PriceAfterCorrectionByBargain = GetPriceAfterCorrectionByBargain(objectWithCoefficient.Key.Price, objectWithCoefficient.Value);
                    objectWithCoefficient.Key.Save();
                }
                
            }
        }
    }
}
