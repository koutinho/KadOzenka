using NUnit.Framework;

namespace KadOzenka.Dal.IntegrationTests.Task
{
	public class BaseTaskTests : BaseTests
	{
		protected string PathToFileFolder => @".\Task\_Files\";

		[OneTimeSetUp]
		protected void OneTimeSetUpForTask()
		{
			
		}
	}
}
