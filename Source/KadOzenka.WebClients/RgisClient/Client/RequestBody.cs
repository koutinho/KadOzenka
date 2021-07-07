using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace KadOzenka.WebClients.RgisClient.Client
{
	public class RequestBody
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("method")]
		public string Method { get; set; }

		[JsonProperty("jsonrpc")]
		public string JsonRpc = "2.0";

		[JsonProperty("params")]
		public object Params { get; set; }
	}
}