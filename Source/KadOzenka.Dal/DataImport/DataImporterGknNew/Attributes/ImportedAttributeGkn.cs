using System;
using KadOzenka.Dal.XmlParser;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes
{
	public class ImportedAttributeGkn: ImportedAttribute
	{
		public Func<xmlObjectParticular, bool> CanSetValue { get; }
		public Func<xmlObjectParticular, object> GetValue { get; }

		public ImportedAttributeGkn(long attributeId, Func<xmlObjectParticular, object> getValue) : base(attributeId)
		{
			GetValue = getValue;
			CanSetValue = particular => true;
		}

		public ImportedAttributeGkn(long attributeId, Func<xmlObjectParticular, object> getValue, Func<xmlObjectParticular, bool> canSetValue) 
			: base(attributeId)
		{
			GetValue = getValue;
			CanSetValue = canSetValue;
		}

		public void SaveAttributeValue(xmlObjectParticular current, long idObject, long idDocument, DateTime sDate, DateTime otDate,
			long idUser)
		{
			if (CanSetValue(current))
			{
				var val = GetValue(current);
				base.SetAttributeValue(val, idObject, idDocument, sDate, otDate, idUser);
			}
		}
	}
}
