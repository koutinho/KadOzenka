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
		public string From { get; set; }
		public string To { get; set; }

        public override string ToString() => $"ID:{Id}\nReferenceId:{ReferenceId}\nTypeControl:{TypeControl}\nType:{Type}\nText:{Text}\nValue:{Value}\nFrom:{From}\nTo:{To}\n\n\n";

        public string ValueCasted
		{
			get
			{
				if (Value == null)
				{
					return null;
				}
				if (Value is string)
				{
					return Value.ToString();
				}
				else
				{
					return ((Newtonsoft.Json.Linq.JArray)Value).First.ToString();
				}
			}
		}
	}
}
