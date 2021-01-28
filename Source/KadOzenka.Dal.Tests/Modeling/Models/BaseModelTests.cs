using System;
using KadOzenka.Dal.Modeling;
using NUnit.Framework;

namespace KadOzenka.Dal.Tests.Modeling.Models
{
	[TestFixture]
	public class BaseModelTests : BaseTests
	{
		protected Random Random { get; set; }
		protected ModelingService ModelingService { get; set; }

		[SetUp]
		public void SetUp()
		{
			Random = new Random();
			ModelingService = new ModelingService();
		}
	}
}