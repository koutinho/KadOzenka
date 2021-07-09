using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace KadOzenka.WebClients.RgisClient.Client
{
	public class ApiResponse<T>
	{
		public HttpStatusCode StatusCode { get; set; }

		public T Data { get; set; }
		public ApiResponse(IRestResponse response)
		{
			StatusCode = response.StatusCode;
			Data = JsonConvert.DeserializeObject<T>(response.Content);
		}
	}
}