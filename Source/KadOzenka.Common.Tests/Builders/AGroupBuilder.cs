using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders
{
	public abstract class AGroupBuilder
	{
		protected readonly OMGroup _group;

		protected AGroupBuilder()
		{
			var algorithm = KoGroupAlgoritm.Model;

			_group = new OMGroup
			{
				GroupName = RandomGenerator.GetRandomString(),
				GroupAlgoritm = algorithm.GetEnumDescription(),
				GroupAlgoritm_Code = algorithm,
				ParentId = RandomGenerator.GenerateRandomInteger(),
				Number = RandomGenerator.GenerateRandomInteger().ToString(),
				AppliedApproachesInCadastralCost = RandomGenerator.GetRandomString(),
				AppliedEvaluationMethodsInCadastralCost = RandomGenerator.GetRandomString(),
				CadastralCostDetermingMethod = RandomGenerator.GetRandomString(),
				ModelJustification = RandomGenerator.GetRandomString(),
				ObjectsSegment = RandomGenerator.GetRandomString(),
				ObjectsSubgroup = RandomGenerator.GetRandomString(),
				CadastralCostCalculationOrderDescription = RandomGenerator.GetRandomString(),
				PriceZoneCharacteristic = RandomGenerator.GetRandomString(),
				MarketSegment = RandomGenerator.GetRandomString(),
				MarketSegmentFunctioningFeatures = RandomGenerator.GetRandomString(),
				CadastralCostEstimationModelsReferences = RandomGenerator.GetRandomString(),
				AssumptionsReference = RandomGenerator.GetRandomString(),
				OtherCostRelatedInfo = RandomGenerator.GetRandomString()
			};
		}

		public AGroupBuilder Number(int number)
		{
			_group.Number = number.ToString();
			return this;
		}

		public AGroupBuilder Number(string number)
		{
			_group.Number = number;
			return this;
		}

		public abstract OMGroup Build();
	}
}
