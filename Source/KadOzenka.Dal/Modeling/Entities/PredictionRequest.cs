using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Entities
{
    public class PredictionRequest : GeneralRequest
    {
        /// <summary>
        /// Для сохранения коэффициентов, на основании которых прогнозировалась цена
        /// </summary>
        [JsonIgnore]
        public List<long> OmModelAttributeRelationIds { get; set; }

        /// <summary>
        /// Для сохранения спрогнозированной цены
        /// </summary>
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
