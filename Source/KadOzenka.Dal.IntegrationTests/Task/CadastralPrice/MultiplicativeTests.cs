using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.Integration._Builders;
using KadOzenka.Dal.Integration._Builders.Model;
using KadOzenka.Dal.Integration._Builders.Task;
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
		private const int ModelA0 = 2;
		
		private RegisterData Tour2018OksRegister { get; set; }
		private RegisterAttribute Tour2018OksIntegerFactor { get; set; }
		private OMTask Task { get; set; }
		private OMUnit Unit { get; set; }
		private OMGroup Group { get; set; }
		private OMModel Model { get; set; }


		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			Tour2018OksRegister = RegisterCache.GetRegisterData(250);
			
			Tour2018OksIntegerFactor = RegisterCache.RegisterAttributes.First(x =>
				x.Value.RegisterId == Tour2018OksRegister.Id && x.Value.Type == RegisterAttributeType.INTEGER &&
				!x.Value.IsPrimaryKey).Value;
		}

		[SetUp]
		public void SetUp()
		{
			Task = new TaskBuilder().Tour2018().Build();
			Group = new GroupBuilder().Build();
			Model = new ModelBuilder().Group(Group.Id).AlgorithmType(KoAlgoritmType.Multi).IsActive(true).A0(ModelA0).Build();
			Unit = new UnitBuilder().Task(Task).Group(Group.Id).Type(PropertyTypes.Building).Build();
			AddUnitFactor(Tour2018OksRegister, Tour2018OksIntegerFactor, Unit.Id, UnitFactorValue);
		}



		[Test]
		public void Can_Calculate_Price_With_One_Factor_Of_None_MarkType()
		{
			//формула: свободный член * [(значение_фактора + поправка)^коэффициент]

			var factor = new ModelFactorBuilder().FactorId(Tour2018OksIntegerFactor.Id).Model(Model)
				.MarkType(MarkType.None)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient)
				.Build();

			PerformCalculation(Task.Id, Group.Id);

			var expectedUpks = ModelA0 * (decimal)Math.Pow((double) (UnitFactorValue + factor.WeightInFormula), (double) factor.B0);
			CheckCalculatedUnit(expectedUpks);
		}

		[Test]
		public void Can_Calculate_Price_With_One_Factor_Of_Default_MarkType()
		{
			//формула: свободный член * [(метка(значение_фактора) + поправка)^коэффициент]

			var factor = new ModelFactorBuilder().FactorId(Tour2018OksIntegerFactor.Id).Model(Model)
				.MarkType(MarkType.Default)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient).Build();
			var mark = new MarkBuilder().Factor(factor.FactorId).Group(Group.Id).Value(UnitFactorValue.ToString()).Metka(1).Build();

			PerformCalculation(Task.Id, Group.Id);

			var expectedCadastralCost = ModelA0 * (decimal)Math.Pow((double) (mark.MetkaFactor.GetValueOrDefault() + factor.WeightInFormula), (double)factor.B0InFormula);
			CheckCalculatedUnit(expectedCadastralCost);
		}

		[Test]
		public void Can_Calculate_Price_With_One_Factor_Of_Straight_MarkType()
		{
			//формула: свободный член * [(корректирующее_слагаемое + значение_фактора)/К + поправка]^коэффициент

			var factor = new ModelFactorBuilder().FactorId(Tour2018OksIntegerFactor.Id).Model(Model)
				.MarkType(MarkType.Straight)
				.CorrectingTerm(FactorCorrectionTerm).K(FactorK)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient).Build();

			PerformCalculation(Task.Id, Group.Id);

			var formulaPart = (factor.CorrectingTermInFormula + UnitFactorValue)/ factor.K + factor.WeightInFormula;
			var expectedCadastralCost = ModelA0 * (decimal)Math.Pow((double) formulaPart, (double) factor.B0InFormula);
			CheckCalculatedUnit(expectedCadastralCost);
		}

		[Test]
		public void Can_Calculate_Price_With_One_Factor_Of_Reverse_MarkType()
		{
			//формула: свободный член * [К/(корректирующее_слагаемое + значение_фактора) + поправка]^коэффициент

			var factor = new ModelFactorBuilder().FactorId(Tour2018OksIntegerFactor.Id).Model(Model)
				.MarkType(MarkType.Reverse)
				.CorrectingTerm(FactorCorrectionTerm).K(FactorK)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient).Build();

			PerformCalculation(Task.Id, Group.Id);

			var formulaPart = factor.KInFormula / (factor.CorrectingTermInFormula + UnitFactorValue) + factor.WeightInFormula;
			var expectedCadastralCost = ModelA0 * (decimal)Math.Pow((double)formulaPart, (double)factor.B0InFormula);
			CheckCalculatedUnit(expectedCadastralCost);
		}


		#region Support Methods

		private void PerformCalculation(long taskId, long groupId)
		{
			var settings = new CadastralPriceCalculationSettions
			{
				IsParcel = false,
				SelectedGroupIds = new List<long> { groupId },
				TaskIds = new List<long> { taskId }
			};

			new CalculateCadastralPriceLongProcess().PerformProc(settings);
		}

		private void CheckCalculatedUnit(decimal expectedCadastralCost)
		{
			var unitWithCalculatedPrice = OMUnit.Where(x => x.Id == Unit.Id)
				.Select(x => new { x.CadastralCost, x.Upks })
				.ExecuteFirstOrDefault();
			
			var expectedUpks = unitWithCalculatedPrice.CadastralCost / Unit.Square.GetValueOrDefault();

			Assert.That(unitWithCalculatedPrice.CadastralCost, Is.EqualTo(expectedCadastralCost).Within(0.01));
			Assert.That(unitWithCalculatedPrice.Upks, Is.EqualTo(expectedUpks).Within(0.01));
		}

		#endregion
	}
}
