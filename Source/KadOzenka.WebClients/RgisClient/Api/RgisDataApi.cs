using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using KadOzenka.WebClients.RgisClient.Client;
using Newtonsoft.Json;
using Serilog;

namespace KadOzenka.WebClients.RgisClient.Api
{
	public class RgisDataApi
	{
		private ILogger _log = Log.ForContext<RgisDataApi>();
		private readonly string _apiUrl;

		public RgisDataApi()
		{
			_log.Debug("Урл для запросов в РГИС = {url}", WebClientsConfig.Current.RgisBaseUrl);
			_apiUrl = WebClientsConfig.Current.RgisBaseUrl;
		}


		public void GetDistanceFactorsValue(RequestData data)
		{
			var paramsList = new List<object>();

			foreach (var dataLayer in data.Layers)
			{
				paramsList.Add(new
				{
					connectiondataset_code = dataLayer,
					dataset_code = "BASE.DSPARCEL",
					cnum = data.Kn,
					options = "shortest"
				});
			}

			RequestBody body = new RequestBody
			{
				Id = 1,
				Method = "CONNECTIONS.DSCALCDISTANCE.GETDATA",
				Params = paramsList
			};



			var api = new ApiClient(_apiUrl);
			var res = api.CallApi(JsonConvert.SerializeObject(body));

		}
	}

}