using System;
using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser.GknParserXmlElements
{
	public class xmlSubConstruction
	{
		public xmlCodeNameValue KeyParameter { get; set; }
		public List<xmlEncumbranceOks> EncumbrancesOks { get; set; }
		public string NumberRecord { get; set; }
		public DateTime? DateCreated { get; set; }

		public xmlSubConstruction()
		{
			EncumbrancesOks = new List<xmlEncumbranceOks>();
			KeyParameter = new xmlCodeNameValue();
		}
	}
}
