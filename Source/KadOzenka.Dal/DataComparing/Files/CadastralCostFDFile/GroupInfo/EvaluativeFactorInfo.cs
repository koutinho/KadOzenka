using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile.GroupInfo
{
	public class EvaluativeFactorInfo
	{
		public string FactorId { get; protected set; }
		public string FactorName { get; protected set; }
		public bool IsSignMarket { get; protected set; }
		public Dictionary<string, QualitativeValue> Qualitatives { get; protected set; }

		public EvaluativeFactorInfo(string factorId, string factorName, bool isSignMarket)
		{
			FactorId = factorId;
			FactorName = factorName;
			IsSignMarket = isSignMarket;
			Qualitatives = new Dictionary<string, QualitativeValue>();
		}

		public void SetQualitativeValues(XElement evaluativeFactorElement, Dictionary<string, string> castAccounts)
		{
			var qualitativeValueElements =
				evaluativeFactorElement.XPathSelectElements("QualitativeValues/QualitativeValue");
			foreach (var qualitativeValueElement in qualitativeValueElements)
			{
				var qualitativeId = qualitativeValueElement.XPathSelectElement("Qualitative_Id")?.Value;
				var qualitativeValue = qualitativeValueElement.XPathSelectElement("Qualitative_Value")?.Value;
				if (qualitativeId != null)
				{
					castAccounts.TryGetValue(qualitativeId, out var qualitativeDimension);
					Qualitatives.TryAdd(qualitativeId, new QualitativeValue(qualitativeId, qualitativeValue, qualitativeDimension));
				}
			}
		}
	}
}

