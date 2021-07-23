using Newtonsoft.Json;

namespace ModelingBusiness.Modeling.Responses
{
    public class GeneralResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("innerError")]
        public string InnerError { get; set; }

        [JsonProperty("response_data")]
        public object Data { get; set; }
    }
}
