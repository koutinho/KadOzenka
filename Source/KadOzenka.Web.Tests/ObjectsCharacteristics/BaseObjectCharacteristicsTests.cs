using KadOzenka.Web.Controllers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.ObjectsCharacteristics
{
	public class BaseObjectCharacteristicsTests : BaseTests
	{
		protected ObjectsCharacteristicsController ObjectsCharacteristicsController => Provider.GetService<ObjectsCharacteristicsController>();

		[SetUp]
		public void BaseTourSetUp()
		{

		}

		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ObjectsCharacteristicsController>();
		}
	}
}
