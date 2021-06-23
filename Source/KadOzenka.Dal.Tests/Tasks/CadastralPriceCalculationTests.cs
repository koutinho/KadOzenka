using System.Collections.Generic;
using Core.Register;
using KadOzenka.Common.Tests;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.LongProcess.TaskLongProcesses;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Tests;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Units;
using KadOzenka.Dal.Units.Repositories;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Tasks
{
	[TestFixture]
	public class CadastralPriceCalculationTests : BaseLongProcessTests
	{
		private CalculateCadastralPriceLongProcess LongProcess => Provider.GetService<CalculateCadastralPriceLongProcess>();
		private Mock<IUnitService> UnitService { get; set; }
		private Mock<IModelingService> ModelingService { get; set; }
		private Mock<IModelFactorsService> ModelFactorsService { get; set; }
		private Mock<IUnitRepository> UnitRepository { get; set; }


		[SetUp]
		public void SetUp()
		{
			UnitService = new Mock<IUnitService>();
			UnitRepository = new Mock<IUnitRepository>();
			ModelingService = new Mock<IModelingService>();
			ModelFactorsService = new Mock<IModelFactorsService>();
		}

		protected override void AddServicesToContainer(ServiceCollection container)
		{
			base.AddServicesToContainer(container);

			container.AddTransient<CalculateCadastralPriceLongProcess>();

			container.AddTransient(typeof(IUnitService), sp => UnitService.Object);
			container.AddTransient(typeof(IUnitRepository), sp => UnitRepository.Object);
			container.AddTransient(typeof(IModelingService), sp => ModelingService.Object);
			container.AddTransient(typeof(IModelFactorsService), sp => ModelFactorsService.Object);
		}


		#region Подстановка значений в формулу

		[Test]
		public void Can_Calculate_Price_With_One_Decimal_Factor()
		{
			var factorId = RandomGenerator.GenerateRandomInteger();
			var value = 2.1m;
			var unitFactors = new List<UnitFactor>
			{
				new UnitFactor(factorId) {Value = value}
			};
			var multiplier = 3;
			var modelingInfo = GetModelingInfo(factorId, multiplier);
			var attribute = new RegisterAttributeBuilder().Id(factorId).Type(RegisterAttributeType.DECIMAL).Build();

			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factorId)).Returns(attribute);

			var price = LongProcess.Calculate(modelingInfo, unitFactors, new List<OMMarkCatalog>());

			Assert.That(price, Is.EqualTo(multiplier * value).Within(0.1));
		}

		[Test]
		public void Can_Calculate_Price_With_One_Integer_Factor()
		{
			var factorId = RandomGenerator.GenerateRandomInteger();
			var value = (long) 2;
			var unitFactors = new List<UnitFactor>
			{
				new UnitFactor(factorId) {Value = value}
			};
			var multiplier = 3;
			var modelingInfo = GetModelingInfo(factorId, multiplier);
			var attribute = new RegisterAttributeBuilder().Id(factorId).Type(RegisterAttributeType.INTEGER).Build();
			
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factorId)).Returns(attribute);

			var price = LongProcess.Calculate(modelingInfo, unitFactors, new List<OMMarkCatalog>());

			Assert.That(price, Is.EqualTo(multiplier * value));
		}

		[Test]
		public void Can_Calculate_Price_With_One_String_Factor()
		{
			var factorId = RandomGenerator.GenerateRandomInteger();
			var value = RandomGenerator.GetRandomString();
			var unitFactors = new List<UnitFactor>
			{
				new UnitFactor(factorId) {Value = value}
			};
			var multiplier = 3;
			var mark = new MarkBuilder().Factor(factorId).Value(value).Build();
			var modelingInfo = GetModelingInfo(factorId, multiplier);
			var attribute = new RegisterAttributeBuilder().Id(factorId).Type(RegisterAttributeType.STRING).Build();
			
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factorId)).Returns(attribute);

			var price = LongProcess.Calculate(modelingInfo, unitFactors, new List<OMMarkCatalog> { mark });

			Assert.That(price, Is.EqualTo(multiplier * mark.MetkaFactor));
		}

		[Test]
		public void Can_Calculate_Price_With_Several_Factors()
		{
			var decimalFactorId = RandomGenerator.GenerateRandomInteger();
			var longFactorId = RandomGenerator.GenerateRandomInteger();
			var decimalValue = 2.1m;
			var longValue = (long) 2;
			var unitFactors = new List<UnitFactor>
			{
				new UnitFactor(decimalFactorId) {Value = decimalValue},
				new UnitFactor(longFactorId) {Value = longValue}
			};

			var firstMultiplier = 3;
			var secondMultiplier = 4;
			var modelingInfo = new CalculateCadastralPriceLongProcess.ModelingInfo()
			{
				Formula = $"{CalculateCadastralPriceLongProcess.AttributePrefixInFormula}{decimalFactorId} * {firstMultiplier} + {CalculateCadastralPriceLongProcess.AttributePrefixInFormula}{longFactorId} * {secondMultiplier}",
				Factors = new List<OMModelFactor>
				{
					new FactorBuilder().FactorId(decimalFactorId).MarkType(MarkType.None).Build(),
					new FactorBuilder().FactorId(longFactorId).MarkType(MarkType.None).Build()
				}
			};
			var decimalAttribute = new RegisterAttributeBuilder().Id(decimalFactorId).Type(RegisterAttributeType.DECIMAL).Build();
			var longAttribute = new RegisterAttributeBuilder().Id(longFactorId).Type(RegisterAttributeType.INTEGER).Build();
			
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(decimalFactorId)).Returns(decimalAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(longFactorId)).Returns(longAttribute);

			var price = LongProcess.Calculate(modelingInfo, unitFactors, new List<OMMarkCatalog>());

			Assert.That(price, Is.EqualTo(firstMultiplier * decimalValue + longValue * secondMultiplier));
		}

		#endregion


		#region Замена формулы

		[Test]
		public void Can_Replace_Formula_With_Simple_Factors()
		{
			var model = new ModelBuilder().Build();
			var factor = new FactorBuilder().MarkType(MarkType.None).Build();
			var attribute = new RegisterAttributeBuilder().Id(factor.Id).Build();
			var formulaGeneralPart = $" * {RandomGenerator.GenerateRandomInteger()}";
			
			ModelFactorsService.Setup(x => x.GetFactors(model.Id, model.AlgoritmType_Code)).Returns(new List<OMModelFactor> {factor});
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.Value)).Returns(attribute);
			ModelingService.Setup(x => x.GetFormula(model, model.AlgoritmType_Code)).Returns($"{attribute.Name}{formulaGeneralPart}");
			
			var modelingInfo = LongProcess.PrepareModelingInfo(model);

			Assert.That(modelingInfo.Formula, Is.EqualTo($"{CalculateCadastralPriceLongProcess.AttributePrefixInFormula}{factor.FactorId}{formulaGeneralPart}"));
		}

		[Test]
		public void Can_Replace_Formula_With_Factor_Of_Default_MarkType()
		{
			var model = new ModelBuilder().Build();
			var factor = new FactorBuilder().MarkType(MarkType.Default).Build();
			var attribute = new RegisterAttributeBuilder().Id(factor.Id).Build();
			var formulaGeneralPart = $" * {RandomGenerator.GenerateRandomInteger()}";

			ModelFactorsService.Setup(x => x.GetFactors(model.Id, model.AlgoritmType_Code)).Returns(new List<OMModelFactor> { factor });
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.Value)).Returns(attribute);
			ModelingService.Setup(x => x.GetFormula(model, model.AlgoritmType_Code)).Returns($"метка({attribute.Name}){formulaGeneralPart}");

			var modelingInfo = LongProcess.PrepareModelingInfo(model);

			Assert.That(modelingInfo.Formula, Is.EqualTo($"{CalculateCadastralPriceLongProcess.AttributePrefixInFormula}{factor.FactorId}{formulaGeneralPart}"));
		}

		#endregion


		#region Support Methods

		private CalculateCadastralPriceLongProcess.ModelingInfo GetModelingInfo(int factorId, int multiplier)
		{
			return new CalculateCadastralPriceLongProcess.ModelingInfo
			{
				Formula = $"{CalculateCadastralPriceLongProcess.AttributePrefixInFormula}{factorId} * {multiplier}",
				Factors = new List<OMModelFactor> { new FactorBuilder().FactorId(factorId).MarkType(MarkType.None).Build() }
			};
		}

		#endregion
	}
}
