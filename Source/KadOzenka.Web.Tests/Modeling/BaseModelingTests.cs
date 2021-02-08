﻿using KadOzenka.Web.Controllers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Modeling
{
	[TestFixture]
	public class BaseModelingTests : BaseTests
	{
		protected ModelingController ModelingController => Provider.GetService<ModelingController>();


		[SetUp]
		public void BaseModelingTestsSetUp()
		{

		}



		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ModelingController>();
		}
	}
}