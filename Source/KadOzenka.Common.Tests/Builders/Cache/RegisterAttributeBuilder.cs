using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Misc;

namespace KadOzenka.Common.Tests.Builders.Cache
{
	public class RegisterAttributeBuilder
	{
		private RegisterAttribute _attribute;

		public RegisterAttributeBuilder()
		{
			_attribute = new RegisterAttribute
			{
				Id = RandomGenerator.GenerateRandomInteger(),
				Name = RandomGenerator.GetRandomString(),
				InternalName = RandomGenerator.GetRandomString(),
				RegisterId = RandomGenerator.GenerateRandomInteger(),
				Type = RegisterAttributeType.STRING,
				ParentId = RandomGenerator.GenerateRandomInteger(),
				ReferenceId = RandomGenerator.GenerateRandomInteger(),
				RegisterName = RandomGenerator.GetRandomString(),
				ValueField = RandomGenerator.GetRandomString(),
				CodeField = RandomGenerator.GetRandomString(),
				IsNullable = true,
				ValueFieldType = RandomGenerator.GetRandomString(),
				CodeFieldType = RandomGenerator.GetRandomString(),
				ValueTemplate = RandomGenerator.GetRandomString(),
				ReferenceName = RandomGenerator.GetRandomString(),
				ChildsCount = RandomGenerator.GenerateRandomInteger(),
				IsPrimaryKey = false,
				UserKey = RandomGenerator.GenerateRandomInteger(),
				DataLength = RandomGenerator.GenerateRandomInteger(),
				DataPrecision = RandomGenerator.GenerateRandomInteger(),
				DataScale = RandomGenerator.GenerateRandomInteger(),
				//QSColumn = 
				IsVirtual = false,
				Description = RandomGenerator.GetRandomString(),
				LayoutTextAlign = RandomGenerator.GetRandomString(),
				LayoutFormat = RandomGenerator.GetRandomString(),
				LayoutStyle = new StyleConditionItemWrapper(),
				LayoutSet = true,
				IsDeleted = false,
				Hidden = false
			};
		}


		public RegisterAttribute Build()
		{
			return _attribute;
		}
	}
}
