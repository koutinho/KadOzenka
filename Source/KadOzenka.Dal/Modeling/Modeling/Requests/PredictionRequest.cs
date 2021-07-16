using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.Dal.Modeling.Modeling.Requests
{
    public class PredictionRequest : GeneralRequest
    {
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

            OmModelToMarketObjectsIds = new List<long>();
        }
    }
}
