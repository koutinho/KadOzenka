using Core.Register;
using KadOzenka.Common.Tests;
using ModelingBusiness.Factors.Entities;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.UnitTests._Builders.Modeling.Factors
{
	public class ModelFactorRelationDtoBuilder
	{
		private ModelFactorRelation _factor;

		public ModelFactorRelationDtoBuilder()
		{
			_factor = new ModelFactorRelation
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


		public ModelFactorRelationDtoBuilder AttributeId(long attributeId)
		{
			_factor.AttributeId = attributeId;
			return this;
		}

		public ModelFactorRelationDtoBuilder DictionaryId(long? dictionaryId)
		{
			_factor.DictionaryId = dictionaryId;
			return this;
		}

		public ModelFactorRelationDtoBuilder MarkType(MarkType markType)
		{
			_factor.MarkType = markType;
			return this;
		}

		public ModelFactorRelationDtoBuilder Active(bool isActive)
		{
			_factor.IsActive = isActive;
			return this;
		}

		public ModelFactorRelation Build()
		{
			return _factor;
		}
	}
}
