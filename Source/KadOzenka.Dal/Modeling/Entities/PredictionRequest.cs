using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Entities
{
    public class PredictionRequest : GeneralRequest
    {
        //для сохранения коэффициентов, на основании которых прогнозировалась цена
        [JsonIgnore]
        public List<long> OmModelAttributeRelationIds { get; set; }

        //для сохранения спрогнозированной цены
        [JsonIgnore]
        public List<long> OmModelToMarketObjectsIds { get; set; }

        [JsonProperty("x")]
        public List<List<decimal?>> Coefficients { get; set; }


        public PredictionRequest()
        {
            Coefficients = new List<List<decimal?>>();

            OmModelAttributeRelationIds = new List<long>();
            OmModelToMarketObjectsIds = new List<long>();
        }
    }
}
