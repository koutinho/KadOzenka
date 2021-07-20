using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModelingBusiness.Modeling.Requests
{
    public class GeneralRequest
    {
        //для отладки, т.к. сервис не завязан на уникальных параметрах и возвращет просто список цен
        [JsonIgnore]
        public List<string> CadastralNumbers { get; set; }

        public GeneralRequest()
        {
            CadastralNumbers = new List<string>();
        }
    }
}
