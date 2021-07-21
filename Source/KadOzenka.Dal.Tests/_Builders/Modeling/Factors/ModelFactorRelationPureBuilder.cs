using Core.Register;
using KadOzenka.Common.Tests;
using ModelingBusiness.Factors.Entities;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling.Factors
{
	public class ModelFactorRelationPureBuilder
	{
		private ModelFactorRelationPure _factor;

		public ModelFactorRelationPureBuilder()
		{
			_factor = new ModelFactorRelationPure
			{
				Id = RandomGenerator.GenerateRandomId(),
				RegisterId = RandomGenerator.GenerateRandomId(),
				AttributeId = RandomGenerator.GenerateRandomId(),
				AttributeName = RandomGenerator.GetRandomString(),
				AttributeType = (long) RegisterAttributeType.STRING,
				MarkType = MarkType.Default,
				DictionaryId = RandomGenerator.GenerateRandomId(),
				DictionaryName = RandomGenerator.GetRandomString(),
				IsActive = true
			};
		}


		public ModelFactorRelationPure Build()
		{
			return _factor;
		}
	}
}
