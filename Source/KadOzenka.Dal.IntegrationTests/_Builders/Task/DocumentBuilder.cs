using KadOzenka.Common.Tests.Builders.Task;
using ObjectModel.Core.TD;

namespace KadOzenka.Dal.Integration._Builders.Task
{
	public class DocumentBuilder : ADocumentBuilder
	{
		public override OMInstance Build()
		{
			_document.Save();
			return _document;
		}
	}
}
