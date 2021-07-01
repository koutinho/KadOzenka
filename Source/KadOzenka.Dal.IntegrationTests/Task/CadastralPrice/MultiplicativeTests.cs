using System;
using System.Linq;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.Integration._Builders.Model;
using KadOzenka.Dal.Integration._Builders.Task;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Task.CadastralPrice
{
	public class MultiplicativeTests : BaseCadastralPriceCalculationTests
	{
		protected OMModel MultiplicativeModel { get; set; }


		[SetUp]
		public void SetUp()
		{
			var modelA0 = 2;
			MultiplicativeModel = new ModelBuilder().Group(Group.Id).IsActive(true)
				.AlgorithmType(KoAlgoritmType.Multi).A0(modelA0).Build();
		}



		[Test]
		public void Can_Calculate_Price_By_Mult_Model_With_One_Factor_Of_None_MarkType()
		{
			//формула: свободный член * [(значение_фактора + поправка)^коэффициент]

			var factor = CreateFactorWithoutMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel);

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedCadastralCost = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCostForNoneMark(factor, UnitFactorValueForIntegerFactor);
			CheckCalculatedUnit(Unit.Id, expectedCadastralCost);
		}

		[Test]
		public void Can_Calculate_Price_By_Mult_Model_With_One_Factor_Of_Default_MarkType()
		{
			//формула: свободный член * [(метка(значение_фактора) + поправка)^коэффициент]

			var factor = CreateFactorWithDefaultMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel, UnitFactorValueForIntegerFactor, out var mark);

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedCadastralCost = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCadastralConstForDefaultMark(mark, factor);
			CheckCalculatedUnit(Unit.Id, expectedCadastralCost);
		}

		[Test]
		public void Can_Calculate_Price_By_Mult_Model_With_One_Factor_Of_Default_MarkType_With_Two_Marks_With_Different_Values()
		{
			//формула: свободный член * [(метка(значение_фактора) + поправка)^коэффициент]

			var factor = CreateFactorWithDefaultMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel, UnitFactorValueForIntegerFactor, out var mark);
			new MarkBuilder().Factor(factor.FactorId).Group(Group.Id)
				.Value(RandomGenerator.GetRandomString()).Metka(RandomGenerator.GenerateRandomDecimal())
				.Build();

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedCadastralCost = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCadastralConstForDefaultMark(mark, factor);
			CheckCalculatedUnit(Unit.Id, expectedCadastralCost);
		}

		[Test]
		//В БД может быть несколько меток с одной и той же группой/фактором/значением
		//когда выясниться, что таких значений быть не должно, тест можно удалить
		public void Can_Calculate_Price_By_Mult_Model_With_One_Factor_Of_Default_MarkType_With_Two_Marks_With_TheSame_Values()
		{
			//формула: свободный член * [(метка(значение_фактора) + поправка)^коэффициент]

			var factor = CreateFactorWithDefaultMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel, UnitFactorValueForIntegerFactor, out var firstMark);
			var secondMark = new MarkBuilder().Factor(factor.FactorId).Group(Group.Id)
				.Value(firstMark.ValueFactor)
				//чтобы случайно не создать такое же значение, как в mark
				.Metka(RandomGenerator.GenerateRandomInteger(maxNumber: 3) + firstMark.MetkaFactor.GetValueOrDefault())
				.Build();

			var errors = PerformCalculation(Task.Id, Group.Id);

			
			var firstPossibleExpectedCadastralCost = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCadastralConstForDefaultMark(firstMark, factor);
			var secondPossibleExpectedCadastralCost = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCadastralConstForDefaultMark(secondMark, factor);
			
			var unitWithCalculatedPrice = GetUnitById(Unit.Id);
			Assert.That(unitWithCalculatedPrice.CadastralCost,
				Is.EqualTo(firstPossibleExpectedCadastralCost).Within(0.01)
				.Or.EqualTo(secondPossibleExpectedCadastralCost).Within(0.01));

			var expectedUpks = unitWithCalculatedPrice.CadastralCost / Unit.Square.GetValueOrDefault();
			Assert.That(unitWithCalculatedPrice.Upks, Is.EqualTo(expectedUpks).Within(0.01));
			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
		}

		[Test]
		public void Can_Calculate_Price_By_Mult_Model_With_One_Factor_Of_Straight_MarkType()
		{
			//формула: свободный член * [(корректирующее_слагаемое + значение_фактора)/К + поправка]^коэффициент

			var factor = CreateFactorWithStraightMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel);

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedCadastralCost = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCadastralCostForStraightType(factor);
			CheckCalculatedUnit(Unit.Id, expectedCadastralCost);
		}

		[Test]
		public void Can_Calculate_Price_By_Mult_Model_With_One_Factor_Of_Reverse_MarkType()
		{
			//формула: свободный член * [К/(корректирующее_слагаемое + значение_фактора) + поправка]^коэффициент

			var factor = CreateFactorWithReverseMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel);

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedCadastralCost = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCadastralCostForReverseMark(factor);
			CheckCalculatedUnit(Unit.Id, expectedCadastralCost);
		}


		[Test]
		public void Can_Calculate_Price_By_Mult_Model_With_All_Possible_Factors_MarkType()
		{
			var factorWithoutMark = CreateFactorWithoutMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel);
			var factorWithDefaultMark = CreateFactorWithDefaultMark(Tour2018OksSecondIntegerFactor, MultiplicativeModel, UnitFactorValueForIntegerFactor, out var mark);
			var factorWithStraightMark = CreateFactorWithStraightMark(Tour2018OksThirdIntegerFactor, MultiplicativeModel);
			var factorWithReverseMark = CreateFactorWithReverseMark(Tour2018OksFourthIntegerFactor, MultiplicativeModel);

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedCadastralCost = MultiplicativeModel.A0ForMultiplicativeInFormula * 
			                            GetExpectedCostForNoneMark(factorWithoutMark, UnitFactorValueForIntegerFactor) *
			                            GetExpectedCadastralConstForDefaultMark(mark, factorWithDefaultMark) *
			                            GetExpectedCadastralCostForStraightType(factorWithStraightMark) *
			                            GetExpectedCadastralCostForReverseMark(factorWithReverseMark);
			
			CheckCalculatedUnit(Unit.Id, expectedCadastralCost);
		}

		[Test]
		public void Can_Calculate_Price_By_Mult_Model_Of_Several_Units_With_One_Factor_Of_None_MarkType()
		{
			//формула: свободный член * [(значение_фактора + поправка)^коэффициент]

			var fistUnitValue = 2;
			var secondUnitValue = 3;
			var firstUnit = new UnitBuilder().Task(Task).Group(Group.Id).Type(PropertyTypes.Building).Build();
			var secondUnit = new UnitBuilder().Task(Task).Group(Group.Id).Type(PropertyTypes.Building).Build();
			AddUnitFactor(Tour2018OksRegister, firstUnit.Id, Tour2018OksFirstIntegerFactor, fistUnitValue);
			AddUnitFactor(Tour2018OksRegister, secondUnit.Id, Tour2018OksFirstIntegerFactor, secondUnitValue);
			var factor = CreateFactorWithoutMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel);

			//TODO изменить параметры конфига в тесте, либо замокать метод в процессе
			var errors = PerformCalculation(Task.Id, Group.Id);


			var firstCalculatedUnit = GetUnitById(firstUnit.Id);
			var secondCalculatedUnit = GetUnitById(secondUnit.Id);
			var firstUnitExpectedCadastralCost = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCostForNoneMark(factor, fistUnitValue);
			var secondUnitExpectedCadastralCost = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCostForNoneMark(factor, secondUnitValue);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			Assert.That(firstCalculatedUnit.CadastralCost, Is.EqualTo(firstUnitExpectedCadastralCost).Within(0.01));
			Assert.That(secondCalculatedUnit.CadastralCost, Is.EqualTo(secondUnitExpectedCadastralCost).Within(0.01));
		}


		#region Support Methods

		protected decimal GetExpectedCostForNoneMark(OMModelFactor factor, decimal unitFactorValue)
		{
			return (decimal)Math.Pow((double)(unitFactorValue + factor.WeightInFormula), (double)factor.B0InFormula);
		}

		private decimal GetExpectedCadastralConstForDefaultMark(OMMarkCatalog mark, OMModelFactor factor)
		{
			return (decimal) Math.Pow(
				       (double) (mark.MetkaFactor.GetValueOrDefault() + factor.WeightInFormula),
				       (double) factor.B0InFormula);
		}

		private decimal GetExpectedCadastralCostForStraightType(OMModelFactor factor)
		{
			var formulaPart = (factor.CorrectingTermInFormula + UnitFactorValueForIntegerFactor) / factor.KInFormula + factor.WeightInFormula;
			
			return (decimal)Math.Pow((double)formulaPart, (double)factor.B0InFormula);
		}

		private decimal GetExpectedCadastralCostForReverseMark(OMModelFactor factor)
		{
			var formulaPart = factor.KInFormula / (factor.CorrectingTermInFormula + UnitFactorValueForIntegerFactor) + factor.WeightInFormula;
			
			return (decimal)Math.Pow((double)formulaPart, (double)factor.B0InFormula);
		}

		#endregion
	}
}
