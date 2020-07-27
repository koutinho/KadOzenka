using Newtonsoft.Json.Linq;

namespace KadOzenka.Web.Models.Prefilter
{
	public class FilterModel
	{
		public long Id { get; set; }
		public long? ReferenceId { get; set; }
		public string TypeControl { get; set; }
		public string Type { get; set; }
		public string Text { get; set; }
		public object Value { get; set; }
		public long? From { get; set; }
		public long? To { get; set; }

        public override string ToString() => $"ID:{Id}\nReferenceId:{ReferenceId}\nTypeControl:{TypeControl}\nType:{Type}\nText:{Text}\nValue:{Value}\nFrom:{From}\nTo:{To}\n\n\n";

        public string ValueStringCasted
		{
			get
			{
				if (Value is string)
				{
					return Value.ToString();
				}

				return null;
			}
		}

		public long[] ValueLongArrayCasted
		{
			get
			{
				if (Value is JArray array)
				{
					return array.ToObject<long[]>();
				} else if (Value is long[] longs)
				{
					return longs;
				}

				return null;
			}
		}

		public string ConvertToString()
		{
			var result = "{" +
			             $"\"typeControl\":\"{TypeControl}\"," +
			             $"\"type\":\"{Type}\"," +
			             $"\"text\":\"{Text}\",";
			result += GetMutablePart();
			result += $"\"id\":{Id}" +
			          "}";

			return result;
		}

		private string GetMutablePart()
		{
			string result = null;
			switch (Type)
			{
				case "REFERENCE":
					result = $"\"value\":[{string.Join(", ", ValueLongArrayCasted)}]," +
					         $"\"referenceId\":{ReferenceId},";
					break;
				case "INTEGER":
					result =
						$"\"from\":\"{(From.HasValue ? From.Value.ToString() : string.Empty)}\"," +
						$"\"to\":\"{(To.HasValue ? To.Value.ToString() : string.Empty)}\",";
					break;
				case "STRING":
					result = $"\"value\":\"{ValueStringCasted}\",";
					break;
			}

			return result;
		}
	}
}
