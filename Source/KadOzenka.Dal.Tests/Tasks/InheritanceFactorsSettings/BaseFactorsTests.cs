using KadOzenka.Dal.Tasks;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings;
using KadOzenka.Dal.Tasks.InheritanceFactorSettings.Repositories;
using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.Tasks.InheritanceFactorsSettings
{
	public class BaseFactorsTests : BaseTests
	{
		protected InheritanceInheritanceFactorSettingsService InheritanceInheritanceFactorSettingsService => Provider.GetService<InheritanceInheritanceFactorSettingsService>();
		protected Mock<IFactorSettingsRepository> FactorSettingsRepository { get; set; }

		[SetUp]
		public void BaseInheritanceFactorsSettingsSetUp()
		{
			FactorSettingsRepository = new Mock<IFactorSettingsRepository>();
		}

		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<InheritanceInheritanceFactorSettingsService>();
			container.AddTransient(typeof(IFactorSettingsRepository), x => FactorSettingsRepository.Object);
		}
	}
}
