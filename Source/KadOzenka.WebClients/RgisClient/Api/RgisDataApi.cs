using System;
using KadOzenka.WebClients.RgisClient.Client;
using KadOzenka.WebClients.RgisClient.Model;
using Newtonsoft.Json;
using Serilog;

namespace KadOzenka.WebClients.RgisClient.Api
{
	public class RgisDataApi
	{
		private readonly ILogger _log = Log.ForContext<RgisDataApi>();
		private readonly Configurator _configurator;

		public RgisDataApi()
		{
			_log.Debug("Урл для запросов в РГИС = {url}", WebClientsConfig.Current.RgisBaseUrl);
			var apiUrl = WebClientsConfig.Current.RgisBaseUrl;
			_configurator = new Configurator(apiUrl);
		}

		/// <summary>
		/// Запрос с большим кол-вом параметров
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public ApiResponse<ResponseData> GetDistanceZuFactorsValue(RequestData data)
		{
			if (data.Layers.Count == 0)
			{
				var error = new Exception("Не переданы слои для построения параметров запроса");
				_log.Error(error, error.Message);
				throw error;
			}
			string method = "CONNECTIONS.DSCALCDISTANCE.GETDATA";
			string dataSetCode = "BASE.DSPARCEL";
			Random rnd = new Random();
			RequestBody body = new RequestBody
			{
				Id = rnd.Next(1000),
				Method = method,
				Params = _configurator.GetParams(data, dataSetCode)
			};
		

			var res = _configurator.ApiClient.CallApi(JsonConvert.SerializeObject(body));

			if (_configurator.IsError(res, out string msg))
			{
				_log.ForContext("body", JsonConvert.SerializeObject(body)).Error(new Exception(msg), msg);
				throw new Exception(msg);
			}
			return new ApiResponse<ResponseData>(res);
		}

		/// <summary>
		/// Ответ от апи отличается если параметр в массиве один, это вариант на такой слчучай
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public ApiResponse<ResponseDataSingle> GetDistanceZuFactorsValueSingle(RequestData data)
		{
			string method = "CONNECTIONS.DSCALCDISTANCE.GETDATA";
			string dataSetCode = "BASE.DSPARCEL";
			Random rnd = new Random();
			RequestBody body = new RequestBody
			{
				Id = rnd.Next(1000),
				Method = method,
				Params = _configurator.GetParams(data, dataSetCode)
			};

			var res = _configurator.ApiClient.CallApi(JsonConvert.SerializeObject(body));
			if (_configurator.IsError(res, out string msg))
			{
				_log.ForContext("body", JsonConvert.SerializeObject(body)).Error(new Exception(msg), msg);
				throw new Exception(msg);
			}
			return new ApiResponse<ResponseDataSingle>(res);
		}


		public ApiResponse<ResponseData> GetDistanceOksFactorsValue(RequestData data)
		{
			string method = "CONNECTIONS.DSCALCDISTANCE.GETDATA";
			string dataSetCode = "BASE.DSREALTY";
			Random rnd = new Random();
			RequestBody body = new RequestBody
			{
				Id = rnd.Next(1000),
				Method = method,
				Params = _configurator.GetParams(data, dataSetCode)
			};

			var res = _configurator.ApiClient.CallApi(JsonConvert.SerializeObject(body));
			if (_configurator.IsError(res, out string msg))
			{
				_log.ForContext("body", JsonConvert.SerializeObject(body)).Error(new Exception(msg), msg);
				throw new Exception(msg);
			}
			return new ApiResponse<ResponseData>(res);
		}

		/// <summary>
		/// Ответ от апи отличается если параметр в массиве один, это вариант на такой слчучай
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public ApiResponse<ResponseDataSingle> GetDistanceOksFactorsValueSingle(RequestData data)
		{
			string method = "CONNECTIONS.DSCALCDISTANCE.GETDATA";
			string dataSetCode = "BASE.DSREALTY";
			Random rnd = new Random();
			RequestBody body = new RequestBody
			{
				Id = rnd.Next(1000),
				Method = method,
				Params = _configurator.GetParams(data, dataSetCode)
			};

			var res = _configurator.ApiClient.CallApi(JsonConvert.SerializeObject(body));
			if (_configurator.IsError(res, out string msg))
			{
				_log.ForContext("body", JsonConvert.SerializeObject(body)).Error(new Exception(msg), msg);
				throw new Exception(msg);
			}
			return new ApiResponse<ResponseDataSingle>(res);
		}
	}

}