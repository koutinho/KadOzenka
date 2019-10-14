using Core.Register;
using Core.Shared.Extensions;
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

		public string GetAttributeName()
		{
			return RegisterCache.GetAttributeData(AttributeId).Name;
		}

		public string GetValueInString()
		{
			if(NumValue != null)
			{
				return NumValue.ToString();
			}
			else if(DtValue != null)
			{
				return DtValue.GetString();
			}

			return StringValue;
		}

		public string GetDocument()
		{
			List<string> docFacets = new List<string>();

			if (DocType.IsNotEmpty()) docFacets.Add(DocType);

			if (DocNumber.IsNotEmpty()) docFacets.Add($"№{DocNumber}");

			if (DocDate.GetString().IsNotEmpty()) docFacets.Add($"от {DocDate.GetString()}");

			return String.Join(" ", docFacets);
		}
	}
}
