using System;
using KadOzenka.Dal.XmlParser;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes
{
	public class ImportedAttributeGkn: ImportedAttribute
	{
		public Func<xmlObjectParticular, bool> CanSetValue { get; }
		public Func<xmlObjectParticular, object> GetValue { get; }
		public Action<xmlObject, object> SetValue { get; }
		protected override bool SkipNullValues { get; } = true;


		public ImportedAttributeGkn(long attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue, Func<xmlObjectParticular, bool> canSetValue)
			: base(attributeId)
		{
			GetValue = getValue;
			SetValue = setValue;
			CanSetValue = canSetValue ??= x => true;
		}

		public void SaveAttributeValue(xmlObjectParticular current, long idObject, long idDocument, DateTime sDate, DateTime otDate,
			long idUser)
		{
			object val = null;
			try
			{
				if (CanSetValue(current))
				{
					val = GetValue(current);
					SetAttributeValue(val, idObject, idDocument, sDate, otDate, idUser);
				}
			}
			catch (Exception e)
			{
				Serilog.Log.ForContext<ImportedAttributeGkn>()
					.ForContext("ObjectId", idObject)
					.ForContext("AttributeId", AttributeId)
					.ForContext("Value", val, destructureObjects: true)
					.Error(e, "Ошибка во время сохранения гбу-атрибута для объекта {CadastralNumber}", current.CadastralNumber);
				
				throw;
			}
		}
	}
}
