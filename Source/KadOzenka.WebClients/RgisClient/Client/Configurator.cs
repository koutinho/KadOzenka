using System.Collections.Generic;
using System.Threading.Tasks;
using Core.ErrorManagment;
using KadOzenka.WebClients.RgisClient.Model;
using Newtonsoft.Json;
using RestSharp;

namespace KadOzenka.WebClients.RgisClient.Client
{
	public class Configurator
	{
		object lockMe = new ();

		private readonly string _basePath;

		public Configurator(string basePath)
		{
			_basePath = basePath;
		}

		private ApiClient _apiClient;

		public ApiClient ApiClient
		{
			get
			{
				return _apiClient ??= new ApiClient(_basePath);
			}
		}


		public List<object> GetParams(RequestData data, string dataSetCode)
		{
			string options = "shortest";
			List<object> res = new ();
			var pOptions = new ParallelOptions
			{
				MaxDegreeOfParallelism = 10
			};

			Parallel.ForEach(data.KadNumbers, pOptions, kn =>
			{
				List<object> tempList = new();

				foreach (var layer in data.Layers)
				{
					tempList.Add(new
					{
						connectiondataset_code = layer,
						dataset_code = dataSetCode,
						cnum = kn,
						options
					});
				}
				lock (lockMe)
				{
					res.AddRange(tempList);
				}
			});

			return res;
		}

		public bool IsError(IRestResponse response, out string message)
		{
			
			var error = JsonConvert.DeserializeObject<ErrorData>(response.Content);
			message = "";
			bool isError =  error != null && error.Error?.Message != null;
			if (isError) message = "РГИС API: " + error.Error.Message;
			return isError;
		}

	}
}