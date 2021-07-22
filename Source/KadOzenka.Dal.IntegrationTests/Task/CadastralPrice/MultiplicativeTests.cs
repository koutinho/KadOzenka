using System;
using System.Linq;
using Core.Register;
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
			var expectedUpks = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCostForNoneMark(factor, UnitFactorValueForIntegerFactor);
			CheckCalculatedUnit(Unit.Id, expectedUpks);
		}

		[Test]
		public void Can_Calculate_Price_By_Mult_Model_With_One_Factor_Of_Default_MarkType()
		{
			//формула: свободный член * [(метка(значение_фактора) + поправка)^коэффициент]

			var factor = CreateFactorWithDefaultMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel, UnitFactorValueForIntegerFactor, out var mark);

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedUpks = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCadastralConstForDefaultMark(mark, factor);
			CheckCalculatedUnit(Unit.Id, expectedUpks);
		}

		[Test]
		public void Can_Calculate_Price_By_Mult_Model_With_One_Factor_Of_Default_MarkType_With_Two_Marks_With_Different_Values()
		{
			//формула: свободный член * [(метка(значение_фактора) + поправка)^коэффициент]

			var factor = CreateFactorWithDefaultMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel, UnitFactorValueForIntegerFactor, out var mark);
			new MarkBuilder().Dictionary(factor.DictionaryId)
				.Value(RandomGenerator.GetRandomString()).Metka(RandomGenerator.GenerateRandomDecimal())
				.Build();

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedUpks = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCadastralConstForDefaultMark(mark, factor);
			CheckCalculatedUnit(Unit.Id, expectedUpks);
		}

		[Test]
		public void Can_Calculate_Price_By_Mult_Model_With_One_Factor_Of_Straight_MarkType()
		{
			//формула: свободный член * [(корректирующее_слагаемое + значение_фактора)/К + поправка]^коэффициент

			var factor = CreateFactorWithStraightMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel);

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedUpks = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCadastralCostForStraightType(factor);
			CheckCalculatedUnit(Unit.Id, expectedUpks);
		}

		[Test]
		public void Can_Calculate_Price_By_Mult_Model_With_One_Factor_Of_Reverse_MarkType()
		{
			//формула: свободный член * [К/(корректирующее_слагаемое + значение_фактора) + поправка]^коэффициент

			var factor = CreateFactorWithReverseMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel);

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			var expectedUpks = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCadastralCostForReverseMark(factor);
			CheckCalculatedUnit(Unit.Id, expectedUpks);
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
			var expectedUpks = MultiplicativeModel.A0ForMultiplicativeInFormula * 
			                            GetExpectedCostForNoneMark(factorWithoutMark, UnitFactorValueForIntegerFactor) *
			                            GetExpectedCadastralConstForDefaultMark(mark, factorWithDefaultMark) *
			                            GetExpectedCadastralCostForStraightType(factorWithStraightMark) *
			                            GetExpectedCadastralCostForReverseMark(factorWithReverseMark);
			
			CheckCalculatedUnit(Unit.Id, expectedUpks);
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
			var firstUnitExpectedUpks = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCostForNoneMark(factor, fistUnitValue);
			var secondUnitExpectedUpks = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCostForNoneMark(factor, secondUnitValue);

			Assert.That(errors.Count, Is.EqualTo(0), string.Join(Environment.NewLine, errors.Select(x => x.Error)));
			Assert.That(firstCalculatedUnit.Upks, Is.EqualTo(firstUnitExpectedUpks).Within(0.01));
			Assert.That(secondCalculatedUnit.Upks, Is.EqualTo(secondUnitExpectedUpks).Within(0.01));
		}

		[Test]
		public void Can_Calculate_Price_By_Mult_Model_If_Unit_Factor_Is_Decimal_Type()
		{
			//формула: свободный член * [(значение_фактора + поправка)^коэффициент]

			var fistUnitValue = 2.2m;
			var firstUnit = new UnitBuilder().Task(Task).Group(Group.Id).Type(PropertyTypes.Building).Build();
			var tour2018OksDecimalFactor = Get2018TourFactorAttributes(RegisterAttributeType.DECIMAL, 1).First();
			AddUnitFactor(Tour2018OksRegister, firstUnit.Id, tour2018OksDecimalFactor, fistUnitValue);
			var factor = CreateFactorWithoutMark(tour2018OksDecimalFactor, MultiplicativeModel);


			PerformCalculation(Task.Id, Group.Id);


			var calculatedUnit = GetUnitById(firstUnit.Id);
			var expectedUpks = MultiplicativeModel.A0ForMultiplicativeInFormula * GetExpectedCostForNoneMark(factor, fistUnitValue);
			Assert.That(calculatedUnit.Upks, Is.EqualTo(expectedUpks).Within(0.01));
		}



		#region Support Methods

		protected decimal GetExpectedCostForNoneMark(OMModelFactor factor, decimal unitFactorValue)
		{
			return (decimal)Math.Pow((double)(unitFactorValue + factor.WeightInFormula), (double)factor.B0InFormula);
		}

		private decimal GetExpectedCadastralConstForDefaultMark(OMModelingDictionariesValues mark, OMModelFactor factor)
		{
			return (decimal) Math.Pow(
				       (double) (mark.CalculationValue + factor.WeightInFormula),
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
