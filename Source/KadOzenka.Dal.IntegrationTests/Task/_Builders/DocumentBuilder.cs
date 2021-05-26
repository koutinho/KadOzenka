using KadOzenka.Common.Tests.Builders.Task;
using ObjectModel.Core.TD;

namespace KadOzenka.Dal.IntegrationTests.Task._Builders
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
