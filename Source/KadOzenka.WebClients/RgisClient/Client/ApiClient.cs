using System;
using Core.Shared.Extensions;
using RestSharp;
using Serilog;

namespace KadOzenka.WebClients.RgisClient.Client
{
	public class ApiClient
	{
		public RestClient RestClient { get; set; }

		private readonly ILogger _log = Log.ForContext<ApiClient>();
		public ApiClient( string basePath = "")
		{
			_log.Debug("api url = {url}", basePath);
			if (basePath.IsEmpty()) throw new ArgumentException("Урл для Api РГИС не прередан");

			RestClient = new RestClient(basePath);
		}

		public IRestResponse CallApi(string body)
		{
			RestRequest request = new RestRequest();
			request.AddHeader("Content-Type", "application/json");
			request.Method = Method.POST;
			request.AddJsonBody(body);
			var response = RestClient.Execute(request);

			return response;
		}
	}
}