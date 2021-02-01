using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests
{
	public class BaseTests
	{
		protected Random Random { get; set; }
		protected ServiceProvider Provider { get; set; }
		

		[OneTimeSetUp]
		public void SetUp()
		{
			Random = new Random();
			ConfigureServices();
		}


		protected string GetRandomString(string beginning = "", int maxNumberOfCharacters = 5)
		{
			var guid = Guid.NewGuid();
			var salted = $"{beginning}_{guid}";

			return salted.Substring(0, Math.Min(maxNumberOfCharacters, salted.Length));
		}

		protected virtual void AddServicesToContainer(ServiceCollection container)
		{

		}

		
		#region Support Methods

		private void ConfigureServices()
		{
			var container = new ServiceCollection();

			AddServicesToContainer(container);

			Provider = container.BuildServiceProvider();
		}

		#endregion
	}
}
