using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Register;
using Core.Register.RegisterEntities;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.Integration._Builders;
using KadOzenka.Dal.Integration._Builders.Model;
using KadOzenka.Dal.Integration._Builders.Task;
using KadOzenka.Dal.LongProcess._Common;
using KadOzenka.Dal.LongProcess.TaskLongProcesses;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Task.CadastralPrice
{
	public class MultiplicativeTests : BaseTaskTests
	{
		//забиваем маленькими константами, чтобы можно было легко посчитать/починить тест
		private const int UnitFactorValue = 1;
		private const double FactorCorrection = 2.1;
		private const double FactorCoefficient = 3.2;
		private const double FactorK = 4.2;
		private const double FactorCorrectionTerm = 5.2;

		private RegisterData Tour2018OksRegister { get; set; }
		private RegisterAttribute Tour2018OksFirstIntegerFactor { get; set; }
		private RegisterAttribute Tour2018OksSecondIntegerFactor { get; set; }
		private RegisterAttribute Tour2018OksThirdIntegerFactor { get; set; }
		private RegisterAttribute Tour2018OksFourthIntegerFactor { get; set; }
		private OMTask Task { get; set; }
		private OMUnit Unit { get; set; }
		private OMGroup Group { get; set; }
		private OMModel Model { get; set; }


		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			Tour2018OksRegister = RegisterCache.GetRegisterData(250);
			
			var possibleFactors = RegisterCache.RegisterAttributes.Where(x =>
				x.Value.RegisterId == Tour2018OksRegister.Id && x.Value.Type == RegisterAttributeType.INTEGER &&
				!x.Value.IsPrimaryKey).Take(4).Select(x => x.Value).ToList();

			Tour2018OksFirstIntegerFactor = possibleFactors[0];
			Tour2018OksSecondIntegerFactor = possibleFactors[1];
			Tour2018OksThirdIntegerFactor = possibleFactors[2];
			Tour2018OksFourthIntegerFactor = possibleFactors[3];
		}

		[SetUp]
		public void SetUp()
		{
			var modelA0 = 2;
			Task = new TaskBuilder().Tour2018().Document(FirstDocument).Build();
			Group = new GroupBuilder().Build();
			new TourGroupBuilder().Group(Group.Id).Tour(Task.TourId.GetValueOrDefault()).Build();
			Model = new ModelBuilder().Group(Group.Id).AlgorithmType(KoAlgoritmType.Multi).IsActive(true).A0(modelA0).Build();
			Unit = new UnitBuilder().Task(Task).Group(Group.Id).CadastralCost(0).Upks(0).Type(PropertyTypes.Building).Build();

			var factors = new List<RegisterAttribute>
			{
				Tour2018OksFirstIntegerFactor, Tour2018OksSecondIntegerFactor, 
				Tour2018OksThirdIntegerFactor, Tour2018OksFourthIntegerFactor
			};
			var unitValues = Enumerable.Repeat((object)UnitFactorValue, factors.Count).ToList();
			AddUnitFactor(Tour2018OksRegister, Unit.Id, factors, unitValues);
		}



		[Test]
		public void Can_Calculate_Price_With_One_Factor_Of_None_MarkType()
		{
			//формула: свободный член * [(значение_фактора + поправка)^коэффициент]

			var factor = CreateFactorWithoutMark();

			var errors = PerformCalculation(Task.Id, Group.Id);

			var expectedCadastralCost = Model.A0ForMultiplicativeInFormula * GetExpectedConstForNoneMark(factor);
			CheckCalculatedUnit(expectedCadastralCost);
			Assert.That(errors.Count, Is.EqualTo(0));
		}

		[Test]
		public void Can_Calculate_Price_With_One_Factor_Of_Default_MarkType()
		{
			//формула: свободный член * [(метка(значение_фактора) + поправка)^коэффициент]

			var factor = CreateFactorWithDefaultMark(out var mark);

			var errors = PerformCalculation(Task.Id, Group.Id);

			var expectedCadastralCost = Model.A0ForMultiplicativeInFormula * GetExpectedCadastralConstForDefaulMark(mark, factor);
			CheckCalculatedUnit(expectedCadastralCost);
			Assert.That(errors.Count, Is.EqualTo(0));
		}

		[Test]
		public void Can_Calculate_Price_With_One_Factor_Of_Default_MarkType_With_Two_Marks_With_Different_Values()
		{
			//формула: свободный член * [(метка(значение_фактора) + поправка)^коэффициент]

			var factor = CreateFactorWithDefaultMark(out var mark);
			new MarkBuilder().Factor(factor.FactorId).Group(Group.Id)
				.Value(RandomGenerator.GetRandomString()).Metka(RandomGenerator.GenerateRandomDecimal())
				.Build();

			var errors = PerformCalculation(Task.Id, Group.Id);

			var expectedCadastralCost = Model.A0ForMultiplicativeInFormula * GetExpectedCadastralConstForDefaulMark(mark, factor);
			CheckCalculatedUnit(expectedCadastralCost);
			Assert.That(errors.Count, Is.EqualTo(0));
		}

		[Test]
		public void Can_Calculate_Price_With_One_Factor_Of_Default_MarkType_With_Two_Marks_With_TheSame_Values()
		{
			//формула: свободный член * [(метка(значение_фактора) + поправка)^коэффициент]

			var factor = CreateFactorWithDefaultMark(out var firstMark);
			var secondMark = new MarkBuilder().Factor(factor.FactorId).Group(Group.Id)
				.Value(firstMark.ValueFactor)
				//чтобы случайно не создать такое же значение, как в mark
				.Metka(RandomGenerator.GenerateRandomInteger(maxNumber: 3) + firstMark.MetkaFactor.GetValueOrDefault())
				.Build();

			var errors = PerformCalculation(Task.Id, Group.Id);

			//В БД может быть несколько меток с одной и той же группой/фактором/значением
			//когда выясниться, что таких значений быть не должно, тест можно удалить
			var firstPossibleExpectedCadastralCost = Model.A0ForMultiplicativeInFormula * GetExpectedCadastralConstForDefaulMark(firstMark, factor);
			var secondPossibleExpectedCadastralCost = Model.A0ForMultiplicativeInFormula * GetExpectedCadastralConstForDefaulMark(secondMark, factor);
			
			var unitWithCalculatedPrice = GetUnitById(Unit.Id);
			Assert.That(unitWithCalculatedPrice.CadastralCost,
				Is.EqualTo(firstPossibleExpectedCadastralCost).Within(0.01)
				.Or.EqualTo(secondPossibleExpectedCadastralCost).Within(0.01));

			var expectedUpks = unitWithCalculatedPrice.CadastralCost / Unit.Square.GetValueOrDefault();
			Assert.That(unitWithCalculatedPrice.Upks, Is.EqualTo(expectedUpks).Within(0.01));
			Assert.That(errors.Count, Is.EqualTo(0));
		}

		[Test]
		public void Can_Calculate_Price_With_One_Factor_Of_Straight_MarkType()
		{
			//формула: свободный член * [(корректирующее_слагаемое + значение_фактора)/К + поправка]^коэффициент

			var factor = CreateFactorWithStraightMark();

			var errors = PerformCalculation(Task.Id, Group.Id);

			var expectedCadastralCost = Model.A0ForMultiplicativeInFormula * GetExpectedCadastralCostForStraightType(factor);
			CheckCalculatedUnit(expectedCadastralCost);
			Assert.That(errors.Count, Is.EqualTo(0));
		}

		[Test]
		public void Can_Calculate_Price_With_One_Factor_Of_Reverse_MarkType()
		{
			//формула: свободный член * [К/(корректирующее_слагаемое + значение_фактора) + поправка]^коэффициент

			var factor = CreateFactorWithReverseMark();

			var errors = PerformCalculation(Task.Id, Group.Id);

			var expectedCadastralCost = Model.A0ForMultiplicativeInFormula * GetExpectedCadastralCostForReverseMark(factor);
			CheckCalculatedUnit(expectedCadastralCost);
			Assert.That(errors.Count, Is.EqualTo(0));
		}


		[Test]
		public void Can_Calculate_Price_With_All_Possible_Factors_MarkType()
		{
			var factorWithoutMark = CreateFactorWithoutMark();
			var factorWithDefaultMark = CreateFactorWithDefaultMark(out var mark);
			var factorWithStraightMark = CreateFactorWithStraightMark();
			var factorWithReverseMark = CreateFactorWithReverseMark();

			var errors = PerformCalculation(Task.Id, Group.Id);

			var expectedCadastralCost = Model.A0ForMultiplicativeInFormula * 
			                            GetExpectedConstForNoneMark(factorWithoutMark) *
			                            GetExpectedCadastralConstForDefaulMark(mark, factorWithDefaultMark) *
			                            GetExpectedCadastralCostForStraightType(factorWithStraightMark) *
			                            GetExpectedCadastralCostForReverseMark(factorWithReverseMark);
			
			CheckCalculatedUnit(expectedCadastralCost);
			Assert.That(errors.Count, Is.EqualTo(0));
		}

		[Test]
		public void Can_Calculate_Price_Of_Several_Units_With_One_Factor_Of_None_MarkType()
		{
			//формула: свободный член * [(значение_фактора + поправка)^коэффициент]

			var fistUnitValue = 2;
			var secondUnitValue = 3;
			var firstUnit = new UnitBuilder().Task(Task).Group(Group.Id).Type(PropertyTypes.Building).Build();
			var secondUnit = new UnitBuilder().Task(Task).Group(Group.Id).Type(PropertyTypes.Building).Build();
			AddUnitFactor(Tour2018OksRegister, firstUnit.Id, Tour2018OksFourthIntegerFactor, fistUnitValue);
			AddUnitFactor(Tour2018OksRegister, secondUnit.Id, Tour2018OksFourthIntegerFactor, secondUnitValue);
			var factor = CreateFactorWithoutMark();

			//TODO изменить параметры конфига в тесте, либо замокать метод в процессе
			var errors = PerformCalculation(Task.Id, Group.Id);


			var firstCalculatedUnit = GetUnitById(firstUnit.Id);
			var secondCalculatedUnit = GetUnitById(secondUnit.Id);
			var firstUnitExpectedCadastralCost = Model.A0ForMultiplicativeInFormula * GetExpectedConstForNoneMark(factor, fistUnitValue);
			var secondUnitExpectedCadastralCost = Model.A0ForMultiplicativeInFormula * GetExpectedConstForNoneMark(factor, secondUnitValue);

			Assert.That(firstCalculatedUnit.CadastralCost, Is.EqualTo(firstUnitExpectedCadastralCost).Within(0.01));
			Assert.That(secondCalculatedUnit.CadastralCost, Is.EqualTo(secondUnitExpectedCadastralCost).Within(0.01));
			Assert.That(errors.Count, Is.EqualTo(0));
		}

		[Test]
		public void If_Price_Is_Not_A_Number_Save_It_As_Error()
		{
			new ModelFactorBuilder().FactorId(Tour2018OksFourthIntegerFactor.Id).Model(Model)
				.MarkType(MarkType.None)
				.Correction(RandomGenerator.GenerateRandomDecimal()).Coefficient(decimal.MaxValue)
				.Build();

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(1));
			StringAssert.Contains(Messages.CadastralPriceCalculationError, errors.First().Error);
		}

		[Test]
		public void If_Price_Is_Zero_Save_It_As_Error()
		{
			// формула: свободный член * [(значение_фактора + поправка) ^ коэффициент]

			new ModelFactorBuilder().FactorId(Tour2018OksFourthIntegerFactor.Id).Model(Model)
				.MarkType(MarkType.None)
				.Correction((double)-UnitFactorValue).Coefficient(FactorCoefficient)
				.Build();

			var errors = PerformCalculation(Task.Id, Group.Id);

			Assert.That(errors.Count, Is.EqualTo(1));
			StringAssert.Contains(Messages.ZeroCadastralPrice, errors.First().Error);
		}

		[Test]
		public void If_Group_Has_No_Active_Model_Save_This_Units_As_Error_And_Calculate_Unit_With_Active_Group()
		{
			var group = new GroupBuilder().Build();
			var unit = new UnitBuilder().Task(Task).Group(group.Id).CadastralCost(0).Upks(0).Type(PropertyTypes.Building).Build();
			new TourGroupBuilder().Group(group.Id).Tour(Task.TourId.GetValueOrDefault()).Build();
			var factor = CreateFactorWithoutMark();

			
			var errors = PerformCalculation(Task.Id, group.Id, Group.Id);


			Assert.That(errors.Count, Is.EqualTo(1));
			var error = errors.FirstOrDefault();
			StringAssert.Contains(Messages.NoActiveModelInCadasralPriceCalculation, error.Error);
			StringAssert.Contains(unit.CadastralNumber, error.CadastralNumber);

			var expectedCadastralCost = Model.A0ForMultiplicativeInFormula * GetExpectedConstForNoneMark(factor);
			CheckCalculatedUnit(expectedCadastralCost);
		}


		#region Support Methods

		private OMModelFactor CreateFactorWithReverseMark()
		{
			return new ModelFactorBuilder().FactorId(Tour2018OksFirstIntegerFactor.Id).Model(Model)
				.MarkType(MarkType.Reverse)
				.CorrectingTerm(FactorCorrectionTerm).K(FactorK)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient).Build();
		}

		private OMModelFactor CreateFactorWithStraightMark()
		{
			return new ModelFactorBuilder().FactorId(Tour2018OksSecondIntegerFactor.Id).Model(Model)
				.MarkType(MarkType.Straight)
				.CorrectingTerm(FactorCorrectionTerm).K(FactorK)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient).Build();
		}

		private OMModelFactor CreateFactorWithDefaultMark(out OMMarkCatalog mark)
		{
			var factor = new ModelFactorBuilder().FactorId(Tour2018OksThirdIntegerFactor.Id).Model(Model)
				.MarkType(MarkType.Default)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient).Build();
			
			mark = new MarkBuilder().Factor(factor.FactorId).Group(Group.Id)
				.Value(UnitFactorValue.ToString()).Metka(1)
				.Build();

			return factor;
		}

		private OMModelFactor CreateFactorWithoutMark()
		{
			return new ModelFactorBuilder().FactorId(Tour2018OksFourthIntegerFactor.Id).Model(Model)
				.MarkType(MarkType.None)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient)
				.Build();
		}

		private decimal GetExpectedConstForNoneMark(OMModelFactor factor, int? unitFactorValue = null)
		{
			var factorValue = unitFactorValue ?? UnitFactorValue;
			
			return (decimal)Math.Pow((double)(factorValue + factor.WeightInFormula), (double)factor.B0);
		}

		private decimal GetExpectedCadastralConstForDefaulMark(OMMarkCatalog mark, OMModelFactor factor)
		{
			return (decimal) Math.Pow(
				       (double) (mark.MetkaFactor.GetValueOrDefault() + factor.WeightInFormula),
				       (double) factor.B0InFormula);
		}

		private decimal GetExpectedCadastralCostForStraightType(OMModelFactor factor)
		{
			var formulaPart = (factor.CorrectingTermInFormula + UnitFactorValue) / factor.K + factor.WeightInFormula;
			
			return (decimal)Math.Pow((double)formulaPart, (double)factor.B0InFormula);
		}

		private decimal GetExpectedCadastralCostForReverseMark(OMModelFactor factor)
		{
			var formulaPart = factor.KInFormula / (factor.CorrectingTermInFormula + UnitFactorValue) + factor.WeightInFormula;
			
			return (decimal)Math.Pow((double)formulaPart, (double)factor.B0InFormula);
		}

		private List<CalcErrorItem> PerformCalculation(long taskId, params long[] groupIds)
		{
			var settings = new CadastralPriceCalculationSettions
			{
				IsParcel = false,
				SelectedGroupIds = groupIds.ToList(),
				TaskIds = new List<long> { taskId },
				TourId = 2018
			};

			return new CalculateCadastralPriceLongProcess().DoCalculation(settings, new CancellationToken());
		}

		private void CheckCalculatedUnit(decimal expectedCadastralCost)
		{
			var unitWithCalculatedPrice = GetUnitById(Unit.Id);
			
			var expectedUpks = unitWithCalculatedPrice.CadastralCost / Unit.Square.GetValueOrDefault();

			Assert.That(unitWithCalculatedPrice.CadastralCost, Is.EqualTo(expectedCadastralCost).Within(0.01));
			Assert.That(unitWithCalculatedPrice.Upks, Is.EqualTo(expectedUpks).Within(0.01));
		}

		private OMUnit GetUnitById(long unitId)
		{
			return OMUnit.Where(x => x.Id == unitId)
				.Select(x => new { x.CadastralCost, x.Upks })
				.ExecuteFirstOrDefault();
		}

		#endregion
	}
}
