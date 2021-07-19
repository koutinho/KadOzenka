using Core.Register;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Factors;
using KadOzenka.Dal.Tests;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using Microsoft.Extensions.DependencyInjection;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Factors.Repositories;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.UnitTests.Modeling.Factors
{
	public class BaseFactorsTests : BaseTests
	{
		protected ModelFactorsService ModelFactorsService => Provider.GetService<ModelFactorsService>();
		protected Mock<IModelFactorsRepository> ModelFactorsRepository { get; set; }

		[SetUp]
		public void BaseModelTestsSetUp()
		{
			ModelFactorsRepository = new Mock<IModelFactorsRepository>();
		}

		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ModelFactorsService>();
			container.AddTransient(typeof(IModelFactorsRepository), sp => ModelFactorsRepository.Object);
		}


		protected ManualModelFactorDto PrepareManualModelFactorForCRUD(MarkType markType, decimal? correctItem,
			decimal? k)
		{
			var factor = new ManualFactorDtoBuilder().Type(markType).CorrectItem(correctItem).K(k).Build();
			
			ModelFactorsRepository.Setup(x =>
					x.IsTheSameAttributeExists(factor.Id, factor.FactorId.Value, factor.ModelId.Value,
						factor.Type))
				.Returns(false);

			var attribute = new RegisterAttributeBuilder().Id(factor.FactorId).Type(RegisterAttributeType.INTEGER).Build();
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.GetValueOrDefault())).Returns(attribute);

			return factor;
		}
	}
}
