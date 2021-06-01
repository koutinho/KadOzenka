using System;
using KadOzenka.Dal.GbuObject;

namespace KadOzenka.Common.Tests.Builders.GbuObject
{
	public abstract class AGbuObjectAttributeBuilder
	{
		protected readonly GbuObjectAttribute _gbuObjectAttribute;

		protected AGbuObjectAttributeBuilder()
		{
			_gbuObjectAttribute = new GbuObjectAttribute
			{
				Id = -1,
				AttributeId = RandomGenerator.GenerateRandomInteger(),
				ObjectId = RandomGenerator.GenerateRandomInteger(),
				ChangeDocId = RandomGenerator.GenerateRandomInteger(maxNumber: int.MaxValue),
				S = DateTime.Now,
				ChangeUserId = RandomGenerator.GenerateRandomInteger(),
				ChangeDate = DateTime.Now,
				Ot = DateTime.Now,
				StringValue = RandomGenerator.GetRandomString()
			};
		}

		protected AGbuObjectAttributeBuilder(GbuObjectAttribute attribute)
		{
			_gbuObjectAttribute = attribute.ShallowCopy();
		}


		public AGbuObjectAttributeBuilder Attribute(long attributeId)
		{
			_gbuObjectAttribute.AttributeId = attributeId;
			return this;
		}

		public AGbuObjectAttributeBuilder Object(long objectId)
		{
			_gbuObjectAttribute.ObjectId = objectId;
			return this;
		}

		public AGbuObjectAttributeBuilder OtAndSDates(DateTime date)
		{
			_gbuObjectAttribute.Ot = date;
			_gbuObjectAttribute.S = date;
			return this;
		}

		public AGbuObjectAttributeBuilder SDate(DateTime s)
		{
			_gbuObjectAttribute.S = s;
			return this;
		}

		public AGbuObjectAttributeBuilder OtDate(DateTime ot)
		{
			_gbuObjectAttribute.Ot = ot;
			return this;
		}

		public AGbuObjectAttributeBuilder Value(string value)
		{
			_gbuObjectAttribute.StringValue = value;
			return this;
		}


		public abstract GbuObjectAttribute Build();
		public abstract AGbuObjectAttributeBuilder ShallowCopy();
	}
}