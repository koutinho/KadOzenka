using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.GbuObject
{
    public class GbuObjectAttribute
    {
		public int Id { get; set; }
		public int ObjectId { get; set; }
		public int AttributeId { get; set; }
		public DateTime Ot { get; set; }
		public DateTime S { get; set; }
		public int RefItemId { get; set; }
		public string StringValue { get; set; }
		public decimal? NumValue { get; set; }
		public DateTime? DtValue { get; set; }

		public int ChangeUserId { get; set; }
		public DateTime ChangeDate { get; set; }
		public int ChangeDocId { get; set; }

		public string UserFullname { get; set; }
		public string DocNumber { get; set; }
		public string DocType { get; set; }
		public DateTime DocDate { get; set; }
		
		public int ChangeId { get; set; }
		public int ChangeSetId { get; set; }
	}
}
