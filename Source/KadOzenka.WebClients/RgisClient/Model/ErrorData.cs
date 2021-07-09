using Newtonsoft.Json;

namespace KadOzenka.WebClients.RgisClient.Model
{
	public class ErrorData
	{
		[JsonProperty("error")]
		public Error Error { get; set; }
	}

	public class Error
	{
		[JsonProperty("code")]
		public long? Code { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("data")]
		public object Data { get; set; }
	}
}