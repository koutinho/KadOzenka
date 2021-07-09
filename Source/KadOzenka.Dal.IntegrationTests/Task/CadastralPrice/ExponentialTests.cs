using System;
using System.Linq;
using KadOzenka.Dal.Integration._Builders.Model;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Task.CadastralPrice
{
	public class ExponentialTests : BaseCadastralPriceCalculationTests
	{
		protected OMModel ExponentialModel { get; set; }


		[SetUp]
		public void SetUp()
		{
			var modelA0 = 2;
			ExponentialModel = new ModelBuilder().Group(Group.Id).IsActive(true)
				.AlgorithmType(KoAlgoritmType.Exp).A0(modelA0).Build();
		}



		[Test]
		public void Can_Calculate_Price_By_Exp_Model_With_All_Possible_Factors_MarkType()
		{
			var factorWithoutMark = CreateFactorWithoutMark(Tour2018OksFirstIntegerFactor, ExponentialModel);
			var factorWithDefaultMark = CreateFactorWithDefaultMark(Tour2018OksSecondIntegerFactor, ExponentialModel, UnitFactorValueForIntegerFactor, out var mark);
			var factorWithStraightMark = CreateFactorWithStraightMark(Tour2018OksThirdIntegerFactor, ExponentialModel);
			var factorWithReverseMark = CreateFactorWithReverseMark(Tour2018OksFourthIntegerFactor, ExponentialModel);

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var factorsSum = GetExpectedCostForNoneMark(factorWithoutMark, UnitFactorValueForIntegerFactor) +
							 GetExpectedCadastralConstForDefaultMark(mark, factorWithDefaultMark) +
							 GetExpectedCadastralCostForStraightType(factorWithStraightMark) +
							 GetExpectedCadastralCostForReverseMark(factorWithReverseMark);
			var expectedUpks = ExponentialModel.A0ForExponentialInFormula * (decimal)Math.Exp((double)factorsSum);
			CheckCalculatedUnit(Unit.Id, expectedUpks);
		}

		
		#region Support Methods

		protected decimal GetExpectedCostForNoneMark(OMModelFactor factor, decimal unitFactorValue)
		{
			return unitFactorValue * factor.B0InFormula;
		}

		private decimal GetExpectedCadastralConstForDefaultMark(OMModelingDictionariesValues mark, OMModelFactor factor)
		{
			return mark.CalculationValue * factor.B0InFormula;
		}

		private decimal GetExpectedCadastralCostForStraightType(OMModelFactor factor)
		{
			return (factor.CorrectingTermInFormula + UnitFactorValueForIntegerFactor) / factor.KInFormula * factor.B0InFormula;
		}

		private decimal GetExpectedCadastralCostForReverseMark(OMModelFactor factor)
		{
			return factor.KInFormula / (factor.CorrectingTermInFormula + UnitFactorValueForIntegerFactor) * factor.B0InFormula;
		}

		#endregion
	}
}
