using System.Collections.Generic;
using System.Linq;
using Core.Register.RegisterEntities;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.Integration._Builders;
using KadOzenka.Dal.Integration._Builders.Model;
using KadOzenka.Dal.Integration._Builders.Task;
using KadOzenka.Dal.LongProcess._Common;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Task.CadastralPrice
{
	public class ValidationTests : BaseCadastralPriceCalculationTests
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
		public void If_Price_Is_Not_A_Number_Save_It_As_Error()
		{
			new ModelFactorBuilder().FactorId(Tour2018OksFirstIntegerFactor.Id).Model(MultiplicativeModel)
				.MarkType(MarkType.None)
				.Correction(RandomGenerator.GenerateRandomDecimal())
				.Coefficient(decimal.MaxValue, MultiplicativeModel.AlgoritmType_Code)
				.Build();

			var errors = PerformCalculation(Task.Id, Group.Id);

			CheckError(errors, Unit, Messages.CadastralPriceCalculationError);
		}

		[Test]
		public void If_Price_Is_Zero_Save_It_As_Error()
		{
			// формула: свободный член * [(значение_фактора + поправка) ^ коэффициент]

			new ModelFactorBuilder().FactorId(Tour2018OksFirstIntegerFactor.Id).Model(MultiplicativeModel)
				.MarkType(MarkType.None)
				.Correction((double)-UnitFactorValueForIntegerFactor)
				.Coefficient((double)RandomGenerator.GenerateRandomInteger( maxNumber:3), MultiplicativeModel.AlgoritmType_Code)
				.Build();

			var errors = PerformCalculation(Task.Id, Group.Id);

			CheckError(errors, Unit, Messages.ZeroCadastralPrice);
		}

		[Test]
		public void If_Group_Has_No_Active_Model_Save_This_Units_As_Error_And_Calculate_Unit_With_Active_Group()
		{
			var groupWithoutActiveModel = new GroupBuilder().Parent(Oks2018ParentGroup).Build();
			var unitWithoutActiveGroup = new UnitBuilder().Task(Task).Group(groupWithoutActiveModel.Id).CadastralCost(0).Upks(0).Type(PropertyTypes.Building).Build();
			CreateFactorWithoutMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel);

			var errors = PerformCalculation(Task.Id, groupWithoutActiveModel.Id, Group.Id);

			var calculatedUnit = GetUnitById(Unit.Id);
			Assert.That(calculatedUnit.CadastralCost, Is.Not.Zero);
			CheckError(errors, unitWithoutActiveGroup, Messages.NoActiveModelInCadasralPriceCalculation);
		}

		[Test]
		public void If_Unit_Has_No_Factors_From_Formula_Save_It_As_Error()
		{
			var unitWithoutFactors = new UnitBuilder().Task(Task).Group(Group.Id).CadastralCost(0).Upks(0).Type(PropertyTypes.Building).Build();
			CreateFactorWithoutMark(Tour2018OksFirstIntegerFactor, MultiplicativeModel);

			var errors = PerformCalculation(Task.Id, Group.Id);

			CheckError(errors, unitWithoutFactors, Messages.UnitDoesNotHaveFactorsToCalculateCandastralPrice);
		}

		[Test]
		public void If_Unit_Has_Not_All_Factors_From_Formula_Save_It_As_Error()
		{
			var unitWithoutAllFactors = new UnitBuilder().Task(Task).Group(Group.Id).CadastralCost(0).Upks(0).Type(PropertyTypes.Building).Build();
			var existedUnitFactor = Tour2018OksFirstIntegerFactor;
			CreateFactorWithoutMark(existedUnitFactor, MultiplicativeModel);
			CreateFactorWithStraightMark(Tour2018OksSecondIntegerFactor, MultiplicativeModel);
			AddUnitFactor(Tour2018OksRegister, unitWithoutAllFactors.Id, existedUnitFactor, RandomGenerator.GenerateRandomInteger());

			var errors = PerformCalculation(Task.Id, Group.Id);

			CheckError(errors, unitWithoutAllFactors, Messages.NotAllUnitFactorsAreFullToCalculateCadastralPrice);
		}


		#region Support Methods

		private void CheckError(List<CalcErrorItem> errors, OMUnit unit, string message)
		{
			Assert.That(errors.Count, Is.EqualTo(1));

			var error = errors.FirstOrDefault();
			StringAssert.Contains(message, error.Error, error.Error);
			StringAssert.Contains(unit.CadastralNumber, error.CadastralNumber);
		}

		#endregion
	}
}
