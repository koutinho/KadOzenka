using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
	public class BaseCadastralPriceCalculationTests : BaseTaskTests
	{
		//забиваем маленькими константами, чтобы можно было легко посчитать/починить тест
		protected const int UnitFactorValueForIntegerFactor = 1;
		private const double FactorCorrection = 2.1;
		private const double FactorCoefficient = 3.2;
		private const double FactorK = 4.2;
		private const double FactorCorrectionTerm = 5.2;

		protected OMTask Task { get; set; }
		protected OMUnit Unit { get; set; }
		protected OMGroup Group { get; set; }
		protected RegisterData Tour2018OksRegister { get; set; }
		protected RegisterAttribute Tour2018OksFirstIntegerFactor { get; set; }
		protected RegisterAttribute Tour2018OksSecondIntegerFactor { get; set; }
		protected RegisterAttribute Tour2018OksThirdIntegerFactor { get; set; }
		protected RegisterAttribute Tour2018OksFourthIntegerFactor { get; set; }


		[OneTimeSetUp]
		public void CadastralPriceCalculationOneTimeSetUp()
		{
			Tour2018OksRegister = RegisterCache.GetRegisterData(250);

			var possibleFactors = Get2018TourFactorAttributes(RegisterAttributeType.INTEGER, 4);

			Tour2018OksFirstIntegerFactor = possibleFactors[0];
			Tour2018OksSecondIntegerFactor = possibleFactors[1];
			Tour2018OksThirdIntegerFactor = possibleFactors[2];
			Tour2018OksFourthIntegerFactor = possibleFactors[3];
		}


		[SetUp]
		public void CadastralPriceCalculationSetUp()
		{
			Task = new TaskBuilder().Tour2018().Document(FirstDocument).Build();
			Group = new GroupBuilder().Parent(Oks2018ParentGroup).Build();
			new TourGroupBuilder().Group(Group.Id).Tour(Task.TourId.GetValueOrDefault()).Build();
			Unit = new UnitBuilder().Task(Task).Group(Group.Id).CadastralCost(0).Upks(0).Type(PropertyTypes.Building).Build();
			var factors = new List<RegisterAttribute>
			{
				Tour2018OksFirstIntegerFactor, Tour2018OksSecondIntegerFactor,
				Tour2018OksThirdIntegerFactor, Tour2018OksFourthIntegerFactor
			};
			var unitValues = Enumerable.Repeat((object)UnitFactorValueForIntegerFactor, factors.Count).ToList();
			AddUnitFactor(Tour2018OksRegister, Unit.Id, factors, unitValues);
		}



		protected OMModelFactor CreateFactorWithReverseMark(RegisterAttribute attribute, OMModel model)
		{
			return new ModelFactorBuilder().FactorId(attribute.Id).Model(model)
				.MarkType(MarkType.Reverse)
				.CorrectingTerm(FactorCorrectionTerm).K(FactorK)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient).Build();
		}

		protected OMModelFactor CreateFactorWithStraightMark(RegisterAttribute attribute, OMModel model)
		{
			return new ModelFactorBuilder().FactorId(attribute.Id).Model(model)
				.MarkType(MarkType.Straight)
				.CorrectingTerm(FactorCorrectionTerm).K(FactorK)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient).Build();
		}

		protected OMModelFactor CreateFactorWithDefaultMark(RegisterAttribute attribute, OMModel model, decimal value,
			out OMMarkCatalog mark)
		{
			var factor = new ModelFactorBuilder().FactorId(attribute.Id).Model(model)
				.MarkType(MarkType.Default)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient).Build();

			mark = new MarkBuilder().Factor(factor.FactorId).Group(model.GroupId)
				.Value(value.ToString()).Metka(1)
				.Build();

			return factor;
		}

		protected OMModelFactor CreateFactorWithoutMark(RegisterAttribute attribute, OMModel model)
		{
			return new ModelFactorBuilder().FactorId(attribute.Id).Model(model)
				.MarkType(MarkType.None)
				.Correction(FactorCorrection).Coefficient(FactorCoefficient)
				.Build();
		}

		protected List<CalcErrorItem> PerformCalculation(long taskId, params long[] groupIds)
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


		protected void CheckCalculatedUnit(long unitId, decimal expectedCadastralCost)
		{
			var unitWithCalculatedPrice = GetUnitById(unitId);

			var expectedUpks = unitWithCalculatedPrice.CadastralCost / unitWithCalculatedPrice.Square.GetValueOrDefault();

			Assert.That(unitWithCalculatedPrice.CadastralCost, Is.EqualTo(expectedCadastralCost).Within(0.01));
			Assert.That(unitWithCalculatedPrice.Upks, Is.EqualTo(expectedUpks).Within(0.01));
		}

		protected OMUnit GetUnitById(long unitId)
		{
			return OMUnit.Where(x => x.Id == unitId)
				.Select(x => new { x.CadastralCost, x.Upks, x.Square })
				.ExecuteFirstOrDefault();
		}

		protected List<RegisterAttribute> Get2018TourFactorAttributes(RegisterAttributeType type, int count)
		{
			return RegisterCache.RegisterAttributes.Where(x =>
				x.Value.RegisterId == Tour2018OksRegister.Id && x.Value.Type == type &&
				!x.Value.IsPrimaryKey).Take(count).Select(x => x.Value).ToList();
		}
	}
}
