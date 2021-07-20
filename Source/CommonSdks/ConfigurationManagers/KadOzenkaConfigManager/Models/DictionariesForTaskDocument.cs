using System.Xml;
using Core.ConfigParam;

namespace CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models
{
	/// <summary>
	/// Схема для импорта данных ГКН
	/// </summary>
	public class DictionariesForTaskDocument
	{
		private const string _basePathPart = "EmbeddedResource.GknImport.";


		private XmlDocument _allDocumentsDictionary;
		public XmlDocument AllDocuments => FillDictionary(ref _allDocumentsDictionary, "dAllDocuments_v05.xsd");

		private XmlDocument _allowedUseDictionary;
		public XmlDocument AllowedUse => FillDictionary(ref _allowedUseDictionary, "dAllowedUse_v02.xsd");

		private XmlDocument _assBuilding;
		public XmlDocument AssBuilding => FillDictionary(ref _assBuilding, "dAssBuilding_v02.xsd");

		private XmlDocument _assFlat;
		public XmlDocument AssFlat => FillDictionary(ref _assFlat, "dAssFlat_v02.xsd");

		private XmlDocument _assFlatType;
		public XmlDocument AssFlatType => FillDictionary(ref _assFlatType, "dAssFlatType_v01.xsd");

		private XmlDocument _categories;
		public XmlDocument Categories => FillDictionary(ref _categories, "dCategories_v01.xsd");

		private XmlDocument _cultural;
		public XmlDocument Cultural => FillDictionary(ref _cultural, "dCultural_v01.xsd");

		private XmlDocument _encumbrances;
		public XmlDocument Encumbrances => FillDictionary(ref _encumbrances, "dEncumbrances_v04.xsd");

		private XmlDocument _forestCategoryProtective;
		public XmlDocument ForestCategoryProtective => FillDictionary(ref _forestCategoryProtective, "dForestCategoryProtective_v01.xsd");

		private XmlDocument _forestEncumbrances;
		public XmlDocument ForestEncumbrances => FillDictionary(ref _forestEncumbrances, "dForestEncumbrances_v01.xsd");

		private XmlDocument _forestUse;
		public XmlDocument ForestUse => FillDictionary(ref _forestUse, "dForestUse_v01.xsd");

		private XmlDocument _formEvents;
		public XmlDocument FormEvents => FillDictionary(ref _formEvents, "dFormEvents_v01.xsd");

		private XmlDocument _inspection;
		public XmlDocument Inspection => FillDictionary(ref _inspection, "dInspection_v01.xsd");

		private XmlDocument _naturalObjects;
		public XmlDocument NaturalObjects => FillDictionary(ref _naturalObjects, "dNaturalObjects_v01.xsd");

		private XmlDocument _parcels;
		public XmlDocument Parcels => FillDictionary(ref _parcels, "dParcels_v02.xsd");

		private XmlDocument _objectType;
		public XmlDocument ObjectType => FillDictionary(ref _objectType, "dRealty_v04.xsd");

		private XmlDocument _specialTypeFlat;
		public XmlDocument SpecialTypeFlat => FillDictionary(ref _specialTypeFlat, "dSpecialTypeFlat_v01.xsd");

		private XmlDocument _typeParameter;
		public XmlDocument TypeParameter => FillDictionary(ref _typeParameter, "dTypeParameter_v01.xsd");

		private XmlDocument _typeStorey;
		public XmlDocument TypeStorey => FillDictionary(ref _typeStorey, "dTypeStorey_v01.xsd");

		private XmlDocument _utilizations;
		public XmlDocument Utilizations => FillDictionary(ref _utilizations, "dUtilizations_v01.xsd");

		private XmlDocument _wall;
		public XmlDocument Wall => FillDictionary(ref _wall, "dWall_v02.xsd");


		#region Support Methods

		private XmlDocument FillDictionary(ref XmlDocument document, string name)
		{
			if (document != null)
				return document;

			var fileContent = Configuration.GetParamEmbeddedResource<string>($"{_basePathPart}{name}");

			document = new XmlDocument();
			document.LoadXml(fileContent);

			return document;
		}

		#endregion
	}
}