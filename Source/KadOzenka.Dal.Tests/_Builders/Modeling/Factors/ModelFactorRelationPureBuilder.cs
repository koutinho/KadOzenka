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
				MarkType = ObjectModel.Directory.Ko.MarkType.Default,
				DictionaryId = RandomGenerator.GenerateRandomId(),
				DictionaryName = RandomGenerator.GetRandomString(),
				IsActive = true
			};
		}


		public ModelFactorRelationPureBuilder AttributeId(long attributeId)
		{
			_factor.AttributeId = attributeId;
			return this;
		}

		public ModelFactorRelationPureBuilder DictionaryId(long? dictionaryId)
		{
			_factor.DictionaryId = dictionaryId;
			return this;
		}

		public ModelFactorRelationPureBuilder MarkType(MarkType markType)
		{
			_factor.MarkType = markType;
			return this;
		}

		public ModelFactorRelationPure Build()
		{
			return _factor;
		}
	}
}
