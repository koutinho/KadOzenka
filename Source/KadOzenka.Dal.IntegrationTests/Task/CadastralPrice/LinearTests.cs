using System;
using System.Linq;
using KadOzenka.Dal.Integration._Builders.Model;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Task.CadastralPrice
{
	public class LinearTests : BaseCadastralPriceCalculationTests
	{
		protected OMModel LinearModel { get; set; }


		[SetUp]
		public void SetUp()
		{
			var modelA0 = 2;
			LinearModel = new ModelBuilder().Group(Group.Id).IsActive(true)
				.AlgorithmType(KoAlgoritmType.Line).A0(modelA0).Build();
		}



		[Test]
		public void Can_Calculate_Price_By_Lin_Model_With_All_Possible_Factors_MarkType()
		{
			var factorWithoutMark = CreateFactorWithoutMark(Tour2018OksFirstIntegerFactor, LinearModel);
			var factorWithDefaultMark = CreateFactorWithDefaultMark(Tour2018OksSecondIntegerFactor, LinearModel, UnitFactorValueForIntegerFactor, out var mark);
			var factorWithStraightMark = CreateFactorWithStraightMark(Tour2018OksThirdIntegerFactor, LinearModel);
			var factorWithReverseMark = CreateFactorWithReverseMark(Tour2018OksFourthIntegerFactor, LinearModel);

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedCadastralCost = LinearModel.A0ForLinearInFormula + GetExpectedCostForNoneMark(factorWithoutMark, UnitFactorValueForIntegerFactor) +
			                            GetExpectedCadastralConstForDefaultMark(mark, factorWithDefaultMark) +
			                            GetExpectedCadastralCostForStraightType(factorWithStraightMark) +
			                            GetExpectedCadastralCostForReverseMark(factorWithReverseMark);
			CheckCalculatedUnit(Unit.Id, expectedCadastralCost);
		}

		
		#region Support Methods

		protected decimal GetExpectedCostForNoneMark(OMModelFactor factor, decimal unitFactorValue)
		{
			return unitFactorValue * factor.B0InFormula;
		}

		private decimal GetExpectedCadastralConstForDefaultMark(OMMarkCatalog mark, OMModelFactor factor)
		{
			return mark.MetkaFactor.GetValueOrDefault() * factor.B0InFormula;
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
