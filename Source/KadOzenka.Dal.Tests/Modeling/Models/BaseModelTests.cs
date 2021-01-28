using KadOzenka.Dal.Modeling;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests.Modeling.Models
{
	[TestFixture]
	public class BaseModelTests : BaseTests
	{
		protected ModelingService ModelingService { get; set; }

		[SetUp]
		public void BaseModelSetUp()
		{
			ModelingService = new ModelingService();
		}
	}
}