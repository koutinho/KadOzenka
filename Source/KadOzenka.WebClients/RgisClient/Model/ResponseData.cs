using System.Collections.Generic;
using Newtonsoft.Json;

namespace KadOzenka.WebClients.RgisClient.Model
{
	public class ResponseDataSingle
	{
		[JsonProperty("result")]
		public List<Result> Result { get; set; }
	}
	public class ResponseData
	{
		[JsonProperty("result")]
		public List<List<Result>> Result { get; set; }
	}

	public class Result
	{
		[JsonProperty("msg")]
		public string Message { get; set; }

		[JsonProperty("distance")]
		public double? Distance { get; set; }

		[JsonProperty("params")]
		public Params Params { get; set; }
	}

	public class Params
	{
		[JsonProperty("cnum")]
		public string KadNumber { get; set; }

		[JsonProperty("connectiondataset_code")]
		public string LayerName { get; set; }
	}
}