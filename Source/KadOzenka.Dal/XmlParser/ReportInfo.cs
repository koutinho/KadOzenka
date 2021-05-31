using System;

namespace KadOzenka.Dal.XmlParser
{
	public class ReportInfo
	{
		public string CadastralNumber { get; set; }
		public string Error { get; private set; }
		public string NotProcessedAttributeNames { get; private set; }
		public string TypeConvertingError { get; private set; }

		public bool MustWriteToReport => !string.IsNullOrWhiteSpace(Error) ||
		                                 !string.IsNullOrWhiteSpace(NotProcessedAttributeNames) ||
		                                 !string.IsNullOrWhiteSpace(TypeConvertingError);


		public void AddNotProcessedAttribute(string message, int rowIndex, int columnIndex)
		{
			NotProcessedAttributeNames = ConcatMessages(NotProcessedAttributeNames, message, rowIndex, columnIndex);
		}

		public void AddError(string message, int rowIndex)
		{
			Error = $"{message}.{Environment.NewLine}Строка №{rowIndex + 1}.";
		}

		public void AddTypeConvertingError(string message, int rowIndex, int columnIndex)
		{
			TypeConvertingError = ConcatMessages(TypeConvertingError, message, rowIndex, columnIndex);
		}

		private string ConcatMessages(string field, string message, int rowIndex, int columnIndex)
		{
			var fullMessage = !string.IsNullOrWhiteSpace(field)
				? $"{field};{Environment.NewLine}{message}"
				: $"{message}";

			fullMessage += $" (Строка №{rowIndex + 1}, колонка {columnIndex + 1})";

			return fullMessage;
		}
	}
}
