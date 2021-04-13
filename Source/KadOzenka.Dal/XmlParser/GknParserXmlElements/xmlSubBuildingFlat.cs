using System;
using System.Collections.Generic;

namespace KadOzenka.Dal.XmlParser.GknParserXmlElements
{
	public class xmlSubBuildingFlat
	{
		public double? Area { get; set; }
		public List<xmlEncumbranceOks> EncumbrancesOks { get; set; }
		public string NumberRecord { get; set; }
		public DateTime? DateCreated { get; set; }

		public xmlSubBuildingFlat()
		{
			EncumbrancesOks = new List<xmlEncumbranceOks>();
		}
	}
}
