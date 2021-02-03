using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace KadOzenka.Web.Tests
{
	public class BaseTests
	{
		protected ServiceProvider Provider { get; set; }
		

		[OneTimeSetUp]
		public void BaseTestsSetUp()
		{
			ConfigureServices();
		}

		protected static List<T> CreateListWithRepeatedElements<T>(T element, int count)
		{
			var elements = new List<T>(count);
			elements.AddRange(Enumerable.Repeat(element, count));
			return elements;
		}

		protected virtual void AddServicesToContainer(ServiceCollection container)
		{

		}

		protected object GetPropertyFromJson(IActionResult result, string propertyName)
		{
			var jsonResult = result as JsonResult;
			return jsonResult?.Value?.GetType().GetProperty(propertyName)?.GetValue(jsonResult.Value, null);
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
