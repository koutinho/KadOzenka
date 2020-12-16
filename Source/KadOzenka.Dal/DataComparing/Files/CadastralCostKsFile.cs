using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using KadOzenka.Dal.DataComparing.StorageManagers;

namespace KadOzenka.Dal.DataComparing.Files
{
	public class CadastralCostKsFile : File
	{
		public XDocument File { get; protected set; }

		public CadastralCostKsFile(string fileName, SystemType systemType) : base(fileName)
		{
			SystemType = systemType;
		}

		public void LoadFileFromStorage()
		{
			var fileStream = CadastralCostDataComparingStorageManager.GetComparingDataFile(FullName);
			File = XDocument.Load(fileStream);
			fileStream.Dispose();
		}

		public void FillUnitCadastralCostDictionary(Dictionary<string, decimal?> dictionary)
		{
			IEnumerable<XElement> list = File.XPathSelectElements("//Parcel");
			foreach (var xElement in list)
			{
				var cadastralNumber = xElement.Attributes().FirstOrDefault(x => x.Name == "CadastralNumber")?.Value;
				var cadastralCostString = xElement.XPathSelectElement("Ground_Payments/CadastralCost")?.Attributes()
					.FirstOrDefault(x => x.Name == "Value")?.Value;
				decimal? cadastralCost = null;
				if (decimal.TryParse(cadastralCostString.Replace(",", "."), out decimal parseResult))
					cadastralCost = parseResult;
				else if(decimal.TryParse(cadastralCostString.Replace(".", ","), out parseResult))
					cadastralCost = parseResult;

				if (!string.IsNullOrEmpty(cadastralNumber))
					dictionary.TryAdd(cadastralNumber, cadastralCost);
			}
		}
	}
}
