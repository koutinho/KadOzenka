using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile.GroupInfo;
using KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile.UnitInfo;
using KadOzenka.Dal.DataComparing.StorageManagers;

namespace KadOzenka.Dal.DataComparing.Files.CadastralCostFDFile
{
	public class CadastralCostFDFile : File
	{
		public XDocument File { get; protected set; }

		public CadastralCostFDFile(string fileName, SystemType systemType) : base(fileName)
		{
			SystemType = systemType;
		}

		public void LoadFileFromStorage()
		{
			var fileStream = CadastralCostDataComparingStorageManager.GetComparingDataFile(FullName);
			File = XDocument.Load(fileStream);
			fileStream.Dispose();
		}

		public void FillUnitFactorsDictionary(Dictionary<string, UnitInfo.UnitInfo> dictionary)
		{
			var groupFactorsInfo = ParseGroupEvaluativeFactorInfos();

			var isModeling = !File.XPathSelectElements("//Package/Appraise/Statistical_Modelling").IsEmpty();
			var modelingAlgorithmType = File.XPathSelectElement("//Package/Appraise/Statistical_Modelling/Group_Real_Estate_Modelling/Rating_Model")?.Value;

			var unitElements = File.XPathSelectElements(isModeling
				? "//Package/Appraise/Statistical_Modelling/Group_Real_Estate_Modelling/Real_Estates/Real_Estate" 
				: "//Package/Appraise/Other/Evaluation_Group/Real_Estates/Real_Estate");
			foreach (var unitElement in unitElements)
			{
				var unitCadastralNumber = unitElement.XPathSelectElement("CadastralNumber")?.Value;
				if (!string.IsNullOrEmpty(unitCadastralNumber))
				{
					var info = isModeling 
						? new ModelingUnitInfo(unitElement, modelingAlgorithmType, groupFactorsInfo) 
						: new UnitInfo.UnitInfo(unitElement);
					dictionary.TryAdd(unitCadastralNumber, info);
				}
			}
		}

		private Dictionary<string, EvaluativeFactorInfo> ParseGroupEvaluativeFactorInfos()
		{
			var castAccountElements = File.XPathSelectElements("//Evaluative_Factors_Modelling/Evaluative_Factor_Modelling/Cast_Accounts/Cast_Account");
			var castAccountInfo = new Dictionary<string, string>();
			foreach (var castAccountElement in castAccountElements)
			{
				var id = castAccountElement.XPathSelectElement("Qualitative_Id")?.Value;
				var dimension = castAccountElement.XPathSelectElement("Dimension")?.Value;
				castAccountInfo.TryAdd(id, dimension);
			}

			var groupFactorsElements = File.XPathSelectElements("//Package/Evaluative_Factors/Evaluative_Factor");
			var groupFactorsInfo = new Dictionary<string, EvaluativeFactorInfo>();
			foreach (var groupFactorsElement in groupFactorsElements)
			{
				var factorId = groupFactorsElement.Attributes().FirstOrDefault(x => x.Name == "Id_Factor")?.Value;
				if (!string.IsNullOrEmpty(factorId))
				{
					var factorName = groupFactorsElement.XPathSelectElement("Name_Factor")?.Value;
					var isSignMarket = groupFactorsElement.Attributes().FirstOrDefault(x => x.Name == "Type")?.Value;
					var factorInfo = new EvaluativeFactorInfo(factorId, factorName, isSignMarket == "1");
					if (factorInfo.IsSignMarket)
					{
						factorInfo.SetQualitativeValues(groupFactorsElement, castAccountInfo);
					}

					groupFactorsInfo.TryAdd(factorId, factorInfo);
				}
			}

			return groupFactorsInfo;
		}
	}
}
