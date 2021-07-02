using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tasks.Repositories;
using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.Tasks.InheritanceFactorsSettings
{
	public class BaseFactorsTests : BaseTests
	{
		protected FactorSettingsService FactorSettingsService => Provider.GetService<FactorSettingsService>();
		protected Mock<IFactorSettingsRepository> FactorSettingsRepository { get; set; }

		[SetUp]
		public void BaseInheritanceFactorsSettingsSetUp()
		{
			FactorSettingsRepository = new Mock<IFactorSettingsRepository>();
		}

		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<FactorSettingsService>();
			container.AddTransient(typeof(IFactorSettingsRepository), x => FactorSettingsRepository.Object);
		}
	}
}
