using System;
using System.Collections.Generic;
using Core.Register;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.LongProcess.TaskLongProcesses;
using KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Entities;
using KadOzenka.Dal.Tests;
using KadOzenka.Dal.Tours;
using KadOzenka.Dal.Units;
using KadOzenka.Dal.Units.Repositories;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Dictionaries;
using Microsoft.Extensions.DependencyInjection;
using ModelingBusiness.Factors;
using ModelingBusiness.Model;
using ModelingBusiness.Model.Formulas;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.UnitTests.Tasks
{
	[TestFixture]
	public class CadastralPriceCalculationTests : BaseLongProcessTests
	{
		private CalculateCadastralPriceLongProcess LongProcess => Provider.GetService<CalculateCadastralPriceLongProcess>();
		private Mock<IUnitService> UnitService { get; set; }
		private Mock<IModelService> ModelService { get; set; }
		private Mock<IModelFactorsService> ModelFactorsService { get; set; }
		private Mock<IUnitRepository> UnitRepository { get; set; }


		[SetUp]
		public void SetUp()
		{
			UnitService = new Mock<IUnitService>();
			UnitRepository = new Mock<IUnitRepository>();
			ModelService = new Mock<IModelService>();
			ModelFactorsService = new Mock<IModelFactorsService>();
		}

		protected override void AddServicesToContainer(ServiceCollection container)
		{
			base.AddServicesToContainer(container);

			container.AddTransient<CalculateCadastralPriceLongProcess>();

			container.AddTransient(typeof(IUnitService), sp => UnitService.Object);
			container.AddTransient(typeof(IUnitRepository), sp => UnitRepository.Object);
			container.AddTransient(typeof(IModelService), sp => ModelService.Object);
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
			var modelingInfo = GetModelingInfo(factorId, RegisterAttributeType.DECIMAL, multiplier);

			var price = LongProcess.CalculateUpks(modelingInfo.Formula, modelingInfo.Factors, unitFactors, new Dictionary<string, decimal?>());

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
			var modelingInfo = GetModelingInfo(factorId, RegisterAttributeType.INTEGER, multiplier);
			
			var price = LongProcess.CalculateUpks(modelingInfo.Formula, modelingInfo.Factors, unitFactors, new Dictionary<string, decimal?>());

			Assert.That(price, Is.EqualTo(multiplier * value));
		}

		[Test]
		public void Can_Calculate_Price_With_One_String_Factor()
		{
			var dictionaryId = RandomGenerator.GenerateRandomInteger();
			var value = RandomGenerator.GetRandomString();
			var unitFactors = new List<UnitFactor>
			{
				new UnitFactor(dictionaryId) {Value = value}
			};
			var multiplier = 3;
			var mark = new MarkBuilder().Dictionary(dictionaryId).Value(value).Build();
			var modelingInfo = GetModelingInfo(dictionaryId, RegisterAttributeType.STRING, multiplier);

			var price = LongProcess.CalculateUpks(modelingInfo.Formula, modelingInfo.Factors, unitFactors,
				new Dictionary<string, decimal?>
				{
					{mark.Value, mark.CalculationValue}
				});

			Assert.That(price, Is.EqualTo(multiplier * mark.CalculationValue));
		}

		[Test]
		public void Can_Calculate_Price_With_Several_Factors()
		{
			var decimalFactorId = RandomGenerator.GenerateRandomInteger();
			var longFactorId = RandomGenerator.GenerateRandomInteger();
			var decimalValue = 2.1m;
			var longValue = (long)2;
			var unitFactors = new List<UnitFactor>
			{
				new UnitFactor(decimalFactorId) {Value = decimalValue},
				new UnitFactor(longFactorId) {Value = longValue}
			};
			var firstMultiplier = 3;
			var secondMultiplier = 4;
			var formula = $"{CalculateCadastralPriceLongProcess.AttributePrefixInFormula}{decimalFactorId} * {firstMultiplier} + {CalculateCadastralPriceLongProcess.AttributePrefixInFormula}{longFactorId} * {secondMultiplier}";
			var factors = new List<FactorInfo>
			{
				new()
				{
					FactorId = decimalFactorId,
					MarkType = MarkType.None,
					AttributeName = RandomGenerator.GetRandomString(),
					AttributeType = RegisterAttributeType.DECIMAL
				},
				new()
				{
					FactorId = longFactorId,
					MarkType = MarkType.None,
					AttributeName = RandomGenerator.GetRandomString(),
					AttributeType = RegisterAttributeType.INTEGER
				}
			};


			var price = LongProcess.CalculateUpks(formula, factors, unitFactors, new Dictionary<string, decimal?>());

			Assert.That(price, Is.EqualTo(firstMultiplier * decimalValue + longValue * secondMultiplier));
		}

		#endregion


		#region Замена формулы

		[Test]
		public void Can_Replace_Formula_With_Simple_Factors()
		{
			var model = new ModelBuilder().Build();
			var factor = new FactorInfo
			{
				FactorId = RandomGenerator.GenerateRandomInteger(),
				MarkType = MarkType.None,
				AttributeName = RandomGenerator.GetRandomString(),
				AttributeType = RegisterAttributeType.DECIMAL
			};
			var formulaGeneralPart = $" * {RandomGenerator.GenerateRandomInteger()}";
			ModelService.Setup(x => x.GetFormula(model, model.AlgoritmType_Code)).Returns($"{factor.AttributeName}{formulaGeneralPart}");

			var formula = LongProcess.PrepareFormula(model, new List<FactorInfo> { factor });

			Assert.That(formula, Is.EqualTo($"{CalculateCadastralPriceLongProcess.AttributePrefixInFormula}{factor.FactorId}{formulaGeneralPart}"));
		}

		[Test]
		public void Can_Replace_Formula_With_Factor_Of_Default_MarkType()
		{
			var model = new ModelBuilder().Build();
			var factor = new FactorInfo
			{
				FactorId = RandomGenerator.GenerateRandomInteger(),
				MarkType = MarkType.Default,
				AttributeName = RandomGenerator.GetRandomString(),
				AttributeType = RegisterAttributeType.DECIMAL
			};
			var formulaGeneralPart = $" * {RandomGenerator.GenerateRandomInteger()}";
			ModelService.Setup(x => x.GetFormula(model, model.AlgoritmType_Code)).Returns($"{BaseFormula.MarkTagInFormula}({factor.AttributeName}){formulaGeneralPart}");

			var formula = LongProcess.PrepareFormula(model, new List<FactorInfo> { factor });

			Assert.That(formula, Is.EqualTo($"{CalculateCadastralPriceLongProcess.AttributePrefixInFormula}{factor.FactorId}{formulaGeneralPart}"));
		}

		#endregion


		#region Support Methods

		private ModelingInfo GetModelingInfo(int factorId, RegisterAttributeType type, int multiplier)
		{
			return new ModelingInfo
			{
				Formula = $"{CalculateCadastralPriceLongProcess.AttributePrefixInFormula}{factorId} * {multiplier}",
				Factors = new List<FactorInfo>
				{
					new()
					{
						FactorId = factorId,
						MarkType = MarkType.None,
						AttributeName = RandomGenerator.GetRandomString(),
						AttributeType = type
					}
				}
			};
		}

		#endregion


		#region Entities

		private class ModelingInfo
		{
			public string Formula { get; set; }
			public List<FactorInfo> Factors { get; set; }
		}

		#endregion
	}
}
