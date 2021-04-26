using System;
using KadOzenka.Dal.XmlParser;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes
{
	//TODO KOMO-20 убрать ненужные конструкторы, в оставшихся сделать через this
	public class ImportedAttributeGkn: ImportedAttribute
	{
		public Func<xmlObjectParticular, bool> CanSetValue { get; }
		public Func<xmlObjectParticular, object> GetValue { get; }
		public Action<xmlObject, object> SetValue { get; }
		protected override bool SkipNullValues { get; } = true;

		public ImportedAttributeGkn(long attributeId, Func<xmlObjectParticular, object> getValue) : base(attributeId)
		{
			GetValue = getValue;
			CanSetValue = particular => true;
		}

		public ImportedAttributeGkn(long attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue) : this(attributeId, getValue)
		{
			SetValue = setValue;
		}
		public ImportedAttributeGkn(long attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue, Func<xmlObjectParticular, bool> canSetValue) 
			: this(attributeId, getValue, setValue)
		{
			CanSetValue = canSetValue ??= x => true;
		}

		public ImportedAttributeGkn(long attributeId, Func<xmlObjectParticular, object> getValue,
			Func<xmlObjectParticular, bool> canSetValue)
			: base(attributeId)
		{
			GetValue = getValue;
			CanSetValue = canSetValue ??= x => true;
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
